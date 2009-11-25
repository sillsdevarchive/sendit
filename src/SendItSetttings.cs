using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;
using System.Xml.Serialization;

namespace SendIt
{
	[Serializable]
	public class SendItSettings
	{
		private static SendItSettings _singleton;
		private string kversion = "0";

		static public SendItSettings Singleton
		{
			get
			{
				if(_singleton == null)
				{
					Load();
				}
				return _singleton;
			}
		}
		public void Save()
		{
			using (TextWriter writer = new StreamWriter(GetPath()))
			{
				XmlSerializer serializer = new XmlSerializer(this.GetType());
				serializer.Serialize(writer, this);
			}
		}
		private static void Load()
		{
			try
			{
				using (var reader = File.OpenRead(GetPath()))
				{
					XmlSerializer serializer = new XmlSerializer(typeof (SendItSettings));
					_singleton = serializer.Deserialize(reader) as SendItSettings;
				}
			}
			catch (Exception)
			{
				_singleton = new SendItSettings();
				_singleton.SmtpClient = "127.0.0.1";
				_singleton.SmtpClientPort = 25;
			}
		}
		private static string GetPath()
		{
			var folder =Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			folder = Path.Combine(folder, "SendIt");
			return Path.Combine(folder, "settings.xml");
		}

		[Category("AdaptIt")]
		[Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Description(@"E.g., C:\Users\John\Documents\Adapt It Work\Palaung-B to Rulai adaptations\Adaptations")]
		public string PathToAdaptationsFolder
		{
			get;
			set;
		}

		[Category("Email Server")]
		[Description("E.g. smtp.google.com,  ap83.sil.org.pg, or 127.0.0.1")]
		public string SmtpClient { get; set; }

		[Category("Email Server")]
		public int SmtpClientPort
		{
			get;
			set;
		}
		[Category("Email Server")]
		public bool SmtpUsesSsl
		{
			get;
			set;
		}

		[Category("Email Account")]
		public string SmptLogin
		{
			get;
			set;
		}

		[Category("Email Account")]
		public string SmtpPassword
		{
			get;
			set;
		}




		[Category("Email Message")]
		public string FromAddress
		{
			get;
			set;
		}

		[Category("Email Message")]
		public string ToAddress
		{
			get;
			set;
		}
	}
}
