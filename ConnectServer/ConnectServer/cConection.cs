using System;

using System.Text;
using System.Data;
using NextwaverDB;
using System.Xml;
namespace ConnectServer
{
    public class cConection
    {
        ConnectServer cConn = new ConnectServer();
        string ErrorLog = "";
        string UserName;
        public cConection(string iUser)
        {
            UserName = iUser;
        }
        public bool Execute(string SQL, string Connection)
        {
            return cConn.Execute(SQL, Connection);
        }
        public bool Execute(string[] SQL, string Connection, out string ErrorMsg)
        {
            ErrorMsg = "";
            if (cConn.Execute(SQL, Connection))
                return true;
            else
            {
                ErrorMsg = cConn._ErrorLog;
                return false;
            }
        }
        public bool Execute(string SQL, string Connection, out string ErrorMsg)
        {
            ErrorMsg = "";
            if (cConn.Execute(SQL, Connection))
                return true;
            else
            {
                ErrorMsg = cConn._ErrorLog;
                return false;
            }
        }
        public bool Execute(string SQL, string Parameter, string img, string Connection,out string ErrorMsg)
        {
            ErrorMsg = "";
            if (cConn.Execute(SQL, Parameter, img, Connection))
                return true;
            else
            {
                ErrorMsg = cConn._ErrorLog;
                return false;
            }
        }
        public DataSet Retreive(string SQL, string Connection, out string ErrorMsg)
        {            
            DataSet ds = cConn.Retreive(SQL, Connection);
            ErrorMsg = cConn._ErrorLog;
            return ds;
        }
        public DataSet Retreive(string SQL, string Connection)
        {
            return cConn.Retreive(SQL, Connection);
        }
        public bool InsertData(string SQL, string Connection)
        {
            return cConn.Execute(SQL, Connection);
        }

        #region Nextwaver Database
        public bool InsertData(string Connection, string OfficeSpaceId, string DatabaseName, string TableName, NextwaverDB.NColumns NColumns,string strDOC)
        {
            WorkSpace.Service WS = new global::ConnectServer.WorkSpace.Service();
            WS.Url = getWebUrl();
            string[] msgOutput = WS.InsertData(Connection, OfficeSpaceId, DatabaseName, TableName, NColumns.ExportString(), strDOC, UserName);
            if (msgOutput[0].ToUpper() == "OK") return true;
            else
            {
                ErrorLog = msgOutput[1];
                return false;
            }
        }
        public bool InsertData(string Connection, string OfficeSpaceId, string DatabaseName, string TableName, NextwaverDB.NColumns NColumns)
        {
            WorkSpace.Service WS = new global::ConnectServer.WorkSpace.Service();
            WS.Url = getWebUrl();
            string[] msgOutput = WS.InsertData(Connection, OfficeSpaceId, DatabaseName, TableName, NColumns.ExportString(), "", UserName);
            if (msgOutput[0].ToUpper() == "OK") return true;
            else
            {
                ErrorLog = msgOutput[1];
                return false;
            }
        }
        public bool UpdateData(string Connection, string OfficeSpaceId, string DatabaseName, string TableName, NextwaverDB.NColumns NColumns, NextwaverDB.NWheres NWheres, string strDOC)
        {
            WorkSpace.Service WS = new global::ConnectServer.WorkSpace.Service();
            WS.Url = getWebUrl();
            string[] msgOutput = WS.UpdateData(Connection, OfficeSpaceId, DatabaseName, TableName, NColumns.ExportString(), NWheres.ExportString(), strDOC, UserName);
            if (msgOutput[0].ToUpper() == "OK") return true;
            else
            {
                ErrorLog = msgOutput[1];
                return false;
            }
        }
        public bool UpdateData(string Connection, string OfficeSpaceId, string DatabaseName, string TableName, NextwaverDB.NColumns NColumns, NextwaverDB.NWheres NWheres)
        {
            WorkSpace.Service WS = new global::ConnectServer.WorkSpace.Service();
            WS.Url = getWebUrl();
            string[] msgOutput = WS.UpdateData(Connection, OfficeSpaceId, DatabaseName, TableName, NColumns.ExportString(), NWheres.ExportString(), "", UserName);
            if (msgOutput[0].ToUpper() == "OK") return true;
            else
            {
                ErrorLog = msgOutput[1];
                return false;
            }
        }
        public DataTable Retreive(string Connection, string OfficeSpaceId, string DatabaseName, string TableName)
        {
            WorkSpace.Service WS = new WorkSpace.Service();
            WS.Url = getWebUrl();
            DataTable dt = WS.SelectAll(Connection, OfficeSpaceId, DatabaseName, TableName, UserName);
            ErrorLog = WS.getError();
            return dt;
        }
        public DataTable Retreive(string Connection, string OfficeSpaceId, string DatabaseName, string TableName, NColumns NCS)
        {
            WorkSpace.Service WS = new WorkSpace.Service();
            WS.Url = getWebUrl();            
            DataTable dt = WS.SelectAllByColumn(Connection, OfficeSpaceId, DatabaseName, TableName, NCS.ExportString(), UserName);
            ErrorLog = WS.getError();
            return dt;
        }
        public DataTable Retreive(string Connection, string OfficeSpaceId, string DatabaseName, string TableName, NWheres NWS)
        {
            WorkSpace.Service WS = new WorkSpace.Service();
            WS.Url = getWebUrl();
            DataTable dt = WS.SelectAllColumnByWhere(Connection, OfficeSpaceId, DatabaseName, TableName, NWS.ExportString(), UserName);
            ErrorLog = WS.getError();
            return dt;
        }
        public DataTable Retreive(string Connection, string OfficeSpaceId, string DatabaseName, string TableName, NColumns NCS, NWheres NWS)
        {
            WorkSpace.Service WS = new WorkSpace.Service();
            WS.Url = getWebUrl();

            string ssss = NCS.ExportString();
            string xxxx = NWS.ExportString();

            string NCS_Encrypt = new EncryptDecrypt.CryptorEngine().Encrypt(NCS.ExportString(), true);
            string NWS_Encrypt = new EncryptDecrypt.CryptorEngine().Encrypt(NWS.ExportString(), true);


            DataTable dt = WS.SelectByColumnAndWhere(Connection, OfficeSpaceId, DatabaseName, TableName, NCS_Encrypt, NWS_Encrypt, UserName);
            ErrorLog = WS.getError();
            return dt;
        }
        public string getLastDoc(string Connection, string OfficeSpaceId, string DatabaseName, string TableName, int ItemId)
        {
            WorkSpace.Service WS = new WorkSpace.Service();
            WS.Url = getWebUrl();
            string OutputXML = WS.SelectLastDocument(Connection, OfficeSpaceId, DatabaseName, TableName, ItemId, UserName);
            ErrorLog = WS.getError();
            return OutputXML;
        }
        public bool Transform(string Connection, string OfficeSpaceId, string DatabaseName, string TableName, int ItemId, string User)
        {
            WorkSpace.Service WS = new WorkSpace.Service();
            WS.Url = getWebUrl();
            return Transform(Connection, OfficeSpaceId, DatabaseName, TableName, ItemId, User);
           
        }
        #endregion

        public string getWebUrl()
        {
            return System.Configuration.ConfigurationSettings.AppSettings["WorkSpaceService"];
        }
        public string ErrorMsg()
        {
            return ErrorLog;
        }
    }
}
