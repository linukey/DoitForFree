using DoitForFree.DAL;
using DoitForFree.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoitForFree.BAL
{
    internal class UserBAL
    {
        #region Add
        public bool Add(MUser s)
        {
            string cmdStr = "insert into T_user(用户编码,密码,邮箱) values(@user,@passwd,@email)";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter user = factory.CreateParameter();
            user.ParameterName = "@user";
            user.Value = s.MName;
            DbParameter passwd = factory.CreateParameter();
            passwd.ParameterName = "@passwd";
            passwd.Value = s.MPwd;
            DbParameter email = factory.CreateParameter();
            email.ParameterName = "@email";
            email.Value = s.MEmail;
            if (new UserDAL().Add(cmdStr, user, passwd, email) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Delete
        public bool Delete(string n)
        {
            string cmdStr = "delete from T_user where 用户编码=@user";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter user = factory.CreateParameter();
            user.ParameterName = "@name";
            user.Value = n;
            if (new UserDAL().Delete(cmdStr, user) == 1) return true;
            return false;
        }
        #endregion
        #region Update
        public bool Update(string preuser, MUser s)
        {
            string cmdStr = "update T_user set 用户编码=@user,密码=@passwd,邮箱=@email where 用户编码=@preuser";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter passwd = factory.CreateParameter();
            passwd.ParameterName = "@passwd";
            passwd.Value = s.MPwd;
            DbParameter user = factory.CreateParameter();
            user.ParameterName = "@user";
            user.Value = s.MName;
            DbParameter email = factory.CreateParameter();
            email.ParameterName = "@email";
            email.Value = s.MEmail;
            DbParameter pn = factory.CreateParameter();
            pn.ParameterName = "@preuser";
            pn.Value = preuser;
            if (new UserDAL().Add(cmdStr, user, email, passwd, pn) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Select
        public MUser Select(string n)
        {
            string cmdStr = "select * from T_user where 用户编码=@user";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter user = factory.CreateParameter();
            user.ParameterName = "@user";
            user.Value = n;
            DataTable dt = new UserDAL().Select(cmdStr, user);
            MUser s = null;
            if (dt != null)
            {
                s = new MUser();
                DataRow row = dt.Rows[0];
                s.MEmail = row["邮箱"].ToString();
                s.MPwd = row["密码"].ToString();
                s.MName = row["用户编码"].ToString();
            }
            return s;
        }
        #endregion
        #region 查询全部
        public DataTable SelectAll()
        {
            string cmdStr = "select * from T_user";
            DataTable dt = new UserDAL().Select(cmdStr);
            return dt;
        }
        #endregion
    }
}
