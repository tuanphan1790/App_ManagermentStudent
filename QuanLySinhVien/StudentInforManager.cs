using DBManager;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien
{
    class StudentInforManager
    {
        public StudentInforManager(StudentBasicInfor student, string connStr)
        {
            studentId = student.studentId;
            FullName = student.fullName;

            var dbStudentInforInKorea = new StudentInforInKoreaDB(connStr);
            var recordStudentInforInKoreas = dbStudentInforInKorea.GetStudentInforInKoreas(studentId);
            if (recordStudentInforInKoreas.Count != 0)
            {
                var studentInforInKoreaLast = recordStudentInforInKoreas.Last();
                AlienCardId = studentInforInKoreaLast.NoIdentifierKorea;
            }
            else
                AlienCardId = "NA";

            Phone = student.phone;
            Passport = student.passportNumber;
            Address = student.homeAddress;
            University = student.applyForUniversity;
            KobecPartner = student.kobecPartner;
            ApplySesmester = student.applySesmesterYear + " - " + student.applySesmesterSesson;
        }

        public string studentId;
        public string FullName { set; get; }
        public string AlienCardId { set; get; }
        public string Phone { set; get; }
        public string Passport { set; get; }
        public string Address { set; get; }
        public string University { set; get; }
        public string KobecPartner { set; get; }
        public string ApplySesmester { set; get; }
    }
}
