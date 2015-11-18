using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoitForFree.Model;
using DoitForFree.DAL;
using System.Data.Common;
using System.Data;

namespace DoitForFree.BAL
{
    internal class SituationBAL
    {
        #region Add
        public bool Add(MSituation s)
        {
            string cmdStr = "insert into T_situation(情境名称,用户编码) values(@name,@user)";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter name = factory.CreateParameter();
            name.ParameterName = "@name";
            name.Value = s.MName;
            DbParameter user = factory.CreateParameter();
            user.ParameterName = "@user";
            user.Value = s.MUser;
            if(new SituationDAL().Add(cmdStr, name, user) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Delete
        public bool Delete(string n)
        {
            string cmdStr = "delete from T_situation where 情境名称=@name";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter name = factory.CreateParameter();
            name.ParameterName = "@name";
            name.Value = n;
            if (new SituationDAL().Delete(cmdStr, name) == 1) return true;
            return false;
        }
        #endregion
        #region Update
        public bool Update(string prename, MSituation s)
        {
            string cmdStr = "update T_situation set 情境名称=@name,用户编码=@user where 情境名称=@prename";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter name = factory.CreateParameter();
            name.ParameterName = "@name";
            name.Value = s.MName;
            DbParameter user = factory.CreateParameter();
            user.ParameterName = "@user";
            user.Value = s.MUser;
            DbParameter pn = factory.CreateParameter();
            pn.ParameterName = "@prename";
            pn.Value = prename;
            if (new SituationDAL().Add(cmdStr, name, user, pn) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Select
        public MSituation Select(string n)
        {
            string cmdStr = "select * from T_situation where 情境名称=@name";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter name = factory.CreateParameter();
            name.ParameterName = "@name";
            name.Value = n;
            DataTable dt = new SituationDAL().Select(cmdStr, name);
            MSituation s = null;
            if(dt != null)
            {
                DataRow row = dt.Rows[0];
                s.MName = row["情境名称"].ToString();
                s.MUser = row["用户编码"].ToString();
            }
            return s;
        }
        #endregion
        #region SelectAll
        public DataTable SelectAll(string userName)
        {
            string cmdStr = "select * from T_situation where 用户编码=@username";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter user = factory.CreateParameter();
            user.ParameterName = "@username";
            user.Value = userName;
            return new SituationDAL().Select(cmdStr, user);
        }
        #endregion
    }
}
