using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
namespace Book
{
    public class cMain
    {
        public static string Connection;
        public static string OfficeSpaceId;
        public static string UserName;

        public bool funEditBook(string iConnection, string iUserName, string iOfficeSpaceId, DataTable dt, int currentIndex)
        {
            Connection = iConnection;
            OfficeSpaceId = iOfficeSpaceId;
            UserName = iUserName;
            if (dt.Rows.Count == 0) return false;

            string BookName = "" + dt.Rows[currentIndex]["BOOKNAME"];
            string BookType = "" + dt.Rows[currentIndex]["BOOKTYPE"];
            string BookPrice = "" + dt.Rows[currentIndex]["BOOKPRICE"];
            string ID = "" + dt.Rows[currentIndex]["ID"];

            frmBook frm = new frmBook();
            frm.Text = "แก้ไขข้อมูลหนังสือ";
            frm.Command = "Edit";
            frm.ID = ID;
            frm.txbBookName.Text = BookName;
            frm.txbBookType.Text = BookType;
            frm.txbPrice.Text = BookPrice;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (!frm.IsDisposed)
                {
                    frm.Close();
                    frm.Dispose();
                }
                return true;
            }
            else
            {
                if (!frm.IsDisposed)
                {
                    frm.Close();
                    frm.Dispose();
                }
                return false;
            }
            return false;
        }
        public bool funNewBook(string iConnection, string iUserName, string iOfficeSpaceId)
        {
            Connection = iConnection;
            OfficeSpaceId = iOfficeSpaceId;
            UserName = iUserName;

            frmBook frm = new frmBook();
            frm.Command = "New";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (!frm.IsDisposed)
                {
                    frm.Close();
                    frm.Dispose();
                }
                return true;
            }
            else
            {
                if (!frm.IsDisposed)
                {
                    frm.Close();
                    frm.Dispose();
                }
                return false;
            }
        }
    }
}
