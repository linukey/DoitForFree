using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDoitPlug
{
    /// <summary>
    /// 程序对外开放的插件接口，通过这个接口，可以自由定制程序插件功能
    /// </summary>
    public interface IPlug
    {
        string Name { set; get; }
        string Discription { set; get; }
        void Execute(string userName = null, string version = null, string providername = null, string conStr = null);
    }
}