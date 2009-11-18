using System;
using System.Windows.Forms;
using SendIt.Properties;

namespace SendIt
{
	public partial class ChooseTextView : UserControl
	{
		private readonly ChooseTextViewModel _model;

		public ChooseTextView(ChooseTextViewModel model)
		{
			_model = model;
			InitializeComponent();
		}

		public ChooseTextViewModel Model
		{
			get
			{
				return _model;
			}
		}

		void UpdateDisplay()
		{
			timer1.Enabled = false;
			if (_model.AllowSetup)
			{
				this._setupButton.Image = global::SendIt.Properties.Resources.preferences_system;
			}
			else
			{
				this._setupButton.Image = global::SendIt.Properties.Resources.lock13x16;
			}
			_sendButton.Enabled = _model.CanSend;
			_status.Text = _model.SendingStatus;
			listBox1.Enabled = !_model.Busy;
			timer1.Enabled = true;
			Cursor = _model.Busy ? Cursors.WaitCursor: Cursors.Default;

		}

		private void _sendButton_Click(object sender, EventArgs e)
		{
			if (!_model.CheckSetupIsCompleteAndNotifyIfNeeded())
			{
				return;
			}
			if(listBox1.SelectedItem == null)
				return;
			Cursor = Cursors.WaitCursor;
			_model.ExportAndZipAndSendEmailAsync();
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			_model.SelectedPath = listBox1.SelectedItem as FileChoice;
			UpdateDisplay();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			foreach (var choice in _model.GetPathChoices())
			{
				listBox1.Items.Add(choice);
			}


			UpdateDisplay();
		}


		private void timer1_Tick(object sender, EventArgs e)
		{
			UpdateDisplay();
		}

		private void _setupButton_Click(object sender, EventArgs e)
		{
			if (!_model.AllowSetup)
			{
				MessageBox.Show(
					"Changing the settings can break SendIt. If you are sure you know about your email accounts, passwords, and locations of AdaptIt Files, etc., then restart SendIt with the Shift key held down, and then you will be able to change the settings.");
				return;
			}

			using (var dlg = new SettingsEditor())
			{
				DialogResult result = dlg.ShowDialog();
				if (System.Windows.Forms.DialogResult.Cancel != result)
					Settings.Default.Save();
			}
		}

	}
}
