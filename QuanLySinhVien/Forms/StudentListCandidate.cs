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
using Model;
using static DBManager.StudentInforBasicDB;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using QuanLySinhVien.Forms.ExcellExport;

namespace QuanLySinhVien.Forms
{
    public partial class StudentListCandidate : UserControl
    {
        FormMain mainForm;

        StudentInforBasicDB db;

        public StudentListCandidate(FormMain mainForm)
        {
            this.mainForm = mainForm;
            db = new StudentInforBasicDB(mainForm.connString);

            InitializeComponent();

            foreach (GridColumn column in gridView1.Columns)
                column.SortMode = ColumnSortMode.Custom;

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

            if (mainForm.seasonSearch == "")
            {
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
            }
            else
                cboSesson.Text = mainForm.seasonSearch;

            if (mainForm.yearSearch == "")
            {
                cboYear.Text = DateTime.Now.ToString("yyyy");
            }
            else
                cboYear.Text = mainForm.yearSearch;

            LoadData();
        }

        void LoadData()
        {
            if (mainForm.user.accountType == 2)
                return;

            if (mainForm.yearSearch != "" && mainForm.seasonSearch != "")
            {
                SearchSesmester();
            }
            else
            {
                List<StudentBasicInfor> listStudents = new List<StudentBasicInfor>();
                AdmissionInforDB dbInforEntryKorea = new AdmissionInforDB(mainForm.connString);

                foreach (var x in db.SearchStudentFollowSesmester(cboYear.Text, cboSesson.Text))
                {
                    if (dbInforEntryKorea.CheckCandidate(x.studentId))
                    {
                        listStudents.Add(x);
                    }
                }

                var listStudentsAfterFilter = FilterStudent(listStudents);

                List<StudentInforCandidate> listStudentAfterConvert = new List<StudentInforCandidate>();
                foreach (var student in listStudentsAfterFilter)
                {
                    StudentInforCandidate x = new StudentInforCandidate(student, mainForm.connString);
                    listStudentAfterConvert.Add(x);
                }

                lblNumberStudent.Text = listStudentAfterConvert.Count.ToString();

                gridListStudents.BeginUpdate();
                try
                {
                    gridView1.Columns.Clear();
                    gridListStudents.DataSource = listStudentAfterConvert;

                    var colDuplicate = gridView1.Columns["Duplicate"];
                    colDuplicate.Visible = false;

                    var cancel = gridView1.Columns["Cancel"];
                    cancel.Visible = false;

                    gridView1.BestFitColumns();
                }
                finally
                {
                    gridListStudents.EndUpdate();
                }
            }
        }

        List<StudentBasicInfor> FilterStudent(List<StudentBasicInfor> listStudents)
        {
            List<StudentBasicInfor> listStudentsUp = new List<StudentBasicInfor>();
            List<StudentBasicInfor> listStudentsLow = new List<StudentBasicInfor>();

            foreach (var x in listStudents)
            {
                if (x.orderKobecAccept == 0)
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

            return listStudentsUp;
        }

        private void btnAddNewStudent_Click(object sender, EventArgs e)
        {
            mainForm.SetControlStudentManager(db);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            db = new StudentInforBasicDB(mainForm.connString);

            List<StudentBasicInfor> listPeoples = new List<StudentBasicInfor>();

            if (txtKeyword.Text != "")
            {
                listPeoples = db.SearchStudentFollowKeywordCandidate(txtKeyword.Text);
            }
            else
            {
                listPeoples = db.GetAllStudents();
            }

            AdmissionInforDB dbInforEntryKorea = new AdmissionInforDB(mainForm.connString);
            List<StudentBasicInfor> listStudents = new List<StudentBasicInfor>();
            foreach (var x in listPeoples)
            {
                if (dbInforEntryKorea.CheckCandidate(x.studentId))
                {
                    listStudents.Add(x);
                }
            }

            var listStudentsAfterFilter = FilterStudent(listStudents);

            List<StudentInforCandidate> listStudentAfterConvert = new List<StudentInforCandidate>();
            foreach (var student in listStudentsAfterFilter)
            {
                StudentInforCandidate x = new StudentInforCandidate(student, mainForm.connString);
                listStudentAfterConvert.Add(x);
            }

            lblNumberStudent.Text = listStudentAfterConvert.Count.ToString();

            if (mainForm.user.accountType != 2)
            {
                gridListStudents.BeginUpdate();
                try
                {
                    gridView1.Columns.Clear();
                    gridListStudents.DataSource = listStudentAfterConvert;

                    var colDuplicate = gridView1.Columns["Duplicate"];
                    colDuplicate.Visible = false;

                    var cancel = gridView1.Columns["Cancel"];
                    cancel.Visible = false;

                    gridView1.BestFitColumns();
                }
                finally
                {
                    gridListStudents.EndUpdate();
                }
            }
        }

        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            var rowH = gridView1.FocusedRowHandle;
            var focusRowView = (StudentInforCandidate)gridView1.GetFocusedRow();
            if (focusRowView == null)
                return;

            if (rowH >= 0)
            {
                popupMenu1.ShowPopup(barManager1, new Point(MousePosition.X, MousePosition.Y));
            }
            else
            {
                popupMenu1.HidePopup();
            }
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            popupMenu1.HidePopup();
        }

