using DBManager;
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
    public partial class HigherEducation : Form
    {
        public HigherEducation(FormMain form)
        {
            InitializeComponent();

            cboHigherschoolType.DataSource =  form.GetListLevels();
        }

        public string GetSchoolName()
        {
            return txtHigherschoolName.Text;
        }

        public string GetHigherSchoolType()
        {
            return cboHigherschoolType.Text;
        }

        public string GetMajor()
        {
            return txtMajor.Text;
        }

        public string GetYearOfGradualtion()
        {
            return txtYearOfGraduation.Text;
        }
        public string GetGPA()
        {
            return txtGPA.Text;
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
