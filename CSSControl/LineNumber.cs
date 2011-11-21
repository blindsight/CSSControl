using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSSControl
{
	public partial class LineNumber : Form
	{
		private int lineChangeTo;

		public LineNumber(int startLine, int endLine, int currentLine)
		{
			InitializeComponent();
			lineChangeTo = 1;
			this.AcceptButton = OkBtn;
			lineNumBox.Text = currentLine.ToString();
			lineNumLabel.Text += "(" + startLine + "-" + endLine+ ")";

			ActiveControl = lineNumBox;
			lineNumBox.SelectAll();
		}

		private void OkBtn_Click(object sender, EventArgs e)
		{
			int num;

			try {
				int.TryParse(lineNumBox.Text, out num);
				lineChangeTo = num -1; //lines is 0 based and the user will use 1 based
			} catch (Exception numError) {
				this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			}

			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		public int LineChangeTo
		{
			get
			{
				return lineChangeTo;
			}
		}
	}
}