        private void barButtonItem_View_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var focusRowView = (StudentInforCandidate)gridView1.GetFocusedRow();
            mainForm.SetControlStudentManager(db, focusRowView.studentId);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SearchSesmester();
        }

        void SearchSesmester()
        {
            db = new StudentInforBasicDB(mainForm.connString);

            mainForm.yearSearch = cboYear.Text;
            mainForm.seasonSearch = cboSesson.Text;

            List<StudentBasicInfor> listPeoples = db.SearchStudentFollowSesmester(cboYear.Text, cboSesson.Text);

            AdmissionInforDB dbInforEntryKorea = new AdmissionInforDB(mainForm.connString);
            List<StudentBasicInfor> listStudents = new List<StudentBasicInfor>();
            foreach (var x in listPeoples)
            {
                if (dbInforEntryKorea.CheckCandidate(x.studentId))
                {
                    listStudents.Add(x);
                }
            }

            var listStudentsAfterFilter = FilterStudent(listStudents);

            List<StudentInforCandidate> listStudentAfterConvert = new List<StudentInforCandidate>();
            foreach (var student in listStudentsAfterFilter)
            {
                StudentInforCandidate x = new StudentInforCandidate(student, mainForm.connString);
                listStudentAfterConvert.Add(x);
            }

            lblNumberStudent.Text = listStudentAfterConvert.Count.ToString();

            if (mainForm.user.accountType != 2)
            {
                gridListStudents.BeginUpdate();
                try
                {
                    gridView1.Columns.Clear();
                    gridListStudents.DataSource = listStudentAfterConvert;

                    var colDuplicate = gridView1.Columns["Duplicate"];
                    colDuplicate.Visible = false;

                    var cancel = gridView1.Columns["Cancel"];
                    cancel.Visible = false;

                    gridView1.BestFitColumns();
                }
                finally
                {
                    gridListStudents.EndUpdate();
                }
            }
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                var colDuplicate = View.Columns["Duplicate"];
                bool rowDataDuplicate = (bool)View.GetRowCellValue(e.RowHandle, colDuplicate);

                if (rowDataDuplicate)
                {
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.SeaShell;
                    e.HighPriority = true;
                }

                var colCancel = View.Columns["Cancel"];
                bool rowDataCancel = (bool)View.GetRowCellValue(e.RowHandle, colCancel);

                if (rowDataCancel)
                {
                    e.Appearance.BackColor = Color.OrangeRed;
                    e.Appearance.BackColor2 = Color.OrangeRed;
                    e.HighPriority = true;
                }

                var colAdmissionResult = View.Columns["AdmissionResult"];
                string rowAdmissionResult = View.GetRowCellValue(e.RowHandle, colAdmissionResult).ToString();

                if (rowAdmissionResult == "Rejected")
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.BackColor2 = Color.Red;
                    e.HighPriority = true;
                }
            }
        }
    }
}
