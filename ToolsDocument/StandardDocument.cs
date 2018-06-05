using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ToolsDocument
{
    public class StandardDocument
    {
        XmlDocument xmlSDoc;
        public StandardDocument(XmlDocument xmlDoc)
        {
            xmlSDoc = xmlDoc;
        }

        public bool GetNormalValue(string XPath,ref string SValue)
        {
            XmlNode nodeTemp = xmlSDoc.SelectSingleNode(XPath);
            if (nodeTemp == null)
            {
                SValue = "XPath ไม่ถูกต้องโปรดระบุใหม่";
                return false;
            }
            try
            {
                SValue = "" + nodeTemp.Attributes["Value"].Value;
                return true;
            }
            catch
            {
                SValue = "ไม่มี Attibulte Value ใน XPath ที่ส่งมา";
                return false;
            }
        }
    }
}
