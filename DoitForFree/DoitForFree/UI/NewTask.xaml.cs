using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DoitForFree.Model;
using DoitForFree.BAL;

namespace DoitForFree.UI
{
    /// <summary>
    /// NewTask.xaml 的交互逻辑
    /// </summary>
    public partial class NewTask : Window
    {
        #region 字段
        private List<MProject> projectList = null;
        private List<MGoal> goalList = null;
        private List<MSituation> situationList = null;
        private string WType; //窗口类型
        private string preTitle; //修改前的任务名称
        private string curMenu;
        private enum WindowType { 添加, 修改 };
        #endregion

        #region 构造函数
        public NewTask()
        {
            InitializeComponent();
            //btn确定.IsEnabled = false;
            //Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Execute(Checkbtn确定));
        }
        //添加任务
        public NewTask(List<MProject> projectList, List<MGoal> goalList, List<MSituation> situationList)
        {
            WType = WindowType.添加.ToString();
            InitializeComponent();
            //btn确定.IsEnabled = false;
            //Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Execute(Checkbtn确定));
            this.projectList = projectList;
            this.goalList = goalList;
            this.situationList = situationList;
        }
        //修改任务
        public NewTask(List<MProject> projectList, List<MGoal> goalList, List<MSituation> situationList, string title, string discription, string enddate, string type, string project, string goal, string situation)
        {
            WType = WindowType.修改.ToString();
            this.projectList = projectList;
            this.goalList = goalList;
            this.situationList = situationList;
            preTitle = title;
            InitializeComponent();
            //btn确定.IsEnabled = false;
            //Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Execute(Checkbtn确定));
            tbx标题.Text = title;
            tbx描述.Text = discription;
            MenuButton情境.Text = situation;
            MenuButton目标.Text = goal;
            MenuButton类型.Text = type;
            MenuButton项目.Text = project;
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

        #region 初始化 类型、项目、目标、情境 列表
        private void MenuButton菜单_Click(object sender, RoutedEventArgs e)
        {
            if (((MenuButton)sender).Name == "MenuButton类型")
            {
                this.Menu类型.IsOpen = true;
                curMenu = "类型";
            }
            else if (((MenuButton)sender).Name == "MenuButton截止时间")
            {
                this.MenuCalendar.IsOpen = true;
                this.MenuCalendar.Visibility = Visibility.Visible;
            }
            else if (((MenuButton)sender).Name == "MenuButton项目")
            {
                Menu项目.Items.Clear();
                foreach (MProject project in projectList)
                {
                    MenuItem m = new MenuItem();
                    m.Header = project.MName;
                    m.Click += MenuItem_Click;
                    m.Template = (ControlTemplate)FindResource("downMenu");
                    Menu项目.Items.Add(m);
                }
                if (Menu项目.Items.Count != 0) Menu项目.IsOpen = true;
                curMenu = "项目";
            }
            else if (((MenuButton)sender).Name == "MenuButton目标")
            {
                Menu目标.Items.Clear();
                foreach (MGoal goal in goalList)
                {
                    MenuItem m = new MenuItem();
                    m.Click += MenuItem_Click;
                    m.Header = goal.MName;
                    m.Template = (ControlTemplate)FindResource("downMenu");
                    Menu目标.Items.Add(m);
                }
                if (Menu目标.Items.Count != 0) Menu目标.IsOpen = true;
                curMenu = "目标";
            }
            else if (((MenuButton)sender).Name == "MenuButton情境")
            {
                Menu情境.Items.Clear();
                foreach (MSituation situation in situationList)
                {
                    MenuItem m = new MenuItem();
                    m.Click += MenuItem_Click;
                    m.Header = situation.MName;
                    m.Template = (ControlTemplate)FindResource("downMenu");
                    Menu情境.Items.Add(m);
                }
                if (Menu情境.Items.Count != 0) Menu情境.IsOpen = true;
                curMenu = "情境";
            }
        }
        //当日历控件选择日期变化时
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime date = (DateTime)Calendar截止时间.SelectedDate;
            if (date.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd")) MenuButton类型.Text = "今日待办";
            this.MenuButton截止时间.Text = date.ToString("yyyy-MM-dd");
        }
        //鼠标移到日历控件外面时
        private void Calendar截止时间_MouseLeave(object sender, MouseEventArgs e)
        {
            this.MenuCalendar.Visibility = Visibility.Hidden;
        }
        //当选择了项目、目标、类型、情境后
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (curMenu == "类型")
            {
                this.MenuButton类型.Text = ((MenuItem)sender).Header.ToString();
                if (this.MenuButton类型.Text == "今日待办") MenuButton截止时间.Text = DateTime.Now.ToString("yyyy-MM-dd");
                else if (MenuButton截止时间.Text == DateTime.Now.ToString("yyyy-MM-dd")) MenuButton截止时间.Text = "截止时间";
            }
            else if (curMenu == "项目") MenuButton项目.Text = ((MenuItem)sender).Header.ToString();
            else if (curMenu == "目标") MenuButton目标.Text = ((MenuItem)sender).Header.ToString();
            else if (curMenu == "情境") MenuButton情境.Text = ((MenuItem)sender).Header.ToString();
        }
        #endregion

        #region 提交按钮
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Name == "btn取消") this.Close();
            else if (((Button)sender).Name == "btn确定")
            {
                if (tbx标题.Text.Trim() == "" || tbx标题.Text.Trim() == "标题" || MenuButton类型.Text == "收集箱" && MenuButton截止时间.Text == "截止时间")
                {
                    MessageBox.Show("标题、类型、截止时间为必填信息！");
                    return;
                }

                MTask task = new MTask();
                task.MName = tbx标题.Text.Trim();
                task.MDiscription = tbx描述.Text.Trim();
                task.MProject = MenuButton项目.Text.Trim();
                task.MSituation = MenuButton情境.Text.Trim();
                task.MGoal = MenuButton目标.Text.Trim();
                task.MStartDate = DateTime.Parse(MenuButton截止时间.Text.Trim().ToString());    
                task.MEndDate = DateTime.Parse(MenuButton截止时间.Text.Trim().ToString());
                task.MType = MTask.stringToTaskType(MenuButton类型.Text.Trim());
                task.MState = MTask.stringToTaskState("未完成");
                task.MUser = Resource.userName;
                if (WType == "添加") new TaskBAL().Add(task);
                else if (WType == "修改")
                {
                    new TaskBAL().Update(preTitle, task);
                }
                this.Close();
            }
        }
        #endregion

        //取消造成界面卡顿直至卡死
        //private void Checkbtn确定()
        //{
        //    ThreadPool.QueueUserWorkItem((o) =>
        //    {
        //        while (true)
        //        {
        //            try
        //            {
        //                this.Dispatcher.Invoke(new Action(() =>
        //                {
        //                    if (tbx标题.Text.Trim() == "" || tbx标题.Text.Trim() == "标题") btn确定.IsEnabled = false;
        //                    else if (MenuButton类型.Text != "收集箱" && MenuButton截止时间.Text == "截止时间") btn确定.IsEnabled = false;
        //                    else btn确定.IsEnabled = true;
        //                }));
        //            }
        //            catch (Exception ex) { }
        //        };
        //    });
        //}
    }
}