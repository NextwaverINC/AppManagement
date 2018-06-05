using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ToolsDocument
{
    public partial class ucCheckBox : UserControl
    {
        public ucCheckBox()
        {
            InitializeComponent();
        }

        string XPath;
        public string _XPath
        {
            set { XPath = value; }
            get { return XPath; }
        }
        public bool _Checked
        {
            get { return chbTemp.Checked; }
            set { chbTemp.Checked = value; }
        }
        public string _Text
        {
            set { chbTemp.Text = value; }
            get { return chbTemp.Text; }
        }

    }
}
