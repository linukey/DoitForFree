using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoitForFree.Model
{
    internal class MUser
    {
        private string mName;
        private string mPwd;
        private string mEmail;

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

        public string MPwd
        {
            get
            {
                return mPwd;
            }

            set
            {
                mPwd = value;
            }
        }

        public string MEmail
        {
            get
            {
                return mEmail;
            }

            set
            {
                mEmail = value;
            }
        }

        public MUser() { }
        public MUser(string name, string pwd, string email)
        {
            this.mName = name;
            this.mPwd = pwd;
            this.mEmail = email;
        }
    }
}
