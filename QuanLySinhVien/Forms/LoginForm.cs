using DBManager;
using DevComponents.DotNetBar;
using Model;
using QuanLySinhVien.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class LoginForm : Form
    {
        public bool IsSuccess { get; set; }

        AccountInforDB accountDB;
        LogHisDB logHisDB;

        FormMain formMain;

        public LoginForm(FormMain formMain, AccountInforDB accountDB, LogHisDB logHisDB)
        {
            this.formMain = formMain;
            this.accountDB = accountDB;
            this.logHisDB = logHisDB;

            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Length > 0 && txtPassword.Text.Length > 0)
            {
                bool check = accountDB.CheckAccountLogin(txtUserName.Text, txtPassword.Text);
                if (!check)
                {
                    MessageBox.Show("Login failure!");
                    return;
                }

                IsSuccess = true;

                AccountInfor user = accountDB.GetUserInfor(txtUserName.Text);
                formMain.user = user;

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Bạn chưa điền tên hoặc password!");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            TaskDialogInfo tResult = new TaskDialogInfo("Exit", eTaskDialogIcon.Information, "", "Do you want exit program?", eTaskDialogButton.Yes);
            if (TaskDialog.Show(tResult) == eTaskDialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtUserName.Text.Length > 0 && txtPassword.Text.Length > 0)
                {
                    bool check = accountDB.CheckAccountLogin(txtUserName.Text, txtPassword.Text);
                    if (!check)
                    {
                        MessageBox.Show("Login failure!");
                        return;
                    }

                    IsSuccess = true;

                    AccountInfor user = accountDB.GetUserInfor(txtUserName.Text);
                    HisLogInfor logInfor = new HisLogInfor();
                    logInfor.operation = user.fullName + " is logon";
                    logInfor.time = DateTime.Now;
                    logInfor.userName = user.fullName;

                    formMain.user = user;

                    logHisDB.AddLog(logInfor);

                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Bạn chưa điền tên hoặc password!");
                }
            }
        }
    }
}
