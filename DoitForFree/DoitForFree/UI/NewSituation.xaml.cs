using DoitForFree.BAL;
using DoitForFree.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DoitForFree.UI
{
    /// <summary>
    /// NewSituation.xaml 的交互逻辑
    /// </summary>
    public partial class NewSituation : Window
    {
        #region 字段
        private string preTitle = null; //修改前的任务名称
        private List<MSituation> situationList = null; //情境列表
        private enum WindowType { 添加, 修改 };
        private string WType = null; //窗口类型
        #endregion

        #region 构造函数
        public NewSituation()
        {
            InitializeComponent();
            DataTable dt = new SituationBAL().SelectAll(Resource.userName);
            preTitle = dt.Rows[0]["情境名称"].ToString();
            tbx标题.Text = dt.Rows[0]["情境名称"].ToString();
        }
        //添加和修改情境都是这一个构造函数
        public NewSituation(List<MSituation> list,string situationName = null)
        {
            InitializeComponent();
            situationList = list;
            if (situationName != null)
            {
                WType = WindowType.修改.ToString();
                preTitle = situationName;
                tbx标题.Text = situationName;
            }
            else
            {
                WType = WindowType.添加.ToString();
            }
        }
        #endregion

        #region 窗口处理
        //窗体拖动
        private void NewGoalWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion

        #region 输入框处理
        //得到光标时
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text.Trim() == "标题") this.tbx标题.Text = "";
        }
        //失去光标时
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text.Trim() == "" && ((TextBox)sender).Name == "tbx标题") this.tbx标题.Text = "标题";
        }
        #endregion

        #region 提交按钮
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Name == "btn取消") this.Close();
            else if (((Button)sender).Name == "btn确定")
            {
                if (preTitle != null && preTitle != tbx标题.Text.Trim() || preTitle == null)
                {
                    if (tbx标题.Text.Trim() == "" || tbx标题.Text.Trim() == "标题")
                    {
                        MessageBox.Show("标题为必填信息！");
                        return;
                    }
                    if (WType == "修改")
                    {
                        string preSituationName = null;
                        string s = null;
                        foreach (MSituation situation in situationList)
                        {
                            preSituationName += (situation.MName + ";");
                            if (situation.MName != preTitle)
                            {
                                s += (situation.MName + ";");
                            }
                            else
                            {
                                s += (tbx标题.Text.Trim() + ";");
                            }
                        }
                        new SituationBAL().Update(s, Resource.userName);
                        new TaskBAL().UpdateSituation(tbx标题.Text.Trim(), Resource.userName, preTitle);
                    }
                    else if(WType == "添加")
                    {
                        string s = "";
                        foreach(MSituation situation in situationList)
                        {
                            s += (situation.MName + ";");
                        }
                        s += (tbx标题.Text.Trim() + ";");
                        new SituationBAL().Update(s, Resource.userName);
                    }
                    this.Close();
                }
            }
        }
        #endregion
    }
}