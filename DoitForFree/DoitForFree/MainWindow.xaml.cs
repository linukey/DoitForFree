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

namespace DoitForFree
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region 事件
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

        private void menu所有项目_Click(object sender, RoutedEventArgs e)
        {
            this.menu所有项目.ImagePath = this.menu所有项目.ImagePath == "Images/下箭头.png" ? "Images/右箭头.png" : "Images/下箭头.png";
        }

        private void menu所有目标_Click(object sender, RoutedEventArgs e)
        {
            this.menu所有目标.ImagePath = this.menu所有目标.ImagePath == "Images/下箭头.png" ? "Images/右箭头.png" : "Images/下箭头.png"; 
        }

        private void menu所有情境_Click(object sender, RoutedEventArgs e)
        {
            this.menu所有情境.ImagePath = this.menu所有情境.ImagePath == "Images/下箭头.png" ? "Images/右箭头.png" : "Images/下箭头.png"; 
        }

        private void menu已完成_Click(object sender, RoutedEventArgs e)
        {
            this.menu已完成.ImagePath = this.menu已完成.ImagePath == "Images/下箭头.png" ? "Images/右箭头.png" : "Images/下箭头.png";
        }

        private void menu垃圾箱_Click(object sender, RoutedEventArgs e)
        {
            this.menu垃圾箱.ImagePath = this.menu垃圾箱.ImagePath == "Images/下箭头.png" ? "Images/右箭头.png" : "Images/下箭头.png";
        }
        #endregion
    }
}