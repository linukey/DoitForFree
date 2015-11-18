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
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        private List<MProject> projectList = null; //项目列表
        private List<MSituation> situationList = null; //情境列表
        private List<MGoal> goalList = null; //目标列表
        private List<MTask> taskList = null; //任务列表
        private MenuButton curSelect = null; //当前选中节点

        private const int MenuButtonNodeHeight = 30;
        private const int MenuButtonNodeWidth = 245;

        public MainWindow()
        {
            InitializeComponent();
            InitProjectList();
            InitSituationList();
            InitGoalList();
            InitTaskList();
        }

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

        #region 左侧菜单栏处理
        private void menu所有项目_Click(object sender, RoutedEventArgs e)
        {
            InitMenuButton(projectList, menu所有项目, wp所有项目);
        }

        private void menu所有目标_Click(object sender, RoutedEventArgs e)
        {
            InitMenuButton(goalList, menu所有目标, wp所有目标);
        }

        private void menu所有情境_Click(object sender, RoutedEventArgs e)
        {
            InitMenuButton(situationList, menu所有情境, wp所有情境);
            if (this.menu所有情境.ImagePath == "Images/下箭头.png")
            {
                wp右侧显示.Children.Clear();
                foreach (MSituation s in situationList)
                {
                    MenuButton m = new MenuButton();
                    m.Style = (Style)FindResource("MenuButton情境节点");
                    m.Template = (ControlTemplate)FindResource("MenuButtonTitle");
                    m.Text = s.MName;
                    m.MouseDoubleClick += MenuButton_MouseDoubleClick;
                    m.Click += MenuButton_Click_1;
                    wp右侧显示.Children.Add(m);
                }
            }
        }

        private void menu已完成_Click(object sender, RoutedEventArgs e)
        {
        }

        private void menu垃圾箱_Click(object sender, RoutedEventArgs e)
        {
        }
        #endregion

        #region 数据初始化
        //初始化项目列表
        public void InitProjectList()
        {
            projectList = new List<MProject>();
            DataTable dt = new ProjectBAL().SelectAll(Resource.userName);
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
        //初始化情境列表
        public void InitSituationList()
        {
            situationList = new List<MSituation>();
            DataTable dt = new SituationBAL().SelectAll(Resource.userName);
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
        //初始化目标列表
        public void InitGoalList()
        {
            goalList = new List<MGoal>();
            DataTable dt = new GoalBAL().SelectAll(Resource.userName);
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
        //初始化任务列表
        public void InitTaskList()
        {
            taskList = new List<MTask>();
            DataTable dt = new TaskBAL().SelectAll(Resource.userName);
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
        #endregion

        #region 左侧菜单效果
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
                    MenuButton project = new MenuButton();
                    project.Height = MenuButtonNodeHeight;
                    project.Width = MenuButtonNodeWidth;
                    project.Template = (ControlTemplate)FindResource("MenuButton");
                    project.ImagePath = "Images/情境节点.png";
                    project.Text = mm.MName;
                    wp.Children.Add(project);
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

        #region 添加
        //新任务
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            new NewTask(projectList, goalList, situationList).Show();
        }
        #endregion

        #region 情境节点处理
        //情境节点双击事件
        private void MenuButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("1");
        }
        //情境节点点击事件
        private void MenuButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (curSelect != null)
                curSelect.Template = (ControlTemplate)FindResource("MenuButtonTitle");
            ((MenuButton)sender).Template = (ControlTemplate)FindResource("MenuButtonNode");
            curSelect = (MenuButton)sender;
        }
        #endregion
    }
}