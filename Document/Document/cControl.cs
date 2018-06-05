using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Data;
namespace Document
{
    class cControl
    {
        public DataTable cbxDocumentType()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(Config.DConfig());           

            XmlNodeList listItem = xmlDoc.SelectNodes("//Config[@ID='1']/Item");

            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Value");

            for (int i = 0; i < listItem.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr.BeginEdit();
                dr["Name"] = listItem[i].Attributes["Name"].Value;
                dr["Value"] = listItem[i].Attributes["Value"].Value;
                dr.EndEdit();
                dt.Rows.Add(dr);
            }
           
            return dt;
        }

        public DataTable cbxItemType()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(Config.DConfig());

            XmlNodeList listItem = xmlDoc.SelectNodes("//Config[@ID='2']/Item");

            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Value");

            for (int i = 0; i < listItem.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr.BeginEdit();
                dr["Name"] = listItem[i].Attributes["Name"].Value;
                dr["Value"] = listItem[i].Attributes["Value"].Value;
                dr.EndEdit();
                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}
