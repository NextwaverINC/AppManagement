using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace frmMangements
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Xceed.Grid.Licenser.LicenseKey = "GRD22-Y849T-2WN1N-44NA";
            //Xceed.Grid.Licenser.LicenseKey = "GRD22-Y849T-2WN1N-44NA";
            //Xceed.SmartUI.Licenser.LicenseKey = "SUN31-L84GT-57NDN-E42A";
            UserControl1 uc = new UserControl1();
            uc.Dock = DockStyle.Fill;
            uc._Position = "";
          
            Form frm = new Form();
            frm.Controls.Add(uc);
        }
    }
}