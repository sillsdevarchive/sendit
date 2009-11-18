namespace SendIt
{
    partial class ChooseTextView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this._cancelButton = new System.Windows.Forms.Button();
            this._status = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this._sendButton = new System.Windows.Forms.Button();
            this._setupButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(12, 53);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(343, 276);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select the AdaptIt text to send on email:";
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(279, 362);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(76, 32);
            this._cancelButton.TabIndex = 2;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            // 
            // _status
            // 
            this._status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._status.AutoSize = true;
            this._status.Location = new System.Drawing.Point(12, 333);
            this._status.MinimumSize = new System.Drawing.Size(350, 0);
            this._status.Name = "_status";
            this._status.Size = new System.Drawing.Size(350, 13);
            this._status.TabIndex = 1;
            this._status.Text = "Status";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // _sendButton
            // 
            this._sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._sendButton.Image = global::SendIt.Properties.Resources.mail_message_new;
            this._sendButton.Location = new System.Drawing.Point(166, 362);
            this._sendButton.Name = "_sendButton";
            this._sendButton.Size = new System.Drawing.Size(90, 32);
            this._sendButton.TabIndex = 2;
            this._sendButton.Text = "Send";
            this._sendButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._sendButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._sendButton.UseVisualStyleBackColor = true;
            this._sendButton.Click += new System.EventHandler(this._sendButton_Click);
            // 
            // _setupButton
            // 
            this._setupButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._setupButton.Image = global::SendIt.Properties.Resources.lock13x16;
            this._setupButton.Location = new System.Drawing.Point(15, 362);
            this._setupButton.Name = "_setupButton";
            this._setupButton.Size = new System.Drawing.Size(81, 32);
            this._setupButton.TabIndex = 3;
            this._setupButton.Text = "Setup...";
            this._setupButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._setupButton.UseVisualStyleBackColor = true;
            this._setupButton.Click += new System.EventHandler(this._setupButton_Click);
            // 
            // ChooseTextView
            // 
            this.Controls.Add(this._setupButton);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._sendButton);
            this.Controls.Add(this._status);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Name = "ChooseTextView";
            this.Size = new System.Drawing.Size(367, 406);
            this.VisibleChanged += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _sendButton;
        private System.Windows.Forms.Label _status;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button _setupButton;
        public System.Windows.Forms.Button _cancelButton;
    }
}

