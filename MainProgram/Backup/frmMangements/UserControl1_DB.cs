using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Collections;
using System.IO;
using System.Globalization;

namespace frmMangements
{
    public partial class UserControl1_DB : UserControl
    {        
        
        string strConnect;
        string position;
        string user;
        string strParameter;
        XmlDocument xmlconfig;
        XmlDocument xmlDetail;
        Form frmParent = null;
        int switchCount = 0;
        ArrayList listButton = new ArrayList();
        int PageSize = 2000;
        int Page = 1;
        int StartPage = 1;
        string ProjectID;


        public UserControl1_DB()
        {
            InitializeComponent();
        }

        #region Properties
        public Image _IMG
        {
            set { pictureBox1.Image = value; }
        }
        public string _ProjectID
        {
            set { ProjectID = value; }
        }
        public Form _frmParent
        {
            set { frmParent = value; }
            get { return frmParent; }
        }
        public string _User
        {
            set { user = value; }
            get { return user; }
        }       
        public string _Position
        {
            set { position = value; }
            get { return position; }
        }
        #endregion
        #region Method
        private void setFilterNew()
        {
            XmlNodeList nodeSEARCH = xmlconfig.SelectNodes("//Management/Searchs/Search");
            cboFieldName1.Items.Clear();
            cboFieldName2.Items.Clear();
            cboFieldName3.Items.Clear();
            cboFieldName4.Items.Clear();
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("ID");
            dt1.Columns.Add("Text");
            for (int i = 0; i < nodeSEARCH.Count; i++)
            {
                DataRow dr = dt1.NewRow();
                dr.BeginEdit();
                dr["ID"] = "" + i;
                dr["Text"] = "" + nodeSEARCH[i].Attributes.GetNamedItem("Text").Value;
                dr.EndEdit();
                dt1.Rows.Add(dr);
            }
            AddDataSource(cboFieldName1, dt1.Copy());
            AddDataSource(cboFieldName2, dt1.Copy());
            AddDataSource(cboFieldName3, dt1.Copy());
            AddDataSource(cboFieldName4, dt1.Copy());
        }
        private void funACL()
        {
            if (grdManange.DataRows.Count == 0) return;
            XmlNode nodeACL = xmlconfig.SelectSingleNode("//Management/ACL");
            string TableName = "" + nodeACL.Attributes["TableName"].Value;
            string ColumnName = "" + nodeACL.Attributes["Column"].Value;
            DataSet dsTemp = (DataSet)grdManange.DataSource;
            DataTable dtTemp = dsTemp.Tables["DATA"];
            int[] listSelectIndex = getIndex();
            if (!dtTemp.Columns.Contains("ACL_ID"))
            {
                MessageBox.Show("äÁèÁÕ Column ª×èÍ ACL_ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!dtTemp.Columns.Contains("UserCreate"))
            {
                MessageBox.Show("äÁèÁÕ Column ª×èÍ UserCreate", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dtTemp.Columns.Contains(ColumnName))
            {
                Xceed.Grid.DataRow dr;
                int current = 0;

                try{dr = grdManange.CurrentRow as Xceed.Grid.DataRow;current = dr.Index;}catch { }
                //ACL.cMain cACL = new ACL.cMain();
                //cACL.funSetACL(dtTemp, listSelectIndex, TableName, ColumnName, strConnect, user, ProjectID);
                funSearch();
                try { grdManange.DataRows[0].IsSelected = false; }
                catch { }
                for (int i = 0; i < listSelectIndex.Length; i++)
                {
                    try { grdManange.DataRows[listSelectIndex[i]].IsSelected = true; }
                    catch { }
                }
                try { grdManange.CurrentRow = grdManange.DataRows[current]; }
                catch { }
            }
            else
            {
                MessageBox.Show("äÁèÁÕ Column ª×èÍ " + ColumnName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void selectGroupToCbx()
        {
            try
            {
                string SQL = xmlconfig.SelectSingleNode("//Management/Query/SQL[@ID='Group']").InnerText;
                labGroupBy.Text = xmlconfig.SelectSingleNode("//Management/Query/SQL[@ID='Group']").Attributes["Text"].Value.ToString();
                ConnectServer.cConection WebService = new ConnectServer.cConection(user);
                DataSet ds = WebService.Retreive(SQL, strConnect);
                if (ds == null)
                {
                    tlpGroupBy.Visible = false;
                    return;
                }
                cbxGroupBy.DataSource = ds.Tables[0];
                cbxGroupBy.DisplayMember = "Name";
                cbxGroupBy.ValueMember = "Code";
                //cbxGroupBy.Items.Add("");
                //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //{
                //    if (ds.Tables[0].Rows[i][0].ToString() != "") cbxGroupBy.Items.Add(ds.Tables[0].Rows[i][0]);
                //}
            }
            catch { }
        }
        public void loadMangement(XmlDocument ixmlconfig, XmlDocument ixmlDetail, XmlDocument ixmlTools, string ixmlParameter, string ConnectionString)
        {
            if (ixmlconfig == null) return;
            xmlDetail = ixmlDetail;
            xmlconfig = ixmlconfig;

            strParameter = ixmlParameter;

            #region Connection String
            string Server = xmlconfig.SelectSingleNode("//Management/Connection/Item").Attributes["Server"].Value.ToString();
            string Database = xmlconfig.SelectSingleNode("//Management/Connection/Item").Attributes["Database"].Value.ToString();
            string Login = xmlconfig.SelectSingleNode("//Management/Connection/Item").Attributes["Login"].Value.ToString();
            string Password = xmlconfig.SelectSingleNode("//Management/Connection/Item").Attributes["Password"].Value.ToString();
            strConnect = "Data Source=@Source; uid=@Uid; pwd=@Pass; Initial Catalog=@Database;";
            strConnect = strConnect.Replace("@Source", Server);
            strConnect = strConnect.Replace("@Uid", Login);
            strConnect = strConnect.Replace("@Pass", Password);
            strConnect = strConnect.Replace("@Database", Database);
            //strConnect = ConnectionString;
            #endregion

            setFilterNew();
            setFilter2();
            setHeader();

            selectGroupToCbx();
            try{ cbxGroupBy.SelectedIndex = 0;  }
            catch { }
            bFirstRun = false;

            funSearch();
            //selectDataToGrid();
          
            tslPage.Text = "Page 1/" + MaxPage;
            setGrid();

            funSelectRowChange();
            XmlDocument xmlTool = new XmlDocument();
            xmlTool.LoadXml(xmlconfig.SelectSingleNode("//Management/Tools").OuterXml);
            splContainerMain.Panel2Collapsed = !setVisible(xmlTool.SelectSingleNode("./Tools/Item[@Name='View']").Attributes["Visible"].Value.ToString());
            CreateToolScriptItems(xmlTool);

            if (ixmlTools != null) loadTools(ixmlTools);

            setRowCount();
            tstbUser.Text = user;
            tstbPositon.Text = position;
            funSelectRowChange();
            //setColor();
        }

        private void setFilter2()
        {
            XmlNodeList nodeSEARCH = xmlconfig.SelectNodes("//Management/SearchLike/Search");
            XmlNode nodeSearchLike1 = xmlconfig.SelectSingleNode("//Management/SearchLike/Search[@ID='1']");
            XmlNode nodeSearchLike2 = xmlconfig.SelectSingleNode("//Management/SearchLike/Search[@ID='2']");
            XmlNode nodeSearchLike3 = xmlconfig.SelectSingleNode("//Management/SearchLike/Search[@ID='3']");
            XmlNode nodeSearchLike4 = xmlconfig.SelectSingleNode("//Management/SearchLike/Search[@ID='4']");
            XmlNode nodeSearchLike5 = xmlconfig.SelectSingleNode("//Management/SearchLike/Search[@ID='5']");

            if (nodeSearchLike1 != null)
            {
                chbC1.Text = "" + nodeSearchLike1.Attributes["Text"].Value;
                chbC1.Tag = "" + nodeSearchLike1.Attributes["FiledName"].Value;
            }
            else chbC1.Visible = false;

            if (nodeSearchLike2 != null)
            {
                chbC2.Text = "" + nodeSearchLike2.Attributes["Text"].Value;
                chbC2.Tag = "" + nodeSearchLike2.Attributes["FiledName"].Value;
            }
            else chbC2.Visible = false;

            if (nodeSearchLike3 != null)
            {
                chbC3.Text = "" + nodeSearchLike3.Attributes["Text"].Value;
                chbC3.Tag = "" + nodeSearchLike3.Attributes["FiledName"].Value;
            }
            else chbC3.Visible = false;

            if (nodeSearchLike4 != null)
            {
                chbC4.Text = "" + nodeSearchLike4.Attributes["Text"].Value;
                chbC4.Tag = "" + nodeSearchLike4.Attributes["FiledName"].Value;
            }
            else chbC4.Visible = false;

            if (nodeSearchLike5 != null)
            {
                chbC5.Text = "" + nodeSearchLike5.Attributes["Text"].Value;
                chbC5.Tag = "" + nodeSearchLike5.Attributes["FiledName"].Value;
            }
            else chbC5.Visible = false;
        }
        private void setPageSige()
        {
            try
            {
                PageSize = int.Parse(xmlconfig.SelectSingleNode("//Management/PageSize").InnerText);
            }
            catch { PageSize = 1000; }
        }
        public void loadTools(XmlDocument ixmlTool)
        {
            tsBar.Name = ixmlTool.SelectSingleNode("//Tool").Attributes["ID"].Value.ToString();
            XmlDocument xmlTool = new XmlDocument();
            xmlTool.LoadXml(ixmlTool.SelectSingleNode("//Tool").OuterXml);
            CreateToolScriptItems(xmlTool);
        }
        private void setRowCount()
        {
            tstb_RowsCount.Text = grdManange.DataRows.Count.ToString();
        }
        private void setColor()
        {
            try
            {
                XmlDocument xmlConfig = new XmlDocument();
                xmlConfig.Load(Application.StartupPath + "/Config.xml");
                XmlNode nodeConfig = xmlConfig.SelectSingleNode("//Configs/Config[@ID='10']");
                string status="", status_name = "DATA_STATUS";

                Dictionary<string, int> dicColor = new Dictionary<string, int>();
                XmlNodeList listColor = nodeConfig.SelectNodes("./Item");
                for (int i = 0; i < listColor.Count; i++)
                    dicColor.Add(listColor[i].Attributes["Value"].Value, int.Parse(listColor[i].Attributes["Color"].Value));

                for (int i = 0; i < grdManange.DataRows.Count; i++)
                {
                    try
                    {
                        try { status = "" + grdManange.DataRows[i].Cells["STATUS"].Value; }
                        catch { }
                        status = status.Trim();                       
                        grdManange.DataRows[i].Cells[status_name].BackColor = Color.FromArgb(dicColor[status]);
                        grdManange.DataRows[i].Cells[status_name].ForeColor = Color.White;

                        try
                        {
                            if (""+grdManange.DataRows[i].Cells["Data_Used"].Value == "USED")
                                grdManange.DataRows[i].Cells["Data_Used"].BackColor = Color.Green;
                            else
                                grdManange.DataRows[i].Cells["Data_Used"].BackColor = Color.Red;
                            grdManange.DataRows[i].Cells["Data_Used"].ForeColor = Color.White;
                        }
                        catch { }
                      
                    }
                    catch { }
                }
                
            }
            catch { }
        }
        private void funSelectRowChange()
        {
            createDataRowValue();            
            EnableButton();
           
            try
            {
                if (grdManange.DataRows.Count == 0) return;
                ToolCommand.csCommand cCom = new ToolCommand.csCommand("SQL");
                cCom._PanelView = panelView;
                DataSet ds = (DataSet)grdManange.DataSource;
                Xceed.Grid.DataRow dr;
                int current = 0;

                try
                {
                    dr = grdManange.CurrentRow as Xceed.Grid.DataRow;
                    current = dr.Index;
                }
                catch { }
                string ToolID = tsBar.Name;

                cCom.RowClick(ToolID, ds.Tables[0], current, strConnect, user, position);
            }
            catch { }
        }
        private void EnableButton()
        {
            try
            {
                Xceed.Grid.DataRow dr = (Xceed.Grid.DataRow)grdManange.CurrentRow;
                string status = "";
                try { status = "" + dr.Cells["STATUS"].Value; }
                catch { }
                //try { status = "" + dr.Cells["DATA_STATUS"].Value; }
                //catch { }


                status = status.Split(new char[] { ' ' }).GetValue(0).ToString();
                for (int i = 0; i < listButton.Count; i++)
                {
                    try
                    {
                        object objTemp = listButton[i];
                        if (objTemp.GetType() == typeof(ToolStripButton))
                        {
                            ToolStripButton tsb = (ToolStripButton)objTemp;
                            XmlNode nodeTemp = (XmlNode)tsb.Tag;
                            string sEnable = "" + nodeTemp.Attributes["S_Enable"].Value;
                            string sVisible = "" + nodeTemp.Attributes["S_Visible"].Value;
                            tsb.Enabled = sEnable.Contains(status);
                            tsb.Visible = sVisible.Contains(status);
                            if (tsb.Text.Length > 1) tsb.Enabled = false;
                            if (position.ToUpper() == "MANAGER" && tsb.Text.ToUpper() == "LOCK")
                                tsb.Enabled = true;
                        }
                        else if (objTemp.GetType() == typeof(ToolStripMenuItem))
                        {
                            ToolStripMenuItem tsmi = (ToolStripMenuItem)objTemp;
                            XmlNode nodeTemp = (XmlNode)tsmi.Tag;
                            string sEnable = "" + nodeTemp.Attributes["S_Enable"].Value;
                            string sVisible = "" + nodeTemp.Attributes["S_Visible"].Value;
                            tsmi.Enabled = sEnable.Contains(status);
                            tsmi.Visible = sVisible.Contains(status);
                            if (tsmi.Text.Length > 1) tsmi.Enabled = false;
                            if (position.ToUpper() == "MANAGER" && tsmi.Text.ToUpper() == "LOCK")
                                tsmi.Enabled = true;
                        }
                        else if (objTemp.GetType() == typeof(ToolStripDropDownButton))
                        {
                            ToolStripDropDownButton tsmi = (ToolStripDropDownButton)objTemp;
                            tsmi.Enabled = true;
                            if (tsmi.Text.Length > 1) tsmi.Enabled = false;
                            if (position.ToUpper() == "MANAGER" && tsmi.Text.ToUpper() == "LOCK")
                                tsmi.Enabled = true;
                        }                       
                        else { }
                    }
                    catch { }
                }
            }
            catch { }
        }
        #region function Search
        int MaxPage;
        private string setPages(string SQL)
        {
            setPageSige();
            string H = @"WITH Entries AS (                        
		                 SELECT ROW_NUMBER() OVER (ORDER BY @TEMP_COLUMN asc) AS [Row],";
            string T = @")
		                    SELECT *
		                    FROM Entries
		                    WHERE Row between
		                    @startrow and @startrow + @pagesize";
            H = H.Replace("@TEMP_COLUMN", xmlconfig.SelectSingleNode("//Management/Table/Column").Attributes["Name"].Value.ToString());

            //SQL = SQL.ToUpper();

            string[] Te = SQL.Split(new string[] { "FROM", "From", "from" }, StringSplitOptions.None);
            string Text = Te[Te.Length - 1];
            string[] OrderBy = Text.Split(new string[] { "ORDER BY", "Order by", "Order By", "order by" }, StringSplitOptions.None);

            Text = @"set dateformat dmy; SELECT COUNT(*) FROM " + OrderBy[0];

            ConnectServer.cConection WebService = new ConnectServer.cConection(user);
            DataSet ds = WebService.Retreive(Text, strConnect);

            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                //MessageBox.Show("äÁèÁÕ¢éÍÁÙÅ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tslPage.Visible = false;
            }
            else
            {
                int sCount;
                try { sCount = int.Parse(ds.Tables[0].Rows[0][0].ToString()); }
                catch { sCount = 0; }

                MaxPage = sCount / PageSize;
                float Sum = (float)sCount / (float)PageSize;
                if (Sum - MaxPage > 0.0) MaxPage++;

                if (sCount > PageSize)
                {
                    tslPage.Visible = true;
                    tsbNext.Enabled = true;
                    tspPage.Visible = true;
                    string[] ORDERBY = SQL.Split(new string[] { "ORDER BY", "Order by", "Order By", "order by" }, StringSplitOptions.None);
                    string[] S = ORDERBY[0].Split(new string[] { "SELECT", "Select", "select" }, StringSplitOptions.None);
                    SQL = H;
                    for (int i = 1; i < S.Length; i++)
                    {
                        SQL = SQL + S[i];
                    }
                    SQL = SQL + T;
                    SQL = SQL.Replace("@startrow", StartPage.ToString());
                    SQL = SQL.Replace("@pagesize", PageSize.ToString());
                    if (ORDERBY.Length > 1) SQL += " ORDER BY " + ORDERBY[1];
                }
                else
                {
                    tspPage.Visible = false;
                }
            }
            return SQL;
        }

        bool bSearchClick = false;
        private void funSearch()
        {
            string SQL = xmlconfig.SelectSingleNode("//Management/Query/SQL[@ID='Main']").InnerText;
            SQL = SQL.Replace("@ProjectID", ProjectID);
            SQL = SQL.Replace("@UserLogin", user);
            SQL = filter(SQL);
            SQL = setPages(SQL);
            ConnectServer.cConection WebService = new ConnectServer.cConection(user);
            DataSet ds = WebService.Retreive(SQL, strConnect);
            if (ds == null)
            {
                tsBar.Enabled = false;
                grdManange.DataRows.Clear();
                return;
            }
            tsBar.Enabled = true;
            grdManange.DataSource = ds;
            grdManange.DataMember = "DATA";
            if (grdManange.DataRows.Count < PageSize) tsbNext.Enabled = false;
            setRowCount();
            setColor();
            bSearchClick = false;          
        }
        private void funSearch2()
        {
            string SQL = xmlconfig.SelectSingleNode("//Management/Query/SQL[@ID='Main']").InnerText;
            SQL = SQL.Replace("@ProjectID", ProjectID);
            SQL = SQL.Replace("@UserLogin", user);
            SQL = filter2(SQL);
            SQL = setPages(SQL);

            ConnectServer.cConection WebService = new ConnectServer.cConection(user);
            DataSet ds = WebService.Retreive(SQL, strConnect);
            if (ds == null)
            {
                MessageBox.Show("äÁèÊÒÁÒÃ¶µÔ´µèÍ°Ò¹¢éÍÁÙÅä´é", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            grdManange.DataSource = ds;
            grdManange.DataMember = "DATA";
            if (grdManange.DataRows.Count < PageSize) tsbNext.Enabled = false;
            setRowCount();
            setColor();
            bSearchClick = true;
        }
        private string filter2(string SQL)
        {
            string strFieldName = null;
            string ID = null;
            bool bCheck = true;
            string[] OrderBy = SQL.Split(new string[] { "ORDER BY", "Order by", "Order By", "order by" }, StringSplitOptions.None);

            SQL = OrderBy[0];
            for (int i = 0; i < SQL.Length; i++)
            {
                if (SQL.Length < i + 5) break;
                if (SQL.Substring(i, 5).ToString().ToUpper() == "WHERE")
                {
                    bCheck = false;
                    break;
                }
            }
            if (bCheck) SQL = SQL + " where 1=1 ";
            
            //try
            //{
            string strGroup = xmlconfig.SelectSingleNode("//Management/Query/SQL[@ID='Group']").Attributes["Name"].Value.ToString();
            if (strGroup != "")
            {
                if ("" + cbxGroupBy.SelectedValue != "") SQL = SQL + " and " + strGroup + " = '" + cbxGroupBy.SelectedValue + "'";
            }
            //}
            //catch { }

            //ID = cboFieldName1.Text.Split(new char[] { ' ' }).GetValue(0).ToString();
            //string sWhere = "";
            //for (int i = 1; i < cboFieldName1.Items.Count; i++)
            //{
            //    ID = cboFieldName1.Items[i].ToString().Split(new char[] { ' ' }).GetValue(0).ToString();
            //    strFieldName = xmlconfig.SelectSingleNode("Management/Searchs/Search[@ID='" + ID + "']").Attributes.GetNamedItem("FiledName").Value.ToString();
            //    if (sWhere == "") sWhere = strFieldName + " like '%" + txbWord.Text + "%'";
            //    else sWhere = sWhere + " OR " + strFieldName + " like '%" + txbWord.Text + "%'";
            //}
            //SQL = SQL + " AND (" + sWhere + ")";

            string sWhere = "";
            if (chbC1.Visible && chbC1.Checked) sWhere = chbC1.Tag + " like '%" + txbWord.Text + "%'";
            if (chbC2.Visible && chbC2.Checked)
            {
                if (sWhere == "") sWhere = chbC2.Tag + " like '%" + txbWord.Text + "%'";
                else sWhere = sWhere + " OR " + chbC2.Tag + " like '%" + txbWord.Text + "%'";
            }
            if (chbC3.Visible && chbC3.Checked)
            {
                if (sWhere == "") sWhere = chbC3.Tag + " like '%" + txbWord.Text + "%'";
                sWhere = sWhere + " OR " + chbC3.Tag + " like '%" + txbWord.Text + "%'";
            }
            if (chbC4.Visible && chbC4.Checked)
            {
                if (sWhere == "") sWhere = chbC4.Tag + " like '%" + txbWord.Text + "%'";
                else sWhere = sWhere + " OR " + chbC4.Tag + " like '%" + txbWord.Text + "%'";
            }
            if (chbC5.Visible && chbC5.Checked)
            {
                if (sWhere == "") sWhere = chbC5.Tag + " like '%" + txbWord.Text + "%'";
                else sWhere = sWhere + " OR " + chbC5.Tag + " like '%" + txbWord.Text + "%'";
            }
            if (sWhere != "") SQL = SQL + " AND (" + sWhere + ")";


            if (OrderBy.Length > 1) SQL = SQL + " Order By " + OrderBy[1];

            return SQL;
        }
        private void AddDataSource(ComboBox cbx, DataTable dt)
        {
            cbx.DataSource = dt;
            cbx.DisplayMember = "Text";
            cbx.ValueMember = "ID";
        }
        private string filter(string SQL)
        {
            string strFieldName = null;
            string ID = null;
            string Type = null;
            bool bCheck = true;
            string[] OrderBy = SQL.Split(new string[] { "ORDER BY", "Order by", "Order By", "order by" }, StringSplitOptions.None);

            SQL = OrderBy[0];
            for (int i = 0; i < SQL.Length; i++)
            {
                if (SQL.Length < i + 5) break;
                if (SQL.Substring(i, 5).ToString().ToUpper() == "WHERE")
                {
                    bCheck = false;
                    break;
                }
            }

            if (bCheck) SQL = SQL + " where 1=1 ";

           
            string strGroup = xmlconfig.SelectSingleNode("//Management/Query/SQL[@ID='Group']").Attributes["Name"].Value.ToString();
            if (strGroup != "")
            {
                if (""+cbxGroupBy.SelectedValue != "") SQL = SQL + " and " + strGroup + " = '" + cbxGroupBy.SelectedValue + "'";
            }
            bool ssssss = false;
            if (cboFieldName1.Text.Trim() != "")
            {
                ID = "" + cboFieldName1.SelectedValue;
                Type = xmlconfig.SelectSingleNode("Management/Searchs/Search[@ID='" + ID + "']").Attributes.GetNamedItem("Type").Value.ToString();
                strFieldName = xmlconfig.SelectSingleNode("Management/Searchs/Search[@ID='" + ID + "']").Attributes.GetNamedItem("FiledName").Value.ToString();
                string sValue = "";
                if (Type.ToUpper().Trim() == "DATE")
                {
                    DateTimePicker DTP = (DateTimePicker)tableLayoutPanel3.GetControlFromPosition(4, 1);
                    sValue = DTP.Value.ToString("dd/MM/yyyy", new CultureInfo("fr-FR"));
                }
                else
                {
                    TextBox TBX = (TextBox)tableLayoutPanel3.GetControlFromPosition(4, 1);
                    sValue = TBX.Text;
                }
                ssssss = true;
                SQL = SQL + " AND (" + strFieldName + " " + cboFilter1.Text + " '" + sValue + "'";
            }
            if (cboFieldName2.Text.Trim() != "")
            {
                ID = "" + cboFieldName2.SelectedValue;
                Type = xmlconfig.SelectSingleNode("Management/Searchs/Search[@ID='" + ID + "']").Attributes.GetNamedItem("Type").Value.ToString();
                strFieldName = xmlconfig.SelectSingleNode("Management/Searchs/Search[@ID='" + ID + "']").Attributes.GetNamedItem("FiledName").Value.ToString();
                string sValue = txtVal2.Text;
                if (Type.ToUpper().Trim() == "DATE")
                {
                    DateTimePicker DTP = (DateTimePicker)tableLayoutPanel3.GetControlFromPosition(8, 1);
                    sValue = DTP.Value.ToString("dd/MM/yyyy", new CultureInfo("fr-FR"));
                }
                else
                {
                    TextBox TBX = (TextBox)tableLayoutPanel3.GetControlFromPosition(8, 1);
                    sValue = TBX.Text;
                }
                SQL = " " + SQL + " " + cboOp1.Text + " " + strFieldName + " " + cboFilter2.Text + " '" + sValue + "'";
            }
            if (cboFieldName3.Text.Trim() != "")
            {
                ID = "" + cboFieldName3.SelectedValue;
                Type = xmlconfig.SelectSingleNode("Management/Searchs/Search[@ID='" + ID + "']").Attributes.GetNamedItem("Type").Value.ToString();
                strFieldName = xmlconfig.SelectSingleNode("Management/Searchs/Search[@ID='" + ID + "']").Attributes.GetNamedItem("FiledName").Value.ToString();
                string sValue = "";
                if (Type.ToUpper().Trim() == "DATE")
                {
                    DateTimePicker DTP = (DateTimePicker)tableLayoutPanel3.GetControlFromPosition(4, 2);
                    sValue = DTP.Value.ToString("dd/MM/yyyy", new CultureInfo("fr-FR"));
                }
                else
                {
                    TextBox TBX = (TextBox)tableLayoutPanel3.GetControlFromPosition(4, 2);
                    sValue = TBX.Text;
                }
                SQL = " " + SQL + " " + cboOp2.Text + " " + strFieldName + " " + cboFilter3.Text + " '" + sValue + "'";
            }
            if (cboFieldName4.Text.Trim() != "")
            {
                ID = "" + cboFieldName4.SelectedValue;
                Type = xmlconfig.SelectSingleNode("Management/Searchs/Search[@ID='" + ID + "']").Attributes.GetNamedItem("Type").Value.ToString();
                strFieldName = xmlconfig.SelectSingleNode("Management/Searchs/Search[@ID='" + ID + "']").Attributes.GetNamedItem("FiledName").Value.ToString();
                string sValue = "";
                if (Type.ToUpper().Trim() == "DATE")
                {
                    DateTimePicker DTP = (DateTimePicker)tableLayoutPanel3.GetControlFromPosition(8, 2);
                    sValue = DTP.Value.ToString("dd/MM/yyyy", new CultureInfo("fr-FR"));
                }
                else
                {
                    TextBox TBX = (TextBox)tableLayoutPanel3.GetControlFromPosition(8, 2);
                    sValue = TBX.Text;
                }
                SQL = " " + SQL + " " + cboOp3.Text + " " + strFieldName + " " + cboFilter4.Text + " '" + sValue + "'";
            }
            if (ssssss) SQL = SQL + ")";
            if (OrderBy.Length > 1) SQL = SQL + " Order By " + OrderBy[1];

            return SQL;
        }
        #endregion
        #region Set Data To Grid
        private void selectDataToGrid()
        {
            string SQL = xmlconfig.SelectSingleNode("//Management/Query/SQL[@ID='Main']").InnerText;
            SQL = SQL.Replace("@ProjectID", ProjectID);
            SQL = SQL.Replace("@UserLogin", user);
            SQL = setPages(SQL);

            ConnectServer.cConection WebService = new ConnectServer.cConection(user);
            DataSet ds = WebService.Retreive(SQL, strConnect);
            if (ds == null)
            {
                MessageBox.Show("äÁèÊÒÁÒÃ¶µÔ´µèÍ°Ò¹¢éÍÁÙÅä´éà¹×èÍ§¨Ò¡ ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            grdManange.DataSource = ds;
            grdManange.DataMember = "DATA";
            if (grdManange.DataRows.Count < PageSize) tsbNext.Enabled = false;
        }
        #endregion
        #region SET GRID
        int widthDefault = 30;
        private void setGrid()
        {
            XmlNodeList nodename = xmlconfig.SelectNodes("//Management/Table/Column");
            String cname = null;
            try { grdManange.Columns["Row"].Visible = false; }
            catch { }
            for (int i = 0; i < nodename.Count; i++)
            {
                cname = "" + nodename[i].Attributes.GetNamedItem("Name").Value;
                int width = stringToInt("" + nodename[i].Attributes.GetNamedItem("Width").Value);
                grdManange.Columns[cname].Width = width;
                if ("" + nodename[i].Attributes.GetNamedItem("Text").Value != "")
                {
                    grdManange.Columns[cname].Title = "" + nodename[i].Attributes.GetNamedItem("Text").Value;
                }
                if ("" + nodename[i].Attributes.GetNamedItem("Format").Value != "xxx")
                {
                    grdManange.Columns[cname].FormatSpecifier = "" + nodename[i].Attributes.GetNamedItem("Format").Value;
                }
                //grdManange.Columns[cname].Fixed = setVisible("" + nodename[i].Attributes.GetNamedItem("FixedColumn").Value);
                grdManange.Columns[cname].Visible = setVisible("" + nodename[i].Attributes.GetNamedItem("Visible").Value);
                grdManange.DataRowTemplate.Cells[cname].HorizontalAlignment = setHorizontal("" + nodename[i].Attributes.GetNamedItem("DataAlign").Value);
                grdManange.Columns[cname].HorizontalAlignment = setHorizontal("" + nodename[i].Attributes.GetNamedItem("ColumnAlign").Value);
            }
        }
        private int stringToInt(string str)
        {
            try
            {
                return int.Parse(str);
            }
            catch
            {
                return widthDefault;
            }
        }
        private Xceed.Grid.HorizontalAlignment setHorizontal(string str)
        {
            if (str == "C")
            {
                return Xceed.Grid.HorizontalAlignment.Center;
            }
            else if (str == "L")
            {
                return Xceed.Grid.HorizontalAlignment.Left;
            }
            else if (str == "R")
            {
                return Xceed.Grid.HorizontalAlignment.Right;
            }
            else
            {
                return Xceed.Grid.HorizontalAlignment.Default;
            }
        }
        private bool setVisible(string str)
        {
            if (str == "T")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool setReadOnly(string str)
        {
            if (str == "T")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region Set Filter
        private void setFilter()
        {
            XmlNodeList nodeSEARCH = xmlconfig.SelectNodes("//Management/Searchs/Search");
            cboFieldName1.Items.Clear();
            cboFieldName2.Items.Clear();
            cboFieldName3.Items.Clear();
            cboFieldName4.Items.Clear();
            for (int i = 0; i < nodeSEARCH.Count; i++)
            {
                if (i != 0)
                {
                    cboFieldName1.Items.Add(i + " " + nodeSEARCH[i].Attributes.GetNamedItem("Text").Value);
                    cboFieldName2.Items.Add(i + " " + nodeSEARCH[i].Attributes.GetNamedItem("Text").Value);
                    cboFieldName3.Items.Add(i + " " + nodeSEARCH[i].Attributes.GetNamedItem("Text").Value);
                    cboFieldName4.Items.Add(i + " " + nodeSEARCH[i].Attributes.GetNamedItem("Text").Value);
                }
                else
                {
                    cboFieldName1.Items.Add("");
                    cboFieldName2.Items.Add("");
                    cboFieldName3.Items.Add("");
                    cboFieldName4.Items.Add("");
                }
            }
        }
        #endregion
        #region Set Header
        private void setHeader()
        {
            Text = "" + xmlconfig.SelectSingleNode("//Management/Headers").Attributes.GetNamedItem("Title").Value;
            Header1.Text = "" + xmlconfig.SelectSingleNode("//Management/Headers/Header").Attributes.GetNamedItem("Value").Value;
        }

        #endregion
        #region Set DataRow Value
        private void createDataRowValue()
        {
            try
            {
                Xceed.Grid.DataRow dr = grdManange.CurrentRow as Xceed.Grid.DataRow;
                XmlNodeList nodeROW = xmlconfig.SelectNodes("//Management/DetailRow/Row");
                grdDetail.DataRows.Clear();
                string text = null;
                for (int i = 0; i < nodeROW.Count; i++)
                {
                    if (nodeROW[i].Attributes["Visible"].Value.ToString() == "T")
                    {
                        Xceed.Grid.DataRow row = grdDetail.DataRows.AddNew();
                        row.EndEdit();
                        text = "" + nodeROW[i].Attributes.GetNamedItem("Text").Value;
                        if (text == "")
                        {
                            try { row.Cells["column1"].Value = "" + nodeROW[i].Attributes.GetNamedItem("ColumnName").Value; }
                            catch { }
                        }
                        else
                        {
                            try { row.Cells["column1"].Value = text; }
                            catch { }
                        }
                        try
                        {
                            string ColumnName = "" + nodeROW[i].Attributes.GetNamedItem("ColumnName").Value;
                            row.Cells["column2"].Value = "" + dr.Cells[ColumnName].Value;
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }
        #endregion
        #region THEME
        public void loadTheme(XmlDocument xmlTheme)
        {
            int red, green, blue;

            #region Header
            XmlNode nodeHeader = xmlTheme.SelectSingleNode("//Theme/Header");
            if ("" + nodeHeader.SelectSingleNode("./Blackground").Attributes.GetNamedItem("Type").Value == "1")
            {
                string[] str = nodeHeader.SelectSingleNode("./Blackground").InnerText.Split(new char[] { ',' });
                red = int.Parse(str[0]);
                green = int.Parse(str[1]);
                blue = int.Parse(str[2]);
                panelHeader.BackColor = Color.FromArgb(red, green, blue);
            }
            else
            {
                panelHeader.BackColor = Color.FromName(nodeHeader.SelectSingleNode("./Blackground").InnerText);
            }
            if ("" + nodeHeader.SelectSingleNode("./Title").Attributes.GetNamedItem("Type").Value == "1")
            {
                string[] str = nodeHeader.SelectSingleNode("./Title").InnerText.Split(new char[] { ',' });
                red = int.Parse(str[0]);
                green = int.Parse(str[1]);
                blue = int.Parse(str[2]);
                Header1.ForeColor = Color.FromArgb(red, green, blue);
                labGroupBy.ForeColor = Color.FromArgb(red, green, blue);
            }
            else
            {
                Header1.ForeColor = Color.FromName(nodeHeader.SelectSingleNode("./Title").InnerText);
                labGroupBy.ForeColor = Color.FromName(nodeHeader.SelectSingleNode("./Title").InnerText);
            }
            #endregion

            #region Search
            XmlNode nodeSearch = xmlTheme.SelectSingleNode("//Theme/Search");
            if ("" + nodeSearch.SelectSingleNode("./Blackground").Attributes.GetNamedItem("Type").Value == "1")
            {
                string[] str = nodeSearch.SelectSingleNode("./Blackground").InnerText.Split(new char[] { ',' });
                red = int.Parse(str[0]);
                green = int.Parse(str[1]);
                blue = int.Parse(str[2]);
                //panelSearch.BackColor = Color.FromArgb(red, green, blue);
                stcSearch.SelectedTabColor = Color.FromArgb(red, green, blue);
            }
            else
            {
                //panelSearch.BackColor = Color.FromName("" + nodeSearch.SelectSingleNode("./Blackground").InnerText);
                stcSearch.SelectedTabColor = Color.FromName("" + nodeSearch.SelectSingleNode("./Blackground").InnerText);
            }
            #endregion

            #region Tool
            XmlNode nodeTool = xmlTheme.SelectSingleNode("//Theme/Tool/Blackground");
            if ("" + nodeTool.Attributes.GetNamedItem("Type").Value == "1")
            {
                string[] str = nodeTool.InnerText.Split(new char[] { ',' });
                red = int.Parse(str[0]);
                green = int.Parse(str[1]);
                blue = int.Parse(str[2]);
                tsBar.BackColor = Color.FromArgb(red, green, blue);
            }
            else
            {
                tsBar.BackColor = Color.FromName("" + nodeTool.InnerText);
            }
            #endregion

            #region Data
            XmlNode nodeData = xmlTheme.SelectSingleNode("//Theme/Data/Blackground");
            if ("" + nodeData.Attributes.GetNamedItem("Type").Value == "1")
            {
                string[] str = nodeData.InnerText.Split(new char[] { ',' });
                red = int.Parse(str[0]);
                green = int.Parse(str[1]);
                blue = int.Parse(str[2]);
                grdManange.RowSelectorPane.BackColor = Color.FromArgb(red, green, blue);
                columnManagerRow1.BackColor = Color.FromArgb(red, green, blue);
            }
            else
            {
                grdManange.RowSelectorPane.BackColor = Color.FromName("" + nodeData.InnerText);
                columnManagerRow1.BackColor = Color.FromName("" + nodeData.InnerText);
            }
            #endregion

            #region Detail
            XmlNode nodeDetail = xmlTheme.SelectSingleNode("//Theme/DetailData");
            if ("" + nodeDetail.SelectSingleNode("./Blackground").Attributes.GetNamedItem("Type").Value == "1")
            {
                string[] str = nodeDetail.SelectSingleNode("./Blackground").InnerText.Split(new char[] { ',' });
                red = int.Parse(str[0]);
                green = int.Parse(str[1]);
                blue = int.Parse(str[2]);
                grdDetail.RowSelectorPane.BackColor = Color.FromArgb(red, green, blue);
                columnManagerRow2.BackColor = Color.FromArgb(red, green, blue);

            }
            else
            {
                grdDetail.RowSelectorPane.BackColor = Color.FromName(nodeDetail.SelectSingleNode("./Blackground").InnerText);
                columnManagerRow2.BackColor = Color.FromName(nodeDetail.SelectSingleNode("./Blackground").InnerText);
            }
            if ("" + nodeDetail.SelectSingleNode("./Row1").Attributes.GetNamedItem("Type").Value == "1")
            {
                string[] str = nodeDetail.SelectSingleNode("./Row1").InnerText.Split(new char[] { ',' });
                red = int.Parse(str[0]);
                green = int.Parse(str[1]);
                blue = int.Parse(str[2]);
                dataRowStyle1.BackColor = Color.FromArgb(red, green, blue);
            }
            else
            {
                dataRowStyle1.BackColor = Color.FromName("" + nodeDetail.SelectSingleNode("./Row1").InnerText);
            }
            if ("" + nodeDetail.SelectSingleNode("./Row2").Attributes.GetNamedItem("Type").Value == "1")
            {
                string[] str = nodeDetail.SelectSingleNode("./Row2").InnerText.Split(new char[] { ',' });
                red = int.Parse(str[0]);
                green = int.Parse(str[1]);
                blue = int.Parse(str[2]);
                dataRowStyle2.BackColor = Color.FromArgb(red, green, blue);
            }
            else
            {
                dataRowStyle2.BackColor = Color.FromName("" + nodeDetail.SelectSingleNode("./Row2"));
            }
            #endregion

            #region Foot
            XmlNode nodeToolf = xmlTheme.SelectSingleNode("//Theme/Tool_Foot/Blackground");
            if ("" + nodeToolf.Attributes.GetNamedItem("Type").Value == "1")
            {
                string[] str = nodeToolf.InnerText.Split(new char[] { ',' });
                red = int.Parse(str[0]);
                green = int.Parse(str[1]);
                blue = int.Parse(str[2]);
                toolfoot.BackColor = Color.FromArgb(red, green, blue);
            }
            else
            {
                toolfoot.BackColor = Color.FromName("" + nodeToolf.InnerText);
            }
            #endregion

            
        }
        #endregion
        #region function Properities
        private bool getVisible(string str)
        {
            if (str == "T")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private ToolStripItemAlignment setAlignment(string align)
        {
            if (align == "R") return ToolStripItemAlignment.Right;
            return ToolStripItemAlignment.Left;
        }
        private Image setImage(string str)
        {
            if (str == "0" || str == "" || str == null) return null;
            if (xmlDetail == null) return null;
            return Image.FromFile(xmlDetail.SelectSingleNode("//Images/Image[@ID='" + str + "']").Attributes["Path"].Value.ToString());
        }
          #endregion
        #region Create ToolScript Items
        private void CreateToolScriptItems(XmlDocument xmlTools)
        {
            XmlNodeList nodeTool;
            if (switchCount == 0)
            {
                nodeTool = xmlTools.SelectNodes("//Item");
                switchCount++;
            }
            else
            {
                nodeTool = xmlTools.SelectNodes("//Tool/Item");
            }
            XmlNodeList nodeSubTool = null;

            string ch;
            for (int i = 0; i < nodeTool.Count; i++)
            {
                ch = "" + nodeTool[i].Attributes.GetNamedItem("Type").Value;
                if (ch == "SB")
                {
                    ToolStripButton tsb = new ToolStripButton();
                    tsb.Text = "" + nodeTool[i].Attributes.GetNamedItem("Text").Value;
                    tsb.ToolTipText = "" + nodeTool[i].Attributes.GetNamedItem("Tooltiptext").Value;
                    tsb.Visible = getVisible("" + nodeTool[i].Attributes.GetNamedItem("Visible").Value);
                    tsb.Tag = nodeTool[i];
                    tsb.Alignment = setAlignment("L");
                    tsb.Click += new EventHandler(tsb_Click);
                    tsb.Image = setImage("" + nodeTool[i].Attributes.GetNamedItem("Image").Value);
                    tsBar.Items.Add(tsb);
                    listButton.Add(tsb);
                }
                else if (ch == "SD")
                {
                    ToolStripDropDownButton tssb = new ToolStripDropDownButton();

                    nodeSubTool = nodeTool[i].SelectNodes("./Item");
                    for (int j = 0; j < nodeSubTool.Count; j++)
                    {
                        ToolStripMenuItem tsmi = new ToolStripMenuItem();
                        tsmi.Text = "" + nodeSubTool[j].Attributes.GetNamedItem("Text").Value;
                        tsmi.ToolTipText = "" + nodeSubTool[j].Attributes.GetNamedItem("Tooltiptext").Value;
                        tsmi.Image = setImage("" + nodeSubTool[j].Attributes.GetNamedItem("Image").Value);
                        tsmi.Click += new EventHandler(tsmi_Click);
                        tsmi.Tag = nodeSubTool[j];
                        tsmi.Visible = getVisible("" + nodeSubTool[j].Attributes.GetNamedItem("Visible").Value);
                        tssb.DropDown.Items.Add(tsmi);
                        listButton.Add(tsmi);
                    }
                    tssb.Image = setImage("" + nodeTool[i].Attributes.GetNamedItem("Image").Value);
                    tssb.Text = "" + nodeTool[i].Attributes.GetNamedItem("Text").Value;
                    tsBar.Items.Add(tssb);
                    listButton.Add(tssb);
                }
                else if (ch == "SS")
                {
                    ToolStripSeparator tss = new ToolStripSeparator();
                    tss.Alignment = setAlignment("L");
                    tsBar.Items.Add(tss);
                }
                else if (ch == "SL")
                {
                    ToolStripLabel tsl = new ToolStripLabel();
                    tsl.Alignment = setAlignment("L");
                    tsl.Text = "" + nodeTool[i].Attributes.GetNamedItem("Text").Value;
                    tsBar.Items.Add(tsl);
                }
                else { }
            }
        }
        #endregion
        #endregion
        #region Even
        private void cboFieldName1_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                string ID = "" + cboFieldName1.SelectedValue;
                string sType = xmlconfig.SelectSingleNode("Management/Searchs/Search[@ID='" + ID + "']").Attributes.GetNamedItem("Type").Value.ToString();
                if (sType.ToUpper().Trim() == "DATE")
                {
                    tableLayoutPanel3.Controls.Remove(tableLayoutPanel3.GetControlFromPosition(4, 1));
                    DateTimePicker DTM = new DateTimePicker();
                    DTM.Dock = DockStyle.Fill;
                    DTM.TabIndex = 24;
                    tableLayoutPanel3.Controls.Add(DTM, 4, 1);
                }
                else
                {
                    tableLayoutPanel3.Controls.Remove(tableLayoutPanel3.GetControlFromPosition(4, 1));
                    TextBox tbx = new TextBox();
                    tbx.KeyDown += new KeyEventHandler(txbWord_KeyDown_1);
                    tbx.BorderStyle = BorderStyle.FixedSingle;
                    tbx.Dock = DockStyle.Fill;
                    tbx.Name = "txtVal1";
                    tbx.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                    tbx.TabIndex = 24;
                    tableLayoutPanel3.Controls.Add(tbx, 4, 1);
                }
                if (cboFieldName1.Text != "")
                {
                    cboOp1.Enabled = true;
                    cboFilter1.SelectedIndex = 0;
                }
                else
                {
                    cboOp1.Enabled = false;
                    cboOp1.Text = "";
                    cboFilter1.Text = null;
                    txtVal1.Text = "";
                }
            }
            catch { }
        }
        private void cboOp1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboOp1.Text != "")
            {
                cboFieldName2.Enabled = true;
                cboFilter2.Enabled = true;
                tableLayoutPanel3.GetControlFromPosition(8, 1).Enabled = true;
            }
            else
            {
                cboFieldName2.Text = "";
                cboFieldName2.Enabled = false;
                cboFilter2.Text = null;
                cboFilter2.Enabled = false;
                txtVal2.Text = "";
                tableLayoutPanel3.GetControlFromPosition(8, 1).Enabled = false;
            }
        }
        private void cboFieldName2_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                string ID = "" + cboFieldName2.SelectedValue;
                string sType = xmlconfig.SelectSingleNode("Management/Searchs/Search[@ID='" + ID + "']").Attributes.GetNamedItem("Type").Value.ToString();
                if (sType.ToUpper().Trim() == "DATE")
                {
                    tableLayoutPanel3.Controls.Remove(tableLayoutPanel3.GetControlFromPosition(8, 1));
                    DateTimePicker DTM = new DateTimePicker();
                    DTM.Dock = DockStyle.Fill;
                    DTM.TabIndex = 24;
                    tableLayoutPanel3.Controls.Add(DTM, 8, 1);
                }
                else
                {
                    tableLayoutPanel3.Controls.Remove(tableLayoutPanel3.GetControlFromPosition(8, 1));
                    TextBox tbx = new TextBox();
                    tbx.KeyDown += new KeyEventHandler(txbWord_KeyDown_1);
                    tbx.BorderStyle = BorderStyle.FixedSingle;
                    tbx.Dock = DockStyle.Fill;
                    tbx.Name = "txtVal2";
                    tbx.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                    tbx.TabIndex = 24;
                    tableLayoutPanel3.Controls.Add(tbx, 8, 1);
                }
                if (cboFieldName2.Text != "")
                {
                    cboOp2.Enabled = true;
                    cboFilter2.SelectedIndex = 0;
                }
                else
                {
                    cboOp2.Text = "";
                    cboOp2.Enabled = false;
                    cboFilter2.Text = null;
                    txtVal2.Text = "";
                }
            }
            catch { }
        }
        private void cboOp2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboOp2.Text != "")
            {
                cboFilter3.Enabled = true;
                cboFieldName3.Enabled = true;
                tableLayoutPanel3.GetControlFromPosition(4, 2).Enabled = true;
            }
            else
            {
                cboFilter3.Text = null;
                cboFilter3.Enabled = false;
                cboFieldName3.Text = "";
                cboFieldName3.Enabled = false;
                txtVal3.Text = "";
                tableLayoutPanel3.GetControlFromPosition(4, 2).Enabled = false;
            }
        }
        private void cboFieldName3_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                string ID = "" + cboFieldName3.SelectedValue;
                string sType = xmlconfig.SelectSingleNode("Management/Searchs/Search[@ID='" + ID + "']").Attributes.GetNamedItem("Type").Value.ToString();
                if (sType.ToUpper().Trim() == "DATE")
                {
                    tableLayoutPanel3.Controls.Remove(tableLayoutPanel3.GetControlFromPosition(4, 2));
                    DateTimePicker DTM = new DateTimePicker();
                    DTM.Dock = DockStyle.Fill;
                    DTM.TabIndex = 24;
                    tableLayoutPanel3.Controls.Add(DTM, 4, 2);
                }
                else
                {
                    tableLayoutPanel3.Controls.Remove(tableLayoutPanel3.GetControlFromPosition(4, 2));
                    TextBox tbx = new TextBox();
                    tbx.KeyDown += new KeyEventHandler(txbWord_KeyDown_1);
                    tbx.BorderStyle = BorderStyle.FixedSingle;
                    tbx.Dock = DockStyle.Fill;
                    tbx.Name = "txtVal3";
                    tbx.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                    tbx.TabIndex = 24;
                    tableLayoutPanel3.Controls.Add(tbx, 4, 2);
                }
                if (cboFieldName3.Text != "")
                {
                    cboOp3.Enabled = true;
                    cboFilter3.Enabled = true;
                    tableLayoutPanel3.GetControlFromPosition(4, 2).Enabled = true;
                    cboFilter3.SelectedIndex = 0;
                }
                else
                {
                    cboOp3.Text = "";
                    cboOp3.Enabled = false;
                    cboFilter3.Text = null;
                    cboFilter3.Enabled = false;
                    txtVal3.Text = "";
                    tableLayoutPanel3.GetControlFromPosition(4, 2).Enabled = false;
                }
            }
            catch { }
        }
        private void cboOp3_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboOp3.Text != "")
            {
                cboFieldName4.Enabled = true;
                cboFilter4.Enabled = true;
                tableLayoutPanel3.GetControlFromPosition(8, 2).Enabled = true;
            }
            else
            {
                cboFieldName4.Text = "";
                cboFieldName4.Enabled = false;
                cboFilter4.Text = null;
                cboFilter4.Enabled = false;
                txtVal4.Text = "";
                tableLayoutPanel3.GetControlFromPosition(8, 2).Enabled = false;
            }
        }
        private void cboFieldName4_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                string ID = "" + cboFieldName4.SelectedValue;
                string sType = xmlconfig.SelectSingleNode("Management/Searchs/Search[@ID='" + ID + "']").Attributes.GetNamedItem("Type").Value.ToString();
                if (sType.ToUpper().Trim() == "DATE")
                {
                    tableLayoutPanel3.Controls.Remove(tableLayoutPanel3.GetControlFromPosition(8, 2));
                    DateTimePicker DTM = new DateTimePicker();
                    DTM.Dock = DockStyle.Fill;
                    DTM.TabIndex = 24;
                    tableLayoutPanel3.Controls.Add(DTM, 8, 2);
                }
                else
                {
                    tableLayoutPanel3.Controls.Remove(tableLayoutPanel3.GetControlFromPosition(8, 2));
                    TextBox tbx = new TextBox();
                    tbx.KeyDown += new KeyEventHandler(txbWord_KeyDown_1);
                    tbx.BorderStyle = BorderStyle.FixedSingle;
                    tbx.Dock = DockStyle.Fill;
                    tbx.Name = "txtVal4";
                    tbx.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                    tbx.TabIndex = 24;
                    tableLayoutPanel3.Controls.Add(tbx, 8, 2);
                }
                if (cboFieldName4.Text == "")
                {
                    cboFilter4.Text = null;
                    txtVal4.Text = "";
                }
                else
                {
                    cboFilter4.SelectedIndex = 0;
                }
            }
            catch { }
        }
        #region Page Size
      
        private void tsbNext_Click(object sender, EventArgs e)
        {
            Page++;
            StartPage = StartPage + PageSize;
            tslPage.Text = "Page " + Page + "/" + MaxPage;
            if (bSearchClick) funSearch2();
            else funSearch();
            tsbBack.Enabled = true;
        }
        private void tsbBack_Click(object sender, EventArgs e)
        {
            Page--;
            StartPage = StartPage - PageSize;
            tslPage.Text = "Page " + Page + "/" + MaxPage;
            if (bSearchClick) funSearch2();
            else funSearch();
            if (Page == 1) tsbBack.Enabled = false;
            tsbNext.Enabled = true;
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            //if (!LOCK)
            //    EnableButton();
            //funSearch();
        }
        #endregion

        bool bFirstRun = true;
        private void cbxGroupBy_SelectedValueChanged(object sender, EventArgs e)
        {            
            if (!bFirstRun)
            {
                if (bSearchClick) funSearch2();
                else funSearch();
                setGrid();
                tslPage.Text = "Page 1/" + MaxPage;                
            }          
        }
       
        private void grdManange_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (grdManange.DataRows.Count == 0) return;
            ToolCommand.csCommand cCom = new ToolCommand.csCommand("SQL");
            cCom._PanelView = panelView;
            DataSet ds = (DataSet)grdManange.DataSource;
            Xceed.Grid.DataRow dr;
            int current = 0;

            try
            {
                dr = grdManange.CurrentRow as Xceed.Grid.DataRow;
                current = dr.Index;
            }
            catch { }
            string ToolID = tsBar.Name;

            cCom.RowDoubleClick(ToolID, ds.Tables[0], current, strConnect, user, position);
        }
        private void funMountCilck(object sender, MouseEventArgs e)
        {
            if (grdManange.DataRows.Count == 0) return;
            ToolCommand.csCommand cCom = new ToolCommand.csCommand("SQL");
            cCom._PanelView = panelView;
            DataSet ds = (DataSet)grdManange.DataSource;
            Xceed.Grid.DataRow dr;
            int current = 0;

            try
            {
                dr = grdManange.CurrentRow as Xceed.Grid.DataRow;
                current = dr.Index;
            }
            catch { }
            string ToolID = tsBar.Name;

            cCom.RowClick(ToolID, ds.Tables[0], current, strConnect, user, position);
        }
        #region Even Click
        void tsmi_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            XmlNode node = tsmi.Tag as XmlNode;
            string str = "" + node.Attributes.GetNamedItem("Click").Value;
            if (str == "0" || str == "") return;
            else if (str == "F") firstB(grdManange);
            else if (str == "P") previousB(grdManange);
            else if (str == "N") nextB(grdManange);
            else if (str == "L") lastB(grdManange);
            else funClick(str);
        }
        void tsb_Click(object sender, EventArgs e)
        {
            ToolStripButton tsb = sender as ToolStripButton;
            XmlNode node = tsb.Tag as XmlNode;
            string str = "" + node.Attributes.GetNamedItem("Click").Value;
            if (str == "0" || str == "") return;
            else if (str == "F") firstB(grdManange);
            else if (str == "P") previousB(grdManange);
            else if (str == "N") nextB(grdManange);
            else if (str == "L") lastB(grdManange);
            else if (str == "S") funHideSearch(tsb);
            else if (str == "D") funHideDetailRow(tsb);
            else if (str == "V") funHideView(tsb);
            else if (str == "A") funACL();
            else funClick(str);
        }
           #endregion
        #region function Click

