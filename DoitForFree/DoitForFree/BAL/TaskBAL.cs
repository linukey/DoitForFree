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
            string cmdStr = "insert into T_task(任务名称,任务描述,开始时间,截止时间,类型,所属情境,所属项目,所属目标,状态,用户编码) values(@name,@discription,@startdate,@enddate,@type,@situation,@project,@goal,@state,@user)";
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

            DbParameter user = factory.CreateParameter();
            user.ParameterName = "@user";
            user.Value = t.MUser;

            if (new TaskDAL().Add(cmdStr, name, discription, startdate, enddate, type, situation, project, goal, state, user) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Delete
        public bool Delete(string name)
        {
            string cmdStr = "delete from T_task where 任务名称 = @name";
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
            string cmdStr = "update T_task set 任务名称=@name,任务描述=@discription,开始时间=@startdate,截止时间=@enddate,类型=@type,所属情境=@situation,所属项目=@project,所属目标=@goal,状态=@state,用户编码=@user where 任务名称=@prename";
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

            DbParameter user = factory.CreateParameter();
            user.ParameterName = "@user";
            user.Value = t.MUser;

            if (new TaskDAL().Update(cmdStr, name, discription, startdate, enddate, type, situation, project, goal, state, pn, user) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Select
        public MTask Select(string name)
        {
            string cmdStr = "select * from T_task where 任务名称 = @name";
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
                t.MName = row["任务名称"].ToString();
                t.MDiscription = row["任务描述"].ToString();
                t.MStartDate = DateTime.Parse(row["开始时间"].ToString());
                t.MEndDate = DateTime.Parse(row["截止时间"].ToString());

                if (row["类型"].ToString() == "今日待办") t.MType = MTask.TaskType.今日待办;
                else if (row["类型"].ToString() == "正在行动") t.MType = MTask.TaskType.正在行动;
                else if (row["类型"].ToString() == "过期") t.MType = MTask.TaskType.过期;
                else t.MType = MTask.TaskType.收集箱;

                t.MSituation = row["所属情境"].ToString();
                t.MProject = row["所属项目"].ToString();
                t.MGoal = row["所属目标"].ToString();
                t.MSituation = row["用户编码"].ToString();

                if (row["状态"].ToString() == "已完成") t.MState = MTask.TaskState.已完成;
                else if (row["状态"].ToString() == "未完成") t.MState = MTask.TaskState.未完成;
                else t.MState = MTask.TaskState.垃圾箱;
            }
            return t;
        }
        #endregion

        #region SelectAll
        public DataTable SelectAll()
        {
            string cmdStr = "select * from T_task";
            return new TaskDAL().Select(cmdStr);
        }
        #endregion
    }
}