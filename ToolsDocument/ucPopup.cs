using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ToolsDocument
{
    public partial class ucPopup : UserControl
    {
        string DataSourceID;
        string ConnectionString;
        public string _DataSourceID
        {
            set { DataSourceID = value; }
            get { return DataSourceID; }
        }
        string RepleaceID = "";
        public string _RepleaceID
        {
            set { RepleaceID = value; }
        }

        public string _ConnectionString
        {
            set { ConnectionString = value; }
        }
        string XPath;
        public string _XPath
        {
            set { XPath = value; }
            get { return XPath; }
        }
        public string _Value
        {
            get { return ""+textBox1.Tag; }
            set { textBox1.Tag = value; }
        }
        public string _Text
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }


        public ucPopup()
        {
            InitializeComponent();
        }

        ucPopup ucSubPopup;
        public ucPopup GetSubPopup()
        {
            return ucSubPopup;
        }
        public void AddSubPopup(ucPopup ucP)
        {
            ucSubPopup = ucP;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            frmPopup frm = new frmPopup();
            frm._Text = textBox1.Text;
            frm._RepleaceID = RepleaceID;
            frm.funLoad(DataSourceID, ConnectionString);           
            if (frm.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = frm._Text;
                textBox1.Tag = frm._Value;
                if (ucSubPopup != null)
                {
                    ucSubPopup._RepleaceID = frm._Value;
                    ucSubPopup._Text = "";
                    ucSubPopup._Value = "";
                }
            }
            if (!frm.IsDisposed)
            {
                frm.Close();
                frm.Dispose();
            }
        }
    }
}
