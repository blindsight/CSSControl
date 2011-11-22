namespace CSSControl
{
    partial class PreviewForm
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
			this.webPreview = new System.Windows.Forms.WebBrowser();
			this.webUrl = new System.Windows.Forms.TextBox();
			this.btnGo = new System.Windows.Forms.Button();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.overrideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gBvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.iE7ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.iE8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.iE8ForceStandardsModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.iE9ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.iE9ForceStandardsModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			//
			// webPreview
			//
			this.webPreview.Location = new System.Drawing.Point(1, 80);
			this.webPreview.MinimumSize = new System.Drawing.Size(20, 20);
			this.webPreview.Name = "webPreview";
			this.webPreview.ScriptErrorsSuppressed = true;
			this.webPreview.Size = new System.Drawing.Size(853, 564);
			this.webPreview.TabIndex = 0;
			this.webPreview.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webPreview_DocumentCompleted);
			this.webPreview.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webPreview_Navigated);
			// 
			// webUrl
			// 
			this.webUrl.Location = new System.Drawing.Point(13, 30);
			this.webUrl.Name = "webUrl";
			this.webUrl.Size = new System.Drawing.Size(772, 20);
			this.webUrl.TabIndex = 1;
			this.webUrl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.webUrl_KeyPress);
			// 
			// btnGo
			// 
			this.btnGo.Location = new System.Drawing.Point(792, 26);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(51, 23);
			this.btnGo.TabIndex = 2;
			this.btnGo.Text = "Go";
			this.btnGo.UseVisualStyleBackColor = true;
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.overrideToolStripMenuItem,
            this.gBvToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(855, 24);
			this.menuStrip1.TabIndex = 3;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// overrideToolStripMenuItem
			// 
			this.overrideToolStripMenuItem.Name = "overrideToolStripMenuItem";
			this.overrideToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
			this.overrideToolStripMenuItem.Text = "Override";
			// 
			// gBvToolStripMenuItem
			// 
			this.gBvToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iE7ToolStripMenuItem,
            this.iE8ToolStripMenuItem,
            this.iE8ForceStandardsModeToolStripMenuItem,
            this.iE9ToolStripMenuItem,
            this.iE9ForceStandardsModeToolStripMenuItem});
			this.gBvToolStripMenuItem.Name = "gBvToolStripMenuItem";
			this.gBvToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
			this.gBvToolStripMenuItem.Text = "Browser Mode";
			this.gBvToolStripMenuItem.Visible = false;
			// 
			// iE7ToolStripMenuItem
			// 
			this.iE7ToolStripMenuItem.CheckOnClick = true;
			this.iE7ToolStripMenuItem.Name = "iE7ToolStripMenuItem";
			this.iE7ToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
			this.iE7ToolStripMenuItem.Text = "IE 7";
			this.iE7ToolStripMenuItem.Click += new System.EventHandler(this.iE7ToolStripMenuItem_Click);
			// 
			// iE8ToolStripMenuItem
			// 
			this.iE8ToolStripMenuItem.CheckOnClick = true;
			this.iE8ToolStripMenuItem.Name = "iE8ToolStripMenuItem";
			this.iE8ToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
			this.iE8ToolStripMenuItem.Text = "IE 8";
			this.iE8ToolStripMenuItem.Click += new System.EventHandler(this.iE8ToolStripMenuItem_Click);
			// 
			// iE8ForceStandardsModeToolStripMenuItem
			// 
			this.iE8ForceStandardsModeToolStripMenuItem.CheckOnClick = true;
			this.iE8ForceStandardsModeToolStripMenuItem.Name = "iE8ForceStandardsModeToolStripMenuItem";
			this.iE8ForceStandardsModeToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
			this.iE8ForceStandardsModeToolStripMenuItem.Text = "IE 8 (Force Standards Mode)";
			this.iE8ForceStandardsModeToolStripMenuItem.Click += new System.EventHandler(this.iE8ForceStandardsModeToolStripMenuItem_Click);
			// 
			// iE9ToolStripMenuItem
			// 
			this.iE9ToolStripMenuItem.CheckOnClick = true;
			this.iE9ToolStripMenuItem.Name = "iE9ToolStripMenuItem";
			this.iE9ToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
			this.iE9ToolStripMenuItem.Text = "IE 9";
			this.iE9ToolStripMenuItem.Click += new System.EventHandler(this.iE9ToolStripMenuItem_Click);
			// 
			// iE9ForceStandardsModeToolStripMenuItem
			// 
			this.iE9ForceStandardsModeToolStripMenuItem.CheckOnClick = true;
			this.iE9ForceStandardsModeToolStripMenuItem.Name = "iE9ForceStandardsModeToolStripMenuItem";
			this.iE9ForceStandardsModeToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
			this.iE9ForceStandardsModeToolStripMenuItem.Text = "IE 9 (Force Standards Mode)";
			this.iE9ForceStandardsModeToolStripMenuItem.Click += new System.EventHandler(this.iE9ForceStandardsModeToolStripMenuItem_Click);
			// 
			// PreviewForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(855, 642);
			this.Controls.Add(this.btnGo);
			this.Controls.Add(this.webUrl);
			this.Controls.Add(this.webPreview);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "PreviewForm";
			this.Text = "Preview";
			this.SizeChanged += new System.EventHandler(this.PreviewForm_SizeChanged);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webPreview;
        private System.Windows.Forms.TextBox webUrl;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem overrideToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem gBvToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem iE7ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem iE8ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem iE8ForceStandardsModeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem iE9ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem iE9ForceStandardsModeToolStripMenuItem;

    }
}