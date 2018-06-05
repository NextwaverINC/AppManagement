using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Document
{
    public partial class frmAddItemProfile : Form
    {
        public frmAddItemProfile()
        {
            InitializeComponent();
        }
        public string _Name
        {
            get { return txbName.Text; }
        }
        private void butDApply_Click(object sender, EventArgs e)
        {
            if (txbName.Text == "" || txbName.Text == null)
            {
                MessageBox.Show("โปรดระบุชื่อ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txbName.Focus();
                return;
            }
            this.DialogResult = DialogResult.Yes;
            this.Hide();
        }

        private void butDCanecl_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Hide();
        }
    }
}