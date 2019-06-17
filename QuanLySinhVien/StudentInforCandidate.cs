using DBManager;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DBManager.StudentInforBasicDB;

namespace QuanLySinhVien
{
    public class StudentInforCandidate
    {
        AdmissionInforDB dbAdd;

        public StudentInforCandidate(StudentBasicInfor student, string connStr)
        {
            dbAdd = new AdmissionInforDB(connStr);

            studentId = student.studentId;

            FullName = student.fullName;

            if (student.cvReview == 0)
            {
                KobecResult = "Waiting CV Review";
            }

            if (student.cvReview == 1)
            {
                KobecResult = "Rejected";
            }

            if (student.interview == 1)
            {
                KobecResult = "Rejected";
            }

            if (student.cvReview == 2 && student.interview == 0)
            {
                KobecResult = "WaitingInterview";
            }

            if (student.cvReview == 1 && student.interview == 1)
            {
                KobecResult = "Rejected";
            }

            if (student.cvReview == 2 && student.interview == 2)
            {
                KobecResult = "Accepted";
            }

            if (student.addmissionResult == 0)
            {
                AdmissionResult = "Waiting";
            }

            if (student.addmissionResult == 1)
            {
                AdmissionResult = "Rejected";
            }

            if (student.addmissionResult == 2)
            {
                AdmissionResult = "Accepted";
            }

            DOB = student.dateOfBirth.ToString("yyyy-MM-dd");

            University = student.applyForUniversity;

            InvoiceDate = "NA";
            VisaCodeDate = "NA";
            PaymentDate = "NA";

            var studentInforBeforEntry = dbAdd.GetAdmissionInforDetail(student.studentId);
            if (studentInforBeforEntry != null)
            {
                if (studentInforBeforEntry.invoiceDate.Ticks != 0)
                    InvoiceDate = studentInforBeforEntry.invoiceDate.ToString("yyyy-MM-dd");

                if (studentInforBeforEntry.visaCodeDate.Ticks != 0)
                    VisaCodeDate = studentInforBeforEntry.visaCodeDate.ToString("yyyy-MM-dd");

                if (studentInforBeforEntry.tuitionPayDate.Ticks != 0)
                    PaymentDate = studentInforBeforEntry.tuitionPayDate.ToString("yyyy-MM-dd");
            }

            Passport = student.passportNumber;
            ApplySesmester = student.applySesmesterYear + " - " + student.applySesmesterSesson;
            KobecPartner = student.kobecPartner;

            Duplicate = student.duplicate;

            if (student.orderKobecAccept == 0)
            {
                No = "";
            }
            else
            {
                No = student.orderKobecAccept.ToString();
            }

            Cancel = student.isCancel;
        }

        public string No { set; get; }
        public string studentId;
        public string FullName { set; get; }
        public string DOB { set; get; }
        public string Passport { set; get; }
        public string University { set; get; }
        public string InvoiceDate { set; get; }
        public string VisaCodeDate { set; get; }
        public string PaymentDate { set; get; }
        public string KobecPartner { set; get; }
        public string ApplySesmester { set; get; }
        public string KobecResult { set; get; }
        public string AdmissionResult { set; get; }

        public bool Duplicate { set; get; }
        public bool Cancel { set; get; }
    }
}
