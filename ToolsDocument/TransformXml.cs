using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Collections;
using System.Data.SqlClient;

namespace ToolsDocument
{
    class TransformXml
    {
        XmlDocument XmlData = new XmlDocument();
        XmlDocument XmlConfig = new XmlDocument();
        ArrayList Sqls = new ArrayList();
        private string MappingXpath = "//TransformXml/Mapping";
        public TransformXml(XmlDocument data, XmlDocument config)
        {
            this.XmlData = data;
            this.XmlConfig = config;
        }
        private string getValue(string id, string xpath, string attribute, string datatype)
        {
            string value = "";
            string logfile = "C:\\ProcessLog.txt";
            string logDate = DateTime.Now.ToString("dd/MM/yyyy H:mm:ss");
            using (StreamWriter sw = new StreamWriter(logfile))
            {
                sw.AutoFlush = true;
                try
                {
                    if (xpath == "") return "''";

                    string dType = "";
                    XmlNode node = XmlData.SelectSingleNode(xpath);

                    if (node != null)
                    {
                        if (datatype.Trim() != "")
                        {
                            value = node.Attributes[attribute].Value.Trim();
                            if (datatype.Trim().IndexOf("/") > -1)
                            {
                                string[] type = datatype.Trim().Split(new char[] { '/' });
                                dType = type[0].Trim();
                            }
                            else
                            {
                                dType = datatype.Trim();
                            }

                            if (dType.ToUpper() == "VARCHAR" || dType.ToUpper() == "NVARCHAR" || dType.ToUpper() == "CHAR")
                            {
                                value = "'" + value + "'";
                            }
                            else if (dType.ToUpper() == "DATETIME")
                            {
                                DateTime dt;
                                if (DateTime.TryParse(value, new CultureInfo("fr-FR"), DateTimeStyles.None, out dt))
                                {
                                    value = "'" + dt.ToString("dd/MM/yyyy", new CultureInfo("fr-FR")) + "'";
                                }
                                else
                                {
                                    value = "NULL";
                                }
                            }
                            else if (dType.ToUpper() == "DECIMAL")
                            {
                                double d = 0.00;
                                if (double.TryParse(value, out d))
                                {
                                    value = d.ToString("0.00");
                                }
                                else
                                {
                                    value = "0";
                                }
                            }
                        }
                    }
                    else
                    {
                        if (datatype.Trim() != "")
                        {
                            if (datatype.Trim().IndexOf("/") > -1)
                            {
                                string[] type = datatype.Trim().Split(new char[] { '/' });
                                dType = type[0].Trim();
                            }
                            else
                            {
                                dType = datatype.Trim();
                            }
                            if (dType.ToUpper() == "VARCHAR" || dType.ToUpper() == "NVARCHAR" || dType.ToUpper() == "CHAR")
                            {
                                value = "''";
                            }
                            else if (dType.ToUpper() == "DATETIME")
                            {
                                value = "NULL";
                            }
                            else if (dType.ToUpper() == "DECIMAL")
                            {
                                value = "0";
                            }
                            else if (dType.ToUpper() == "XML")
                            {
                                value = "0";
                            }
                        }
                        //sw.WriteLine(logDate + "      ID= " + id + " Error is 'xpath or attribute is wrong'");
                    }
                }
                catch (Exception ex)
                {
                    sw.WriteLine(logDate + "      ID= " + id + " Error is '" + ex.Message + "'" + xpath);
                }
            }
            return value;
        }

