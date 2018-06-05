using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Users
{
    public partial class frmUser : Form
    {
        public string Command;
        public string ID_TEMP;
        public string USERNAME_TEMP;
        public string _USERNAME
        {
            set { txbUSERNAME.Text = value; USERNAME_TEMP = value; }
        }
        public string _Password
        {
            set { txbPASSWORD.Text = value; txbPASSWORD2.Text = value; }
        }
        public string _Title
        {
            set { txbTITLE.Text = value; }
        }
        public string _FIRSTNAME
        {
            set { txbFIRSTNAME.Text = value; }
        }
        public string _LASTNAME
        {
            set { txbLASTNAME.Text = value; }
        }
        public string _POSITION_CODE
        {
            set { txbPOSITION_CODE.Tag = value; }
        }
        public string _POSITION_NAME
        {
            set { txbPOSITION_CODE.Text = value; }
        }


        ConnectServer.cConection cConn = new ConnectServer.cConection(cMain.UserName);
        public frmUser()
        {
            InitializeComponent();
        }
        private bool Validation()
        {
            if (txbUSERNAME.Text == "")
            {
                MessageBox.Show("โปรดระบุ ชื่อผู้ใช้งาน", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; 
            }
            if (txbPASSWORD.Text == "")
            {
                MessageBox.Show("โปรดระบุ รหัสผ่าน", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; 
            }
            if (txbPASSWORD2.Text == "")
            {
                MessageBox.Show("โปรดระบุ ยืนยันรหัสผ่าน", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txbPASSWORD.Text != txbPASSWORD2.Text)
            {
                MessageBox.Show("โปรดระบุ รหัสผ่าน และ ยืนยันรหัสผ่าน ให้เหมือนกัน", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txbPOSITION_CODE.Text == "")
            {
                MessageBox.Show("โปรดระบุ ตำแหน่ง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
                       
            NextwaverDB.NWheres NWS = new NextwaverDB.NWheres();
            NWS.whereType =true;
            NWS.Where = "[@USERNAME@ = '"+txbUSERNAME.Text+"']";           
            if (Command != "New")
            {
                NWS.Where += "[@USERNAME@ != '" + USERNAME_TEMP + "']";
            }                       

            NextwaverDB.NColumns NCS_S = new NextwaverDB.NColumns();
            NCS_S.Add(new NextwaverDB.NColumn("USERNAME"));

            DataTable dt = cConn.Retreive(cMain.Connection, cMain.OfficeSpaceId, cMain.DatabaseName, "users", NCS_S, NWS);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("ชื่อผู้ใช้งานนี้มีอยู่แล้วในระบบโปรดระบุใหม่", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        void funSave()
        {
            if (Validation())
            {
                string USERNAME = txbUSERNAME.Text;
                string PASSWORD = txbPASSWORD.Text;
                string TITLE = txbTITLE.Text;
                string FIRSTNAME = txbFIRSTNAME.Text;
                string LASTNAME = txbLASTNAME.Text;
                string POSITION_CODE = "" + txbPOSITION_CODE.Tag;

                if (Command == "New")
                {
                    NextwaverDB.NColumns NCS = new NextwaverDB.NColumns();
                    NCS.Add(new NextwaverDB.NColumn("USERNAME", USERNAME));
                    NCS.Add(new NextwaverDB.NColumn("PASSWORD", PASSWORD));
                    NCS.Add(new NextwaverDB.NColumn("TITLE", TITLE));
                    NCS.Add(new NextwaverDB.NColumn("FIRSTNAME", FIRSTNAME));
                    NCS.Add(new NextwaverDB.NColumn("LASTNAME", LASTNAME));
                    NCS.Add(new NextwaverDB.NColumn("POSITION_CODE", POSITION_CODE));
                    NCS.Add(new NextwaverDB.NColumn("CREATE_DATE", DateTime.Now.ToString("dd/MM/yyyy")));
                    NCS.Add(new NextwaverDB.NColumn("CREATE_BY", cMain.UserName));
                    NCS.Add(new NextwaverDB.NColumn("UPDATE_DATE", DateTime.Now.ToString("dd/MM/yyyy")));
                    NCS.Add(new NextwaverDB.NColumn("UPDATE_BY", cMain.UserName));

                    if (cConn.InsertData(cMain.Connection, cMain.OfficeSpaceId, cMain.DatabaseName, "users", NCS))
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
                    NCS.Add(new NextwaverDB.NColumn("PASSWORD", PASSWORD));
                    NCS.Add(new NextwaverDB.NColumn("TITLE", TITLE));
                    NCS.Add(new NextwaverDB.NColumn("FIRSTNAME", FIRSTNAME));
                    NCS.Add(new NextwaverDB.NColumn("LASTNAME", LASTNAME));
                    NCS.Add(new NextwaverDB.NColumn("POSITION_CODE", POSITION_CODE));                  
                    NCS.Add(new NextwaverDB.NColumn("UPDATE_DATE", DateTime.Now.ToString("dd/MM/yyyy")));
                    NCS.Add(new NextwaverDB.NColumn("UPDATE_BY", cMain.UserName));

                    NextwaverDB.NWheres NWS = new NextwaverDB.NWheres();
                    NWS.Add("ID", ID_TEMP);

                    if (cConn.UpdateData(cMain.Connection, cMain.OfficeSpaceId, cMain.DatabaseName, "users", NCS, NWS))
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
        private void funLoad()
        {
            if (Command != "New")
            {
                txbUSERNAME.Enabled = false;
            }
        }
        private void btnSelectPosition_Click(object sender, EventArgs e)
        {
            frmGetPosition frm = new frmGetPosition();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txbPOSITION_CODE.Text = frm._POSITION_NAME;
                txbPOSITION_CODE.Tag = frm._POSITION_CODE;
            }
            if (!frm.IsDisposed)
            {
                frm.Close();
                frm.Dispose();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmUser_Load(object sender, EventArgs e)
        {
            funLoad();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            funSave();
        }
    }
}
