using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ToolsDocument
{
    public partial class ucRichTextBox : UserControl
    {
        public ucRichTextBox()
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
            get { return richTextBox1.Text; }
            set { richTextBox1.Text = value; }
        }

    }
}
