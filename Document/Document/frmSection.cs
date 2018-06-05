using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Document
{
    public partial class frmSection : Form
    {
        int AutoID=9999;
        public int _AutoID
        {
            set { AutoID = value; }
        }
        public string _SectionID
        {
            get { return tbxSectionID.Text; }       
        }
        public string _SectionName
        {
            get { return tbxSectionName.Text; }
        }
        public string _SectionType
        {
            get { return cbxSectionType.SelectedValue.ToString(); }
        }
        public frmSection()
        {
            InitializeComponent();
        }
        private bool funValidate()
        {
            if (checkNull(tbxSectionID.Text))
            {
                MessageBox.Show("โปรดระบุ SectionID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (checkNull(tbxSectionName.Text))
            {
                MessageBox.Show("โปรดระบุ Section Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (checkNull(cbxSectionType.Text))
            {
                MessageBox.Show("โปรดระบุ Section Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool checkNull(string str)
        {
            if (str == "" || str == null) return true;
            else return false;
        }
        private void butOK_Click(object sender, EventArgs e)
        {
            if (funValidate())
            {
                this.DialogResult = DialogResult.OK;
                this.Hide();
            } 
        }
        private void frmSection_Load(object sender, EventArgs e)
        {
            if (AutoID != 9999)
            {                
                tbxSectionID.Text = "" + AutoID;
                tbxSectionID.Enabled = false;
            }
            funLoad();
        }
        private void funLoad()
        {
            cControl control = new cControl();
            cbxSectionType.DataSource = control.cbxDocumentType();
            cbxSectionType.DisplayMember = "Name";
            cbxSectionType.ValueMember = "Value";

        }
    }
}