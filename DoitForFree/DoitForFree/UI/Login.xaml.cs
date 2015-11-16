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
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        #region 事件
        //窗体移动
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        //关闭窗体
        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void btnUser_MouseEnter(object sender, MouseEventArgs e)
        {
            if(this.btnUser.Text == "邮箱/用户名")
            this.btnUser.Text = "";
        }

        private void btnPwd_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.btnPwd.Text == "密码")
                this.btnPwd.Text = "";
        }
        //设置菜单点击事件
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            this.downMenu.IsOpen = true;
        }
        #endregion
    }
}
