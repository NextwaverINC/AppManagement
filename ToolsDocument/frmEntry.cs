using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ToolsDocument
{
    public partial class frmEntry : Form
    {
        TableLayoutPanel tlpMain;  
        string Command;
        public string _Command
        {
            set { Command = value; }
        }
        DataGridView dgvTemp;
        public DataGridView _dgvTemp
        {
            set { dgvTemp = value; }
        }
        public frmEntry()
        {
            InitializeComponent();
        }

        #region Event
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            getValue();
        }
        void txbDouble_TextChanged(object sender, EventArgs e)
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
        #endregion       

        #region Method
        private void InitializeForm()
        {
            int lableWidth = 0;
            for (int i = 0; i < dgvTemp.Columns.Count; i++)
            {
                int CalWidth = (9 * dgvTemp.Columns[i].HeaderText.Length);
                if (CalWidth > lableWidth) lableWidth = CalWidth;
            }

            int shiegth = (30 * dgvTemp.Columns.Count);
            int winHiegth = 90 + shiegth;
            int winWidth = 350;// (int)lableWidth + 350 + widthData;

            tlpMain = new TableLayoutPanel();
            tlpMain.Name = "table";
            tlpMain.Margin = new Padding(5);
            tlpMain.Height = shiegth;
            tlpMain.Dock = DockStyle.Top;
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, lableWidth));

            for (int i = 0; i < dgvTemp.Columns.Count; i++)
            {
                string sName = dgvTemp.Columns[i].Name;
                string sText = dgvTemp.Columns[i].HeaderText;
                string sType = dgvTemp.Columns[i].ValueType.ToString();
                string sValue = "";
                if (Command == "Edit") sValue = "" + dgvTemp.SelectedRows[0].Cells[i].Value;

                Label labTemp = new Label();
                labTemp.Margin = new Padding(3);
                labTemp.TextAlign = ContentAlignment.MiddleRight;
                labTemp.Dock = DockStyle.Fill;
                labTemp.Text = sText;
                labTemp.ForeColor = Color.DarkBlue;
                labTemp.Font = new Font("Tahoma", 9, FontStyle.Bold);
                tlpMain.Controls.Add(labTemp, 0, i);

                float sHeight = 30F;

                Control ctl = new Control();
                sType = sType.Replace("System.", "");
                switch (sType)
                {
                    case "Int32": ctl = IntData(sName, sValue, sText); break;
                    case "Double": ctl = DoubleData(sName, sValue, sText); break;
                    case "DateTime": ctl = DateData(sName, sValue, sText); break;
                    default: ctl = normalData(sName, sValue, sText); break;
                }

                tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, sHeight));
                tlpMain.Controls.Add(ctl, 1, i);
            }

            tlpMain.Height = shiegth;

            Rectangle rtg = Screen.GetWorkingArea(this);
            if (winHiegth > rtg.Height) winHiegth = rtg.Height;

            this.Size = new Size(winWidth, winHiegth);
            panelMain.Controls.Add(tlpMain);
        }      
        private void getValue()
        {
            DataTable dt = (DataTable)dgvTemp.DataSource;
            DataRow dr;
            if (Command != "Edit") dr = dt.NewRow();
            else dr = dt.Rows[dgvTemp.SelectedRows[0].Index];
            dr.BeginEdit();
            for (int i = 0; i < dgvTemp.Columns.Count; i++)
            {
                string dName = dgvTemp.Columns[i].Name;
                Control ctl = tlpMain.Controls[dName];
                string sType = ctl.GetType().Name.Replace("System.", "");
                switch (sType)
                {
                    case "NumericUpDown":
                        NumericUpDown nud = (NumericUpDown)ctl;
                        dr[i] = nud.Value;
                        break;
                    case "DateTimePicker":
                        DateTimePicker dtp = (DateTimePicker)ctl;
                        dr[i] = dtp.Value;
                        break;
                    default:
                        TextBox txb = (TextBox)ctl;
                        try { dr[i] = txb.Text; }
                        catch { }
                        break;                 
                }
            }
            dr.EndEdit();

            if (Command != "Edit") dt.Rows.Add(dr);

            dgvTemp.DataSource = dt;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Controls
        private NumericUpDown IntData(string sName, string sValue, string sText)
        {
            NumericUpDown nud = new NumericUpDown();
            try { nud.Value = decimal.Parse(sValue); }
            catch { }
            nud.Maximum = 1000000;
            nud.Dock = DockStyle.Fill;
            nud.Name = sName;
            return nud;
        }
        private TextBox normalData(string sName, string sValue, string sText)
        {
            TextBox txb = new TextBox();
            txb.Name = sName;
            txb.Tag = "NM";
            txb.Margin = new Padding(3);
            txb.Dock = DockStyle.Fill;
            txb.Text = sValue;
            txb.Font = new Font("Tahoma", 9, FontStyle.Bold);
            return txb;
        }
        private DateTimePicker DateData(string sName, string sValue, string sText)
        {
            DateTimePicker DTP = new DateTimePicker();
            try { DTP.Value = DateTime.Parse(sValue); }
            catch { }
            DTP.Dock = DockStyle.Fill;
            DTP.Name = sName;
            return DTP;
        }
        private TextBox DoubleData(string sName, string sValue, string sText)
        {
            TextBox txbDouble = new TextBox();
            txbDouble.TextChanged += new EventHandler(txbDouble_TextChanged);
            txbDouble.Text = sValue;
            txbDouble.Name = sName;
            txbDouble.Margin = new Padding(3);
            txbDouble.Dock = DockStyle.Fill;
            txbDouble.Font = new Font("Tahoma", 9, FontStyle.Bold);
            return txbDouble;
        }
        #endregion

        private void frmEntry_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }

       


    }
}