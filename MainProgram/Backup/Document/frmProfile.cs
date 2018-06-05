using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Document
{
    public partial class frmProfile : Form
    {
        public frmProfile()
        {
            InitializeComponent();
        }

        string Name;
        public TreeView _trvProfile
        {
            set { trvInput = value; }
            get { return trvInput; }
        }
        TreeView trvInput;
        private void butDApply_Click(object sender, EventArgs e)
        {
            if (trvProfile.Nodes == null) return;
            trvProfile.SelectedNode.Text = txbName.Text;
            MessageBox.Show("แก้ไขข้อมูลเรียบร้อยแล้ว", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void butDCanecl_Click(object sender, EventArgs e)
        {
            txbName.Text = Name;
        }

        private void tsbiDelete_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }
        private void tsbiAdd_Click(object sender, EventArgs e)
        {
            AddItem();
        }
        private void AddItem()
        {
            frmAddItemProfile frm = new frmAddItemProfile();           
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                TreeNode nodeTemp = new TreeNode();
                nodeTemp.Text = frm._Name;
                trvProfile.Nodes.Add(nodeTemp);
            }
            if (!frm.IsDisposed)
            {
                frm.Close();
                frm.Dispose();
            }

        }
        private void DeleteItem()
        {
            if (trvProfile.SelectedNode == null) return;
            if (MessageBox.Show("คุณแน่ใจหรือไม่? ที่จะลบข้อมูลนี้", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                trvProfile.Nodes.Remove(trvProfile.SelectedNode);
            }
        }
        private void SelectItemChange()
        {
            if (trvProfile.SelectedNode == null)
            {
                groupBox1.Enabled = false;
                return;
            }
            groupBox1.Enabled = true;
            TreeNode treNode = trvProfile.SelectedNode;
            txbName.Text = treNode.Text;
            Name = treNode.Text;
        }
        private void trvProfile_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectItemChange();
        }
        private void trvProfile_Click(object sender, EventArgs e)
        {
            SelectItemChange();
        }
        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            trvInput.Nodes.Clear();
            for (int i = 0; i < trvProfile.Nodes.Count; i++)
            {
                TreeNode node = new TreeNode();
                node.Text = trvProfile.Nodes[i].Text;
                trvInput.Nodes.Add(node);
            }
            this.DialogResult = DialogResult.Yes;
            this.Hide();
        }

        private void frmProfile_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < trvInput.Nodes.Count; i++)
            {
                TreeNode node = new TreeNode();
                node.Text = trvInput.Nodes[i].Text;
                trvProfile.Nodes.Add(node);
            }
        }
    }
}