using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien.Forms
{
    public partial class UserInfor : Form
    {
        public UserInfor()
        {
            InitializeComponent();
        }

        public UserInfor(AccountInfor acc)
        {
            InitializeComponent(); ;

            LoadData(acc);
        }

        void LoadData(AccountInfor acc)
        {
            txtFullName.Text = acc.fullName;

            txtUserLogin.Text = acc.userName;
            txtUserLogin.Enabled = false;

            txtPassword.Text = acc.password;

            cboAccountType.SelectedIndex = acc.accountType;

            txtEmail.Text = acc.email;
            txtPhone.Text = acc.phone;
            txtAddress.Text = acc.address;
        }

        public string GetFullName()
        {
            return txtFullName.Text;
        }
        
        public string GetUserLogin()
        {
            return txtUserLogin.Text;
        }
        public string GetPassword()
        {
            return txtPassword.Text;
        }

        public int GetTypeUser()
        {
            return cboAccountType.SelectedIndex;
        }

        public string GetEmail()
        {
            return txtEmail.Text;
        }

        public string GetPhone()
        {
            return txtPhone.Text;
        }

        public string GetAddress()
        {
            return txtAddress.Text;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
