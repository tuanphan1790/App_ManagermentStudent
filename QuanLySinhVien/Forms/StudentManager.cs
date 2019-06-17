using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Model;
using DBManager;
using DevComponents.DotNetBar.SuperGrid;
using System.Drawing.Imaging;
using System.IO;
using Spire.Doc.Fields;
using Spire.Doc;
using Spire.Doc.Documents;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using System.Globalization;

namespace QuanLySinhVien.Forms
{
    public partial class StudentManager : UserControl
    {
        StudentInforBasicDB dbBasicInfor;
        AdmissionInforDB dbAdmissionInfor;
        StudentInforInKoreaDB dbStudentInforInKorea;
        StudentTranscriptDB dbStudentTranscript;
        StudentFileManagerDB dbFile;
        StudentEventManagerDB dbEvent;

        FormMain mainForm;

        bool IsCreated;

        string directory;

        public StudentManager(FormMain form, StudentInforBasicDB db)
        {
            var exepath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            directory = System.IO.Path.GetDirectoryName(exepath);

            this.dbBasicInfor = db;
            this.mainForm = form;

            dbAdmissionInfor = new AdmissionInforDB(mainForm.connString);
            dbStudentInforInKorea = new StudentInforInKoreaDB(mainForm.connString);
            dbStudentTranscript = new StudentTranscriptDB(mainForm.connString);
            dbFile = new StudentFileManagerDB(mainForm.connString);
            dbEvent = new StudentEventManagerDB(mainForm.connString);

            IsCreated = false;

            InitializeComponent();

            if (mainForm.user.accountType == 0)
                btnCancel.Visible = true;

            rbIntervirewNone.Checked = true;
            rbCVReviewing.Checked = true;
            rbAdmissionResultNone.Checked = true;

            LoadCategory();

            cboYear.Text = DateTime.Now.Year.ToString();
            var monthNow = DateTime.Now.Month;
            if (monthNow >= 1 && monthNow <= 3)
            {
                cboSesson.Text = "Spring";
                cboYear.Text = DateTime.Now.AddYears(1).Year.ToString();
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


            if (form.user.accountType != 0)
            {
                VisibleResultProcess(false);
            }

            GenStudentID.Visible = true;
        }

        void VisibleResultProcess(bool visible)
        {
            labelX106.Visible = visible;
            rbtReject_CV.Visible = visible;
            rbtReject_Interview.Visible = visible;
            rbtAccept_CV.Visible = visible;
            rbtAccept_Interview.Visible = visible;
            lblResultKobec.Visible = visible;
            groupBox4.Visible = visible;
            groupBox3.Visible = visible;
        }

        public StudentManager(FormMain form, StudentInforBasicDB db, string studentId)
        {
            var exepath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            directory = System.IO.Path.GetDirectoryName(exepath);

            this.dbBasicInfor = db;
            this.mainForm = form;

            dbAdmissionInfor = new AdmissionInforDB(mainForm.connString);
            dbStudentInforInKorea = new StudentInforInKoreaDB(mainForm.connString);
            dbStudentTranscript = new StudentTranscriptDB(mainForm.connString);
            dbFile = new StudentFileManagerDB(mainForm.connString);
            dbEvent = new StudentEventManagerDB(mainForm.connString);

            IsCreated = true;

            InitializeComponent();

            rbIntervirewNone.Checked = true;
            rbCVReviewing.Checked = true;
            rbAdmissionResultNone.Checked = true;

            LoadCategory();

            if (form.user.accountType != 0)
            {
                VisibleResultProcess(false);
            }

            LoadData(studentId);

            mainForm.SetStudentName(txtFullName.Text);
            mainForm.SetPassport(txtPassportNo.Text);

            GenStudentID.Visible = false;
        }

        void LoadCategory()
        {
            cboKobecPartner.DataSource = mainForm.GetKobecPartner();
            cboUniversity.DataSource = mainForm.GetUniversity();

            List<string> sess = new List<string>();
            sess.Add("Spring");
            sess.Add("Summer");
            sess.Add("Autumn");
            sess.Add("Winter");
            cboSesson.DataSource = sess;

            List<string> visa = new List<string>();
            visa.Add("No");
            visa.Add("Yes");
            cboVisa.DataSource = visa;

            for (int i = 2018; i <= Convert.ToInt32(DateTime.Now.ToString("yyyy")); i++)
            {
                cboYear.Items.Add(i);
            }
        }

        void LoadData(string studentId)
        {
            #region Basic Infor Student
            StudentBasicInfor student = dbBasicInfor.GetInforDetailStudent(studentId);

            lblStudentID.Text = student.studentId;
            lblFullName.Text = txtFullName.Text = student.fullName;
            rbtMale.Checked = student.gender;
            rbtFemale.Checked = !student.gender;
            if (rbtMale.Checked)
            {
                lblGenger.Text = "Male";
            }
            else
            {
                lblGenger.Text = "Female";
            }

            dtBirthday.DateTime = student.dateOfBirth;
            lblBirthDay.Text = dtBirthday.DateTime.ToString("yyyy-MM-dd");
            lblHomeAddress.Text = txtHomeAddress.Text = student.homeAddress;
            txtPhone.Text = student.phone;
            lblHighschoolName.Text = txtHighschoolName.Text = student.highschoolName;
            txtSchoolAddress.Text = student.highschoolAddress;
            txtSchoolContact.Text = student.highschoolContact;

            foreach (var record in dbBasicInfor.GetHigherSchoolsInfor(lblStudentID.Text))
            {
                GridPanel panelx = sgHigherSchool.PrimaryGrid;
                sgHigherSchool.BeginUpdate();
                object[] ob1x = new object[]
                        {
                    record.id, record.schoolName, record.schoolType, record.major, record.yearOfGradualtion, record.gpa
                        };

                panelx.DefaultRowHeight = 0;
                panelx.Rows.Add(new GridRow(ob1x));
                sgHigherSchool.EndUpdate();
            }

            lblPassportNo.Text = txtPassportNo.Text = student.passportNumber;

            FormatDateTime(dtIssueDate, student.passportIssueDate);
            lblIssueDate.Text = dtIssueDate.DateTime.ToString("yyyy-MM-dd");

            FormatDateTime(dtExpireDate, student.pasportExpireDate);
            lblExpireDate.Text = dtExpireDate.DateTime.ToString("yyyy-MM-dd");

            lblApplyForUniversity.Text = cboUniversity.Text = student.applyForUniversity;
            cboYear.Text = student.applySesmesterYear;
            cboSesson.Text = student.applySesmesterSesson;
            lblSemester.Text = student.applySesmesterYear;
            lblSesson.Text = student.applySesmesterSesson;
            txtFatherName.Text = student.fatherName;
            FormatDateTime(dtFatherDob, student.fatherDob);
            txtFatherContact.Text = student.fatherContact;
            txtFatherJob.Text = student.fatherJob;
            txtMotherName.Text = student.motherName;
            FormatDateTime(dtMotherDob, student.motherDob);
            txtMotherContact.Text = student.motherContact;
            txtMotherJob.Text = student.motherJob;
            numMemberOfFamily.Value = student.numberOfFamily;
            lblKobecPartner.Text = cboKobecPartner.Text = student.kobecPartner;
            FormatDateTime(dtCVRecordDate, student.cvRecordDate);
            rtbRemarkBasicInfor.Text = student.remarks;
            txtBankBalance.Text = student.bankBalance.ToString("0.#####");
            lblBankBalance.Text = String.Format("{0:n0}", student.bankBalance);
            lblBankAccountOwner.Text = txtBankAccountOwner.Text = student.bankAccountOwner;
            lblRelationship.Text = txtRelationship.Text = student.relationship;
            FormatDateTime(dtHardCopyCVRecivedDate, student.HardCopyCVReceiveDate);

            if (student.imageBase64 != null)
            {
                picImage.Image = Utilities.Base64StringToBitmap(student.imageBase64);

                File.WriteAllText(directory + @"\tempPicture.jpg", "empty");
                picImage.Image.Save(directory + @"\tempPicture.jpg", ImageFormat.Jpeg);

                mainForm.SetImage(Utilities.Base64StringToBitmap(student.imageBase64));
            }
            else
            {
                mainForm.SetImage(Utilities.Base64StringToBitmap(student.imageBase64));
            }

            switch (student.cvReview)
            {
                case 0:
                    break;
                case 1:
                    rbtReject_CV.Checked = true;
                    break;
                case 2:
                    rbtAccept_CV.Checked = true;
                    break;
            }

            switch (student.interview)
            {
                case 0:
                    break;
                case 1:
                    rbtReject_Interview.Checked = true;
                    break;
                case 2:
                    rbtAccept_Interview.Checked = true;
                    break;
            }

            GridPanel panel = sgStudyHighschool.PrimaryGrid;
            sgStudyHighschool.BeginUpdate();
            object[] ob1 = new object[]
                    {
                    student.gpa10, student.gpa11, student.gpa12, student.dayOff10, student.dayOff11, student.dayOff12
                    };

            panel.DefaultRowHeight = 0;
            panel.Rows[0] = new GridRow(ob1);
            sgStudyHighschool.EndUpdate();

            var avr = System.Math.Round((student.gpa10 + student.gpa11 + student.gpa12) / 3, 2);
            lblAverage.Text = avr.ToString();

            int dayoffTotal = student.dayOff10 + student.dayOff11 + student.dayOff12;
            lblTotalDayOff.Text = dayoffTotal.ToString();

            FormatDateTime(dtGraduationDate, student.gradualtionDate);
            lblGraduationDate.Text = dtGraduationDate.DateTime.ToString("yyyy-MM-dd");

            switch (dbBasicInfor.GetResultKobec(studentId))
            {
                case StudentInforBasicDB.KobecResult.Rejected:
                    lblResultKobec.Text = "REJECTED";
                    break;
                case StudentInforBasicDB.KobecResult.WaitingCVReview:
                    lblResultKobec.Text = "WAITING FOR CV REVIEW";
                    break;
                case StudentInforBasicDB.KobecResult.WaitingInterview:
                    lblResultKobec.Text = "WAITING FOR INTERVIEW";
                    break;
                case StudentInforBasicDB.KobecResult.Accepted:
                    {
                        lblResultKobec.Text = "ACCEPTED";

                        stInforStudentInKorea.Visible = false;
                        stStudyInfor.Visible = false;
                        stViewStudentInfor.Visible = false;

                        VisibleAdmissionResult(true);
                        FormatDateTime(dtSubmissionDate, student.submissionDate);
                        switch (student.addmissionResult)
                        {
                            case 0:
                                break;
                            case 1:
                                rbtAdmissionReject.Checked = true;
                                break;
                            case 2:
                                {
                                    if (mainForm.user.accountType == 2)
                                        break;

                                    stAdmissionInfor.Visible = true;
                                    stInvoice.Visible = true;

                                    rbtAdmissionAccept.Checked = true;

                                    if (mainForm.user.accountType != 0)
                                    {
                                        btnSaveBasicInfor.Enabled = false;
                                        btnRefresh.Enabled = false;
                                    }

                                    break;
                                }
                        }
                    }

                    break;
            }

            if (!dbAdmissionInfor.CheckCandidate(lblStudentID.Text))
            {
                if (mainForm.user.accountType != 2)
                {
                    stInforStudentInKorea.Visible = true;
                    stStudyInfor.Visible = true;
                    stViewStudentInfor.Visible = true;
                    stInvoice.Visible = false;
                }
            }

            var listFiles = dbFile.GetAllDocs(lblStudentID.Text);

            var panelBasic = sgLoadFileBasicInfor.PrimaryGrid;
            panelBasic.Rows.Clear();
            var panelAdmission = sgUploadFileAdmission.PrimaryGrid;
            panelAdmission.Rows.Clear();
            var panelInforInKorea = sgUploadFileInforKorea.PrimaryGrid;
            panelInforInKorea.Rows.Clear();

            foreach (var file in listFiles)
            {
                if (file.documentType == 1)
                {
                    sgLoadFileBasicInfor.BeginUpdate();

                    ob1 = new object[]
                        {
                            file.id, file.fileName
                        };

                    panelBasic.DefaultRowHeight = 0;
                    panelBasic.Rows.Add(new GridRow(ob1));
                    sgLoadFileBasicInfor.EndUpdate();
                }
                else if (file.documentType == 2)
                {
                    sgUploadFileAdmission.BeginUpdate();

                    ob1 = new object[]
                       {
                           file.id, file.fileName
                       };

                    panelAdmission.DefaultRowHeight = 0;
                    panelAdmission.Rows.Add(new GridRow(ob1));
                    sgUploadFileAdmission.EndUpdate();
                }
                else if (file.documentType == 3)
                {
                    sgUploadFileInforKorea.BeginUpdate();

                    ob1 = new object[]
                       {
                           file.id, file.fileName
                       };

                    panelInforInKorea.DefaultRowHeight = 0;
                    panelInforInKorea.Rows.Add(new GridRow(ob1));
                    sgUploadFileInforKorea.EndUpdate();
                }
            }

            lblLastedUpdateBasicInfor.Text = student.lastedUpdate;

            SettingColorForBasicForm();
            #endregion

            //Load data Admission Infor
            #region Load data Admission Infor
            cboDepositUnit.Text = "VND";

            var recordAdmissionInfor = dbAdmissionInfor.GetAdmissionInforDetail(lblStudentID.Text);
            if (recordAdmissionInfor != null)
            {
                FormatDateTime(dtInvoiceDate, recordAdmissionInfor.invoiceDate);
                if (dtInvoiceDate.DateTime.Ticks != 0)
                {
                    dtVisaCodeDate.Enabled = true;
                }

                txtAddmissionNo.Text = recordAdmissionInfor.addmissionNo;
                txtTuitionPayAmount.Text = recordAdmissionInfor.tuitionPayAmount.ToString("0.#####");

                FormatDateTime(dtTutionPayDate, recordAdmissionInfor.tuitionPayDate);

                cboVisa.Text = recordAdmissionInfor.visa;
                FormatDateTime(dtVisaCodeDate, recordAdmissionInfor.visaCodeDate);
                if (dtVisaCodeDate.DateTime.Ticks != 0)
                {
                    cboVisa.Enabled = true;
                    cboVisa.Text = recordAdmissionInfor.visa;
                }

                txtDepositAmount.Text = recordAdmissionInfor.depositAmount.ToString("0.#####");
                cboDepositUnit.Text = recordAdmissionInfor.depositUnit;

                FormatDateTime(dtDepositStartDate, recordAdmissionInfor.depositStartDate);

                txtDepositProfit.Text = recordAdmissionInfor.depositProfit.ToString();

                FormatDateTime(dtKoreaEntryPlan, recordAdmissionInfor.koreaEntryPlan);
                txtFlyNumber.Text = recordAdmissionInfor.flyNumber;

                FormatDateTime(dtEntranceDate, recordAdmissionInfor.EntranceDate);

                if (dtEntranceDate.EditValue == null && mainForm.user.accountType == 0)
                    btnCancel.Visible = true;

                lblLastedUpdateAdmissionInfor.Text = recordAdmissionInfor.lastedUpdate;
            }

            var remarkAdmissionInfor = dbAdmissionInfor.GetRemarkAdmissionInfor(lblStudentID.Text);
            if (remarkAdmissionInfor != null)
            {
                rtbRemarkAdmissionInfor.Text = remarkAdmissionInfor.remarks;
                lblLastedUpdateAdmissonRemark.Text = remarkAdmissionInfor.lastedUpdate;
            }
            #endregion

            //Load Infor in Korea
            #region Load Infor in Korea
            var remarkInKorea = dbStudentInforInKorea.GetRemarkInforInKorea(lblStudentID.Text);
            if (remarkInKorea != null)
            {
                rtbRemarkInforInKorea.Text = remarkInKorea.remarks;
                lblLastedUpdateRemarkKoreaInfor.Text = remarkInKorea.lastedUpdate;
            }

            sgUpdateInfor.PrimaryGrid.Rows.Clear();
            //sgInforInKorea_View.PrimaryGrid.Rows.Clear();

            var recordStudentInforInKoreas = dbStudentInforInKorea.GetStudentInforInKoreas(lblStudentID.Text);
            foreach (var record in recordStudentInforInKoreas)
            {
                panel = sgUpdateInfor.PrimaryGrid;
                sgUpdateInfor.BeginUpdate();

                string IdentifierExpireDate = "NA";
                if (record.IdentifierExpireDate.Ticks != 0)
                {
                    IdentifierExpireDate = record.IdentifierExpireDate.ToString("yyyy-MM-dd");
                }

                string addressEntry = "NA";
                if (record.addressEntry.Ticks != 0)
                {
                    addressEntry = record.addressEntry.ToString("yyyy-MM-dd");
                }

                string addressEnd = "NA";
                if (record.addressEnd.Ticks != 0)
                {
                    addressEnd = record.addressEnd.ToString("yyyy-MM-dd");
                }

                string parttimeEntry = "NA";
                if (record.parttimeEntry.Ticks != 0)
                {
                    parttimeEntry = record.parttimeEntry.ToString("yyyy-MM-dd");
                }

                string parttimeEnd = "NA";
                if (record.parttimeEnd.Ticks != 0)
                {
                    parttimeEnd = record.parttimeEnd.ToString("yyyy-MM-dd");
                }

                string AddressManager = "NA";
                switch (record.addressManager)
                {
                    case 1:
                        AddressManager = "Yes";
                        break;
                    case 2:
                        AddressManager = "No";
                        break;
                    case 3:
                        AddressManager = "Custom";
                        break;
                }

                ob1 = new object[]
                        {
                                record.id, record.dateUpdate.ToString("yyyy-MM-dd"), record.NoIdentifierKorea, IdentifierExpireDate, record.contact,
                                record.address, addressEntry, addressEnd, AddressManager, record.addressNote,
                                record.userAccount, record.numberAccount, record.money, record.managerName, record.managerPhone,
                                record.parttimeJob, parttimeEntry, parttimeEnd, record.parttimeAddress, record.parttimeNote
                        };

                panel.DefaultRowHeight = 0;
                panel.Rows.Add(new GridRow(ob1));
                sgUpdateInfor.EndUpdate();
            }

            if (recordStudentInforInKoreas.Count != 0)
            {
                var studentInforInKoreaLast = recordStudentInforInKoreas.Last();

                lblNoIdentifierKorea.Text = studentInforInKoreaLast.NoIdentifierKorea;

                if (studentInforInKoreaLast.IdentifierExpireDate.Ticks != 0)
                    lblIdentifierExpire.Text = studentInforInKoreaLast.IdentifierExpireDate.ToString("yyyy-MM-dd");

                lblContact.Text = studentInforInKoreaLast.contact;
                lblAddress.Text = studentInforInKoreaLast.address;

                if (studentInforInKoreaLast.addressEntry.Ticks != 0)
                    lblAddressEntry.Text = studentInforInKoreaLast.addressEntry.ToString("yyyy-MM-dd");

                if (studentInforInKoreaLast.addressEnd.Ticks != 0)
                    lblAddressEnd.Text = studentInforInKoreaLast.addressEnd.ToString("yyyy-MM-dd");

                lblAddressManager.Text = "NA";
                switch (studentInforInKoreaLast.addressManager)
                {
                    case 1:
                        lblAddressManager.Text = "Yes";
                        break;
                    case 2:
                        lblAddressManager.Text = "No";
                        break;
                    case 3:
                        lblAddressManager.Text = "Custom";
                        break;
                }

                lblUserAccount.Text = studentInforInKoreaLast.userAccount;
                lblNumberAccount.Text = studentInforInKoreaLast.numberAccount;
                lblMoney.Text = studentInforInKoreaLast.money;
                lblManagerName.Text = studentInforInKoreaLast.managerName;
                lblManagerPhone.Text = studentInforInKoreaLast.managerPhone;
                lblPartTimeJob.Text = studentInforInKoreaLast.parttimeJob;

                if (studentInforInKoreaLast.parttimeEntry.Ticks != 0)
                    lblPartTimeEntry.Text = studentInforInKoreaLast.parttimeEntry.ToString("yyyy-MM-dd");

                if (studentInforInKoreaLast.parttimeEnd.Ticks != 0)
                    lblPartTimeEnd.Text = studentInforInKoreaLast.parttimeEnd.ToString("yyyy-MM-dd");

                lblPartTimeAddress.Text = studentInforInKoreaLast.parttimeAddress;

                lblLastedUpdateInforInKorea.Text = studentInforInKoreaLast.lastedUpdate;
            }

            #endregion

            //Load study information
            #region Load study information
            sgStudylanguge.PrimaryGrid.Rows.Clear();
            sgStudySpecialized.PrimaryGrid.Rows.Clear();

            var recordStudyLanguge = dbStudentTranscript.GetAllStudentStudyLanguge(lblStudentID.Text);
            foreach (var recordLang in recordStudyLanguge)
            {
                string entryDate = "NA";
                if (recordLang.entryDate != DateTime.MinValue)
                {
                    entryDate = recordLang.entryDate.ToString("yyyy-MM-dd");
                }

                string finalDate = "NA";
                if (recordLang.finalDate != DateTime.MinValue)
                {
                    finalDate = recordLang.finalDate.ToString("yyyy-MM-dd");
                }

                panel = sgStudylanguge.PrimaryGrid;
                sgStudylanguge.BeginUpdate();
                ob1 = new object[]
                        {
                            recordLang.id, recordLang.dateUpdate.ToString("yyyy-MM-dd"), recordLang.schoolName,
                            recordLang.level, entryDate, finalDate, recordLang.year + "-" + recordLang.season,
                            recordLang.attendancePoint.ToString() + "%", recordLang.finalPoint, recordLang.result, recordLang.note
                        };

                panel.DefaultRowHeight = 0;
                panel.Rows.Add(new GridRow(ob1));
                sgStudylanguge.EndUpdate();

            }

            if (recordStudyLanguge.Count != 0)
            {
                lblLastedUpdateStudyLanguge.Text = recordStudyLanguge.Last().lastedUpdate;
            }

            var recordStudySpec = dbStudentTranscript.GetAllStudentStudySpecificated(lblStudentID.Text);
            foreach (var record in recordStudySpec)
            {
                string entryDate = "NA";
                if (record.entryDate != DateTime.MinValue)
                {
                    entryDate = record.entryDate.ToString("yyyy-MM-dd");
                }

                string finalDate = "NA";
                if (record.finalDate != DateTime.MinValue)
                {
                    finalDate = record.finalDate.ToString("yyyy-MM-dd");
                }

                panel = sgStudySpecialized.PrimaryGrid;
                sgStudySpecialized.BeginUpdate();
                ob1 = new object[]
                        {
                                   record.id, record.dateUpdate.ToString("yyyy-MM-dd"), record.schoolName,
                                   entryDate, finalDate,
                                   record.year + "-" + record.season, record.schoolarship, record.finalPoint, record.note
                        };

                panel.DefaultRowHeight = 0;
                panel.Rows.Add(new GridRow(ob1));
                sgStudySpecialized.EndUpdate();
            }

            if (recordStudySpec.Count != 0)
            {
                lblLastedUpdateStudySpecialized.Text = recordStudySpec.Last().lastedUpdate;
            }

            #endregion

            //Load View Information
            #region Load view Infor
            sgInforInKorea_View.PrimaryGrid.Rows.Clear();
            sgStudyLanguge_View.PrimaryGrid.Rows.Clear();
            sgStudySpecialized_View.PrimaryGrid.Rows.Clear();

            lblUnitAmount.Text = cboDepositUnit.Text;
            var recordStudentInforBeforeEntry = dbAdmissionInfor.GetAdmissionInforDetail(lblStudentID.Text);
            if (recordStudentInforBeforeEntry != null)
            {
                lblAmount.Text = String.Format("{0:n0}", recordStudentInforBeforeEntry.depositAmount);
                if (recordStudentInforBeforeEntry.depositStartDate.Ticks != 0)
                    lblStartDate.Text = recordStudentInforBeforeEntry.depositStartDate.ToString("yyyy-MM-dd");

                lblProfit.Text = recordStudentInforBeforeEntry.depositProfit.ToString("0.#####");
                if (recordStudentInforBeforeEntry.EntranceDate.Ticks != 0)
                    lblKoreaEntryDate.Text = recordStudentInforBeforeEntry.EntranceDate.ToString("yyyy-MM-dd");
            }

            recordStudentInforInKoreas = dbStudentInforInKorea.GetStudentInforInKoreas(lblStudentID.Text);
            foreach (var record in recordStudentInforInKoreas)
            {
                string IdentifierExpireDate = "NA";
                if (record.IdentifierExpireDate != DateTime.MinValue)
                {
                    IdentifierExpireDate = record.IdentifierExpireDate.ToString("yyyy-MM-dd");
                }

                string addressEntry = "NA";
                if (record.addressEntry != DateTime.MinValue)
                {
                    addressEntry = record.addressEntry.ToString("yyyy-MM-dd");
                }

                string addressEnd = "NA";
                if (record.addressEnd != DateTime.MinValue)
                {
                    addressEnd = record.addressEnd.ToString("yyyy-MM-dd");
                }

                string paymentDate = "NA";
                if (record.paymentDate != DateTime.MinValue)
                {
                    paymentDate = record.paymentDate.ToString("yyyy-MM-dd");
                }

                string parttimeEntry = "NA";
                if (record.parttimeEntry != DateTime.MinValue)
                {
                    parttimeEntry = record.parttimeEntry.ToString("yyyy-MM-dd");
                }

                string parttimeEnd = "NA";
                if (record.parttimeEnd != DateTime.MinValue)
                {
                    parttimeEnd = record.parttimeEnd.ToString("yyyy-MM-dd");
                }

                panel = sgInforInKorea_View.PrimaryGrid;
                sgInforInKorea_View.BeginUpdate();

                ob1 = new object[]
                        {
                                record.id, record.NoIdentifierKorea, IdentifierExpireDate, record.address, paymentDate,
                                record.userAccount, record.numberAccount, record.money, record.managerName, record.managerPhone
                        };

                panel.DefaultRowHeight = 0;
                panel.Rows.Add(new GridRow(ob1));
                sgInforInKorea_View.EndUpdate();
            }


            recordStudyLanguge = dbStudentTranscript.GetAllStudentStudyLanguge(lblStudentID.Text);
            foreach (var recordLang in recordStudyLanguge)
            {
                string entryDate = "NA";
                if (recordLang.entryDate != DateTime.MinValue)
                {
                    entryDate = recordLang.entryDate.ToString("yyyy-MM-dd");
                }

                string finalDate = "NA";
                if (recordLang.finalDate != DateTime.MinValue)
                {
                    finalDate = recordLang.finalDate.ToString("yyyy-MM-dd");
                }

                panel = sgStudyLanguge_View.PrimaryGrid;
                sgStudyLanguge_View.BeginUpdate();
                ob1 = new object[]
                        {
                            recordLang.id, recordLang.schoolName, entryDate, recordLang.season,
                            recordLang.attendancePoint, recordLang.finalPoint
                        };

                panel.DefaultRowHeight = 0;
                panel.Rows.Add(new GridRow(ob1));
                sgStudyLanguge_View.EndUpdate();
            }

            recordStudySpec = dbStudentTranscript.GetAllStudentStudySpecificated(lblStudentID.Text);
            foreach (var record in recordStudySpec)
            {
                string entryDate = "NA";
                if (record.entryDate != DateTime.MinValue)
                {
                    entryDate = record.entryDate.ToString("yyyy-MM-dd");
                }

                string finalDate = "NA";
                if (record.finalDate != DateTime.MinValue)
                {
                    finalDate = record.finalDate.ToString("yyyy-MM-dd");
                }

                panel = sgStudySpecialized_View.PrimaryGrid;
                sgStudySpecialized_View.BeginUpdate();
                ob1 = new object[]
                        {
                                   record.id, record.schoolName, entryDate, record.season, record.finalPoint
                        };

                panel.DefaultRowHeight = 0;
                panel.Rows.Add(new GridRow(ob1));
                sgStudySpecialized_View.EndUpdate();
            }
            #endregion

            if (student.isCancel)
            {
                btnSaveAdmissionInfor.Enabled = false;
                btnCancel.Enabled = false;
                btnSaveBasicInfor.Enabled = false;
                stInvoice.Visible = false;
            }
        }

        void VisibleAdmissionResult(bool visible)
        {
            labelX35.Visible = visible;
            labelX49.Visible = visible;
            dtSubmissionDate.Visible = visible;
            rbtAdmissionReject.Visible = visible;
            rbtAdmissionAccept.Visible = visible;
            groupBox2.Visible = visible;
        }

        int idStudyLanguge = 0;
        private void newRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateProcessStudyLanguge form = new UpdateProcessStudyLanguge();
            if (form.ShowDialog() == DialogResult.OK)
            {
                StudentStudyLanguge record = new StudentStudyLanguge(lblStudentID.Text);
                record.schoolName = form.GetSchoolName();
                record.entryDate = form.GetEntryDate();
                record.finalDate = form.GetEndDate();
                record.year = form.GetYear();
                record.season = form.GetSeason();
                record.level = form.GetLevel();
                record.result = form.GetResult();
                record.attendancePoint = form.GetAttandancePoint();
                record.finalPoint = form.GetFinalPoint();
                record.dateUpdate = form.GetDateUpdate();
                record.note = form.GetNote();

                string entryDate = "NA";
                if (record.entryDate.Ticks != 0)
                {
                    entryDate = record.entryDate.ToString("yyyy-MM-dd");
                }

                string finalDate = "NA";
                if (record.finalDate.Ticks != 0)
                {
                    finalDate = record.finalDate.ToString("yyyy-MM-dd");
                }

                record.lastedUpdate = mainForm.user.fullName;
                int id = dbStudentTranscript.AddNewStudyLanguge(record);

                GridPanel panel = sgStudylanguge.PrimaryGrid;
                sgStudylanguge.BeginUpdate();

                object[] ob1 = new object[]
                        {
                    id, record.dateUpdate.ToString("yyyy-MM-dd"), record.schoolName, record.level,
                    entryDate, finalDate, record.year + "-" + record.season, record.attendancePoint.ToString() + "%",
                    record.finalPoint, record.result, record.note
                        };

                panel.DefaultRowHeight = 0;
                panel.Rows.Add(new GridRow(ob1));
                sgStudylanguge.EndUpdate();

                panel = sgStudyLanguge_View.PrimaryGrid;
                sgStudyLanguge_View.BeginUpdate();
                ob1 = new object[]
                        {
                    id, record.schoolName, entryDate, record.year + "-" + record.season, record.attendancePoint, record.finalPoint
                        };

                panel.DefaultRowHeight = 0;
                panel.Rows.Add(new GridRow(ob1));
                sgStudyLanguge_View.EndUpdate();

                mainForm.SetHisOperate("New record in study languge of StudentID= " + lblStudentID.Text + " Name= " + txtFullName.Text);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (idStudyLanguge == 0)
                return;

            var oldRecord = dbStudentTranscript.GetStudentStudyLangugeDetail(lblStudentID.Text, idStudyLanguge);
            if (oldRecord != null)
            {
                UpdateProcessStudyLanguge form = new UpdateProcessStudyLanguge(oldRecord);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    StudentStudyLanguge record = new StudentStudyLanguge(lblStudentID.Text);
                    record.schoolName = form.GetSchoolName();
                    record.entryDate = form.GetEntryDate();
                    record.finalDate = form.GetEndDate();
                    record.year = form.GetYear();
                    record.season = form.GetSeason();
                    record.level = form.GetLevel();
                    record.result = form.GetResult();
                    record.attendancePoint = form.GetAttandancePoint();
                    record.finalPoint = form.GetFinalPoint();
                    record.dateUpdate = form.GetDateUpdate();
                    record.note = form.GetNote();

                    string entryDate = "NA";
                    if (record.entryDate.Ticks != 0)
                    {
                        entryDate = record.entryDate.ToString("yyyy-MM-dd");
                    }

                    string finalDate = "NA";
                    if (record.finalDate.Ticks != 0)
                    {
                        finalDate = record.finalDate.ToString("yyyy-MM-dd");
                    }

                    record.lastedUpdate = mainForm.user.fullName;
                    dbStudentTranscript.EditRecordStudyLanguge(idStudyLanguge, record);

                    GridPanel panel = sgStudylanguge.PrimaryGrid;
                    var IRows = panel.Rows.GetEnumerator();
                    while (IRows.MoveNext())
                    {
                        GridRow r = (GridRow)IRows.Current;
                        if ((int)r[0].Value == oldRecord.id)
                        {
                            r[1].Value = record.dateUpdate.ToString("yyyy-MM-dd");
                            r[2].Value = record.schoolName;
                            r[3].Value = record.level;
                            r[4].Value = entryDate;
                            r[5].Value = finalDate;
                            r[6].Value = record.year + "-" + record.season;
                            r[7].Value = record.attendancePoint.ToString() + "%";
                            r[8].Value = record.finalPoint;
                            r[9].Value = record.result;
                            r[10].Value = record.note;
                        }
                    }

                    panel = sgStudyLanguge_View.PrimaryGrid;
                    IRows = panel.Rows.GetEnumerator();
                    while (IRows.MoveNext())
                    {
                        GridRow r = (GridRow)IRows.Current;
                        if ((int)r[0].Value == oldRecord.id)
                        {
                            r[1].Value = record.schoolName;
                            r[2].Value = record.entryDate.ToString("yyyy-MM-dd");
                            r[3].Value = record.year + "-" + record.season;
                            r[4].Value = record.attendancePoint;
                            r[5].Value = record.finalPoint;
                        }
                    }

                    mainForm.SetHisOperate("Edit record in study languge of StudentID= " + lblStudentID.Text + " Name= " + txtFullName.Text);
                }
            }

            idStudyLanguge = 0;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (idStudyLanguge == 0)
            {
                return;
            }
            else
            {
                dbStudentTranscript.DeleteStudyLanguge(idStudyLanguge);

                GridPanel panel = sgStudylanguge.PrimaryGrid;
                var IRows = panel.Rows.GetEnumerator();
                GridRow _r = null;

                while (IRows.MoveNext())
                {
                    GridRow r = (GridRow)IRows.Current;
                    if ((int)r[0].Value == idStudyLanguge)
                    {
                        _r = r;
                    }
                }
                panel.Rows.Remove(_r);

                panel = sgStudyLanguge_View.PrimaryGrid;
                IRows = panel.Rows.GetEnumerator();
                _r = null;
                while (IRows.MoveNext())
                {
                    GridRow r = (GridRow)IRows.Current;
                    if ((int)r[0].Value == idStudyLanguge)
                    {
                        _r = r;
                    }
                }
                panel.Rows.Remove(_r);

                idStudyLanguge = 0;

                mainForm.SetHisOperate("Delete record in study languge of StudentID= " + lblStudentID.Text + " Name= " + txtFullName.Text);
            }
        }

