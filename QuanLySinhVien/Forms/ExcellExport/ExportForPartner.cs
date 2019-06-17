using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI.XSSF.UserModel;
using DBManager;
using System.IO;
using DevComponents.DotNetBar.SuperGrid;
using NPOI.SS.UserModel;
using Model;

namespace QuanLySinhVien.Forms.ExcellExport
{
    public partial class ExportForPartner : UserControl
    {
        XSSFWorkbook wb;
        XSSFSheet sh;

        StudentInforBasicDB db;

        FormMain mainForm;

        string directory;

        public ExportForPartner(string conString, FormMain main)
        {
            db = new StudentInforBasicDB(conString);

            mainForm = main;

            InitializeComponent();

            List<string> sess = new List<string>();
            sess.Add("Spring");
            sess.Add("Summer");
            sess.Add("Autumn");
            sess.Add("Winter");
            cboSesson.DataSource = sess;

            var monthNow = Convert.ToUInt16(DateTime.Now.ToString("MM"));
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

            cboYear.Text = DateTime.Now.ToString("yyyy");

            cboKobecPartner.DataSource = main.GetKobecPartner();
            cboKobecPartner.Text = "";

            List<StudentBasicInfor> listStudents = new List<StudentBasicInfor>();
            AdmissionInforDB dbInforEntryKorea = new AdmissionInforDB(main.connString);
            foreach (var x in db.SearchStudentFollowSesmester(cboYear.Text, cboSesson.Text))
            {
                if (dbInforEntryKorea.CheckCandidate(x.studentId))
                {
                    listStudents.Add(x);
                }
            }

            LoadDataAfterFilter(listStudents);

            var exepath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            directory = System.IO.Path.GetDirectoryName(exepath);

            if (File.Exists(directory + "/file-excel-partner.xlsx"))
            {
                using (var fs = new FileStream(directory + "/file-excel-partner.xlsx", FileMode.Open, FileAccess.Read))
                {
                    wb = new XSSFWorkbook(fs);
                    sh = (XSSFSheet)wb.GetSheet("ICC+ Final Result");
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            GridPanel panel = sgPartner.PrimaryGrid;

            ICellStyle cellStyleTitle = wb.CreateCellStyle();
            IFont fontTitle = wb.CreateFont();
            fontTitle.FontName = "Arial";
            fontTitle.FontHeightInPoints = 20;
            fontTitle.IsBold = true;
            cellStyleTitle.SetFont(fontTitle);
            cellStyleTitle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            cellStyleTitle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
            cellStyleTitle.VerticalAlignment = VerticalAlignment.Center;

            if (sh.GetRow(0) == null)
                sh.CreateRow(0);

            if (sh.GetRow(0).GetCell(0) == null)
                sh.GetRow(0).CreateCell(0);

            sh.GetRow(0).GetCell(0).CellStyle = cellStyleTitle;

            var icellTitle = sh.GetRow(0).GetCell(0);
            icellTitle.SetCellValue(txtTitle.Text);

            for (int i = 0; i < panel.Rows.Count; i++)
            {
                if (sh.GetRow(i + 5) == null)
                    sh.CreateRow(i + 5);

                for (int j = 0; j < panel.Columns.Count; j++)
                {
                    if (panel.GetCell(i, j).Value != null)
                    {
                        ICellStyle cellStyle = wb.CreateCellStyle();
                        IFont font = wb.CreateFont();
                        font.FontName = "Arial";
                        font.FontHeightInPoints = 10;
                        cellStyle.SetFont(font);
                        cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
                        cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                        if (j == 1 | j == 7 | j == 8 | j == 18 | j == 19 | j == 20 | j == 21 | j == 22 | j == 23)
                        {
                            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
                        }
                        if (j == 1)
                        {
                            font.IsBold = true;
                        }
                        cellStyle.VerticalAlignment = VerticalAlignment.Center;

                        if (sh.GetRow(i + 5).GetCell(j) == null)
                            sh.GetRow(i + 5).CreateCell(j);

                        sh.GetRow(i + 5).GetCell(j).CellStyle = cellStyle;

                        var icell = sh.GetRow(i + 5).GetCell(j);
                        icell.SetCellValue(panel.GetCell(i, j).Value.ToString());
                        icell.CellStyle.WrapText = true;
                    }
                }
            }

            //using (var fs = new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write))
            //{
            //    wb.Write(fs);
            //}

            using (var fs = new FileStream(directory + "/tempExportExcellForPartner.xlsx", FileMode.Create, FileAccess.Write))
            {
                wb.Write(fs);
            }

            System.Diagnostics.Process.Start(directory + "/tempExportExcellForPartner.xlsx");
            //}
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            AdmissionInforDB dbAdmision = new AdmissionInforDB(mainForm.connString);
            List<StudentBasicInfor> listPeoples = new List<StudentBasicInfor>();
            if (cboKobecPartner.Text != "")
            {
                 listPeoples = db.SearchStudentFollowPartnerAndSesmester(cboKobecPartner.Text, cboYear.Text, cboSesson.Text);
            }
            else if (cboKobecPartner.Text == "")
            {
                 listPeoples = db.SearchStudentFollowSesmester(cboYear.Text, cboSesson.Text);
            }
            else
            {
                 listPeoples = db.GetAllStudents();
            }

            List<StudentBasicInfor> listCandidates = new List<StudentBasicInfor>();
            foreach (var x in listPeoples)
            {
                if (dbAdmision.CheckCandidate(x.studentId))
                {
                    listCandidates.Add(x);
                }
            }

            LoadDataAfterFilter(listCandidates);
        }

        void LoadDataAfterFilter(List<StudentBasicInfor> listStudents)
        {
            sgPartner.PrimaryGrid.Rows.Clear();

            int i = 1;
            foreach (var student in listStudents)
            {
                var gpa10 = student.gpa10;
                var gpa11 = student.gpa11;
                var gpa12 = student.gpa12;
                var avr = System.Math.Round((gpa10 + gpa11 + gpa12) / 3, 2);

                var dayoff10 = student.dayOff10;
                var dayoff11 = student.dayOff11;
                var dayoff12 = student.dayOff12;
                var total = dayoff10 + dayoff11 + dayoff12;

                string gender = "";
                if (student.gender)
                    gender = "M";
                else
                    gender = "F";

                string cvReviewKobec = "";
                if (student.cvReview == 0)
                    cvReviewKobec = "NA";
                else if (student.cvReview == 1)
                    cvReviewKobec = "Rejected";
                else if (student.cvReview == 2)
                    cvReviewKobec = "Accepted";

                string interviewKobec = "";
                if (student.interview == 0)
                    interviewKobec = "NA";
                else if (student.interview == 1)
                    interviewKobec = "Rejected";
                else if (student.interview == 2)
                    interviewKobec = "Accepted";

                string addmissionresult = "";
                if (student.addmissionResult == 0)
                    addmissionresult = "NA";
                else if (student.addmissionResult == 1)
                    addmissionresult = "Rejected";
                else if (student.addmissionResult == 2)
                    addmissionresult = "Accepted";

                var pasportExpireDate = "NA";
                if (student.pasportExpireDate != DateTime.MinValue)
                {
                    pasportExpireDate = student.pasportExpireDate.ToString("yyyy-MM-dd");
                }

                var passportIssueDate = "NA";
                if (student.passportIssueDate != DateTime.MinValue)
                {
                    passportIssueDate = student.passportIssueDate.ToString("yyyy-MM-dd");
                }

                var submissionDate = "NA";
                if (student.submissionDate != DateTime.MinValue)
                {
                    submissionDate = student.submissionDate.ToString("yyyy-MM-dd");
                }

                string gradualtionDate = "NA";
                if (student.gradualtionDate != DateTime.MinValue)
                {
                    gradualtionDate = student.gradualtionDate.ToString("yyyy-MM");
                }

                GridPanel panel = sgPartner.PrimaryGrid;
                sgPartner.BeginUpdate();
                object[] ob1 = new object[]
                        {
                    i, Utilities.ConvertToUnSign2(student.fullName).ToUpper(), gender, student.dateOfBirth.ToString("yyyy-MM-dd"), student.passportNumber, passportIssueDate,
                    pasportExpireDate, student.highschoolName, student.highschoolAddress, gradualtionDate,
                    gpa10, gpa11, gpa12, avr, dayoff10, dayoff11, dayoff12, total , student.homeAddress.ToUpper(), cvReviewKobec, interviewKobec, interviewKobec,
                    submissionDate, addmissionresult
                        };

                panel.DefaultRowHeight = 0;
                panel.Rows.Add(new GridRow(ob1));
                sgPartner.EndUpdate();

                i++;
            }
        }

        private void sgPartner_Click(object sender, EventArgs e)
        {

        }
    }
}
