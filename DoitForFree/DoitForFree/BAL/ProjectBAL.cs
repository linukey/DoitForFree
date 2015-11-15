using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            string cmdStr = "insert into T_project(名称,描述,开始时间,截止时间,周期) values(@name,@discription,@startdate,@enddate,@cycle)";
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

            if(new ProjectDAL().Add(cmdStr, name, discription, startdate, enddate, cycle) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Delete
        public bool Delete(string name)
        {
            string cmdStr = "delete from T_project where 名称=@name";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter n = factory.CreateParameter();
            n.ParameterName = "@name";
            n.Value = name;
            if(new ProjectDAL().Delete(cmdStr, n) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update
        public bool Update(string prename, MProject p)
        {
            string cmdStr = "update T_project set 名称=@name,描述=@discription,开始时间=@startdate,截止时间=@enddate,周期=@cycle where 名称=@prename";
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
            if (new ProjectDAL().Update(cmdStr, name, discription, startdate, enddate, cycle, pn) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Select
        public MProject Select(string name)
        {
            string cmdStr = "select * from T_project where 名称=@name";
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