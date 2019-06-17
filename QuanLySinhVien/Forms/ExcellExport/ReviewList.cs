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
using NPOI.XSSF.UserModel;
using System.IO;
using DevComponents.DotNetBar.SuperGrid;
using NPOI.SS.UserModel;
using Model;

namespace QuanLySinhVien.Forms.ExcellExport
{
    public partial class ReviewList : UserControl
    {
        XSSFWorkbook wb;
        XSSFSheet sh;

        StudentInforBasicDB db;

        FormMain formMain;

        string directory;
        public ReviewList(string conString, FormMain form)
        {
            db = new StudentInforBasicDB(conString);
            formMain = form;

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

            List<StudentBasicInfor> listStudents = new List<StudentBasicInfor>();
            AdmissionInforDB dbInforEntryKorea = new AdmissionInforDB(conString);
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

            if (File.Exists(directory + "/file-excel-review-list.xlsx"))
            {
                using (var fs = new FileStream(directory + "/file-excel-review-list.xlsx", FileMode.Open, FileAccess.Read))
                {
                    wb = new XSSFWorkbook(fs);
                    sh = (XSSFSheet)wb.GetSheet("Sheet1");
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //SaveFileDialog dlg = new SaveFileDialog();
            //dlg.Filter = "Excel file|*.xlsx";
            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            GridPanel panel = sgReview.PrimaryGrid;

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

            if (sh.GetRow(1) == null)
                sh.CreateRow(1);

            if (sh.GetRow(1).GetCell(0) == null)
                sh.GetRow(1).CreateCell(0);

            sh.GetRow(1).GetCell(0).CellStyle = cellStyleTitle;

            var icellSesmester = sh.GetRow(1).GetCell(0);
            icellSesmester.SetCellValue(cboYear.Text + "-" + cboSesson.Text);

            for (int i = 0; i < panel.Rows.Count; i++)
            {
                if (sh.GetRow(i + 6) == null)
                    sh.CreateRow(i + 6);

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
                        if (j == 1 | j == 5 | j == 7 | j == 18 | j == 19 | j == 20 | j == 21 | j == 22 | j == 24 | j == 26 
                            | j == 29 | j == 31 | j == 32 | j == 33)
                        {
                            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
                        }
                        cellStyle.VerticalAlignment = VerticalAlignment.Center;

                        if (sh.GetRow(i + 6).GetCell(j) == null)
                            sh.GetRow(i + 6).CreateCell(j);

                        sh.GetRow(i + 6).GetCell(j).CellStyle = cellStyle;

                        var icell = sh.GetRow(i + 6).GetCell(j);
                        icell.SetCellValue(panel.GetCell(i, j).Value.ToString());
                        icell.CellStyle.WrapText = true;
                    }
                }
            }

            using (var fs = new FileStream(directory + "/tempExportExcellReview.xlsx", FileMode.Create, FileAccess.Write))
            {
                wb.Write(fs);
            }

            System.Diagnostics.Process.Start(directory + "/tempExportExcellReview.xlsx");

            //}
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            AdmissionInforDB dbAdmision = new AdmissionInforDB(formMain.connString);
            List<StudentBasicInfor> listPeoples = db.SearchStudentFollowSesmester(cboYear.Text, cboSesson.Text);

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
            sgReview.PrimaryGrid.Rows.Clear();

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

                string higher_schoolName = "";
                string higher_university_college = "";
                string higher_major = "";
                string higher_yearOfGraduation = "";
                string higher_gpa = "";

                var listHigherSchool = student.listHigherSchool;
                var count = listHigherSchool.Count;
                string _r_n = "\r\n";
                for (int k = 0; k < count; k++)
                {
                    if (k == count - 1)
                    {
                        _r_n = "";
                    }

                    higher_schoolName += listHigherSchool[k].schoolName + _r_n;
                    higher_university_college += listHigherSchool[k].schoolType + _r_n;
                    higher_major += listHigherSchool[k].major + _r_n;
                    higher_yearOfGraduation += listHigherSchool[k].yearOfGradualtion.ToString() + _r_n;
                    higher_gpa += listHigherSchool[k].gpa + _r_n;
                }

                GridPanel panel = sgReview.PrimaryGrid;
                sgReview.BeginUpdate();

                string fatherDob = "NA";
                if(student.fatherDob != DateTime.MinValue)
                {
                    fatherDob = student.fatherDob.ToString("yyyy-MM-dd");
                }
                string motherDob = "NA";
                if (student.motherDob != DateTime.MinValue)
                {
                    motherDob = student.motherDob.ToString("yyyy-MM-dd");
                }
                string gradualtionDate = "NA";
                if (student.gradualtionDate != DateTime.MinValue)
                {
                    gradualtionDate = student.gradualtionDate.ToString("yyyy-MM");
                }
                object[] ob1 = new object[]
                        {
                    i, Utilities.ConvertToUnSign2(student.fullName).ToUpper(), gender, student.dateOfBirth.ToString("yyyy-MM-dd"), student.passportNumber,student.homeAddress,student.phone, student.highschoolName,
                    gpa10, gpa11, gpa12, avr, dayoff10, dayoff11, dayoff12, total, gradualtionDate,student.bankBalance.ToString("0.#####"), student.bankAccountOwner.ToUpper(),
                    student.fatherName + " - " + fatherDob, student.fatherJob,
                    student.motherName + " - " + motherDob, student.motherJob, student.numberOfFamily,
                    higher_schoolName, higher_university_college, higher_major, higher_yearOfGraduation, higher_gpa,
                    student.kobecPartner, student.cvRecordDate.ToString("yyyy-MM-dd"),
                    cvReviewKobec, interviewKobec, interviewKobec
                        };

                panel.DefaultRowHeight = 0;
                panel.Rows.Add(new GridRow(ob1));
                sgReview.EndUpdate();

                i++;
            }
        }
    }
}
