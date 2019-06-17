using System;
using System.Collections.Generic;
using DBManager;
using Model;
using QuanLySinhVien.Forms.Category;
using QuanLySinhVien.Forms.ExcellExport;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using QuanLySinhVien.Forms;
using DevComponents.DotNetBar;

namespace QuanLySinhVien.Forms
{
    public partial class FormMain : Form
    {
        bool IsOK = false;
        public string connString;

        AccountInforDB accountDB;
        LogHisDB hisLogDB;
        StudentEventManagerDB eventDB;
        CategoryManagerDB catDB;

        public AccountInfor user { set; get; }

        public FormMain()
        {
            InitializeComponent();

            var exepath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(exepath);
            XElement XSettingPath = XElement.Load(directory + "/setting.xml");
            var XConnString = XSettingPath.Element("connectionStringDB");
            connString = XConnString.Attribute("path").Value;

            accountDB = new AccountInforDB(connString);
            hisLogDB = new LogHisDB(connString);
            catDB = new CategoryManagerDB(connString);

            LoginForm login = new LoginForm(this, accountDB, hisLogDB);
            if (login.ShowDialog() == DialogResult.OK)
            {
                if (login.IsSuccess)
                {
                    this.WindowState = FormWindowState.Maximized;

                    ShowFormBegin();

                    switch(user.accountType)
                    {
                        case 1:
                            accSystemManager.Visible = false;
                            break;
                        case 2:
                            accStudent.Visible = false;
                            accExportExcel.Visible = false;
                            accNotification.Visible = false;
                            accSystemManager.Visible = false;
                            break;
                    }

                    IsOK = BeginInit();

                    if (!IsOK)
                    {
                        MessageBox.Show("Don't enough data in category, you need set data for this!");
                    }
                }
            }
        }

        List<StudentLevel> listLevels = new List<StudentLevel>();
        List<UniversityInfor> listUniversitys = new List<UniversityInfor>();
        List<PartnerInfor> listKobecs = new List<PartnerInfor>();

        public List<StudentLevel> GetListLevels()
        {
            return listLevels;
        }

        public List<UniversityInfor> GetUniversity()
        {
            return listUniversitys;
        }

        public List<PartnerInfor> GetKobecPartner()
        {
            return listKobecs;
        }

        private bool BeginInit()
        {
            listLevels = catDB.GetAllLevel();
            if (listLevels.Count == 0)
                return false;

            listUniversitys = catDB.GetAllUniversity();
            if (listUniversitys.Count == 0)
                return false;

            listKobecs = catDB.GetAllPartner();
            if (listKobecs.Count == 0)
                return false;

            CompareUniversity compare1 = new CompareUniversity();
            listUniversitys.Sort(compare1);

            ComparePartner compare2 = new ComparePartner();
            listKobecs.Sort(compare2);

            return true;
        }

        public List<NoticePaymentDeposit> GetNoticePaymentDeposit()
        {
            eventDB = new StudentEventManagerDB(connString);

            var dateNow = DateTime.Now;

            List<NoticePaymentDeposit> listPaymentDeposit = new List<NoticePaymentDeposit>();
            var allPaymentDeposits = eventDB.GetAllNoticePaymentDeposit();
            foreach (var x in allPaymentDeposits)
            {
                var expiradate = x.expirationDate;
                if (expiradate.Ticks == 0)
                    continue;

                double numberOfDay = (expiradate - dateNow).TotalDays;
                if (numberOfDay < 30 && x.status == false)
                {
                    listPaymentDeposit.Add(x);
                }
            }

            return listPaymentDeposit;
        }

        public List<NoticeVisaRenewal> GetVisaRenewal()
        {
            eventDB = new StudentEventManagerDB(connString);

            var dateNow = DateTime.Now;

            List<NoticeVisaRenewal> listVisaRenewal = new List<NoticeVisaRenewal>();
            var allVisaRenewals = eventDB.GetAllVisarenewal();
            foreach (var x in allVisaRenewals)
            {
                var expirateDate = x.expirationDate;
                if (expirateDate.Ticks == 0)
                    continue;

                double numberOfdays = (expirateDate - dateNow).TotalDays;
                if (numberOfdays < 30 && x.status == false)
                {
                    listVisaRenewal.Add(x);
                }
            }

            return listVisaRenewal;
        }

