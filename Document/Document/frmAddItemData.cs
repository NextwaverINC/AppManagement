using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Document
{
    public partial class frmAddItemData : Form
    {
        public frmAddItemData()
        {
            InitializeComponent();
        }
        public string getName
        {
            get { return txbItemName.Text; }
        }
        public string getType
        {
            get { return cbxType.SelectedValue.ToString(); }
        }
        XmlNode nodeListItem;
        public XmlNode _ListItem
        {
            get { return nodeListItem; }
        }
        XmlNode nodeInputItem;
        public XmlNode _InputItem
        {
            set { nodeInputItem = value; }
        }
        XmlDocument xmlDoc;
        public XmlDocument _xmlDoc
        {
            set { xmlDoc = value; }
        }
        XmlNode nodeMeans = null;
        public XmlNode _nodeMeans
        {
            set { nodeMeans = value; }
            get { return nodeMeans; }
        }
        XmlNode nodeOut;
        public XmlNode _nodeOut
        {
            get { return nodeOut; }
        }
        XmlNode nodeOutItem;
        public XmlNode _nodeOutItem
        {
            get { return nodeOutItem; }
        }
        public void funStart()
        {
            cControl control = new cControl();
            cbxType.DataSource = control.cbxItemType();
            cbxType.DisplayMember = "Name";
            cbxType.ValueMember = "Value";
        }
        DataTable dt;
        private void funLoad()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(Config.DConfig());

            XmlNodeList listItem = xmlDoc.SelectNodes("//Config[@ID='3']/Item");

            dt = new DataTable();
            for (int i = 0; i < listItem.Count; i++)
            {
                dt.Columns.Add(listItem[i].Attributes["Name"].Value);
            }
            if (nodeInputItem != null)
            {
                txbItemName.Text = "" + nodeInputItem.Attributes["Name"].Value;
                cbxType.SelectedValue = "" + nodeInputItem.Attributes["Type"].Value;
                XmlNodeList listItems;
                if (nodeInputItem.Attributes["Type"].Value.ToString() != "SEQ")
                {
                    listItems = nodeInputItem.SelectNodes("./Item");                   
                }
                else
                {
                    listItems = nodeInputItem.SelectNodes("./Means/Mean");                   
                }
                for (int i = 0; i < listItems.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr.BeginEdit();
                    dr["Name"] = listItems[i].Attributes["Name"].Value;
                    dr["Type"] = listItems[i].Attributes["Type"].Value;
                    dr.EndEdit();
                    dt.Rows.Add(dr);
                }
            }           

            dataGridView1.DataSource = dt;
        }
        private void frmAddItemData_Load(object sender, EventArgs e)
        {
            funLoad();
        }
        private void funAddItem()
        {
            frmEditItem frm = new frmEditItem();
            frm.funLoad();
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                DataRow dr = dt.NewRow();
                dr.BeginEdit();
                dr["Name"] = frm.ItemName;
                dr["Type"] = frm.ItemType;
                dr.EndEdit();
                dt.Rows.Add(dr);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
            }
            if (!frm.IsDisposed)
            {
                frm.Close();
                frm.Dispose();
            }
        }
        private void tsbAddItem_Click(object sender, EventArgs e)
        {
            funAddItem();            
        }
        private void tsbDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณแน่ใจหรือไม่? ที่จะลบข้อมูลนี้", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            frmEditItem frm = new frmEditItem();
            frm.funLoad();
            frm.ItemName = dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();
            frm.ItemType = dataGridView1.SelectedRows[0].Cells["Type"].Value.ToString();
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                dataGridView1.SelectedRows[0].Cells["Name"].Value = frm.ItemName;
                dataGridView1.SelectedRows[0].Cells["Type"].Value = frm.ItemType;
            }
            if (!frm.IsDisposed)
            {
                frm.Close();
                frm.Dispose();
            }
        }
        private bool funValidate()
        {
            if (txbItemName.Text == null || txbItemName.Text == "")
            {
                MessageBox.Show("โปรดระบุ Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txbItemName.Focus();
                return false;
            }
            if (cbxType.Text == "" || cbxType.Text == null)
            {
                MessageBox.Show("โปรดระบุ Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbxType.Focus();
                return false;
            }
            if (dataGridView1.Rows.Count == 0)
            {
                return false;
            }
            return true;
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public void Save()
        {
            XmlAttribute att;
            XmlNode nodeMeans = xmlDoc.CreateElement("Means");
            att = xmlDoc.CreateAttribute("Name");
            att.Value = txbItemName.Text;
            nodeMeans.Attributes.Append(att);

            if (cbxType.SelectedValue.ToString() != "FIX")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    XmlNode nodeMean = xmlDoc.CreateElement("Mean");

                    att = xmlDoc.CreateAttribute("ID");
                    if (i < 10) att.Value = "C0" + i;
                    else att.Value = "C" + i;
                    nodeMean.Attributes.Append(att);

                    att = xmlDoc.CreateAttribute("Name");
                    att.Value = "" + dataGridView1.Rows[i].Cells["Name"].Value;
                    nodeMean.Attributes.Append(att);

                    att = xmlDoc.CreateAttribute("Type");
                    att.Value = "" + dataGridView1.Rows[i].Cells["Type"].Value;
                    nodeMean.Attributes.Append(att);

                    nodeMeans.AppendChild(nodeMean);
                }

                XmlNode Item = xmlDoc.CreateElement("Item");
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (i < 10) att = xmlDoc.CreateAttribute("C0" + i);
                    else att = xmlDoc.CreateAttribute("C" + i);
                    Item.Attributes.Append(att);
                }

                nodeOutItem = Item;
                nodeOut = nodeMeans;
            }
            else
            {                
                XmlNode listItem = xmlDoc.CreateElement("ListItem");
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    XmlNode Item = xmlDoc.CreateElement("Item");
                    att = xmlDoc.CreateAttribute("Name");
                    att.Value = "" + dataGridView1.Rows[i].Cells["Name"].Value;
                    Item.Attributes.Append(att);

                    att = xmlDoc.CreateAttribute("Type");
                    att.Value = "" + dataGridView1.Rows[i].Cells["Type"].Value;
                    Item.Attributes.Append(att);

                    att = xmlDoc.CreateAttribute("Value");
                    Item.Attributes.Append(att);

                    listItem.AppendChild(Item);
                }
                nodeListItem = listItem;
            }
        }
        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (funValidate())
            {
                Save();
                this.DialogResult = DialogResult.Yes;
            }
        }     
    }
}