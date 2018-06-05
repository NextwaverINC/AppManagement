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
    public partial class ucDataGridView : UserControl
    {
        string DataText;
        public string _DataText
        {
            set { DataText = value; }
            get { return DataText; }
        }
        public ucDataGridView()
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
        public DataGridView _dgvData
        {
            get { return dgvData; }
            set { dgvData = value; }
        }

        public bool CreateDataSource(XmlNode nodeDataGrid)
        {
            try
            {              
                XmlNodeList nodeMeans = nodeDataGrid.SelectNodes("./Means/Mean");
                string ID, Name, Type;

                DataTable dt = new DataTable();
                Dictionary<string, int> dic = new Dictionary<string, int>();
                int WidthValue, WidthMax = 200, WidthMin = 80;
                
                // 80 = 12 ตัวอักษร
                for (int i = 0; i < nodeMeans.Count; i++)
                {
                    ID = "" + nodeMeans[i].Attributes["ID"].Value;
                    Name = "" + nodeMeans[i].Attributes["Name"].Value;
                    Type = "" + nodeMeans[i].Attributes["Type"].Value;
                    int CalWidth = (int)((WidthMin / 12) * Name.Length) + 3;
                    if (Name.Length <= 13) dic.Add(ID, WidthMin);
                    else dic.Add(ID, CalWidth);

                    switch (Type.ToUpper())
                    {
                        case "INT": dt.Columns.Add(ID, typeof(int)); break;
                        case "DOUBLE": dt.Columns.Add(ID, typeof(Double)); break;
                        case "DATE": dt.Columns.Add(ID, typeof(DateTime)); break;
                        default: dt.Columns.Add(ID); break;
                    }
                   
                }

                XmlNodeList listItem = nodeDataGrid.SelectNodes("./Item");
                string sValue = "";
                for (int i = 0; i < listItem.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr.BeginEdit();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        sValue = "" + listItem[i].Attributes[dt.Columns[j].ColumnName].Value;
                        try { dr[j] = listItem[i].Attributes[dt.Columns[j].ColumnName].Value; }
                        catch { }

                        WidthValue = dic[dt.Columns[j].ColumnName];                        
                        int CalWidth = (int)((WidthMin / 12) * sValue.Length) + 3;
                        if (WidthValue < CalWidth && CalWidth < WidthMax) dic[dt.Columns[j].ColumnName] = CalWidth;
                    }
                    dr.EndEdit();
                    dt.Rows.Add(dr);
                }

                dgvData.DataSource = dt;
                for (int i = 0; i < nodeMeans.Count; i++)
                {
                    ID = "" + nodeMeans[i].Attributes["ID"].Value;
                    Name = "" + nodeMeans[i].Attributes["Name"].Value;
                    dgvData.Columns[ID].HeaderText = Name;
                    dgvData.Columns[ID].Width = dic[ID];
                }
                SetTextRowCount();
                return true;
            }
            catch { return false; }
        }
        private void SetTextRowCount()
        {
            tsslRowCount.Text = "จำนวนรายการทั้งหมด " + dgvData.Rows.Count + " รายการ";
        }
        private void messageComplete(string msg)
        {
            MessageBox.Show(msg, "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            frmEntry frm = new frmEntry();
            frm._Command = "New";
            frm._dgvTemp = dgvData;
            frm.Text = "หน้าต่างจัดการข้อมูล " + DataText;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                SetTextRowCount();
                messageComplete("เพิ่มข้อมูลเรียบร้อยแล้ว");
            }
            if (!frm.IsDisposed)
            {
                frm.Close();
                frm.Dispose();
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count != 0)
            {
                frmEntry frm = new frmEntry();
                frm._Command = "Edit";
                frm._dgvTemp = dgvData;
                frm.Text = "หน้าต่างจัดการข้อมูล " + DataText;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    SetTextRowCount();
                    messageComplete("แก้ไขข้อมูลเรียบร้อยแล้ว");
                }
                if (!frm.IsDisposed)
                {
                    frm.Close();
                    frm.Dispose();
                }
            }
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count != 0)
            {
                if (MessageBox.Show("คุณแน่ใจหรือไม่ที่จะลบข้อมูลนี้?", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataTable dt = (DataTable)dgvData.DataSource;
                    DataRow dr = dt.Rows[dgvData.SelectedRows[0].Index];
                    dt.Rows.Remove(dr);

                    SetTextRowCount();
                    messageComplete("ลบข้อมูลเรียบร้อยแล้ว");
                }
            }
        }

    }
}
