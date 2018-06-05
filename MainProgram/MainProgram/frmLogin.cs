using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MainProgram
{
    public partial class frmLogin : Form
    {
        public string USERNAME = "";
        public string Position = null;
        public string Position_ID = "";
        public bool bExtra = false;
        public string _Connection;
        public frmLogin()
        {
            InitializeComponent();
        }
        private bool funValidate()
        {
            NextwaverDB.NWheres NWS = new NextwaverDB.NWheres();
            NWS.Add(new NextwaverDB.NWhere("USERNAME", txtUser.Text));
            NWS.Add(new NextwaverDB.NWhere("PASSWORD", txtPass.Text));

            NextwaverDB.NColumns NCS_S = new NextwaverDB.NColumns();
            NCS_S.Add(new NextwaverDB.NColumn("USERNAME"));
            NCS_S.Add(new NextwaverDB.NColumn("POSITION_CODE"));
            NCS_S.Add(new NextwaverDB.NColumn("ID"));
            ConnectServer.cConection cConn = new ConnectServer.cConection("System");
            
            DataTable dt = cConn.Retreive(_Connection, Program.OffiecSpaceId, Program.DatabaseName, "users", NCS_S, NWS);
            if (dt.Rows.Count == 0) return false;

            Position = "" + dt.Rows[0]["POSITION_CODE"];

            Position_ID = "" + dt.Rows[0]["ID"];
            return true;           
        }       
        private void checkPass()
        {
            if (!funValidate())
            {
                MessageBox.Show("UserName หรือ Password ไม่ถูกต้อง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            if (!bExtra)
            {
                frmMenu frm = new frmMenu(Program.ServerName);
                frm._Connection = _Connection;
                frm._UserID = txtUser.Text;
                frm._Position = Position;
                frm.position_id = Position_ID;
                this.Hide();
                frm.ShowDialog();
                this.Close();
                this.Dispose();
            }
            else
            {
                USERNAME = txtUser.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
                this.Dispose();
            }
        }
        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            checkPass();
        }
    }
}
