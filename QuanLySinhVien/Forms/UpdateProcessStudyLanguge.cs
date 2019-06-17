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
    public partial class UpdateProcessStudyLanguge : Form
    {
        public UpdateProcessStudyLanguge()
        {
            InitializeComponent();

            LoadCategory();

            dtDateUpdate.DateTime = DateTime.Now;
        }

        public UpdateProcessStudyLanguge(StudentStudyLanguge record)
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

            List<string> level = new List<string>();
            level.Add("Level 1");
            level.Add("Level 2");
            level.Add("Level 3");
            level.Add("Level 4");
            level.Add("Level 5");
            level.Add("Level 6");
            cboLevel.DataSource = level;

            List<string> result = new List<string>();
            result.Add("Fail");
            result.Add("Pass");
            cboResult.DataSource = result;

            validateAttendanPoint = true;
            validateFinalPoint = true;
        }

        void LoadData(StudentStudyLanguge record)
        {
            txtSchoolName.Text = record.schoolName;
            cboYear.Text = record.year;
            cboSesson.Text = record.season;
            cboLevel.Text = record.level;
            cboResult.Text = record.result;
            FormatDateTime(dtEntryDate, record.entryDate);
            FormatDateTime(dtEndDate, record.finalDate);
            txtAttendancePoint.Text = record.attendancePoint.ToString();
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

        public string GetLevel()
        {
            return cboLevel.Text;
        }
        public string GetResult()
        {
            return cboResult.Text;
        }

        public float GetAttandancePoint()
        {
            return Convert.ToSingle(txtAttendancePoint.Text);
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
            if (!validateFinalPoint | !validateAttendanPoint)
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
        bool validateAttendanPoint;
        private void txtAttendancePoint_Validating(object sender, CancelEventArgs e)
        {
            TextBox obj = (TextBox)sender;
            float result;
            bool ret = float.TryParse(obj.Text, out result);
            if (!ret)
            {
                validateAttendanPoint = false;
                MessageBox.Show("Attendance Point you entered is not correct!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                validateAttendanPoint = true;
            }
        }

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
    }
}
