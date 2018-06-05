using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Users
{
    public partial class frmPosition : Form
    {
        public string Command;
        public string ID_TEMP;

        public string _POSITION_NAME
        {
            set { txbPOSITION_NAME.Text = value; }
        }
        public string _POSITION_CODE
        {
            set { txbPOSITION_CODE.Text = value; }
        }
        public string _PARENT_CODE
        {
            set { txbPARENT_CODE.Tag = value; }
        }
        public string _PARENT_NAME
        {
            set { txbPARENT_CODE.Text = value; }
        }

        ConnectServer.cConection cConn = new ConnectServer.cConection(cMain.UserName);
        public frmPosition()
        {
            InitializeComponent();
        }
       
        bool funValidation()
        {
            string POSITION_NAME = txbPOSITION_NAME.Text;
            if (POSITION_NAME == "")
            {
                MessageBox.Show("โปรดระบุชื่อตำแหน่ง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        string getNewPOSITION_CODE(string PARENT_CODE)
        {
            NextwaverDB.NXPath NXP = new NextwaverDB.NXPath();
            NextwaverDB.NWheres NWS = new NextwaverDB.NWheres();
            NWS.whereType = true;
            NWS.Where = "[" + NXP.getMaxValue("@POSITION_CODE@") + "]";
            NWS.Where += "[@PARENT_CODE@ = '" + PARENT_CODE + "']";


            NextwaverDB.NColumns NCS_S = new NextwaverDB.NColumns();
            NCS_S.Add(new NextwaverDB.NColumn("POSITION_CODE"));

            DataTable dt = cConn.Retreive(cMain.Connection, cMain.OfficeSpaceId, cMain.DatabaseName, "position", NCS_S, NWS);
            int MaxID = 0;
            try
            {
                string PC = dt.Rows[0][0].ToString();
                if (PARENT_CODE != "")
                    PC = PC.Replace(PARENT_CODE, "");
                MaxID = int.Parse(PC);

            }
            catch { }

            string NewID = "" + (MaxID + 1);
            for (int i = NewID.Length; i < 3; i++)
            {
                NewID = "0" + NewID;
            }
            return NewID;
        }

        void funSave()
        {
            XmlNode nodeView = xDoc.SelectSingleNode("//Views");
            string strDoc = nodeView.OuterXml;
            string POSITION_CODE = txbPOSITION_CODE.Text;
            string POSITION_NAME = txbPOSITION_NAME.Text;
            string PARENT_CODE = "" + txbPARENT_CODE.Tag;

            if (!funValidation())
            {
                MessageBox.Show("โปรดระบุชื่อตำแหน่ง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Command == "New")
            {
                NextwaverDB.NColumns NCS = new NextwaverDB.NColumns();
                NCS.Add(new NextwaverDB.NColumn("POSITION_CODE", PARENT_CODE + getNewPOSITION_CODE(PARENT_CODE)));
                NCS.Add(new NextwaverDB.NColumn("POSITION_NAME", POSITION_NAME));
                NCS.Add(new NextwaverDB.NColumn("PARENT_CODE", PARENT_CODE));
                NCS.Add(new NextwaverDB.NColumn("CREATE_DATE", DateTime.Now.ToString("dd/MM/yyyy")));
                NCS.Add(new NextwaverDB.NColumn("CREATE_BY", cMain.UserName));
                NCS.Add(new NextwaverDB.NColumn("UPDATE_DATE", DateTime.Now.ToString("dd/MM/yyyy")));
                NCS.Add(new NextwaverDB.NColumn("UPDATE_BY", cMain.UserName));

                if (cConn.InsertData(cMain.Connection, cMain.OfficeSpaceId, cMain.DatabaseName, "position", NCS, strDoc))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("ไม่สามารถเพิ่มข้อมูลได้เนื่องจาก:" + cConn.ErrorMsg(), "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                NextwaverDB.NColumns NCS = new NextwaverDB.NColumns();
                NCS.Add(new NextwaverDB.NColumn("POSITION_NAME", POSITION_NAME));               
                NCS.Add(new NextwaverDB.NColumn("UPDATE_DATE", DateTime.Now.ToString("dd/MM/yyyy")));
                NCS.Add(new NextwaverDB.NColumn("UPDATE_BY", cMain.UserName));

                NextwaverDB.NWheres NWS = new NextwaverDB.NWheres();
                NWS.Add("ID", ID_TEMP);
              
                if (cConn.UpdateData(cMain.Connection, cMain.OfficeSpaceId, cMain.DatabaseName, "position", NCS,NWS, strDoc))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("ไม่สามารถแก้ไขข้อมูลได้เนื่องจาก:" + cConn.ErrorMsg(), "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }

        }
        XmlDocument xDoc;
        public void funRefreshTree(XmlNodeList listView)
        {
            tvMain.Nodes.Clear();
            for (int i = 0; i < listView.Count; i++)
            {
                TreeNode tnTemp = new TreeNode();
                tnTemp.Text = listView[i].Attributes["Name"].Value;
                tnTemp.Tag = listView[i];

                if (listView[i].Attributes["Enable"].Value == "T") tnTemp.Checked = true;
                else tnTemp.Checked = false;

                XmlNodeList listItem = listView[i].SelectNodes("./Item");
                for (int j = 0; j < listItem.Count; j++)
                {
                    TreeNode tnnTemp = new TreeNode();
                    tnnTemp.Text = listItem[j].Attributes["Name"].Value;
                    tnnTemp.Tag = listItem[j];
                    if (listItem[j].Attributes["Enable"].Value == "T") tnnTemp.Checked = true;
                    else tnnTemp.Checked = false;
                    tnTemp.Nodes.Add(tnnTemp);
                }
                tvMain.Nodes.Add(tnTemp);
            }
            tvMain.ExpandAll();
        }
        public void funLoadTree()
        {
            xDoc = new XmlDocument();
            XmlNodeList listView;
            if (Command == "New")
            {
                xDoc.Load(Application.StartupPath + "/MConfig.xml");
                listView = xDoc.SelectNodes("//Config/Views/View");
            }
            else
            {
                string strXML = cConn.getLastDoc(cMain.Connection, cMain.OfficeSpaceId, cMain.DatabaseName, "position", int.Parse(ID_TEMP));
                xDoc.LoadXml(strXML);
                listView = xDoc.SelectNodes("//Views/View");
            }
            funRefreshTree(listView);
        }

        private void frmPosition_Load(object sender, EventArgs e)
        {        
            funLoadTree();
        }

        private void tvMain_AfterCheck(object sender, TreeViewEventArgs e)
        {
            XmlNode nodeTemp = (XmlNode)e.Node.Tag;
            if (e.Node.Checked)
                nodeTemp.Attributes["Enable"].Value = "T";
            else
                nodeTemp.Attributes["Enable"].Value = "F";
        }   

        private void btnParent_Click(object sender, EventArgs e)
        {
            frmGetPosition frm = new frmGetPosition();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txbPARENT_CODE.Text = frm._POSITION_NAME;
                txbPARENT_CODE.Tag = frm._POSITION_CODE;
            }
            if (!frm.IsDisposed)
            {
                frm.Close();
                frm.Dispose();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            funSave();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            xDoc = new XmlDocument();
            XmlNodeList listView;
            xDoc.Load(Application.StartupPath + "/MConfig.xml");
            listView = xDoc.SelectNodes("//Config/Views/View");
            funRefreshTree(listView);
        }
    }
}