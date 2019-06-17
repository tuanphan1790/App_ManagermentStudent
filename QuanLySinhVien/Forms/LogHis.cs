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
using DBManager;
using DevComponents.DotNetBar.SuperGrid;

namespace QuanLySinhVien.Forms
{
    public partial class LogHis : UserControl
    {
        LogHisDB db;
        FormMain form;

        public LogHis(LogHisDB db, FormMain form)
        {
            this.db = db;
            this.form = form;

            InitializeComponent();

            LoadData();
        }

        void LoadData()
        {
            GridPanel panel = sgLogHis.PrimaryGrid;
            panel.Rows.Clear();
            sgLogHis.BeginUpdate();
            List<HisLogInfor> hisLogs = db.GetListLog();
            hisLogs.Reverse();

            foreach (var log in hisLogs)
            {
                object[] ob1 = new object[]
                    {
                    log.time.ToString("yyyy-MM-dd HH-mm"), log.userName, log.operation
                    };

                panel.Rows.Add(new GridRow(ob1));
            }

            sgLogHis.EndUpdate();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if(form.user.accountType != 0)
            {
                MessageBox.Show("You don't permission!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Do you want clear all log history?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                db.DeleteAllHis();
                sgLogHis.PrimaryGrid.Rows.Clear();
            }
        }
    }
}
