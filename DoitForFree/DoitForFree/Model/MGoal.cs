using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoitForFree.Model
{
    internal class MGoal
    {
        #region 字段
        private string mName;
        private string mDiscription;
        private DateTime mStartDate;
        private DateTime mEndDate;
        private int mCycle;
        #endregion

        #region 字段
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
        #endregion

        public MGoal() { }
        public MGoal(string name, string discription, DateTime startdate, DateTime enddate, int cycle)
        {
            this.mName = name;
            this.mDiscription = discription;
            this.mStartDate = startdate;
            this.mEndDate = enddate;
            this.mCycle = cycle;
        }
    }
}
