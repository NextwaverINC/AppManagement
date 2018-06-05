using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;

namespace Users
{
    public partial class frmGroup : Form
    {
        ConnectServer.cConection cConn = new ConnectServer.cConection(cMain.UserName);
        public string _Command = "New";
        public string _ID = "";
        public frmGroup()
        {
            InitializeComponent();
        }

        public string _GroupName
        {
            set { txbGroupName.Text = value; }
        }
        DataTable createTable()
        {
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("ID");
            dtTemp.Columns.Add("USERNAME");
            return dtTemp;
        }
        public void fnLoad()
        {

            NextwaverDB.NColumns NCS = new NextwaverDB.NColumns();
            NCS.Add(new NextwaverDB.NColumn("ID"));
            NCS.Add(new NextwaverDB.NColumn("USERNAME"));

            DataTable dtTemp = cConn.Retreive(cMain.Connection, cMain.OfficeSpaceId, "system", "users", NCS);
            

            if (_Command == "New")
            {
                lbUsed.DataSource = createTable();
                lbUsed.DisplayMember = "USERNAME";
                lbUsed.ValueMember = "ID";
            }
            else
            {
                string strDoc = cConn.getLastDoc(cMain.Connection, cMain.OfficeSpaceId, "system", "user_group", int.Parse(_ID));
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(strDoc);

                DataTable dtUsed = createTable();
                XmlNodeList listItem = xDoc.SelectNodes("//Item");
                for (int i = 0; i < listItem.Count; i++)
                {
                    string ID = listItem[i].Attributes["C00"].Value;

                    DataRow dr = dtUsed.NewRow();
                    dr.BeginEdit();
                    dr["ID"] = ID;
                    dr["USERNAME"] = listItem[i].Attributes["C01"].Value;
                    dr.EndEdit();
                    dtUsed.Rows.Add(dr);

                    DataRow[] dr_list = dtTemp.Select("ID = '" + ID + "'");
                    dtTemp.Rows.Remove(dr_list[0]);
                }
                lbUsed.DataSource = dtUsed;
                lbUsed.DisplayMember = "USERNAME";
                lbUsed.ValueMember = "ID";
            }

            lbAll.DataSource = dtTemp;
            lbAll.DisplayMember = "USERNAME";
            lbAll.ValueMember = "ID";
           
        }
        private void btnNextAll_Click(object sender, EventArgs e)
        {
            DataTable dtAll = (DataTable)lbAll.DataSource;
            DataTable dtUsed = (DataTable)lbUsed.DataSource;
            for (int i = 0; i < dtAll.Rows.Count; i++)
            {
                string ID = "" + dtAll.Rows[i]["ID"];
                string USERNAME = "" + dtAll.Rows[i]["USERNAME"];
                DataRow dr = dtUsed.NewRow();
                dr.BeginEdit();
                dr["ID"] = ID;
                dr["USERNAME"] = USERNAME;
                dr.EndEdit();
                dtUsed.Rows.Add(dr);
            }
            lbUsed.DataSource = dtUsed;
            lbUsed.DisplayMember = "USERNAME";
            lbUsed.ValueMember = "ID";
                        
            lbAll.DataSource = createTable();
            lbAll.DisplayMember = "USERNAME";
            lbAll.ValueMember = "ID";
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            int Count = lbAll.SelectedItems.Count;
            DataTable dtAll = (DataTable)lbAll.DataSource;
            DataTable dtUsed = (DataTable)lbUsed.DataSource;
            string[] ID_List = new string[Count];
            for (int i = 0; i < Count; i++)
            {
                DataRowView drvTemp = (DataRowView)lbAll.SelectedItems[i];
                string ID = "" + drvTemp.Row["ID"];
                string USERNAME = "" + drvTemp.Row["USERNAME"];

                ID_List[i] = ID;

                DataRow dr = dtUsed.NewRow();
                dr.BeginEdit();
                dr["ID"] = ID;
                dr["USERNAME"] = USERNAME;
                dr.EndEdit();
                dtUsed.Rows.Add(dr);
            }
            for (int i = 0; i < ID_List.Length; i++)
            {
                DataRow[] dr_list = dtAll.Select("ID = '" + ID_List[i] + "'");
                dtAll.Rows.Remove(dr_list[0]);
            }
            lbUsed.DataSource = dtUsed;
            lbUsed.DisplayMember = "USERNAME";
            lbUsed.ValueMember = "ID";

            lbAll.DataSource = dtAll;
            lbAll.DisplayMember = "USERNAME";
            lbAll.ValueMember = "ID";
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            int Count = lbUsed.SelectedItems.Count;
            DataTable dtAll = (DataTable)lbAll.DataSource;
            DataTable dtUsed = (DataTable)lbUsed.DataSource;
            string[] ID_List = new string[Count];
            for (int i = 0; i < Count; i++)
            {
                DataRowView drvTemp = (DataRowView)lbUsed.SelectedItems[i];
                string ID = "" + drvTemp.Row["ID"];
                string USERNAME = "" + drvTemp.Row["USERNAME"];

                ID_List[i] = ID;

                DataRow dr = dtAll.NewRow();
                dr.BeginEdit();
                dr["ID"] = ID;
                dr["USERNAME"] = USERNAME;
                dr.EndEdit();
                dtAll.Rows.Add(dr);
            }
            for (int i = 0; i < ID_List.Length; i++)
            {
                DataRow[] dr_list = dtUsed.Select("ID = '" + ID_List[i] + "'");
                dtUsed.Rows.Remove(dr_list[0]);
            }
            lbUsed.DataSource = dtUsed;
            lbUsed.DisplayMember = "USERNAME";
            lbUsed.ValueMember = "ID";

            lbAll.DataSource = dtAll;
            lbAll.DisplayMember = "USERNAME";
            lbAll.ValueMember = "ID";
        }
        private void btnBackAll_Click(object sender, EventArgs e)
        {
            DataTable dtAll = (DataTable)lbAll.DataSource;
            DataTable dtUsed = (DataTable)lbUsed.DataSource;
            for (int i = 0; i < dtUsed.Rows.Count; i++)
            {
                string ID = "" + dtUsed.Rows[i]["ID"];
                string USERNAME = "" + dtUsed.Rows[i]["USERNAME"];
                DataRow dr = dtAll.NewRow();
                dr.BeginEdit();
                dr["ID"] = ID;
                dr["USERNAME"] = USERNAME;
                dr.EndEdit();
                dtAll.Rows.Add(dr);
            }
            lbAll.DataSource = dtAll;
            lbAll.DisplayMember = "USERNAME";
            lbAll.ValueMember = "ID";

            lbUsed.DataSource = createTable();
            lbUsed.DisplayMember = "USERNAME";
            lbUsed.ValueMember = "ID";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txbGroupName.Text == "")
            {
                MessageBox.Show("โปรดระบุชื่อกลุ่ม", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (lbUsed.Items.Count == 0)
            {
                MessageBox.Show("โปรดเลือกผู้ใช้งาน", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dtUsed = (DataTable)lbUsed.DataSource;

            string Items = "";
            for (int i = 0; i < dtUsed.Rows.Count; i++)
            {
                string ID = "" + dtUsed.Rows[i]["ID"];
                string USERNAME = "" + dtUsed.Rows[i]["USERNAME"];
                Items += "<Item C00='" + ID + "' C01='" + USERNAME + "' />";
            }

            string strDoc = @"<Document ID='' Name=''> 
                              <Data>
                                <Section ID='1' Name='' Type='STR'>
                                  <Items Name='' Type='SEQ'>
                                    <Means Name='Item'>
                                      <Mean ID='C00' Name='ID' Type='STR' />
                                      <Mean ID='C01' Name='USERNAME' Type='STR' />
                                    </Means>
                                    @Item
                                  </Items>
                                </Section>    
                              </Data>
                            </Document>";
            strDoc = strDoc.Replace("@Item", Items);

            NextwaverDB.NColumns NCS = new NextwaverDB.NColumns();
            NCS.Add(new NextwaverDB.NColumn("GROUP_NAME", txbGroupName.Text));
            NCS.Add(new NextwaverDB.NColumn("CREATE_DATE", DateTime.Now.ToString("dd/MM/yyyy")));
            NCS.Add(new NextwaverDB.NColumn("CREATE_BY", cMain.UserName));
            NCS.Add(new NextwaverDB.NColumn("UPDATE_DATE", DateTime.Now.ToString("dd/MM/yyyy")));
            NCS.Add(new NextwaverDB.NColumn("UPDATE_BY", cMain.UserName));

            if (_Command == "New")
            { 
                if (cConn.InsertData(cMain.Connection, cMain.OfficeSpaceId, "system", "user_group", NCS, strDoc))
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
                NextwaverDB.NWheres NWS = new NextwaverDB.NWheres();
                NWS.Add(new NextwaverDB.NWhere("ID", _ID));
                if (cConn.UpdateData(cMain.Connection, cMain.OfficeSpaceId, "system", "user_group", NCS, NWS, strDoc))
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
    }
}
