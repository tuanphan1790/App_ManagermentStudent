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

using DevExpress.XtraCharts;
using Model;

namespace QuanLySinhVien.Forms
{
    public partial class BeginForm : UserControl
    {
        FormMain mainForm;

        StudentInforBasicDB db;

        public BeginForm(FormMain form)
        {
            this.mainForm = form;
            db = new StudentInforBasicDB(form.connString);

            InitializeComponent();

            List<string> sess = new List<string>();
            sess.Add("Spring");
            sess.Add("Summer");
            sess.Add("Autumn");
            sess.Add("Winter");
            cboSesson.DataSource = sess;

            var monthNow = DateTime.Now.Month;
            if (monthNow >= 1 && monthNow <= 3)
            {
                cboSesson.Text = "Spring";
            }
            else if (monthNow >= 4 && monthNow <= 6)
            {
                cboSesson.Text = "Summer";
            }
            else if (monthNow >= 7 && monthNow <= 9)
            {
                cboSesson.Text = "Autumn";
            }
            else
            {
                cboSesson.Text = "Winter";
            }

            for (int i = 2018; i <= Convert.ToInt32(DateTime.Now.ToString("yyyy")); i++)
            {
                cboYear.Items.Add(i);
            }

            LoadData();

            lblSesmester1.Text = lblSesmester2.Text = cboYear.Text + "-" + cboSesson.Text;
        }

        List<StudentBasicInfor> listPeople;

