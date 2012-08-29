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
using System.Linq;
using System.Windows.Forms;

namespace CSSControl
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			//TODO: move this to setup and get removed on uninstall

			Microsoft.Win32.RegistryKey key;
			//Microsoft.Win32.Registry.CurrentUser.
			key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BEHAVIORS");
			key.SetValue(System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName, 00000001, Microsoft.Win32.RegistryValueKind.DWord);
			key.Close();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EditorForm());
        }
    }
}