        private void funClick(string commandClick)
        {
            DataSet ds = (DataSet)grdManange.DataSource;
            Xceed.Grid.DataRow dr;
            int current = 0;

            try
            {
                dr = grdManange.CurrentRow as Xceed.Grid.DataRow;
                current = dr.Index;
            }
            catch { }

            string ToolID = tsBar.Name;

            DataTable dt = ds.Tables[0];
            int[] listSelectIndex = getIndex();

            ToolCommand.csCommand csCommand = new ToolCommand.csCommand("SQL");           
            csCommand._ProjectID = ProjectID;
            csCommand._gridTemp = grdManange;
            csCommand._PanelView = panelView;
            csCommand.funCommand(ToolID, commandClick, dt, listSelectIndex, current, strConnect, user, position, strParameter);
            if (bSearchClick) funSearch2();
            else funSearch();

            try { grdManange.SelectedRows[0].IsSelected = false; }
            catch { }
            for (int i = 0; i < listSelectIndex.Length; i++)
            {
                try
                {
                    grdManange.DataRows[listSelectIndex[i]].IsSelected = true;                  
                }
                catch { }
            }
            try
            {
                grdManange.CurrentRow = grdManange.DataRows[current];
                grdManange.DataRows[current].BringIntoView();
                csCommand.RowClick(ToolID, ds.Tables[0], current, strConnect, user, position);
                this.ParentForm.Activate();               
                funSelectRowChange();
            }
            catch { }

        }
        private int[] getIndex()
        {
            int count = grdManange.SelectedRows.Count;
            int[] listIndex = new int[count];
            for (int i = 0; i < grdManange.SelectedRows.Count; i++)
            {
                Xceed.Grid.DataRow dr = grdManange.SelectedRows[i] as Xceed.Grid.DataRow;
                listIndex[i] = dr.Index;
            }
            return listIndex;
        }
        #endregion
        #region function Move current
        private void funHideView(ToolStripButton tsb)
        {
            if (tsb.Checked)
            {
                tsb.Checked = false;
                splContainerMain.Panel2Collapsed = false;
            }
            else
            {
                tsb.Checked = true;
                splContainerMain.Panel2Collapsed = true;
            }
        }
        bool hideSearch = true;
        private void funHideSearch(ToolStripButton tsb)
        {
            if (hideSearch)
            {
                hideSearch = false;
                tsb.Checked = true;
            }
            else
            {
                hideSearch = true;
                tsb.Checked = false;
            }
            panelSearch.Visible = hideSearch;
        }
        bool hideDetailRow = false;
        private void funHideDetailRow(ToolStripButton tsb)
        {
            if (hideDetailRow)
            {
                hideDetailRow = false;
                tsb.Checked = false;
            }
            else
            {
                hideDetailRow = true;
                tsb.Checked = true;
            }
            splContainer.Panel2Collapsed = hideDetailRow;
        }
        #endregion
        #region Event Control Grid
        private ArrayList m_foundRows = new ArrayList();
        private void SelectDataRow(Xceed.Grid.DataRow dataRow, Xceed.Grid.GridControl grdControl)
        {
            dataRow.BringIntoView();
            grdControl.CurrentRow = dataRow;
            grdControl.SelectedRows.Clear();
            grdControl.SelectedRows.Add(dataRow);
        }

