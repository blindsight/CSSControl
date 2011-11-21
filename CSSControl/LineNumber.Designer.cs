namespace CSSControl
{
	partial class LineNumber
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
			if (disposing && (components != null)) {
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
			this.OkBtn = new System.Windows.Forms.Button();
			this.lineNumBox = new System.Windows.Forms.TextBox();
			this.lineNumLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// OkBtn
			// 
			this.OkBtn.Location = new System.Drawing.Point(113, 51);
			this.OkBtn.Name = "OkBtn";
			this.OkBtn.Size = new System.Drawing.Size(50, 23);
			this.OkBtn.TabIndex = 1;
			this.OkBtn.Text = "Go";
			this.OkBtn.UseVisualStyleBackColor = true;
			this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
			// 
			// lineNumBox
			// 
			this.lineNumBox.Location = new System.Drawing.Point(9, 25);
			this.lineNumBox.Name = "lineNumBox";
			this.lineNumBox.Size = new System.Drawing.Size(154, 20);
			this.lineNumBox.TabIndex = 2;
			// 
			// lineNumLabel
			// 
			this.lineNumLabel.AutoSize = true;
			this.lineNumLabel.Location = new System.Drawing.Point(12, 9);
			this.lineNumLabel.Name = "lineNumLabel";
			this.lineNumLabel.Size = new System.Drawing.Size(67, 13);
			this.lineNumLabel.TabIndex = 3;
			this.lineNumLabel.Text = "Line Number";
			// 
			// LineNumber
			// 
			this.AcceptButton = this.OkBtn;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(175, 85);
			this.Controls.Add(this.lineNumLabel);
			this.Controls.Add(this.lineNumBox);
			this.Controls.Add(this.OkBtn);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LineNumber";
			this.ShowInTaskbar = false;
			this.Text = "Go To";
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button OkBtn;
		private System.Windows.Forms.TextBox lineNumBox;
		private System.Windows.Forms.Label lineNumLabel;
	}
}