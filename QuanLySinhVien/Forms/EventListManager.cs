using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBManager;
using DevComponents.DotNetBar.SuperGrid;

namespace QuanLySinhVien.Forms
{
    public partial class EventListManager : UserControl
    {
        FormMain form;

        public EventListManager(FormMain form)
        {
            this.form = form;

            InitializeComponent();

            LoadData();
        }

        void LoadData()
        {
            var noticePaymentDeposits = form.GetNoticePaymentDeposit();
            foreach (var record in noticePaymentDeposits)
            {
                GridPanel panel = sgPaymentDeposit.PrimaryGrid;
                sgPaymentDeposit.BeginUpdate();
                object[] ob1 = new object[]
                        {
                    record.id, record.nameStudent, record.guaranteeName, record.money, record.paymentDate.ToString("yyyy-MM-dd"),
                    record.expirationDate.ToString("yyyy-MM-dd"), record.interestRate, record.totalMoney, record.status
                        };

                panel.DefaultRowHeight = 0;
                panel.Rows.Add(new GridRow(ob1));
                sgPaymentDeposit.EndUpdate();
            }

            if(noticePaymentDeposits.Count > 0)
            {
                lblLastedUpdatePaymentDeposit.Text = noticePaymentDeposits.Last().lastedUpdate;
            }


            var visaRenewals = form.GetVisaRenewal();
            foreach (var record in visaRenewals)
            {
                GridPanel panel = sgVisaRenewal.PrimaryGrid;
                sgVisaRenewal.BeginUpdate();
                object[] ob1 = new object[]
                        {
                    record.id,record.nameStudent, record.studentId, record.expirationDate.ToString("yyyy-MM-dd"), record.address, record.phoneStudent,
                    record.phoneManager, record.status
                        };

                panel.DefaultRowHeight = 0;
                panel.Rows.Add(new GridRow(ob1));
                sgVisaRenewal.EndUpdate();
            }

            if(visaRenewals.Count > 0)
            {
                lblLastedUpdateVisaRenew.Text = visaRenewals.Last().lastedUpdate;
            }


            var payRents = form.GetPayRent();
            foreach (var record in payRents)
            {
                GridPanel panel = sgPayRent.PrimaryGrid;
                sgPayRent.BeginUpdate();
                object[] ob1 = new object[]
                        {
                    record.id, record.address, record.numberAccount, record.money, record.paymentDate.ToString("yyyy-MM-dd"), record.nameStudent,
                            record.status
                        };

                panel.DefaultRowHeight = 0;
                panel.Rows.Add(new GridRow(ob1));
                sgPayRent.EndUpdate();
            }

            if(payRents.Count > 0)
            {
                lblLastedUpdatePayrent.Text = payRents.Last().lastedUpdate;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            StudentEventManagerDB db = new StudentEventManagerDB(form.connString);

            var panelPaymentDeposit = sgPaymentDeposit.PrimaryGrid;
            var IRows = panelPaymentDeposit.Rows.GetEnumerator();
            while (IRows.MoveNext())
            {
                GridRow r = (GridRow)IRows.Current;
                db.ChangeStatusPaymentDeposit((int)r[0].Value, (bool)r[8].Value, form.user.fullName);

                if((bool)r[8].Value)
                {
                    form.SetHisOperate("Accepted payment deposit for StudentName= " + r[1].Value.ToString());
                }
            }

            var panelVisaRenewal = sgVisaRenewal.PrimaryGrid;
            IRows = panelVisaRenewal.Rows.GetEnumerator();
            while (IRows.MoveNext())
            {
                GridRow r = (GridRow)IRows.Current;
                db.ChangeStatusVisaRenewal((int)r[0].Value, (bool)r[7].Value, form.user.fullName);

                if ((bool)r[7].Value)
                {
                    form.SetHisOperate("Accepted visa renewal for StudentName= " + r[1].Value.ToString());
                }
            }

            var panelPayrent = sgPayRent.PrimaryGrid;
            IRows = panelPayrent.Rows.GetEnumerator();
            while (IRows.MoveNext())
            {
                GridRow r = (GridRow)IRows.Current;
                db.ChangeStatusPayRent((int)r[0].Value, (bool)r[6].Value, form.user.fullName);

                if ((bool)r[6].Value)
                {
                    form.SetHisOperate("Accepted pay rent for StudentName= " + r[5].Value.ToString());
                }
            }

            MessageBox.Show("Update success!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
