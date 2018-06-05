using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ToolsDocument
{
    public partial class ucTextBox : UserControl
    {
        public ucTextBox()
        {
            InitializeComponent();
        }

        string XPath;
        public char _PasswordChar
        {
            set { textBox1.PasswordChar = value; }
            get { return textBox1.PasswordChar; }
        }
        public string _XPath
        {
            set { XPath = value; }
            get { return XPath; }
        }
        public string _Value
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
    }
}
