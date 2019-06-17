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

namespace QuanLySinhVien.Forms.Category
{
    public partial class PartnerForm : Form
    {
        public PartnerForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public string GetIDPartner()
        {
            return txtIDPartner.Text;
        }
        public string GetNamePartner()
        {
            return txtNamePartner.Text;
        }
    }
}