        private void firstB(Xceed.Grid.GridControl grdControl)
        {
            if (grdControl.DataRows.Count < 1) return;
            Xceed.Grid.Collections.ReadOnlyDataRowList dataRows = grdControl.GetSortedDataRows(true);

            Xceed.Grid.DataRow firstTaggedDataRow = dataRows[0];

            if (m_foundRows.Count > 0)
            {
                foreach (Xceed.Grid.DataRow dataRow in dataRows)
                {
                    if (m_foundRows.Contains(dataRow))
                    {
                        firstTaggedDataRow = dataRow;
                        break;
                    }
                }
            }
            this.SelectDataRow(firstTaggedDataRow, grdControl);
        }
        private void previousB(Xceed.Grid.GridControl grdControl)
        {
            if (grdControl.DataRows.Count < 1) return;
            Xceed.Grid.Collections.ReadOnlyDataRowList dataRows = grdControl.GetSortedDataRows(true);

            int currentIndex = 0;

            if (grdControl.CurrentRow is Xceed.Grid.DataRow)
                currentIndex = dataRows.IndexOf((Xceed.Grid.DataRow)grdControl.CurrentRow);

            Xceed.Grid.DataRow previousTaggedDataRow = dataRows[currentIndex];

            if (m_foundRows.Count > 0)
            {
                for (int i = currentIndex - 1; i >= 0; i--)
                {
                    if (m_foundRows.Contains(dataRows[i]))
                    {
                        previousTaggedDataRow = dataRows[i];
                        break;
                    }
                }
            }
            else
            {
                if (currentIndex > 0) previousTaggedDataRow = dataRows[currentIndex - 1];
            }
            this.SelectDataRow(previousTaggedDataRow, grdControl);
        }
        private void nextB(Xceed.Grid.GridControl grdControl)
        {
            if (grdControl.DataRows.Count < 1) return;
            Xceed.Grid.Collections.ReadOnlyDataRowList dataRows = grdControl.GetSortedDataRows(true);

            int currentIndex = 0;

            if (grdControl.CurrentRow is Xceed.Grid.DataRow)
                currentIndex = dataRows.IndexOf((Xceed.Grid.DataRow)grdControl.CurrentRow);

            Xceed.Grid.DataRow nextTaggedDataRow = dataRows[currentIndex];

            if (m_foundRows.Count > 0)
            {
                for (int i = currentIndex + 1; i < dataRows.Count; i++)
                {
                    if (m_foundRows.Contains(dataRows[i]))
                    {
                        nextTaggedDataRow = dataRows[i];
                        break;
                    }
                }
            }
            else
            {
                if (currentIndex < dataRows.Count - 1)
                    nextTaggedDataRow = dataRows[currentIndex + 1];
            }
            this.SelectDataRow(nextTaggedDataRow, grdControl);
        }
        private void lastB(Xceed.Grid.GridControl grdControl)
        {
            if (grdControl.DataRows.Count < 1) return;
            Xceed.Grid.Collections.ReadOnlyDataRowList dataRows = grdControl.GetSortedDataRows(true);
            int rowCount = dataRows.Count;
            Xceed.Grid.DataRow lastTaggedDataRow = dataRows[rowCount - 1];
            if (m_foundRows.Count > 0)
            {
                for (int i = rowCount - 1; i >= 0; i--)
                {
                    if (m_foundRows.Contains(dataRows[i]))
                    {
                        lastTaggedDataRow = dataRows[i];
                        break;
                    }
                }
            }
            this.SelectDataRow(lastTaggedDataRow, grdControl);
        }
        #endregion
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt;
            try
            {
                dt = (DataTable)grdManange.DataSource;
            }
            catch { dt = ((DataView)grdManange.DataSource).Table; }
            Xceed.Grid.DataRow dr;
            int current = 0;

