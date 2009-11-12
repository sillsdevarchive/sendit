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
			_sendButton.Enabled = _model.CanSend;
		}

		private void _sendButton_Click(object sender, EventArgs e)
		{
			if(listBox1.SelectedItem == null)
				return;
			_model.SendPath();
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

	}
}
