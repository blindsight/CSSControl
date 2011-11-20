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
            this.overrideToolStripMenuItem});
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

    }
}