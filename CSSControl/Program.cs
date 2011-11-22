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
