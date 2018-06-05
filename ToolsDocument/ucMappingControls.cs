using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;

namespace ToolsDocument
{  
    public partial class ucMappingControls : UserControl
    {
        DocumentControls DocControl = new DocumentControls();     

        public void AddControl(Control ctrl)
        {
            this.Visible = false;
            string XPath, sValue;
            XmlNode nodeControl;
            switch (ctrl.GetType().ToString().Replace("ToolsDocument.", ""))
            {
                case "ucCheckBox":
                    ucCheckBox ucChb = (ucCheckBox)ctrl;
                    XPath = ucChb._XPath;
                    nodeControl = xmlDoc.SelectSingleNode(XPath);
                    sValue = GetData(nodeControl);
                    ucChb._Checked = strToBool(sValue);
                    break;
                case "ucComboBox":
                    ucComboBox ucCbx = (ucComboBox)ctrl;
                    XPath = ucCbx._XPath;
                    nodeControl = xmlDoc.SelectSingleNode(XPath);
                    sValue = GetData(nodeControl);
                    ucCbx._SelectedValue = sValue;
                    break;
                case "ucDateTimePicker":
                    ucDateTimePicker ucDtp = (ucDateTimePicker)ctrl;
                    XPath = ucDtp._XPath;
                    nodeControl = xmlDoc.SelectSingleNode(XPath);
                    sValue = GetData(nodeControl);
                    strToDate(sValue, ucDtp);
                    break;
                case "ucRichTextBox":
                    ucRichTextBox ucRtb = (ucRichTextBox)ctrl;
                    XPath = ucRtb._XPath;
                    nodeControl = xmlDoc.SelectSingleNode(XPath);
                    sValue = GetData(nodeControl);
                    ucRtb._Value = sValue;
                    break;
                case "ucTextBox":
                    ucTextBox ucTbx = (ucTextBox)ctrl;
                    XPath = ucTbx._XPath;
                    nodeControl = xmlDoc.SelectSingleNode(XPath);
                    sValue = GetData(nodeControl);
                    ucTbx._Value = sValue;
                    break;
                case "ucPopup":
                    ucPopup ucPP = (ucPopup)ctrl;
                    XPath = ucPP._XPath;
                    nodeControl = xmlDoc.SelectSingleNode(XPath);
                    sValue = GetData(nodeControl);
                    ucPP.Tag = sValue;
                    ucPP._Text = nodeControl.InnerText;
                    ucPopup ucSub = ucPP.GetSubPopup();
                    if (ucSub != null) ucSub._RepleaceID = sValue;
                    break;
                case "ucDataGridView":
                    ucDataGridView ucDGB = (ucDataGridView)ctrl;
                    XPath = ucDGB._XPath;
                    ucDGB.CreateDataSource(xmlDoc.SelectSingleNode(XPath));
                    break;
                case "ucRadioButton":
                    ucRadioButton ucRB = (ucRadioButton)ctrl;
                    XPath = ucRB._XPath;
                    nodeControl = xmlDoc.SelectSingleNode(XPath);
                    sValue = GetData(nodeControl);
                    ucRB._Value = sValue;
                    break;
                case "ucPictureImage":
                    ucPictureImage ucPI = (ucPictureImage)ctrl;
                    XPath = ucPI._XPath;
                    nodeControl = xmlDoc.SelectSingleNode(XPath);
                    sValue = nodeControl.InnerText;
                    ucPI._imgBinary = sValue;
                    break;
                case "ucNumericUpDown":
                    ucNumericUpDown ucNUD = (ucNumericUpDown)ctrl;
                    XPath = ucNUD._XPath;
                    nodeControl = xmlDoc.SelectSingleNode(XPath);
                    sValue = GetData(nodeControl);
                    ucNUD._Value = strToDecimal(sValue);
                    break;
                case "ucDoubleTextBox":
                    ucDoubleTextBox ucDTB = (ucDoubleTextBox)ctrl;
                    XPath = ucDTB._XPath;
                    nodeControl = xmlDoc.SelectSingleNode(XPath);
                    sValue = GetData(nodeControl);
                    ucDTB._Value = sValue;
                    break;
            }
            DocControl.AddControls(ctrl);
        }