            try
            {
                dr = grdManange.CurrentRow as Xceed.Grid.DataRow;
                current = dr.Index;
            }
            catch { }

            string ToolID = tsBar.Name;


            int[] listSelectIndex = getIndex();

            funSearch();

            try { grdManange.SelectedRows[0].IsSelected = false; }
            catch { }
            for (int i = 0; i < listSelectIndex.Length; i++)
            {
                try
                {
                    grdManange.DataRows[listSelectIndex[i]].IsSelected = true;
                }
                catch { }
            }
            try
            {
                grdManange.CurrentRow = grdManange.DataRows[current];
                grdManange.DataRows[current].BringIntoView();
                this.ParentForm.Activate();
                funSelectRowChange();
            }
            catch { }
            tslPage.Text = "Page 1/" + MaxPage;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tstb_time.Text = "วันที่ : " + DateTime.Now.ToLongDateString() + "เวลา : " + DateTime.Now.ToLongTimeString();
        }

        private void cmsiFrom_Opening(object sender, CancelEventArgs e)
        {
            if (frmParent == null) cmsiFrom.Enabled = false;
            else cmsiFrom.Enabled = true;
        }

        private void tsmiExport_Click(object sender, EventArgs e)
        {
            if (this.ParentForm.MdiParent != null)
            {
                this.ParentForm.MdiParent = null;
                tsmiExport.Text = "Add Form Parent";
                tsmiExport.ForeColor = Color.Red;
            }
            else
            {
                this.ParentForm.MdiParent = frmParent;
                tsmiExport.Text = "Clear Form Parent";
                tsmiExport.ForeColor = Color.Blue;
            }
        }


