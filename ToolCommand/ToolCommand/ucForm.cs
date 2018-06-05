using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ToolCommand
{
    public partial class ucForm : UserControl
    {
        public ucForm()
        {
            InitializeComponent();
        }

        public string _Header
        {
            set { Header1.Text = value; }
        }
        public Panel _MainPanel
        {
            get { return panelMain; }
            set { panelMain = value; }
        }

       

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (this.Parent.GetType() == typeof(System.Windows.Forms.TabPage))
            {
                System.Windows.Forms.TabPage TP = (System.Windows.Forms.TabPage)this.Parent;
                System.Windows.Forms.TabControl TC = (System.Windows.Forms.TabControl)TP.Parent;
                TC.TabPages.Remove(TP);
            }
            else
            {
                this.ParentForm.Close();
            }  
        }
    }
}
