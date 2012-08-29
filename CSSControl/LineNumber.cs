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
