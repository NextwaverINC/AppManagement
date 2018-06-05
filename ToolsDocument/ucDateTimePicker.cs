using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ToolsDocument
{
    public partial class ucDateTimePicker : UserControl
    {
        public ucDateTimePicker()
        {
            InitializeComponent();
        }

        string XPath;
        public string _XPath
        {
            set { XPath = value; }
            get { return XPath; }
        }        
        public int _Day
        {
            get { return dateTimePicker1.Value.Day; }          
        }
        public int _Month
        {
            get { return dateTimePicker1.Value.Month; }
        }
        public int _Year
        {
            get { return dateTimePicker1.Value.Year; }
        }
        public void setValue(int Day, int Month, int Year)
        {
            dateTimePicker1.Value = new DateTime(Year, Month, Day);
        }
    }
}
