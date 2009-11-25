using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SendIt.Properties;

namespace SendIt
{
	public partial class SettingsEditor : Form
	{

		public SettingsEditor()
		{
			InitializeComponent();
			this.propertyGrid1.SelectedObject = SendItSettings.Singleton;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private void _setUUPlusPresets_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			SendItSettings.Singleton.SmtpUsesSsl = false;
			SendItSettings.Singleton.SmtpClient = "127.0.0.1";
			SendItSettings.Singleton.SmtpClientPort = 25;
			propertyGrid1.Refresh();
		}
	}
}