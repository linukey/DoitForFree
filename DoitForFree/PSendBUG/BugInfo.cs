using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data.Common;

namespace PSendBUG
{
    public partial class BugInfo : Form
    {
        public BugInfo(string providername, string conStr)
        {
            InitializeComponent();
            this.providername = providername;
            this.conStr = conStr;
        }
        private string providername;
        private string conStr;
        //提交
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(!Regex.IsMatch(tbxEmail.Text.Trim(), @"^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$"))
            {
                MessageBox.Show("请填写正确的邮箱格式！", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(tbxBug.Text.Trim() == "")
            {
                MessageBox.Show("请填写bug信息！", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string cmdStr = "insert into T_bug(Email, BugInfo) values(@email,@buginfo)";
            DbProviderFactory factory = DbProviderFactories.GetFactory(providername);

            try
            {
                using (DbConnection conn = factory.CreateConnection())
                {
                    conn.ConnectionString = conStr;

                    DbParameter email = factory.CreateParameter();
                    email.ParameterName = "@email";
                    email.Value = tbxEmail.Text.Trim();

                    DbParameter buginfo = factory.CreateParameter();
                    buginfo.ParameterName = "@buginfo";
                    buginfo.Value = tbxBug.Text.Trim();

                    DbCommand cmd = factory.CreateCommand();
                    cmd.CommandText = cmdStr;
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(email);
                    cmd.Parameters.Add(buginfo);

                    conn.Open();

                    if(cmd.ExecuteNonQuery() == 1) MessageBox.Show("发送成功，感谢您的反馈！");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("发送失败！\n" + ex.Message);
                this.Close();
            }
            
        }

        //取消操作
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //实现窗体拖动，坑爹的winform。。。
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x4e:
                case 0xd:
                case 0xe:
                case 0x14:
                    base.WndProc(ref m);
                    break;
                case 0x84://鼠标点任意位置后可以拖动窗体
                    this.DefWndProc(ref m);
                    if (m.Result.ToInt32() == 0x01)
                    {
                        m.Result = new IntPtr(0x02);
                    }
                    break;
                case 0xA3://禁止双击最大化
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }
}