        void LoadData()
        {
            int countNotifications = mainForm.GetNoticePaymentDeposit().Count + mainForm.GetPayRent().Count + mainForm.GetVisaRenewal().Count;
            if(countNotifications > 0)
            {
                picImage.Visible = true;
            }
            else
            {
                picImage.Visible = false;
            }

            lblPaymentDeposit.Text = mainForm.GetNoticePaymentDeposit().Count().ToString();
            lblPayRent.Text = mainForm.GetPayRent().Count.ToString();
            lblVisaRenewal.Text = mainForm.GetVisaRenewal().Count.ToString();

            cboYear.Text = DateTime.Now.ToString("yyyy");

            listPeople = db.GetAllStudents(cboYear.Text);
            if (listPeople.Count == 0)
            {
                return;
            }

            CalculatePercent(listPeople);

            Series seriesKobec = new Series("KOBEC Result", ViewType.Pie);
            seriesKobec.Points.Add(new SeriesPoint("Waiting CV", Kobec_studentWaitingCVResult));
            seriesKobec.Points.Add(new SeriesPoint("CV Rejected", Kobec_studentRejectByCV));
            seriesKobec.Points.Add(new SeriesPoint("Waiting Interview", Kobec_studentWaitingInterview));
            seriesKobec.Points.Add(new SeriesPoint("Interview Rejected", Kobec_studentRejectByInterview));
            seriesKobec.Points.Add(new SeriesPoint("Accepted", Kobec_Accepted));

            charKobecResult.Series.Clear();

            // Adjust the text pattern of the series label. 
            PieSeriesLabel label = (PieSeriesLabel)seriesKobec.Label;
            label.TextPattern = "{A}: {V:0}";
            //label.Font.Size
            // Detect overlapping of series labels. 
            label.ResolveOverlappingMode = ResolveOverlappingMode.Default;
            charKobecResult.Series.Add(seriesKobec);

            Series seriesAddmission = new Series("Addmission Result", ViewType.Pie);
            seriesAddmission.Points.Add(new SeriesPoint("Waiting Results", Addmission_studentWaitingResult));
            seriesAddmission.Points.Add(new SeriesPoint("Rejected", Addmission_studentReject));
            seriesAddmission.Points.Add(new SeriesPoint("Accepted", Addmission_Accepted));
            seriesAddmission.Points.Add(new SeriesPoint("Visa Cancel", Visa_Cancel));

            chartAddmissionResult.Series.Clear();
            //Adjust the text pattern of the series label. 
            label = (PieSeriesLabel)seriesAddmission.Label;
            label.TextPattern = "{A}: {V:0}";
            // Detect overlapping of series labels. 
            label.ResolveOverlappingMode = ResolveOverlappingMode.Default;
            chartAddmissionResult.Series.Add(seriesAddmission);

            StatisticStudentInYear();
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            StatisticStudentInYear();

            lblSesmester1.Text = lblSesmester2.Text = cboYear.Text + "-" + cboSesson.Text;

            List<StudentBasicInfor> listStudent = new List<StudentBasicInfor>();
            listStudent = listPeople;

            if (cboSesson.Text != "")
                listStudent = db.SearchStudentFollowSesmester(cboYear.Text, cboSesson.Text);

            if (listStudent.Count == 0)
            {
                MessageBox.Show("No students enter the school in " + cboYear.Text + "-" + cboSesson.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                charKobecResult.Series.Clear();
                chartAddmissionResult.Series.Clear();

                return;
            }

            CalculatePercent(listStudent);

            Series seriesKobec = new Series("KOBEC Result", ViewType.Pie);
            seriesKobec.Points.Add(new SeriesPoint("Waiting CV", Kobec_studentWaitingCVResult));
            seriesKobec.Points.Add(new SeriesPoint("CV Rejected", Kobec_studentRejectByCV));
            seriesKobec.Points.Add(new SeriesPoint("Waiting Interview", Kobec_studentWaitingInterview));
            seriesKobec.Points.Add(new SeriesPoint("Interview Rejected", Kobec_studentRejectByInterview));
            seriesKobec.Points.Add(new SeriesPoint("Accepted", Kobec_Accepted));

            charKobecResult.Series.Clear();

            //Adjust the text pattern of the series label. 
            PieSeriesLabel label = (PieSeriesLabel)seriesKobec.Label;
            label.TextPattern = "{A}: {V:0}";
            // Detect overlapping of series labels. 
            label.ResolveOverlappingMode = ResolveOverlappingMode.Default;
            charKobecResult.Series.Add(seriesKobec);

            Series seriesAddmission = new Series("Addmission Result", ViewType.Pie);
            seriesAddmission.Points.Add(new SeriesPoint("Waiting Results", Addmission_studentWaitingResult));
            seriesAddmission.Points.Add(new SeriesPoint("Rejected", Addmission_studentReject));
            seriesAddmission.Points.Add(new SeriesPoint("Accepted", Addmission_Accepted));
            seriesAddmission.Points.Add(new SeriesPoint("Visa Cancel", Visa_Cancel));

            chartAddmissionResult.Series.Clear();
            // Adjust the text pattern of the series label. 
            label = (PieSeriesLabel)seriesAddmission.Label;
            label.TextPattern = "{A}: {V:0}";
            // Detect overlapping of series labels. 
            label.ResolveOverlappingMode = ResolveOverlappingMode.Default;
            chartAddmissionResult.Series.Add(seriesAddmission);
        }

        int TotalStudent;

        int Kobec_studentWaitingCVResult = 0;
        int Kobec_studentRejectByCV = 0;
        int Kobec_studentWaitingInterview = 0;
        int Kobec_studentRejectByInterview = 0;
        int Kobec_Accepted = 0;

        int Addmission_studentWaitingResult = 0;
        int Addmission_studentReject = 0;
        int Addmission_Accepted = 0;
        int Visa_Cancel = 0;

        void CalculatePercent(List<StudentBasicInfor> listStudents)
        {
            TotalStudent = listStudents.Count;

            Kobec_studentWaitingCVResult = 0;
            Kobec_studentRejectByCV = 0;
            Kobec_studentWaitingInterview = 0;
            Kobec_studentRejectByInterview = 0;
            Kobec_Accepted = 0;

            Addmission_studentWaitingResult = 0;
            Addmission_studentReject = 0;
            Addmission_Accepted = 0;
            Visa_Cancel = 0;

            AdmissionInforDB dbAdmission = new AdmissionInforDB(mainForm.connString);

            foreach (var student in listStudents)
            {
                if (student.cvReview == 0)
                {
                    Kobec_studentWaitingCVResult++;
                }
                else if (student.cvReview == 1)
                {
                    Kobec_studentRejectByCV++;
                }
                else if (student.cvReview == 2 && student.interview == 0)
                {
                    Kobec_studentWaitingInterview++;
                }

                if (student.cvReview == 2 && student.interview == 1)
                {
                    Kobec_studentRejectByInterview++;
                }
                else if (student.cvReview == 2 && student.interview == 2)
                {
                    Kobec_Accepted++;
                }

                if (student.addmissionResult == 0)
                {
                    Addmission_studentWaitingResult++;
                }
                else if (student.addmissionResult == 1)
                {
                    Addmission_studentReject++;
                }
                else if (student.addmissionResult == 2)
                {
                    Addmission_Accepted++;
                }

                if (dbAdmission.CheckVisaCancel(student.studentId))
                    Visa_Cancel++;
            }

            //percent_Kobec_studentWaitingCVResult = ((float)Kobec_studentWaitingCVResult / (float)TotalStudent) * (float)100;
            //percent_Kobec_studentRejectByCV = (float)((float)Kobec_studentRejectByCV / (float)TotalStudent) * (float)100;
            //percent_Kobec_studentWaitingInterviewResult = (float)((float)Kobec_studentWaitingInterview / (float)TotalStudent) * (float)100;
            //percent_Kobec_studentRejectByInterview = (float)((float)Kobec_studentRejectByInterview / (float)TotalStudent) * (float)100;
            //percent_Kobec_Accepted = (float)((float)Kobec_Accepted / (float)TotalStudent) * (float)100;

            //percent_Addmission_studentWaitingResult = (float)((float)Addmission_studentWaitingResult / (float)TotalStudent) * (float)100;
            //percent_Addmission_studentReject = (float)((float)Addmission_studentReject / (float)TotalStudent) * (float)100;
            //percent_Addmission_Accepted = (float)((float)Addmission_Accepted / (float)TotalStudent) * (float)100;
        }

        void StatisticStudentInYear()
        {
            List<StudentBasicInfor> listStudents = new List<StudentBasicInfor>();
            List<StudentBasicInfor> listCandidates = new List<StudentBasicInfor>();

            AdmissionInforDB dbInforEntryKorea = new AdmissionInforDB(mainForm.connString);
            foreach (var x in listPeople)
            {
                if (dbInforEntryKorea.CheckCandidate(x.studentId))
                {
                    listCandidates.Add(x);
                }
                else
                {
                    listStudents.Add(x);
                }
            }

            lblYear.Text = cboYear.Text;
            int studentInyears_Spring = 0;
            int studentInyears_Summer = 0;
            int studentInyears_Autumn = 0;
            int studentInyears_Winter = 0;

            foreach (var student in listStudents)
            {
                if (student.applySesmesterSesson == "Spring")
                {
                    studentInyears_Spring++;
                }

                if (student.applySesmesterSesson == "Summer")
                {
                    studentInyears_Summer++;
                }

                if (student.applySesmesterSesson == "Autumn")
                {
                    studentInyears_Autumn++;
                }

                if (student.applySesmesterSesson == "Winter")
                {
                    studentInyears_Winter++;
                }
            }

            lblToTalStudentInYear_Spring.Text = studentInyears_Spring.ToString();
            lblToTalStudentInYear_Summer.Text = studentInyears_Summer.ToString();
            lblToTalStudentInYear_Autumn.Text = studentInyears_Autumn.ToString();
            lblToTalStudentInYear_Winter.Text = studentInyears_Winter.ToString();

            lblToTalStudent.Text = listStudents.Count.ToString();

            lblCandidateReceied.Text = listCandidates.Count.ToString();
        }
    }
}
