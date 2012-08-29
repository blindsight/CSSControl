/*
CSS editor for Windows. Allows CSS to be edited in real time.
    Copyright (C) 2012  Timothy Rhodes <phoenixevolution@gmail.com>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.

*/
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
			//TODO: check for install of ie 9.. maybe 8?
			Microsoft.Win32.RegistryKey editKey;
			editKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION");
			try {
				int currentValue;
				string currentStringValue = editKey.GetValue(System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName).ToString();
				int.TryParse(currentStringValue, out currentValue);

				switch (currentValue) {
					case 7000:
						iE7ToolStripMenuItem.Checked = true;
						break;
					case 8000:
						iE8ToolStripMenuItem.Checked = true;
						break;
					case 8888:
						iE8ForceStandardsModeToolStripMenuItem.Checked = true;
						break;
					case 9000:
						iE9ToolStripMenuItem.Checked = true;
						break;
					case 9999:
						iE9ForceStandardsModeToolStripMenuItem.Checked = true;
						break;
				}
			} catch { //ie 7 is the normal system default
				iE7ToolStripMenuItem.Checked = true;
			} finally {
				editKey.Close();
			}

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

		private void iE7ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem currentItem = (ToolStripMenuItem)sender;

			foreach (ToolStripMenuItem item in currentItem.GetCurrentParent().Items) {
				if (item != currentItem) {
					item.Checked = false;
				}
			}

			setIeMode(7000);
		}

		private void iE8ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem currentItem = (ToolStripMenuItem)sender;

			foreach (ToolStripMenuItem item in currentItem.GetCurrentParent().Items) {
				if (item != currentItem) {
					item.Checked = false;
				}
			}

			
			setIeMode(8000);
		}

		private void iE8ForceStandardsModeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem currentItem = (ToolStripMenuItem)sender;

			foreach (ToolStripMenuItem item in currentItem.GetCurrentParent().Items) {
				if (item != currentItem) {
					item.Checked = false;
				}
			}

			setIeMode(8888);
		}

		private void iE9ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem currentItem = (ToolStripMenuItem)sender;

			foreach (ToolStripMenuItem item in currentItem.GetCurrentParent().Items) {
				if (item != currentItem) {
					item.Checked = false;
				}
			}

			setIeMode(9000);
		}

		private void iE9ForceStandardsModeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem currentItem = (ToolStripMenuItem)sender;

			foreach (ToolStripMenuItem item in currentItem.GetCurrentParent().Items) {
				if (item != currentItem) {
					item.Checked = false;
				}
			}

			setIeMode(9999);
		}

		private void setIeMode(int modeNumber)
		{
			Microsoft.Win32.RegistryKey editKey;
			editKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION");
			editKey.SetValue(System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName, modeNumber, Microsoft.Win32.RegistryValueKind.DWord);
			editKey.Close();
		}
    }
}
