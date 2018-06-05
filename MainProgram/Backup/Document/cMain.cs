using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Document
{
    public class cMain
    {
        public static string Connection;
        public static string UserName;
        public static string OfficeSpaceId;
        public static string DatabaseName = "document";
        public bool funNewDocument(string iConnection, string iUserName, string iOfficeSpaceId, string Database)
        {
            Connection = iConnection;
            UserName = iUserName;
            OfficeSpaceId = iOfficeSpaceId;
            DatabaseName = Database;
            frmDocument frm = new frmDocument();
            frm._Command = "New";
            bool OP = false;
            if (frm.ShowDialog() == DialogResult.OK)
                OP = true;
            if (!frm.IsDisposed)
            {
                frm.Close();
                frm.Dispose();
            }
            return OP;
            
        }
        public bool funEditDocument(string iConnection, string iUserName, string iOfficeSpaceId, DataTable dt, int currentIndex, string Database)
        {
            Connection = iConnection;
            UserName = iUserName;
            OfficeSpaceId = iOfficeSpaceId;
            DatabaseName = Database;
            if (dt.Rows.Count == 0) return false;

            string ID = "" + dt.Rows[currentIndex]["ID"];
            string Name = "" + dt.Rows[currentIndex]["NAME"];

            ConnectServer.cConection cConn = new ConnectServer.cConection(iUserName);
            
            frmDocument frm = new frmDocument();
            frm._DocXML = cConn.getLastDoc(iConnection, iOfficeSpaceId, DatabaseName, "document", int.Parse(ID));
            frm._DocumentID = ID;
            frm._DocumentName = Name;
            frm._Command = "Edit";
            bool OP = false;
            if (frm.ShowDialog() == DialogResult.OK)
                OP = true;
            if (!frm.IsDisposed)
            {
                frm.Close();
                frm.Dispose();
            }
            return OP;
            
        }

        
    }
}
