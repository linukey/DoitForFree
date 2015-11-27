using System;
using IDoitPlug;
using System.Data.Common;
using System.Data;
using System.Windows.Forms;

namespace PUpdate
{
    public class Update : IPlug
    {
        public Update()
        {
            providername = "System.Data.SqlClient";
            conStr = "server=(local);integrated security=SSPI;database=DoitDB";
            name = "检查更新!";
            discription = "检查更新！";
        }

        #region 字段
        private string name;
        private string discription;
        private string providername;
        private string conStr;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Discription
        {
            get
            {
                return discription;
            }

            set
            {
                discription = value;
            }
        }
        #endregion

        public void Execute(string userName)
        {
            string cmdStr1 = "select * from T_update where 用户编码=@user";
            string cmdStr2 = "select * from T_update where 用户编码=@doit";
            DbProviderFactory factory = DbProviderFactories.GetFactory(providername);
            try
            {
                DataTable dtUser = new DataTable();
                DataTable dtDoit = new DataTable();
                using (DbConnection conn = factory.CreateConnection())
                {
                    conn.ConnectionString = conStr;

                    DbParameter user = factory.CreateParameter();
                    user.ParameterName = "@user";
                    user.Value = userName;

                    DbCommand cmdUser = factory.CreateCommand();
                    cmdUser.CommandText = cmdStr1;
                    cmdUser.CommandType = System.Data.CommandType.Text;
                    cmdUser.Connection = conn;
                    cmdUser.Parameters.Add(user);

                    DbDataAdapter daUser = factory.CreateDataAdapter();
                    daUser.SelectCommand = cmdUser;
                    daUser.Fill(dtUser);
                }
                using (DbConnection conn = factory.CreateConnection())
                {
                    conn.ConnectionString = conStr;

                    DbParameter doit = factory.CreateParameter();
                    doit.ParameterName = "@doit";
                    doit.Value = "Doit";

                    DbCommand cmdDoit = factory.CreateCommand();
                    cmdDoit.CommandText = cmdStr2;
                    cmdDoit.CommandType = CommandType.Text;
                    cmdDoit.Connection = conn;
                    cmdDoit.Parameters.Add(doit);

                    DbDataAdapter daDoit = factory.CreateDataAdapter();
                    daDoit.SelectCommand = cmdDoit;
                    daDoit.Fill(dtDoit);
                }
                if (dtUser != null && dtDoit != null && dtUser.Rows.Count > 0 && dtDoit.Rows.Count > 0)
                {
                    DataRow rowUser = dtUser.Rows[0];
                    DataRow rowDoit = dtDoit.Rows[0];
                    if ((DateTime)rowUser["最后一次更新时间"] < (DateTime)rowDoit["最后一次更新时间"])
                    {
                        if (MessageBox.Show("检测到新版本，是否进行更新？", "更新！", MessageBoxButtons.OKCancel, MessageBoxIcon.Information).GetHashCode() == 1)
                        {
                            new ToCheck(rowDoit["下载地址"].ToString()).Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("您的版本已经是最新版本，无需更新！", "更新", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("未知错误信息！", "检测更新失败！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误消息！\n" + ex.Message, "检测更新失败！", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
