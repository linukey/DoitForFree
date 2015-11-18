using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoitForFree.Model
{
    public class MTask : IMBase 
    {
        #region 枚举[类型 状态]
        public enum TaskType { 今日待办, 正在行动, 过期, 收集箱 };
        public enum TaskState { 已完成, 未完成, 垃圾箱 };
        #endregion

        #region 字段
        private string mName;
        private string mDiscription;
        private DateTime mStartDate;
        private DateTime mEndDate;
        private TaskType mType;
        private string mSituation;
        private string mProject;
        private string mGoal;
        private TaskState mState;
        private string mUser;
        #endregion

        #region 属性
        public string MName
        {
            get
            {
                return mName;
            }

            set
            {
                mName = value;
            }
        }
        public string MDiscription
        {
            get
            {
                return mDiscription;
            }

            set
            {
                mDiscription = value;
            }
        }
        public DateTime MStartDate
        {
            get
            {
                return mStartDate;
            }

            set
            {
                mStartDate = value;
            }
        }
        public DateTime MEndDate
        {
            get
            {
                return mEndDate;
            }

            set
            {
                mEndDate = value;
            }
        }
        public TaskType MType
        {
            get
            {
                return mType;
            }

            set
            {
                mType = value;
            }
        }
        public string MSituation
        {
            get
            {
                return mSituation;
            }

            set
            {
                mSituation = value;
            }
        }
        public string MProject
        {
            get
            {
                return mProject;
            }

            set
            {
                mProject = value;
            }
        }
        public string MGoal
        {
            get
            {
                return mGoal;
            }

            set
            {
                mGoal = value;
            }
        }
        public TaskState MState
        {
            get
            {
                return mState;
            }

            set
            {
                mState = value;
            }
        }

        public string MUser
        {
            get
            {
                return mUser;
            }

            set
            {
                mUser = value;
            }
        }
        #endregion

        public MTask() { }
        public MTask(string name, string discription, DateTime startdate, DateTime enddate, TaskType type, string situation, string project, string goal, TaskState state, string user)
        {
            this.mName = name;
            this.mDiscription = discription;
            this.mStartDate = startdate;
            this.mEndDate = enddate;
            this.mType = type;
            this.mSituation = situation;
            this.mProject = project;
            this.mGoal = goal;
            this.mState = state;
            this.mUser = user;
        }

        public static TaskType stringToTaskType(string s)
        {
            if (s == "今日待办") return TaskType.今日待办;
            else if (s == "正在行动") return TaskType.正在行动;
            else if (s == "过期") return TaskType.过期;
            else return TaskType.收集箱;
        }

        public static TaskState stringToTaskState(string s)
        {
            if (s == "已完成") return TaskState.已完成;
            else if (s == "未完成") return TaskState.未完成;
            else return TaskState.垃圾箱;
        }
    }
}