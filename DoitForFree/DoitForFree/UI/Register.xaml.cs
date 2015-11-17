using System;
using System.Collections.Generic;
using System.Data;
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
using DoitForFree.BAL;
using System.Text.RegularExpressions;
using DoitForFree.UI;
using DoitForFree.Model;

namespace DoitForFree.UI
{
    /// <summary>
    /// Register.xaml 的交互逻辑
    /// </summary>
    public partial class Register : Window
    {
        HashSet<string> checkEmail = new HashSet<string>();
        HashSet<string> checkUsername = new HashSet<string>();
        bool bUser = false;
        bool bEmail = false;
        bool bPasswd = false;
        public Register()
        {
            InitializeComponent();

            DataTable dt = new UserBAL().SelectAll();
            foreach (DataRow row in dt.Rows)
            {
                checkEmail.Add(row["邮箱"].ToString());
                checkUsername.Add(row["用户编码"].ToString());
            }
        }

        #region 顶层菜单栏处理
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            btnUser_LostFocus(null, null);
            btnEmail_LostFocus(null, null);
            btnPasswdCheck_LostFocus(null, null);
            if (bUser && bEmail && bPasswd)
            {
                MUser user = new MUser();
                user.MName = btnUser.Text.Trim();
                user.MEmail = btnEmail.Text.Trim();
                user.MPwd = btnPasswd.Text.Trim();
                new UserBAL().Add(user);
                MessageBox.Show("注册成功！");
                this.Close();
            }
            else
            {
                MessageBox.Show("请正确填写用户信息！");
            }
        }

        #region 注册信息处理
        private void btnUser_LostFocus(object sender, RoutedEventArgs e)
        {
            if (btnUser.Text.Trim() == "")
                btnUser.Text = "用户名";
            else if (btnUser.Text.Trim().Length > 10)
                btnUser.Text = "用户名不能超过10字符！";
            else if (checkUsername.Contains(btnUser.Text.Trim()) == true)
                btnUser.Text = "用户名已存在！";
            else if (btnUser.Text.Trim() != "用户名" && btnUser.Text.Trim() != "用户名不能超过10字符！"
                && btnUser.Text.Trim() != "用户名已存在！")
                bUser = true;
        }

        private void btnUser_GotFocus(object sender, RoutedEventArgs e)
        {
            if (btnUser.Text.Trim() == "用户名" || btnUser.Text.Trim() == "用户名已存在！" || btnUser.Text.Trim() == "用户名不能超过10字符！")
                btnUser.Text = "";
        }

        private void btnEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (btnEmail.Text.Trim() == "")
                btnEmail.Text = "邮箱";
            else if (!Regex.IsMatch(btnEmail.Text.Trim(), @"^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$"))
                btnEmail.Text = "邮箱格式错误！";
            else if (checkEmail.Contains(btnEmail.Text.Trim()) == true)
                btnEmail.Text = "邮箱已存在！";
            else if (btnEmail.Text.Trim() != "邮箱" && btnEmail.Text.Trim() != "邮箱格式错误！" && btnEmail.Text.Trim() != "邮箱已存在！")
                bEmail = true;
        }

        private void btnEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            if (btnEmail.Text.Trim() == "邮箱" || btnEmail.Text.Trim() == "邮箱已存在！" || btnEmail.Text.Trim() == "邮箱格式错误！")
                btnEmail.Text = "";
        }

        private void btnPasswd_LostFocus(object sender, RoutedEventArgs e)
        {
            if (btnPasswd.Text.Trim() == "")
                btnPasswd.Text = "密码";
        }

        private void btnPasswd_GotFocus(object sender, RoutedEventArgs e)
        {
            if (btnPasswd.Text.Trim() == "密码")
                btnPasswd.Text = "";
        }

        private void btnPasswdCheck_LostFocus(object sender, RoutedEventArgs e)
        {
            if (btnPasswdCheck.Text.Trim() == "")
                btnPasswdCheck.Text = "密码确认";
            else if (btnPasswdCheck.Text.Trim() != btnPasswd.Text.Trim())
                btnPasswdCheck.Text = "密码不一致！";
            else if (btnPasswdCheck.Text.Trim() != "密码确认" && btnPasswdCheck.Text.Trim() != "密码不一致!")
                bPasswd = true;
        }

        private void btnPasswdCheck_GotFocus(object sender, RoutedEventArgs e)
        {
            if (btnPasswdCheck.Text.Trim() == "密码确认" || btnPasswdCheck.Text.Trim() == "密码不一致！")
                btnPasswdCheck.Text = "";
        }
        #endregion
    }
}
