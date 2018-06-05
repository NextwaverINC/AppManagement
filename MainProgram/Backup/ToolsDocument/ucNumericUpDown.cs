using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ToolsDocument
{
    public partial class ucNumericUpDown : UserControl
    {
        public decimal _MinValue
        {
            set { numericUpDown1.Minimum = value; }
            get { return numericUpDown1.Minimum; }
        }
        public decimal _MaxValue
        {
            set { numericUpDown1.Maximum = value; }
            get { return numericUpDown1.Maximum; }
        }
        public decimal _Value
        {
            set { numericUpDown1.Value = value; }
            get { return numericUpDown1.Value; }
        }
        string XPath;
        public string _XPath
        {
            set { XPath = value; }
            get { return XPath; }
        }

        public ucNumericUpDown()
        {
            InitializeComponent();
        }
    }
}
