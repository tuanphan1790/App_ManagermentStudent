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
    public partial class ShortList : UserControl
    {
        XSSFWorkbook wb;
        XSSFSheet sh;

        StudentInforBasicDB db;
        AdmissionInforDB dbInKorea;

        string directory;

        FormMain formMain;

        public ShortList(string conString, FormMain form)
        {
            this.db = new StudentInforBasicDB(conString);
            dbInKorea = new AdmissionInforDB(conString);
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

            if (File.Exists(directory + "/file-excel-short-list.xlsx"))
            {
                using (var fs = new FileStream(directory + "/file-excel-short-list.xlsx", FileMode.Open, FileAccess.Read))
                {
                    wb = new XSSFWorkbook(fs);
                    sh = (XSSFSheet)wb.GetSheet("고백");
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //SaveFileDialog dlg = new SaveFileDialog();
            //dlg.Filter = "Excel file|*.xlsx";
            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            GridPanel panel = sgShortList.PrimaryGrid;

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
                        if (j == 1 | j == 5 | j == 8 | j == 13 )
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

            using (var fs = new FileStream(directory + "/tempExportExcellShortlist.xlsx", FileMode.Create, FileAccess.Write))
            {
                wb.Write(fs);
            }

            System.Diagnostics.Process.Start(directory + "/tempExportExcellShortlist.xlsx");

            //}
        }

        void LoadDataAfterFilter(List<StudentBasicInfor> listStudents)
        {
            sgShortList.PrimaryGrid.Rows.Clear();

            int i = 1;
            foreach (var student in listStudents)
            {
                var gpa10 = student.gpa10;
                var gpa11 = student.gpa11;
                var gpa12 = student.gpa12;
                var avr = System.Math.Round((gpa10 + gpa11 + gpa12) / 3, 2);

                string gpa = "( " + gpa10.ToString() + " - " + gpa11.ToString() + " - " + gpa12.ToString() + " ) " + "\r\n" + "Avr = " + avr.ToString();

                var dayoff10 = student.dayOff10;
                var dayoff11 = student.dayOff11;
                var dayoff12 = student.dayOff12;
                var total = dayoff10 + dayoff11 + dayoff12;

                string gender = "";
                if (student.gender)
                    gender = "M";
                else
                    gender = "F";

                string addmissionresult = "";
                if (student.addmissionResult == 0)
                    addmissionresult = "NA";
                else if (student.addmissionResult == 1)
                    addmissionresult = "Rejected";
                else if (student.addmissionResult == 2)
                    addmissionresult = "Accepted";

                var inforBeforeEntryKorea = dbInKorea.GetAdmissionInforDetail(student.studentId);

                string invoiceDate = "NA";
                string tuitionpayamount = "NA";
                string tuitionPaydate = "NA";
                string visaCodeDate = "NA";
                string deposit = "NA";
                string visa = "NA";
                if (inforBeforeEntryKorea != null)
                {
                    if (inforBeforeEntryKorea.invoiceDate != DateTime.MinValue)
                    {
                        invoiceDate = inforBeforeEntryKorea.invoiceDate.ToString("yyyy-MM-dd");
                    }

                    tuitionpayamount = inforBeforeEntryKorea.tuitionPayAmount.ToString("0.#####");

                    if (inforBeforeEntryKorea.tuitionPayDate != DateTime.MinValue)
                    {
                        tuitionPaydate = inforBeforeEntryKorea.tuitionPayDate.ToString("yyyy-MM-dd");
                    }

                    if (inforBeforeEntryKorea.visaCodeDate != DateTime.MinValue)
                    {
                        visaCodeDate = inforBeforeEntryKorea.visaCodeDate.ToString("yyyy-MM-dd");
                    }

                    string depositStartDate = "NA";
                    if (inforBeforeEntryKorea.depositStartDate != DateTime.MinValue)
                    {
                        depositStartDate = inforBeforeEntryKorea.depositStartDate.ToString("yyyy-MM-dd");
                    }
                    deposit = depositStartDate + " " + inforBeforeEntryKorea.depositAmount.ToString("0.#####");

                    visa = inforBeforeEntryKorea.visa;
                }

                string gradualtionDate = "NA";
                if (student.gradualtionDate != DateTime.MinValue)
                {
                    gradualtionDate = student.gradualtionDate.ToString("yyyy-MM");
                }

                var submissionDate = "NA";
                if (student.submissionDate != DateTime.MinValue)
                {
                    submissionDate = student.submissionDate.ToString("yyyy-MM-dd");
                }

                GridPanel panel = sgShortList.PrimaryGrid;
                sgShortList.BeginUpdate();
                object[] ob1 = new object[]
                        {
                    i, Utilities.ConvertToUnSign2(student.fullName).ToUpper(), gender, student.dateOfBirth.ToString("yyyy-MM-dd"), student.passportNumber,student.homeAddress, gpa, gradualtionDate, student.remarks,
                    student.kobecPartner, student.cvRecordDate.ToString("yyyy-MM-dd"), student.applyForUniversity, submissionDate,addmissionresult,
                    invoiceDate, tuitionpayamount, tuitionPaydate,
                    visaCodeDate, visa, deposit, student.remarks
                        };

                panel.DefaultRowHeight = 0;
                panel.Rows.Add(new GridRow(ob1));
                sgShortList.EndUpdate();

                i++;
            }
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
    }
}
