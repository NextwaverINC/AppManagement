using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using System.IO;


namespace Document
{
    public partial class frmDocument : Form
    {        
        public frmDocument()
        {
            InitializeComponent();
        }

        ConnectServer.cConection cConn = new ConnectServer.cConection(cMain.UserName);

        #region Parameter
        string LogDocumentID;
        string LogDocumentName;
        string strSectionID;
        string strSectionName;
        string strSectoinType;
        string DocID;
        string DocName;
        int AutoID = 0;
        string docXML;
        XmlDocument xmlDoc;
        string Command;
        #endregion
        #region Properties
        public string _DocumentID
        {
            set { LogDocumentID = value; tbxDocID.Text = value; }
        }
        public string _DocumentName
        {
            set { LogDocumentName = value; tbxDocName.Text = value; tbxDocName.ReadOnly = true; }
        }
        public string _DocXML
        {
            set { docXML = value; }
            get { return docXML; }
        }
        public string _Command
        {
            set { Command = value; }
        }
        #endregion

        #region Method
        private bool CheckDocumentName()
        {
            NextwaverDB.NWheres NWS = new NextwaverDB.NWheres();
            NWS.whereType = true;
            NWS.Where = "[@NAME@ = '" + tbxDocName.Text + "']";
            if (Command != "New")
            {
                NWS.Where += "[@ID@ != '" + LogDocumentID + "']";
            }

            NextwaverDB.NColumns NCS_S = new NextwaverDB.NColumns();
            NCS_S.Add(new NextwaverDB.NColumn("NAME"));

            DataTable dt = cConn.Retreive(cMain.Connection, cMain.OfficeSpaceId, cMain.DatabaseName, "document", NCS_S, NWS);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("ชื่อผู้ใช้งานนี้มีอยู่แล้วในระบบโปรดระบุใหม่", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private bool funValidation()
        {
            if (VAD.isNull(tbxDocName))
            {
                MSG.Error("โปรดระบุ Name", "ข้อผิดพลาด");
                tbxDocID.Focus();
                return false;
            }
            if (!CheckDocumentName())
            {
                MSG.Error("Name นี้ไม่สามารถใช้ได้โปรดระบุใหม่", "ข้อผิดพลาด");
                tbxDocName.Focus();
                return false;
            }            
            return true;
        }
        private void funClose()
        {
            this.Close();
            this.Dispose();
        }
        private void funSave()
        {
            if (funValidation())
            {
                DocID = tbxDocID.Text;
                DocName = tbxDocName.Text;
                xmlDoc.SelectSingleNode("//Document").Attributes["ID"].Value = tbxDocID.Text;
                xmlDoc.SelectSingleNode("//Document").Attributes["Name"].Value = tbxDocName.Text;

                NextwaverDB.NColumns NCS = new NextwaverDB.NColumns();
                NCS.Add(new NextwaverDB.NColumn("NAME", tbxDocName.Text));
                NCS.Add(new NextwaverDB.NColumn("CREATE_DATE", DateTime.Now.ToString("dd/MM/yyyy")));
                NCS.Add(new NextwaverDB.NColumn("CREATE_BY", cMain.UserName));
                NCS.Add(new NextwaverDB.NColumn("UPDATE_DATE", DateTime.Now.ToString("dd/MM/yyyy")));
                NCS.Add(new NextwaverDB.NColumn("UPDATE_BY", cMain.UserName));

                if (Command == "New")
                {
                    if (cConn.InsertData(cMain.Connection, cMain.OfficeSpaceId, cMain.DatabaseName, "document", NCS, xmlDoc.OuterXml))
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Hide();
                    }
                    else
                    {
                        MSG.Error("ไม่สามารถเพิ่มข้อมูลได้เนื่องจาก:" + cConn.ErrorMsg(), "ข้อผิดพลาด");
                    }
                }
                else
                {
                    NextwaverDB.NWheres NWS =new NextwaverDB.NWheres();
                    NWS.Add(new NextwaverDB.NWhere("ID",LogDocumentID));
                    if(cConn.UpdateData(cMain.Connection,cMain.OfficeSpaceId,cMain.DatabaseName,"document",NCS,NWS,xmlDoc.OuterXml))
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Hide();
                    }
                    else
                    {
                        MSG.Error("ไม่สามารถแก้ไขข้อมูลได้เนื่องจาก:" + cConn.ErrorMsg(), "ข้อผิดพลาด");
                    }
                }
            }
        }
        private void funNew()
        {
            if (MSG.Confirm("ข้อมูลเก่าที่คุณสร้างจะถูกแทนที่ คุณแน่ใจหรือไม่?", "คำเตือน") == DialogResult.Yes)
            {
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(Config.DefaultDocument());
                funDrawMainTree();
                funDrawData();
            }           
        }
        private void funLoad()
        {
            cControl control = new cControl();
            cbxSectionType.DataSource = control.cbxDocumentType();
            cbxSectionType.DisplayMember = "Name";
            cbxSectionType.ValueMember = "Value";

            if (Command == "Edit")
            {
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(docXML);
            }
            else
            {
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(Config.DefaultDocument());
            }
            funDrawMainTree();
            funDrawData();
            funDrawProfile();
        }
        private void funDrawProfile()
        {
            XmlNodeList listProfile = xmlDoc.SelectNodes("//Document/Profile/Section");
            for (int i = 0; i < listProfile.Count; i++)
            {
                string strName = "" + listProfile[i].Attributes["Name"].Value;
                TreeView trvTemp = new TreeView();
                switch (strName)
                {
                    case "Who": trvTemp = trvWho; break;
                    case "What": trvTemp = trvWhat; break;
                    case "Where": trvTemp = trvWhere; break;
                    case "When": trvTemp = trvWhen; break;
                    case "Why": trvTemp = trvWhy; break;
                }
                XmlNodeList listItem = listProfile[i].SelectNodes("./Item");
                for (int j = 0; j < listItem.Count; j++)
                {
                    string iName = "" + listItem[j].Attributes["ID"].Value;
                    TreeNode node = new TreeNode();
                    node.Text = iName;
                    trvTemp.Nodes.Add(node);
                }               
            }
        }
        private void funDrawMainTree()
        {
            tvMain.Nodes.Clear();
            setValue();
            tvMain.ExpandAll();
        }
        private void funDrawMainTree(XmlDocument Doc)
        {
            tvMain.Nodes.Clear();
            setValue(Doc);
            tvMain.ExpandAll();
        }
        private void setValue(XmlDocument Doc)
        {
            //Create Profile
            XmlNode nodeProfile = Doc.SelectSingleNode("//Profile");
            XmlNode nodeWhat = nodeProfile.SelectSingleNode("./Section[@Name='What']");
            XmlNode nodeWhen = nodeProfile.SelectSingleNode("./Section[@Name='When']");
            XmlNode nodeWhere = nodeProfile.SelectSingleNode("./Section[@Name='Where']");
            XmlNode nodeWho = nodeProfile.SelectSingleNode("./Section[@Name='Who']");
            XmlNode nodeWhy = nodeProfile.SelectSingleNode("./Section[@Name='Why']");

           


            TreeNode treeProfile = new TreeNode();
            treeProfile.Name = "Profile";
            treeProfile.Text = "Profile";

            treeProfile.Nodes.Add(createNode("Who", "Section Who", nodeWho.SelectNodes("./Item")));
            treeProfile.Nodes.Add(createNode("What", "Section What", nodeWhat.SelectNodes("./Item")));
            treeProfile.Nodes.Add(createNode("Where", "Section Where", nodeWhere.SelectNodes("./Item")));
            treeProfile.Nodes.Add(createNode("When", "Section When", nodeWhen.SelectNodes("./Item")));
            treeProfile.Nodes.Add(createNode("Why", "Section Why", nodeWhy.SelectNodes("./Item")));

            tvMain.Nodes.Add(treeProfile);

            //Create Data
            TreeNode treeData = new TreeNode();
            treeData.Name = "Data";
            treeData.Text = "Data";
            XmlNode nodeData = Doc.SelectSingleNode("//Data");
            XmlNodeList listSection = nodeData.SelectNodes("./Section");

            string SectionID, SectionName;
            for (int i = 0; i < listSection.Count; i++)
            {
                SectionID = "" + listSection[i].Attributes["ID"].Value;
                SectionName = "" + listSection[i].Attributes["Name"].Value;

                TreeNode treeSection = new TreeNode();
                treeSection.Name = "Section";
                treeSection.Text = "Section[ID=" + SectionID + "][Name=" + SectionName + "]";

                XmlNodeList listItem = listSection[i].SelectNodes("./Items");
                treeSection = createTreeItems(treeSection, listItem);
                treeData.Nodes.Add(treeSection);
            }

            tvMain.Nodes.Add(treeData);
        }
        private void setValue()
        {
            //Create Profile
            XmlNode nodeProfile = xmlDoc.SelectSingleNode("//Profile");
            XmlNode nodeWhat = nodeProfile.SelectSingleNode("./Section[@Name='What']");
            XmlNode nodeWhen = nodeProfile.SelectSingleNode("./Section[@Name='When']");
            XmlNode nodeWhere = nodeProfile.SelectSingleNode("./Section[@Name='Where']");
            XmlNode nodeWho = nodeProfile.SelectSingleNode("./Section[@Name='Who']");
            XmlNode nodeWhy = nodeProfile.SelectSingleNode("./Section[@Name='Why']");

           

            TreeNode treeProfile = new TreeNode();
            treeProfile.Name = "Profile";
            treeProfile.Text = "Profile";

            treeProfile.Nodes.Add(createNode("Who", "Section Who", nodeWho.SelectNodes("./Item")));
            treeProfile.Nodes.Add(createNode("What", "Section What", nodeWhat.SelectNodes("./Item")));
            treeProfile.Nodes.Add(createNode("Where", "Section Where", nodeWhere.SelectNodes("./Item")));
            treeProfile.Nodes.Add(createNode("When", "Section When", nodeWhen.SelectNodes("./Item")));
            treeProfile.Nodes.Add(createNode("Why", "Section Why", nodeWhy.SelectNodes("./Item")));

            tvMain.Nodes.Add(treeProfile);

            //Create Data
            TreeNode treeData = new TreeNode();
            treeData.Name = "Data";
            treeData.Text = "Data";
            XmlNode nodeData = xmlDoc.SelectSingleNode("//Data");
            XmlNodeList listSection = nodeData.SelectNodes("./Section");

            string SectionID, SectionName, SectionType;
            for (int i = 0; i < listSection.Count; i++)
            {
                SectionID = "" + listSection[i].Attributes["ID"].Value;
                SectionName = "" + listSection[i].Attributes["Name"].Value;
                SectionType = "" + listSection[i].Attributes["Type"].Value;

                TreeNode treeSection = new TreeNode();
                treeSection.Name = "Section";
                treeSection.Text = "Section[ID=" + SectionID + "][Name=" + SectionName + "][Type=" + SectionType + "]";

                XmlNodeList listItem = listSection[i].SelectNodes("./Items");
                treeSection = createTreeItems(treeSection, listItem);
                treeData.Nodes.Add(treeSection);


            }
            tvMain.Nodes.Add(treeData);
        }
        private void funDrawData()
        {
            XmlNodeList listSection = xmlDoc.SelectNodes("//Data/Section");
            for (int i = 0; i < listSection.Count; i++)
            {
                string SectionID = "" + listSection[i].Attributes["ID"].Value;
                string SectionName = "" + listSection[i].Attributes["Name"].Value;
                string SectionType = "" + listSection[i].Attributes["Type"].Value;
                TreeNode tSection = new TreeNode();
                tSection.Name = SectionID + ":" + SectionName + ":" + SectionType;
                tSection.Text = "SectionID=" + SectionID + ":Name=" + SectionName;
                tvData.Nodes.Add(tSection);
            }
        }

