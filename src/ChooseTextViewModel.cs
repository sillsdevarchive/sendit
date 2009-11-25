using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
			if (!string.IsNullOrEmpty(SendItSettings.Singleton.MailServerPath))
			{
				if (File.Exists(SendItSettings.Singleton.MailServerPath))
				{
					string expectedProcessName = Path.GetFileNameWithoutExtension(SendItSettings.Singleton.MailServerPath);
					var serverIsRunning = System.Diagnostics.Process.GetProcessesByName(expectedProcessName).Length > 0;
					if (!serverIsRunning)
					{
						string fileName = Path.GetFileName(SendItSettings.Singleton.MailServerPath);
						SendingStatus = "Starting " + fileName + "...";
						ProcessStartInfo startInfo = new ProcessStartInfo(SendItSettings.Singleton.MailServerPath);
						startInfo.WorkingDirectory = Path.GetDirectoryName(SendItSettings.Singleton.MailServerPath);
						startInfo.UseShellExecute = true;
						var  proc = Process.Start(startInfo);
						Thread.Sleep(5000);
						serverIsRunning = System.Diagnostics.Process.GetProcessesByName(expectedProcessName).Length > 0;
						if (!serverIsRunning)
						{
							throw new ApplicationException("SendIt could not start email server (" + fileName +
														   "). Restart your computer and try again.");
						}
					}
				}
			}
			//don't worry about certificates
			ServicePointManager.ServerCertificateValidationCallback =
				delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
					{ return true; };

			var client = new System.Net.Mail.SmtpClient(SendItSettings.Singleton.SmtpClient,
														SendItSettings.Singleton.SmtpClientPort);
			client.DeliveryMethod = SmtpDeliveryMethod.Network;
			client.EnableSsl = SendItSettings.Singleton.SmtpUsesSsl;

			client.Credentials = new NetworkCredential(SendItSettings.Singleton.SmptLogin,
													   SendItSettings.Singleton.SmtpPassword);
			MailAddress from = new MailAddress(SendItSettings.Singleton.FromAddress,
											   SendItSettings.Singleton.FromAddress);
			MailAddress to = new MailAddress(SendItSettings.Singleton.ToAddress, SendItSettings.Singleton.ToAddress);
			MailMessage message = new MailMessage(from, to);
			message.Subject = SelectedPath.ToString();
			Attachment file = new Attachment(zipPath);
			message.Attachments.Add(file);
			//client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);

			SendingStatus = "Giving Message To Mail Server...";
			try
			{
				client.Send(message);
			}
			catch(Exception error)
			{
				MessageBox.Show("SendIt could not talk the email server/program. You might try restarting your computer before trying again.\r\n"+error.Message, "SendIt Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
				throw;
			}

		}

		private string GetZipFile(string processedFile)
		{
			var zipPath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(SelectedPath.Path)+".zip");
			var zip = ZipStorer.Create(zipPath, string.Empty);
			zip.AddFile(ZipStorer.Compression.Deflate, processedFile, Path.GetFileName(processedFile), string.Empty);
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

		   var strings = new[] {SendItSettings.Singleton.PathToAdaptationsFolder, SendItSettings.Singleton.FromAddress, SendItSettings.Singleton.ToAddress, SendItSettings.Singleton.SmptLogin, SendItSettings.Singleton.SmtpPassword};
		   if (strings.Any(s => string.IsNullOrEmpty(s))
					   || (default(int) == SendItSettings.Singleton.SmtpClientPort))
		   {
			   MessageBox.Show(
				  "Sorry, SendIt does not have all the information it needs to send.  Use the Setup button to add that information.");
			   return false;
		   }

		   if (!Directory.Exists(SendItSettings.Singleton.PathToAdaptationsFolder))
		   {
			   MessageBox.Show(
				  "Sorry, the path to the adaptations folder is incorrect.  Use the Setup button to fix it.");
			   return false;
		   }
			return true;
		}

		public void SetupClick()
		{
			if (!AllowSetup)
			{
				MessageBox.Show(
					"Changing the settings can break SendIt. If you are sure you know about your email accounts, passwords, and locations of AdaptIt Files, etc., then restart SendIt with the Shift key held down, and then you will be able to change the settings.");
				return;
			}

			using (var dlg = new SettingsEditor())
			{
				DialogResult result = dlg.ShowDialog();
				if (System.Windows.Forms.DialogResult.Cancel != result)
					SendItSettings.Singleton.Save();
			}
		}
	}
}
