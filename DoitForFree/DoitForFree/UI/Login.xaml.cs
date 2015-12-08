using DoitForFree.BAL;
using DoitForFree.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DoitForFree.UI
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        private bool bUser = false;
        private bool bPasswd = false;

        #region 构造函数
        public Login()
        {
            InitializeComponent();
        }
        #endregion
        
        #region 窗体处理
        //回车事件
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_Click(null, null);
            }
        }
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
        //窗体设置菜单点击事件
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            this.downMenu.IsOpen = true;
        }
        //打开注册窗口
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
        }
        #endregion

        #region 登录信息处理
        //当失去光标时
        private void btn_LostFocus(object sender, RoutedEventArgs e)
        {
            MenuButton button = (MenuButton)sender;
            if (button.Name == "btnUser")
            {
                if (btnUser.Text.Trim() == "") btnUser.Text = "用户名";
                else if (btnUser.Text.Length > 10) btnUser.Text = "用户名限制10字符！";
                else if (btnUser.Text.Trim() != "用户名限制10字符！" && btnUser.Text.Trim() != "用户名") bUser = true;
            }
            else if (button.Name == "btnPwd")
            {
                if (btnPwd.Text == "") btnPwd.Text = "密码";
                else if (btnPwd.Text != "密码") bPasswd = true;
            }
        }
        //当得到光标时
        private void btn_GotFocus(object sender, RoutedEventArgs e)
        {
            MenuButton button = (MenuButton)sender;
            if (button.Name == "btnUser")
            {
                if (btnUser.Text.Trim() == "用户名") btnUser.Text = "";
                else if (btnUser.Text.Trim() == "用户名限制10字符！") btnUser.Text = "";
            }
            else if (button.Name == "btnPwd")
            {
                if (btnPwd.Text == "密码") btnPwd.Text = "";
            }
        }
        #endregion

        #region 提交按钮处理
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            btn_LostFocus(btnUser, null);
            btn_LostFocus(btnPwd, null);
            if (bUser && bPasswd)
            {
                MUser user = new UserBAL().Select(btnUser.Text.Trim());
                if (user == null) MessageBox.Show("用户不存在！");
                else if (user.MPwd != btnPwd.Text.Trim()) MessageBox.Show("密码错误！");
                else
                {
                    Resource.userName = btnUser.Text.Trim();
                    new MainWindow().Show();
                    this.Close();
                }
            }
            else MessageBox.Show("请正确填写登录信息！");
            MessageBox.Show(btnPwd.Text.Trim());
        }
        #endregion
    }
}