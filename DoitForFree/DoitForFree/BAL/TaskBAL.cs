using System;
using System.Data.Common;
using DoitForFree.DAL;
using DoitForFree.Model;
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
            startdate.Value = t.MStartDate.ToString("yyyy-MM-dd hh:mm:ss");

            DbParameter enddate = factory.CreateParameter();
            enddate.ParameterName = "@enddate";
            enddate.Value = t.MEndDate.ToString("yyyy-MM-dd hh:mm:ss");

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
        public bool Delete(string name, string userName)
        {
            string cmdStr = "delete from T_task where 任务名称 = @name and 用户编码=@user";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter n = factory.CreateParameter();
            n.ParameterName = "@name";
            n.Value = name;
            DbParameter user = factory.CreateParameter();
            user.ParameterName = "@user";
            user.Value = userName;
            if(new TaskDAL().Delete(cmdStr, n, user) == 1)
            {
                return true;
            }
            return false;
        }
        public int DeleteByProject(string name, string userName)
        {
            string cmdStr = "delete from T_task where 所属项目=@project and 用户编码=@user";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter n = factory.CreateParameter();
            n.ParameterName = "@project";
            n.Value = name;
            DbParameter user = factory.CreateParameter();
            user.ParameterName = "@user";
            user.Value = userName;
            return new TaskDAL().Delete(cmdStr, n, user);
        }
        public int DeleteByGoal(string name, string userName)
        {
            string cmdStr = "delete from T_task where 所属目标=@goal and 用户编码=@user";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter n = factory.CreateParameter();
            n.ParameterName = "@goal";
            n.Value = name;
            DbParameter user = factory.CreateParameter();
            user.ParameterName = "@user";
            user.Value = userName;
            return new TaskDAL().Delete(cmdStr, n, user);
        }
        public int DeleteBySituation(string name, string userName)
        {
            string cmdStr = "delete from T_task where 所属情境=@situation and 用户编码=@user";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter n = factory.CreateParameter();
            n.ParameterName = "@situation";
            n.Value = name;
            DbParameter user = factory.CreateParameter();
            user.ParameterName = "@user";
            user.Value = userName;
            return new TaskDAL().Delete(cmdStr, n, user);
        }
        #endregion

        #region Update
        public bool Update(string prename, MTask t)
        {
            string cmdStr = "update T_task set 任务名称=@name,任务描述=@discription,截止时间=@enddate,类型=@type,所属情境=@situation,所属项目=@project,所属目标=@goal,状态=@state,用户编码=@user where 任务名称=@prename";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter name = factory.CreateParameter();
            name.ParameterName = "@name";
            name.Value = t.MName;

            DbParameter discription = factory.CreateParameter();
            discription.ParameterName = "@discription";
            discription.Value = t.MDiscription;

            //DbParameter startdate = factory.CreateParameter();
            //startdate.ParameterName = "@startdate";
            //startdate.Value = t.MStartDate.ToString("yyyy-MM-dd");

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

            if (new TaskDAL().Update(cmdStr, name, discription, enddate, type, situation, project, goal, state, pn, user) == 1)
            {
                return true;
            }
            return false;
        }

        public void UpdateProject(string projectName, string userName, string preProject)
        {
            string cmdStr = "update T_task set 所属项目=@project where 用户编码=@user and 所属项目=@preproject";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter project = factory.CreateParameter();
            project.ParameterName = "@project";
            project.Value = projectName;
            DbParameter username = factory.CreateParameter();
            username.ParameterName = "@user";
            username.Value = userName;
            DbParameter preproject = factory.CreateParameter();
            preproject.ParameterName = "@preproject";
            preproject.Value = preProject;
            new TaskDAL().Update(cmdStr, project, username, preproject);
        }

        public void UpdateGoal(string goalName, string userName, string preGoal)
        {
            string cmdStr = "update T_task set 所属目标=@goal where 用户编码=@user and 所属目标=@pregoal";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter goal = factory.CreateParameter();
            goal.ParameterName = "@goal";
            goal.Value = goalName;
            DbParameter username = factory.CreateParameter();
            username.ParameterName = "@user";
            username.Value = userName;
            DbParameter pregoal = factory.CreateParameter();
            pregoal.ParameterName = "@pregoal";
            pregoal.Value = preGoal;
            new TaskDAL().Update(cmdStr, goal, username, pregoal);
        }

        public void UpdateSituation(string situationName, string userName, string preSituation)
        {
            string cmdStr = "update T_task set 所属情境=@situation where 用户编码=@user and 所属情境=@presituation";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter situation = factory.CreateParameter();
            situation.ParameterName = "@situation";
            situation.Value = situationName;
            DbParameter username = factory.CreateParameter();
            username.ParameterName = "@user";
            username.Value = userName;
            DbParameter presituation = factory.CreateParameter();
            presituation.ParameterName = "@presituation";
            presituation.Value = preSituation;
            new TaskDAL().Update(cmdStr, situation, username, presituation);
        }

        public void UpdateTaskState(string taskName, string state, string userName)
        {
            string cmdStr = "update T_task set 状态=@state where 用户编码=@user and 任务名称=@name";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter taskname = factory.CreateParameter();
            taskname.ParameterName = "@name";
            taskname.Value = taskName;
            DbParameter s = factory.CreateParameter();
            s.ParameterName = "@state";
            s.Value = state;
            DbParameter username = factory.CreateParameter();
            username.ParameterName = "@user";
            username.Value = userName;
            new TaskDAL().Update(cmdStr, taskname, s, username);
        }

        public void UpdateTaskType(string taskName, string type, string userName)
        {
            string cmdStr = "update T_task set 类型=@type where 用户编码=@user and 任务名称=@name";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter taskname = factory.CreateParameter();
            taskname.ParameterName = "@name";
            taskname.Value = taskName;
            DbParameter t = factory.CreateParameter();
            t.ParameterName = "@type";
            t.Value = type;
            DbParameter username = factory.CreateParameter();
            username.ParameterName = "@user";
            username.Value = userName;
            new TaskDAL().Update(cmdStr, taskname, t, username);
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
                t.MName = row[1].ToString();
                t.MDiscription = row[2].ToString();
                t.MStartDate = DateTime.Parse(row[3].ToString());
                t.MEndDate = DateTime.Parse(row[4].ToString());

                if (row[5].ToString() == "今日待办") t.MType = MTask.TaskType.今日待办;
                else if (row[5].ToString() == "正在行动") t.MType = MTask.TaskType.正在行动;
                else if (row[5].ToString() == "过期") t.MType = MTask.TaskType.过期;
                else t.MType = MTask.TaskType.收集箱;

                t.MSituation = row[6].ToString();
                t.MProject = row[7].ToString();
                t.MGoal = row[8].ToString();
                t.MSituation = row[10].ToString();

                if (row[9].ToString() == "已完成") t.MState = MTask.TaskState.已完成;
                else if (row[9].ToString() == "未完成") t.MState = MTask.TaskState.未完成;
                else t.MState = MTask.TaskState.垃圾箱;
            }
            return t;
        }

        public DataTable SelectAll(string userName)
        {
            string cmdStr = "select * from T_task where 用户编码=@username";
            DbProviderFactory factory = DbProviderFactories.GetFactory(DbHelper.provider);
            DbParameter user = factory.CreateParameter();
            user.ParameterName = "@username";
            user.Value = userName;
            return new TaskDAL().Select(cmdStr, user);
        }
        #endregion
    }
}