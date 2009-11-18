using System.Drawing;
using System.Windows.Forms;

namespace SendIt
{
	public partial class Shell : Form
	{
		private readonly ChooseTextView _chooseTextView;

		public Shell(ChooseTextView chooseTextView)
		{
			_chooseTextView = chooseTextView;
			InitializeComponent();
			this.Size = new Size(chooseTextView.Size.Width + 10, chooseTextView.Height + 10);
			chooseTextView.Dock = DockStyle.Fill;
			chooseTextView._cancelButton.Click += ((sender, e) => Close());
			this.Controls.Add(chooseTextView);
			timer1.Enabled = true;
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			if (_chooseTextView.Model.ShouldReportSuccessAndClose)
			{
				timer1.Enabled = false;
				Controls.Remove(_chooseTextView);
				var successView = new SuccessView();
				successView._closeButton.Click += ((s, args) => Close());
				this.AcceptButton = successView._closeButton;
				this.CancelButton = successView._closeButton;
				successView._closeButton.Focus();
				successView.Dock = DockStyle.Fill;
				Controls.Add(successView);
			}
		}


	}
}
