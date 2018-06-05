using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Windows.Forms;

namespace AppTest
{
    public class cMain
    {
        public static string Connection;
        public static string UserName;
        public static string OfficeSpaceId;
        public static string DatabaseName = "document";
        public bool funNew(string iConnection, string iUserName, string iOfficeSpaceId, string Database)
        {
            Connection = iConnection;
            UserName = iUserName;
            OfficeSpaceId = iOfficeSpaceId;
            DatabaseName = Database;
            frmEmployee frm = new frmEmployee();
            frm.Command = "New";
            frm.funLoad();
            bool bOutput = false;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bOutput = true;
            }
            if (!frm.IsDisposed)
            {
                frm.Close();
                frm.Dispose();
            }
            return bOutput;           
        }
        public bool funEdit(string iConnection, string iUserName, string iOfficeSpaceId, DataTable dt, int currentIndex, string Database)
        {
            Connection = iConnection;
            UserName = iUserName;
            OfficeSpaceId = iOfficeSpaceId;
            DatabaseName = Database;
            if (dt.Rows.Count == 0) return false;
            frmEmployee frm = new frmEmployee();
            frm.Command = "Edit";
            frm.ID_TEMP = dt.Rows[currentIndex]["ID"].ToString();
            frm.funLoad();
            bool bOutput = false;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bOutput = true;
            }
            if (!frm.IsDisposed)
            {
                frm.Close();
                frm.Dispose();
            }
            return bOutput;          
        }
    }
}