        int idStudySpecified = 0;
        private void newRecordToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UpdateProcessStudySpecialized form = new UpdateProcessStudySpecialized();
            if (form.ShowDialog() == DialogResult.OK)
            {
                StudentStudySpecificated record = new StudentStudySpecificated(lblStudentID.Text);
                record.schoolName = form.GetSchoolName();
                record.entryDate = form.GetEntryDate();
                record.finalDate = form.GetEndDate();
                record.season = form.GetSeason();
                record.year = form.GetYear();
                record.finalPoint = form.GetFinalPoint();
                record.schoolarship = form.GetSchoolarship();
                record.dateUpdate = form.GetDateUpdate();
                record.note = form.GetNote();

                string entryDate = "NA";
                if (record.entryDate.Ticks != 0)
                {
                    entryDate = record.entryDate.ToString("yyyy-MM-dd");
                }

                string finalDate = "NA";
                if (record.finalDate.Ticks != 0)
                {
                    finalDate = record.finalDate.ToString("yyyy-MM-dd");
                }

                record.lastedUpdate = mainForm.user.fullName;
                int id = dbStudentTranscript.AddNewStudySpecificated(record);

                GridPanel panel = sgStudySpecialized.PrimaryGrid;
                sgStudySpecialized.BeginUpdate();

                object[] ob1 = new object[]
                        {
                    id, record.dateUpdate.ToString("yyyy-MM-dd"), record.schoolName, entryDate, finalDate,
                    record.year + "-" + record.season, record.schoolarship,record.finalPoint, record.note
                        };

                panel.DefaultRowHeight = 0;
                panel.Rows.Add(new GridRow(ob1));
                sgStudySpecialized.EndUpdate();

                panel = sgStudySpecialized_View.PrimaryGrid;
                sgStudySpecialized_View.BeginUpdate();
                ob1 = new object[]
                        {
                    id, record.schoolName, entryDate, record.year + "-" + record.season, record.finalPoint
                        };

                panel.DefaultRowHeight = 0;
                panel.Rows.Add(new GridRow(ob1));
                sgStudySpecialized_View.EndUpdate();

                mainForm.SetHisOperate("New record in study specificated of StudentID= " + lblStudentID.Text + " Name= " + txtFullName.Text);
            }
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (idStudySpecified == 0)
                return;

