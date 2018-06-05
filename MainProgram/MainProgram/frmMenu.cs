using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using System.Deployment.Application;
namespace MainProgram
{
    public partial class frmMenu : Form
    {
        
        string AuthorXML;
        string UserID;
        string position;
        public string position_id;
        string strConnect;
        string ServerName;
        XmlDocument xmlConfig;
        string ProjectID;

        ArrayList frmList = new ArrayList();

        public frmMenu(string iServerName)
        {
            InitializeComponent();

            ServerName = iServerName;
        }

        #region Properties
        public string _ProjectID
        {
            set { ProjectID = value; }
        }
        public string _AuthorXML
        {
            set { AuthorXML = value; }
        }       
        public string _UserID
        {
            set { UserID = value; }
            get { return UserID; }
        }       
        public string _Position
        {
            set { position = value; }
            get { return position; }
        }       
        public string _Connection
        {
            set { strConnect = value; }
            get { return strConnect; }
        }
        #endregion       
        #region Method
        #region MDI
        private void funArrangeIcon()
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.ArrangeIcons);
        }
        private void funCascade()
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
        }
        private void funHorizontal()
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
        }
        private void funVertical()
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
        }
        private void funMaximized()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                this.MdiChildren[i].WindowState = FormWindowState.Maximized;
            }
        }
        private void funMinimized()
        {
            Form[] charr = this.MdiChildren;
            foreach (Form chform in charr)
                chform.WindowState = FormWindowState.Minimized;
        }
        private void funClssAll()
        {
            Form[] charr = this.MdiChildren;
            foreach (Form chform in charr)
                chform.Close();
        }
         #endregion
        private string setPositon(string str)
        {
            if (str == "A")
                return "Administrator";

            NextwaverDB.NColumns NCS = new NextwaverDB.NColumns();
            NCS.Add(new NextwaverDB.NColumn("ID"));
            NCS.Add(new NextwaverDB.NColumn("POSITION_NAME"));

            NextwaverDB.NWheres NWS = new NextwaverDB.NWheres();
            NWS.Add(new NextwaverDB.NWhere("POSITION_CODE", str));

            ConnectServer.cConection cConn = new ConnectServer.cConection(UserID);
            DataTable dt = cConn.Retreive(strConnect, Program.OffiecSpaceId, Program.DatabaseName, "position", NCS, NWS);
            position_id = dt.Rows[0]["ID"].ToString();
            
            return dt.Rows[0]["POSITION_NAME"].ToString();           
        }
        private bool checkMdi(string strFrom)
        {
            for (int i = 0; i < frmList.Count; i++)
            {
                Form frmTemp = (Form)frmList[i];
                if (frmTemp.Tag.ToString() == strFrom)
                {
                    if (frmTemp.IsDisposed)
                    {
                        frmList.Remove(frmList[i]);
                        return true; ;
                    }
                    frmTemp.WindowState = FormWindowState.Maximized;
                    return false;
                }
            }
            return true;
        }
        private bool funCheckTab(string MName)
        {
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                if (tabControl1.TabPages[i].Tag.ToString() == MName)
                {
                    tabControl1.SelectedIndex = i;
                    return false;
                }
            }
            return true;
        }
        private void OpenConfig()
        {
            Form frm = new Form();
            frm.WindowState = FormWindowState.Maximized;
            CreateConfigMangement.uscMain uc = new CreateConfigMangement.uscMain();
            uc._Command = "1";
            uc.funSetValue("MConfig.xml");
            uc.Dock = DockStyle.Fill;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Icon = new Icon("Icon/FauxS.ico");
            frm.Text = "Edit Config";
            frm.Size = new Size(800, 600);
            frm.Controls.Add(uc);
            frm.ShowDialog();
            funCelear();
            frm.Close();
            frm.Dispose();
            funCreateExpand();
        }
        private void funCelear()
        {
            if (tsmiMode.Text == "Tabs Mode")
            {
                tabControl1.Controls.Clear();
            }
            else
            {
                int length = this.MdiChildren.Length;
                for (int i = 0; i < length; i++)
                {
                    this.MdiChildren[0].Close();
                }
            }
        }
        private void funClose()
        {
            if (tsmiMode.Text == "Tabs Mode")
            {
                if (tabControl1.TabPages.Count == 0) return;
                tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
            }
            else
            {
                if (frmList.Count == 0) return;
                Form frmTemp = (Form)frmList[0];
                frmTemp.Close();
                frmTemp.Dispose();
                frmList.Remove(frmList[0]);
                return;
            }
        }
        private void setEnable(bool flag)
        {
            tsmiCascade.Enabled = flag;
            tsmiHorizontal.Enabled = flag;
            tsmiMinimize.Enabled = flag;
            tsmiVertical.Enabled = flag;
            tsmiCloseAll.Enabled = flag;
            tsmiMaximum.Enabled = flag;
        }
        private string SelectXML()
        {
            string SQL = @"select Author_XML from [user]
                            where UserID='@UserID'";
            SQL = SQL.Replace("@UserID", UserID);
            ConnectServer.cConection WebService = new ConnectServer.cConection(UserID);
            DataSet ds = WebService.Retreive(SQL, strConnect);
            if (ds == null) return null;
            DataTable dt = ds.Tables[0];
            return dt.Rows[0][0].ToString();
        }
        private void funCreateExpand()
        {
            //taskPane1
            ConnectServer.cConection cConn = new ConnectServer.cConection(UserID);
            string strDoc = cConn.getLastDoc(strConnect, Program.OffiecSpaceId, Program.DatabaseName, "position", int.Parse(position_id));

            XmlDocument xmlImage = new XmlDocument();
            xmlImage.Load(Application.StartupPath + @"\Image.xml");

            xmlConfig = new XmlDocument();
            XmlNodeList nodeView;
            if (position == "A")
            {
                XmlDocument xmlConfigT = new XmlDocument();
                xmlConfigT.Load("MConfig.xml");
                nodeView = xmlConfigT.SelectNodes("//Config/Views/View");
            }
            else
            {
                if (strDoc == "")
                {
                    XmlDocument xmlConfigT = new XmlDocument();
                    xmlConfigT.Load("MConfig.xml");
                    nodeView = xmlConfigT.SelectNodes("//Config/Views/View");
                }
                else
                {
                    XmlDocument xmlConfigT = new XmlDocument();
                    xmlConfigT.LoadXml(strDoc);
                    nodeView = xmlConfigT.SelectNodes("//Views/View");
                }
            }
            xmlConfig.Load("MConfig.xml");
          
            taskPane1.Controls.Clear();    

            for (int i = 0; i < nodeView.Count; i++)
            {
                string Name = nodeView[i].Attributes["Name"].Value;
                string PathTitle = "";
                try { PathTitle = xmlImage.SelectSingleNode("//Image[@ID='" + nodeView[i].Attributes["IconID"].Value + "']").Attributes["Path"].Value; }
                catch { }
                string PathWaterMark = "";
                try { PathWaterMark = xmlImage.SelectSingleNode("//Image[@ID='" + nodeView[i].Attributes["WaterMarkID"].Value + "']").Attributes["Path"].Value; }
                catch { }
                if (nodeView[i].Attributes["Enable"].Value.ToString() == "T")
                {
                    XPExplorerBar.Expando expTemp = funExpando("" + nodeView[i].Attributes["Name"].Value, PathTitle, PathWaterMark);
                    XmlNodeList listItem = nodeView[i].SelectNodes("./Item");
                    for (int j = 0; j < listItem.Count; j++)
                    {
                        string IconPath = "";
                        try { IconPath = "" + xmlImage.SelectSingleNode("//Image[@ID='" + listItem[j].Attributes["IconID"].Value + "']").Attributes["Path"].Value; }
                        catch { }
                        if (listItem[j].Attributes["Enable"].Value.ToString() == "T")
                        {
                            XmlNode nodeMap = xmlConfig.SelectSingleNode("//Config/Mappings/Mapping[@ID='" + listItem[j].Attributes["MappingID"].Value + "']");

                            XPExplorerBar.TaskItem xItem = new XPExplorerBar.TaskItem();

                            xItem.Text = "" + nodeMap.Attributes["Name"].Value;
                         //   xItem.Text = "THODSAPON";

                            xItem.Tag = nodeMap;
                            try { xItem.Image = Image.FromFile(IconPath); }
                            catch { }
                            try { xItem.Image.Tag = IconPath; }
                            catch { }
                            xItem.Click += new EventHandler(xItem_Click);
                            expTemp.Items.Add(xItem);
                        }
                    }
                    //expTemp.Collapse();
                    taskPane1.Controls.Add(expTemp);
                }
            }
        }
        private XPExplorerBar.Expando funExpando(string Text, string PathTitle, string PathWaterMark)
        {
            XPExplorerBar.Expando expTemp = new XPExplorerBar.Expando();
            expTemp.AutoLayout = true;
            try { expTemp.TitleImage = Image.FromFile(Application.StartupPath + @"/" + PathTitle); }
            catch { }
            expTemp.ExpandedHeight = 44;
            try { expTemp.Watermark = Image.FromFile(Application.StartupPath + @"/" + PathWaterMark); }
            catch { }
            expTemp.CustomHeaderSettings.NormalTitleColor = Color.White;
            expTemp.CustomHeaderSettings.NormalBackColor = Color.FromArgb(0, 0, 64);
            expTemp.CustomSettings.NormalBorderColor = Color.Black;
            expTemp.CustomSettings.NormalBackColor = Color.White;
            expTemp.CustomHeaderSettings.NormalTitleHotColor = Color.White;
            expTemp.ForeColor = Color.White;
            expTemp.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            expTemp.ForeColor = System.Drawing.Color.White;
            expTemp.Location = new System.Drawing.Point(12, 12);
            expTemp.Size = new System.Drawing.Size(226, 44);
            expTemp.SpecialGroup = false;
            expTemp.TabIndex = 3;
            expTemp.Text = Text;
            //expTemp.Watermark = ((System.Drawing.Image)(resources.GetObject("expManament.Watermark")));
            return expTemp;
        }
        private bool checkRow(string RowID)
        {
            if (RowID == "A")
            {
                if (position == "A") return true;
                else return false;
            }
            return true;
        }
        #endregion
        #region Even
        void xItem_Click(object sender, EventArgs e)
        {
            #region Code
            XPExplorerBar.TaskItem taskItem = (XPExplorerBar.TaskItem)sender;

            lbStatus.Text = "Progress...";
            this.Update();
            XmlNode nTemp = (XmlNode)taskItem.Tag;
            if ("" + nTemp.Attributes["ManagementID"].Value == "")
            {
                MessageBox.Show("รายการเมนูนี้ยังไม่ถูกสร้างขึ้น", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string strPathM = "//Managements/Management[@ID='" + nTemp.Attributes["ManagementID"].Value + "']";
            XmlDocument xmlMange = new XmlDocument();
            try { xmlMange.LoadXml(xmlConfig.SelectSingleNode(strPathM).OuterXml); }
            catch { }

            string strPathT = "//Themes/Theme[@ID='" + nTemp.Attributes["ThemeID"].Value + "']";
            XmlDocument xmlTheme = new XmlDocument();
            try { xmlTheme.LoadXml(xmlConfig.SelectSingleNode(strPathT).OuterXml); }
            catch { }

            string strPathD = "//Config/Images";
            XmlDocument xmlDetail = new XmlDocument();
            try { xmlDetail.LoadXml(xmlConfig.SelectSingleNode(strPathD).OuterXml); }
            catch { }

            string strPathTool = "//Tools/Tool[@ID='" + nTemp.Attributes["ToolID"].Value + "']";
            XmlDocument xmlTool = new XmlDocument();
            try { xmlTool.LoadXml(xmlConfig.SelectSingleNode(strPathTool).OuterXml); }
            catch { }

            string strPathParameter = "//Parameters/Parameter[@ID='" + nTemp.Attributes["ParameterID"].Value + "']";
            XmlDocument xmlParameter = new XmlDocument();
            string strParameter = "";
            try { strParameter = xmlConfig.SelectSingleNode(strPathParameter).OuterXml; }
            catch { }

            string MenuType = "";
            try
            {
                MenuType = nTemp.Attributes["ManagementID"].Value;
            }
            catch { }


            string DisplayType = "";

            try
            {
                DisplayType = xmlMange.SelectSingleNode("//Management").Attributes["Type"].Value;
            }
            catch { }

            if (tsmiMode.Text == "Tabs Mode")
            {
                if (funCheckTab(nTemp.Attributes["ManagementID"].Value + nTemp.Attributes["Name"].Value))
                {
                    if (MenuType.ToUpper() == "USERCONTROL")
                    {
                        TabPage tabTemp = new TabPage();
                        tabTemp.Text = taskItem.Text;
                        tabTemp.Tag = nTemp.Attributes["ManagementID"].Value + nTemp.Attributes["Name"].Value;
                        //tabTemp.Controls.Add(uct);
                        ToolCommand.csUserControl CSUC = new ToolCommand.csUserControl();
                        CSUC.OfficeSpaceId = Program.OffiecSpaceId;
                        ToolCommand.ucForm paTemp = CSUC.getPanel(strConnect, UserID, position, nTemp.Attributes["ToolID"].Value);
                        paTemp.Margin = new Padding(0);
                        paTemp.Dock = DockStyle.Fill;
                        tabTemp.Controls.Add(paTemp);
                        tabControl1.Controls.Add(tabTemp);
                        tabControl1.SelectedTab = tabTemp;
                    }
                    else
                    {
                        if (DisplayType == "SQL")
                        {
                            frmMangements.UserControl1_DB uct = new frmMangements.UserControl1_DB();
                            uct._IMG = Image.FromFile("" + taskItem.Image.Tag);
                            uct._Position = setPositon(position);
                            uct._User = UserID;
                            uct._ProjectID = ProjectID;
                            uct.loadMangement(xmlMange, xmlDetail, xmlTool, strParameter, strConnect);
                            //uct.loadTools(xmlTool);
                            uct.loadTheme(xmlTheme);
                            uct.Dock = DockStyle.Fill;
                            uct.Margin = new System.Windows.Forms.Padding(0);

                            TabPage tabTemp = new TabPage();
                            tabTemp.Text = taskItem.Text;
                            tabTemp.Tag = nTemp.Attributes["ManagementID"].Value + nTemp.Attributes["Name"].Value;
                            tabTemp.Controls.Add(uct);
                            tabControl1.Controls.Add(tabTemp);
                            tabControl1.SelectedTab = tabTemp;
                        }
                        else
                        {
                            frmMangements.UserControl1 uct = new frmMangements.UserControl1();
                            uct.OffiecSpaceId = Program.OffiecSpaceId;
                            try { uct._IMG = Image.FromFile("" + taskItem.Image.Tag); }
                            catch { }
                            uct._Position = setPositon(position);
                            uct._User = UserID;
                            uct._ProjectID = ProjectID;
                            uct.loadMangement(xmlMange, xmlDetail, xmlTool, strParameter, strConnect);
                            //uct.loadTools(xmlTool);
                            uct.loadTheme(xmlTheme);
                            uct.Dock = DockStyle.Fill;
                            uct.Margin = new System.Windows.Forms.Padding(0);

                            TabPage tabTemp = new TabPage();
                            tabTemp.Text = taskItem.Text;
                            tabTemp.Tag = nTemp.Attributes["ManagementID"].Value + nTemp.Attributes["Name"].Value;
                            tabTemp.Controls.Add(uct);
                            tabControl1.Controls.Add(tabTemp);
                            tabControl1.SelectedTab = tabTemp;
                        }
                    }
                }
            }
            else
            {
                if (checkMdi(nTemp.Attributes["ManagementID"].Value + nTemp.Attributes["Name"].Value))
                {
                    if (MenuType.ToUpper() == "USERCONTROL")
                    {
                        ToolCommand.csUserControl CSUC = new ToolCommand.csUserControl();
                        CSUC.OfficeSpaceId = Program.OffiecSpaceId;
                        ToolCommand.ucForm paTemp = CSUC.getPanel(strConnect, UserID, position, nTemp.Attributes["ToolID"].Value);
                        paTemp.Margin = new Padding(0);
                        paTemp.Dock = DockStyle.Fill;

                        Form frm = new Form();
                        frm.Name = taskItem.Text;
                        frm.WindowState = FormWindowState.Maximized;
                        frm.Text = taskItem.Text;
                        frm.Icon = new Icon("Icon/FauxS.ico");
                        frm.Tag = nTemp.Attributes["ManagementID"].Value + nTemp.Attributes["Name"].Value;
                        frm.MdiParent = this;
                        frm.Size = new Size(800, 600);
                        frm.Controls.Add(paTemp);
                        frm.Show();
                        frmList.Add(frm);

                    }
                    else
                    {
                        if (DisplayType == "SQL")
                        {
                            frmMangements.UserControl1_DB uct = new frmMangements.UserControl1_DB();
                            uct._IMG = Image.FromFile("" + taskItem.Image.Tag);
                            uct._Position = setPositon(position);
                            uct._ProjectID = ProjectID;
                            uct._User = UserID;
                            uct.loadMangement(xmlMange, xmlDetail, xmlTool, strParameter, strConnect);
                            uct.loadTheme(xmlTheme);
                            //uct.loadTools(xmlTool);                       
                            uct.Dock = DockStyle.Fill;
                            uct.Margin = new System.Windows.Forms.Padding(0);

                            uct._frmParent = this;
                            Form frm = new Form();
                            frm.Name = "Form Management";
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Text = taskItem.Text;
                            frm.Icon = new Icon("Icon/FauxS.ico");
                            frm.Tag = nTemp.Attributes["ManagementID"].Value + nTemp.Attributes["Name"].Value;
                            frm.MdiParent = this;
                            frm.Size = new Size(800, 600);
                            frm.Controls.Add(uct);
                            frm.Show();
                            frmList.Add(frm);
                        }
                        else
                        {
                            frmMangements.UserControl1 uct = new frmMangements.UserControl1();
                            uct.OffiecSpaceId = Program.OffiecSpaceId;
                            uct._IMG = Image.FromFile("" + taskItem.Image.Tag);
                            uct._Position = setPositon(position);
                            uct._ProjectID = ProjectID;
                            uct._User = UserID;
                            uct.loadMangement(xmlMange, xmlDetail, xmlTool, strParameter, strConnect);
                            uct.loadTheme(xmlTheme);
                            //uct.loadTools(xmlTool);                       
                            uct.Dock = DockStyle.Fill;
                            uct.Margin = new System.Windows.Forms.Padding(0);

                            uct._frmParent = this;
                            Form frm = new Form();
                            frm.Name = "Form Management";
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Text = taskItem.Text;
                            frm.Icon = new Icon("Icon/FauxS.ico");
                            frm.Tag = nTemp.Attributes["ManagementID"].Value + nTemp.Attributes["Name"].Value;
                            frm.MdiParent = this;
                            frm.Size = new Size(800, 600);
                            frm.Controls.Add(uct);
                            frm.Show();
                            frmList.Add(frm);
                        }
                    }
                }
            }
            this.Update();
            lbStatus.Text = "Ready...";
            #endregion
        }
        private void frmMenu_Load(object sender, EventArgs e)
        {            
            tslPosition.Text = "Position:" + setPositon(position);
            tslUserName.Text = "UserName:" + UserID;
            funCreateExpand();
            if (ApplicationDeployment.IsNetworkDeployed)
            { 
                string sMs = "Menu (DeployVersion => @Version - LastUpdate => @Date)";
                sMs = sMs.Replace("@Version", ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString());
                sMs = sMs.Replace("@Date", ApplicationDeployment.CurrentDeployment.TimeOfLastUpdateCheck.ToLongDateString());
                this.Text = sMs;
            }
            else
            {               
                this.Text = "Menu (Version => " + Application.ProductVersion + ")";
            }
        }
        private void tsbClose_Click(object sender, EventArgs e)
        {
            funClose();
        }
        private void tbtsmShowTree_Click(object sender, EventArgs e)
        {
            if (tsmiMode.Text == "Tabs Mode")
            {
                if (taskPane1.Visible)
                {
                    taskPane1.Visible = false;
                }
                else
                {
                    taskPane1.Visible = true;
                }
            }
            else
            {
                if (taskPane1.Visible)
                {
                    taskPane1.Visible = false;
                }
                else
                {
                    taskPane1.Visible = true;
                }
            }            
        }
        private void tsmiCascade_Click(object sender, EventArgs e)
        {
            funCascade();
        }
        private void tsmiMinimize_Click(object sender, EventArgs e)
        {
            funMinimized();
        }
        private void tsmiVertical_Click(object sender, EventArgs e)
        {
            funVertical();
        }
        private void tsmiHorizontal_Click(object sender, EventArgs e)
        {
            funHorizontal();
        }
        private void tsmiCloseAll_Click(object sender, EventArgs e)
        {
            funClssAll();
        }       
        private void tsmiClose_Click(object sender, EventArgs e)
        {
            funClose();      
        }
        private void tsmiMaximum_Click(object sender, EventArgs e)
        {
            funMaximized();
        }
        private void tsmiMode_Click(object sender, EventArgs e)
        {
            if (tsmiMode.Text == "Windows Mode")
            {
                tsmiMode.Text = "Tabs Mode";
                tabControl1.Visible = true;             
                int length = this.MdiChildren.Length;
                for (int i = 0; i < length; i++)
                {
                    this.MdiChildren[0].Close();
                }
                setEnable(false);
            }
            else
            {
                tsmiMode.Text = "Windows Mode";
                tabControl1.Visible = false;             
                tabControl1.Controls.Clear();
                setEnable(true);
            }
            this.Update();           
            this.Update();
        }
        private void statusStrip1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            if (position != "A") return;
            if (e.Button.ToString() == "Right") OpenConfig();
        } 
        #endregion

        private void tsmiLockoff_Click(object sender, EventArgs e)
        {            
            frmLogin frmlogin = new frmLogin();
            frmlogin._Connection = strConnect;
            frmlogin.bExtra = true;
            if (frmlogin.ShowDialog() == DialogResult.OK)
            {
                funCelear();
                _UserID = frmlogin.USERNAME;
                _Position = frmlogin.Position;               
                tslPosition.Text = "Position:" + setPositon(position);
                tslUserName.Text = "UserName:" + UserID;
                tabControl1.TabPages.Clear();
                funCreateExpand();
            }
            if (!frmlogin.IsDisposed)
            {
                frmlogin.Close();
                frmlogin.Dispose();
            }

        }
    }
}