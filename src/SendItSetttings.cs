using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;

namespace SendIt
{
	[Serializable]
	public class SendItSetttings
	{
		[Category("AdaptIt")]
		[Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Description(@"E.g., C:\Users\John\Documents\Adapt It Work\Palaung-B to Rulai adaptations")]
		public string PathToAdaptationsFolder
		{
			get;
			set;
		}

		[Category("Email Server")]
		[Description("E.g. smtp.google.com,  ap83.sil.org.pg, or 127.0.0.1")]
		public string SmtpClient { get; set; }

		[Category("Email Server")]
		[DefaultValue(25)]
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