        private TreeNode createNode(string Name, string Text, XmlNodeList listItem)
        {
            TreeNode treeTemp = new TreeNode();
            treeTemp.Name = Name;
            treeTemp.Text = Text;

            for (int i = 0; i < listItem.Count; i++)
            {
                TreeNode treeItem = new TreeNode();
                treeItem.Name = "" + listItem[i].Attributes["ID"].Value + listItem[i].Attributes["Value"].Value;
                treeItem.Text = "" + listItem[i].Attributes["ID"].Value;
                treeTemp.Nodes.Add(treeItem);
            }

            return treeTemp;
        }
        private void funDeleteSection()
        {
            if (tvData.SelectedNode == null) return;
            if (MSG.Confirm("คุณแน่ใจหรือไม่? ที่จะลบข้อมูลนี้", "คำเตือน") == DialogResult.Yes)
            {
                TreeNode tTemp = tvData.SelectedNode;
                xmlDoc.SelectSingleNode("//Data").RemoveChild(xmlDoc.SelectSingleNode("//Data/Section[@ID='" + tTemp.Name.Split(new char[] { ':' }).GetValue(0).ToString() + "']"));
                tvData.Nodes.Remove(tTemp);
                funDrawMainTree();
            }
        }
        private void funNewSection(string SectionID, string SectionName, string SectionType)
        {
            TreeNode treeTemp = new TreeNode();
            treeTemp.Name = SectionID + ":" + SectionName + ":" + SectionType;
            treeTemp.Text = "SectionID=" + SectionID + ":Name=" + SectionName;
            tvData.Nodes.Add(treeTemp);

            XmlNode nodeSection = xmlDoc.CreateElement("Section");

            XmlAttribute att = xmlDoc.CreateAttribute("ID");
            att.Value = SectionID;
            nodeSection.Attributes.Append(att);

            att = xmlDoc.CreateAttribute("Name");
            att.Value = SectionName;
            nodeSection.Attributes.Append(att);

            att = xmlDoc.CreateAttribute("Type");
            att.Value = SectionType;
            nodeSection.Attributes.Append(att);

            xmlDoc.SelectSingleNode("//Document/Data").AppendChild(nodeSection);
            funDrawMainTree();
        }
        private void funClearProfile()
        {
            funDrawMainTree();
            MSG.Information("ล้างค่า Profile เรียบร้อยแล้ว", "ผลการทำงาน");
        }
        private void funDApply()
        {
            if (funValidation())
            {
                DocID = tbxDocID.Text;
                DocName = tbxDocName.Text;
                xmlDoc.SelectSingleNode("//Document").Attributes["ID"].Value = tbxDocID.Text;
                xmlDoc.SelectSingleNode("//Document").Attributes["Name"].Value = tbxDocName.Text;
            }
        }
        private void funDCancel()
        {
            tbxDocID.Text = DocID;
            tbxDocName.Text = DocName;
        }
        private TreeNode createTreeItems(XmlNodeList listItems, TreeNode treeItems)
        {
            for (int j = 0; j < listItems.Count; j++)
            {
                string Name = "" + listItems[j].Attributes["Name"].Value;
                string sType = "" + listItems[j].Attributes["Type"].Value;
                TreeNode nodeItems = new TreeNode();
                nodeItems.Name = Name + ":" + sType;
                nodeItems.Text = "Item Name:" + Name + " Type:" + sType;
                if (sType != "FIX")
                {
                    nodeItems = createTreeItems(listItems[j].SelectNodes("./Item/Items"), nodeItems);
                }
                nodeItems.Tag = listItems[j];
                treeItems.Nodes.Add(nodeItems);
            }
            return treeItems;
        }
        private TreeNode createTreeItems(TreeNode treeTemp, XmlNodeList listItems)
        {
            for (int k = 0; k < listItems.Count; k++)
            {
                string ItemsName = listItems[k].Attributes["Name"].Value;
                string Type = listItems[k].Attributes["Type"].Value;
                TreeNode treeItems = new TreeNode();
                treeItems.Name = ItemsName;
                treeItems.Text = "Items[Name=" + ItemsName + "][Type=" + Type + "]";

                if (Type == "SEQ")
                {
                    XmlNodeList listItem = listItems[k].SelectNodes("./Item/Items");
                    treeItems = createTreeItems(treeItems, listItems[k].SelectNodes("./Item/Items"));
                }
                treeTemp.Nodes.Add(treeItems);
            }
            return treeTemp;
        }
        private void cancelData()
        {
            tbxSectionID.Text = strSectionID;
            tbxSectionName.Text = strSectionName;
            cbxSectionType.SelectedValue = strSectoinType;
        }
        private void okData()
        {
            TreeNode tTemp = tvData.SelectedNode;
            tTemp.Name = tbxSectionID.Text + ":" + tbxSectionName.Text + ":" + cbxSectionType.SelectedValue;
            tTemp.Text = "SectionID=" + tbxSectionID.Text + ":Name=" + tbxSectionName.Text;
            xmlDoc.SelectSingleNode("//Data/Section[@ID='" + strSectionID + "']").Attributes["Name"].Value = tbxSectionName.Text;
            xmlDoc.SelectSingleNode("//Data/Section[@ID='" + strSectionID + "']").Attributes["Type"].Value = tbxSectionName.Text;
            xmlDoc.SelectSingleNode("//Data/Section[@ID='" + strSectionID + "']").Attributes["ID"].Value = tbxSectionName.Text;
            strSectionID = tbxSectionID.Text;
            strSectionName = tbxSectionName.Text;
            strSectoinType = cbxSectionType.SelectedValue.ToString();
            funDrawMainTree();

        }
        private void LoadXML(string FileName)
        {
            try
            {
                XmlDocument xmlTemp = new XmlDocument();
                xmlTemp.Load(FileName);
                funDrawMainTree(xmlTemp);
                xmlDoc = new XmlDocument();
                xmlDoc.Load(FileName);
            }
            catch
            {
                MSG.Error("โหลด File Xml นี้ไม่ได้เนื่องจาก ไม่ตรงกับ Format ของ XML Documents.", "ผลการทำงาน");                
            }
            funDrawMainTree();
            funDrawData();
        }
        public void SaveFile(string filePath, string stringData)
        {
            byte[] bin = Encoding.UTF8.GetBytes(stringData);
            FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write);
            fs.Write(bin, 0, bin.Length);
            fs.Close();
            fs.Dispose();
        }
        private void funEditProfile(string strName, TreeView trvTemp)
        {
            frmProfile frm = new frmProfile();
            frm._trvProfile = trvTemp;
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                trvTemp = frm._trvProfile;
                DrawTreeProfile(strName, trvTemp);
                funDrawMainTree();
            }
            if (!frm.IsDisposed)
            {
                frm.Close();
                frm.Dispose();
            }
        }
        private void DrawTreeProfile(string strName, TreeView trvTemp)
        {
            XmlNode sectionProfile = xmlDoc.SelectSingleNode("//Profile/Section[@Name='" + strName + "']");

            int Count = sectionProfile.ChildNodes.Count;
            for (int i = 0; i < Count; i++)
            {
                sectionProfile.RemoveChild(sectionProfile.ChildNodes[0]);
            }

            XmlAttribute att;
            for (int i = 0; i < trvTemp.Nodes.Count; i++)
            {
                XmlNode nodeItem = xmlDoc.CreateElement("Item");
                att = xmlDoc.CreateAttribute("ID");
                att.Value = trvTemp.Nodes[i].Text;
                nodeItem.Attributes.Append(att);

                att = xmlDoc.CreateAttribute("Value");
                nodeItem.Attributes.Append(att);
                sectionProfile.AppendChild(nodeItem);
            }
        }
        private void AfterSelectData()
        {
            if (tvData.SelectedNode == null)
            {
                tacData.Enabled = false;
                return;
            }
            tacData.Enabled = true;
            string[] aStr = tvData.SelectedNode.Name.Split(new char[] { ':' });
            tbxSectionID.Text = aStr[0]; strSectionID = aStr[0];
            tbxSectionName.Text = aStr[1]; strSectionName = aStr[1];
            cbxSectionType.SelectedValue = aStr[2]; strSectoinType = aStr[2];
            tvwItem.Nodes.Clear();
            string ID = tvData.SelectedNode.Name.ToString().Split(new char[] { ':' }).GetValue(0).ToString();
            XmlNode Section = xmlDoc.SelectSingleNode("//Document/Data/Section[@ID='" + ID + "']");
            XmlNodeList listItems = Section.SelectNodes("./Items");
            for (int i = 0; i < listItems.Count; i++)
            {
                string Name = "" + listItems[i].Attributes["Name"].Value;
                string sType = "" + listItems[i].Attributes["Type"].Value;
                TreeNode nodeItems = new TreeNode();
                nodeItems.Name = Name + ":" + sType;
                nodeItems.Text = "Item Name:" + Name + " Type:" + sType;

                if (sType != "FIX")
                {
                    nodeItems = createTreeItems(listItems[i].SelectNodes("./Item/Items"), nodeItems);
                }
                nodeItems.Tag = listItems[i];
                tvwItem.Nodes.Add(nodeItems);
            }
            tvwItem.ExpandAll();
        }
        private void AddItem(string sCommand)
        {
            if (tvData.SelectedNode == null) return;
            if (sCommand == "Root")
            {
                XmlNode node = (XmlNode)tvData.SelectedNode.Tag;
                frmAddItemData frm = new frmAddItemData();
                frm._xmlDoc = xmlDoc;
                frm._nodeMeans = node;
                frm.funStart();

                if (frm.ShowDialog() == DialogResult.Yes)
                {
                    TreeNode nodeT = new TreeNode();
                    nodeT.Name = frm.getName + ":" + frm.getType;
                    nodeT.Text = "Item Name:" + frm.getName + " Type:" + frm.getType;

                    XmlNode Section;

                    string ID = tvData.SelectedNode.Name.Split(new char[] { ':' }).GetValue(0).ToString();
                    if (tvwItem.SelectedNode == null) Section = xmlDoc.SelectSingleNode("//Document/Data/Section[@ID='" + ID + "']");
                    else
                    {
                        if (tvwItem.SelectedNode.Parent != null) Section = (XmlNode)tvwItem.SelectedNode.Parent.Tag;
                        else Section = xmlDoc.SelectSingleNode("//Document/Data/Section[@ID='" + ID + "']");
                    }

                    XmlNode Items = xmlDoc.CreateElement("Items");
                    XmlAttribute att = xmlDoc.CreateAttribute("Name");
                    att.Value = frm.getName;
                    Items.Attributes.Append(att);
                    att = xmlDoc.CreateAttribute("Type");
                    att.Value = frm.getType;
                    Items.Attributes.Append(att);

                    if (frm._nodeOut != null) Items.AppendChild(frm._nodeOut);
                    if (frm._ListItem == null) Items.AppendChild(frm._nodeOutItem);
                    else
                    {
                        XmlNodeList Item = frm._ListItem.SelectNodes("./Item");
                        for (int i = 0; i < Item.Count; i++)
                        {
                            Items.AppendChild(Item[i]);
                        }
                    }
                    Section.AppendChild(Items);
                    nodeT.Tag = Items;
                    if (tvwItem.SelectedNode == null) tvwItem.Nodes.Add(nodeT);
                    else
                    {
                        if (tvwItem.SelectedNode.Parent != null) tvwItem.SelectedNode.Parent.Nodes.Add(nodeT);
                        else tvwItem.Nodes.Add(nodeT);
                    }
                }
                if (!frm.IsDisposed)
                {
                    frm.Close();
                    frm.Dispose();
                }
            }
            else
            {
                if (sCommand != "Root")
                {
                    if (tvwItem.SelectedNode.Name.Split(new char[] { ':' }).GetValue(1).ToString().ToUpper() == "FIX")
                    {
                        MSG.Error("ไม่สามารถเพิ่ม Item ได้เนื่องจาก Item นี้มี Type เป็น FIX โปรดแก้ไข Type ก่อน", "ข้อผิดพลาด");
                        return;
                    }
                }
                XmlNode node = (XmlNode)tvData.SelectedNode.Tag;
                frmAddItemData frm = new frmAddItemData();
                frm._xmlDoc = xmlDoc;
                frm._nodeMeans = node;
                frm.funStart();

                if (frm.ShowDialog() == DialogResult.Yes)
                {
                    TreeNode nodeT = new TreeNode();
                    nodeT.Name = frm.getName + ":" + frm.getType;
                    nodeT.Text = "Item Name:" + frm.getName + " Type:" + frm.getType;

                    XmlNode Section;

                    XmlNode SelectItem = (XmlNode)tvwItem.SelectedNode.Tag;
                    Section = SelectItem.SelectSingleNode("./Item");

                    XmlNode Items = xmlDoc.CreateElement("Items");
                    XmlAttribute att = xmlDoc.CreateAttribute("Name");
                    att.Value = frm.getName;
                    Items.Attributes.Append(att);
                    att = xmlDoc.CreateAttribute("Type");
                    att.Value = frm.getType;
                    Items.Attributes.Append(att);

                    if (frm._nodeOut != null) Items.AppendChild(frm._nodeOut);
                    if (frm._ListItem == null) Items.AppendChild(frm._nodeOutItem);
                    else
                    {
                        XmlNodeList Item = frm._ListItem.SelectNodes("./Item");
                        for (int i = 0; i < Item.Count; i++)
                        {
                            Items.AppendChild(Item[i]);
                        }
                    }

                    Section.AppendChild(Items);
                    nodeT.Tag = Items;
                    tvwItem.SelectedNode.Nodes.Add(nodeT);
                }
            }
            tvwItem.ExpandAll();
            funDrawMainTree();
        }
        #endregion

        #region Even
        private void tsbClose_Click(object sender, EventArgs e)
        {
            funClose();
        }
        private void tsbSave_Click(object sender, EventArgs e)
        {
            funSave();
        }
        private void tsbNew_Click(object sender, EventArgs e)
        {
            funNew();
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                tsbProfile.Checked = true;
                tsbData.Checked = false;
                tsbControl.Checked = false;
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                tsbProfile.Checked = false;
                tsbData.Checked = true;
                tsbControl.Checked = false;
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                tsbProfile.Checked = false;
                tsbData.Checked = false;
                tsbControl.Checked = true;
            }
            else
            {

            }
        }
        private void tsbProperties_Click(object sender, EventArgs e)
        {

        }
        private void tsbProfile_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }
        private void tsbData_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }
        private void tsbControl_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }
        private void frmDocument_Load(object sender, EventArgs e)
        {
            funLoad();
            AfterSelectData();
        }
       
        private void tsmiNewSection_Click(object sender, EventArgs e)
        {
            frmSection frm = new frmSection();
            tacData.SelectedIndex = 0;
            if (chbAutoID.Checked)
            {
                frm._AutoID = (int.Parse(nudMaxID.Value.ToString()) + 1);
            }
            if (frm.ShowDialog() == DialogResult.OK)
            {
                AutoID++;
                nudMaxID.Value = AutoID;
                funNewSection(frm._SectionID, frm._SectionName, frm._SectionType);
            }
            if (!frm.IsDisposed)
            {
                frm.Close();
                frm.Dispose();
            }
        }
        private void tsmiDeleteSection_Click(object sender, EventArgs e)
        {
            funDeleteSection();
            AfterSelectData();
        }

        private void butDApply_Click(object sender, EventArgs e)
        {
            funDApply();
        }
        private void butDCanecl_Click(object sender, EventArgs e)
        {
            funDCancel();
        }
        private void butWho_Click(object sender, EventArgs e)
        {
            funEditProfile("Who", trvWho);
        }
        private void butWhat_Click(object sender, EventArgs e)
        {
            funEditProfile("What", trvWhat);
        }
        private void butWhere_Click(object sender, EventArgs e)
        {
            funEditProfile("Where", trvWhere);
        }
        private void butWhen_Click(object sender, EventArgs e)
        {
            funEditProfile("When", trvWhen);
        }
        private void butWhy_Click(object sender, EventArgs e)
        {
            funEditProfile("Why", trvWhy);
        }
        private void tvData_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AfterSelectData();
        }
        private void tvData_Click(object sender, EventArgs e)
        {
            AfterSelectData();
        }
        private void tvData_MouseClick(object sender, MouseEventArgs e)
        {
            AfterSelectData();
        }
        private void butOK_Click(object sender, EventArgs e)
        {
            okData();
        }
        private void butCancel_Click(object sender, EventArgs e)
        {
            cancelData();
        }
        private void tsmiAddItem_Click(object sender, EventArgs e)
        {
            if (tacData.Enabled)
            {
                tacData.SelectedIndex = 1;
            }
        }
        private void tsmiAddItem_Click_1(object sender, EventArgs e)
        {
            if (tvData.SelectedNode == null) return;
            AddItem("Root");
        }
        private void tsmiDeleteItem_Click(object sender, EventArgs e)
        {
            if (tvwItem.SelectedNode == null) return;
            if (MSG.Confirm("คุณแน่ใจหรือไม่? ที่จะลบข้อมูลนี้", "คำเตือน") == DialogResult.Yes)
            {
                XmlNode nodeItems = (XmlNode)tvwItem.SelectedNode.Tag;
                XmlNode nodeParant = nodeItems.ParentNode;
                nodeParant.RemoveChild(nodeItems);
                if (tvwItem.SelectedNode.Parent == null)
                {

                    tvwItem.Nodes.Remove(tvwItem.SelectedNode);
                }
                else
                {
                    tvwItem.SelectedNode.Parent.Nodes.Remove(tvwItem.SelectedNode);
                }
            }
            funDrawMainTree();
        }
        private void tsmiAddChildsItem_Click(object sender, EventArgs e)
        {
            if (tvData.SelectedNode == null) return;
            if (tvwItem.SelectedNode == null) return;
            else AddItem("Child");
        }
        private void tsbExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            String strCuu = Directory.GetCurrentDirectory();
            saveFileDialog1.Filter = "XML Document file|*.xml";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SaveFile(saveFileDialog1.FileName, xmlDoc.OuterXml);
            }
            Directory.SetCurrentDirectory(strCuu);
        }
        private void tsbLoad_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Xml Document|*.xml";
                string strCuu = Directory.GetCurrentDirectory();
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    LoadXML(openFileDialog1.FileName);
                }
                Directory.SetCurrentDirectory(strCuu);
            }
            catch { }
        }
        private void tsbViewXML_Click(object sender, EventArgs e)
        {
            string Filename = Directory.GetCurrentDirectory() + "/testData.xml";
            frmViewXML frm = new frmViewXML();
            xmlDoc.Save(Filename);
            frm._FilePaht = Filename;
            frm.ShowDialog();
        }
        private void tsmiEditItems_Click(object sender, EventArgs e)
        {
            if (tvwItem.SelectedNode == null) return;
            XmlNode Item = (XmlNode)tvwItem.SelectedNode.Tag;
            frmAddItemData frm = new frmAddItemData();
            frm.funStart();
            frm._InputItem = Item;
            frm._xmlDoc = xmlDoc;
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                XmlNode Section;

                string ID = tvData.SelectedNode.Name.Split(new char[] { ':' }).GetValue(0).ToString();
                if (tvwItem.SelectedNode.Parent == null) Section = xmlDoc.SelectSingleNode("//Document/Data/Section[@ID='" + ID + "']");
                else Section = ((XmlNode)tvwItem.SelectedNode.Tag).ParentNode;

                Section.RemoveChild(Item);

                XmlNode Items = xmlDoc.CreateElement("Items");
                XmlAttribute att = xmlDoc.CreateAttribute("Name");
                att.Value = frm.getName;
                Items.Attributes.Append(att);
                att = xmlDoc.CreateAttribute("Type");
                att.Value = frm.getType;
                Items.Attributes.Append(att);

                if (frm._nodeOut != null) Items.AppendChild(frm._nodeOut);
                if (frm._ListItem == null) Items.AppendChild(frm._nodeOutItem);
                else
                {
                    XmlNodeList Item2 = frm._ListItem.SelectNodes("./Item");
                    for (int i = 0; i < Item2.Count; i++)
                    {
                        Items.AppendChild(Item2[i]);
                    }
                }
                Section.AppendChild(Items);
                tvwItem.SelectedNode.Name = frm.getName + ":" + frm.getType;
                tvwItem.SelectedNode.Text = "Item Name:" + frm.getName + " Type:" + frm.getType;
                tvwItem.SelectedNode.Tag = Items;

                if (frm.getType == "FIX")
                {
                    tvwItem.SelectedNode.Nodes.Clear();
                }
            }
            if (!frm.IsDisposed)
            {
                frm.Close();
                frm.Dispose();
            }
            funDrawMainTree();
        }
        #endregion    
    }
}