using System;
using System.Data.Common;
using DoitForFree.DAL;
using DoitForFree.Model;
using System.Data;

namespace DoitForFree.BAL
{
    internal class ProjectBAL
    {
        #region Add
        public bool Add(MProject p)
        {
            string cmdStr = "insert into T_project(项目名称,项目描述,开始时间,截止时间,用户编码) values(@name,@discription,@startdate,@enddate,@user)";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter name = factory.CreateParameter();
            name.ParameterName = "@name";
            name.Value = p.MName;

            DbParameter discription = factory.CreateParameter();
            discription.ParameterName = "@discription";
            discription.Value = p.MDiscription;

            DbParameter startdate = factory.CreateParameter();
            startdate.ParameterName = "@startdate";
            startdate.Value = p.MStartDate;

            DbParameter enddate = factory.CreateParameter();
            enddate.ParameterName = "@enddate";
            enddate.Value = p.MEndDate;

            DbParameter user = factory.CreateParameter();
            user.ParameterName = "@user";
            user.Value = p.MUser;

            if (new ProjectDAL().Add(cmdStr, name, discription, startdate, enddate, user) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Delete
        public bool Delete(string name, string user)
        {
            string cmdStr = "delete from T_project where 项目名称=@name and 用户编码=@user";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter n = factory.CreateParameter();
            n.ParameterName = "@name";
            n.Value = name;
            DbParameter u = factory.CreateParameter();
            u.ParameterName = "@user";
            u.Value = user;
            if(new ProjectDAL().Delete(cmdStr, n, u) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update
        public bool Update(string prename, MProject p)
        {
            string cmdStr = "update T_project set 项目名称=@name,项目描述=@discription,截止时间=@enddate,用户编码=@user where 项目名称=@prename";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter name = factory.CreateParameter();
            name.ParameterName = "@name";
            name.Value = p.MName;

            DbParameter discription = factory.CreateParameter();
            discription.ParameterName = "@discription";
            discription.Value = p.MDiscription;

            DbParameter enddate = factory.CreateParameter();
            enddate.ParameterName = "@enddate";
            enddate.Value = p.MEndDate;

            DbParameter user = factory.CreateParameter();
            user.ParameterName = "@user";
            user.Value = p.MUser;

            DbParameter pn = factory.CreateParameter();
            pn.ParameterName = "@prename";
            pn.Value = prename;
            if (new ProjectDAL().Update(cmdStr, name, discription, enddate, pn, user) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Select
        public MProject Select(string name)
        {
            string cmdStr = "select * from T_project where 项目名称=@name";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter n = factory.CreateParameter();
            n.ParameterName = "@name";
            n.Value = name;
            MProject p = null;
            DataTable dt = new ProjectDAL().Select(cmdStr, n);
            if(dt != null)
            {
                p = new MProject();
                DataRow row = dt.Rows[0];
                p.MName = row[1].ToString();
                p.MDiscription = row[2].ToString();
                p.MStartDate = DateTime.Parse(row[3].ToString());
                p.MEndDate = DateTime.Parse(row[4].ToString());
                p.MUser = row[5].ToString();
            }
            return p;
        }
        #endregion

        #region SelectAll
        public DataTable SelectAll(string userName)
        {
            string cmdStr = "select * from T_project where 用户编码=@username";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter user = factory.CreateParameter();
            user.ParameterName = "@username";
            user.Value = userName;
            return new ProjectDAL().Select(cmdStr, user);
        }
        #endregion
    }
}