        XmlDocument xmlDoc = new XmlDocument();
        public XmlDocument _XmlDocument
        {
            set { xmlDoc = value; }
        }
       
        public bool funSaveXml()
        {
            for (int i = 0; i < DocControl.CountControl; i++)
            {
                Control ctrl = DocControl.GetControls(i);
                string XPath, sValue;
                XmlNode nodeControl;
                switch (ctrl.GetType().ToString().Replace("ToolsDocument.", ""))
                {
                    case "ucCheckBox":
                        ucCheckBox ucChb = (ucCheckBox)ctrl;
                        XPath = ucChb._XPath;
                        nodeControl = xmlDoc.SelectSingleNode(XPath);
                        sValue = "" + ucChb._Checked;
                        AddData(nodeControl, sValue);
                        break;
                    case "ucComboBox":
                        ucComboBox ucCbx = (ucComboBox)ctrl;
                        XPath = ucCbx._XPath;
                        nodeControl = xmlDoc.SelectSingleNode(XPath);
                        sValue = "" + ucCbx._SelectedValue;
                        AddData(nodeControl, sValue);
                        break;
                    case "ucDateTimePicker":
                        ucDateTimePicker ucDtp = (ucDateTimePicker)ctrl;
                        XPath = ucDtp._XPath;
                        nodeControl = xmlDoc.SelectSingleNode(XPath);
                        sValue = ucDtp._Day + "/" + ucDtp._Month + "/" + ucDtp._Year;
                        AddData(nodeControl, sValue);
                        break;
                    case "ucRichTextBox":
                        ucRichTextBox ucRtb = (ucRichTextBox)ctrl;
                        XPath = ucRtb._XPath;
                        nodeControl = xmlDoc.SelectSingleNode(XPath);
                        sValue = ucRtb._Value;
                        AddData(nodeControl, sValue);
                        break;
                    case "ucTextBox":
                        ucTextBox ucTbx = (ucTextBox)ctrl;
                        XPath = ucTbx._XPath;
                        nodeControl = xmlDoc.SelectSingleNode(XPath);
                        sValue = ucTbx._Value;
                        AddData(nodeControl, sValue);
                        break;
                    case "ucPopup":
                        ucPopup ucPP = (ucPopup)ctrl;
                        XPath = ucPP._XPath;
                        nodeControl = xmlDoc.SelectSingleNode(XPath);
                        sValue = ucPP._Value;
                        AddData(nodeControl, sValue);
                        nodeControl.InnerText = ucPP._Text;
                        break;
                    case "ucDataGridView":
                        ucDataGridView ucDGB = (ucDataGridView)ctrl;
                        XPath = ucDGB._XPath;
                        GetDataFromDataGridView(xmlDoc.SelectSingleNode(XPath), ucDGB);
                        break;
                    case "ucRadioButton":
                        ucRadioButton ucRB = (ucRadioButton)ctrl;
                        XPath = ucRB._XPath; 
                        nodeControl = xmlDoc.SelectSingleNode(XPath);
                        sValue = ucRB._Value;
                        AddData(nodeControl, sValue);
                        break;
                    case "ucPictureImage":
                        ucPictureImage ucPI = (ucPictureImage)ctrl;
                        XPath = ucPI._XPath;
                        nodeControl = xmlDoc.SelectSingleNode(XPath);
                        sValue = ucPI._imgBinary;
                        int cCount = nodeControl.ChildNodes.Count;
                        for (int j = 0; j < cCount; j++)
                        {
                            nodeControl.RemoveChild(nodeControl.ChildNodes[0]);
                        }
                        nodeControl.AppendChild(CreateCDataSection(sValue));
                        break;
                    case "ucNumericUpDown":
                        ucNumericUpDown ucNUD = (ucNumericUpDown)ctrl;
                        XPath = ucNUD._XPath;
                        nodeControl = xmlDoc.SelectSingleNode(XPath);
                        sValue = "" + ucNUD._Value;
                        AddData(nodeControl, sValue);
                        break;
                    case "ucDoubleTextBox":
                        ucDoubleTextBox ucDTB = (ucDoubleTextBox)ctrl;
                        XPath = ucDTB._XPath;
                        nodeControl = xmlDoc.SelectSingleNode(XPath);
                        sValue = ucDTB._Value;
                        AddData(nodeControl, sValue.Replace(",", ""));
                        break;
                }
            }
            return true;
        }

