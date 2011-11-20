using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mshtml;
using System.IO;
using System.Net;

namespace CSSControl
{
    public partial class PreviewForm : Form
    {
        protected int editCssSheet = 0;
        protected int webStartHeight;

        public PreviewForm()
        {
            InitializeComponent();
            webStartHeight = webPreview.Top;
            webUrl.Text = "http://tailormadeprinting.com";
        }

        private void PreviewForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.Height > webStartHeight)
            {
                webPreview.Width = this.Width;
                webPreview.Height = this.Height - webStartHeight;
            }
        }

        public void setCSSRules(string NewRule, string overRideUrl)
        {
           if (editCssSheet > 0)
            {
                IHTMLDocument2 currentDocument = (IHTMLDocument2)webPreview.Document.DomDocument;
                IHTMLStyleSheet styleSheet;

                if (overRideUrl.Equals(""))
                {
                    styleSheet = currentDocument.styleSheets.item(editCssSheet);
                }
                else
                {
                    styleSheet = currentDocument.styleSheets.item(editCssSheet);

                    foreach (IHTMLStyleSheet currentSheet in currentDocument.styleSheets)
                    {
                        if (currentSheet.href == overRideUrl)
                        {
                            styleSheet = currentSheet;
                        }
                    }
                }


                styleSheet.cssText = NewRule;
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            webPreview.Navigate(webUrl.Text);
        }

        private void webPreview_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            resetOverrideMenu();

            IHTMLDocument2 currentDocument = (IHTMLDocument2)webPreview.Document.DomDocument;

            editCssSheet = currentDocument.styleSheets.length;
            IHTMLStyleSheet styleSheet = currentDocument.createStyleSheet(@"", editCssSheet);
        }

        public void resetOverrideMenu()
        {
            overrideToolStripMenuItem.DropDownItems.Clear();

            IHTMLDocument2 currentDocument = (IHTMLDocument2)webPreview.Document.DomDocument;

            foreach (IHTMLStyleSheet currentSheet in currentDocument.styleSheets)
            {
                if (currentSheet.href == null) continue;

                ToolStripMenuItem currentItem = new ToolStripMenuItem(currentSheet.href);
                overrideToolStripMenuItem.DropDownItems.Add(currentItem);

                EditorForm editor = (EditorForm)this.Owner;

                foreach (String[] currentCss in editor.ListAllCssFiles())
                {
                    string name = currentCss[0];
                    string href = currentCss[1];

                    if (href == null)
                    {
                        ToolStripItem documentMenu = currentItem.DropDownItems.Add(name);
                        documentMenu.Click += new EventHandler(documentMenu_Click);
                    }
                    else if(href.Equals(currentSheet.href))
                    {
                        ToolStripMenuItem documentMenu = (ToolStripMenuItem)currentItem.DropDownItems.Add(name);
                        documentMenu.Checked = true;
                        
                        documentMenu.Click += new EventHandler(documentMenu_Click);
                    }
                }

                ToolStripItem extractMenu = currentItem.DropDownItems.Add("extract and override");
                extractMenu.Click += new EventHandler(extractMenu_Click);
            }
        }

        void documentMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem fileMenu = (ToolStripMenuItem)sender;
            EditorForm editor = (EditorForm)this.Owner;

            if (fileMenu.Checked == true)
            {
                editor.UnSetOverride(fileMenu.Text, fileMenu.OwnerItem.Text);

                IHTMLDocument2 currentDocument = (IHTMLDocument2)webPreview.Document.DomDocument;

                foreach (IHTMLStyleSheet currentSheet in currentDocument.styleSheets)
                {
                    if (fileMenu.OwnerItem.Text == currentSheet.href)
                    {

                        WebRequest req = WebRequest.Create(currentSheet.href);
                        WebResponse response = req.GetResponse();
                        Stream stream = response.GetResponseStream();
                        //TODO: should this be saved in memory in case there the connect to the internet is lost?
                        StreamReader readStream = new StreamReader(stream);
                        currentSheet.cssText = readStream.ReadToEnd();
                        fileMenu.Checked = false;
                        break;
                    }
                }
            } else 
            {
                fileMenu.Checked = true;

                editor.SetOverride(fileMenu.Text, fileMenu.OwnerItem.Text);
            }
        }

        void extractMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem extractMenu = (ToolStripMenuItem)sender;
            IHTMLDocument2 currentDocument = (IHTMLDocument2)webPreview.Document.DomDocument;


            foreach (IHTMLStyleSheet currentSheet in currentDocument.styleSheets)
            {
                if (extractMenu.OwnerItem.Text == currentSheet.href)
                {
                    EditorForm editor = (EditorForm)this.Owner;

                    WebRequest req = WebRequest.Create(currentSheet.href);
                    WebResponse response = req.GetResponse();
                    Stream stream = response.GetResponseStream();

                    StreamReader readStream = new StreamReader(stream);


                    editor.newEditor(Path.GetFileName(currentSheet.href), readStream.ReadToEnd(), currentSheet.href);

                    resetOverrideMenu();
                }
            }
        }

        private void webPreview_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            resetOverrideMenu();

            IHTMLDocument2 currentDocument = (IHTMLDocument2)webPreview.Document.DomDocument;

            editCssSheet = currentDocument.styleSheets.length;
            IHTMLStyleSheet styleSheet = currentDocument.createStyleSheet(@"", editCssSheet);
            //TODO: copy overrides on navigation too
        }

        private void webUrl_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
    }
}
