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
	public partial class Options : Form
	{
		public Options(EditorForm formOwner)
		{
			InitializeComponent();
			this.Owner = formOwner;

		
			//browserCheckBox.SelectedIndex = formOwner.browserIndex;
			browserCheckBox.SetItemChecked(formOwner.browserIndex, true);
			browserCheckBox.CheckOnClick = true;

			if (formOwner.futureBrowserIndex > -1) {
				futureModeLbl.Text = "On Restart Mode: " + browserCheckBox.GetItemText(browserCheckBox.Items[formOwner.futureBrowserIndex]);
			}
		}

		private void browserCheckBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			EditorForm formOwner = (EditorForm)this.Owner;
			
			formOwner.futureBrowserIndex = browserCheckBox.SelectedIndex;
			futureModeLbl.Text = "On Restart Mode: " + browserCheckBox.GetItemText(browserCheckBox.SelectedItem);
		}


		private void setIeMode(int modeNumber)
		{
			Microsoft.Win32.RegistryKey editKey;
			editKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION");
			editKey.SetValue(System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName, modeNumber, Microsoft.Win32.RegistryValueKind.DWord);
			editKey.Close();
		}

		private void Options_FormClosing(object sender, FormClosingEventArgs e)
		{

		}

		private void ApplyBtn_Click(object sender, EventArgs e)
		{
			EditorForm formOwner = (EditorForm)this.Owner;

			switch (formOwner.futureBrowserIndex) {
				case 0:
					setIeMode(7000);
					break;
				case 1:
					setIeMode(8000);
					break;
				case 2:
					setIeMode(8888);
					break;
				case 3:
					setIeMode(9000);
					break;
				case 4:
					setIeMode(9999);
					break;
			}

			this.Close();
		}

		private void browserCheckBox_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			for (int indices = 0; indices < browserCheckBox.Items.Count; indices++) {
				if (e.Index != indices) {
					browserCheckBox.SetItemChecked(indices, false);
				}
			}

		}

	}
}
