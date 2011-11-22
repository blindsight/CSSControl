namespace CSSControl
{
	partial class Options
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
			this.optionsTab = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.browserTab = new System.Windows.Forms.TabPage();
			this.browserCheckBox = new System.Windows.Forms.CheckedListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.browserModeLbl = new System.Windows.Forms.Label();
			this.ApplyBtn = new System.Windows.Forms.Button();
			this.futureModeLbl = new System.Windows.Forms.Label();
			this.optionsTab.SuspendLayout();
			this.browserTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// optionsTab
			// 
			this.optionsTab.Controls.Add(this.tabPage1);
			this.optionsTab.Controls.Add(this.browserTab);
			this.optionsTab.Location = new System.Drawing.Point(12, 12);
			this.optionsTab.Name = "optionsTab";
			this.optionsTab.SelectedIndex = 0;
			this.optionsTab.Size = new System.Drawing.Size(504, 385);
			this.optionsTab.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(496, 359);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// browserTab
			// 
			this.browserTab.Controls.Add(this.futureModeLbl);
			this.browserTab.Controls.Add(this.browserCheckBox);
			this.browserTab.Controls.Add(this.label1);
			this.browserTab.Controls.Add(this.browserModeLbl);
			this.browserTab.Location = new System.Drawing.Point(4, 22);
			this.browserTab.Name = "browserTab";
			this.browserTab.Padding = new System.Windows.Forms.Padding(3);
			this.browserTab.Size = new System.Drawing.Size(496, 359);
			this.browserTab.TabIndex = 1;
			this.browserTab.Text = "Browser Options";
			this.browserTab.UseVisualStyleBackColor = true;
			// 
			// browserCheckBox
			// 
			this.browserCheckBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.browserCheckBox.FormattingEnabled = true;
			this.browserCheckBox.Items.AddRange(new object[] {
            "IE 7",
            "IE 8",
            "IE 8 Forced Standards",
            "IE 9 ",
            "IE 9 Forced Standards"});
			this.browserCheckBox.Location = new System.Drawing.Point(240, 35);
			this.browserCheckBox.Name = "browserCheckBox";
			this.browserCheckBox.Size = new System.Drawing.Size(149, 90);
			this.browserCheckBox.TabIndex = 2;
			this.browserCheckBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.browserCheckBox_ItemCheck);
			this.browserCheckBox.SelectedIndexChanged += new System.EventHandler(this.browserCheckBox_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(74, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(148, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Requires restart of Application";
			// 
			// browserModeLbl
			// 
			this.browserModeLbl.AutoSize = true;
			this.browserModeLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.browserModeLbl.Location = new System.Drawing.Point(131, 35);
			this.browserModeLbl.Name = "browserModeLbl";
			this.browserModeLbl.Size = new System.Drawing.Size(91, 13);
			this.browserModeLbl.TabIndex = 0;
			this.browserModeLbl.Text = "Browser Mode:";
			// 
			// ApplyBtn
			// 
			this.ApplyBtn.Location = new System.Drawing.Point(437, 414);
			this.ApplyBtn.Name = "ApplyBtn";
			this.ApplyBtn.Size = new System.Drawing.Size(75, 23);
			this.ApplyBtn.TabIndex = 1;
			this.ApplyBtn.Text = "Apply";
			this.ApplyBtn.UseVisualStyleBackColor = true;
			this.ApplyBtn.Click += new System.EventHandler(this.ApplyBtn_Click);
			// 
			// futureModeLbl
			// 
			this.futureModeLbl.AutoSize = true;
			this.futureModeLbl.Location = new System.Drawing.Point(240, 117);
			this.futureModeLbl.Name = "futureModeLbl";
			this.futureModeLbl.Size = new System.Drawing.Size(0, 13);
			this.futureModeLbl.TabIndex = 3;
			// 
			// Options
			// 
			this.AcceptButton = this.ApplyBtn;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(533, 449);
			this.Controls.Add(this.ApplyBtn);
			this.Controls.Add(this.optionsTab);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Options";
			this.ShowInTaskbar = false;
			this.Text = "Options";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Options_FormClosing);
			this.optionsTab.ResumeLayout(false);
			this.browserTab.ResumeLayout(false);
			this.browserTab.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl optionsTab;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage browserTab;
		private System.Windows.Forms.Button ApplyBtn;
		private System.Windows.Forms.Label browserModeLbl;
		private System.Windows.Forms.CheckedListBox browserCheckBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label futureModeLbl;
	}
}