            StudentStudySpecificated oldRecord = dbStudentTranscript.GetStudentStudySpecificatedDetail(lblStudentID.Text, idStudySpecified);
            if (oldRecord != null)
            {
                UpdateProcessStudySpecialized form = new UpdateProcessStudySpecialized(oldRecord);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    StudentStudySpecificated record = new StudentStudySpecificated(lblStudentID.Text);
                    record.schoolName = form.GetSchoolName();
                    record.entryDate = form.GetEntryDate();
                    record.finalDate = form.GetEndDate();
                    record.season = form.GetSeason();
                    record.year = form.GetYear();
                    record.finalPoint = form.GetFinalPoint();
                    record.schoolarship = form.GetSchoolarship();
                    record.dateUpdate = form.GetDateUpdate();
                    record.note = form.GetNote();
                    record.lastedUpdate = mainForm.user.fullName;

                    dbStudentTranscript.EditRecordStudySpecificated(idStudySpecified, record);

                    string entryDate = "NA";
                    if (record.entryDate.Ticks != 0)
                    {
                        entryDate = record.entryDate.ToString("yyyy-MM-dd");
                    }

                    string finalDate = "NA";
                    if (record.finalDate.Ticks != 0)
                    {
                        finalDate = record.finalDate.ToString("yyyy-MM-dd");
                    }

                    GridPanel panel = sgStudySpecialized.PrimaryGrid;
                    var IRows = panel.Rows.GetEnumerator();
                    while (IRows.MoveNext())
                    {
                        GridRow r = (GridRow)IRows.Current;
                        if ((int)r[0].Value == oldRecord.id)
                        {
                            r[1].Value = record.dateUpdate.ToString("yyyy-MM-dd");
                            r[2].Value = record.schoolName;
                            r[3].Value = entryDate;
                            r[4].Value = finalDate;
                            r[5].Value = record.year + "-" + record.season;
                            r[6].Value = record.schoolarship;
                            r[7].Value = record.finalPoint;
                            r[8].Value = record.note;
                        }
                    }

                    panel = sgStudySpecialized_View.PrimaryGrid;
                    IRows = panel.Rows.GetEnumerator();
                    while (IRows.MoveNext())
                    {
                        GridRow r = (GridRow)IRows.Current;
                        if ((int)r[0].Value == oldRecord.id)
                        {
                            r[1].Value = record.schoolName;
                            r[2].Value = entryDate;
                            r[3].Value = record.year + "-" + record.season;
                            r[4].Value = record.finalPoint;
                        }
                    }

                    mainForm.SetHisOperate("Edit record in study specificated of StudentID= " + lblStudentID.Text + " Name= " + txtFullName.Text);
                }
            }

