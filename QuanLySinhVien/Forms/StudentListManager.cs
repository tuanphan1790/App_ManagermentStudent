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

namespace QuanLySinhVien.Forms
{
    public partial class StudentListManager : UserControl
    {
        FormMain mainForm;

        StudentInforBasicDB db;

        public StudentListManager(FormMain mainForm)
        {
            this.mainForm = mainForm;
            db = new StudentInforBasicDB(mainForm.connString);

            InitializeComponent();

            LoadData();
        }

        void LoadData()
        {
            List<StudentBasicInfor> listStudents = new List<StudentBasicInfor>();

            AdmissionInforDB dbInforEntryKorea = new AdmissionInforDB(mainForm.connString);
            foreach(var x in db.GetAllStudents())
            {
                if(!dbInforEntryKorea.CheckCandidate(x.studentId))
                {
                    listStudents.Add(x);
                }
            }
            List<StudentInforManager> listStudentAfterConvert = new List<StudentInforManager>();

            foreach (var student in listStudents)
            {
                StudentInforManager x = new StudentInforManager(student, mainForm.connString);
                listStudentAfterConvert.Add(x);
            }

            lblNumberStudent.Text = listStudentAfterConvert.Count.ToString();

            gridListStudents.BeginUpdate();
            try
            {
                gridView1.Columns.Clear();
                gridListStudents.DataSource = listStudentAfterConvert;
                gridView1.BestFitColumns();
            }
            finally
            {
                gridListStudents.EndUpdate();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            db = new StudentInforBasicDB(mainForm.connString);

            List<StudentBasicInfor> listPeoples = new List<StudentBasicInfor>();

            if (txtKeyword.Text != "")
            {
                listPeoples = db.SearchStudentFollowKeywordManager(txtKeyword.Text);
            }
            else
            {
                listPeoples = db.GetAllStudents();
            }

            AdmissionInforDB dbInforEntryKorea = new AdmissionInforDB(mainForm.connString);
            List<StudentBasicInfor> listStudents = new List<StudentBasicInfor>();
            foreach (var x in listPeoples)
            {
                if (!dbInforEntryKorea.CheckCandidate(x.studentId))
                {
                    listStudents.Add(x);
                }
            }

            List<StudentInforManager> listStudentAfterConvert = new List<StudentInforManager>();
            foreach (var student in listStudents)
            {
                StudentInforManager x = new StudentInforManager(student, mainForm.connString);
                listStudentAfterConvert.Add(x);
            }

            gridListStudents.BeginUpdate();
            try
            {
                gridView1.Columns.Clear();
                gridListStudents.DataSource = listStudentAfterConvert;
            }
            finally
            {
                gridListStudents.EndUpdate();
            }
        }

        private void barButtonItem_View_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var focusRowView = (StudentInforManager)gridView1.GetFocusedRow();
            mainForm.SetControlStudentManager(db, focusRowView.studentId);
        }

        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            var rowH = gridView1.FocusedRowHandle;
            var focusRowView = (StudentInforManager)gridView1.GetFocusedRow();
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

    }
}
