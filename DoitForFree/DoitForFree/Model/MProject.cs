﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoitForFree.Model
{
    internal class MProject
    {
        #region 字段
        private string mName;
        private string mDiscription;
        private DateTime mStartDate;
        private DateTime mEndDate;
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

        public MProject() { }
        public MProject(string name, string discription, DateTime startdate, DateTime enddate, string user)
        {
            this.mName = name;
            this.mDiscription = discription;
            this.mStartDate = startdate;
            this.mEndDate = enddate;
            this.mUser = user;
        }
    }
}
