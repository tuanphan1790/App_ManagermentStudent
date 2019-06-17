using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using DevComponents.DotNetBar.SuperGrid;
using DBManager;

namespace QuanLySinhVien.Forms
{
    public partial class UserManager : UserControl
    {
        FormMain form;
        AccountInforDB accountDB;

        public UserManager(FormMain form, AccountInforDB accountDB)
        {
            this.form = form;
            this.accountDB = accountDB;

            InitializeComponent();

            LoadData();
        }

        void LoadData()
        {
            List<AccountInfor> listAccount = new List<AccountInfor>();
            if (form.user.accountType == 0)
            {
                listAccount = accountDB.GetListUserInfors();
            }
            else
            {
                listAccount.Add(form.user);
            }

            foreach (var acc in listAccount)
            {
                GridPanel panel = sgUserManager.PrimaryGrid;

                string accountType = "";
                switch(acc.accountType)
                {
                    case 0:
                        accountType = "Administrator";
                        break;
                    case 1:
                        accountType = "Manager";
                        break;
                    case 2:
                        accountType = "Student";
                        break;
                }

                sgUserManager.BeginUpdate();
                object[] ob1 = new object[]
                        {
                    acc.id, acc.userName, acc.fullName,acc.email, acc.phone, acc.address, accountType
                        };

                panel.Rows.Add(new GridRow(ob1));
                sgUserManager.EndUpdate();
            }
        }

        int idUser = 0;
        private void sgUserManager_CellMouseUp(object sender, GridCellMouseEventArgs e)
        {
            contextMenuStrip1.Items[0].Enabled = false;
            contextMenuStrip1.Items[1].Enabled = true;
            contextMenuStrip1.Items[2].Enabled = true;

            int rowIndex = sgUserManager.ActiveRow.RowIndex;
            GridCell gridCell = sgUserManager.GetCell(rowIndex, 0);
            idUser = (int)gridCell.Value;
        }

        private void sgUserManager_MouseDown(object sender, MouseEventArgs e)
        {
            contextMenuStrip1.Items[0].Enabled = true;
            contextMenuStrip1.Items[1].Enabled = false;
            contextMenuStrip1.Items[2].Enabled = false;
        }

        private void editUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (form.user.accountType != 0)
            {
                MessageBox.Show("Don't permit edit account");
                return;
            }

            if (idUser == 0)
                return;

            AccountInfor oldAccount = accountDB.GetUserInfor(idUser);
            if (oldAccount != null)
            {
                UserInfor acc = new UserInfor(oldAccount);
                if (acc.ShowDialog() == DialogResult.OK)
                {
                    AccountInfor accInfor = new AccountInfor();
                    accInfor.fullName = acc.GetFullName();
                    accInfor.userName = acc.GetUserLogin();
                    accInfor.password = acc.GetPassword();
                    accInfor.accountType = acc.GetTypeUser();
                    accInfor.email = acc.GetEmail();
                    accInfor.phone = acc.GetPhone();
                    accInfor.address = acc.GetAddress();

                    accountDB.EditUser(accInfor);

                    string accountType = "";
                    switch (accInfor.accountType)
                    {
                        case 0:
                            accountType = "Administrator";
                            break;
                        case 1:
                            accountType = "Manager";
                            break;
                        case 2:
                            accountType = "Student";
                            break;
                    }

                    GridPanel panel = sgUserManager.PrimaryGrid;
                    var IRows = panel.Rows.GetEnumerator();
                    while (IRows.MoveNext())
                    {
                        GridRow r = (GridRow)IRows.Current;
                        if ((int)r[0].Value == oldAccount.id)
                        {
                            r[1].Value = accInfor.userName;
                            r[2].Value = accInfor.fullName;
                            r[3].Value = accInfor.email;
                            r[4].Value = accInfor.phone;
                            r[5].Value = accInfor.address;
                            r[6].Value = accountType;
                        }
                    }
                }
            }

            idUser = 0;
        }

        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (form.user.accountType != 0)
            {
                MessageBox.Show("Don't permit delete account");
                return;
            }

            if (idUser == 0)
            {
                return;
            }
            else
            {
                accountDB.DeleteUser(idUser);

                GridPanel panel = sgUserManager.PrimaryGrid;
                var IRows = panel.Rows.GetEnumerator();
                GridRow _r = null;

                while (IRows.MoveNext())
                {
                    GridRow r = (GridRow)IRows.Current;
                    if ((int)r[0].Value == idUser)
                    {
                        _r = r;
                    }
                }
                panel.Rows.Remove(_r);

                idUser = 0;
            }
        }

        private void createUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (form.user.accountType != 0)
            {
                MessageBox.Show("Don't permit create account");
                return;
            }

            UserInfor acc = new UserInfor();
            if (acc.ShowDialog() == DialogResult.OK)
            {
                AccountInfor accInfor = new AccountInfor();
                accInfor.fullName = acc.GetFullName();
                accInfor.userName = acc.GetUserLogin();
                accInfor.password = acc.GetPassword();
                accInfor.accountType = acc.GetTypeUser();
                accInfor.email = acc.GetEmail();
                accInfor.phone = acc.GetPhone();
                accInfor.address = acc.GetAddress();

                var id = accountDB.AddNewUser(accInfor);

                if (id == -1)
                {
                    MessageBox.Show("Duplicate userLogin");
                    return;
                }

                string accountType = "";
                switch (accInfor.accountType)
                {
                    case 0:
                        accountType = "Administrator";
                        break;
                    case 1:
                        accountType = "Manager";
                        break;
                    case 2:
                        accountType = "Student";
                        break;
                }

                GridPanel panel = sgUserManager.PrimaryGrid;
                sgUserManager.BeginUpdate();

                object[] ob1 = new object[]
                        {
                            id, accInfor.userName, accInfor.fullName,accInfor.email, accInfor.phone, accInfor.address, accountType
                        };

                panel.Rows.Add(new GridRow(ob1));
                sgUserManager.EndUpdate();
            }
        }
    }
}
