﻿using System;
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
using DoitForFree.Model;
using System.Threading;

namespace DoitForFree.UI
{
    /// <summary>
    /// NewTask.xaml 的交互逻辑
    /// </summary>
    public partial class NewTask : Window
    {
        List<MProject> projectList = null;
        List<MGoal> goalList = null;
        List<MSituation> situationList = null;

        private delegate void Execute();

        public NewTask()
        {
            InitializeComponent();
            btn确定.IsEnabled = false;
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Execute(Checkbtn确定));
        }

        public NewTask(List<MProject> projectList, List<MGoal> goalList, List<MSituation> situationList)
        {
            this.projectList = projectList;
            this.goalList = goalList;
            this.situationList = situationList;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        #region 输入框处理
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.tbx标题.Text.Trim() == "标题")
                this.tbx标题.Text = "";
        }

        private void TextBox_GotFocus_1(object sender, RoutedEventArgs e)
        {
            if (this.tbx描述.Text.Trim() == "描述")
                this.tbx描述.Text = "";
        }

        private void tbx标题_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.tbx标题.Text.Trim() == "")
                this.tbx标题.Text = "标题";
        }

        private void tbx描述_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.tbx描述.Text.Trim() == "")
                this.tbx描述.Text = "描述";
        }

        #endregion

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.MenuCalendar.IsOpen = true;
            this.MenuCalendar.Visibility = Visibility.Visible;
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime date = (DateTime)Calendar截止时间.SelectedDate;
            this.MenuButton截止时间.Text = date.ToString("yyyy-MM-dd");
        }

        private void Calendar截止时间_MouseLeave(object sender, MouseEventArgs e)
        {
            this.MenuCalendar.Visibility = Visibility.Hidden;
        }

        private void MenuButton类型_Click(object sender, RoutedEventArgs e)
        {
            this.Menu类型.IsOpen = true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.MenuButton类型.Text = ((MenuItem)sender).Header.ToString();
            if (this.MenuButton类型.Text == "今日待办")
                MenuButton截止时间.Text = DateTime.Now.ToString("yyyy-MM-dd");
            else if (MenuButton截止时间.Text == DateTime.Now.ToString("yyyy-MM-dd"))
                MenuButton截止时间.Text = "截止时间";
        }

        private void MenuButton添加_Click(object sender, RoutedEventArgs e)
        {
            if (((MenuButton)sender).Name == "Menu项目")
            {
                foreach (MProject project in projectList)
                {
                    MenuItem m = new MenuItem();
                    m.Header = project.MName;
                    m.Template = (ControlTemplate)FindResource("downMenu");
                    ((ContextMenu)sender).Items.Add(m);
                }
            }
            else if (((MenuButton)sender).Name == "Menu目标")
            {
                foreach (MGoal goal in goalList)
                {
                    MenuItem m = new MenuItem();
                    m.Header = goal.MName;
                    m.Template = (ControlTemplate)FindResource("downMenu");
                    ((ContextMenu)sender).Items.Add(m);
                }
            }
            else if (((MenuButton)sender).Name == "Menu情境")
            {
                foreach (MSituation situation in situationList)
                {
                    MenuItem m = new MenuItem();
                    m.Header = situation.MName;
                    m.Template = (ControlTemplate)FindResource("downMenu");
                    ((ContextMenu)sender).Items.Add(m);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Name == "btn取消") this.Close();
            else if (((Button)sender).Name == "btn确定")
            {

            }
        }

        private void Checkbtn确定()
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                while (true)
                {
                    try {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            if (tbx标题.Text.Trim() == "" || tbx标题.Text.Trim() == "标题") btn确定.IsEnabled = false;
                            else if (MenuButton类型.Text != "收集箱" && MenuButton截止时间.Text == "截止时间") btn确定.IsEnabled = false;
                            else btn确定.IsEnabled = true;
                        }));
                    }
                    catch(Exception ex) { }
                };
            });
        }
    }
}
