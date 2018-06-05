using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Data;
using System.Windows.Forms;
using Message;

namespace ToolCommand
{
    public class csCommand
    {
        public Xceed.Grid.GridControl _gridTemp;
        public string DatabaseTemp;
        public string TableNameTemp;
        public string OfficeSpaceId;
        string currentTools;

        Panel panelView;
        public Panel _PanelView
        {
            set { panelView = value; }
            get { return panelView; }
        }
        string ProjectID;
        public string _ProjectID
        {
            set { ProjectID = value; }
        }
        public csCommand(string iOfficeSpaceId)
        {
            OfficeSpaceId = iOfficeSpaceId;
        }
        public void RowClick(string ToolID, DataTable dt, int currentIndex, string strConnect, string user, string Position)
        {
            currentTools = ToolID;
            //switch (ToolID)
            //{
            //    default: Default(); break;
            //}
        }
        public void RowDoubleClick(string ToolID, DataTable dt, int currentIndex, string strConnect, string user, string Position)
        {
            currentTools = ToolID;
            //switch (ToolID)
            //{
            //    case "Tools3": Books("EditBook", dt, null, currentIndex, user, Position, strConnect); break;
            //    default: Default(); break;
            //}
        }
        private void Tool_1DoubleClick(DataTable dt, int currentIndex, string strConnect, string user)
        {
            
        }
        public bool funCommand(string ToolID, string command, DataTable dt, int[] listSelectIndex, int currentIndex, string strConnect, string user, string Position, string strParameter)
        {
            currentTools = ToolID;
            switch (ToolID)
            {
                case "Tools1": return Users(command, dt, listSelectIndex, currentIndex, user, Position, strConnect);
                case "Tools2": return Users(command, dt, listSelectIndex, currentIndex, user, Position, strConnect);
                case "Tools3": return Users(command, dt, listSelectIndex, currentIndex, user, Position, strConnect);
                default: return Default();          
            }
        }

        private bool fnACL(DataTable dt,  int currentIndex, string user, string Position, string strConnect)
        {
            if (dt.Rows.Count == 0) return false;
            

            string DocId = dt.Rows[currentIndex]["ID"].ToString();
            
            ACL.cMain CM = new ACL.cMain();
            return CM.OpenACL(strConnect, user, OfficeSpaceId, DatabaseTemp, TableNameTemp, DocId);
           
        }
        private bool RunRobot(DataTable dt, int[] listSelectIndex, int currentIndex, string user, string Position, string strConnect)
        {
            if (dt.Rows.Count == 0) return false;
            Robot.cMain RB = new Robot.cMain();
            return RB.Run(strConnect, user, OfficeSpaceId, currentTools, dt, listSelectIndex, currentIndex, DatabaseTemp);
        }
        private bool StopRobot()
        {
            Robot.cMain RB = new Robot.cMain();
            return RB.Stop(currentTools);
        }
        private bool funAppTest(string command, DataTable dt, int[] listSelectIndex, int currentIndex, string user, string Position, string strConnect)
        {
            AppTest.cMain CM = new AppTest.cMain();
            switch (command)
            {
                case "New": return CM.funNew(strConnect, user, OfficeSpaceId, DatabaseTemp);
                case "Edit": return CM.funEdit(strConnect, user, OfficeSpaceId, dt, currentIndex, DatabaseTemp);
                default: return false;
            }
        }
        private bool funDocument(string command, DataTable dt, int[] listSelectIndex, int currentIndex, string user, string Position, string strConnect)
        {
            Document.cMain CM = new Document.cMain();
            switch (command)
            {
                case "NewDocument": return CM.funNewDocument(strConnect, user, OfficeSpaceId,DatabaseTemp);
                case "EditDocument": return CM.funEditDocument(strConnect, user, OfficeSpaceId, dt, currentIndex,DatabaseTemp);
                default: return false;               
            }           
        }
        private bool Users(string command, DataTable dt, int[] listSelectIndex, int currentIndex, string user, string Position, string strConnect)
        {
            Users.cMain US = new Users.cMain();
            switch (command)
            {
                default: return false;
                case "NewPosition": return US.newPosition(strConnect, user, OfficeSpaceId, DatabaseTemp);
                case "EditPosition": return US.editPosition(strConnect, user, OfficeSpaceId, dt, currentIndex, DatabaseTemp);
                case "AddUser": return US.newUser(strConnect, user, OfficeSpaceId, DatabaseTemp);
                case "EditUser": return US.editUser(strConnect, user, OfficeSpaceId, dt, currentIndex, DatabaseTemp);
                case "RunRobot": return RunRobot(dt, listSelectIndex, currentIndex, user, Position, strConnect);
                case "StopRobot": return StopRobot();
                case "ACL": return fnACL(dt, currentIndex, user, Position, strConnect);
                case "NewGroup": return US.newGroup(strConnect, user, OfficeSpaceId, DatabaseTemp);
                case "EditGroup": return US.editGroup(strConnect, user, OfficeSpaceId, dt, currentIndex, DatabaseTemp);
            }            
        }
        private bool Default()
        {
            MSG.Error("No Function Tool", "Error");
            return false;
        }
    }
}