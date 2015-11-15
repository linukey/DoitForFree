using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoitForFree.Model
{
    internal class MTask
    {
        #region 枚举[类型 情境 状态]
        public enum TaskType { 今日待办, 正在行动, 过期, 收集箱 };
        public enum TaskSituation { 家里, 办公室, 外出 };
        public enum TaskState { 已完成, 未完成, 垃圾箱 };
        #endregion

        #region 字段
        private string mName;
        private string mDiscription;
        private DateTime mStartDate;
        private DateTime mEndDate;
        private int mCycle;
        private TaskType mType;
        private TaskSituation mSituation;
        private string mProject;
        private string mGoal;
        private TaskState mState;
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
        public int MCycle
        {
            get
            {
                return mCycle;
            }

            set
            {
                mCycle = value;
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
        public TaskSituation MSituation
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
        #endregion

        public MTask() { }
        public MTask(string name, string discription, DateTime startdate, DateTime enddate, int cycle, TaskType type, TaskSituation situation, string project, string goal, TaskState state)
        {
            this.mName = name;
            this.mDiscription = discription;
            this.mStartDate = startdate;
            this.mEndDate = enddate;
            this.mCycle = cycle;
            this.mType = type;
            this.mSituation = situation;
            this.mProject = project;
            this.mGoal = goal;
            this.mState = state;
        }
    }
}