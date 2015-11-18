using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoitForFree.Model
{
    public class MSituation : IMBase
    {
        private string mName;
        private string mUser;

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

        public MSituation() { }
        public MSituation(string name, string user)
        {
            this.mName = name;
            this.mUser = user;
        }
    }
}