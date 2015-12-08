using DoitForFree.BAL;
using DoitForFree.Model;
using DoitForFree.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using IDoitPlug;
using System.IO;
using System.Reflection;
using DoitForFree.DAL;

namespace DoitForFree
{
    public partial class MainWindow : Window
    {
        #region 构造函数
        public MainWindow()
        {
            InitializeComponent();
            Resource.userName = "linukey";
            InitPlug();         //初始化外部查件
            InitProjectList();  //初始化项目列表
            InitSituationList();//初始化情境列表
            InitGoalList();     //初始化目标列表
            InitTaskList();     //初始化任务列表
            CheckTaskState();   //检查任务是否过期
        }
        #endregion

        #region 字段声明
        private List<MProject> projectList = null;     //项目列表
        private List<MSituation> situationList = null; //情境列表
        private List<MGoal> goalList = null;           //目标列表
        private List<MTask> taskList = null;           //任务列表
        private TitleNodeButton curSelect = null;      //当前选中节点
        private MenuButton curSelectMenuButton = null;       //当前选中的左侧菜单按钮，需要进行效果处理
        private string curMenu = null;                         //当前选中的左侧菜单
        private string curNode = null;                         //当前选中的 情境 项目 目标 里的节点
        private const int MenuButtonNodeHeight = 30;
        private const int MenuButtonNodeWidth = 245;
        #endregion

        #region 窗体处理
        //窗体拖动
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
        //窗体设置菜单
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            this.downMenu.IsOpen = true;
        }
        #endregion

