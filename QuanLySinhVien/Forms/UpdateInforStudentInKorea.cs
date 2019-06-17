using DBManager;
using DevExpress.XtraEditors;
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
    public partial class UpdateInforStudentInKorea : Form
    {
        public UpdateInforStudentInKorea()
        {
            InitializeComponent();

            dtDateUpdate.DateTime = DateTime.Now;
        }

        public UpdateInforStudentInKorea(StudentInforInKorea record, bool newRecord)
        {
            InitializeComponent();

            if (newRecord)
            {
                dtDateUpdate.DateTime = DateTime.Now;
            }

            LoadData(record);
        }

        void LoadData(StudentInforInKorea record)
        {
            txtNoIdentifierKorea.Text = record.NoIdentifierKorea;
            FormatDateTime(dtIdentifierExpireDate, record.IdentifierExpireDate);
            txtContact.Text = record.contact;
            txtAddress.Text = record.address;
            FormatDateTime(dtAddressEntry, record.addressEntry);
            FormatDateTime(dtAddressEnd, record.addressEnd);

            switch (record.addressManager)
            {
                case 1:
                    cboAddressManager.Text = "Yes";
                    break;
                case 2:
                    cboAddressManager.Text = "No";
                    break;
                case 3:
                    cboAddressManager.Text = "Custom";
                    break;
            }

            rtbAddressNote.Text = record.addressNote;
            txtUserAccount.Text = record.userAccount;
            txtNumAccount.Text = record.numberAccount;
            txtMoney.Text = record.money;
            txtManagementName.Text = record.managerName;
            txtManagementPhone.Text = record.managerPhone;
            FormatDateTime(dtDateUpdate, record.dateUpdate);
            txtPartTimeJob.Text = record.parttimeJob;
            txtPartTimeAddress.Text = record.parttimeAddress;
            FormatDateTime(dtPartTimeEntry, record.parttimeEntry);
            FormatDateTime(dtPartTimeEnd, record.parttimeEnd);
            rtbPartTimeNote.Text = record.parttimeNote;
        }

        private void FormatDateTime(DateEdit date1, DateTime date2)
        {
            if (date2.Ticks == 0)
            {
                date1.Properties.NullText = String.Empty;
            }
            else
            {
                date1.DateTime = date2;
            }
        }

        public string GetNoIdntifier()
        {
            return txtNoIdentifierKorea.Text;
        }
        public DateTime GetIdentifierExpireDate()
        {
            return dtIdentifierExpireDate.DateTime;
        }

        public string GetContact()
        {
            return txtContact.Text;
        }

        public string GetAddress()
        {
            return txtAddress.Text;
        }
        public DateTime GetAddressEntry()
        {
            return dtAddressEntry.DateTime;
        }
        public DateTime GetAddressEnd()
        {
            return dtAddressEnd.DateTime;
        }
        public int GetAddressManager()
        {
            switch (cboAddressManager.Text)
            {
                case "Yes":
                    return 1;
                case "No":
                    return 2;
                case "Custom":
                    return 3;
            }

            return 0;
        }

        public string GetStrAddressManager()
        {
            return cboAddressManager.Text;
        }

        public string GetAddressNote()
        {
            return rtbAddressNote.Text;
        }
        public DateTime GetPaymentDate()
        {
            return dtAddressEntry.DateTime;
        }
        public string GetNumberAccount()
        {
            return txtNumAccount.Text;
        }
        public string GetUserAccount()
        {
            return txtUserAccount.Text;
        }

        public string GetMoney()
        {
            return txtMoney.Text;
        }
        public string GetManagementName()
        {
            return txtManagementName.Text;
        }
        public string GetManagementPhone()
        {
            return txtManagementPhone.Text;
        }
        public DateTime GetDateUpdate()
        {
            return dtDateUpdate.DateTime;
        }
        public string GetParttimeJob()
        {
            return txtPartTimeJob.Text;
        }
        public string GetPartTimeAddress()
        {
            return txtPartTimeAddress.Text;
        }
        public DateTime GetParttimeEntry()
        {
            return dtPartTimeEntry.DateTime;
        }
        public DateTime GetParttimeEnd()
        {
            return dtPartTimeEnd.DateTime;
        }
        public string GetParttimeNote()
        {
            return rtbPartTimeNote.Text;
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
