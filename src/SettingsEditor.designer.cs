namespace SendIt
{
	partial class SettingsEditor
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
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.button1 = new System.Windows.Forms.Button();
			this._setUUPlusPresets = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			//
			// propertyGrid1
			//
			this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.propertyGrid1.Location = new System.Drawing.Point(-1, 0);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(449, 337);
			this.propertyGrid1.TabIndex = 0;
			this.propertyGrid1.ToolbarVisible = false;
			//
			// button1
			//
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(358, 345);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			//
			// _setUUPlusPresets
			//
			this._setUUPlusPresets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._setUUPlusPresets.AutoSize = true;
			this._setUUPlusPresets.Location = new System.Drawing.Point(13, 345);
			this._setUUPlusPresets.Name = "_setUUPlusPresets";
			this._setUUPlusPresets.Size = new System.Drawing.Size(81, 13);
			this._setUUPlusPresets.TabIndex = 2;
			this._setUUPlusPresets.TabStop = true;
			this._setUUPlusPresets.Text = "UUPlus Presets";
			this._setUUPlusPresets.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._setUUPlusPresets_LinkClicked);
			//
			// SettingsEditor
			//
			this.AcceptButton = this.button1;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(445, 380);
			this.Controls.Add(this._setUUPlusPresets);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.propertyGrid1);
			this.Name = "SettingsEditor";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Set up SendIt...";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.LinkLabel _setUUPlusPresets;
	}
}