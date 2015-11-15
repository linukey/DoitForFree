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
    internal class GoalBAL
    {
        #region Add
        public bool Add(MGoal g)
        {
            string cmdStr = "insert into T_goal(名称,描述,开始时间,截止时间,周期) values(@name,@discription,@startdate,@enddate,@cycle)";
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

            DbParameter cycle = factory.CreateParameter();
            cycle.ParameterName = "@cycle";
            cycle.Value = g.MCycle;

            if (new GoalDAL().Add(cmdStr, name, discription, startdate, enddate, cycle) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Delete
        public bool Delete(string name)
        {
            string cmdStr = "delete from T_goal where 名称=@name";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter n = factory.CreateParameter();
            n.ParameterName = "@name";
            n.Value = name;
            if (new GoalDAL().Delete(cmdStr, n) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update
        public bool Update(string prename, MGoal p)
        {
            string cmdStr = "update T_goal set 名称=@name,描述=@discription,开始时间=@startdate,截止时间=@enddate,周期=@cycle where 名称=@prename";
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

            DbParameter cycle = factory.CreateParameter();
            cycle.ParameterName = "@cycle";
            cycle.Value = p.MCycle;

            DbParameter pn = factory.CreateParameter();
            pn.ParameterName = "@prename";
            pn.Value = prename;

            if (new GoalDAL().Update(cmdStr, name, discription, startdate, enddate, cycle, pn) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Select
        public MGoal Select(string name)
        {
            string cmdStr = "select * from T_goal where 名称=@name";
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
                p.MName = row["名称"].ToString();
                p.MDiscription = row["描述"].ToString();
                p.MStartDate = DateTime.Parse(row["开始时间"].ToString());
                p.MEndDate = DateTime.Parse(row["截止时间"].ToString());
                p.MCycle = Convert.ToInt32(row["周期"].ToString());
                return p;
            }
            return p;
        }
        #endregion
    }
}
