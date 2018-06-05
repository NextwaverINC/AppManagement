using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace ACL
{
    public class cMain
    {
        public static string Connection;
        public static string UserName;
        public static string OfficeSpaceId;
        public static string DatabaseName;
        public static string TableName;
        public static string DocId;


        public bool checkACL(string iConnection, string iOfficeSpaceId, string Database, string iTableName, string iDocId, string iUserName, string FUNCTION)
        {
            try
            {
                ConnectServer.cConection cConn = new ConnectServer.cConection(iUserName);
                NextwaverDB.NWheres NWS = new NextwaverDB.NWheres();
                NWS.Add("CLASS", Database);
                NWS.Add("OBJECT", iTableName);
                NWS.Add("DOC_ID", iDocId);
                NWS.Add("USERNAME", iUserName);
                NWS.Add("FUNCTION", FUNCTION);

                DataTable dt = cConn.Retreive(iConnection, iOfficeSpaceId, "system", "acl_users", NWS);
                if (dt.Rows.Count > 0) return true;
                else return false;
            }
            catch { return false; }
        }

        public bool OpenACL(string iConnection, string iUserName, string iOfficeSpaceId, string Database, string iTableName, string iDocId)
        {
            Connection = iConnection;
            UserName = iUserName;
            OfficeSpaceId = iOfficeSpaceId;
            DatabaseName = Database;
            TableName = iTableName;
            DocId = iDocId;
            frmACL frm = new frmACL();

            frm.fnLoad(iConnection, iUserName, iOfficeSpaceId, Database, iTableName, iDocId);

            frm.ShowDialog();


            return false;
        }
    }
}
