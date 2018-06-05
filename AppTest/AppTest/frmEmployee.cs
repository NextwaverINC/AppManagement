using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace AppTest
{
    public partial class frmEmployee : Form
    {
        public string Command;
        XmlDocument xDoc;
        public string ID_TEMP;

        public frmEmployee()
        {
            InitializeComponent();
        }

        public void funLoad()
        {
            xDoc = new XmlDocument();
            if (Command == "New")
            {
                xDoc.Load(Application.StartupPath + "/TempDoc/Emp.xml");
            }
            else
            {
                ConnectServer.cConection cConn = new ConnectServer.cConection(cMain.UserName);
                string strDoc = cConn.getLastDoc(cMain.Connection, cMain.OfficeSpaceId, cMain.DatabaseName, "emp_job", int.Parse(ID_TEMP));
                xDoc.LoadXml(strDoc);
            }
            string RootPath = "//Document/Data/Section[@ID='1']/Items[@Name='Data']";
            ucTxbName._XPath = RootPath + "/Item[@Name='Name']";
            ucTxbPosition._XPath = RootPath + "/Item[@Name='Position']";
            ucNDSalary._XPath = RootPath + "/Item[@Name='Salary']";
            picImage._XPath = RootPath + "/Item[@Name='Image']";
            ucGridWork._XPath = "//Document/Data/Section[@ID='2']/Items[@Name='Data']";

            ucMappingControls1._XmlDocument = xDoc;
            ucMappingControls1.AddControl(ucTxbName);
            ucMappingControls1.AddControl(ucTxbPosition);
            ucMappingControls1.AddControl(ucNDSalary);
            ucMappingControls1.AddControl(picImage);
            ucMappingControls1.AddControl(ucGridWork);

        }

        private void funSave()
        {
            if (ucMappingControls1.funSaveXml())
            {
                string strDoc = xDoc.OuterXml;
                NextwaverDB.NColumns NCS = new NextwaverDB.NColumns();
                NCS.Add(new NextwaverDB.NColumn("NAME", ucTxbName._Value));
                NCS.Add(new NextwaverDB.NColumn("CREATE_BY", cMain.UserName));
                NCS.Add(new NextwaverDB.NColumn("CREATE_DATE", DateTime.Now.ToString("dd-mm-yyyy")));
                ConnectServer.cConection cConn = new ConnectServer.cConection(cMain.UserName);
                if (Command == "New")
                {
                    if (cConn.InsertData(cMain.Connection, cMain.OfficeSpaceId, cMain.DatabaseName, "emp_job", NCS, strDoc))
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก :" + cConn.ErrorMsg());
                    }
                }
                else
                {
                    NextwaverDB.NWheres NWS = new NextwaverDB.NWheres();
                    NWS.Add(new NextwaverDB.NWhere("ID", ID_TEMP));

                    if (cConn.UpdateData(cMain.Connection, cMain.OfficeSpaceId, cMain.DatabaseName
                        , "emp_job", NCS, NWS, strDoc))
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก :" + cConn.ErrorMsg());
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            funSave();
        }
    }
}
