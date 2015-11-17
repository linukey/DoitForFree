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
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        bool bUser = false;
        bool bPasswd = false;

        public Login()
        {
            InitializeComponent();
        }

        #region 窗体处理
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
        
        //设置菜单点击事件
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            this.downMenu.IsOpen = true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
        }
        #endregion

        #region 登录信息处理
        private void btnUser_LostFocus(object sender, RoutedEventArgs e)
        {
            if (btnUser.Text.Trim() == "")
                btnUser.Text = "用户名";
            else if (btnUser.Text.Length > 10)
                btnUser.Text = "用户名限制10字符！";
            else if(btnUser.Text.Trim() != "用户名限制10字符！" && btnUser.Text.Trim() != "用户名")
                bUser = true;
        }

        private void btnUser_GotFocus(object sender, RoutedEventArgs e)
        {
            if (btnUser.Text.Trim() == "用户名")
                btnUser.Text = "";
            else if (btnUser.Text.Trim() == "用户名限制10字符！")
                btnUser.Text = "";
        }

        private void btnPwd_LostFocus(object sender, RoutedEventArgs e)
        {
            if (btnPwd.Text == "")
                btnPwd.Text = "密码";
            else if(btnPwd.Text != "密码")
                bPasswd = true;
        }

        private void btnPwd_GotFocus(object sender, RoutedEventArgs e)
        {
            if (btnPwd.Text == "密码")
                btnPwd.Text = "";
        }
        #endregion

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            btnUser_LostFocus(null, null);
            btnPwd_LostFocus(null, null);
            if (bUser && bPasswd)
            {
                MUser user = new UserBAL().Select(btnUser.Text.Trim());
                if (user == null) MessageBox.Show("用户不存在！");
                else if (user.MPwd != btnPwd.Text.Trim()) MessageBox.Show("密码错误！");
                else
                {
                    new MainWindow().Show();
                    this.Close();
                }
            }
            else MessageBox.Show("请正确填写登录信息！");
        }
    }
}
