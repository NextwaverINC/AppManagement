using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACL
{
    public partial class frmACL : Form
    {
        ConnectServer.cConection cConn;
        public string Connection;
        public string UserName;
        public string OfficeSpaceId;
        public string DatabaseName;
        public string TableName;
        public string DocId;
        public frmACL()
        {
            InitializeComponent();
        }

        void fnLoadGrid()
        {
            NextwaverDB.NWheres NWS = new NextwaverDB.NWheres();
            NWS.Add(new NextwaverDB.NWhere("CLASS", DatabaseName));
            NWS.Add(new NextwaverDB.NWhere("OBJECT", TableName));
            NWS.Add(new NextwaverDB.NWhere("DOC_ID", DocId));

            DataTable dt = cConn.Retreive(Connection, OfficeSpaceId, "system", "acl_users", NWS);

            dataGridView1.DataSource = dt;

            tsslDetail.Text = "จำนวนข้อมูลทั้งหมด " + dt.Rows.Count + " รายการ";

            if (dt.Rows.Count == 0)
            {
                labHeader.Text = "ข้อมูล ACL(รายการสร้างใหม่)";
                dataGridView1.Enabled = false;
            }
            else
            {
                dataGridView1.Rows[0].Selected = true;
                gridSelection();
                labHeader.Text = "ข้อมูล ACL(รายการแก้ไข)";
            }
        }

        public void fnLoad(string iConnection, string iUserName, string iOfficeSpaceId, string iDatabaseName, string iTableName,string iDocId)
        {
            Connection = iConnection;
            UserName = iUserName;
            OfficeSpaceId = iOfficeSpaceId;
            DatabaseName = iDatabaseName;
            TableName = iTableName;
            DocId = iDocId;
            cConn = new ConnectServer.cConection(iUserName);

            fnLoadGrid();

            labCLASS.Text = DatabaseName;
            labOBJECT.Text = TableName;
            labDOC_ID.Text = DocId;

            DataTable dt = cConn.Retreive(Connection, OfficeSpaceId, "system", "users");
            cbxUser.DataSource = dt;
            cbxUser.DisplayMember = "USERNAME";
            cbxUser.ValueMember = "USERNAME";

            DataTable dtFun = new DataTable("Data");
            dtFun.Columns.Add("Name");

            string[] FunList = Function.FUNCTION_LIST;

            for (int i = 0; i < FunList.Length; i++)
            {
                DataRow dr = dtFun.NewRow();
                dr.BeginEdit();
                dr["Name"] = FunList[i];
                dr.EndEdit();
                dtFun.Rows.Add(dr);
            }

            cbxFunction.DataSource = dtFun;
            cbxFunction.DisplayMember = "Name";
            cbxFunction.ValueMember = "Name";


        }

        void gridSelection()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                labHeader.Text = "ข้อมูล ACL(รายการแก้ไข)";
                cbxFunction.SelectedValue = "" + dataGridView1.SelectedRows[0].Cells["FUNCTION"].Value;
                labID.Text = "" + dataGridView1.SelectedRows[0].Cells["ID"].Value;
                cbxUser.SelectedValue = "" + dataGridView1.SelectedRows[0].Cells["USERNAME"].Value;
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            gridSelection();
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = false;            
            labID.Text = "New";
            labHeader.Text = "ข้อมูล ACL(รายการสร้างใหม่)";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbxFunction.SelectedValue == "")
            {
                MessageBox.Show("ข้อผิดพลาด", "โปรดเลือก Function", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!dataGridView1.Enabled) //รายการใหม่
            {
                NextwaverDB.NColumns NCS = new NextwaverDB.NColumns();
                NCS.Add(new NextwaverDB.NColumn("CLASS", DatabaseName));
                NCS.Add(new NextwaverDB.NColumn("OBJECT", TableName));
                NCS.Add(new NextwaverDB.NColumn("DOC_ID", DocId));
                NCS.Add(new NextwaverDB.NColumn("USERNAME", "" + cbxUser.SelectedValue));
                NCS.Add(new NextwaverDB.NColumn("FUNCTION", "" + cbxFunction.SelectedValue));

                if (cConn.InsertData(Connection, OfficeSpaceId, "system", "acl_users", NCS))
                {
                    dataGridView1.Enabled = true;
                    fnLoadGrid();
                }
                else
                {
                    MessageBox.Show("ไม่สามารถเพิ่มข้อมูลได้เนื่องจาก:" + cConn.ErrorMsg(), "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else //รายการแก้ไข
            {
                NextwaverDB.NColumns NCS = new NextwaverDB.NColumns();
                NCS.Add(new NextwaverDB.NColumn("FUNCTION", "" + cbxFunction.SelectedValue));
                NCS.Add(new NextwaverDB.NColumn("USERNAME", "" + cbxUser.SelectedValue));
                NextwaverDB.NWheres NWS = new NextwaverDB.NWheres();
                NWS.Add("ID", labID.Text);

                if (cConn.UpdateData(Connection, OfficeSpaceId, "system", "acl_users", NCS, NWS))
                {
                    dataGridView1.Enabled = true;
                    fnLoadGrid();
                }
                else
                {
                    MessageBox.Show("ไม่สามารถแก้ไขข้อมูลได้เนื่องจาก:" + cConn.ErrorMsg(), "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

           
        }
    }
}