            idStudySpecified = 0;
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (idStudySpecified == 0)
            {
                return;
            }
            else
            {
                dbStudentTranscript.DeleteStudySpecificated(idStudySpecified);

                GridPanel panel = sgStudySpecialized.PrimaryGrid;
                var IRows = panel.Rows.GetEnumerator();
                GridRow _r = null;

                while (IRows.MoveNext())
                {
                    GridRow r = (GridRow)IRows.Current;
                    if ((int)r[0].Value == idStudySpecified)
                    {
                        _r = r;
                    }
                }
                panel.Rows.Remove(_r);

                panel = sgStudySpecialized_View.PrimaryGrid;
                IRows = panel.Rows.GetEnumerator();
                _r = null;
                while (IRows.MoveNext())
                {
                    GridRow r = (GridRow)IRows.Current;
                    if ((int)r[0].Value == idStudySpecified)
                    {
                        _r = r;
                    }
                }
                panel.Rows.Remove(_r);

                idStudySpecified = 0;

                mainForm.SetHisOperate("Delete record in study specificated of StudentID= " + lblStudentID.Text + " Name= " + txtFullName.Text);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                picImage.Image = System.Drawing.Image.FromFile(dlg.FileName);
            }
        }

        private void btnSaveBasicInfor_Click(object sender, EventArgs e)
        {
            if (txtFullName.Text == "")
            {
                MessageBox.Show("There is no data in the fullname field", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rbtMale.Checked == false && rbtFemale.Checked == false)
            {
                MessageBox.Show("There is no data in the gender field", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dtBirthday.EditValue == null)
            {
                MessageBox.Show("There is no data in the birthday field", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cboYear.Text == "")
            {
                MessageBox.Show("There is no data in the sesmester field", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dtCVRecordDate.EditValue == null)
            {
                MessageBox.Show("There is no data in the CV recordate field", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dtGraduationDate.EditValue == null | txtHighschoolName.Text == "" | txtSchoolAddress.Text == "" | sgStudyHighschool.PrimaryGrid.Size.IsEmpty)
            {
                MessageBox.Show("There is no data in the highchool information", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (lblStudentID.Text == "")
            {
                MessageBox.Show("Don't generate StudentID", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            StudentBasicInfor student = new StudentBasicInfor(lblStudentID.Text);

            lblFullName.Text = student.fullName = txtFullName.Text;
            student.gender = rbtMale.Checked;
            student.gender = !rbtFemale.Checked;
            student.dateOfBirth = dtBirthday.DateTime;
            student.homeAddress = txtHomeAddress.Text;
            student.phone = txtPhone.Text;
            student.highschoolName = txtHighschoolName.Text;
            student.highschoolAddress = txtSchoolAddress.Text;
            student.highschoolContact = txtSchoolContact.Text;
            student.passportNumber = txtPassportNo.Text;
            if (dtIssueDate.EditValue != null)
                student.passportIssueDate = dtIssueDate.DateTime;

            if (dtExpireDate.EditValue != null)
                student.pasportExpireDate = dtExpireDate.DateTime;

            student.applyForUniversity = cboUniversity.Text;
            student.applySesmesterYear = cboYear.Text;
            student.applySesmesterSesson = cboSesson.Text;
            student.fatherName = txtFatherName.Text;
            if (dtFatherDob.EditValue != null)
                student.fatherDob = dtFatherDob.DateTime;

            student.fatherContact = txtFatherContact.Text;
            student.fatherJob = txtFatherJob.Text;
            student.motherName = txtMotherName.Text;
            if (dtMotherDob.EditValue != null)
                student.motherDob = dtMotherDob.DateTime;
            student.motherContact = txtMotherContact.Text;
            student.motherJob = txtMotherJob.Text;
            student.numberOfFamily = Convert.ToInt32(numMemberOfFamily.Value);
            student.kobecPartner = cboKobecPartner.Text;
            if (dtCVRecordDate.EditValue != null)
                student.cvRecordDate = dtCVRecordDate.DateTime;
            student.remarks = rtbRemarkBasicInfor.Text;

            decimal balanced = 0;
            if (decimal.TryParse(txtBankBalance.Text, out balanced))
            {
                student.bankBalance = balanced;
            }
            else
            {
                MessageBox.Show("You enter bank balanced wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            student.bankAccountOwner = txtBankAccountOwner.Text;
            student.relationship = txtRelationship.Text;
            student.HardCopyCVReceiveDate = dtHardCopyCVRecivedDate.DateTime;

            if (picImage.Image != null)
            {
                Bitmap bmp = new Bitmap(picImage.Image);
                student.imageBase64 = Utilities.BitmapToBase64String(bmp, ImageFormat.Png);
            }

            if (rbtReject_CV.Checked)
            {
                student.cvReview = 1;
            }
            else if (rbtAccept_CV.Checked)
            {
                student.cvReview = 2;
            }
            else
            {
                student.cvReview = 0;
            }

            if (rbtReject_Interview.Checked)
            {
                student.interview = 1;
            }
            else if (rbtAccept_Interview.Checked)
            {
                student.interview = 2;
            }
            else
            {
                student.interview = 0;
            }

            float gpa10 = Convert.ToSingle(sgStudyHighschool.GetCell(0, 0).Value);
            float gpa11 = Convert.ToSingle(sgStudyHighschool.GetCell(0, 1).Value);
            float gpa12 = Convert.ToSingle(sgStudyHighschool.GetCell(0, 2).Value);

            int dayOff10 = Convert.ToInt32(sgStudyHighschool.GetCell(0, 3).Value);
            int dayOff11 = Convert.ToInt32(sgStudyHighschool.GetCell(0, 4).Value);
            int dayOff12 = Convert.ToInt32(sgStudyHighschool.GetCell(0, 5).Value);

            student.gpa10 = gpa10;
            student.gpa11 = gpa11;
            student.gpa12 = gpa12;

            student.dayOff10 = dayOff10;
            student.dayOff11 = dayOff11;
            student.dayOff12 = dayOff12;

            student.gradualtionDate = dtGraduationDate.DateTime;

            bool checkKobecAccept = false;
            if (student.cvReview == 0)
            {
                lblResultKobec.Text = "WAITING FOR CV REVIEW";

                VisibleAdmissionResult(false);
            }
            else if (student.cvReview == 1)
            {
                lblResultKobec.Text = "REJECTED";

                student.addmissionResult = 1;

                VisibleAdmissionResult(false);
            }
            else if (student.interview == 1)
            {
                lblResultKobec.Text = "REJECTED";

                student.addmissionResult = 1;

                VisibleAdmissionResult(false);
            }
            else if (student.cvReview == 2 && student.interview == 0)
            {
                lblResultKobec.Text = "WAITING FOR INTERVIEW";

                VisibleAdmissionResult(false);
            }
            else if (student.cvReview == 1 && student.interview == 1)
            {
                lblResultKobec.Text = "REJECTED";

                student.addmissionResult = 1;

                VisibleAdmissionResult(false);
            }
            else if (student.cvReview == 2 && student.interview == 2)
            {
                lblResultKobec.Text = "ACCEPTED";

                checkKobecAccept = true;

                VisibleAdmissionResult(true);

                student.submissionDate = dtSubmissionDate.DateTime;
                if (rbtAdmissionAccept.Checked)
                {
                    student.addmissionResult = 2;
                }
                else if (rbtAdmissionReject.Checked)
                {
                    student.addmissionResult = 1;
                }
                else
                {
                    student.addmissionResult = 0;
                }

                if (student.addmissionResult == 2)
                {
                    if (mainForm.user.accountType != 0)
                    {
                        btnSaveBasicInfor.Enabled = false;
                        btnRefresh.Enabled = false;
                    }
                }
            }

            if (!IsCreated)
            {
                if (txtPassportNo.Text != "")
                {
                    var studentDuplicatepassport = dbBasicInfor.CheckPassportNumber(txtPassportNo.Text);
                    if (studentDuplicatepassport.Count > 0)
                    {
                        if (MessageBox.Show("Duplicate passport number of this student! \nDo you want to still save?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                            return;
                        else
                        {
                            foreach (var otherStu in studentDuplicatepassport)
                            {
                                otherStu.duplicate = true;
                                dbBasicInfor.EditInforStudent(otherStu.studentId, otherStu);
                            }

                            student.duplicate = true;
                        }
                    }
                }

                var studentDuplicate_name_dob = dbBasicInfor.CheckNameAndBirthday(txtFullName.Text, dtBirthday.DateTime.ToString("yyyy-MM-dd"));
                if (studentDuplicate_name_dob.Count > 0)
                {
                    if (MessageBox.Show("Duplicate Fullname and birthday of this student! \nDo you want to still save?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                        return;
                    else
                    {
                        foreach (var otherStu in studentDuplicate_name_dob)
                        {
                            otherStu.duplicate = true;
                            dbBasicInfor.EditInforStudent(otherStu.studentId, otherStu);
                        }

                        student.duplicate = true;
                    }
                }

                student.lastedUpdate = mainForm.user.fullName;

                if (checkKobecAccept)
                {
                    student.orderKobecAccept = dbBasicInfor.UpdateOrderAcceptKobecForNewStudent(cboYear.Text + "." + cboSesson.Text);
                }

                dbBasicInfor.AddNewStudent(student);

                IsCreated = true;

                mainForm.SetHisOperate("Create new student with StudentID= " + lblStudentID.Text + " Name= " + txtFullName.Text);

                if (MessageBox.Show("Create new candidate student success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    mainForm.SetControlStudentListCandidate();
                }
            }
            else
            {
                var editStudent = dbBasicInfor.GetInforDetailStudent(student.studentId);
                student.duplicate = editStudent.duplicate;

                if (!student.duplicate)
                {
                    if (txtPassportNo.Text != "")
                    {
                        var studentDuplicatepassport = dbBasicInfor.CheckPassportNumber(txtPassportNo.Text, lblStudentID.Text);
                        if (studentDuplicatepassport.Count > 0)
                        {
                            if (MessageBox.Show("Duplicate passport number of this student! \nDo you want to still save?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                            {
                                return;
                            }
                            else
                            {
                                foreach (var otherStu in studentDuplicatepassport)
                                {
                                    otherStu.duplicate = true;
                                    dbBasicInfor.EditInforStudent(otherStu.studentId, otherStu);
                                }

                                student.duplicate = true;
                            }
                        }
                    }

                    var studentDuplicate_name_dob = dbBasicInfor.CheckNameAndBirthday(txtFullName.Text, dtBirthday.DateTime.ToString("yyyy-MM-dd"), lblStudentID.Text);
                    if (studentDuplicate_name_dob.Count > 0)
                    {
                        if (MessageBox.Show("Duplicate Fullname and birthday of this student! \nDo you want to still save?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            foreach (var otherStu in studentDuplicate_name_dob)
                            {
                                otherStu.duplicate = true;
                                dbBasicInfor.EditInforStudent(otherStu.studentId, otherStu);
                            }

                            student.duplicate = true;
                        }
                    }
                }

                mainForm.SetHisOperate("Edit student basic information of student with StudentID= " + lblStudentID.Text + " Name= " + txtFullName.Text);

                if (MessageBox.Show("You edited candidate student file success!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {

                }

                student.lastedUpdate = mainForm.user.fullName;
                dbBasicInfor.EditInforStudent(lblStudentID.Text, student);
                if (checkKobecAccept)
                {
                    dbBasicInfor.UpdateOrderAcceptKobecForOldStudent(cboYear.Text + "." + cboSesson.Text, lblStudentID.Text);
                }
            }


            if (rbtMale.Checked)
            {
                lblGenger.Text = "Male";
            }
            else
            {
                lblGenger.Text = "Female";
            }

            lblHomeAddress.Text = txtHomeAddress.Text;
            lblHighschoolName.Text = txtHighschoolName.Text;
            lblSemester.Text = cboYear.Text;
            lblSesson.Text = cboSesson.Text;
            lblKobecPartner.Text = cboKobecPartner.Text;
            lblBankBalance.Text = txtBankBalance.Text;
            lblBankAccountOwner.Text = txtBankAccountOwner.Text;
            lblRelationship.Text = txtRelationship.Text;
            lblGraduationDate.Text = dtGraduationDate.DateTime.ToString("yyyy-MM-dd");

            SettingColorForBasicForm();

            if (student.addmissionResult == 2)
            {
                if (mainForm.user.accountType == 2)
                    return;

                stAdmissionInfor.Visible = true;
                stInvoice.Visible = true;
            }
            else
            {
                stAdmissionInfor.Visible = false;
                stInvoice.Visible = false;
            }
        }

        void SettingColorForBasicForm()
        {
            if (txtFullName.Text != "")
            {
                txtFullName.BackColor = Color.White;
            }
            if (dtBirthday.EditValue != null)
            {
                dtBirthday.BackColor = Color.White;
            }
            if (txtHomeAddress.Text != "")
            {
                txtHomeAddress.BackColor = Color.White;
            }
            if (txtPhone.Text != "")
            {
                txtPhone.BackColor = Color.White;
            }
            if (txtBankBalance.Text != "")
            {
                txtBankBalance.BackColor = Color.White;
            }
            if (txtBankAccountOwner.Text != "")
            {
                txtBankAccountOwner.BackColor = Color.White;
            }
            if (txtRelationship.Text != "")
            {
                txtRelationship.BackColor = Color.White;
            }
            if (txtPassportNo.Text != "")
            {
                txtPassportNo.BackColor = Color.White; ;
            }
            if (dtIssueDate.EditValue != null)
            {
                dtIssueDate.BackColor = Color.White;
            }
            if (dtExpireDate.EditValue != null)
            {
                dtExpireDate.BackColor = Color.White;
            }
            if (dtGraduationDate.EditValue != null)
            {
                dtGraduationDate.BackColor = Color.White;
            }
            if (txtHighschoolName.Text != "")
            {
                txtHighschoolName.BackColor = Color.White;
            }
            if (txtSchoolAddress.Text != "")
            {
                txtSchoolAddress.BackColor = Color.White;
            }
            if (txtSchoolContact.Text != "")
            {
                txtSchoolContact.BackColor = Color.White;
            }
            if (cboYear.Text != "")
            {
                cboYear.BackColor = Color.White;
            }
            if (txtFatherName.Text != "")
            {
                txtFatherName.BackColor = Color.White;
            }
            if (txtFatherJob.Text != "")
            {
                txtFatherJob.BackColor = Color.White;
            }
            if (dtFatherDob.EditValue != null)
            {
                dtFatherDob.BackColor = Color.White;
            }
            if (txtFatherContact.Text != "")
            {
                txtFatherContact.BackColor = Color.White;
            }
            if (txtMotherName.Text != "")
            {
                txtMotherName.BackColor = Color.White;
            }
            if (txtMotherJob.Text != "")
            {
                txtMotherJob.BackColor = Color.White;
            }
            if (dtMotherDob.EditValue != null)
            {
                dtMotherDob.BackColor = Color.White;
            }
            if (txtMotherContact.Text != "")
            {
                txtMotherContact.BackColor = Color.White;
            }
            if (dtCVRecordDate.EditValue != null)
            {
                dtCVRecordDate.BackColor = Color.White;
            }
            if (rtbRemarkBasicInfor.Text != "")
            {
                rtbRemarkBasicInfor.BackColorRichTextBox = Color.White;
            }
            if (dtHardCopyCVRecivedDate.EditValue != null)
            {
                dtHardCopyCVRecivedDate.BackColor = Color.White;
            }
            if (dtSubmissionDate.EditValue != null)
            {
                dtSubmissionDate.BackColor = Color.White;
            }
        }

        private void btnSaveUpdateInfor_Click(object sender, EventArgs e)
        {
            if (lblStudentID.Text == "")
            {
                MessageBox.Show("Candidate not yet created", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AdmissionInfor record = new AdmissionInfor(lblStudentID.Text);

            record.invoiceDate = dtInvoiceDate.DateTime;
            record.addmissionNo = txtAddmissionNo.Text;

            decimal TuitionPayAmount = 0;
            if (decimal.TryParse(txtTuitionPayAmount.Text, out TuitionPayAmount))
            {
                record.tuitionPayAmount = TuitionPayAmount;
            }
            else
            {
                MessageBox.Show("You enter tuition pay amount wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            record.tuitionPayDate = dtTutionPayDate.DateTime;
            record.visa = cboVisa.Text;
            record.visaCodeDate = dtVisaCodeDate.DateTime;

            decimal DepositAmount = 0;
            if (decimal.TryParse(txtDepositAmount.Text, out DepositAmount))
            {
                record.depositAmount = DepositAmount;
            }
            else
            {
                MessageBox.Show("You enter deposit amount wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            lblUnitAmount.Text = record.depositUnit = cboDepositUnit.Text;
            record.depositStartDate = dtDepositStartDate.DateTime;
            float DepositProfit = 0;
            if (float.TryParse(txtDepositProfit.Text, out DepositProfit))
            {
                record.depositProfit = DepositProfit;
            }
            else
            {
                MessageBox.Show("You enter deposit profit wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            record.koreaEntryPlan = dtKoreaEntryPlan.DateTime;
            record.flyNumber = txtFlyNumber.Text;

            record.EntranceDate = dtEntranceDate.DateTime;

            if (dtEntranceDate.EditValue != null)
            {
                btnCancel.Visible = false;

                var student = dbBasicInfor.GetInforDetailStudent(lblStudentID.Text);
                if (student.duplicate)
                {
                    if (MessageBox.Show("You edited candidate student file success, this student have record duplicate.\nDo you want to remove all duplicate student dublicate?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        //Note Remark
                        var note = dbBasicInfor.DeleteStudentDuplicate(student);

                        student.duplicate = false;
                        //dbBasicInfor.EditInforStudent(lblStudentID.Text, student);

                        StudentRemarksAdmission remark = new StudentRemarksAdmission(lblStudentID.Text);
                        remark.remarks += "\n " + note;
                        dbAdmissionInfor.SaveRemarks(lblStudentID.Text, remark);

                        if (mainForm.user.accountType != 2)
                        {
                            stInforStudentInKorea.Visible = true;
                            stStudyInfor.Visible = true;
                            stViewStudentInfor.Visible = true;
                            stInvoice.Visible = false;
                        }
                    }
                }
            }

            record.lastedUpdate = mainForm.user.fullName;
            if (dbAdmissionInfor.GetAdmissionInforDetail(lblStudentID.Text) == null)
            {
                dbAdmissionInfor.AddNewAdmissionInfor(record);
            }
            else
            {
                dbAdmissionInfor.EditAdmissionInfor(lblStudentID.Text, record);
            }

            mainForm.SetHisOperate("Update information of student before entry Korea, StudentID= " + lblStudentID.Text + " Name= " + txtFullName.Text);

            lblAmount.Text = txtDepositAmount.Text;
            if (dtDepositStartDate.DateTime.Ticks != 0)
                lblStartDate.Text = dtDepositStartDate.DateTime.ToString("yyyy-MM-dd");
            lblProfit.Text = txtDepositProfit.Text;

            if (dtEntranceDate.DateTime.Ticks != 0)
                lblKoreaEntryDate.Text = dtEntranceDate.DateTime.ToString("yyyy-MM-dd");

            // save note
            NoticePaymentDeposit rec = new NoticePaymentDeposit();
            rec.studentId = lblStudentID.Text;
            rec.nameStudent = txtFullName.Text;
            rec.guaranteeName = txtBankAccountOwner.Text;
            rec.paymentDate = dtDepositStartDate.DateTime;
            if (dtDepositStartDate.DateTime.Ticks != 0)
            {
                rec.expirationDate = dtDepositStartDate.DateTime.AddYears(1);
            }

            double money = Convert.ToDouble(txtDepositAmount.Text);
            float rate = Convert.ToSingle(txtDepositProfit.Text);
            rec.money = txtDepositAmount.Text;
            rec.interestRate = rate;
            rec.totalMoney = (money + money * rate).ToString();

            dbEvent.AddEventPaymentDeposit(rec);
        }

        int idUpdateInfor = 0;
        private void newRecordToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            UpdateInforStudentInKorea form;
            var records = dbStudentInforInKorea.GetStudentInforInKoreas(lblStudentID.Text);
            if (records.Count != 0)
            {
                StudentInforInKorea lastRecord = records.Last();
                form = new UpdateInforStudentInKorea(lastRecord, true);
            }
            else
            {
                form = new UpdateInforStudentInKorea();
            }

            if (form.ShowDialog() == DialogResult.OK)
            {
                StudentInforInKorea record = new StudentInforInKorea(lblStudentID.Text);
                record.NoIdentifierKorea = form.GetNoIdntifier();
                record.IdentifierExpireDate = form.GetIdentifierExpireDate();
                record.contact = form.GetContact();
                record.address = form.GetAddress();
                record.addressEntry = form.GetAddressEntry();
                record.addressEnd = form.GetAddressEnd();
                record.addressManager = form.GetAddressManager();
                record.addressNote = form.GetAddressNote();
                record.userAccount = form.GetUserAccount();
                record.numberAccount = form.GetNumberAccount();
                record.money = form.GetMoney();
                record.managerName = form.GetManagementName();
                record.managerPhone = form.GetManagementPhone();
                record.dateUpdate = form.GetDateUpdate();
                record.parttimeJob = form.GetParttimeJob();
                record.parttimeAddress = form.GetPartTimeAddress();
                record.parttimeEntry = form.GetParttimeEntry();
                record.parttimeEnd = form.GetParttimeEnd();
                record.parttimeNote = form.GetParttimeNote();
                record.lastedUpdate = mainForm.user.fullName;

                int id = dbStudentInforInKorea.AddNewStudentInforInKorea(record);

                mainForm.SetHisOperate("New record information of student in Korea, StudentID= " + lblStudentID.Text + " Name= " + txtFullName.Text);

                GridPanel panel = sgUpdateInfor.PrimaryGrid;
                sgUpdateInfor.BeginUpdate();

                string IdentifierExpireDate = "NA";
                if (record.IdentifierExpireDate != DateTime.MinValue)
                {
                    IdentifierExpireDate = record.IdentifierExpireDate.ToString("yyyy-MM-dd");
                }

                string addressEntry = "NA";
                if (record.addressEntry != DateTime.MinValue)
                {
                    addressEntry = record.addressEntry.ToString("yyyy-MM-dd");
                }

                string addressEnd = "NA";
                if (record.addressEnd != DateTime.MinValue)
                {
                    addressEnd = record.addressEnd.ToString("yyyy-MM-dd");
                }

                string parttimeEntry = "NA";
                if (record.parttimeEntry != DateTime.MinValue)
                {
                    parttimeEntry = record.parttimeEntry.ToString("yyyy-MM-dd");
                }

                string parttimeEnd = "NA";
                if (record.parttimeEnd != DateTime.MinValue)
                {
                    parttimeEnd = record.parttimeEnd.ToString("yyyy-MM-dd");
                }

                object[] ob1 = new object[]
                        {
                    id, record.dateUpdate.ToString("yyyy-MM-dd"), record.NoIdentifierKorea, IdentifierExpireDate, record.contact,
                    record.address,addressEntry, addressEnd, form.GetStrAddressManager(), record.addressNote,
                    record.userAccount, record.numberAccount, record.money, record.managerName,record.managerPhone,
                    record.parttimeJob, parttimeEntry, parttimeEnd, record.parttimeAddress, record.parttimeNote
                        };

                panel.Rows.Add(new GridRow(ob1));
                sgUpdateInfor.EndUpdate();

                panel = sgInforInKorea_View.PrimaryGrid;
                sgInforInKorea_View.BeginUpdate();

                ob1 = new object[]
                        {
                    id, record.NoIdentifierKorea,record.IdentifierExpireDate.ToString("yyyy-MM-dd"), record.address, record.paymentDate.ToString("yyyy-MM-dd"),
                    record.userAccount, record.numberAccount, record.money, record.managerName, record.managerPhone
                        };

                panel.Rows.Add(new GridRow(ob1));
                sgInforInKorea_View.EndUpdate();

                //Save note visa
                if (form.GetIdentifierExpireDate().Ticks != 0)
                {
                    NoticeVisaRenewal noteVisa = new NoticeVisaRenewal();
                    noteVisa.studentId = lblStudentID.Text;
                    noteVisa.nameStudent = txtFullName.Text;
                    noteVisa.expirationDate = form.GetIdentifierExpireDate();
                    noteVisa.address = form.GetAddress();
                    noteVisa.phoneStudent = txtPhone.Text;
                    noteVisa.phoneManager = form.GetManagementPhone();

                    dbEvent.AddVisaRenewal(noteVisa);
                }

                //Save note pay rent
                if (form.GetPaymentDate().Ticks != 0)
                {
                    NoticePayRent notePayRent = new NoticePayRent();
                    notePayRent.studentId = lblStudentID.Text;
                    notePayRent.address = form.GetAddress();
                    notePayRent.numberAccount = form.GetNumberAccount();
                    notePayRent.paymentDate = form.GetPaymentDate();
                    notePayRent.money = form.GetMoney();
                    notePayRent.nameStudent = txtFullName.Text;
                    if (form.GetAddressEntry().Ticks != 0)
                    {
                        notePayRent.nowMonth = form.GetAddressEntry().Month;
                    }

                    dbEvent.AddEventPayRent(notePayRent);
                }
            }
        }

        private void editToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (idUpdateInfor == 0)
                return;

            StudentInforInKorea oldRecord = dbStudentInforInKorea.GetStudentInforInKoreaDetail(lblStudentID.Text, idUpdateInfor);
            if (oldRecord != null)
            {
                UpdateInforStudentInKorea form = new UpdateInforStudentInKorea(oldRecord, false);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    StudentInforInKorea record = new StudentInforInKorea(lblStudentID.Text);
                    record.NoIdentifierKorea = form.GetNoIdntifier();
                    record.IdentifierExpireDate = form.GetIdentifierExpireDate();
                    record.contact = form.GetContact();
                    record.address = form.GetAddress();
                    record.addressEntry = form.GetAddressEntry();
                    record.addressEnd = form.GetAddressEnd();
                    record.addressManager = form.GetAddressManager();
                    record.addressNote = form.GetAddressNote();
                    record.paymentDate = form.GetPaymentDate();
                    record.userAccount = form.GetUserAccount();
                    record.numberAccount = form.GetNumberAccount();
                    record.money = form.GetMoney();
                    record.managerName = form.GetManagementName();
                    record.managerPhone = form.GetManagementPhone();
                    record.dateUpdate = form.GetDateUpdate();
                    record.parttimeJob = form.GetParttimeJob();
                    record.parttimeEntry = form.GetParttimeEntry();
                    record.parttimeEnd = form.GetParttimeEnd();
                    record.parttimeAddress = form.GetPartTimeAddress();
                    record.parttimeNote = form.GetParttimeNote();
                    record.lastedUpdate = mainForm.user.fullName;

                    dbStudentInforInKorea.EditStudentInforInKorea(idUpdateInfor, record);

                    mainForm.SetHisOperate("Edit record information of student in Korea, StudentID= " + lblStudentID.Text + " Name= " + txtFullName.Text);

                    string IdentifierExpireDate = "NA";
                    if (record.IdentifierExpireDate != DateTime.MinValue)
                    {
                        IdentifierExpireDate = record.IdentifierExpireDate.ToString("yyyy-MM-dd");
                    }

                    string addressEntry = "NA";
                    if (record.addressEntry != DateTime.MinValue)
                    {
                        addressEntry = record.addressEntry.ToString("yyyy-MM-dd");
                    }

                    string addressEnd = "NA";
                    if (record.addressEnd != DateTime.MinValue)
                    {
                        addressEnd = record.addressEnd.ToString("yyyy-MM-dd");
                    }

                    string paymentDate = "NA";
                    if (record.paymentDate != DateTime.MinValue)
                    {
                        paymentDate = record.paymentDate.ToString("yyyy-MM-dd");
                    }

                    string parttimeEntry = "NA";
                    if (record.parttimeEntry != DateTime.MinValue)
                    {
                        parttimeEntry = record.parttimeEntry.ToString("yyyy-MM-dd");
                    }

                    string parttimeEnd = "NA";
                    if (record.parttimeEnd != DateTime.MinValue)
                    {
                        parttimeEnd = record.parttimeEnd.ToString("yyyy-MM-dd");
                    }

                    GridPanel panel = sgUpdateInfor.PrimaryGrid;
                    var IRows = panel.Rows.GetEnumerator();
                    while (IRows.MoveNext())
                    {
                        GridRow r = (GridRow)IRows.Current;
                        if ((int)r[0].Value == oldRecord.id)
                        {
                            r[1].Value = record.dateUpdate.ToString("yyyy-MM-dd");
                            r[2].Value = record.NoIdentifierKorea;
                            r[3].Value = IdentifierExpireDate;
                            r[4].Value = record.contact;
                            r[5].Value = record.address;
                            r[6].Value = addressEntry;
                            r[7].Value = addressEnd;
                            r[8].Value = form.GetStrAddressManager();
                            r[9].Value = record.addressNote;
                            r[10].Value = record.userAccount;
                            r[11].Value = record.numberAccount;
                            r[12].Value = record.money;
                            r[13].Value = record.managerName;
                            r[14].Value = record.managerPhone;
                            r[15].Value = record.parttimeJob;
                            r[16].Value = parttimeEntry;
                            r[17].Value = parttimeEnd;
                            r[18].Value = record.parttimeAddress;
                            r[19].Value = record.parttimeNote;
                        }
                    }

                    panel = sgInforInKorea_View.PrimaryGrid;
                    IRows = panel.Rows.GetEnumerator();
                    while (IRows.MoveNext())
                    {
                        GridRow r = (GridRow)IRows.Current;
                        if ((int)r[0].Value == oldRecord.id)
                        {
                            r[1].Value = record.NoIdentifierKorea;
                            r[2].Value = IdentifierExpireDate;
                            r[3].Value = record.address;
                            r[4].Value = paymentDate;
                            r[5].Value = record.userAccount;
                            r[6].Value = record.numberAccount;
                            r[7].Value = record.money;
                            r[8].Value = record.managerName;
                            r[9].Value = record.managerPhone;
                        }
                    }

                    //Save note visa
                    if (form.GetIdentifierExpireDate().Ticks != 0)
                    {
                        NoticeVisaRenewal noteVisa = new NoticeVisaRenewal();
                        noteVisa.studentId = lblStudentID.Text;
                        noteVisa.nameStudent = txtFullName.Text;
                        noteVisa.expirationDate = form.GetIdentifierExpireDate();
                        noteVisa.address = form.GetAddress();
                        noteVisa.phoneStudent = txtPhone.Text;
                        noteVisa.phoneManager = form.GetManagementPhone();

                        dbEvent.AddVisaRenewal(noteVisa);
                    }

                    //Save note pay rent
                    if (form.GetPaymentDate().Ticks != 0)
                    {
                        NoticePayRent notePayRent = new NoticePayRent();
                        notePayRent.studentId = lblStudentID.Text;
                        notePayRent.address = form.GetAddress();
                        notePayRent.numberAccount = form.GetNumberAccount();
                        notePayRent.paymentDate = form.GetPaymentDate();
                        notePayRent.money = form.GetMoney();
                        notePayRent.nameStudent = txtFullName.Text;

                        if (form.GetAddressEntry().Ticks != 0)
                        {
                            notePayRent.nowMonth = form.GetAddressEntry().Month;
                        }

                        dbEvent.AddEventPayRent(notePayRent);
                    }

                }
            }

            idUpdateInfor = 0;
        }

        private void deleteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (idUpdateInfor == 0)
            {
                return;
            }
            else
            {
                dbStudentInforInKorea.DeleteStudentInforInKorea(idUpdateInfor);

                mainForm.SetHisOperate("Delete record information of student in Korea, StudentID= " + lblStudentID.Text + " Name= " + txtFullName.Text);

                GridPanel panel = sgUpdateInfor.PrimaryGrid;
                var IRows = panel.Rows.GetEnumerator();
                GridRow _r = null;

                while (IRows.MoveNext())
                {
                    GridRow r = (GridRow)IRows.Current;
                    if ((int)r[0].Value == idUpdateInfor)
                    {
                        _r = r;
                    }
                }
                panel.Rows.Remove(_r);

                panel = sgInforInKorea_View.PrimaryGrid;
                IRows = panel.Rows.GetEnumerator();
                _r = null;
                while (IRows.MoveNext())
                {
                    GridRow r = (GridRow)IRows.Current;
                    if ((int)r[0].Value == idUpdateInfor)
                    {
                        _r = r;
                    }
                }
                panel.Rows.Remove(_r);

                idUpdateInfor = 0;

                lblNoIdentifierKorea.Text = "NA";
                lblIdentifierExpire.Text = "NA";
                lblContact.Text = "NA";
                lblAddress.Text = "NA";
                lblAddressEntry.Text = "NA";
                lblAddressEnd.Text = "NA";
                lblAddressManager.Text = "NA";
                lblUserAccount.Text = "NA";
                lblNumberAccount.Text = "NA";
                lblMoney.Text = "NA";
                lblManagerName.Text = "NA";
                lblManagerPhone.Text = "NA";
                lblPartTimeJob.Text = "NA";
                lblPartTimeEntry.Text = "NA";
                lblPartTimeEnd.Text = "NA";
                lblPartTimeAddress.Text = "NA";
            }
        }

        private void sgStudylanguge_CellMouseUp(object sender, GridCellMouseEventArgs e)
        {
            contextStudyLanguge.Items[0].Enabled = false;
            contextStudyLanguge.Items[1].Enabled = true;
            contextStudyLanguge.Items[2].Enabled = true;

            int rowIndex = sgStudylanguge.ActiveRow.RowIndex;
            GridCell gridCell = sgStudylanguge.GetCell(rowIndex, 0);
            idStudyLanguge = (int)gridCell.Value;
        }

        private void sgStudySpecialized_CellMouseUp(object sender, GridCellMouseEventArgs e)
        {
            contextStudySpecialized.Items[0].Enabled = false;
            contextStudySpecialized.Items[1].Enabled = true;
            contextStudySpecialized.Items[2].Enabled = true;

            int rowIndex = sgStudySpecialized.ActiveRow.RowIndex;
            GridCell gridCell = sgStudySpecialized.GetCell(rowIndex, 0);
            idStudySpecified = (int)gridCell.Value;
        }

        private void sgStudylanguge_MouseDown(object sender, MouseEventArgs e)
        {
            contextStudyLanguge.Items[0].Enabled = true;
            contextStudyLanguge.Items[1].Enabled = false;
            contextStudyLanguge.Items[2].Enabled = false;
        }

        private void sgStudySpecialized_MouseDown(object sender, MouseEventArgs e)
        {
            contextStudySpecialized.Items[0].Enabled = true;
            contextStudySpecialized.Items[1].Enabled = false;
            contextStudySpecialized.Items[2].Enabled = false;
        }


        private void btnSaveRemarks_Click(object sender, EventArgs e)
        {
            StudentRemarksAdmission remark = new StudentRemarksAdmission(lblStudentID.Text);
            remark.remarks = rtbRemarkAdmissionInfor.Text;
            remark.lastedUpdate = mainForm.user.fullName;

            dbAdmissionInfor.SaveRemarks(lblStudentID.Text, remark);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            HigherEducation form = new HigherEducation(mainForm);
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (lblStudentID.Text == "")
                {
                    MessageBox.Show("Candidate not yet created", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                StudentBasicInforHigherSchool record = new StudentBasicInforHigherSchool(lblStudentID.Text);
                record.schoolName = form.GetSchoolName();
                record.schoolType = form.GetHigherSchoolType();
                record.major = form.GetMajor();
                record.yearOfGradualtion = form.GetYearOfGradualtion();
                record.gpa = form.GetGPA();

                int id = dbBasicInfor.AddHigherSchoolsInfor(record);

                GridPanel panel = sgHigherSchool.PrimaryGrid;
                sgHigherSchool.BeginUpdate();

                object[] ob1 = new object[]
                        {
                    id, record.schoolName, record.schoolType, record.major, record.yearOfGradualtion, record.gpa
                        };

                panel.Rows.Add(new GridRow(ob1));
                sgHigherSchool.EndUpdate();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (idFileAdmission != 0)
            {
                StudentFile file = dbFile.GetFileDetail(idFileAdmission);

                string nameFile = directory + @"\temp.pdf";

                File.WriteAllBytes(nameFile, file.fileContent);
                file = null;
                FormViewPdf form = new FormViewPdf(nameFile);
                form.ShowDialog();
            }

            if (idFileBasicInfor != 0)
            {
                StudentFile file = dbFile.GetFileDetail(idFileBasicInfor);

                string nameFile = directory + @"\temp.pdf";

                File.WriteAllBytes(nameFile, file.fileContent);
                file = null;
                FormViewPdf form = new FormViewPdf(nameFile);
                form.ShowDialog();
            }

            if (idFileInforInKorea != 0)
            {
                StudentFile file = dbFile.GetFileDetail(idFileInforInKorea);

                string nameFile = directory + @"\temp.pdf";

                File.WriteAllBytes(nameFile, file.fileContent);
                file = null;
                FormViewPdf form = new FormViewPdf(nameFile);
                form.ShowDialog();
            }
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog form = new OpenFileDialog();
            if (form.ShowDialog() == DialogResult.OK)
            {
                StudentDocument doc = new StudentDocument();
                doc.studentId = lblStudentID.Text;
                doc.fileName = form.SafeFileName;
                doc.documentType = 2; //Admission Infor

                if (!form.CheckPathExists)
                {
                    MessageBox.Show("File don't exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FileStream fStream = File.OpenRead(form.FileName);
                byte[] contents = new byte[fStream.Length];
                fStream.Read(contents, 0, (int)fStream.Length);
                fStream.Close();

                StudentFile file = new StudentFile();
                file.fileContent = contents;

                int id = dbFile.UploadFile(doc, file);

                GridPanel panel = sgUploadFileAdmission.PrimaryGrid;
                sgUploadFileAdmission.BeginUpdate();

                object[] ob1 = new object[]
                        {
                    id, doc.fileName
                        };

                panel.Rows.Add(new GridRow(ob1));
                sgUploadFileAdmission.EndUpdate();
            }
        }

        private void sgUpdateInfor_CellMouseUp(object sender, GridCellMouseEventArgs e)
        {
            contextUpdateInfor.Items[0].Enabled = false;
            contextUpdateInfor.Items[1].Enabled = true;
            contextUpdateInfor.Items[2].Enabled = true;

            int rowIndex = sgUpdateInfor.ActiveRow.RowIndex;
            GridCell gridCell = sgUpdateInfor.GetCell(rowIndex, 0);
            idUpdateInfor = (int)gridCell.Value;


            GridPanel panel = sgUpdateInfor.PrimaryGrid;
            var IRows = panel.Rows.GetEnumerator();
            while (IRows.MoveNext())
            {
                GridRow r = (GridRow)IRows.Current;
                if ((int)r[0].Value == idUpdateInfor)
                {
                    lblNoIdentifierKorea.Text = r[2].Value.ToString();
                    lblIdentifierExpire.Text = r[3].Value.ToString();
                    lblContact.Text = r[4].Value.ToString();
                    lblAddress.Text = r[5].Value.ToString();
                    lblAddressEntry.Text = r[6].Value.ToString();
                    lblAddressEnd.Text = r[7].Value.ToString();
                    lblAddressManager.Text = r[8].Value.ToString();
                    lblUserAccount.Text = r[10].Value.ToString();
                    lblNumberAccount.Text = r[11].Value.ToString();
                    lblMoney.Text = r[12].Value.ToString();
                    lblManagerName.Text = r[13].Value.ToString();
                    lblManagerPhone.Text = r[14].Value.ToString();
                    lblPartTimeJob.Text = r[15].Value.ToString();
                    lblPartTimeEntry.Text = r[16].Value.ToString();
                    lblPartTimeEnd.Text = r[17].Value.ToString();
                    lblPartTimeAddress.Text = r[18].Value.ToString();
                }
            }
        }

        private void sgUpdateInfor_MouseDown(object sender, MouseEventArgs e)
        {
            contextUpdateInfor.Items[0].Enabled = true;
            contextUpdateInfor.Items[1].Enabled = false;
            contextUpdateInfor.Items[2].Enabled = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure refresh datas?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            txtFullName.Clear();
            rbtMale.Checked = false; rbtFemale.Checked = false;
            dtBirthday.EditValue = null;
            txtHomeAddress.Clear();
            txtPhone.Clear();
            txtBankBalance.Clear();
            txtBankAccountOwner.Clear();
            txtRelationship.Clear();
            txtPassportNo.Clear();
            dtIssueDate.EditValue = null;
            dtExpireDate.EditValue = null;

            cboUniversity.Refresh();
            cboYear.Refresh();
            txtFatherContact.Clear();
            txtFatherName.Clear();
            txtFatherJob.Clear();
            dtFatherDob.EditValue = null;
            txtMotherName.Clear();
            txtMotherContact.Clear();
            txtMotherJob.Clear();
            dtMotherDob.EditValue = null;
            numMemberOfFamily.Value = 0;

            sgStudyHighschool.GetCell(0, 0).Value = 0;
            sgStudyHighschool.GetCell(0, 1).Value = 0;
            sgStudyHighschool.GetCell(0, 2).Value = 0;
            sgStudyHighschool.GetCell(0, 3).Value = 0;
            sgStudyHighschool.GetCell(0, 4).Value = 0;
            sgStudyHighschool.GetCell(0, 5).Value = 0;
            lblTotalDayOff.ResetText();
            lblAverage.ResetText();
            dtGraduationDate.EditValue = null;
            txtHighschoolName.Clear();
            txtSchoolAddress.Clear();
            txtSchoolContact.Clear();

            sgHigherSchool.PrimaryGrid.Rows.Clear();

            if (picImage.Image != null)
            {
                picImage.Image.Dispose();
                picImage.Image = null;
            }
            cboKobecPartner.Refresh();
            dtCVRecordDate.ResetText();
            rtbRemarkBasicInfor.Clear();
            rbtAccept_CV.Checked = false; rbtReject_CV.Checked = false;
            rbtAccept_Interview.Checked = false; rbtReject_Interview.Checked = false;
            dtHardCopyCVRecivedDate.EditValue = null;
            dtSubmissionDate.EditValue = null;
            rbtAdmissionAccept.Checked = false; rbtAdmissionReject.Checked = false;
        }


        private void btnExportToFile_Click(object sender, EventArgs e)
        {
            Document document = new Document(directory + "/FormExport.docx");

            if (picImage.Image != null)
            {
                Table table1 = (Table)document.Sections[0].Tables[0];
                DocPicture picture = table1.Rows[0].Cells[2].Paragraphs[0].AppendPicture(picImage.Image);
                picture.Width = 90;
                picture.Height = 130;
            }

            document.FindString("IDStudent", true, true)[0] = lblStudentID.Text;
            document.FindString("FullNameVietNam", true, true)[0] = txtFullName.Text.ToUpper();
            document.FindString("FullNameEnglish", true, true)[0] = Utilities.ConvertToUnSign2(txtFullName.Text).ToUpper();
            document.FindString("DOB", true, true)[0] = dtBirthday.DateTime.ToString("yyyy.MM.dd");
            document.FindString("HomeAddress", true, true)[0] = txtHomeAddress.Text;

            if (rbtMale.Checked)
                document.FindString("GENDER", true, true)[0] = "MALE";
            else
                document.FindString("GENDER", true, true)[0] = "FEMALE";

            document.FindString("NoPassport", true, true)[0] = txtPassportNo.Text;
            document.FindString("IssueDate", true, true)[0] = dtIssueDate.DateTime.ToString("yyyy.MM.dd");
            document.FindString("ContactVN", true, true)[0] = txtPhone.Text;
            document.FindString("KobecPartner", true, true)[0] = cboKobecPartner.Text;

            var inforStudentInKoreas = dbStudentInforInKorea.GetStudentInforInKoreas(lblStudentID.Text);
            if (inforStudentInKoreas.Count > 0)
            {
                var inforStudentInKoreaLast = inforStudentInKoreas.Last();
                if (inforStudentInKoreaLast.NoIdentifierKorea != "")
                {
                    document.FindString("AlienRegistration", true, true)[0] = inforStudentInKoreaLast.NoIdentifierKorea;
                }
                else
                {
                    document.FindString("AlienRegistration", true, true)[0] = "NA";
                }

                string duration = "NA";
                if (inforStudentInKoreaLast.addressEntry.Ticks != 0)
                {
                    duration = inforStudentInKoreaLast.addressEntry.ToString("yyyy/MM/dd") + " - ";
                    if (inforStudentInKoreaLast.addressEnd.Ticks != 0)
                    {
                        duration += inforStudentInKoreaLast.addressEnd.ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        duration += "NA";
                    }

                    document.FindString("DurationStay", true, true)[0] = duration;
                }
                else
                {
                    document.FindString("DurationStay", true, true)[0] = "NA";
                }

                if (inforStudentInKoreaLast.address != "")
                {
                    document.FindString("CurrentAddressKorea", true, true)[0] = inforStudentInKoreaLast.address;
                }
                else
                {
                    document.FindString("CurrentAddressKorea", true, true)[0] = "NA";
                }

                if (inforStudentInKoreaLast.managerName != "")
                {
                    document.FindString("ManagerName", true, true)[0] = inforStudentInKoreaLast.managerName;
                }
                else
                {
                    document.FindString("ManagerName", true, true)[0] = "NA";
                }

                if (inforStudentInKoreaLast.contact != "")
                {
                    document.FindString("ContactKorea", true, true)[0] = inforStudentInKoreaLast.contact;
                }
                else
                {
                    document.FindString("ContactKorea", true, true)[0] = "NA";
                }

                if (inforStudentInKoreaLast.managerPhone != "")
                {
                    document.FindString("ManagerContact", true, true)[0] = inforStudentInKoreaLast.managerPhone;
                }
                else
                {
                    document.FindString("ManagerContact", true, true)[0] = "NA";
                }
            }
            else
            {
                document.FindString("AlienRegistration", true, true)[0] = "NA";
                document.FindString("DurationStay", true, true)[0] = "NA";
                document.FindString("CurrentAddressKorea", true, true)[0] = "NA";
                document.FindString("ManagerName", true, true)[0] = "NA";
                document.FindString("ContactKorea", true, true)[0] = "NA";
                document.FindString("ManagerContact", true, true)[0] = "NA";
            }

            var session = document.Sections[0];

            #region family
            Paragraph pa = session.AddParagraph();

            var textRange = pa.AppendText("FAMILY PROFILE---------------------------------------------------------\n");
            SetFormatFontWord(textRange);
            textRange.CharacterFormat.Bold = true;

            textRange = pa.AppendText("14. Total family member: " + numMemberOfFamily.Value.ToString());
            SetFormatFontWord(textRange);

            string dboFather = "NA";
            if (dtFatherDob.DateTime.Ticks != 0)
            {
                dboFather = dtFatherDob.DateTime.ToString("yyyy/MM/dd");
            }

            string dboMother = "NA";
            if (dtMotherDob.DateTime.Ticks != 0)
            {
                dboMother = dtMotherDob.DateTime.ToString("yyyy/MM/dd");
            }

            Table table = session.AddTable(true);
            String[] header1 = { "Member name", "Relationship", "DOB", "Job", "Contact" };
            String[][] data1 = {
                                  new String[]{ txtFatherName.Text,"Father", dboFather, txtFatherJob.Text,txtFatherContact.Text},
                                  new String[]{ txtMotherName.Text,"Mother", dboMother, txtMotherJob.Text, txtMotherContact.Text },
                              };

            FillDataIntoTable(table, header1, data1);
            table.AutoFit(AutoFitBehaviorType.AutoFitToWindow);

            #endregion

            #region school profile
            pa = session.AddParagraph();
            pa.AppendText("\n");

            textRange = pa.AppendText("SCHOOL PROFILE---------------------------------------------------------\n");
            SetFormatFontWord(textRange);
            textRange.CharacterFormat.Bold = true;

            textRange = pa.AppendText("15. Highschool \n");
            SetFormatFontWord(textRange);

            textRange = pa.AppendText("15-1. Highschool name:" + txtHighschoolName.Text);
            SetFormatFontWord(textRange);
            textRange = pa.AppendText("               15-2. Year of Graduation:" + dtGraduationDate.DateTime.ToString("yyyy") + "\n");
            SetFormatFontWord(textRange);

            textRange = pa.AppendText("15-3. Highschool add: " + txtSchoolAddress.Text);
            SetFormatFontWord(textRange);
            textRange = pa.AppendText("               15-4. Contact: " + txtSchoolContact.Text);
            SetFormatFontWord(textRange);

            table = session.AddTable(true);
            string[] header2 = { "Transcript", "10 class", "11 class", "12 class", "Averange / Total" };
            String[][] data2 = {
                                  new String[] { "GPA", sgStudyHighschool.GetCell(0, 0).Value.ToString(), sgStudyHighschool.GetCell(0, 1).Value.ToString(), sgStudyHighschool.GetCell(0, 2).Value.ToString(), lblAverage.Text },
                                  new String[] { "Days-off", sgStudyHighschool.GetCell(0, 3).Value.ToString(), sgStudyHighschool.GetCell(0, 4).Value.ToString(), sgStudyHighschool.GetCell(0, 5).Value.ToString(), lblTotalDayOff.Text }
                               };
            //Add Cells
            FillDataIntoTable(table, header2, data2);
            table.AutoFit(AutoFitBehaviorType.AutoFitToWindow);

            pa = session.AddParagraph();
            pa.AppendText("\n");

            textRange = pa.AppendText("16. Other Education");
            SetFormatFontWord(textRange);

            table = session.AddTable(true);
            string[] header3 = { "School name (University/college)", "Major", "Year of Graduation", "GPA" };

            var panel = sgHigherSchool.PrimaryGrid;
            var IRows = panel.Rows.GetEnumerator();

            String[][] data3 = new String[panel.Rows.Count][];

            int index = 0;
            while (IRows.MoveNext())
            {
                GridRow r = (GridRow)IRows.Current;
                String[] obj = new String[] { r[1].Value.ToString(), r[3].Value.ToString(), r[4].Value.ToString(), r[5].Value.ToString() };
                data3.SetValue(obj, index);

                index++;
            }

            FillDataIntoTable(table, header3, data3);
            table.AutoFit(AutoFitBehaviorType.AutoFitToWindow);

            #endregion

            #region application process
            pa = session.AddParagraph();
            pa.AppendText("\n");

            textRange = pa.AppendText("APPLICATION  PROCESS---------------------------------------------------------\n");
            SetFormatFontWord(textRange);
            textRange.CharacterFormat.Bold = true;

            textRange = pa.AppendText("17. School Name: " + cboUniversity.Text + " \n");
            SetFormatFontWord(textRange);
            textRange = pa.AppendText("18. Major: " + "Korean Language Trainning \n");
            SetFormatFontWord(textRange);
            textRange = pa.AppendText("19. Semester: " + cboYear.Text + "-" + cboSesson.Text);
            SetFormatFontWord(textRange);

            table = session.AddTable(true);
            string cvReview = "";
            if (rbtReject_CV.Checked)
                cvReview = "Rejected";
            else if (rbtAccept_CV.Checked)
                cvReview = "Accepted";
            else
                cvReview = "Waiting";

            string cvInterview = "";
            if (rbtReject_Interview.Checked)
                cvInterview = "Rejected";
            else if (rbtAccept_Interview.Checked)
                cvInterview = "Accepted";
            else
                cvInterview = "Waiting";

            string admissionResult = "";
            if (rbtAdmissionReject.Checked)
                admissionResult = "Rejected";
            else if (rbtAdmissionAccept.Checked)
                admissionResult = "Accepted";
            else
                admissionResult = "Waiting";

            string cvRecordDate = "NA";
            if (dtCVRecordDate.DateTime.Ticks != 0)
                cvRecordDate = dtCVRecordDate.DateTime.ToString("yyyy.MM.dd");

            string submissionDate = "NA";
            if (dtSubmissionDate.DateTime.Ticks != 0)
                submissionDate = dtSubmissionDate.DateTime.ToString("yyyy.MM.dd");

            string invoiceDate = "NA";
            if (dtInvoiceDate.DateTime.Ticks != 0)
                invoiceDate = dtInvoiceDate.DateTime.ToString("yyyy.MM.dd");

            string tuitionPayDate = "NA";
            if (dtTutionPayDate.DateTime.Ticks != 0)
                tuitionPayDate = dtTutionPayDate.DateTime.ToString("yyyy.MM.dd") + " - " + txtTuitionPayAmount.Text + " won";

            string hardCopyReceiveDate = "NA";
            if (dtHardCopyCVRecivedDate.DateTime.Ticks != 0)
                hardCopyReceiveDate = dtHardCopyCVRecivedDate.DateTime.ToString("yyyy.MM.dd");

            string visaCodeDate = "NA";
            if (dtVisaCodeDate.DateTime.Ticks != 0)
                visaCodeDate = dtVisaCodeDate.DateTime.ToString("yyyy.MM.dd");

            string depositStartDate = "NA";
            if (dtDepositStartDate.DateTime.Ticks != 0)
                depositStartDate = dtDepositStartDate.DateTime.ToString("yyyy.MM.dd");

            string koreaEntryDate = "NA";
            string koreaTerminalDate = "NA";
            if (dtEntranceDate.DateTime.Ticks != 0)
            {
                koreaEntryDate = dtEntranceDate.DateTime.ToString("yyyy.MM.dd");
                koreaTerminalDate = dtEntranceDate.DateTime.AddYears(1).ToString("yyyy.MM.dd");
            }

            string[] headerProcess = { "KOBEC Process", "Time - Note", "UNI. Process", "Time - Note" };
            String[][] dataProcess =  {
                                           new String[] { "Online CV record", cvRecordDate, "Uni. Submission", submissionDate},
                                           new String[] { "CV Review", cvReview, "Admission result", admissionResult},
                                           new String[] { "Interview", cvInterview, "Invoice", invoiceDate },
                                           new String[] { "Kobec admission", lblResultKobec.Text, "Tution payment", tuitionPayDate },
                                           new String[] { "Hard Copy CV Record", hardCopyReceiveDate, "VISA code", visaCodeDate },
                                           new String[] { "Deposit amount", txtDepositAmount.Text, "VISA", cboVisa.Text  },
                                           new String[] { "Deposit date", depositStartDate, "Korea Entry", koreaEntryDate },
                                           new String[] { "Profit (if any)", txtDepositProfit.Text + " %" },
                                           new String[]{ "Deposit terminated", koreaTerminalDate }
                                      };

            FillDataIntoTableApplicationProcess(table, headerProcess, dataProcess);
            table.AutoFit(AutoFitBehaviorType.AutoFitToWindow);


            #endregion

            #region KOREA EDUCATION  PROCESS
            pa = session.AddParagraph();
            pa.AppendText("\n");

            textRange = pa.AppendText("KOREA EDUCATION PROCESS---------------------------------------------------------\n");
            SetFormatFontWord(textRange);
            textRange.CharacterFormat.Bold = true;

            textRange = pa.AppendText("20. Korean Language Education");
            SetFormatFontWord(textRange);

            panel = sgStudylanguge.PrimaryGrid;
            table = session.AddTable(true);
            string[] header4 = { "Course", "Period", "Attended", "Score", "Up/F", "NOTE" };
            IRows = panel.Rows.GetEnumerator();

            String[][] data4 = new String[panel.Rows.Count][];

            index = 0;
            while (IRows.MoveNext())
            {
                GridRow r = (GridRow)IRows.Current;
                string period = r[4].Value.ToString() + " ~ " + r[5].Value.ToString();
                string UpFailue = r[7].Value.ToString();

                String[] obj = new String[] { r[6].Value.ToString(), period, r[7].Value.ToString(), r[8].Value.ToString(), UpFailue, r[10].Value.ToString() };
                data4.SetValue(obj, index);

                index++;
            }

            FillDataIntoTable(table, header4, data4);
            if (panel.Rows.Count > 0)
                table.AutoFit(AutoFitBehaviorType.AutoFitToContents);
            else
                table.AutoFit(AutoFitBehaviorType.AutoFitToWindow);

            pa = session.AddParagraph();
            pa.AppendText("\n");

            textRange = pa.AppendText("21. Higher Education");
            SetFormatFontWord(textRange);

            panel = sgStudySpecialized.PrimaryGrid;

            table = session.AddTable(true);
            string[] header5 = { "Course", "Period", "GPA", "Schoolarship", "NOTE" };
            IRows = panel.Rows.GetEnumerator();

            String[][] data5 = new String[panel.Rows.Count][];

            index = 0;
            while (IRows.MoveNext())
            {
                GridRow r = (GridRow)IRows.Current;
                string period = r[3].Value.ToString() + " ~ " + r[4].Value.ToString();
                string gpa = r[7].Value.ToString() + "/" + "4.5";

                String[] obj = new String[] { r[5].Value.ToString(), period, gpa, r[6].Value.ToString(), r[8].Value.ToString() };
                data5.SetValue(obj, index);

                index++;
            }

            FillDataIntoTable(table, header5, data5);
            if (panel.Rows.Count > 0)
            {
                table.AutoFit(AutoFitBehaviorType.AutoFitToContents);
            }
            else
                table.AutoFit(AutoFitBehaviorType.AutoFitToWindow);

            #endregion

            #region RESIDENT  PROCESS

            pa = session.AddParagraph();
            pa.AppendText("\n");

            textRange = pa.AppendText("RESIDENT  PROCESS---------------------------------------------------------\n");
            SetFormatFontWord(textRange);
            textRange.CharacterFormat.Bold = true;

            textRange = pa.AppendText("22. Resident process");
            SetFormatFontWord(textRange);

            panel = sgUpdateInfor.PrimaryGrid;

            table = session.AddTable(true);
            string[] header6 = { "Address", "Period", "Manager", "Accommodation fee", "Accommodation owner", "Note" };
            IRows = panel.Rows.GetEnumerator();

            String[][] data6 = new String[panel.Rows.Count][];

            index = 0;
            while (IRows.MoveNext())
            {
                GridRow r = (GridRow)IRows.Current;
                string period = r[6].Value.ToString() + " ~ " + r[7].Value.ToString();
                string accOwner = r[14].Value.ToString() + "\n" + r[15].Value.ToString();
                String[] obj = new String[] { r[5].Value.ToString(), period, r[11].Value.ToString(), r[13].Value.ToString(), accOwner, r[9].Value.ToString() };
                data6.SetValue(obj, index);

                index++;
            }

            FillDataIntoTable(table, header6, data6);
            if (panel.Rows.Count > 0)
            {
                table.AutoFit(AutoFitBehaviorType.AutoFitToContents);
            }
            else
                table.AutoFit(AutoFitBehaviorType.AutoFitToWindow);

            pa = session.AddParagraph();
            pa.AppendText("\n");

            textRange = pa.AppendText("23. Partime process");
            SetFormatFontWord(textRange);

            panel = sgUpdateInfor.PrimaryGrid;

            table = session.AddTable(true);
            string[] header7 = { "Address", "Period", "Job", "Note" };
            IRows = panel.Rows.GetEnumerator();

            String[][] data7 = new String[panel.Rows.Count][];

            index = 0;
            while (IRows.MoveNext())
            {
                GridRow r = (GridRow)IRows.Current;
                string period = r[16].Value.ToString() + " ~ " + r[17].Value.ToString();
                String[] obj = new String[] { r[18].Value.ToString(), period, r[15].Value.ToString(), r[19].Value.ToString() };
                data7.SetValue(obj, index);

                index++;
            }

            FillDataIntoTable(table, header7, data7);
            table.AutoFit(AutoFitBehaviorType.AutoFitToWindow);

            #endregion

            pa = session.AddParagraph();
            pa.AppendText("\n");

            textRange = pa.AppendText("NOTE \n");
            SetFormatFontWord(textRange);
            textRange.CharacterFormat.Bold = true;

            textRange = pa.AppendText(rtbRemarkBasicInfor.Text + "\n");
            textRange = pa.AppendText(rtbRemarkAdmissionInfor.Text + "\n");
            textRange = pa.AppendText(rtbRemarkInforInKorea.Text + "\n");
            SetFormatFontWord(textRange);

            try
            {
                document.SaveToFile("tempDoc.doc", FileFormat.Doc);

                System.Diagnostics.Process.Start(directory + "/tempDoc.doc");
            }
            catch
            {
                MessageBox.Show("You need close file word before save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void FillDataIntoTable(Table table, String[] Header, string[][] data)
        {
            //set right border of table
            table.TableFormat.Borders.Right.BorderType = Spire.Doc.Documents.BorderStyle.Hairline;
            //set top border of table
            table.TableFormat.Borders.Top.BorderType = Spire.Doc.Documents.BorderStyle.Hairline;
            //set left border of table
            table.TableFormat.Borders.Left.BorderType = Spire.Doc.Documents.BorderStyle.Hairline;
            //set bottom border is none
            table.TableFormat.Borders.Bottom.BorderType = Spire.Doc.Documents.BorderStyle.Hairline;
            //set vertical and horizontal border
            table.TableFormat.Borders.Vertical.BorderType = Spire.Doc.Documents.BorderStyle.Dot;
            table.TableFormat.Borders.Horizontal.BorderType = Spire.Doc.Documents.BorderStyle.Dot;

            table.ResetCells(data.Length + 1, Header.Length);

            //Header Row
            TableRow FRow = table.Rows[0];
            FRow.IsHeader = true;
            //Row Height
            FRow.Height = 23;
            //Header Format
            FRow.RowFormat.BackColor = Color.AliceBlue;
            FRow.RowFormat.Borders.BorderType = Spire.Doc.Documents.BorderStyle.Hairline;
            for (int i = 0; i < Header.Length; i++)
            {
                //Cell Alignment
                var paragrapHeader = FRow.Cells[i].AddParagraph();
                FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                paragrapHeader.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                paragrapHeader.Format.BeforeAutoSpacing = false;
                paragrapHeader.Format.BeforeSpacing = 0;
                paragrapHeader.Format.AfterAutoSpacing = false;
                paragrapHeader.Format.AfterSpacing = 0;

                //Data Format
                var textRange = paragrapHeader.AppendText(Header[i]);
                SetFormatFontWord(textRange);
                textRange.CharacterFormat.Bold = true;
            }

            //Data Row
            for (int r = 0; r < data.Length; r++)
            {
                TableRow DataRow = table.Rows[r + 1];

                //Row Height
                DataRow.Height = 20;

                //C Represents Column.
                for (int c = 0; c < data[r].Length; c++)
                {
                    //Cell Alignment
                    DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    //Fill Data in Rows
                    Paragraph paragrapRow = DataRow.Cells[c].AddParagraph();
                    var textRange = paragrapRow.AppendText(data[r][c]);
                    //Format Cells
                    paragrapRow.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    paragrapRow.Format.BeforeAutoSpacing = false;
                    paragrapRow.Format.BeforeSpacing = 0;
                    paragrapRow.Format.AfterAutoSpacing = false;
                    paragrapRow.Format.AfterSpacing = 0;
                    SetFormatFontWord(textRange);
                    if (c == 0)
                        paragrapRow.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                }
            }
        }

        void FillDataIntoTableApplicationProcess(Table table, String[] Header, string[][] data)
        {
            //set right border of table
            table.TableFormat.Borders.Right.BorderType = Spire.Doc.Documents.BorderStyle.Hairline;
            //set top border of table
            table.TableFormat.Borders.Top.BorderType = Spire.Doc.Documents.BorderStyle.Hairline;
            //set left border of table
            table.TableFormat.Borders.Left.BorderType = Spire.Doc.Documents.BorderStyle.Hairline;
            //set bottom border is none
            table.TableFormat.Borders.Bottom.BorderType = Spire.Doc.Documents.BorderStyle.Hairline;
            //set vertical and horizontal border
            table.TableFormat.Borders.Vertical.BorderType = Spire.Doc.Documents.BorderStyle.Dot;
            table.TableFormat.Borders.Horizontal.BorderType = Spire.Doc.Documents.BorderStyle.Dot;

            table.ResetCells(data.Length + 1, Header.Length);

            //Header Row
            TableRow FRow = table.Rows[0];
            FRow.IsHeader = true;
            //Row Height
            FRow.Height = 23;
            //Header Format
            FRow.RowFormat.BackColor = Color.AliceBlue;
            FRow.RowFormat.Borders.BorderType = Spire.Doc.Documents.BorderStyle.Hairline;
            for (int i = 0; i < Header.Length; i++)
            {
                //Cell Alignment
                var paragrapHeader = FRow.Cells[i].AddParagraph();
                FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                paragrapHeader.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                paragrapHeader.Format.BeforeAutoSpacing = false;
                paragrapHeader.Format.BeforeSpacing = 0;
                paragrapHeader.Format.AfterAutoSpacing = false;
                paragrapHeader.Format.AfterSpacing = 0;

                //Data Format
                var textRange = paragrapHeader.AppendText(Header[i]);
                SetFormatFontWord(textRange);
                textRange.CharacterFormat.Bold = true;
            }

            //Data Row
            for (int r = 0; r < data.Length; r++)
            {
                TableRow DataRow = table.Rows[r + 1];

                //Row Height
                DataRow.Height = 20;

                //C Represents Column.
                for (int c = 0; c < data[r].Length; c++)
                {
                    //Cell Alignment
                    DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    //Fill Data in Rows
                    Paragraph paragrapRow = DataRow.Cells[c].AddParagraph();
                    var textRange = paragrapRow.AppendText(data[r][c]);
                    //Format Cells
                    paragrapRow.Format.BeforeAutoSpacing = false;
                    paragrapRow.Format.BeforeSpacing = 0;
                    paragrapRow.Format.AfterAutoSpacing = false;
                    paragrapRow.Format.AfterSpacing = 0;
                    paragrapRow.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                    SetFormatFontWord(textRange);
                    if (c == 0 | c == 2)
                        textRange.CharacterFormat.Italic = true;
                }
            }
        }

        void SetFormatFontWord(TextRange textRange)
        {
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontSize = 9;
            textRange.CharacterFormat.TextColor = Color.Black;
            textRange.OwnerParagraph.Format.LineSpacingRule = LineSpacingRule.Exactly;
        }

        private void btnLoadFileBasicInfor_Click(object sender, EventArgs e)
        {
            OpenFileDialog form = new OpenFileDialog();
            if (form.ShowDialog() == DialogResult.OK)
            {
                StudentDocument doc = new StudentDocument();
                doc.studentId = lblStudentID.Text;
                doc.fileName = form.SafeFileName;
                doc.documentType = 1;

                if (!form.CheckPathExists)
                {
                    MessageBox.Show("File don't exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FileStream fStream = File.OpenRead(form.FileName);
                byte[] contents = new byte[fStream.Length];
                fStream.Read(contents, 0, (int)fStream.Length);
                fStream.Close();

                StudentFile file = new StudentFile();
                file.fileContent = contents;

                int id = dbFile.UploadFile(doc, file);

                GridPanel panel = sgLoadFileBasicInfor.PrimaryGrid;
                sgLoadFileBasicInfor.BeginUpdate();

                object[] ob1 = new object[]
                        {
                    id, doc.fileName
                        };

                panel.Rows.Add(new GridRow(ob1));
                sgLoadFileBasicInfor.EndUpdate();
            }
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

        private void rbtReject_CV_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton r = (RadioButton)sender;
            if (r.Checked)
            {
                groupBox3.Enabled = false;
            }
        }

        private void rbtAccept_CV_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton r = (RadioButton)sender;
            if (r.Checked)
            {
                groupBox3.Enabled = true;
            }
        }

        private void rbtReject_Interview_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton r = (RadioButton)sender;
            if (r.Checked)
            {
                groupBox2.Enabled = false;
                dtSubmissionDate.Enabled = false;
            }
        }

        private void rbtAccept_Interview_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton r = (RadioButton)sender;
            if (r.Checked)
            {
                groupBox2.Enabled = true;
                dtSubmissionDate.Enabled = true;
            }
        }

        private void dtIssueDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (dtIssueDate.DateTime.Ticks != 0)
            {
                dtExpireDate.DateTime = dtIssueDate.DateTime.AddYears(10);
            }
        }

        private void deleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (idFileAdmission != 0)
            {
                dbFile.DeleteFile(idFileAdmission);

                GridPanel panel = sgUploadFileAdmission.PrimaryGrid;
                var IRows = panel.Rows.GetEnumerator();
                GridRow _r = null;

                while (IRows.MoveNext())
                {
                    GridRow r = (GridRow)IRows.Current;
                    if ((int)r[0].Value == idFileAdmission)
                    {
                        _r = r;
                    }
                }
                panel.Rows.Remove(_r);

                idFileAdmission = 0;
            }

            if (idFileBasicInfor != 0)
            {
                dbFile.DeleteFile(idFileBasicInfor);

                GridPanel panel = sgLoadFileBasicInfor.PrimaryGrid;
                var IRows = panel.Rows.GetEnumerator();
                GridRow _r = null;

                while (IRows.MoveNext())
                {
                    GridRow r = (GridRow)IRows.Current;
                    if ((int)r[0].Value == idFileBasicInfor)
                    {
                        _r = r;
                    }
                }
                panel.Rows.Remove(_r);

                idFileBasicInfor = 0;
            }

            if (idFileInforInKorea != 0)
            {
                dbFile.DeleteFile(idFileInforInKorea);

                GridPanel panel = sgUploadFileInforKorea.PrimaryGrid;
                var IRows = panel.Rows.GetEnumerator();
                GridRow _r = null;

                while (IRows.MoveNext())
                {
                    GridRow r = (GridRow)IRows.Current;
                    if ((int)r[0].Value == idFileInforInKorea)
                    {
                        _r = r;
                    }
                }
                panel.Rows.Remove(_r);

                idFileInforInKorea = 0;
            }
        }

        private void cboVisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cboVisaSelect = (System.Windows.Forms.ComboBox)sender;
            if (cboVisaSelect.Text == "Yes")
            {
                dtEntranceDate.Enabled = true;
            }
            else
            {
                dtEntranceDate.Enabled = false;
                dtEntranceDate.EditValue = null;
            }
        }

        private void dtInvoiceDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (dtInvoiceDate.DateTime.Ticks != 0)
            {
                dtVisaCodeDate.Enabled = true;
            }
            else
            {
                dtVisaCodeDate.Enabled = false;
            }
        }

        private void dtVisaCodeDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (dtVisaCodeDate.DateTime.Ticks != 0)
            {
                cboVisa.Enabled = true;
            }
            else
            {
                cboVisa.Enabled = false;
            }
        }

        private void GenStudentID_Click(object sender, EventArgs e)
        {
            if (!IsCreated)
            {
                if ((rbtMale.Checked | rbtFemale.Checked) && dtBirthday.DateTime.Ticks != 0)
                {
                    lblStudentID.Text = Utilities.GenUnixId(dtBirthday.DateTime.ToString("yyMMdd"), rbtMale.Checked);
                    GenStudentID.Visible = false;
                }
                else
                {
                    MessageBox.Show("Not eligible to create student IDs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void sgStudyHighschool_CellValueChanged(object sender, GridCellValueChangedEventArgs e)
        {
            float gpa10 = Convert.ToSingle(sgStudyHighschool.GetCell(0, 0).Value);
            float gpa11 = Convert.ToSingle(sgStudyHighschool.GetCell(0, 1).Value);
            float gpa12 = Convert.ToSingle(sgStudyHighschool.GetCell(0, 2).Value);
            var avr = System.Math.Round((gpa10 + gpa11 + gpa12) / 3, 2);

            int dayOff10 = Convert.ToInt32(sgStudyHighschool.GetCell(0, 3).Value);
            int dayOff11 = Convert.ToInt32(sgStudyHighschool.GetCell(0, 4).Value);
            int dayOff12 = Convert.ToInt32(sgStudyHighschool.GetCell(0, 5).Value);
            var dayOffs = dayOff10 + dayOff11 + dayOff12;

            lblAverage.Text = avr.ToString();
            lblTotalDayOff.Text = dayOffs.ToString();
        }

        private void btnSaveInforInKorea_Click(object sender, EventArgs e)
        {
            StudentRemarksInforInKorea remark = new StudentRemarksInforInKorea(lblStudentID.Text);
            remark.remarks = rtbRemarkInforInKorea.Text;
            remark.lastedUpdate = mainForm.user.fullName;

            dbStudentInforInKorea.SaveRemarks(lblStudentID.Text, remark);
        }

        private void btnLoadFileInforKorea_Click(object sender, EventArgs e)
        {
            OpenFileDialog form = new OpenFileDialog();
            if (form.ShowDialog() == DialogResult.OK)
            {
                StudentDocument doc = new StudentDocument();
                doc.studentId = lblStudentID.Text;
                doc.fileName = form.SafeFileName;
                doc.documentType = 3; //Infor in Korea

                if (!form.CheckPathExists)
                {
                    MessageBox.Show("File don't exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FileStream fStream = File.OpenRead(form.FileName);
                byte[] contents = new byte[fStream.Length];
                fStream.Read(contents, 0, (int)fStream.Length);
                fStream.Close();

                StudentFile file = new StudentFile();
                file.fileContent = contents;

                int id = dbFile.UploadFile(doc, file);

                GridPanel panel = sgUploadFileInforKorea.PrimaryGrid;
                sgUploadFileInforKorea.BeginUpdate();

                object[] ob1 = new object[]
                        {
                    id, doc.fileName
                        };

                panel.Rows.Add(new GridRow(ob1));
                sgUploadFileInforKorea.EndUpdate();
            }
        }

        int idFileInforInKorea = 0;
        int idFileAdmission = 0;
        int idFileBasicInfor = 0;
        private string ob1;

        private void sgLoadFileBasicInfor_CellMouseUp(object sender, GridCellMouseEventArgs e)
        {
            contextFile.Enabled = true;
            int rowIndex = sgLoadFileBasicInfor.ActiveRow.RowIndex;
            GridCell gridCell = sgLoadFileBasicInfor.GetCell(rowIndex, 0);
            idFileBasicInfor = (int)gridCell.Value;
            idFileAdmission = 0;
            idFileInforInKorea = 0;
        }

        private void sgUploadFile_CellMouseUp(object sender, GridCellMouseEventArgs e)
        {
            contextFile.Enabled = true;
            int rowIndex = sgUploadFileAdmission.ActiveRow.RowIndex;
            GridCell gridCell = sgUploadFileAdmission.GetCell(rowIndex, 0);
            idFileAdmission = (int)gridCell.Value;
            idFileBasicInfor = 0;
            idFileInforInKorea = 0;
        }
        private void sgUploadFileInforKorea_CellMouseUp(object sender, GridCellMouseEventArgs e)
        {
            contextFile.Enabled = true;
            int rowIndex = sgUploadFileInforKorea.ActiveRow.RowIndex;
            GridCell gridCell = sgUploadFileInforKorea.GetCell(rowIndex, 0);
            idFileInforInKorea = (int)gridCell.Value;
            idFileAdmission = 0;
            idFileBasicInfor = 0;
        }

        private void sgUploadFileInforKorea_MouseDown(object sender, MouseEventArgs e)
        {
            contextFile.Enabled = false;
        }
        private void sgLoadFileBasicInfor_MouseDown(object sender, MouseEventArgs e)
        {
            contextFile.Enabled = false;
        }
        private void sgUploadFile_MouseDown(object sender, MouseEventArgs e)
        {
            contextFile.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormCancel dlg = new FormCancel();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                rtbRemarkAdmissionInfor.Text += "\n " + dlg.GetReason();
                StudentRemarksAdmission remark = new StudentRemarksAdmission(lblStudentID.Text);
                remark.remarks = rtbRemarkAdmissionInfor.Text;
                dbAdmissionInfor.SaveRemarks(lblStudentID.Text, remark);

                rtbRemarkBasicInfor.Text += "\n " + dlg.GetReason();
                var studentBasic = dbBasicInfor.GetInforDetailStudent(lblStudentID.Text);
                studentBasic.remarks += rtbRemarkBasicInfor.Text;
                studentBasic.isCancel = true;
                dbBasicInfor.EditInforStudent(lblStudentID.Text, studentBasic);

                var admissionInfor = dbAdmissionInfor.GetAdmissionInforDetail(lblStudentID.Text);
                cboVisa.Text = admissionInfor.visa = "No";
                dbAdmissionInfor.EditAdmissionInfor(lblStudentID.Text, admissionInfor);

                btnSaveAdmissionInfor.Enabled = false;
                btnCancel.Enabled = false;
                btnSaveBasicInfor.Enabled = false;
                stInvoice.Visible = false;

                var student = dbBasicInfor.GetInforDetailStudent(lblStudentID.Text);
                student.isCancel = true;
                dbBasicInfor.EditInforStudent(lblStudentID.Text, student);
            }
        }

        private void stInvoice_Click(object sender, EventArgs e)
        {
            lblInvoiceName.Text = lblFullName.Text;
            lblInvoiceDob.Text = lblBirthDay.Text;
            lblInvoiceAdmissionNo.Text = txtAddmissionNo.Text;
            lblInvoicePassportNo.Text = txtPassportNo.Text;
            lblInvoiceNational.Text = "Viet Nam";
            lblInvoiceDate.Text = DateTime.Now.ToString("yyyy.MM.dd");

            List<string> kobecPartner = new List<string>();
            foreach (var partner in mainForm.GetKobecPartner())
            {
                if (partner.idPartner == cboKobecPartner.Text)
                {
                    kobecPartner.Add(partner.namePartner);
                    break;
                }
            }
            kobecPartner.Add(txtFullName.Text);
            cboInvoiceKobecPartner.DataSource = kobecPartner;
            cboInvoiceKobecPartner.Text = lblKobecPartner.Text;

            Zen.Barcode.Code128BarcodeDraw brCode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            picBarcode.Image = brCode.Draw(lblStudentID.Text, 60);

            GridPanel panel = sgInvoiceBill.PrimaryGrid;
            panel.Rows.Clear();
            sgInvoiceBill.BeginUpdate();

            int amount = 0;

            object[] ob1 = new object[] { "Tuition fee", "4,850,000" };
            panel.Rows.Add(new GridRow(ob1));
            int num;
            if (int.TryParse("4,850,000", NumberStyles.AllowThousands,
                             CultureInfo.InvariantCulture, out num))
            {
                amount += num;
            }

            object[] ob2 = new object[] { "Dormitory (6 month)", "1,500,000" };
            panel.Rows.Add(new GridRow(ob2));
            if (int.TryParse("1,500,000", NumberStyles.AllowThousands,
                             CultureInfo.InvariantCulture, out num))
            {
                amount += num;
            }

            object[] ob3 = new object[] { "Inssurance (12 months)", "150,000" };
            panel.Rows.Add(new GridRow(ob3));
            if (int.TryParse("150,000", NumberStyles.AllowThousands,
                             CultureInfo.InvariantCulture, out num))
            {
                amount += num;
            }

            object[] ob4 = new object[] { "Alien Card Register", "50,000" };
            panel.Rows.Add(new GridRow(ob4));
            if (int.TryParse("50,000", NumberStyles.AllowThousands,
                             CultureInfo.InvariantCulture, out num))
            {
                amount += num;
            }

            object[] ob5 = new object[] { "Pick up fee", "50,000" };
            panel.Rows.Add(new GridRow(ob5));
            if (int.TryParse("50,000", NumberStyles.AllowThousands,
                             CultureInfo.InvariantCulture, out num))
            {
                amount += num;
            }

            object[] ob6 = new object[] { "Start support program fee", "NO" };
            panel.Rows.Add(new GridRow(ob6));

            lblInvoiceTotalAmount.Text = String.Format("{0:n0}", amount);
            dtInvoicePaymentPeriod.DateTime = DateTime.Now.AddDays(14);

            sgInvoiceBill.EndUpdate();
        }

        void UpdateParagrapInvoice(Paragraph pg, string des, Paragraph pg1, string amount)
        {
            var text = pg.AppendText(des);

            pg.Format.BeforeAutoSpacing = false;
            text.CharacterFormat.FontName = "Century Schoolbook";
            text.CharacterFormat.FontSize = 10;
            pg.Format.BeforeSpacing = 0;
            pg.Format.AfterAutoSpacing = false;
            pg.Format.AfterSpacing = 0;

            var text1 = pg1.AppendText(amount);
            text1.CharacterFormat.FontName = "Century Schoolbook";
            text1.CharacterFormat.FontSize = 10;
            pg1.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
            pg1.Format.BeforeAutoSpacing = false;
            pg1.Format.BeforeSpacing = 0;
            pg1.Format.AfterAutoSpacing = false;
            pg1.Format.AfterSpacing = 0;
        }

        Dictionary<string, string> DicAddMoreBills = new Dictionary<string, string>();

        private void addMoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddMoreDescription dlg = new AddMoreDescription();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string des = dlg.GetDes();
                string amount = dlg.GetAmount();

                GridPanel panel = sgInvoiceBill.PrimaryGrid;
                sgInvoiceBill.BeginUpdate();

                int iAmount = 0;
                if (int.TryParse(amount, out iAmount))
                {
                    string amountConvert = String.Format("{0:n0}", iAmount);

                    object[] ob1 = new object[] { des, amountConvert };
                    panel.Rows.Add(new GridRow(ob1));
                }
                else
                {
                    object[] ob1 = new object[] { des, amount };
                    panel.Rows.Add(new GridRow(ob1));
                }

                sgInvoiceBill.EndUpdate();

                int num;
                if (int.TryParse(amount, NumberStyles.AllowThousands,
                                 CultureInfo.InvariantCulture, out num))
                {
                    int totalAmount;
                    if (int.TryParse(lblInvoiceTotalAmount.Text, NumberStyles.AllowThousands,
                                     CultureInfo.InvariantCulture, out totalAmount))
                    {
                        lblInvoiceTotalAmount.Text = String.Format("{0:n0}", totalAmount + num);
                    }
                }

                DicAddMoreBills[des] = amount;
            }
        }

        private void sgInvoiceBill_CellValueChanged(object sender, GridCellValueChangedEventArgs e)
        {
            var oldValue = e.OldValue.ToString();
            var newValue = e.NewValue.ToString();

            int numNewValue = 0;
            var checkNewNumber = (int.TryParse(newValue, NumberStyles.AllowThousands,
                             CultureInfo.InvariantCulture, out numNewValue));

            int numOldValue = 0;
            var checkOldNumber = int.TryParse(oldValue, NumberStyles.AllowThousands,
                             CultureInfo.InvariantCulture, out numOldValue);

            int totalAmount;
            int.TryParse(lblInvoiceTotalAmount.Text, NumberStyles.AllowThousands,
                             CultureInfo.InvariantCulture, out totalAmount);

            if (checkNewNumber)
            {
                e.GridCell.Value = String.Format("{0:n0}", numNewValue);
            }
            else
            {
                e.GridCell.Value = newValue;
            }

            lblInvoiceTotalAmount.Text = String.Format("{0:n0}", totalAmount + numNewValue - numOldValue);
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            var document = GetDocumentInvoice();
            if (document == null)
                return;

            document.SaveToFile("tempDocInvoice.doc", FileFormat.Doc);

            System.Diagnostics.Process.Start(directory + "/tempDocInvoice.doc");
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            var document = GetDocumentInvoice();
            if (document == null)
                return;

            document.SaveToFile("tempDocInvoice.pdf", FileFormat.PDF);

            StudentDocument doc = new StudentDocument();
            doc.studentId = lblStudentID.Text;
            doc.fileName = "INVOICE_" + DateTime.Now.ToString("yyyy.MM.dd");
            doc.documentType = 2; //Admission Infor

            FileStream fStream = File.OpenRead(directory + "/tempDocInvoice.pdf");
            byte[] contents = new byte[fStream.Length];
            fStream.Read(contents, 0, (int)fStream.Length);
            fStream.Close();

            StudentFile file = new StudentFile();
            file.fileContent = contents;

            int id = dbFile.UploadFile(doc, file);

            GridPanel panel = sgInvoiceBill.PrimaryGrid;
            panel = sgUploadFileAdmission.PrimaryGrid;
            sgUploadFileAdmission.BeginUpdate();

            object[] ob1 = new object[]
                    {
                    id, doc.fileName
                    };

            panel.Rows.Add(new GridRow(ob1));
            sgUploadFileAdmission.EndUpdate();

            fStream.Close();

            System.Diagnostics.Process.Start(directory + "/tempDocInvoice.pdf");
        }

        Document GetDocumentInvoice()
        {
            try
            {
                Document document;
                if (Utilities.ConvertToUnSign2(cboInvoiceKobecPartner.Text).ToUpper() == "HANOI COLLEGE OF TECHNOLOGY AND TRADE")
                    document = new Document(directory + "/FormInvoice_2.doc");
                else
                    document = new Document(directory + "/FormInvoice.doc");

                TextSelection selection = document.FindString("Barcode", true, true);
                DocPicture pic = new DocPicture(document);
                pic.LoadImage(picBarcode.Image);
                pic.Width = 130f;
                pic.Height = 30f;
                TextRange range = selection.GetAsOneRange();
                int index = range.OwnerParagraph.ChildObjects.IndexOf(range);
                range.OwnerParagraph.ChildObjects.Insert(index, pic);
                range.OwnerParagraph.ChildObjects.Remove(range);

                document.FindString("NowDate", true, true)[0] = lblInvoiceDate.Text;
                document.FindString("PaymentPeriod", true, true)[0] = dtInvoicePaymentPeriod.DateTime.ToString("yyyy.MM.dd");
                document.FindString("KobecPartner", true, true)[0] = Utilities.ConvertToUnSign2(cboInvoiceKobecPartner.Text).ToUpper();
                document.FindString("FullNameEnglish", true, true)[0] = Utilities.ConvertToUnSign2(lblInvoiceName.Text).ToUpper();
                document.FindString("xxx", true, true)[0] = lblInvoicePassportNo.Text;
                document.FindString("DOB", true, true)[0] = lblInvoiceDob.Text;
                document.FindString("INUAdmission", true, true)[0] = lblInvoiceAdmissionNo.Text;

                GridPanel panel = sgInvoiceBill.PrimaryGrid;
                var IRows = panel.Rows.GetEnumerator();
                while (IRows.MoveNext())
                {
                    GridRow r = (GridRow)IRows.Current;
                    if (r[0].Value.ToString() == "Tuition fee")
                    {
                        document.FindString("TuitionFee", true, true)[0] = r[1].Value.ToString();
                    }
                    else if (r[0].Value.ToString() == "Dormitory (6 month)")
                    {
                        document.FindString("yyy", true, true)[0] = r[1].Value.ToString();
                    }
                    else if (r[0].Value.ToString() == "Inssurance (12 months)")
                    {
                        document.FindString("iii", true, true)[0] = r[1].Value.ToString();
                    }
                    else if (r[0].Value.ToString() == "Alien Card Register")
                    {
                        document.FindString("AlienCard", true, true)[0] = r[1].Value.ToString();
                    }
                    else if (r[0].Value.ToString() == "Pick up fee")
                    {
                        document.FindString("PickUpFee", true, true)[0] = r[1].Value.ToString();
                    }
                    else if (r[0].Value.ToString() == "Start support program fee")
                    {
                        document.FindString("StartSupportProgramFee", true, true)[0] = r[1].Value.ToString();
                    }
                    else
                    {
                        Section section = document.Sections[0];
                        Table table = section.Tables[3] as Table;

                        var cell = table.Rows[1].Cells[0];
                        var cell1 = table.Rows[1].Cells[1];

                        foreach (var x in DicAddMoreBills)
                        {
                            if (r[0].Value.ToString() == x.Key)
                            {
                                var pg = cell.AddParagraph();
                                var pg1 = cell1.AddParagraph();

                                UpdateParagrapInvoice(pg, r[0].Value.ToString(), pg1, r[1].Value.ToString());
                            }
                        }
                    }
                }

                document.FindString("TotalAmount", true, true)[0] = lblInvoiceTotalAmount.Text;

                return document;
            }
            catch
            {
                MessageBox.Show("You need close file pdf before save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


    }
}
