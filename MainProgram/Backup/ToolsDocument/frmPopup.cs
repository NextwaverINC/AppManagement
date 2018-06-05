using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ToolsDocument
{
    public partial class frmPopup : Form
    {
        ConnectServer.cConection cConn = new ConnectServer.cConection("");
        string Query;
        string ConnectionString;
        string ValueMember;
        string DisplayMember;
        string Search = "";
        XmlNodeList listItem;
        public frmPopup()
        {
            InitializeComponent();
        }
        public string _Value
        {
            get { return "" + dgvData.SelectedRows[0].Cells[ValueMember].Value; }
        }
        public string _Text
        {
            get { return "" + dgvData.SelectedRows[0].Cells[DisplayMember].Value; }
            set { txbSearch.Text = value; }
        }
        string RepleaceID = "";
        public string _RepleaceID
        {
            set { RepleaceID = value; }
        }


        public void funLoad(string DataSourceID, string iConnectionString)
        {
            ConnectionString = iConnectionString;
            XmlDocument XDoc = new XmlDocument();
            XDoc.Load(Application.StartupPath + @"\Config\PopupSource.xml");
            XmlNode nodeSource = XDoc.SelectSingleNode("//DataSource[@ID='" + DataSourceID + "']");
            ValueMember = "" + nodeSource.Attributes["ValueMember"].Value;
            DisplayMember = "" + nodeSource.Attributes["DisplayMember"].Value;
            Search = "" + nodeSource.Attributes["Search"].Value;
            Query = nodeSource.SelectSingleNode("./Item[@ID='Query']").InnerText;
            listItem = nodeSource.SelectNodes("./Item[@ID='Display']/Item");
            if (RepleaceID != "")
            {
                string Parameter = "" + nodeSource.Attributes["Parameter"].Value;
                Query = Query.Replace(Parameter, RepleaceID);
            }
            funSearch();
        }
        private void funSearch()
        {
            //string Sql = Query.Replace(Search, txbSearch.Text);
            //DataSet ds = cConn.SelectData(Sql, ConnectionString);
            //if (ds == null) return;
            //dgvData.DataSource = ds.Tables[0];
            //string ColumnName, HeaderText;
            //int Width;
            //for (int i = 0; i < listItem.Count; i++)
            //{
            //    ColumnName = "" + listItem[i].Attributes["ColumnName"].Value;
            //    HeaderText = "" + listItem[i].Attributes["HeaderText"].Value;
            //    Width = setWidth("" + listItem[i].Attributes["Width"].Value);
            //    dgvData.Columns[ColumnName].HeaderText = HeaderText;
            //    dgvData.Columns[ColumnName].Width = Width;
            //}
            //tsslRowCount.Text = "จำนวนรายการทั้งหมด " + dgvData.Rows.Count + " รายการ";
        }
        private int setWidth(string str)
        {
            try { return int.Parse(str); }
            catch { return 80; }
        }
        private void OK()
        {
            if (dgvData.SelectedRows.Count == 0)
            {
                MessageBox.Show("โปรดเลือกรายการข้อมูล", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            funSearch();
        }

        private void dgvData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OK();
        }

        private void txbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) funSearch();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txbSearch.Text = "";
        }
    }
}