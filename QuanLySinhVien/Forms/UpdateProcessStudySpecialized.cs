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
    public partial class UpdateProcessStudySpecialized : Form
    {
        public UpdateProcessStudySpecialized()
        {
            InitializeComponent();

            LoadCategory();

            dtDateUpdate.DateTime = DateTime.Now;
        }

        public UpdateProcessStudySpecialized(StudentStudySpecificated record)
        {
            InitializeComponent();

            LoadCategory();

            LoadData(record);
        }

        void LoadCategory()
        {
            List<string> sess = new List<string>();
            sess.Add("Spring");
            sess.Add("Summer");
            sess.Add("Autumn");
            sess.Add("Winter");
            cboSesson.DataSource = sess;

            for (int i = 2018; i <= Convert.ToInt32(DateTime.Now.ToString("yyyy")); i++)
            {
                cboYear.Items.Add(i);
            }

            cboYear.Text = DateTime.Now.Year.ToString();

            validateFinalPoint = true;
            validateSchoolarship = true;
        }

        void LoadData(StudentStudySpecificated record)
        {
            txtSchoolName.Text = record.schoolName;
            cboYear.Text = record.year;
            cboSesson.Text = record.season;
            txtSchoolarship.Text = record.schoolarship.ToString();
            FormatDateTime(dtEntryDate, record.entryDate);
            FormatDateTime(dtEndDate, record.finalDate);
            txtFinalPoint.Text = record.finalPoint.ToString();
            FormatDateTime(dtDateUpdate, record.dateUpdate);
            rtbNote.Text = record.note;
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

        public string GetSchoolName()
        {
            return txtSchoolName.Text;
        }

        public DateTime GetEntryDate()
        {
            return dtEntryDate.DateTime;
        }

        public DateTime GetEndDate()
        {
            return dtEntryDate.DateTime;
        }

        public string GetSeason()
        {
            return cboSesson.Text;
        }
        public string GetYear()
        {
            return cboYear.Text;
        }

        public int GetSchoolarship()
        {
            return Convert.ToInt32(txtSchoolarship.Text);
        }
        public float GetFinalPoint()
        {
            return Convert.ToSingle(txtFinalPoint.Text);
        }

        public DateTime GetDateUpdate()
        {
            return DateTime.Now;
        }

        public string GetNote()
        {
            return rtbNote.Text;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!validateFinalPoint | !validateSchoolarship)
            {
                MessageBox.Show("The information you entered is not correct!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        bool validateFinalPoint;
        private void txtFinalPoint_Validating(object sender, CancelEventArgs e)
        {
            TextBox obj = (TextBox)sender;
            float result;
            bool ret = float.TryParse(obj.Text, out result);
            if (!ret)
            {
                validateFinalPoint = false;
                MessageBox.Show("Final Point you entered is not correct!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                validateFinalPoint = true;
            }
        }

        bool validateSchoolarship;
        private void txtSchoolarship_Validating(object sender, CancelEventArgs e)
        {
            TextBox obj = (TextBox)sender;
            int result;
            bool ret = int.TryParse(obj.Text, out result);
            if (!ret)
            {
                validateSchoolarship = false;
                MessageBox.Show("Schoolarship you entered is not correct!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                validateSchoolarship = true;
            }
        }
    }
}
