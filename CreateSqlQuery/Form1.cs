using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CreateSqlQuery
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adp;
        DataTable dt;
        bool keyCtrl;
        bool keyA;

        public Form1()
        {
            InitializeComponent();
            conn = new SqlConnection();
            cmd = new SqlCommand();
            cmd.Connection = conn;
            adp = new SqlDataAdapter();
            adp.SelectCommand = cmd;
            dt = new DataTable();
            txtTableName.Focus();
        }

        private void btnCreateSqlQuery_Click(object sender, EventArgs e)
        {
            if (txtSqlConnectionString.Text.Trim() == "") { MessageBox.Show("Fill ConnectionString!"); return; }
            if (txtTableName.Text.Trim() == "") { MessageBox.Show("Fill Table Name!"); return; }

            dt.Clear();
            conn.ConnectionString = txtSqlConnectionString.Text;
            conn.Open();
            string sql = "select column_Name, data_type, Is_Nullable from INFORMATION_SCHEMA.COLUMNS where table_name=@TableName";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@TableName", SqlDbType.VarChar).Value = txtTableName.Text;
            cmd.CommandText = sql;
            adp.Fill(dt);

            string method = CreateMethod(dt);

            string quote = "\"";
            string query = "using(DbManager dbManager = new DbManager())" + Environment.NewLine + "{" + Environment.NewLine;

            #region Insert Into
            if (radioInsert.Checked)
            {
                query += "string sql =@" + quote + "Insert Into " + txtTableName.Text + "(";

                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    string columnName = dt.Rows[i][0].ToString();
                    query = query + columnName + ", ";
                }
                query = query.Remove(query.Length - 2);
                query += ") " + Environment.NewLine + "Values(";
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    string columnName = dt.Rows[i][0].ToString();
                    query = query + "@" + columnName + ", ";
                }
                query = query.Remove(query.Length - 2);
                query += ")" + quote + ";";
                query += Environment.NewLine;
                query += "dbManager.BeginTransaction();";
                query = GetInsertOrUpdate(query);
            }
            #endregion

            #region Update
            else if (radioUpdate.Checked)
            {
                query += "string sql =@" + quote + "Update " + txtTableName.Text + " set ";
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    string columnName = dt.Rows[i][0].ToString();
                    query = query + columnName + "=@" + columnName + ", ";
                }
                query = query.Remove(query.Length - 2);
                query += Environment.NewLine;
                query += "where " + dt.Rows[0][0].ToString() + "=@" + dt.Rows[0][0].ToString() + quote + ";";
                query += Environment.NewLine;
                query += "dbManager.BeginTransaction();";
                query = GetInsertOrUpdate(query);
                query += Environment.NewLine;
                query += "dbManager.DataCommand.Parameters.Add('@" + dt.Rows[0][0].ToString()
                    + "', SqlDbType." + GetDataType(dt.Rows[0][1].ToString()) + ").Value = ";

                if (checkIsObject.Checked)
                    query += txtTableName.Text[0].ToString().ToLower() + txtTableName.Text.Substring(1) + ".";

                string value = dt.Rows[0][0].ToString()[0].ToString().ToLower() + dt.Rows[0][0].ToString().Substring(1) + ";";
                value = ReplaceTurkishCharacter(value);
                query += value;
            }
            #endregion

            #region Delete
            else if (radioDelete.Checked)
            {
                query += "string sql =@" + quote + "Delete From " + txtTableName.Text + " where " + dt.Rows[0][0].ToString() + "=@" + dt.Rows[0][0].ToString() + quote + ";";
                query += Environment.NewLine;
                query += "dbManager.BeginTransaction();" + Environment.NewLine;
                query += "dbManager.DataCommand.Parameters.Add('@" + dt.Rows[0][0].ToString()
                  + "', SqlDbType." + GetDataType(dt.Rows[0][1].ToString()) + ").Value = ";

                if (checkIsObject.Checked)
                    query += txtTableName.Text[0].ToString().ToLower() + txtTableName.Text.Substring(1) + ".";

                string columnName = dt.Rows[0][0].ToString()[0].ToString().ToLower() + dt.Rows[0][0].ToString().Substring(1) + ";";
                columnName = ReplaceTurkishCharacter(columnName);
                query += columnName;
            }
            #endregion
            query += Environment.NewLine;
            query += "dbManager.RunCommand(sql);" + Environment.NewLine;
            query += "return dbManager.CommitTransaction();" + Environment.NewLine;
            query += "}";
            query = query.Replace("'", quote);
            txtSqlQuery.Text = method + query + Environment.NewLine + "}";
            conn.Close();
            txtSqlQuery.SelectAll();
        }

        private string CreateMethod(DataTable dt)
        {
            string tableNameFirstCharLower = txtTableName.Text.Substring(0, 1).ToLower() + txtTableName.Text.Substring(1);
            string tableNameFirstCharUpper = txtTableName.Text.Substring(0, 1).ToUpper() + txtTableName.Text.Substring(1);

            string method = "public static bool ";
            if (radioDelete.Checked) method += "Delete";
            else if (radioInsert.Checked) method += "Add";
            else method += "Update";

            method += tableNameFirstCharUpper.Remove(0, 3) + "(";

            if (checkIsObject.Checked) method += tableNameFirstCharUpper + " " + tableNameFirstCharLower;
            else
            {
                if (!radioDelete.Checked)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (radioInsert.Checked && i == 0) continue;
                        string columnName = dt.Rows[i][0].ToString();
                        columnName = columnName.Substring(0, 1).ToLower() + columnName.Substring(1);
                        string dataType = dt.Rows[i][1].ToString();
                        string strIsNullable = dt.Rows[i][2].ToString();
                        bool isNullable = strIsNullable == "YES" ? true : false;
                        dataType = GetDataType(dataType, true);
                        method += dataType;
                        if (isNullable)
                        {
                            if (dataType != "string") method += "? ";
                            else method += " ";
                        }
                        else method += " ";

                        columnName = ReplaceTurkishCharacter(columnName);

                        method += columnName + ", ";
                    }
                }
                else
                {
                    string columnName = dt.Rows[0][0].ToString();
                    columnName = columnName.Substring(0, 1).ToLower() + columnName.Substring(1);
                    columnName = ReplaceTurkishCharacter(columnName);
                    string dataType = dt.Rows[0][1].ToString();
                    dataType = GetDataType(dataType, true);
                    method += dataType + " " + columnName;
                }
            }
            if (!checkIsObject.Checked && !radioDelete.Checked) method = method.Remove(method.Length - 2);
            method += ")";
            method += Environment.NewLine;
            method += "{" + Environment.NewLine;
            return method;
        }

        private string GetInsertOrUpdate(string query)
        {
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                string columnName = dt.Rows[i][0].ToString();

                string dataType = dt.Rows[i][1].ToString();
                string strIsNullable = dt.Rows[i][2].ToString();
                bool isNullable = strIsNullable == "YES" ? true : false;
                dataType = GetDataType(dataType);
                string value = "";
                query += Environment.NewLine;
                if (checkIsObject.Checked)
                {
                    value = txtTableName.Text[0].ToString().ToLower() + txtTableName.Text.Substring(1)
                    + "." + columnName[0].ToString().ToUpper() + columnName.Substring(1);
                }
                else
                {
                    value = columnName[0].ToString().ToLower() + columnName.Substring(1);
                    value = ReplaceTurkishCharacter(value);
                }

                if (isNullable && GetDataType(dataType.ToLower(), true) != "string")
                    value += " ?? (object)DBNull.Value";
                query += "dbManager.DataCommand.Parameters.Add('@" + columnName
                    + "', SqlDbType." + dataType + ").Value = " + value + ";";
            }
            return query;
        }

        private static string ReplaceTurkishCharacter(string value)
        {
            if (value.Substring(0, 1) == "ı") value = "i" + value.Substring(1);
            return value;
        }

        private static string GetDataType(string dataType, bool isMethod = false)
        {
            if (isMethod)
            {
                switch (dataType)
                {
                    case "varchar": dataType = "string"; break;
                    case "nvarchar": dataType = "string"; break;
                    case "bigint": dataType = "long"; break;
                    case "smallint": dataType = "short"; break;
                    case "int": dataType = "int"; break;
                    case "tinyint": dataType = "byte"; break;
                    case "bit": dataType = "bool"; break;
                    case "datetime": dataType = "DateTime"; break;
                    case "date": dataType = "DateTime"; break;
                    case "float": dataType = "float"; break;
                    case "decimal": dataType = "decimal"; break;
                    case "money": dataType = "decimal"; break;
                    case "nchar": dataType = "string"; break;
                    case "char": dataType = "string"; break;
                    case "time": dataType = "string"; break;
                    default: break;
                }
            }
            else
            {
                switch (dataType)
                {
                    case "varchar": dataType = "VarChar"; break;
                    case "nvarchar": dataType = "NVarChar"; break;
                    case "bigint": dataType = "BigInt"; break;
                    case "int": dataType = "Int"; break;
                    case "tinyint": dataType = "TinyInt"; break;
                    case "smallint": dataType = "SmallInt"; break;
                    case "bit": dataType = "Bit"; break;
                    case "datetime": dataType = "DateTime"; break;
                    case "date": dataType = "Date"; break;
                    case "float": dataType = "Float"; break;
                    case "decimal": dataType = "Decimal"; break;
                    case "money": dataType = "Money"; break;
                    case "nchar": dataType = "NChar"; break;
                    case "char": dataType = "Char"; break;
                    case "time": dataType = "Time"; break;
                    default:
                        break;
                }
            }
            return dataType;
        }

        private void txtSqlQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) keyCtrl = true;
            if (e.KeyCode == Keys.A) keyA = true;

            if (keyCtrl && keyA) txtSqlQuery.SelectAll();
        }

        private void txtSqlQuery_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control) keyCtrl = false;
            if (e.KeyCode == Keys.A) keyA = false;
        }
    }
}