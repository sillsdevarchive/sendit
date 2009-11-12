using System;
using System.Windows.Forms;

namespace SendIt
{
	public partial class ChooseTextView : Form
	{
		private readonly ChooseTextViewModel _model;

		public ChooseTextView(ChooseTextViewModel model)
		{
			_model = model;
			InitializeComponent();
		}

		void UpdateDisplay()
		{
			timer1.Enabled = false;
			if (_model.ShouldReportSuccessAndClose)
			{
				MessageBox.Show("The text was given to the email system for delivery.");
				Close();
			}
			_sendButton.Enabled = _model.CanSend;
			_status.Text = _model.SendingStatus;
			listBox1.Enabled = !_model.Busy;
			timer1.Enabled = true;
			Cursor = _model.Busy ? Cursors.WaitCursor: Cursors.Default;

		}

		private void _sendButton_Click(object sender, EventArgs e)
		{
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

		private void _cancelButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			UpdateDisplay();
		}

	}
}
