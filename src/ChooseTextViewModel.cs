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
			ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

			var client = new System.Net.Mail.SmtpClient("ap83.sil.org.pg", 465);
			client.DeliveryMethod = SmtpDeliveryMethod.Network;
			client.EnableSsl = true;

			client.Credentials = new NetworkCredential("j.hatton@sil.org.pg", "chiang99");
			MailAddress from = new MailAddress("j.hatton@sil.org.pg", "john hatton");
			MailAddress to = new MailAddress("j.hatton@sil.org.pg", "john hatton");
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
				SendingStatus = ((Exception)e.Result).Message + ": "+((Exception)e.Result).InnerException.Message;
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
	}
}
