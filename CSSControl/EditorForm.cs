using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mshtml;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace CSSControl
{
    public partial class EditorForm : Form
    {
        private const int WM_SETREDRAW = 0x000B;
        private const int WM_USER = 0x400;
        private const int EM_GETEVENTMASK = (WM_USER + 59);
        private const int EM_SETEVENTMASK = (WM_USER + 69);
		public int browserIndex;
		public int futureBrowserIndex;
		public string browserString;


        [DllImport("user32", CharSet = CharSet.Auto)]
        private extern static IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

        //TODO: some of this should be moved out of the gui thread
        PreviewForm currentPreview;
        int untited;
        List<CssFile> cssFiles = new List<CssFile>();
        int currentSelectIndex = -1;
        int oldLength = -1;
        CssLanguage FormLanguage;

        public EditorForm()
        {
            FormLanguage = new CssLanguage();
            InitializeComponent();
            editControl.TabPages.Clear();
            untited = 1;
            newEditor("");

			futureBrowserIndex = -1;
			Microsoft.Win32.RegistryKey editKey;
			editKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION");
			try {
				int currentValue;
				string currentStringValue = editKey.GetValue(System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName).ToString();
				int.TryParse(currentStringValue, out currentValue);

				switch (currentValue) {
					case 7000:
						browserIndex = 0;
						browserString = "IE7";
						break;
					case 8000:
						browserIndex = 1;
						browserString = "IE8 ";
						break;
					case 8888:
						browserIndex = 2;
						browserString = "IE8 (forced standards mode)";
						break;
					case 9000:
						browserIndex = 3;
						browserString = "IE9";
						break;
					case 9999:
						browserIndex = 4;
						browserString = "IE9 (forced standards mode)";
						break;
				}
			} catch { //ie 7 is the normal system default
				browserIndex = 0;
				browserString = "IE7";
			} finally {
				editKey.Close();
			}
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPreview = new PreviewForm();
            currentPreview.Owner = this;
            currentPreview.Show();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            untited++;
            newEditor("");
        }

        public void newEditor(string Contents)
        {
            newEditor("Untitled " + untited, Contents);
        }

        public int newEditor(string fileName, string Contents)
        {
            TabPage newPage = new TabPage(fileName);

            RichTextBox newEditor = new RichTextBox();
            newEditor.Text = Contents;
            newEditor.TextChanged += new EventHandler(newEditor_TextChanged);
            newEditor.KeyDown += new KeyEventHandler(newEditor_KeyDown);
			newEditor.KeyUp += new KeyEventHandler(newEditor_KeyUp);
            newEditor.VScroll += new EventHandler(newEditor_VScroll);
           // newEditor.Dock = DockStyle.Right;
            newEditor.Width = editControl.Width - 40;
            newEditor.Location = new Point(40, 0);
            newEditor.Font = new Font("Consolas", 8, FontStyle.Regular);
            newEditor.AcceptsTab = true;
            
            newPage.Controls.Add(newEditor);

            RichTextBox lineNumbers = new RichTextBox();
            lineNumbers.Location = new Point(0, 0);
           // lineNumbers.Dock = DockStyle.Left;
            lineNumbers.Text = "1";
            lineNumbers.ScrollBars = RichTextBoxScrollBars.None;
            lineNumbers.BorderStyle = BorderStyle.None;
            lineNumbers.BackColor = newPage.BackColor;
            lineNumbers.Font = new Font(newEditor.Font, FontStyle.Bold);

            newPage.Controls.Add(lineNumbers);
            editControl.TabPages.Add(newPage);

            CssFile newFile = new CssFile();
            newFile.index = editControl.TabPages.IndexOf(newPage);
            newFile.name = fileName;
            //the index in cssfile must page the tabindex at all times
            cssFiles.Insert(newFile.index,newFile);


            newEditor.Height = newPage.Height;
            newEditor.Width = newPage.Width - 40;
            lineNumbers.Width = 40;
            lineNumbers.Height = newPage.Height;

			UpdateLineNumbers(lineNumbers, newEditor);
            newPage.SizeChanged += new EventHandler(newPage_SizeChanged);
            return newFile.index;
        }

		void newEditor_KeyUp(object sender, KeyEventArgs e)
		{
		}

        void newEditor_VScroll(object sender, EventArgs e)
        {
            RichTextBox editBox = (RichTextBox)editControl.SelectedTab.Controls[0];
            RichTextBox lineNumbers = (RichTextBox)editControl.SelectedTab.Controls[1];

            UpdateLineNumbers(lineNumbers, editBox);

			int topIndex = editBox.GetCharIndexFromPosition(new Point(1, 1));
			int bottomIndex = editBox.GetCharIndexFromPosition(new Point(editBox.Width, editBox.Height - 1));
			ParseLine(editBox.Text, editBox, topIndex, bottomIndex);
			editBox.Invalidate();
        }

        void newPage_SizeChanged(object sender, EventArgs e)
        {
            RichTextBox editBox = (RichTextBox)editControl.SelectedTab.Controls[0];
            RichTextBox lineNumbers = (RichTextBox)editControl.SelectedTab.Controls[1];

            editBox.Height = editControl.SelectedTab.Height;
            lineNumbers.Height = editControl.SelectedTab.Height;
            lineNumbers.Width = 40;

            if (editControl.SelectedTab.Width >= lineNumbers.Width)
            {
                editBox.Width = editControl.SelectedTab.Width - lineNumbers.Width;
            }

            if (editBox.WordWrap)
            { //when wordwrap is on then the line numbers might have to be spaced out
                UpdateLineNumbers(lineNumbers, editBox);
            }
        }

        void UpdateLineNumbers(RichTextBox lineNumbers, RichTextBox thisRtb)
        {

            //getLinefromCharindex isn't the same as line number when dealing with wordwrap
           // string[] textLines = thisRtb.Text.Split('\n');
            int topLine = lineFromCharIndex(thisRtb.Text, (thisRtb.GetCharIndexFromPosition(new Point(0,0))));

            int bottomLine = thisRtb.GetLineFromCharIndex(thisRtb.GetCharIndexFromPosition(new Point(1, thisRtb.Height)));

            int currentLine = 0;
            lineNumbers.Text = "";
            int lastLine = 0;

            for (int i = topLine; i <= bottomLine; i++)
            {

                currentLine = lineFromCharIndex(thisRtb.Text, thisRtb.GetFirstCharIndexFromLine(i-1));
                
                
                if (lastLine != currentLine)
                {
                    lineNumbers.AppendText(currentLine + "");
                    lastLine = currentLine;
                }

                lineNumbers.AppendText("\n");
            }

            lineNumbers.SelectAll();
            lineNumbers.SelectionAlignment = HorizontalAlignment.Right;
        }

        int lineFromCharIndex(String text, int CharIndex)
        {
            int count = 1;
            int start = 0;
            while ((start = text.IndexOf('\n', start)) != -1)
            {
                if (start >= CharIndex) return count;
                count++;
                start++;
            }
            return count;
        }

		[DllImport("user32.dll", EntryPoint = "ShowCaret")]
		public static extern long ShowCaret(IntPtr hwnd);

		[DllImport("user32.dll", EntryPoint = "HideCaret")]
		public static extern long HideCaret(IntPtr hwnd);

        void newEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                RichTextBox currentTextBox = (RichTextBox)editControl.SelectedTab.Controls[0];
                RichTextBox lineNumbers = (RichTextBox)editControl.SelectedTab.Controls[1];
                currentSelectIndex = currentTextBox.SelectionStart;
                oldLength = currentTextBox.TextLength;

                UpdateLineNumbers(lineNumbers, currentTextBox);
            } else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control) {
                RichTextBox currentTextBox = (RichTextBox)editControl.SelectedTab.Controls[0];
                RichTextBox lineNumbers = (RichTextBox)editControl.SelectedTab.Controls[1];

                UpdateLineNumbers(lineNumbers, currentTextBox);
            }
            else if (e.KeyCode == Keys.OemSemicolon && e.Modifiers == Keys.Shift
                || e.KeyCode == Keys.Space
                || e.KeyCode == Keys.Tab
				|| e.KeyCode == Keys.OemSemicolon)
            {
                RichTextBox currentTextBox = (RichTextBox)editControl.SelectedTab.Controls[0];
                RichTextBox lineNumbers = (RichTextBox)editControl.SelectedTab.Controls[1];

                int firstChar = currentTextBox.GetFirstCharIndexOfCurrentLine();
                int currentLine = currentTextBox.GetLineFromCharIndex(firstChar);
                ParseLine(currentTextBox.Text, currentTextBox, firstChar, currentTextBox.Lines[currentLine].Length + firstChar);
				currentTextBox.Invalidate();
			} else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return) {
				RichTextBox currentTextBox = (RichTextBox)editControl.SelectedTab.Controls[0];
				RichTextBox lineNumbers = (RichTextBox)editControl.SelectedTab.Controls[1];

				//Off hand I can't see this being important to have
				/* int firstChar = currentTextBox.GetFirstCharIndexOfCurrentLine();
				 int currentLine = currentTextBox.GetLineFromCharIndex(firstChar);
				 ParseLine(currentTextBox.Text, currentTextBox, firstChar, currentTextBox.Lines[currentLine].Length + firstChar);
				 */
				int previousLine = currentTextBox.GetLineFromCharIndex(currentTextBox.GetFirstCharIndexOfCurrentLine());
				int currentLine = previousLine + 1;
				


				if (previousLine < currentTextBox.Lines.Length && previousLine >= 0) {
					//get the format from the previous line
					Match hasWhiteSpace = Regex.Match(currentTextBox.Lines[previousLine], @"^[\s]+");

					if (hasWhiteSpace.Success) {
						string beginning = hasWhiteSpace.Groups[0].Value;
						int charFromLine = currentTextBox.GetFirstCharIndexFromLine(currentLine);

						currentTextBox.SelectionStart = charFromLine;
						currentTextBox.SelectionLength = 0;
						currentTextBox.SelectedText = beginning +"\n";

						//This event is suppressed so we can insert the newline where we need it
						e.SuppressKeyPress = true;

						currentTextBox.SelectionStart = charFromLine + beginning.Length;
					}
				}
				UpdateLineNumbers(lineNumbers, currentTextBox);

				
			} else if(e.Modifiers == Keys.Control && (e.KeyCode == Keys.G || e.KeyCode == Keys.L)) {
				RichTextBox currentTextBox = (RichTextBox)editControl.SelectedTab.Controls[0];
				//we use +1 to convert index to line
				LineNumber lineBox = new LineNumber(1, currentTextBox.Lines.Length, currentTextBox.GetLineFromCharIndex(currentTextBox.GetFirstCharIndexOfCurrentLine())+1);

				if (lineBox.ShowDialog(this) == System.Windows.Forms.DialogResult.OK) {
					currentTextBox.SelectionStart = currentTextBox.GetFirstCharIndexFromLine(lineBox.LineChangeTo);
				}

				lineBox.Close();
			}
        }

        public void newEditor(string fileName, string Contents,string overRideLocation)
        {
            int cssFileIndex = newEditor(fileName, Contents);
            CssFile currentCssFile = cssFiles[cssFileIndex];
            currentCssFile.overrideLocation(overRideLocation);
        }

        public void SetOverride(string tabName, string overRideLocation)
        {
            int cssFileIndex = 0;

            foreach (CssFile currentFile in cssFiles)
            {
                if (currentFile.name == tabName)
                {
                    cssFileIndex = currentFile.index;
                }
            }

            TabPage cssPage = editControl.TabPages[cssFileIndex];
            CssFile currentCssFile = cssFiles[cssFileIndex];
            currentCssFile.overrideLocation(overRideLocation);

            currentPreview.setCSSRules(cssPage.Controls[0].Text, currentCssFile.Href);

        }

        public void UnSetOverride(string tabName, string overRideLocation)
        {
            int cssFileIndex = 0;

            foreach (CssFile currentFile in cssFiles)
            {
                if (currentFile.name == tabName)
                {
                    cssFileIndex = currentFile.index;
                }
            }

            TabPage cssPage = editControl.TabPages[cssFileIndex];
            CssFile currentCssFile = cssFiles[cssFileIndex];
            currentCssFile.overrideLocation(overRideLocation);
        }

        void newEditor_TextChanged(object sender, EventArgs e)
        {
            if (currentPreview != null)
            {
                string overrideUrl = "";

                //TODO: deal with tabindex if it doesn't exist
                CssFile currentCssFile = cssFiles[editControl.SelectedIndex];

                if(currentCssFile.doesOverRide()) {
                    overrideUrl = currentCssFile.Href;
                    currentPreview.setCSSRules(currentCssText(), overrideUrl);
                }
            }

            if (currentSelectIndex >= 0)
            {
                RichTextBox richBox = (RichTextBox)editControl.SelectedTab.Controls[0];

                int startIndex = richBox.GetFirstCharIndexFromLine(richBox.GetLineFromCharIndex(currentSelectIndex));
                //textlength should always be longer
                int endIndex = richBox.GetFirstCharIndexFromLine(richBox.GetLineFromCharIndex(currentSelectIndex+(richBox.TextLength-oldLength))+1);

                ParseLine(richBox.Text.Substring(startIndex,(richBox.TextLength-currentSelectIndex)), richBox, startIndex, endIndex);
                currentSelectIndex = -1;

                richBox.Invalidate();
            }

        }

        void ParseLine(string line, RichTextBox m_rtb, int startIndex = 0, int endIndex = 0)
        {
            if (line.Trim().Length < 1 && endIndex <= startIndex) return;

			m_rtb.VScroll -= new EventHandler(newEditor_VScroll);
			m_rtb.TextChanged -= new EventHandler(newEditor_TextChanged);
            int startCursor = m_rtb.SelectionStart;
            IntPtr eventMask = IntPtr.Zero;
			Point ScrollPoint = GetScrollPos(m_rtb.Handle);

            try
            {
                m_rtb.SuspendLayout();
                // Stop redrawing:
                SendMessage(m_rtb.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
                // Stop sending of events:
                eventMask = SendMessage(m_rtb.Handle, EM_GETEVENTMASK, 0, IntPtr.Zero);
				

                Regex r = new Regex("([ \\n\\t{}:;])");

                int selectedIndex = startIndex;
                int endingIndex = (endIndex > startIndex) ? endIndex : m_rtb.TextLength;


                line = line.Substring(selectedIndex, endingIndex - selectedIndex);

                String[] tokens = r.Split(line);
                bool inProperty = false;
                bool inDeclare = false;
                bool inComment = false;
				bool inValue = false;
				int startComment = -1;
                String token = "";

                for (int tokenIndex = 0; tokenIndex < tokens.Count(); tokenIndex++)
                {
                    token = tokens[tokenIndex];


                    if (token.Trim().Equals("")) {
                        selectedIndex += token.Length;
                        continue;
                    }

                    if (token.StartsWith("/*") || token.Contains("/*"))
                    {
						startComment = selectedIndex;
                        inComment = true;
                    } else if (inComment && token.Contains("*/")) {
						int closeComment = token.IndexOf("*/") + 1;
						inComment = false;
						int commentLength = (selectedIndex - startComment) + closeComment;
						m_rtb.SelectionStart = startComment;
						m_rtb.SelectionLength = commentLength;
						m_rtb.SelectionColor = Color.Green;
						token = token.Substring(closeComment);
						selectedIndex += closeComment;
					} else if (token.Equals(";") && !inComment)
                    {
						inValue = false;
                        inProperty = false;
                    }
                    else if (token.Equals("{") && !inComment)
                    {
                        inDeclare = true;
                    }
                    else if (token.Equals("}") && !inComment)
                    {
                        inDeclare = false;
					} else if (!inComment && token.Equals(":")) {
						m_rtb.SelectionStart = selectedIndex;
						m_rtb.SelectionLength = token.Length;
						m_rtb.SelectionColor = Color.Black;
						m_rtb.SelectedText = token;
						inProperty = false;
						inValue = true;
					} else if (inValue && !inComment) {
						if (token.StartsWith("#")) { //hex colors only
							Regex hexColorCheck = new Regex(@"(^[#][A-Fa-f0-9]{3}$|^[#][A-Fa-f0-9]{6}$)");
							
							if (hexColorCheck.IsMatch(token)) {
								string colorToken = token;
								if(token.Length == 4) { //a short 3 double color
									colorToken = "#" + token[1] + token[1] + token[2] + token[2] + token[3] + token[3];
								}
								ColorConverter hexConvert = new ColorConverter();
								Color hexColor = (Color) hexConvert.ConvertFromString(colorToken);
								m_rtb.SelectionStart = selectedIndex;
								m_rtb.SelectionLength = token.Length;
								m_rtb.SelectionColor = hexColor;
								m_rtb.SelectedText = token;
							}
						}
					} else if (FormLanguage.tokenList.ContainsKey(token) && !inProperty && !inComment) {
						SyntaxToken keyword = (SyntaxToken)FormLanguage.tokenList[token];
						m_rtb.SelectionStart = selectedIndex;
						m_rtb.SelectionLength = token.Length;
						m_rtb.SelectionColor = keyword.Color;
						m_rtb.SelectedText = token;
						inProperty = true;
					}
                    selectedIndex += token.Length;

                    if (selectedIndex >= endingIndex) break;
                }
            }
            finally
            {

                m_rtb.SelectionStart = endIndex;
                m_rtb.SelectionColor = Color.Black;
				
                // turn on events
                SendMessage(m_rtb.Handle, EM_SETEVENTMASK, 0, eventMask);
                // turn on redrawing
                SendMessage(m_rtb.Handle, WM_SETREDRAW, 1, IntPtr.Zero);

                m_rtb.SelectionStart = startCursor;
                m_rtb.SelectionLength = 0;
                m_rtb.ResumeLayout();
            }

			SetScrollPos(ScrollPoint, m_rtb.Handle);
			m_rtb.VScroll += new EventHandler(newEditor_VScroll);
			m_rtb.TextChanged += new EventHandler(newEditor_TextChanged);
        } 

        private String currentCssText()
        {
            TabPage currentPage = editControl.TabPages[editControl.SelectedIndex];
            RichTextBox currentEditBox = (RichTextBox)currentPage.Controls[0];
            return currentEditBox.Text;
        }

        public ArrayList isNotOverridding()
        {
            ArrayList cssList = new ArrayList();
            foreach (CssFile currentFile in cssFiles)
            {
                if(!currentFile.doesOverRide()) {
                    cssList.Add(currentFile);
                }
            }

            return cssList;
        }

        public ArrayList ListAllCssFiles()
        {
            ArrayList cssList = new ArrayList();

            foreach (CssFile currentFile in cssFiles)
            {
                    cssList.Add(new String[] {currentFile.name, currentFile.Href});
            }

            return cssList;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string FileName = openFile.FileName;
                string FileContents = File.ReadAllText(FileName);

                if (editControl.SelectedTab.Text.Contains("Untitled") && editControl.SelectedTab.Controls[0].Text.Equals(""))
                {
                    cssFiles.Remove(cssFiles[editControl.SelectedIndex]);
                    editControl.TabPages.Remove(editControl.SelectedTab);
                }

                int fileIndex = newEditor(Path.GetFileName(FileName), FileContents);

                editControl.SelectedTab = editControl.TabPages[fileIndex];

                RichTextBox editorBox = (RichTextBox)editControl.SelectedTab.Controls[0];
                RichTextBox lineNumbers = (RichTextBox)editControl.SelectedTab.Controls[1];

                ParseLine(editorBox.Text, editorBox, 1,  editorBox.GetCharIndexFromPosition(new Point(1, editorBox.Height)));

               // UpdateLineNumbers(lineNumbers, editorBox);

                editorBox.Focus();
                editorBox.SelectionStart = 0;
                CssFile currentCssFile = cssFiles[fileIndex];
                currentCssFile.name = Path.GetFileName(FileName);
                currentCssFile.FileLocation = FileName;                

                if (currentPreview != null)
                {
                    currentPreview.resetOverrideMenu();
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                CssFile currentCss = cssFiles[editControl.SelectedIndex];
                currentCss.FileLocation = saveFile.FileName;

                File.WriteAllText(currentCss.FileLocation, currentCssText());
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            CssFile currentCss = cssFiles[editControl.SelectedIndex];

            if (currentCss.FileLocation.Equals(""))
            {
                SaveFileDialog saveFile = new SaveFileDialog();

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    currentCss.FileLocation = saveFile.FileName;
                }
            }

            File.WriteAllText(currentCss.FileLocation, currentCssText());

        }

        private void wrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox editBox = (RichTextBox)editControl.SelectedTab.Controls[0];

            if (editBox.WordWrap)
            {
                editBox.WordWrap = false;
                wrapToolStripMenuItem.Checked = false;
            }
            else
            {
                editBox.WordWrap = true;
                wrapToolStripMenuItem.Checked = true;
            }
        }

        private void editControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (editControl.TabCount > 0 && editControl.SelectedTab.Controls.Count > 0)
            {
                RichTextBox editBox = (RichTextBox)editControl.SelectedTab.Controls[0];

                if (editBox.WordWrap)
                {
                    wrapToolStripMenuItem.Checked = true;
                }
                else
                {
                    wrapToolStripMenuItem.Checked = false;
                }
            }
        }

		public const int EM_GETSCROLLPOS = (WM_USER + 221);
		public const int EM_SETSCROLLPOS = (WM_USER + 222);

		private unsafe Point GetScrollPos(IntPtr Handle)
		{
			Point ScrollPos = new Point();
			IntPtr ScrollPosPtr = new IntPtr(&ScrollPos);
			SendMessage(Handle, EM_GETSCROLLPOS, 0, ScrollPosPtr);
			return ScrollPos;
		}

		private unsafe void SetScrollPos(Point position, IntPtr Handle)
		{
			IntPtr ScrollPosPtr = new IntPtr(&position);
			SendMessage(Handle, EM_SETSCROLLPOS, 0, ScrollPosPtr);
		}

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Options optionsForm = new Options(this);

			optionsForm.ShowDialog();
		}
    }
}
