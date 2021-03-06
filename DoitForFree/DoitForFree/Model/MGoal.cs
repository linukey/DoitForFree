﻿using System;

namespace DoitForFree.Model
{
    public class MGoal : IMBase
    {
        #region 字段
        private string mName;
        private string mDiscription;
        private DateTime mStartDate;
        private DateTime mEndDate;
        private string mUser;
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

        public MGoal() { }
        public MGoal(string name, string discription, DateTime startdate, DateTime enddate, string user)
        {
            this.mName = name;
            this.mDiscription = discription;
            this.mStartDate = startdate;
            this.mEndDate = enddate;
            this.MUser = user;
        }
    }
}
