using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;
using DoitForFree.BAL;
using System.Text.RegularExpressions;
using DoitForFree.Model;

namespace DoitForFree.UI
{
    /// <summary>
    /// Register.xaml 的交互逻辑
    /// </summary>
    public partial class Register : Window
    {
        #region 字段声明
        HashSet<string> checkEmail = new HashSet<string>(); //现有邮件列表
        HashSet<string> checkUsername = new HashSet<string>(); //现有用户名列表
        //判断信息填写是否已经合格
        bool bUser = false;
        bool bEmail = false;
        bool bPasswd = false;
        #endregion

        #region 构造函数
        public Register()
        {
            InitializeComponent();

            DataTable dt = new UserBAL().SelectAll();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    checkEmail.Add(row["邮箱"].ToString());
                    checkUsername.Add(row["用户编码"].ToString());
                }
            }
        }
        #endregion

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

        #region 提交
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            btn_LostFocus(btnUser, null);
            btn_LostFocus(btnEmail, null);
            btn_LostFocus(btnPasswdCheck, null);
            if (bUser && bEmail && bPasswd)
            {
                MUser user = new MUser();
                user.MName = btnUser.Text.Trim();
                user.MEmail = btnEmail.Text.Trim();
                user.MPwd = btnPasswd.Text.Trim();
                new UserBAL().Add(user);

                //初始化用户默认情境
                MSituation userS = new MSituation();
                userS.MName = "家里;办公室;外出;";
                userS.MUser = Resource.userName;
                new SituationBAL().Add(userS);

                MessageBox.Show("注册成功！");
                this.Close();
            }
            else
            {
                MessageBox.Show("请正确填写用户信息！");
            }
        }
        #endregion

        #region 注册信息处理

        private void btn_LostFocus(object sender, RoutedEventArgs e)
        {
            MenuButton button = (MenuButton)sender;
            if (button.Name == "btnUser")
            {
                if (btnUser.Text.Trim() == "") btnUser.Text = "用户名";
                else if (btnUser.Text.Trim().Length > 10) btnUser.Text = "用户名不能超过10字符！";
                else if (checkUsername.Contains(btnUser.Text.Trim()) == true) btnUser.Text = "用户名已存在！";
                else if (btnUser.Text.Trim() != "用户名" && btnUser.Text.Trim() != "用户名不能超过10字符！" && btnUser.Text.Trim() != "用户名已存在！") bUser = true;
            }
            else if (button.Name == "btnEmail")
            {
                if (btnEmail.Text.Trim() == "") btnEmail.Text = "邮箱";
                else if (!Regex.IsMatch(btnEmail.Text.Trim(), @"^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$")) btnEmail.Text = "邮箱格式错误！";
                else if (checkEmail.Contains(btnEmail.Text.Trim()) == true) btnEmail.Text = "邮箱已存在！";
                else if (btnEmail.Text.Trim() != "邮箱" && btnEmail.Text.Trim() != "邮箱格式错误！" && btnEmail.Text.Trim() != "邮箱已存在！") bEmail = true;
            }
            else if (button.Name == "btnPasswd")
            {
                if (btnPasswd.Text.Trim() == "") btnPasswd.Text = "密码";
            }
            else if (button.Name == "btnPasswdCheck")
            {
                if (btnPasswdCheck.Text.Trim() == "") btnPasswdCheck.Text = "密码确认";
                else if (btnPasswdCheck.Text.Trim() != btnPasswd.Text.Trim()) btnPasswdCheck.Text = "密码不一致！";
                else if (btnPasswdCheck.Text.Trim() != "密码确认" && btnPasswdCheck.Text.Trim() != "密码不一致!") bPasswd = true;
            }
        }

        private void btn_GotFocus(object sender, RoutedEventArgs e)
        {
            MenuButton button = (MenuButton)sender;
            if (button.Name == "btnUser")
            {
                if (btnUser.Text.Trim() == "用户名" || btnUser.Text.Trim() == "用户名已存在！" || btnUser.Text.Trim() == "用户名不能超过10字符！")
                    btnUser.Text = "";
            }
            else if (button.Name == "btnEmail")
            {
                if (btnEmail.Text.Trim() == "邮箱" || btnEmail.Text.Trim() == "邮箱已存在！" || btnEmail.Text.Trim() == "邮箱格式错误！")
                    btnEmail.Text = "";
            }
            else if (button.Name == "btnPasswd")
            {
                if (btnPasswd.Text.Trim() == "密码")
                    btnPasswd.Text = "";
            }
            else if (button.Name == "btnPasswdCheck")
            {
                if (btnPasswdCheck.Text.Trim() == "密码确认" || btnPasswdCheck.Text.Trim() == "密码不一致！")
                    btnPasswdCheck.Text = "";
            }
        }
        #endregion
    }
}