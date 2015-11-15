using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using DoitForFree.DAL;
using DoitForFree.Model;
using System.Windows;
using System.Data.Common;
using DoitForFree.DAL;
using System.Data;

namespace DoitForFree.BAL
{
    internal class TaskBAL
    {
        #region Add
        public bool Add(MTask t)
        {
            string cmdStr = "insert into T_task(名称,描述,开始时间,截止时间,周期,类型,情境,项目,目标,状态) values(@name,@discription,@startdate,@enddate,@cycle,@type,@situation,@project,@goal,@state)";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter name = factory.CreateParameter();
            name.ParameterName = "@name";
            name.Value = t.MName;

            DbParameter discription = factory.CreateParameter();
            discription.ParameterName = "@discription";
            discription.Value = t.MDiscription;

            DbParameter startdate = factory.CreateParameter();
            startdate.ParameterName = "@startdate";
            startdate.Value = t.MStartDate.ToString("yyyy-MM-dd");

            DbParameter enddate = factory.CreateParameter();
            enddate.ParameterName = "@enddate";
            enddate.Value = t.MEndDate.ToString("yyyy-MM-dd");

            DbParameter cycle = factory.CreateParameter();
            cycle.ParameterName = "@cycle";
            cycle.Value = t.MCycle;

            DbParameter type = factory.CreateParameter();
            type.ParameterName = "@type";
            type.Value = t.MType.ToString();

            DbParameter situation = factory.CreateParameter();
            situation.ParameterName = "@situation";
            situation.Value = t.MSituation.ToString();

            DbParameter project = factory.CreateParameter();
            project.ParameterName = "@project";
            project.Value = t.MProject;

            DbParameter goal = factory.CreateParameter();
            goal.ParameterName = "@goal";
            goal.Value = t.MGoal;

            DbParameter state = factory.CreateParameter();
            state.ParameterName = "@state";
            state.Value = t.MState.ToString();

            if(new TaskDAL().Add(cmdStr, name, discription, startdate, enddate, cycle, type, situation, project, goal, state) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Delete
        public bool Delete(string name)
        {
            string cmdStr = "delete from T_task where 名称 = @name";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter n = factory.CreateParameter();
            n.ParameterName = "@name";
            n.Value = name;
            if(new GoalDAL().Delete(cmdStr, n) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update
        public bool Update(string prename, MTask t)
        {
            string cmdStr = "update T_task set 名称=@name,描述=@discription,开始时间=@startdate,截止时间=@enddate,周期=@cycle,类型=@type,情境=@situation,项目=@project,目标=@goal,状态=@state where 名称=@prename";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter name = factory.CreateParameter();
            name.ParameterName = "@name";
            name.Value = t.MName;

            DbParameter discription = factory.CreateParameter();
            discription.ParameterName = "@discription";
            discription.Value = t.MDiscription;

            DbParameter startdate = factory.CreateParameter();
            startdate.ParameterName = "@startdate";
            startdate.Value = t.MStartDate.ToString("yyyy-MM-dd");

            DbParameter enddate = factory.CreateParameter();
            enddate.ParameterName = "@enddate";
            enddate.Value = t.MEndDate.ToString("yyyy-MM-dd");

            DbParameter cycle = factory.CreateParameter();
            cycle.ParameterName = "@cycle";
            cycle.Value = t.MCycle;

            DbParameter type = factory.CreateParameter();
            type.ParameterName = "@type";
            type.Value = t.MType.ToString();

            DbParameter situation = factory.CreateParameter();
            situation.ParameterName = "@situation";
            situation.Value = t.MSituation.ToString();

            DbParameter project = factory.CreateParameter();
            project.ParameterName = "@project";
            project.Value = t.MProject;

            DbParameter goal = factory.CreateParameter();
            goal.ParameterName = "@goal";
            goal.Value = t.MGoal;

            DbParameter state = factory.CreateParameter();
            state.ParameterName = "@state";
            state.Value = t.MState.ToString();

            DbParameter pn = factory.CreateParameter();
            pn.ParameterName = "@prename";
            pn.Value = prename;

            if (new TaskDAL().Update(cmdStr, name, discription, startdate, enddate, cycle, type, situation, project, goal, state, pn) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Select
        public MTask Select(string name)
        {
            string cmdStr = "select * from T_task where 名称 = @name";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter n = factory.CreateParameter();
            n.ParameterName = "@name";
            n.Value = name;
            MTask t = null;
            DataTable dt = DbHelper.ExecuteDateTable(cmdStr, n);
            if (dt != null)
            {
                t = new MTask();
                DataRow row = dt.Rows[0];
                t.MName = row["名称"].ToString();
                t.MDiscription = row["描述"].ToString();
                t.MStartDate = DateTime.Parse(row["开始时间"].ToString());
                t.MEndDate = DateTime.Parse(row["截止时间"].ToString());
                t.MCycle = Convert.ToInt32(row["周期"].ToString());

                if (row["类型"].ToString() == "今日待办") t.MType = MTask.TaskType.今日待办;
                else if (row["类型"].ToString() == "正在行动") t.MType = MTask.TaskType.正在行动;
                else if (row["类型"].ToString() == "过期") t.MType = MTask.TaskType.过期;
                else t.MType = MTask.TaskType.收集箱;

                if (row["情境"].ToString() == "家里") t.MSituation = MTask.TaskSituation.家里;
                else if (row["情境"].ToString() == "办公室") t.MSituation = MTask.TaskSituation.办公室;
                else t.MSituation = MTask.TaskSituation.外出;

                t.MProject = row["项目"].ToString();
                t.MGoal = row["目标"].ToString();

                if (row["状态"].ToString() == "已完成") t.MState = MTask.TaskState.已完成;
                else if (row["状态"].ToString() == "未完成") t.MState = MTask.TaskState.未完成;
                else t.MState = MTask.TaskState.垃圾箱;
            }
            return t;
        }
        #endregion
    }
}