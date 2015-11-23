using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DoitForFree.BAL;
using DoitForFree.Model;
using System.ComponentModel;
using DoitForFree.UI;
using System.Data;

namespace DoitForFree
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 构造函数
        public MainWindow()
        {
            InitializeComponent();
            Resource.userName = "linukey";

            InitProjectList();
            InitSituationList();
            InitGoalList();
            InitTaskList();
        }
        #endregion

        #region 字段声明
        private List<MProject> projectList = null; //项目列表
        private List<MSituation> situationList = null; //情境列表
        private List<MGoal> goalList = null; //目标列表
        private List<MTask> taskList = null; //任务列表
        private Button curSelect = null; //当前选中节点
        string curMenu = null; //当前选中的左侧菜单
        string curNode = null; //当前选中的 情境 项目 目标 里的节点

        private const int MenuButtonNodeHeight = 30;
        private const int MenuButtonNodeWidth = 245;
        #endregion

        #region 窗体处理
        //窗体移动
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        //窗体关闭
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //窗体最小化
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        //设置菜单
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            this.downMenu.IsOpen = true;
        }
        #endregion

        #region 左侧菜单栏点击事件
        private void menu收集箱_Click(object sender, RoutedEventArgs e)
        {
            InitNewButton(sender as MenuButton);
            InitTitleButton("收集箱");
        }

        private void menu今日待办_Click(object sender, RoutedEventArgs e)
        {
            InitNewButton(sender as MenuButton);
            InitTitleButton("今日待办");
        }

        private void menu正在行动_Click(object sender, RoutedEventArgs e)
        {
            InitNewButton(sender as MenuButton);
            InitTitleButton("正在行动");
        }

        private void menu过期_Click(object sender, RoutedEventArgs e)
        {
            InitNewButton(sender as MenuButton);
            InitTitleButton("过期");
        }

        private void menu所有项目_Click(object sender, RoutedEventArgs e)
        {
            InitMenuButton(projectList, menu所有项目, wp所有项目);
            InitNewButton(sender as MenuButton);
        }

        private void menu所有目标_Click(object sender, RoutedEventArgs e)
        {
            InitMenuButton(goalList, menu所有目标, wp所有目标);
            InitNewButton(sender as MenuButton);
        }

        private void menu所有情境_Click(object sender, RoutedEventArgs e)
        {
            InitMenuButton(situationList, menu所有情境, wp所有情境);
            InitNewButton(sender as MenuButton);
        }

        private void menu已完成_Click(object sender, RoutedEventArgs e)
        {
            InitNewButton(sender as MenuButton);
            InitTitleButton("已完成");
        }

        private void menu垃圾箱_Click(object sender, RoutedEventArgs e)
        {
            InitNewButton(sender as MenuButton);
            InitTitleButton("垃圾箱");
        }
        #endregion

        #region 右侧数据初始化
        private void InitTitleButton(string judge)
        {
            wp右侧显示.Children.Clear();
            curMenu = judge;
            List<DateTime> time = new List<DateTime>();
            HashSet<string> j = new HashSet<string>();
            if (judge == "已完成" || judge == "垃圾箱")
            {
                foreach (MTask task in taskList)
                {
                    if (task.MState.ToString() == judge && !j.Contains(task.MStartDate.ToString()))
                    {
                        j.Add(task.MStartDate.ToString());
                        time.Add(task.MStartDate);
                    }
                }
            }
            else
            {
                foreach (MTask task in taskList)
                {
                    if (task.MType.ToString() == judge && !j.Contains(task.MStartDate.ToString()))
                    {
                        j.Add(task.MStartDate.ToString());
                        time.Add(task.MStartDate);
                    }
                }
            }
            time.Sort();
            foreach (DateTime t in time)
            {
                TitleButton title = new TitleButton();
                title.Template = (ControlTemplate)FindResource("TitleButton");
                title.Click += TitleButton_Click;
                title.ImagePath = "Images/下箭头.png";
                title.Week = t.DayOfWeek.ToString();
                title.Month = t.ToString("yyyy-MM-dd");
                title.Date = t;
                wp右侧显示.Children.Add(title);
                WrapPanel w = new WrapPanel();
                wp右侧显示.Children.Add(w);
                try { wp右侧显示.UnregisterName("w" + t.Year + t.Month + t.Day); }
                catch (Exception ex) { }
                wp右侧显示.RegisterName("w" + t.Year + t.Month + t.Day, w);
            }
            foreach (MTask t in taskList)
            {
                if (judge == "已完成" || judge == "垃圾箱")
                {
                    if (t.MState.ToString() == judge)
                    {
                        TitleNodeButton node = new TitleNodeButton();
                        node.Height = 45;
                        node.Width = 830;
                        node.StartDate = t.MStartDate.ToString("hh:mm:ss");
                        node.Title = t.MName;
                        node.Discription = t.MDiscription;
                        node.ImagePath = @"Images/任务.png";
                        node.Template = (ControlTemplate)FindResource("TitleNodeButton");
                        node.Click += TitleNodeButton_Click;
                        node.MouseDoubleClick += TitleNodeButton_MouseDoubleClick;
                        WrapPanel ww = (WrapPanel)wp右侧显示.FindName("w" + t.MStartDate.Year + t.MStartDate.Month + t.MStartDate.Day);
                        ww.Children.Add(node);
                    }
                }
                else
                {
                    if (t.MType.ToString() == judge)
                    {
                        TitleNodeButton node = new TitleNodeButton();
                        node.Height = 45;
                        node.Width = 830;
                        node.StartDate = t.MStartDate.ToString("hh:mm:ss");
                        node.Title = t.MName;
                        node.Discription = t.MDiscription;
                        node.ImagePath = @"Images/任务.png";
                        node.Template = (ControlTemplate)FindResource("TitleNodeButton");
                        node.Click += TitleNodeButton_Click;
                        node.MouseDoubleClick += TitleNodeButton_MouseDoubleClick;
                        WrapPanel ww = (WrapPanel)wp右侧显示.FindName("w" + t.MStartDate.Year + t.MStartDate.Month + t.MStartDate.Day);
                        ww.Children.Add(node);
                    }
                }
            }
        }

        private void InitTitleButton(object sender, RoutedEventArgs e)
        {
            wp右侧显示.Children.Clear();
            MenuButton menu = (MenuButton)sender;
            WrapPanel parentWrap = (WrapPanel)menu.Parent;
            curNode = menu.Name;
            List<DateTime> time = new List<DateTime>();
            HashSet<string> j = new HashSet<string>();
            if (parentWrap.Name == (curMenu = "wp所有项目"))
            {
                //添加查看项目、情境、目标按钮
                InitNewButton(null, curMenu);

                foreach (MTask task in taskList)
                {
                    if (task.MProject == menu.Name && !j.Contains(task.MStartDate.ToString()))
                    {
                        j.Add(task.MStartDate.ToString());
                        time.Add(task.MStartDate);
                    }
                }
            }
            else if (parentWrap.Name == (curMenu = "wp所有情境"))
            {
                //添加查看项目、情境、目标按钮
                InitNewButton(null, curMenu);

                foreach (MTask task in taskList)
                {
                    if (task.MSituation == menu.Name && !j.Contains(task.MStartDate.ToString()))
                    {
                        j.Add(task.MStartDate.ToString());
                        time.Add(task.MStartDate);
                    }
                }
            }
            else if (parentWrap.Name == (curMenu = "wp所有目标"))
            {
                //添加查看项目、情境、目标按钮
                InitNewButton(null, curMenu);

                foreach (MTask task in taskList)
                {
                    if (task.MGoal == menu.Name && !j.Contains(task.MStartDate.ToString()))
                    {
                        j.Add(task.MStartDate.ToString());
                        time.Add(task.MStartDate);
                    }
                }
            }
            time.Sort();
            foreach (DateTime t in time)
            {
                TitleButton title = new TitleButton();
                title.Template = (ControlTemplate)FindResource("TitleButton");
                title.Click += TitleButton_Click2;
                title.ImagePath = "Images/下箭头.png";
                title.Week = t.DayOfWeek.ToString();
                title.Month = t.ToString("yyyy-MM-dd");
                title.Date = t;
                wp右侧显示.Children.Add(title);
                WrapPanel w = new WrapPanel();
                wp右侧显示.Children.Add(w);
                try { wp右侧显示.UnregisterName("w" + t.Year + t.Month + t.Day); }
                catch (Exception ex) { }
                wp右侧显示.RegisterName("w" + t.Year + t.Month + t.Day, w);
            }
            foreach (MTask t in taskList)
            {
                string judge = null;
                if (parentWrap.Name == "wp所有项目") judge = t.MProject;
                else if (parentWrap.Name == "wp所有情境") judge = t.MSituation;
                else if (parentWrap.Name == "wp所有目标") judge = t.MGoal;
                if (judge == menu.Name && t.MState.ToString() != "已完成")
                {
                    TitleNodeButton node = new TitleNodeButton();
                    node.Height = 45;
                    node.Width = 830;
                    node.StartDate = t.MStartDate.ToString("hh:mm:ss");
                    node.Title = t.MName;
                    node.Discription = t.MDiscription;
                    node.ImagePath = @"Images/任务.png";
                    node.Template = (ControlTemplate)FindResource("TitleNodeButton");
                    node.Click += TitleNodeButton_Click;
                    node.MouseDoubleClick += TitleNodeButton_MouseDoubleClick;
                    WrapPanel ww = (WrapPanel)wp右侧显示.FindName("w" + t.MStartDate.Year + t.MStartDate.Month + t.MStartDate.Day);
                    ww.Children.Add(node);
                }
            }
        }
        #endregion

        #region 数据初始化
        //初始化项目列表
        public void InitProjectList()
        {
            projectList = new List<MProject>();
            DataTable dt = new ProjectBAL().SelectAll(Resource.userName);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    MProject m = new MProject();
                    m.MName = row["项目名称"].ToString();
                    m.MDiscription = row["项目描述"].ToString();
                    m.MStartDate = DateTime.Parse(row["开始时间"].ToString());
                    m.MEndDate = DateTime.Parse(row["截止时间"].ToString());
                    m.MUser = row["用户编码"].ToString();
                    projectList.Add(m);
                    ;
                }
            }
        }
        //初始化情境列表
        public void InitSituationList()
        {
            situationList = new List<MSituation>();
            DataTable dt = new SituationBAL().SelectAll(Resource.userName);
            if (dt != null)
            {
                DataRow row = dt.Rows[0];
                string str = row["情境名称"].ToString();
                int start = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == ';')
                    {
                        MSituation s = new MSituation();
                        s.MName = str.Substring(start, i - start);
                        s.MUser = Resource.userName;
                        situationList.Add(s);
                        start = i + 1;
                    }
                    else if (i + 1 == str.Length && start != str.Length)
                    {
                        MSituation s = new MSituation();
                        s.MName = str.Substring(start, i - start + 1);
                        s.MUser = Resource.userName;
                        situationList.Add(s);
                        start = i + 1;
                    }
                }
            }
        }
        //初始化目标列表
        public void InitGoalList()
        {
            goalList = new List<MGoal>();
            DataTable dt = new GoalBAL().SelectAll(Resource.userName);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    MGoal g = new MGoal();
                    g.MName = row["目标名称"].ToString();
                    g.MDiscription = row["目标描述"].ToString();
                    g.MStartDate = DateTime.Parse(row["开始时间"].ToString());
                    g.MEndDate = DateTime.Parse(row["截止时间"].ToString());
                    g.MUser = row["用户编码"].ToString();
                    goalList.Add(g);
                }
            }
        }
        //初始化任务列表
        public void InitTaskList()
        {
            taskList = new List<MTask>();
            DataTable dt = new TaskBAL().SelectAll(Resource.userName);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    MTask t = new MTask();
                    t.MName = row["任务名称"].ToString();
                    t.MDiscription = row["任务描述"].ToString();
                    t.MStartDate = DateTime.Parse(row["开始时间"].ToString());
                    t.MEndDate = DateTime.Parse(row["截止时间"].ToString());
                    t.MType = MTask.stringToTaskType(row["类型"].ToString());
                    t.MSituation = row["所属情境"].ToString();
                    t.MProject = row["所属项目"].ToString();
                    t.MGoal = row["所属目标"].ToString();
                    t.MState = MTask.stringToTaskState(row["状态"].ToString());
                    t.MUser = row["用户编码"].ToString();
                    taskList.Add(t);
                }
            }
        }
        #endregion

        #region 左侧菜单处理
        //左侧菜单单击动态显示效果
        private void setMargin(MenuButton m, string judge)
        {
            if (judge == "下箭头")
                m.Margin = new Thickness(m.Margin.Left, m.Margin.Top, m.Margin.Right, m.Margin.Bottom + 10);
            else m.Margin = new Thickness(m.Margin.Left, m.Margin.Top, m.Margin.Right, m.Margin.Bottom - 10);
        }
        //左侧菜单单击数据加载
        private void InitMenuButton(IEnumerable<IMBase> l, MenuButton m, WrapPanel wp)
        {
            m.ImagePath = m.ImagePath == "Images/下箭头.png" ? "Images/右箭头.png" : "Images/下箭头.png";
            setMargin(m, m.ImagePath == "Images/下箭头.png" ? "下箭头" : "右箭头");
            if (m.ImagePath == "Images/下箭头.png")
            {
                foreach (IMBase mm in l)
                {
                    MenuButton button = new MenuButton();
                    button.Height = MenuButtonNodeHeight;
                    button.Width = MenuButtonNodeWidth;
                    button.Template = (ControlTemplate)FindResource("MenuButton");
                    button.ImagePath = "Images/情境节点.png";
                    button.Text = mm.MName;
                    button.Name = mm.MName;
                    button.Click += InitTitleButton;
                    wp.Children.Add(button);
                }
            }
            else wp.Children.Clear();
        }
        //左侧菜单鼠标手势变换
        private void mouseChangeEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void mouseChangeLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
        #endregion

        #region 添加栏
        //新任务
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MenuButton button = (MenuButton)sender;

            if (button.Text == "新任务")
            {
                NewTask window = new NewTask(projectList, goalList, situationList);
                window.Closing += Window_Closing;
                window.Show();
            }
            else if (button.Text == "新项目")
            {
                NewProject window = new NewProject(projectList);
                window.Closing += Window_Closing;
                window.Show();
            }
            else if (button.Text == "新目标")
            {
                NewGoal window = new NewGoal(goalList);
                window.Closing += Window_Closing;
                window.Show();
            }
            else if(button.Text == "新情境")
            {
                NewSituation window = new NewSituation(situationList);
                window.Closing += Window_Closing;
                window.Show();
            }
            else if (button.Text == "编辑项目")
            {
                MProject project = new ProjectBAL().Select(curNode);
                NewProject window = new NewProject(project.MName, project.MDiscription, project.MEndDate.ToString("yyyy-MM-dd"));
                window.Closing += Window_Closing;
                window.Show();
            }
            else if (button.Text == "编辑目标")
            {
                MGoal goal = new GoalBAL().Select(curNode);
                NewGoal window = new NewGoal(goal.MName, goal.MDiscription, goal.MEndDate.ToString("yyyy-MM-dd"));
                window.Closing += Window_Closing;
                window.Show();
            }
            else if(button.Text == "编辑情境")
            {
                NewSituation window = new NewSituation(situationList, curNode);
                window.Closing += Window_Closing;
                window.Show();
            }
        }

        //初始化按钮
        private void InitNewButton(MenuButton button, string parentName = null)
        {
            if (button != null)
            {
                spn添加.Children.Clear();
                MenuButton m = new MenuButton();
                if (button.Name == "menu收集箱" || button.Name == "menu今日待办" ||
                    button.Name == "menu正在行动" || button.Name == "menu过期" ||
                    button.Name == "menu已完成" || button.Name == "menu垃圾箱")
                    m.Text = "新任务";
                else if (button.Name == "menu所有项目") m.Text = "新项目";
                else if (button.Name == "menu所有目标") m.Text = "新目标";
                else if (button.Name == "menu所有情境") m.Text = "新情境";

                m.ImagePath = @"Images/新项目.png";
                m.Template = (ControlTemplate)FindResource("MenuButton添加按钮");
                m.MouseEnter += mouseChangeEnter;
                m.MouseLeave += mouseChangeLeave;
                m.Click += MenuButton_Click;
                spn添加.Children.Add(m);
            }
            else if(parentName != null)
            {
                spn编辑.Children.Clear();
                MenuButton m = new MenuButton();
                if (parentName == "wp所有项目") m.Text = "编辑项目";
                else if (parentName == "wp所有目标") m.Text = "编辑目标";
                else if (parentName == "wp所有情境") m.Text = "编辑情境";
                m.ImagePath = @"Images/新项目.png";
                m.Template = (ControlTemplate)FindResource("MenuButton添加按钮");
                m.MouseEnter += mouseChangeEnter;
                m.MouseLeave += mouseChangeLeave;
                m.Click += MenuButton_Click;
                spn编辑.Children.Add(m);
            }
        }
        #endregion

        #region 任务列表点击事件处理
        private void TitleButton_Click(object sender, RoutedEventArgs e)
        {
            TitleButton title = (TitleButton)sender;
            WrapPanel w = (WrapPanel)wp右侧显示.FindName("w" + title.Date.Year + title.Date.Month + title.Date.Day);
            if (title.ImagePath == "Images/下箭头.png")
            {
                title.ImagePath = @"Images/右箭头.png";
                w.Children.Clear();
            }
            else if (title.ImagePath == "Images/右箭头.png")
            {
                title.ImagePath = "Images/下箭头.png";
                if (curMenu == "已完成" || curMenu == "垃圾箱")
                {
                    foreach (MTask t in taskList)
                    {
                        if (t.MState.ToString() == curMenu && t.MStartDate.ToString("yyyy-M-d") == (title.Date.Year + "-" + title.Date.Month + "-" + title.Date.Day))
                        {
                            TitleNodeButton node = new TitleNodeButton();
                            node.Height = 45;
                            node.Width = 830;
                            node.StartDate = t.MStartDate.ToString("hh:mm:ss");
                            node.Title = t.MName;
                            node.Discription = t.MDiscription;
                            node.ImagePath = @"Images/任务.png";
                            node.Template = (ControlTemplate)FindResource("TitleNodeButton");
                            node.Click += TitleNodeButton_Click;
                            node.MouseDoubleClick += TitleNodeButton_MouseDoubleClick;
                            WrapPanel ww = (WrapPanel)wp右侧显示.FindName("w" + t.MStartDate.Year + t.MStartDate.Month + t.MStartDate.Day);
                            ww.Children.Add(node);
                        }
                    }
                }
                else
                {
                    foreach (MTask t in taskList)
                    {
                        if (t.MType.ToString() == curMenu && t.MStartDate.ToString("yyyy-M-d") == (title.Date.Year + "-" + title.Date.Month + "-" + title.Date.Day))
                        {
                            TitleNodeButton node = new TitleNodeButton();
                            node.Height = 45;
                            node.Width = 830;
                            node.StartDate = t.MStartDate.ToString("hh:mm:ss");
                            node.Title = t.MName;
                            node.Discription = t.MDiscription;
                            node.ImagePath = @"Images/任务.png";
                            node.Template = (ControlTemplate)FindResource("TitleNodeButton");
                            node.Click += TitleNodeButton_Click;
                            node.MouseDoubleClick += TitleNodeButton_MouseDoubleClick;
                            WrapPanel ww = (WrapPanel)wp右侧显示.FindName("w" + t.MStartDate.Year + t.MStartDate.Month + t.MStartDate.Day);
                            ww.Children.Add(node);
                        }
                    }
                }
            }
        }

        private void TitleButton_Click2(object sender, RoutedEventArgs e)
        {
            TitleButton title = (TitleButton)sender;
            WrapPanel w = (WrapPanel)wp右侧显示.FindName("w" + title.Date.Year + title.Date.Month + title.Date.Day);
            if (title.ImagePath == "Images/右箭头.png")
            {
                title.ImagePath = "Images/下箭头.png";
                foreach (MTask t in taskList)
                {
                    string judge = null;
                    if (curMenu == "wp所有项目") judge = t.MProject;
                    else if (curMenu == "wp所有情境") judge = t.MSituation;
                    else if (curMenu == "wp所有目标") judge = t.MGoal;
                    if (judge == curNode && t.MState.ToString() != "已完成" && t.MStartDate.ToString("yyyy-M-d") == (title.Date.Year + "-" + title.Date.Month + "-" + title.Date.Day))
                    {
                        TitleNodeButton node = new TitleNodeButton();
                        node.Height = 45;
                        node.Width = 830;
                        node.StartDate = t.MStartDate.ToString("hh:mm:ss");
                        node.Title = t.MName;
                        node.Discription = t.MDiscription;
                        node.ImagePath = @"Images/任务.png";
                        node.Template = (ControlTemplate)FindResource("TitleNodeButton");
                        node.Click += TitleNodeButton_Click;
                        node.MouseDoubleClick += TitleNodeButton_MouseDoubleClick;
                        w.Children.Add(node);
                    }
                }
            }
            else
            {
                title.ImagePath = "Images/右箭头.png";
                w.Children.Clear();
            }
        }

        private void TitleNodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (curSelect != null) curSelect.Template = (ControlTemplate)FindResource("TitleNodeButton");
            ((Button)sender).Template = (ControlTemplate)FindResource("TitleNodeButton2");
            curSelect = (Button)sender;
        }

        private void TitleNodeButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            foreach (MTask task in taskList)
            {
                if (task.MName == ((TitleNodeButton)sender).Title)
                {
                    NewTask window = new NewTask(task.MName, task.MDiscription, task.MEndDate.ToString("yyyy-MM-dd hh:mm:ss"), task.MType.ToString(), task.MProject, task.MGoal, task.MSituation);
                    window.Closing += Window_Closing;
                    window.Show();
                }
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            InitProjectList();
            InitSituationList();
            InitGoalList();
            InitTaskList();
        }
        #endregion
    }
}