        /// <summary>
        /// Command=>Insert,Update,Delete
        /// </summary>
        /// <param name="xmlConfig"></param>
        /// <param name="Command"></param>
        /// <returns></returns>
        public bool funSaveToDB(XmlDocument xmlConfig, string Command, string ConnectoinString)
        {
            //if (funSaveXml())
            //{
            //    TransformXml TFX = new TransformXml(xmlDoc, xmlConfig);
            //    ArrayList AL = new ArrayList();
            //    switch (Command.ToUpper())
            //    {
            //        case "INSERT": AL = TFX.getSql("1"); break;
            //        case "UPDATE": AL = TFX.getSql("2"); break;
            //        case "DELETE": AL = TFX.getSql("3"); break;
            //    }
            //    if (AL.Count == 0) return false;

            //    object[] objs = new object[AL.Count];
            //    for (int z = 0; z < AL.Count; z++)
            //        objs[z] = AL[z];
                
            //    ConnectServer.cConection cConn = new ConnectServer.cConection("");
            //    string Output = cConn.ExecuteSqlTransaction(objs, ConnectoinString);
            //    if (Output == "") return true;
            //    else return false;
            //}
            //else return false;
            return false;
        }

        #region Method
        private decimal strToDecimal(string str)
        {
            try { return decimal.Parse(str); }
            catch { return 0; }
        }
        private void GetDataFromDataGridView(XmlNode nodeDataGrid, ucDataGridView ucDGB)
        {
            XmlNodeList listItem = nodeDataGrid.SelectNodes("./Item");
            for (int i = 0; i < listItem.Count; i++)
            {
                nodeDataGrid.RemoveChild(listItem[i]);
            }
            XmlAttribute att;
            XmlNode nodeItem;
            for (int i = 0; i < ucDGB._dgvData.Rows.Count; i++)
            {
                nodeItem = xmlDoc.CreateElement("Item");
                for (int j = 0; j < ucDGB._dgvData.Columns.Count; j++)
                {
                    att = xmlDoc.CreateAttribute(ucDGB._dgvData.Columns[j].Name);
                    att.Value = "" + ucDGB._dgvData.Rows[i].Cells[j].Value;
                    nodeItem.Attributes.Append(att);
                }
                nodeDataGrid.AppendChild(nodeItem);
            }
        }
        private void AddData(XmlNode nodeControl, string sValue)
        {
            nodeControl.Attributes["Value"].Value = sValue;
        }
        private string GetData(XmlNode nodeControl)
        {
            return "" + nodeControl.Attributes["Value"].Value;
        }
        private bool strToBool(string str)
        {
            try
            {
                return bool.Parse(str);
            }
            catch { return false; }
        }
        private void strToDate(string str, ucDateTimePicker ucDtp)
        {
            try
            {
                string[] lTemp = str.Split(new char[] { '/' });
                ucDtp.setValue(int.Parse(lTemp[0]), int.Parse(lTemp[1]), int.Parse(lTemp[2]));
            }
            catch { }
        }
        #endregion

        private XmlCDataSection CreateCDataSection(string sValue)
        {
            XmlCDataSection XCDS = xmlDoc.CreateCDataSection(sValue);
            return XCDS;
        }
        public ucMappingControls()
        {
            InitializeComponent();
        }      
    }
}
