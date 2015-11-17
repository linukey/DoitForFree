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
            this.menu所有项目.ImagePath = this.menu所有项目.ImagePath == "Images/下箭头.png" ? "Images/右箭头.png" : "Images/下箭头.png";
            if (this.menu所有项目.ImagePath == "Images/下箭头.png")
            {
                foreach(MProject m in projectList)
                {
                    MenuButton project = new MenuButton();
                    project.Height = MenuButtonNodeHeight;
                    project.Width = MenuButtonNodeWidth;
                    project.Template = (ControlTemplate)FindResource("MenuButton");
                    project.ImagePath = "Images/情境节点.png";
                    project.Text = m.MName;
                    wp所有项目.Children.Add(project);
                }
            }
            else wp所有项目.Children.Clear();
        }

        private void menu所有目标_Click(object sender, RoutedEventArgs e)
        {
            this.menu所有目标.ImagePath = this.menu所有目标.ImagePath == "Images/下箭头.png" ? "Images/右箭头.png" : "Images/下箭头.png";
            if (this.menu所有目标.ImagePath == "Images/下箭头.png")
            {
                foreach(MGoal g in goalList)
                {
                    MenuButton goal = new MenuButton();
                    goal.Height = MenuButtonNodeHeight;
                    goal.Width = MenuButtonNodeWidth;
                    goal.Template = (ControlTemplate)FindResource("MenuButton");
                    goal.ImagePath = "Images/情境节点.png";
                    goal.Text = g.MName;
                    wp所有目标.Children.Add(goal);
                }
            }
            else wp所有目标.Children.Clear();
        }

        private void menu所有情境_Click(object sender, RoutedEventArgs e)
        {
            this.menu所有情境.ImagePath = this.menu所有情境.ImagePath == "Images/下箭头.png" ? "Images/右箭头.png" : "Images/下箭头.png";
            if (this.menu所有情境.ImagePath == "Images/下箭头.png")
            {
                foreach(MSituation s in situationList)
                {
                    MenuButton situation = new MenuButton();
                    situation.Height = MenuButtonNodeHeight;
                    situation.Width = MenuButtonNodeWidth;
                    situation.Template = (ControlTemplate)FindResource("MenuButton");
                    situation.ImagePath = "Images/情境节点.png";
                    situation.Text = s.MName;
                    wp所有情境.Children.Add(situation);
                }
            }
            else wp所有情境.Children.Clear();
        }

        private void menu已完成_Click(object sender, RoutedEventArgs e)
        {
            this.menu已完成.ImagePath = this.menu已完成.ImagePath == "Images/下箭头.png" ? "Images/右箭头.png" : "Images/下箭头.png";
            if (this.menu已完成.ImagePath == "Images/下箭头.png")
            {
                foreach (MTask t in taskList)
                {
                    if (t.MState == MTask.TaskState.已完成)
                    {
                        MenuButton task = new MenuButton();
                        task.Height = MenuButtonNodeHeight;
                        task.Width = MenuButtonNodeWidth;
                        task.Template = (ControlTemplate)FindResource("MenuButton");
                        task.ImagePath = "Images/情境节点.png";
                        task.Text = t.MName;
                        wp已完成.Children.Add(task);
                    }
                }
            }
            else wp已完成.Children.Clear();
        }

        private void menu垃圾箱_Click(object sender, RoutedEventArgs e)
        {
            this.menu垃圾箱.ImagePath = this.menu垃圾箱.ImagePath == "Images/下箭头.png" ? "Images/右箭头.png" : "Images/下箭头.png";
            if (this.menu垃圾箱.ImagePath == "Images/下箭头.png")
            {
                foreach (MTask t in taskList)
                {
                    if (t.MState == MTask.TaskState.垃圾箱)
                    {
                        MenuButton task = new MenuButton();
                        task.Height = MenuButtonNodeHeight;
                        task.Width = MenuButtonNodeWidth;
                        task.Template = (ControlTemplate)FindResource("MenuButton");
                        task.ImagePath = "Images/情境节点.png";
                        task.Text = t.MName;
                        wp垃圾箱.Children.Add(task);
                    }
                }
            }
            else wp垃圾箱.Children.Clear();
        }
        #endregion

        #region 数据初始化
        //初始化项目列表
        public void InitProjectList()
        {
            projectList = new List<MProject>();
            DataTable dt = new ProjectBAL().SelectAll();
            foreach (DataRow row in dt.Rows)
            {
                MProject m = new MProject();
                m.MName = row["项目名称"].ToString();
                m.MDiscription = row["项目描述"].ToString();
                m.MStartDate = DateTime.Parse(row["开始时间"].ToString());
                m.MEndDate = DateTime.Parse(row["截止时间"].ToString());
                m.MUser = row["用户编码"].ToString();
                projectList.Add(m);
;            }
        }
        //初始化情境列表
        public void InitSituationList()
        {
            situationList = new List<MSituation>();
            DataTable dt = new SituationBAL().SelectAll();
            foreach(DataRow row in dt.Rows)
            {
                MSituation s = new MSituation();
                s.MName = row["情境名称"].ToString();
                s.MUser = row["用户编码"].ToString();
                situationList.Add(s);
            }
        }
        //初始化目标列表
        public void InitGoalList()
        {
            goalList = new List<MGoal>();
            DataTable dt = new GoalBAL().SelectAll();
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
            DataTable dt = new TaskBAL().SelectAll();
            foreach(DataRow row in dt.Rows)
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
}
}