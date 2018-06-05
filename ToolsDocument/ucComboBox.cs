using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ToolsDocument
{
    public partial class ucComboBox : UserControl
    {
        public delegate void TextChange(object sender, EventArgs e);
        // Declare the Event (field-like syntax)
        public event TextChange textChange;

        ConnectServer.cConection cConn = new ConnectServer.cConection("");
        string ConnectionString;
        public ucComboBox()
        {
            InitializeComponent();
        }
        string XPath;
        /// <summary>
        /// XPath of XML Document
        /// </summary>
        public string _XPath
        {
            set { XPath = value; }
            get { return XPath; }
        }
        /// <summary>
        ///get Table Souce => ColumnName = Text,Value
        /// </summary>
        public DataTable _CreateTableSource
        {
            get
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Text");
                dt.Columns.Add("Value");
                return dt;
            }
        }
        /// <summary>
        ///set DataSource(DataSource Create From CreateTableSource)
        /// </summary>
        public DataTable _DataSource
        {
            set
            {
                cbxTemp.DataSource = value;
                cbxTemp.DisplayMember = DisplayMember;
                cbxTemp.ValueMember = ValueMember;               
            }
        }
        string DisplayMember;
        string ValueMember;
        public string _SelectedValue
        {
            set { cbxTemp.SelectedValue = value; }
            get { return "" + cbxTemp.SelectedValue; }
        }
        public string _SelectedText
        {
            set { cbxTemp.SelectedText = value; }
            get { return cbxTemp.SelectedText; }
        }

        string DataSourceID;
        public string _DataSourceID
        {
            get { return DataSourceID; }
            set { DataSourceID = value; }
        }
        string RepleaceID = "";
        public string _RepleaceID
        {
            set { RepleaceID = value; }
        }

        public bool CreateDataSource(string iDataSourceID, string iConnectionString)
        {
            DataSourceID = iDataSourceID;
            ConnectionString = iConnectionString;
            XmlDocument xTemp = new XmlDocument();
            xTemp.Load(Application.StartupPath + @"\Config\ComboBoxSource.xml");

            XmlNode nodeSource = xTemp.SelectSingleNode("//DataSource[@ID='" + DataSourceID + "']");
            string SourceType = "" + nodeSource.Attributes["Type"].Value;

            switch (SourceType)
            {
                case "Item":
                    return CreateItem(nodeSource.SelectNodes("./Item"));
                case "Query":
                    return CreateQuery(nodeSource);
            }
            return false;
        }
        private bool CreateQuery(XmlNode nodeSource)
        {
            //string Sql = nodeSource.SelectSingleNode("./Query").InnerText;
            //if (RepleaceID != "")
            //{
            //    string Where = nodeSource.SelectSingleNode("./Where").InnerText;
            //    string Parameter = "" + nodeSource.SelectSingleNode("./Where").Attributes["Parameter"].Value;
            //    Sql = Sql + " " + Where.Replace(Parameter, RepleaceID);
            //}
            //ValueMember = "" + nodeSource.Attributes["Value"].Value;
            //DisplayMember = "" + nodeSource.Attributes["Text"].Value;

            //DataSet ds = cConn.SelectData(Sql, ConnectionString);
            //_DataSource = ds.Tables[0];
            //SelectSub();
            return false;
        }
        private bool CreateItem(XmlNodeList listItem)
        {
            try
            {
                DataTable dt = _CreateTableSource;
                for (int i = 0; i < listItem.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr.BeginEdit();
                    dr["Text"] = "" + listItem[i].Attributes["Text"].Value;
                    dr["Value"] = "" + listItem[i].Attributes["Value"].Value;
                    dr.EndEdit();
                    dt.Rows.Add(dr);
                }
                ValueMember = "Value";
                DisplayMember = "Text";
                _DataSource = dt;
                return true;
            }
            catch { return false; }
        }

        ucComboBox ucbSubComboBox;
        public void AddSubComBobox(ucComboBox ucb)
        {
            ucbSubComboBox = ucb;
        }
        private void SelectSub()
        {
            if (ucbSubComboBox != null)
            {
                string RepID = "" + cbxTemp.SelectedValue;
                if (RepID == "") return;
                ucbSubComboBox._RepleaceID = RepID;
                ucbSubComboBox.CreateDataSource(ucbSubComboBox._DataSourceID, ConnectionString);
            }
        }
        private void cbxTemp_TextChanged(object sender, System.EventArgs e)
        {
            SelectSub();
            if (textChange != null)
            {
                textChange(sender, e);
            }
        }     
    }
}
