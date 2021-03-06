﻿using DoitForFree.BAL;
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
    /// NewGoal.xaml 的交互逻辑
    /// </summary>
    public partial class NewGoal : Window
    {
        #region 字段
        private List<MGoal> goalList = null; //目标列表
        private string WType; //窗口类型
        private string preTitle; //修改前的任务名称
        private enum WindowType { 添加, 修改 };
        #endregion

        #region 构造函数
        public NewGoal()
        {
            InitializeComponent();
        }
        //添加目标的时候
        public NewGoal(List<MGoal> goalList)
        {
            WType = WindowType.添加.ToString();
            InitializeComponent();
            this.goalList = goalList;
        }
        //修改现有目标的时候
        public NewGoal(List<MGoal> goalList, string title, string discription, string enddate)
        {
            this.goalList = goalList;
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
        //点击截止时间
        private void MenuButton截止时间_Click(object sender, RoutedEventArgs e)
        {
            this.MenuCalendar.IsOpen = true;
            this.MenuCalendar.Visibility = Visibility.Visible;
        }
        //日历选择变化时
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime date = (DateTime)Calendar截止时间.SelectedDate;
            this.MenuButton截止时间.Text = date.ToString("yyyy-MM-dd");
        }
        //当光标移到日历控件外面时
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
                foreach(MGoal g in goalList)
                {
                    if(g.MName == tbx标题.Text.Trim())
                    {
                        MessageBox.Show("该目标已经存在！", "警告！", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                MGoal goal = new MGoal();
                goal.MName = tbx标题.Text.Trim();
                goal.MDiscription = tbx描述.Text.Trim();
                goal.MStartDate = DateTime.Now;
                goal.MEndDate = DateTime.Parse(MenuButton截止时间.Text.Trim());
                goal.MUser = Resource.userName;

                if (WType == "添加")
                {
                    new GoalBAL().Add(goal);
                }
                else if (WType == "修改")
                {
                    if (preTitle != tbx标题.Text.Trim())
                    {
                        new GoalBAL().Update(preTitle, goal);
                        new TaskBAL().UpdateGoal(tbx标题.Text.Trim(), Resource.userName, preTitle);
                    }
                }
                this.Close();
            }
        }
        #endregion
    }
}
