#region Using
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace CreateSqlQuery
{
    public class DbManager : IDisposable
    {
        #region Global Variable
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adp;
        SqlTransaction transaction;
        DataTable dt;
        #endregion

        public static string connStr = @"Server=.;
                                         Database=xxx;
                                         Trusted_Connection=True;";

        #region Constructor
        public DbManager()
        {
            conn = new SqlConnection();
            conn.ConnectionString = connStr;
            ConnOpen();
            cmd = new SqlCommand();
            cmd.CommandTimeout = 60;
            cmd.Connection = conn;
            adp = new SqlDataAdapter();
            adp.SelectCommand = cmd;
        }
        #endregion

        #region Property
        public SqlCommand DataCommand
        {
            get { return cmd; }
            set { cmd = value; }
        }

        public SqlDataAdapter SqlDataAdapter
        {
            get { return adp; }
            set { adp = value; }
        }
        #endregion

        #region Connection Functions 
        /// <summary>
        /// Open SQL Connection
        /// </summary>
        /// <returns>If open connection return true, else return false</returns>
        public bool ConnOpen()
        {
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                return true;
            }
            catch { return false; }
        }
        /// <summary>
        /// Close SQL Connection
        /// </summary>
        /// <returns>If close connection return true, else return false</returns>
        public bool ConnClose()
        {
            try
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                return true;
            }
            catch { return false; }
        }
        #endregion

        #region Functions
        #region Get
        public DataTable GetDataTable(string sqlCode)
        {
            return GetDataTable(sqlCode, CommandType.Text);
        }
        public DataTable GetDataTable(string sqlCode, CommandType commandType)
        {
            dt = new DataTable();
            cmd.CommandType = commandType; 
            cmd.CommandText = sqlCode;
            adp.Fill(dt);
            return dt;
        }

        public object GetExecuteScalar(string sqlCode)
        {
            return GetExecuteScalar(sqlCode, CommandType.Text);
        }
        public object GetExecuteScalar(string sqlCode, CommandType commandType)
        {
            object objValue;
            cmd.CommandType = commandType;
            cmd.CommandText = sqlCode;
            try
            {
                if (!sqlCode.Contains("@")) cmd.Parameters.Clear();
                objValue = DataCommand.ExecuteScalar();
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                objValue = null;
            }
            return objValue;
        }
        #endregion

        public bool BeginTransaction()
        {
            try { transaction = conn.BeginTransaction(); return true; }
            catch (Exception ex) { return false; }
        }

        public bool CommitTransaction()
        {
            if (transaction == null) return false;
            try { transaction.Commit(); return true; }
            catch (Exception ex) { return false; }
        }

        public int RunCommand(string sqlCode)
        {
            return RunCommand(sqlCode, CommandType.Text);
        }
        public int RunCommand(string sqlCode, CommandType commandType)
        {
            cmd.Transaction = transaction;
            int identityID = -1;
            try
            {
                cmd.CommandText = sqlCode;
                cmd.CommandType = commandType;
                cmd.ExecuteNonQuery();
                if (!sqlCode.ToLower().StartsWith("update") &&
                    !sqlCode.ToLower().StartsWith("delete"))
                    identityID = Convert.ToInt32(GetExecuteScalar("select @@IDENTITY"));
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                return -1;
            }
            return identityID;
        }

        public bool IsThereRow(string sqlCode)
        {
            return GetDataTable(sqlCode).Rows.Count > 0;
        }
        #endregion

        public void Dispose()
        {
            ConnClose();
            if (conn != null) conn.Dispose();
            if (cmd != null) cmd.Dispose();
            if (adp != null) adp.Dispose();
            if (dt != null) dt.Dispose();
            if (transaction != null) transaction.Dispose();
        }
    }
}