        private string getValue(string value, string datatype)
        {
            string dType = "";
            if (datatype.Trim() != "")
            {
                if (datatype.Trim().IndexOf("/") > -1)
                {
                    string[] type = datatype.Trim().Split(new char[] { '/' });
                    dType = type[0].Trim();
                }
                else
                {
                    dType = datatype.Trim();
                }

                if (dType.ToUpper() == "VARCHAR" || dType.ToUpper() == "NVARCHAR" || dType.ToUpper() == "CHAR")
                {
                    value = "'" + value + "'";
                }
                else if (dType.ToUpper() == "DATETIME")
                {
                    DateTime dt;
                    if (DateTime.TryParse(value, new CultureInfo("fr-FR"), DateTimeStyles.None, out dt))
                    {
                        value = "'" + dt.ToString("dd/MM/yyyy", new CultureInfo("fr-FR")) + "'";
                    }
                    else
                    {
                        value = "NULL";
                    }
                }
                else if (dType.ToUpper() == "DECIMAL")
                {
                    double d = 0.00;
                    if (double.TryParse(value, out d))
                    {
                        value = d.ToString("0.00");
                    }
                    else
                    {
                        value = "0";
                    }
                }
            }
            return value;
        }
        /// <summary>
        /// 1=INSERT,2=UPDATE,3=DELETE
        /// </summary> cg
        /// <param name="command"></param>
        private void GenerateSql(string command)
        {
            if (Sqls.Count > 0) Sqls.Clear();
            XmlNode mapping_node = XmlConfig.SelectSingleNode(MappingXpath);
            if (mapping_node != null)
            {
                string mapping_id = mapping_node.Attributes["ID"].Value.Trim();
                XmlNode sql_node = XmlConfig.SelectSingleNode("//TransformXml/SQL/Item[@ID='" + command + "']");
                string defual_sql = sql_node.InnerText.Trim();
                string sql = "";
                switch (mapping_id)
                {
                    case "1":
                        sql = defual_sql;
                        foreach (XmlNode n in mapping_node.ChildNodes)
                        {
                            string id = n.Attributes["ID"].Value.Trim();
                            string xpath = n.Attributes["XPATH"].Value.Trim();
                            string attribute = n.Attributes["ATTNAME"].Value.Trim();
                            string datatype = n.Attributes["DATATYPE"].Value.Trim();
                            string column = n.InnerText.Trim();
                            if (sql.Contains(column))
                            {
                                string value = getValue(id, xpath, attribute, datatype);
                                sql = sql.Replace(column, value);
                            }
                        }
                        Sqls.Add(sql);
                        break;
                    case "2":
                        string seq_xpath = mapping_node.Attributes["XPATH"].Value.Trim() + "/Item";
                        XmlNodeList seq_node = XmlData.SelectNodes(seq_xpath);
                        foreach (XmlNode n in seq_node)
                        {
                            sql = defual_sql;
                            foreach (XmlNode m in mapping_node.ChildNodes)
                            {
                                string id = m.Attributes["ID"].Value.Trim();
                                string xpath = m.Attributes["XPATH"].Value.Trim();
                                string attribute = m.Attributes["ATTNAME"].Value.Trim();
                                string datatype = m.Attributes["DATATYPE"].Value.Trim();
                                string column = m.InnerText.Trim();
                                if (sql.Contains(column))
                                {
                                    if (!string.IsNullOrEmpty(xpath))
                                    {
                                        string value = getValue(id, xpath, attribute, datatype);
                                        sql = sql.Replace(column, value);
                                    }
                                    else
                                    {
                                        string value = n.Attributes[attribute].Value.Trim();
                                        value = getValue(value, datatype);
                                        sql = sql.Replace(column, value);
                                    }
                                }
                            }
                            Sqls.Add(sql);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 1=INSERT,2=UPDATE,3=DELETE
        /// </summary> cg
        /// <param name="command"></param>
        public ArrayList getSql(string command)
        {
            GenerateSql(command);
            return Sqls;
        }

        public bool Start(string command, string connection)
        {
            GenerateSql(command);
            return ExecuteSqlTransaction(Sqls, connection);
        }

        private bool ExecuteSqlTransaction(ArrayList sql, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("SampleTransaction");

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    foreach (object obj in sql)
                    {
                        command.CommandText = obj.ToString();
                        command.ExecuteNonQuery();
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    //Console.WriteLine("Both records are written to database.");

                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    //Console.WriteLine("  Message: {0}", ex.Message);

                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        //Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        //Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                    return false;
                }
            }
            return true;
        }

    }
}