        public List<NoticePayRent> GetPayRent()
        {
            eventDB = new StudentEventManagerDB(connString);

            var dateNow = DateTime.Now;

            List<NoticePayRent> listPayrent = new List<NoticePayRent>();
            var allPayRents = eventDB.GetAllListPayRent();
            foreach (var x in allPayRents)
            {
                var expirateDate = x.paymentDate;
                if (expirateDate.Ticks == 0)
                    continue;

                double numberOfdays = expirateDate.Day - dateNow.Day;
                if (numberOfdays < 10 && x.nowMonth <= DateTime.Now.Month)
                {
                    listPayrent.Add(x);
                }
            }

            return listPayrent;
        }

        public void SetControlStudentListCandidate()
        {
            ResetInforStudent();

            if (IsOK)
            {
                ResetPanel();

                StudentListCandidate studentCandidate = new StudentListCandidate(this);
                studentCandidate.Dock = DockStyle.Fill;
                panelMain.Controls.Add(studentCandidate);
                studentCandidate.Show();
            }
            else
            {
                MessageBox.Show("You need set data for category!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SetControlStudentManager(StudentInforBasicDB db)
        {
            ResetInforStudent();

            if (IsOK)
            {

                ResetPanel();

                StudentManager studentManager = new StudentManager(this, db);
                studentManager.Dock = DockStyle.Fill;
                panelMain.Controls.Add(studentManager);
                studentManager.Show();
            }
            else
            {
                MessageBox.Show("You need set data for category!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SetControlStudentManager(StudentInforBasicDB db, string studentId)
        {
            if (IsOK)
            {
                ResetPanel();

                StudentManager studentManager = new StudentManager(this, db, studentId);
                studentManager.Dock = DockStyle.Fill;
                panelMain.Controls.Add(studentManager);
                studentManager.Show();
            }
            else
            {
                MessageBox.Show("You need set data for category!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowFormBegin()
        {
            ResetInforStudent();

            ResetPanel();

            BeginForm bg = new BeginForm(this);
            bg.Dock = DockStyle.Fill;
            panelMain.Controls.Add(bg);
            bg.Show();
        }

        public void SetHisOperate(string action)
        {
            HisLogInfor logInfor = new HisLogInfor();
            logInfor.operation = action;
            var src = DateTime.Now;
            var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0);
            logInfor.time = hm;
            logInfor.userName = user.fullName;

            hisLogDB.AddLog(logInfor);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit program", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void accordionControlElement3_Click(object sender, EventArgs e)
        {
            ResetInforStudent();

            if (IsOK)
            {
                ResetPanel();

                UserManager userManager = new UserManager(this, accountDB);
                userManager.Dock = DockStyle.Fill;
                panelMain.Controls.Add(userManager);
                userManager.Show();
            }
            else
            {
                MessageBox.Show("You need set data for category!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void accordionControlElement5_Click(object sender, EventArgs e)
        {
            ResetInforStudent();

            ResetPanel();

            CategoryManager category = new CategoryManager(catDB);
            category.Dock = DockStyle.Fill;
            panelMain.Controls.Add(category);
            category.Show();
        }

        private void accordionControlElement4_Click(object sender, EventArgs e)
        {
            ResetInforStudent();

            ResetPanel();

            LogHis his = new LogHis(hisLogDB, this);
            his.Dock = DockStyle.Fill;
            panelMain.Controls.Add(his);
            his.Show();
        }

        public string yearSearch = "";
        public string seasonSearch = "";
        private void accordionControlElement6_Click(object sender, EventArgs e)
        {
            ResetInforStudent();

            if (IsOK)
            {
                ResetPanel();

                StudentListCandidate ds = new StudentListCandidate(this);
                ds.Dock = DockStyle.Fill;
                panelMain.Controls.Add(ds);
                ds.Show();
            }
            else
            {
                MessageBox.Show("You need set data for category!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void accordionControlElement9_Click(object sender, EventArgs e)
        {
            ResetInforStudent();

            if (IsOK)
            {
                ResetPanel();

                ExportForUniversity export = new ExportForUniversity(connString, this);
                export.Dock = DockStyle.Fill;
                panelMain.Controls.Add(export);
                export.Show();
            }
            else
            {
                MessageBox.Show("You need set data for category!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void accordionControlElement10_Click(object sender, EventArgs e)
        {
            ResetInforStudent();

            if (IsOK)
            {
                ResetPanel();

                ExportForPartner export = new ExportForPartner(connString, this);
                export.Dock = DockStyle.Fill;
                panelMain.Controls.Add(export);
                export.Show();
            }
            else
            {
                MessageBox.Show("You need set data for category!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void accordionControlElement11_Click(object sender, EventArgs e)
        {
            ResetInforStudent();

            if (IsOK)
            {
                ResetPanel();

                ShortList export = new ShortList(connString, this);
                export.Dock = DockStyle.Fill;
                panelMain.Controls.Add(export);
                export.Show();
            }
            else
            {
                MessageBox.Show("You need set data for category!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void accordionControlElement12_Click(object sender, EventArgs e)
        {
            ResetInforStudent();

            if (IsOK)
            {
                ResetPanel();

                ReviewList export = new ReviewList(connString, this);
                export.Dock = DockStyle.Fill;
                panelMain.Controls.Add(export);
                export.Show();
            }
            else
            {
                MessageBox.Show("You need set data for category!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void accordionControlElement13_Click(object sender, EventArgs e)
        {
            ResetInforStudent();

            ResetPanel();

            BeginForm beginForm = new BeginForm(this);
            beginForm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(beginForm);
            beginForm.Show();
        }

        private void accordionControlElement15_Click(object sender, EventArgs e)
        {
            ResetInforStudent();

            if (IsOK)
            {
                ResetPanel();

                DefineExcel form = new DefineExcel(this);
                form.Dock = DockStyle.Fill;
                panelMain.Controls.Add(form);
                form.Show();
            }
            else
            {
                MessageBox.Show("You need set data for category!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void accordionControlElement16_Click(object sender, EventArgs e)
        {
            ResetInforStudent();

            if (IsOK)
            {
                ResetPanel();

                StudentListManager ds = new StudentListManager(this);
                ds.Dock = DockStyle.Fill;
                panelMain.Controls.Add(ds);
                ds.Show();
            }
            else
            {
                MessageBox.Show("You need set data for category!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SetStudentName(string name)
        {
            lblStudentName.Visible = true;
            lblStudentName.Text = name;
        }

        public void SetPassport(string passport)
        {
            lblPassport.Visible = true;
            lblPassport.Text = passport;
        }

        public void SetImage(Bitmap pic)
        {
            if (pic != null)
                picImage.Image = pic;
            else
            {
                if (picImage.Image != null)
                {
                    picImage.Image.Dispose();
                    picImage.Image = null;
                }
            }
        }

        public void ResetInforStudent()
        {
            lblStudentName.Visible = false;
            lblPassport.Visible = false;

            lblStudentName.Text = "NA";
            lblPassport.Text = "NA";
            if (picImage.Image != null)
            {
                picImage.Image.Dispose();
                picImage.Image = null;
            }
        }

        private void accordionControlElement8_Click(object sender, EventArgs e)
        {
            ResetInforStudent();

            if (IsOK)
            {
                panelMain.Controls.Clear();
                EventListManager events = new EventListManager(this);
                events.Dock = DockStyle.Fill;
                panelMain.Controls.Add(events);
                events.Show();
            }
            else
            {
                MessageBox.Show("You need set data for category!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ResetPanel()
        {
            foreach (IDisposable control in panelMain.Controls)
                control.Dispose();

            panelMain.Controls.Clear();
        }
    }
}

public class CompareUniversity : IComparer<UniversityInfor>
{
    public int Compare(UniversityInfor x, UniversityInfor y)
    {
        if (x.number == 0 | y.number == 0)
            return 0;

        return x.number.CompareTo(y.number);
    }
}

public class ComparePartner : IComparer<PartnerInfor>
{
    public int Compare(PartnerInfor x, PartnerInfor y)
    {
        if (x.number == 0 | y.number == 0)
            return 0;

        return x.number.CompareTo(y.number);
    }
}
