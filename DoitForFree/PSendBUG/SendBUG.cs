using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IDoitPlug;

namespace PSendBUG
{
    public class SendBUG : IPlug
    {
        public SendBUG()
        {
            name = "提交BUG！";
            discription = "提交BUG。";
        }
 
        #region 字段
        private string name;
        private string discription;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Discription
        {
            get
            {
                return discription;
            }

            set
            {
                discription = value;
            }
        }
        #endregion

        public void Execute(string userName, string version, string providername, string conStr)
        {
            new BugInfo(providername,conStr).Show();
        }
    }
}