        private void grdManange_SelectedRowsChanged(object sender, EventArgs e)
        {
            funSelectRowChange();
        }
        private void grdManange_MouseClick(object sender, MouseEventArgs e)
        {
            funSelectRowChange();
            funMountCilck(sender, e);
        }
        #endregion    

        private void btnClose_Click(object sender, EventArgs e)
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

        private void btnSearchWord_Click(object sender, EventArgs e)
        {
            DataTable dt;
            try
            {
                dt = (DataTable)grdManange.DataSource;
            }
            catch { dt = ((DataView)grdManange.DataSource).Table; }
            Xceed.Grid.DataRow dr;
            int current = 0;

            try
            {
                dr = grdManange.CurrentRow as Xceed.Grid.DataRow;
                current = dr.Index;
            }
            catch { }

            string ToolID = tsBar.Name;


            int[] listSelectIndex = getIndex();

            funSearch2();

            try { grdManange.SelectedRows[0].IsSelected = false; }
            catch { }
            for (int i = 0; i < listSelectIndex.Length; i++)
            {
                try
                {
                    grdManange.DataRows[listSelectIndex[i]].IsSelected = true;
                }
                catch { }
            }
            try
            {
                grdManange.CurrentRow = grdManange.DataRows[current];
                grdManange.DataRows[current].BringIntoView();
                this.ParentForm.Activate();
                funSelectRowChange();
            }
            catch { }
            tslPage.Text = "Page 1/" + MaxPage;
        }
        private void txbWord_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) funSearch();
        }
        private void txbWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) funSearch2();
        }
    
    }
}