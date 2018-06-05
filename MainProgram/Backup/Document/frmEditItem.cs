using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
namespace Document
{
    public partial class frmEditItem : Form
    {
        public frmEditItem()
        {
            InitializeComponent();
        }
        public string ItemName
        {
            set { tbxItemName.Text = value; }
            get { return tbxItemName.Text; }
        }
        public string ItemType
        {
            set { cbxItemType.SelectedValue = value; }
            get { return cbxItemType.SelectedValue.ToString(); }
        }
        public void funLoad()
        {
            cControl control = new cControl();
            cbxItemType.DataSource = control.cbxDocumentType();
            cbxItemType.DisplayMember = "Name";
            cbxItemType.ValueMember = "Value";
        }
        private bool funValidation()
        {
            if (tbxItemName.Text == null || tbxItemName.Text == "")
            {
                MessageBox.Show("โปรดระบุ Item Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cbxItemType.Text == null || cbxItemType.Text == "")
            {
                MessageBox.Show("โปรดระบุ Item Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void butDCanecl_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Hide();
        }

        private void butDApply_Click(object sender, EventArgs e)
        {
            if (funValidation())
            {
                this.DialogResult = DialogResult.Yes;
                this.Hide();
            }
        }
    }
}