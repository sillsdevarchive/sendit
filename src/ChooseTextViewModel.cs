using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SendIt.Properties;

namespace SendIt
{
	public class ChooseTextViewModel
	{
		private readonly IChoiceProvider _choiceProvider;
		private readonly IFileProcessor _fileProcessor;
		private BackgroundWorker _worker;

		public ChooseTextViewModel( IChoiceProvider choiceProvider, IFileProcessor fileProcessor)
		{
			_choiceProvider = choiceProvider;
			_fileProcessor = fileProcessor;
			AllowSetup = Control.ModifierKeys == Keys.Shift;
		}

		public IEnumerable<FileChoice> GetPathChoices()
		{
			return _choiceProvider.GetChoices();
		}

		public FileChoice SelectedPath { get; set; }

		public bool CanSend
		{
			get { return !Busy && SelectedPath != null;}
		}

		private void DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				SendingStatus = "Getting file from AdaptIt...";
				var processedFile = _fileProcessor.Process(SelectedPath.Path);

				SendingStatus = "Zipping file...";
				string zipPath = GetZipFile(processedFile);

				SendingStatus = "Preparing email message...";
				SendEmail(zipPath);
			}
			catch (Exception error)
			{
				e.Result = error;
			}
		}

		private void SendEmail(string zipPath)
		{
			//don't worry about certificates
			ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

			var client = new System.Net.Mail.SmtpClient(Settings.Default.SendItSettings.SmtpClient, Settings.Default.SendItSettings.SmtpClientPort);
			client.DeliveryMethod = SmtpDeliveryMethod.Network;
			client.EnableSsl = Settings.Default.SendItSettings.SmtpUsesSsl;

			client.Credentials = new NetworkCredential(Settings.Default.SendItSettings.SmptLogin, Settings.Default.SendItSettings.SmtpPassword);
			MailAddress from = new MailAddress(Settings.Default.SendItSettings.FromAddress, Settings.Default.SendItSettings.FromAddress);
			MailAddress to = new MailAddress(Settings.Default.SendItSettings.ToAddress, Settings.Default.SendItSettings.ToAddress);
			MailMessage message = new MailMessage(from, to);
			message.Subject = SelectedPath.ToString();
			Attachment file = new Attachment(zipPath);
			message.Attachments.Add(file);
			//client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);

			SendingStatus = "Giving Message To Mail Server...";
			client.Send(message);

		}

		private string GetZipFile(string processedFile)
		{
			var zipPath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(SelectedPath.Path)+".zip");
			var zip = ZipStorer.Create(zipPath, string.Empty);
			zip.AddFile(ZipStorer.Compression.Store, processedFile, Path.GetFileName(processedFile), string.Empty);
			zip.Close();
			return zipPath;
		}

		public void ExportAndZipAndSendEmailAsync()
		{
			try
			{
				_worker = new BackgroundWorker();
				_worker.DoWork += DoWork;
				_worker.RunWorkerCompleted += WorkerCompleted;
				_worker.RunWorkerAsync();
			}
			catch (Exception error)
			{
				SendingStatus = error.Message;
				throw;
			}
		}

		private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				using (SoundPlayer player = new SoundPlayer(Properties.Resources.error))
				{
					player.Play();
				}
				SendingStatus = e.Error.Message;
			}
			else if (e.Result != null)
			{
				using (SoundPlayer player = new SoundPlayer(Properties.Resources.error))
				{
					player.Play();
				}
				SendingStatus = ((Exception) e.Result).Message;
				if (null != ((Exception)e.Result).InnerException)
					SendingStatus += ": " + ((Exception)e.Result).InnerException.Message;
			}
			else
			{
				using (SoundPlayer player = new SoundPlayer(Properties.Resources.finished))
				{
					player.Play();
				}
				SendingStatus = "Done";
				ShouldReportSuccessAndClose = true;
			}
		}

		public string SendingStatus { get; private set; }



		public bool ShouldReportSuccessAndClose { get; private set; }

		public bool Busy
		{
			get
			{
				return _worker != null && _worker.IsBusy;
			}
		}

		public bool AllowSetup { get; set; }

		public bool CheckSetupIsCompleteAndNotifyIfNeeded()
		{

		   var strings = new[] {Settings.Default.SendItSettings.PathToAdaptationsFolder, Settings.Default.SendItSettings.FromAddress, Settings.Default.SendItSettings.ToAddress, Settings.Default.SendItSettings.SmptLogin, Settings.Default.SendItSettings.SmtpPassword};
		   if (strings.Any(s => string.IsNullOrEmpty(s))
					   || (default(int) == Settings.Default.SendItSettings.SmtpClientPort))
		   {
			   MessageBox.Show(
				  "Sorry, SendIt does not have all the information it needs to send.  Use the Setup button to add that information.");
			   return false;
		   }

		   if (!Directory.Exists(Settings.Default.SendItSettings.PathToAdaptationsFolder))
		   {
			   MessageBox.Show(
				  "Sorry, the path to the adaptations folder is incorrect.  Use the Setup button to fix it.");
			   return false;
		   }
			return true;
		}
	}
}
