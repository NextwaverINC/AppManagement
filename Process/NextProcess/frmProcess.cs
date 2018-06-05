using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace NextProcess
{
    public partial class frmProcess : Form
    {
        public frmProcess()
        {
            InitializeComponent();
        }

        private void frmProcess_Load(object sender, EventArgs e)
        {
            fnLoadConfig();
        }
        private XmlDocument getXmlConfig()
        {
            XmlDocument xConfig = new XmlDocument();
            xConfig.Load(Application.StartupPath + "/Config_Process.xml");
            return xConfig;
        }
        private void fnLoadConfig()
        {
            XmlDocument xConfig = getXmlConfig();
            XmlNodeList nodeProcess = xConfig.SelectNodes("//Config/Process");

            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("ID");
            dtTemp.Columns.Add("Name");
            dtTemp.Columns.Add("WSTable");
           

            dtTemp.Columns.Add("WSConnectionID");
            dtTemp.Columns.Add("DBConnectionID");
            dtTemp.Columns.Add("Detail");
            dtTemp.Columns.Add("UpdateDate");
           

            for (int i = 0; i < nodeProcess.Count; i++)
            {
                //<Process ID="P01" Name="users" WSConnectionID="NWS01" DBConnectionID="MSSQL01" Detail="ข้อมูลผู้ใช้งาน" UpdateDate="">
                string ID = nodeProcess[i].Attributes["ID"].Value;
                string Name = nodeProcess[i].Attributes["Name"].Value;
                string WSTable = nodeProcess[i].Attributes["WSTable"].Value;
               
                string WSConnectionID = nodeProcess[i].Attributes["WSConnectionID"].Value;
                string DBConnectionID = nodeProcess[i].Attributes["DBConnectionID"].Value;
                string Detail = nodeProcess[i].Attributes["Detail"].Value;
                string UpdateDate = nodeProcess[i].Attributes["UpdateDate"].Value;

                DataRow DR = dtTemp.NewRow();
                DR.BeginEdit();
                DR["ID"] = ID;
                DR["Name"] = Name;
                DR["WSConnectionID"] = WSConnectionID;
                DR["DBConnectionID"] = DBConnectionID;
                DR["WSTable"] = WSTable;
                DR["Detail"] = Detail;
                DR["UpdateDate"] = UpdateDate;
                DR.EndEdit();
                dtTemp.Rows.Add(DR);
            }

            dgvProcess.DataSource = dtTemp;
            
        }
        private void fnProcess(object sender, DoWorkEventArgs e)
        {
            this.Invoke(
               (MethodInvoker)delegate()
               {
                   XmlDocument xConfig = getXmlConfig();
                   ConnectServer.cConection cConn = new ConnectServer.cConection("system");
                   rtbError.Text = "[" + DateTime.Now.ToShortTimeString() + "] Start Loop Process";
                   for (int i = 0; i < dgvProcess.SelectedRows.Count; i++)
                   {
                       List<string> Sql_List = new List<string>();

                       string ID = "" + dgvProcess.SelectedRows[i].Cells["ID"].Value;
                       string WSConnectionID = "" + dgvProcess.SelectedRows[i].Cells["WSConnectionID"].Value;
                       string DBConnectionID = "" + dgvProcess.SelectedRows[i].Cells["DBConnectionID"].Value;
                       string ProcessName = "" + dgvProcess.SelectedRows[i].Cells["Name"].Value;

                       rtbError.Text += Environment.NewLine + "[" + DateTime.Now.ToShortTimeString() + "]   Start Process " + ProcessName;

                       //<Item ID="NEXWS01" Type="NEXWS" Connection="NextwaverDatabase" Database="system" />
                       XmlNode nodeWSConnection = xConfig.SelectSingleNode("//Config/Connection/Item[@ID='" + WSConnectionID + "']");
                       string WSConnection = nodeWSConnection.Attributes["Connection"].Value;
                       string WSDatabase = nodeWSConnection.Attributes["Database"].Value;
                       string OfficeSpaceId = nodeWSConnection.Attributes["OfficeSpaceId"].Value;

                       XmlNode nodeProcess = xConfig.SelectSingleNode("//Process[@ID='" + ID + "']");
                       string Sql_Insert = nodeProcess.SelectSingleNode("./Query/Insert").InnerText;
                       string Sql_Delete = nodeProcess.SelectSingleNode("./Query/Delete").InnerText;
                       string WSTable = nodeProcess.Attributes["WSTable"].Value;

                       DataTable dt = cConn.Retreive(WSConnection, OfficeSpaceId, WSDatabase, WSTable);
                       XmlNodeList listColumn = nodeProcess.SelectNodes("./Columns/Column");

                       for (int k = 0; k < dt.Rows.Count; k++)
                       {
                           string TempInsert = Sql_Insert;
                           string TempDelete = Sql_Delete;
                           DataRow DR = dt.Rows[k];
                           for (int j = 0; j < listColumn.Count; j++)
                           {
                               //<Column Type="STR" Name="ID" Parameter="@ID@" />
                               string TempType = "" + listColumn[j].Attributes["Type"].Value;
                               string TempName = "" + listColumn[j].Attributes["Name"].Value;
                               string TempParameter = "" + listColumn[j].Attributes["Parameter"].Value;
                               string TempValue = "";
                               try
                               {
                                   TempValue = "" + DR[TempName];
                               }
                               catch { }
                               TempDelete = TempDelete.Replace(TempParameter, TempValue);
                               switch (TempType)
                               {
                                   case "STR":
                                       TempInsert = TempInsert.Replace(TempParameter, "" + DR[TempName]);
                                       break;
                                   case "XML":
                                       string strXML = cConn.getLastDoc(WSConnection, OfficeSpaceId, WSDatabase, WSTable, int.Parse("" + DR["ID"]));
                                       TempInsert = TempInsert.Replace(TempParameter, strXML);
                                       break;
                               }
                           }

                           Sql_List.Add(TempDelete);
                           Sql_List.Add(TempInsert);
                       }

                       //<Item ID="MSSQL01" Type="MSSQL" Server="localhost" Database="TestProcess" Login="sa" Password="nextwaver" />    
                       XmlNode nodeDBConnection = xConfig.SelectSingleNode("//Config/Connection/Item[@ID='" + DBConnectionID + "']");
                       string DBServer = nodeDBConnection.Attributes["Server"].Value;
                       string DBDatabase = nodeDBConnection.Attributes["Database"].Value;
                       string DBLogin = nodeDBConnection.Attributes["Login"].Value;
                       string DBPassword = nodeDBConnection.Attributes["Password"].Value;
                       string ConnectoinString = "Data Source=" + DBServer + "; uid=" + DBLogin + "; pwd=" + DBPassword + "; Initial Catalog=" + DBDatabase + ";";

                       string ErrMsg = "";
                       if (!cConn.Execute(Sql_List.ToArray(), ConnectoinString, out ErrMsg))
                       {
                           rtbError.Text += Environment.NewLine + "[" + DateTime.Now.ToShortTimeString() + "]   ประมวลผล " + ProcessName + " ไม่สำเร็จ";
                           rtbError.Text += Environment.NewLine + "              ข้อผิดพลาด : " + ErrMsg;
                       }
                       else
                       {
                           rtbError.Text += Environment.NewLine + "[" + DateTime.Now.ToShortTimeString() + "]   ประมวลผล " + ProcessName + " สำเร็จ";
                       }
                       nodeProcess.Attributes["UpdateDate"].Value = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
                       rtbError.Text += Environment.NewLine + "[" + DateTime.Now.ToShortTimeString() + "]   End Process " + ProcessName;
                   }
                   rtbError.Text += Environment.NewLine + "[" + DateTime.Now.ToShortTimeString() + "] End Loop Process";

                   xConfig.Save(Application.StartupPath + "/Config_Process.xml");
                   fnLoadConfig();
               }
           );

           
        }

        frmWait frmW = new frmWait();
        private void btnProcess_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(fnProcess);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunComplete);
            frmW = new frmWait();
            bw.RunWorkerAsync();
            frmW.ShowDialog();
            frmW = null;           


            //fnProcess();
        }

        private void RunComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (frmW != null)
            {
                frmW.Hide();
                frmW = null;
            }
        }
    }
}
