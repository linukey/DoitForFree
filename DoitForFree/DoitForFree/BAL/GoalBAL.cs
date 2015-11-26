using DoitForFree.DAL;
using DoitForFree.Model;
using System;
using System.Data;
using System.Data.Common;

namespace DoitForFree.BAL
{
    internal class GoalBAL
    {
        #region Add
        public bool Add(MGoal g)
        {
            string cmdStr = "insert into T_goal(目标名称,目标描述,开始时间,截止时间,用户编码) values(@name,@discription,@startdate,@enddate,@user)";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter name = factory.CreateParameter();
            name.ParameterName = "@name";
            name.Value = g.MName;

            DbParameter discription = factory.CreateParameter();
            discription.ParameterName = "@discription";
            discription.Value = g.MDiscription;

            DbParameter startdate = factory.CreateParameter();
            startdate.ParameterName = "@startdate";
            startdate.Value = g.MStartDate;

            DbParameter enddate = factory.CreateParameter();
            enddate.ParameterName = "@enddate";
            enddate.Value = g.MEndDate;

            DbParameter user = factory.CreateParameter();
            user.ParameterName = "@user";
            user.Value = g.MUser;

            if (new GoalDAL().Add(cmdStr, name, discription, startdate, enddate, user) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Delete
        public bool Delete(string name, string user)
        {
            string cmdStr = "delete from T_goal where 目标名称=@name and 用户编码=@user";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter n = factory.CreateParameter();
            n.ParameterName = "@name";
            n.Value = name;
            DbParameter u = factory.CreateParameter();
            u.ParameterName = "@user";
            u.Value = user;
            if (new GoalDAL().Delete(cmdStr, n, u) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update
        public bool Update(string prename, MGoal p)
        {
            string cmdStr = "update T_goal set 目标名称=@name,目标描述=@discription,截止时间=@enddate,用户编码=@user where 目标名称=@prename";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter name = factory.CreateParameter();
            name.ParameterName = "@name";
            name.Value = p.MName;

            DbParameter discription = factory.CreateParameter();
            discription.ParameterName = "@discription";
            discription.Value = p.MDiscription;

            //DbParameter startdate = factory.CreateParameter();
            //startdate.ParameterName = "@startdate";
            //startdate.Value = p.MStartDate;

            DbParameter enddate = factory.CreateParameter();
            enddate.ParameterName = "@enddate";
            enddate.Value = p.MEndDate;

            DbParameter user = factory.CreateParameter();
            user.ParameterName = "@user";
            user.Value = p.MUser;

            DbParameter pn = factory.CreateParameter();
            pn.ParameterName = "@prename";
            pn.Value = prename;

            if (new GoalDAL().Update(cmdStr, name, discription, enddate, user, pn) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Select
        public MGoal Select(string name)
        {
            string cmdStr = "select * from T_goal where 目标名称=@name";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter n = factory.CreateParameter();
            n.ParameterName = "@name";
            n.Value = name;
            MGoal p = null;
            DataTable dt = new GoalDAL().Select(cmdStr, n);
            if (dt != null)
            {
                p = new MGoal();
                DataRow row = dt.Rows[0];
                p.MName = row["目标名称"].ToString();
                p.MDiscription = row["目标描述"].ToString();
                p.MStartDate = DateTime.Parse(row["开始时间"].ToString());
                p.MEndDate = DateTime.Parse(row["截止时间"].ToString());
                p.MUser = row["用户编码"].ToString();
            }
            return p;
        }
        #endregion
        #region SelectAll
        public DataTable SelectAll(string userName)
        {
            string cmdStr = "select * from T_goal where 用户编码=@username";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter user = factory.CreateParameter();
            user.ParameterName = "@username";
            user.Value = userName;
            return new GoalDAL().Select(cmdStr, user);
        }
        #endregion
    }
}
