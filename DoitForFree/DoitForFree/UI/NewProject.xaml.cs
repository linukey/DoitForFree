using DoitForFree.BAL;
using DoitForFree.Model;
using System;
using System.Collections.Generic;
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
    /// NewProject.xaml 的交互逻辑
    /// </summary>
    public partial class NewProject : Window
    {
        #region 字段
        private List<MProject> projectList = null; //项目列表
        private string WType; //窗口类型
        private string preTitle; //修改前的任务名称
        private enum WindowType { 添加, 修改 };
        #endregion

        #region 构造函数
        public NewProject()
        {
            InitializeComponent();
        }
        //添加项目时
        public NewProject(List<MProject> projectList)
        {
            WType = WindowType.添加.ToString();
            InitializeComponent();
            this.projectList = projectList;
        }
        //修改项目时
        public NewProject(string title, string discription, string enddate)
        {
            WType = WindowType.修改.ToString();
            preTitle = title;
            InitializeComponent();
            tbx标题.Text = title;
            tbx描述.Text = discription;
            MenuButton截止时间.Text = enddate;
        }
        #endregion

        #region 主窗口处理
        //窗体拖动
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion

        #region 输入框处理
        //得到光标时
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text.Trim() == "标题") this.tbx标题.Text = "";
            else if (((TextBox)sender).Text.Trim() == "描述") this.tbx描述.Text = "";
        }
        //失去光标时
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text.Trim() == "" && ((TextBox)sender).Name == "tbx标题") this.tbx标题.Text = "标题";
            else if (((TextBox)sender).Text.Trim() == "" && ((TextBox)sender).Name == "tbx描述") this.tbx描述.Text = "描述";
        }
        #endregion

        #region 初始化 截止时间
        //点击截止时间时
        private void MenuButton截止时间_Click(object sender, RoutedEventArgs e)
        {
                this.MenuCalendar.IsOpen = true;
                this.MenuCalendar.Visibility = Visibility.Visible;        
        }
        //日历控件选择变化时
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime date = (DateTime)Calendar截止时间.SelectedDate;
            this.MenuButton截止时间.Text = date.ToString("yyyy-MM-dd");
        }
        //鼠标移到日历控件外面时
        private void Calendar截止时间_MouseLeave(object sender, MouseEventArgs e)
        {
            this.MenuCalendar.Visibility = Visibility.Hidden;
        }
        #endregion

        #region 提交按钮
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Name == "btn取消") this.Close();
            else if (((Button)sender).Name == "btn确定")
            {
                if (tbx标题.Text.Trim() == "" || tbx标题.Text.Trim() == "标题" || MenuButton截止时间.Text == "截止时间")
                {
                    MessageBox.Show("标题、截止时间为必填信息！");
                    return;
                }

                MProject project = new MProject();
                project.MName = tbx标题.Text.Trim();
                project.MDiscription = tbx描述.Text.Trim();
                project.MStartDate = DateTime.Now;
                project.MEndDate = DateTime.Parse(MenuButton截止时间.Text.Trim());
                project.MUser = Resource.userName;

                if (WType == "添加") new ProjectBAL().Add(project);
                else if (WType == "修改")
                {
                    if (preTitle != tbx标题.Text.Trim())
                    {
                        new ProjectBAL().Update(preTitle, project);
                        new TaskBAL().UpdateProject(tbx标题.Text, Resource.userName, preTitle);
                    }
                }
                this.Close();
            }
        }
        #endregion
    }
}
