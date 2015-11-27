using System;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Windows;

namespace DoitForFree.DAL
{
    /// <summary>
    /// 基于工厂设计模式编写独立于特定数据库的数据库操作代码
    /// 根据配置文件里面的 providerName 信息来区别是哪一种数据库
    /// </summary>
    internal class DbHelper
    {
        //provider
        public static readonly string provider = ConfigurationManager.ConnectionStrings[0].ProviderName;

        #region 获取连接字符串
        private static string getConnectionStringByProvider(string provider)
        {
            string connStr = null;
            if (provider != null)
            {
                ConnectionStringSettingsCollection set = ConfigurationManager.ConnectionStrings;
                if (set != null)
                {
                    foreach (ConnectionStringSettings tmp in set)
                    {
                        if (provider == tmp.ProviderName)
                        {
                            connStr = tmp.ConnectionString;
                        }
                    }
                }
            }
            return connStr;
        }
        #endregion

        #region 获取连接对象
        private static DbConnection getDbConnection(string provider, string connStr)
        {
            DbConnection conn = null;
            if (connStr != null && provider != null)
            {
                try
                {
                    DbProviderFactory factory = DbProviderFactories.GetFactory(provider);
                    conn = factory.CreateConnection();
                    conn.ConnectionString = connStr;
                }
                catch (Exception ex)
                {
                    if (conn != null) conn = null;
                    MessageBox.Show(ex.Message);
                }
            }
            return conn;
        }
        #endregion

        #region ExecuteNonQuery
        public static int ExecuteNonQuery(string cmdStr, params DbParameter[] paras)
        {
            if (cmdStr != null)
            {
                try
                {
                    string connStr = getConnectionStringByProvider(provider);
                    using (DbConnection conn = getDbConnection(provider, connStr))
                    {
                        DbCommand cmd = conn.CreateCommand();
                        cmd.CommandText = cmdStr;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddRange(paras);
                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return -1;
        }
        #endregion

        #region ExecuteScalar
        public static object ExecuteScalar(string cmdStr, params DbParameter[] paras)
        {
            if (cmdStr != null)
            {
                try
                {
                    string connStr = getConnectionStringByProvider(provider);
                    using (DbConnection conn = getDbConnection(provider, connStr))
                    {
                        DbCommand cmd = conn.CreateCommand();
                        cmd.CommandText = cmdStr;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddRange(paras);
                        conn.Open();
                        return cmd.ExecuteScalar();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return null;
        }
        #endregion

        #region ExecuteDateTable
        public static DataTable ExecuteDateTable(string cmdStr, params DbParameter[] paras)
        {
            DataTable dt = null;
            if(cmdStr != null)
            {
                try
                {
                    string connStr = getConnectionStringByProvider(provider);
                    using (DbConnection conn = getDbConnection(provider, connStr))
                    {
                        DbCommand cmd = conn.CreateCommand();
                        cmd.CommandText = cmdStr;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddRange(paras);
                        DbProviderFactory factory = DbProviderFactories.GetFactory(provider);
                        DbDataAdapter da = factory.CreateDataAdapter();
                        da.SelectCommand = cmd;
                        dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count == 0) dt = null;
                    }
                }
                catch(Exception ex)
                {
                    if (dt != null) dt = null;
                    MessageBox.Show(ex.Message);
                }
            }
            return dt;
        }
        #endregion
    }
}