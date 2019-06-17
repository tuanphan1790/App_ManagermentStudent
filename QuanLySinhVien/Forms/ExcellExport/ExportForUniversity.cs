using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NPOI.XSSF.UserModel;
using DevComponents.DotNetBar.SuperGrid;
using NPOI.SS.UserModel;
using DBManager;
using Model;

namespace QuanLySinhVien.Forms.ExcellExport
{
    public partial class ExportForUniversity : UserControl
    {
        XSSFWorkbook wb;
        XSSFSheet sh;

        StudentInforBasicDB db;

        FormMain mainForm;

        string directory;

        public ExportForUniversity(string conString, FormMain main)
        {
            this.db = new StudentInforBasicDB(conString);
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

            cboUniversity.DataSource = main.GetUniversity();
            cboUniversity.Text = "";

            List<StudentBasicInfor> listStudents = new List<StudentBasicInfor>();
            AdmissionInforDB dbInforEntryKorea = new AdmissionInforDB(main.connString);
            foreach (var x in db.GetListStudentKobecAccept(cboYear.Text, cboSesson.Text))
            {
                if (dbInforEntryKorea.CheckCandidate(x.studentId))
                {
                    listStudents.Add(x);
                }
            }

            LoadDataAfterFilter(listStudents);

            var exepath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            directory = System.IO.Path.GetDirectoryName(exepath);

            if (File.Exists(directory + "/file-excel-university.xlsx"))
            {
                using (var fs = new FileStream(directory + "/file-excel-university.xlsx", FileMode.Open, FileAccess.Read))
                {
                    wb = new XSSFWorkbook(fs);
                    sh = (XSSFSheet)wb.GetSheet("고백");
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            GridPanel panel = sgUniversity.PrimaryGrid;

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
                if (sh.GetRow(i + 4) == null)
                    sh.CreateRow(i + 4);

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
                        if (j == 1 | j == 7 | j == 9 | j == 10 | j == 13 | j == 14)
                        {
                            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
                        }
                        cellStyle.VerticalAlignment = VerticalAlignment.Center;

                        if (sh.GetRow(i + 4).GetCell(j) == null)
                            sh.GetRow(i + 4).CreateCell(j);

                        sh.GetRow(i + 4).GetCell(j).CellStyle = cellStyle;

                        var icell = sh.GetRow(i + 4).GetCell(j);
                        icell.SetCellValue(panel.GetCell(i, j).Value.ToString());
                        icell.CellStyle.WrapText = true;
                    }
                }
            }

            using (var fs = new FileStream(directory + "/tempExportExcellForUniversity.xlsx", FileMode.Create, FileAccess.Write))
            {
                wb.Write(fs);
            }

            System.Diagnostics.Process.Start(directory + "/tempExportExcellForUniversity.xlsx");
            //}
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            AdmissionInforDB dbAdmision = new AdmissionInforDB(mainForm.connString);
            List<StudentBasicInfor> listPeoples = new List<StudentBasicInfor>();

            var _listPeoples = db.GetListStudentKobecAccept(cboYear.Text, cboSesson.Text);

            if (cboUniversity.Text != "")
            {
                foreach (var x in db.GetListStudentKobecAccept(cboYear.Text, cboSesson.Text))
                {
                    if (x.applyForUniversity == cboUniversity.Text)
                    {
                        listPeoples.Add(x);
                    }
                }
            }
            else
            {
                listPeoples = db.GetListStudentKobecAccept(cboYear.Text, cboSesson.Text);
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
            sgUniversity.PrimaryGrid.Rows.Clear();

            List<StudentBasicInfor> listStudentsUp = new List<StudentBasicInfor>();
            List<StudentBasicInfor> listStudentsLow = new List<StudentBasicInfor>();

            foreach(var x in listStudents)
            {
                if(x.orderKobecAccept == 0)
                {
                    listStudentsLow.Add(x);
                }
                else
                {
                    listStudentsUp.Add(x);
                }
            }

            CompareOrderStudent compare1 = new CompareOrderStudent();
            listStudentsUp.Sort(compare1);

            listStudentsUp.AddRange(listStudentsLow);

            foreach (var student in listStudentsUp)
            {
                var gpa10 = student.gpa10;
                var gpa11 = student.gpa11;
                var gpa12 = student.gpa12;
                var avr = System.Math.Round((gpa10 + gpa11 + gpa12) / 3, 2);

                string gpa = "( " + gpa10.ToString() + " - " + gpa11.ToString() + " - " + gpa12.ToString() + " ) " + "\r\n" + "Avr = " + avr.ToString();

                string gender = "";
                if (student.gender)
                    gender = "M";
                else
                    gender = "F";

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

                string gradualtionDate = "NA";
                if (student.gradualtionDate != DateTime.MinValue)
                {
                    gradualtionDate = student.gradualtionDate.ToString("yyyy-MM");
                }

                string remark = "";
                if (student.remarks != "")
                    remark += student.remarks + "\n";

                //var dbAdmission = new AdmissionInforDB(mainForm.connString);
                //if (dbAdmission.GetRemarkAdmissionInfor(student.studentId) != null)
                //    remark += dbAdmission.GetRemarkAdmissionInfor(student.studentId) + "\n";

                var dbStudentInforKorea = new StudentInforInKoreaDB(mainForm.connString);
                if (dbStudentInforKorea.GetRemarkInforInKorea(student.studentId) != null)
                    remark += dbStudentInforKorea.GetRemarkInforInKorea(student.studentId) + "\n";

                var listHigherSchools = db.GetHigherSchoolsInfor(student.studentId);
                if (listHigherSchools.Count > 0)
                {
                    string higherschoolRemark = "Higherschool Information: ";
                    foreach (var x in listHigherSchools)
                    {
                        higherschoolRemark += "\n";
                        higherschoolRemark += "School Type: " + x.schoolType + " Major: " + x.major + " Graduated in: " + x.yearOfGradualtion;
                    }
                    remark += higherschoolRemark;
                }
                int order = student.orderKobecAccept;
                string _order = "";
                if(order != 0)
                {
                    _order = order.ToString();
                }

                GridPanel panel = sgUniversity.PrimaryGrid;
                sgUniversity.BeginUpdate();
                object[] ob1 = new object[]
                        {
                    _order, Utilities.ConvertToUnSign2(student.fullName).ToUpper(), gender, student.dateOfBirth.ToString("yyyy-MM-dd"), student.passportNumber, passportIssueDate,
                    pasportExpireDate, student.homeAddress, student.phone, student.highschoolName, student.highschoolAddress,
                    gpa, gradualtionDate, student.bankBalance.ToString("0.#####"), student.bankAccountOwner.ToUpper(), remark
                        };

                panel.DefaultRowHeight = 0;
                panel.Rows.Add(new GridRow(ob1));
                panel.ActiveRowIndicatorStyle = ActiveRowIndicatorStyle.None;
                sgUniversity.EndUpdate();
            }
        }
    }

    public class CompareOrderStudent : IComparer<StudentBasicInfor>
    {
        public int Compare(StudentBasicInfor x, StudentBasicInfor y)
        {
            if (x.orderKobecAccept == 0 | y.orderKobecAccept == 0)
                return 0;

            return x.orderKobecAccept.CompareTo(y.orderKobecAccept);
        }
    }
}


