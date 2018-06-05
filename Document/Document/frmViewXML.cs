using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Document
{
    public partial class frmViewXML : Form
    {
        public frmViewXML()
        {
            InitializeComponent();
        }
        string FilePath;
        public string _FilePaht
        {
            set { FilePath = value; }
        }
        private void frmViewXML_Load(object sender, EventArgs e)
        {           
            webBrowser1.Navigate(FilePath);
        }
    }
}