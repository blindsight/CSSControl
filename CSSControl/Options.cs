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
