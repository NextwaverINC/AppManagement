using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;

namespace ToolsDocument
{
    public partial class ucRadioButton : UserControl
    {
        ArrayList AL = new ArrayList();
        string sValue;
        public string _Value
        {
            set
            {
                string vData = value;
                for (int i = 0; i < AL.Count; i++)
                {
                    RadioButton rab = (RadioButton)AL[i];
                    if ("" + rab.Tag == vData)
                    {
                        rab.Checked = true;
                        return;
                    }
                }               
            }
            get { return sValue; }
        }

        public string _Text
        {
            set { groupBox1.Text = value; }
            get { return groupBox1.Text; }
        }
        string XPath;
        public string _XPath
        {
            set { XPath = value; }
            get { return XPath; }
        }

        public void CreateDataSource(string DataSourceID)
        {
            XmlDocument xTemp = new XmlDocument();
            xTemp.Load(Application.StartupPath + @"\Config\RadioButtonSource.xml");

            XmlNode nodeSource = xTemp.SelectSingleNode("//DataSource[@ID='" + DataSourceID + "']");
            string Style = "" + nodeSource.Attributes["Style"].Value;
            XmlNodeList listItem = nodeSource.SelectNodes("./Item");

            int CalWidth;

            TableLayoutPanel tlpMain = new TableLayoutPanel();
            //tlpMain.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tlpMain.Name = "table";
            tlpMain.Margin = new Padding(10);
            tlpMain.Dock = DockStyle.Fill;

            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            if (Style == "H") tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
           

            int Index = 0;
            for (int i = 0; i < listItem.Count; i++)
            {
                Index = i;
                string sText = "" + listItem[i].Attributes["Text"].Value;
                string iValue = "" + listItem[i].Attributes["Value"].Value;
                string Checked = "" + listItem[i].Attributes["Checked"].Value;
                CalWidth = (int)((80 / 12) * sText.Length) + 3;
                if (CalWidth < 80) CalWidth = 80;
                
                RadioButton rdbTemp = new RadioButton();
                rdbTemp.Dock = DockStyle.Fill;
                rdbTemp.TextAlign = ContentAlignment.MiddleLeft;
                rdbTemp.Text = sText;
                rdbTemp.Margin = new Padding(3);
                rdbTemp.Tag = iValue;               
                rdbTemp.CheckedChanged += new EventHandler(rdbTemp_CheckedChanged);
                AL.Add(rdbTemp);

                if (Checked == "T") rdbTemp.Checked = true;

                if (Style == "H")
                {
                    tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, CalWidth));
                    tlpMain.Controls.Add(rdbTemp, i+1, 0);
                }
                else
                {
                    tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
                    tlpMain.Controls.Add(rdbTemp, 1, i);
                }
            }

            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Label labt = new Label();
            labt.Text = "";
            tlpMain.Controls.Add(labt, 1, Index + 1);
            groupBox1.Controls.Add(tlpMain);
        }
        void rdbTemp_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdbTemp = (RadioButton)sender;
            if (rdbTemp.Checked)
            {
                sValue = "" + rdbTemp.Tag;
            }

        }
        public ucRadioButton()
        {
            InitializeComponent();
        }
    }
}
