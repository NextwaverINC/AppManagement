using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.Deployment.Application;
using System.Data;
using System.Diagnostics;
using System.Threading;

namespace MainProgram
{
    static class Program
    {
        static string connectTemp;
        public static string ServerName;
        public static string DatabaseName = "system";
        public static string OffiecSpaceId = "OF.0001";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        { 
            Xceed.Grid.Licenser.LicenseKey = "GRD22-Y849T-2WN1N-44NA";
            Xceed.SmartUI.Licenser.LicenseKey = "SUN31-L84GT-57NDN-E42A";

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            XmlDocument xmlConfig = new XmlDocument();
            xmlConfig.Load("Config.xml");
            string login = xmlConfig.SelectSingleNode("//Config[@ID='0']/Item[@ID='1']").Attributes["Value"].Value;

            XmlDocument mConfig = new XmlDocument();
            mConfig.Load("MConfig.xml");

            XmlNode nodeConnect = mConfig.SelectSingleNode("//Management[@ID='Management1']/NextwaverDB");
            connectTemp = nodeConnect.Attributes["Connection"].Value; 

            if (login == "T")
            {
                frmLogin frmlogin = new frmLogin();
                frmlogin._Connection = connectTemp;
                Application.Run(frmlogin);
            }
            else
            {
                frmMenu frm = new frmMenu(ServerName);
                frm._UserID = "Admin";                
                frm._Position = "A";
                frm._Connection = connectTemp;
                Application.Run(frm);
            }
        }     
    }
}