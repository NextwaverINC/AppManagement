using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book
{
    public partial class frmBook : Form
    {
        public string ID;
        public string Command;

        public frmBook()
        {
            InitializeComponent();
        }

        private bool funValidation()
        {
            if (txbBookName.Text == "")
            {
                MessageBox.Show("โปรดระบุชื่อหนังสือ");
                txbBookName.Focus();
                return false;
            }
            if (txbBookType.Text == "")
            {
                MessageBox.Show("โปรดระบุประเภทหนังสือ");
                txbBookType.Focus();
                return false;
            }
            if (txbPrice.Text == "")
            {
                MessageBox.Show("โปรดระบุราคา");
                txbPrice.Focus();
                return false;
            }

            return true;
        }
        private void funSave()
        {
            string BookName = txbBookName.Text;
            string BookType = txbBookType.Text;
            string BookPrice = txbPrice.Text;

            ConnectServer.cConection cConn = new ConnectServer.cConection(cMain.UserName);
            NextwaverDB.NColumns NCS = new NextwaverDB.NColumns();
            NCS.Add(new NextwaverDB.NColumn("BOOKNAME", BookName));
            NCS.Add(new NextwaverDB.NColumn("BOOKTYPE", BookType));
            NCS.Add(new NextwaverDB.NColumn("BOOKPRICE", BookPrice));

            if (Command == "New")
            {
                if (cConn.InsertData(cMain.Connection, cMain.OfficeSpaceId, "Test", "Book", NCS))
                {
                    MessageBox.Show("เพิ่มข้อมูลเรียบร้อยแล้ว");
                    this.DialogResult = DialogResult.OK;
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("ไม่สามารถเพิ่มข้อมูลได้เนื่องจาก :" + cConn.ErrorMsg());
                }
            }
            else
            {
                NextwaverDB.NWheres NWS = new NextwaverDB.NWheres();
                NWS.Add(new NextwaverDB.NWhere("ID", ID));

                if (cConn.UpdateData(cMain.Connection, cMain.OfficeSpaceId, "Test", "Book", NCS, NWS))
                {
                    MessageBox.Show("แก้ไขข้อมูลเรียบร้อยแล้ว");
                    this.DialogResult = DialogResult.OK;
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("ไม่สามารถแก้ไขข้อมูลได้เนื่องจาก :" + cConn.ErrorMsg());
                }

            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (funValidation())
                funSave();
        }
    }
}
