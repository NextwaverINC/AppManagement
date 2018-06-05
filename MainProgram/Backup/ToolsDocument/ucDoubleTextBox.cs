using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ToolsDocument
{
    public partial class ucDoubleTextBox : UserControl
    {
        public ucDoubleTextBox()
        {
            InitializeComponent();
        }
        string XPath;
        public string _XPath
        {
            set { XPath = value; }
            get { return XPath; }
        }
        public string _Value
        {
            get { return textBox1.Text; }
            set
            {
                string sValue = value;
                if (sValue != null && sValue != "")
                    textBox1.Text = value;
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox txb = (TextBox)sender;
            try
            {
                if (txb.Text == "") return;
             
                double value = double.Parse(txb.Text);                
            }
            catch
            {
                MessageBox.Show("โปรดระบุข้อมูลเป็นตัวเลข", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txb.Text = "";
            }
        }
    }
}