        #region 左侧菜单栏点击事件
        private void menu收集箱_Click(object sender, RoutedEventArgs e)
        {
            InitNewButton(sender as MenuButton);
            InitTitleButton("收集箱", sender as MenuButton);
        }
        private void menu今日待办_Click(object sender, RoutedEventArgs e)
        {
            InitNewButton(sender as MenuButton);
            InitTitleButton("今日待办", sender as MenuButton);
        }
        private void menu正在行动_Click(object sender, RoutedEventArgs e)
        {
            InitNewButton(sender as MenuButton);
            InitTitleButton("正在行动", sender as MenuButton);
        }
        private void menu过期_Click(object sender, RoutedEventArgs e)
        {
            InitNewButton(sender as MenuButton);
            InitTitleButton("过期", sender as MenuButton);
        }
        private void menu已完成_Click(object sender, RoutedEventArgs e)
        {
            InitNewButton(sender as MenuButton);
            InitTitleButton("已完成", sender as MenuButton);
        }
        private void menu垃圾箱_Click(object sender, RoutedEventArgs e)
        {
            InitNewButton(sender as MenuButton);
            InitTitleButton("垃圾箱", sender as MenuButton);
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
        //左侧项目、目标、情境菜单单击数据加载
        private void InitMenuButton(IEnumerable<IMBase> l, MenuButton m, WrapPanel wp)
        {
            //左侧菜单选中效果处理
            if (curSelectMenuButton != null) curSelectMenuButton.Template = (ControlTemplate)FindResource("MenuButton");

            wp右侧显示.Children.Clear();
            curMenu = m.Name;
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
                    try { wp.UnregisterName(mm.MName); }
                    catch (Exception ex) { }
                    wp.RegisterName(mm.MName, button);
                    button.Click += LeftMenuButtonNode_Click;
                    wp.Children.Add(button);
                }
            }
            else wp.Children.Clear();
        }
        #endregion

        #region 左侧菜单点击右侧数据初始化
        private void InitTitleButton(string judge, MenuButton btn)
        {
            //进行左侧菜单选中效果的处理
            if (curSelectMenuButton != null) curSelectMenuButton.Template = (ControlTemplate)FindResource("MenuButton");
            btn.Template = (ControlTemplate)FindResource("MenuButton2");
            curSelectMenuButton = btn;
            wp右侧显示.Children.Clear();

            #region 筛选出所有种类的日期
            List<DateTime> time = new List<DateTime>();
            HashSet<string> j = new HashSet<string>();
            if (judge != null)
            {
                curMenu = judge;
                foreach (MTask task in taskList)
                {
                    if (judge == "已完成" || judge == "垃圾箱")
                    {
                        if (task.MState.ToString() == judge && !j.Contains(task.MStartDate.ToString()))
                        {
                            j.Add(task.MStartDate.ToString());
                            time.Add(task.MStartDate);
                        }
                    }
                    else if (judge == "收集箱" || judge == "今日待办" || judge == "正在行动" || judge == "过期")
                    {
                        if (task.MType.ToString() == judge && task.MState.ToString() == "未完成" && !j.Contains(task.MStartDate.ToString()))
                        {
                            j.Add(task.MStartDate.ToString());
                            time.Add(task.MStartDate);
                        }
                    }
                }
            }
            else if (judge == null)
            {
                WrapPanel parentWrap = (WrapPanel)curSelectMenuButton.Parent;
                curNode = curSelectMenuButton.Name;
                foreach (MTask task in taskList)
                {
                    if (parentWrap.Name == (curMenu = "wp所有项目"))
                    {
                        if (task.MProject == curSelectMenuButton.Name && task.MState.ToString() == "未完成" && !j.Contains(task.MStartDate.ToString()))
                        {
                            j.Add(task.MStartDate.ToString());
                            time.Add(task.MStartDate);
                        }
                    }
                    else if (parentWrap.Name == (curMenu = "wp所有情境"))
                    {
                        if (task.MSituation == curSelectMenuButton.Name && task.MState.ToString() == "未完成" && !j.Contains(task.MStartDate.ToString()))
                        {
                            j.Add(task.MStartDate.ToString());
                            time.Add(task.MStartDate);
                        }
                    }
                    else if (parentWrap.Name == (curMenu = "wp所有目标"))
                    {
                        if (task.MGoal == curSelectMenuButton.Name && task.MState.ToString() == "未完成" && !j.Contains(task.MStartDate.ToString()))
                        {
                            j.Add(task.MStartDate.ToString());
                            time.Add(task.MStartDate);
                        }
                    }
                    InitNewButton(null, curMenu);//添加查看项目、情境、目标按钮
                }
            }
            time.Sort();
            #endregion

            #region 添加右侧显示任务收缩栏
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
            #endregion

            #region 添加右侧显示任务列表
            foreach (MTask t in taskList)
            {
                if (judge != null)
                {
                    WrapPanel ww = (WrapPanel)wp右侧显示.FindName("w" + t.MStartDate.Year + t.MStartDate.Month + t.MStartDate.Day);
                    if (judge == "已完成" || judge == "垃圾箱")
                    {
                        if (t.MState.ToString() == judge)
                        {
                            createNode(t, ww);
                        }
                    }
                    else if (judge == "收集箱" || judge == "今日待办" || judge == "正在行动" || judge == "过期")
                    {
                        if (t.MType.ToString() == judge && t.MState.ToString() == "未完成")
                        {
                            createNode(t, ww);
                        }
                    }
                }
                else
                {
                    WrapPanel parentWrap = (WrapPanel)curSelectMenuButton.Parent;
                    string jj = null;
                    if (parentWrap.Name == "wp所有项目") jj = t.MProject;
                    else if (parentWrap.Name == "wp所有情境") jj = t.MSituation;
                    else if (parentWrap.Name == "wp所有目标") jj = t.MGoal;
                    if (jj == curSelectMenuButton.Name && t.MState.ToString() == "未完成")
                    {
                        WrapPanel ww = (WrapPanel)wp右侧显示.FindName("w" + t.MStartDate.Year + t.MStartDate.Month + t.MStartDate.Day);
                        createNode(t, ww);
                    }
                }
            }
            #endregion
        }
        private void createNode(MTask t, WrapPanel w)
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
        private void LeftMenuButtonNode_Click(object sender, RoutedEventArgs e)
        {
            InitTitleButton(null, sender as MenuButton);
            InitNewButton(null, curMenu);
        }
        #endregion

        #region 数据初始化、检查任务是否过期
        //初始化项目列表
        public void InitProjectList()
        {
            projectList = new List<MProject>();
            DataTable dt = new ProjectBAL().SelectAll(Resource.userName);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    MProject m = new MProject();
                    m.MName = row[1].ToString();
                    m.MDiscription = row[2].ToString();
                    m.MStartDate = DateTime.Parse(row[3].ToString());
                    m.MEndDate = DateTime.Parse(row[4].ToString());
                    m.MUser = row[5].ToString();
                    projectList.Add(m);
                }
            }
        }
        //初始化情境列表
        public void InitSituationList()
        {
            situationList = new List<MSituation>();
            DataTable dt = new SituationBAL().SelectAll(Resource.userName);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string str = row[1].ToString();
                int start = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == ';')
                    {
                        MSituation s = new MSituation();
                        s.MName = str.Substring(start, i - start);
                        s.MUser = Resource.userName;
                        if (s.MName != "") situationList.Add(s);
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
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    MGoal g = new MGoal();
                    g.MName = row[1].ToString();
                    g.MDiscription = row[2].ToString();
                    g.MStartDate = DateTime.Parse(row[3].ToString());
                    g.MEndDate = DateTime.Parse(row[4].ToString());
                    g.MUser = row[5].ToString();
                    goalList.Add(g);
                }
            }
        }
        //初始化任务列表
        public void InitTaskList()
        {
            taskList = new List<MTask>();
            DataTable dt = new TaskBAL().SelectAll(Resource.userName);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    MTask t = new MTask();
                    t.MName = row[1].ToString();
                    t.MDiscription = row[2].ToString();
                    t.MStartDate = DateTime.Parse(row[3].ToString());
                    t.MEndDate = DateTime.Parse(row[4].ToString());
                    t.MType = MTask.stringToTaskType(row[5].ToString());
                    t.MSituation = row[6].ToString();
                    t.MProject = row[7].ToString();
                    t.MGoal = row[8].ToString();
                    t.MState = MTask.stringToTaskState(row[9].ToString());
                    t.MUser = row[10].ToString();
                    taskList.Add(t);
                }
            }
        }
        //更新数据逻辑
        public void CheckTaskState()
        {
            //检测任务是否过期
            foreach (MTask task in taskList)
            {
                if (task.MEndDate.Month < DateTime.Now.Month && task.MEndDate.Day < DateTime.Now.Day)
                {
                    new TaskBAL().UpdateTaskType(task.MName, "过期", Resource.userName);
                }
                if (task.MEndDate == DateTime.Now)
                {
                    new TaskBAL().UpdateTaskType(task.MName, "今日待办", Resource.userName);
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
        //左侧菜单鼠标手势变换
        private void mouseChange(object sender, MouseEventArgs e)
        {
            Cursor = Cursor == Cursors.Hand ? Cursors.Arrow : Cursors.Hand;
        }
        #endregion

        #region 添加任务、项目、目标、情境按钮
        //初始化按钮
        private void InitNewButton(MenuButton button = null, string judge = null)
        {
            spn通用.Children.Clear();
            if (button != null)//对应菜单栏的添加任务、项目、目标、情境按钮
            {
                spn添加.Children.Clear();

                MenuButton m = new MenuButton();
                if (button.Name == "menu收集箱" || button.Name == "menu今日待办" ||
                    button.Name == "menu正在行动" || button.Name == "menu过期" ||
                    button.Name == "menu已完成")
                    m.Text = "新任务";
                else if (button.Name == "menu垃圾箱") m.Text = "清空垃圾箱";
                else if (button.Name == "menu所有项目") m.Text = "新项目";
                else if (button.Name == "menu所有目标") m.Text = "新目标";
                else if (button.Name == "menu所有情境") m.Text = "新情境";
                m.ImagePath = @"Images/新项目.png";
                m.Template = (ControlTemplate)FindResource("MenuButton添加按钮");
                m.MouseEnter += mouseChange;
                m.MouseLeave += mouseChange;
                m.Click += MenuButton_Click;

                spn添加.Children.Add(m);
            }
            else if (judge != null) //对项目、目标、情境的编辑删除按钮
            {
                if (curMenu != "垃圾箱")
                {
                    if (curMenu != "已完成")//已完成菜单栏里的任务不能再有完成任务这一功能了
                    {
                        MenuButton m1 = new MenuButton();
                        if (judge == "wp所有项目") m1.Text = "编辑项目";
                        else if (judge == "wp所有目标") m1.Text = "编辑目标";
                        else if (judge == "wp所有情境" || judge == "menu所有情境") m1.Text = "编辑情境";
                        else if (judge == "已完成") m1.Text = "完成任务";
                        m1.ImagePath = @"Images/新项目.png";
                        m1.Template = (ControlTemplate)FindResource("MenuButton添加按钮");
                        m1.MouseEnter += mouseChange;
                        m1.MouseLeave += mouseChange;
                        m1.Click += MenuButton_Click;
                        spn通用.Children.Add(m1);
                    }

                    MenuButton m2 = new MenuButton();
                    if (judge == "wp所有项目") m2.Text = "删除项目";
                    else if (judge == "wp所有目标") m2.Text = "删除目标";
                    else if (judge == "wp所有情境" || judge == "menu所有情境") m2.Text = "删除情境";
                    else if (judge == "已完成") m2.Text = "删除任务";
                    m2.ImagePath = @"Images/新项目.png";
                    m2.Template = (ControlTemplate)FindResource("MenuButton添加按钮");
                    m2.MouseEnter += mouseChange;
                    m2.MouseLeave += mouseChange;
                    m2.Click += MenuButton_Click;
                    spn通用.Children.Add(m2);
                }
            }
        }
        //新窗口
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MenuButton button = (MenuButton)sender;

            if (button.Text == "新任务")
            {
                NewTask window = new NewTask(taskList, projectList, goalList, situationList);
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
            else if (button.Text == "新情境")
            {
                NewSituation window = new NewSituation(situationList);
                window.Closing += Window_Closing;
                window.Show();
            }
            else if (button.Text == "编辑项目")
            {
                MProject project = new ProjectBAL().Select(curNode);
                NewProject window = new NewProject(projectList, project.MName, project.MDiscription, project.MEndDate.ToString("yyyy-MM-dd"));
                window.Closing += Window_Closing;
                window.Show();
            }
            else if (button.Text == "编辑目标")
            {
                MGoal goal = new GoalBAL().Select(curNode);
                NewGoal window = new NewGoal(goalList, goal.MName, goal.MDiscription, goal.MEndDate.ToString("yyyy-MM-dd"));
                window.Closing += Window_Closing;
                window.Show();
            }
            else if (button.Text == "编辑情境")
            {
                NewSituation window = new NewSituation(situationList, curNode);
                window.Closing += Window_Closing;
                window.Show();
            }
            else if (button.Text == "清空垃圾箱")
            {
                if (MessageBox.Show("是否要清空垃圾箱？", "询问！", MessageBoxButton.YesNo).GetHashCode() == 6)
                {
                    foreach (MTask task in taskList)
                    {
                        if (task.MState.ToString() == "垃圾箱")
                        {
                            new TaskBAL().Delete(task.MName, Resource.userName);
                        }
                    }
                    Window_Closing(null, null);
                }
            }
            else if (button.Text == "删除项目")
            {
                if (OperatorAsk("项目"))
                {
                    new ProjectBAL().Delete(curNode, Resource.userName);
                    foreach (MTask task in taskList)
                    {
                        if (task.MProject == curNode)
                        {
                            new TaskBAL().DeleteByProject(curNode, Resource.userName);
                        }
                    }
                }
                Window_Closing(null, null);
            }
            else if (button.Text == "删除目标")
            {
                if (OperatorAsk("目标"))
                {
                    new GoalBAL().Delete(curNode, Resource.userName);
                    foreach (MTask task in taskList)
                    {
                        if (task.MGoal == curNode)
                        {
                            new TaskBAL().DeleteByGoal(curNode, Resource.userName);
                        }
                    }
                }
                Window_Closing(null, null);
            }
            else if (button.Text == "删除情境")
            {
                if (OperatorAsk("情境"))
                {
                    string s = null;
                    foreach (MSituation situation in situationList)
                    {
                        if (situation.MName != curNode)
                        {
                            s += (situation.MName + ";");
                        }
                    }
                    new SituationBAL().Update(s, Resource.userName);
                    foreach (MTask task in taskList)
                    {
                        if (task.MSituation == curNode)
                        {
                            new TaskBAL().DeleteBySituation(curNode, Resource.userName);
                        }
                    }
                }
                Window_Closing(null, null);
            }
            else if (button.Text == "完成任务")
            {
                new TaskBAL().UpdateTaskState(curSelect.Title, "已完成", Resource.userName);
                spn通用.Children.Clear();
                Window_Closing(null, null);
            }
            else if (button.Text == "删除任务")
            {
                new TaskBAL().UpdateTaskState(curSelect.Title, "垃圾箱", Resource.userName);
                spn通用.Children.Clear();
                Window_Closing(null, null);
            }
        }
        //用户删除项目、目标、情境时进行询问
        private bool OperatorAsk(string type)
        {
            MessageBoxResult result = MessageBox.Show("删除该" + type + "的话，该项目下的任务将会一并删除，是否继续？", "警告", MessageBoxButton.YesNo);
            if (result.GetHashCode() == 7) return false;
            else return true;
        }
        //添加窗体运行完毕后执行数据更新事件
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            #region 添加或修改数据后再次进行数据初始化
            InitProjectList();
            InitSituationList();
            InitGoalList();
            InitTaskList();
            #endregion
            #region 数据修改同步UI
            if (curMenu == "今日待办" || curMenu == "正在行动" || curMenu == "收集箱" || curMenu == "过期" || curMenu == "已完成" || curMenu == "垃圾箱")
            {
                MenuButton button = (MenuButton)spMenu.FindName("menu" + curMenu);
                InitTitleButton(curMenu, button);
            }
            //通过模拟菜单的两次单击来刷新数据
            else if (curMenu == "menu所有项目" || curMenu == "wp所有项目")
            {
                menu所有项目_Click(null, null);
                menu所有项目_Click(null, null);
            }
            else if (curMenu == "menu所有目标" || curMenu == "wp所有目标")
            {
                menu所有目标_Click(null, null);
                menu所有目标_Click(null, null);
            }
            else if (curMenu == "menu所有情境" || curMenu == "wp所有情境")
            {
                menu所有情境_Click(null, null);
                menu所有情境_Click(null, null);
            }
            #endregion
        }
        #endregion

        #region 右侧显示任务列表点击事件处理
        //右侧显示任务日期统计栏点击事件
        private void TitleButton_Click(object sender, RoutedEventArgs e)
        {
            spn通用.Children.Clear();
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
                else if (curMenu == "今日待办" || curMenu == "正在行动" || curMenu == "过期" || curMenu == "收集箱")
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
                else
                {
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
            }
        }
        //右侧显示单个任务点击事件
        private void TitleNodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (curSelect != null) curSelect.Template = (ControlTemplate)FindResource("TitleNodeButton");
            ((Button)sender).Template = (ControlTemplate)FindResource("TitleNodeButton2");
            curSelect = (TitleNodeButton)sender;
            InitNewButton(null, "已完成"); //初始化下侧菜单按钮
        }
        //右侧显示单个任务双击事件
        private void TitleNodeButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            foreach (MTask task in taskList)
            {
                if (task.MName == ((TitleNodeButton)sender).Title)
                {
                    NewTask window = new NewTask(taskList, projectList, goalList, situationList, task.MName, task.MDiscription, task.MEndDate.ToString("yyyy-MM-dd hh:mm:ss"), task.MType.ToString(), task.MProject, task.MGoal, task.MSituation);
                    window.Closing += Window_Closing;
                    window.Show();
                    break;
                }
            }
        }
        #endregion

        #region 插件处理
        #region 外部插件处理，通过反射机制加载外部插件
        private void InitPlug()
        {
            string[] files = Directory.GetFiles("Plugs", "*.dll");
            foreach (string file in files)
            {
                Assembly asm = Assembly.LoadFile(Directory.GetCurrentDirectory() + "/" + file);
                Type[] types = asm.GetExportedTypes();
                foreach (Type type in types)
                {
                    Type p = typeof(IPlug);
                    if (p.IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
                    {
                        IPlug plug = (IPlug)Activator.CreateInstance(type);
                        MenuItem button = new MenuItem();
                        button.Template = (ControlTemplate)FindResource("downMenu");
                        button.Header = plug.Name;
                        button.Click += MenuItem_Click;
                        button.Tag = plug;
                        downMenu.Items.Add(button);
                    }
                }
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            IPlug plug = (IPlug)((MenuItem)sender).Tag;
            plug.Execute(Resource.userName, ConfigurationManager.AppSettings.GetValues("version")[0], DbHelper.provider, DbHelper.getConnectionStringByProvider(DbHelper.provider));
        }
        #endregion

        #region 内部插件处理
        //退出账户
        private void MenuItem退出账户_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否要退出此账户？", "退出！", MessageBoxButton.OKCancel, MessageBoxImage.Question).GetHashCode() == 1)
            {
                new Login().Show();
                this.Close();
            }
        }
        #endregion
        #endregion

    }
}