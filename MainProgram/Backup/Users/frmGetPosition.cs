using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Users
{
    public partial class frmGetPosition : Form
    {
        public string _POSITION_NAME
        {
            get
            {
                return "" + tvMain.SelectedNode.Tag;
            }
        }
        public string _POSITION_CODE
        {
            get
            {
                return tvMain.SelectedNode.Name;
            }
        }


        public frmGetPosition()
        {
            InitializeComponent();
        }

        private void frmGetPosition_Load(object sender, EventArgs e)
        {
            ConnectServer.cConection cConn = new ConnectServer.cConection(cMain.UserName);

            NextwaverDB.NColumns NCS_S = new NextwaverDB.NColumns();
            NCS_S.Add(new NextwaverDB.NColumn("POSITION_CODE"));
            NCS_S.Add(new NextwaverDB.NColumn("POSITION_NAME"));
            NCS_S.Add(new NextwaverDB.NColumn("PARENT_CODE"));

            DataTable dt = cConn.Retreive(cMain.Connection, cMain.OfficeSpaceId, cMain.DatabaseName, "position", NCS_S);
            string strSort = "POSITION_CODE ASC";

            DataView dtview = new DataView(dt);
            dtview.Sort = strSort;
            DataTable dtsorted = dtview.ToTable();
            ArrayList alTemp = new ArrayList();
            string POSITION_NAME, POSITION_CODE, PARENT_CODE;
            for (int i = 0; i < dtsorted.Rows.Count; i++)
            {
                POSITION_NAME = dtsorted.Rows[i]["POSITION_NAME"].ToString();
                POSITION_CODE = dtsorted.Rows[i]["POSITION_CODE"].ToString();
                PARENT_CODE = dtsorted.Rows[i]["PARENT_CODE"].ToString();

                TreeNode tnTemp = new TreeNode();
                tnTemp.Text = POSITION_NAME + " - " + POSITION_CODE;
                tnTemp.Tag = POSITION_NAME;
                tnTemp.Name = POSITION_CODE;

                alTemp.Add(tnTemp);

                if (PARENT_CODE == "")
                    tvMain.Nodes.Add(tnTemp);
                else
                {
                    for (int j = 0; j < alTemp.Count; j++)
                    {
                        TreeNode tnnTemp = (TreeNode)alTemp[j];
                        if (tnnTemp.Name == PARENT_CODE)
                        {
                            tnnTemp.Nodes.Add(tnTemp);
                            break;
                        }
                    }
                }
            }
            tvMain.ExpandAll();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (tvMain.SelectedNode == null)
            {
                MessageBox.Show("โปรดเลือกตำแหน่งหัวหน้าก่อน", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }
    }
}
