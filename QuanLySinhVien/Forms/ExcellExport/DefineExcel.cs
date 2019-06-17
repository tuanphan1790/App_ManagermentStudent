using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DBManager;
using Model;
using System.Data.SqlClient;

namespace QuanLySinhVien.Forms.ExcellExport
{
    public partial class DefineExcel : UserControl
    {
        List<string> listStudentId = new List<string>();
        bool init = false;

        public DefineExcel(FormMain form)
        {
            InitializeComponent();

            connectionString = form.connString;

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

            init = true;

            Initialized();
        }

        void Initialized()
        {
            var nodeFullName = treeView1.Nodes.Find("fullname", true);
            nodeFullName[0].Checked = true;

            var nodeDOB = treeView1.Nodes.Find("date_of_birth", true);
            nodeDOB[0].Checked = true;

            var nodePassport = treeView1.Nodes.Find("passport_no", true);
            nodePassport[0].Checked = true;
        }

        void ResreshSesmester(List<string> nodeTemps)
        {
            foreach (var node in nodeTemps)
            {
                var treenode = treeView1.Nodes.Find(node, true);
                treenode[0].Checked = true;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = CreateTable();
        }

        private DataTable CreateTable()
        {
            DataTable tbl = new DataTable();
            foreach (var col in dicTreeNodes)
            {
                var node = (TreeNode)col.Key;
                tbl.Columns.Add(node.Text, typeof(string));
            }

            object[] ob1 = new object[dicTreeNodes.Count];

            for (var j = 0; j < countData; j++)
            {
                int i = 0;
                foreach (var col in dicTreeNodes)
                {
                    ob1[i] = col.Value[j];
                    i++;
                }
                tbl.Rows.Add(ob1);
            }

            return tbl;
        }

        List<GridColumn> listColumns = new List<GridColumn>();
        int countData = 0;
        Dictionary<TreeNode, List<string>> dicTreeNodes = new Dictionary<TreeNode, List<string>>();

        List<string> ConvertGender(List<string> genders)
        {
            List<string> ret = new List<string>();

            foreach (var gender in genders)
            {
                if (gender == "True")
                    ret.Add("M");
                else
                    ret.Add("F");
            }

            return ret;
        }

        List<string> ConvertCVReview(List<string> cvReviews)
        {
            List<string> ret = new List<string>();

            foreach (var cv in cvReviews)
            {
                if (cv == "0")
                    ret.Add("NA");
                else if (cv == "1")
                    ret.Add("Rejected");
                else if (cv == "2")
                    ret.Add("Accepted");
            }

            return ret;
        }

        List<string> ConvertInterview(List<string> interviews)
        {
            List<string> ret = new List<string>();

            foreach (var interview in interviews)
            {
                if (interview == "0")
                    ret.Add("NA");
                else if (interview == "1")
                    ret.Add("Rejected");
                else if (interview == "2")
                    ret.Add("Accepted");
            }

            return ret;
        }

        List<string> ConvertDateTime(List<string> dateTimes)
        {
            List<string> ret = new List<string>();

            foreach (var datetime in dateTimes)
            {
                var dt = Convert.ToDateTime(datetime);
                if (dt.Ticks == 0)
                    ret.Add("");
                else
                    ret.Add(dt.ToString("yyyy/MM/dd"));
            }

            return ret;
        }

        string ConvertDateTime(string dateTime)
        {
            if (dateTime == "")
                return "";

            var dt = Convert.ToDateTime(dateTime);
            if (dt.Ticks == 0)
                return "";

            return dt.ToString("yyyy/MM/dd");
        }

        string connectionString;
        private List<string> ReadOrderData(string input, string table)
        {
            List<string> ret = new List<string>();

            string queryString =
                "SELECT " + input + " FROM dbo." + table + " WHERE apply_year = " + cboYear.Text + " AND " + "apply_sesson = '" + cboSesson.Text + "';";
            using (SqlConnection connection = new SqlConnection(
                       connectionString))
            {
                SqlCommand command = new SqlCommand(
                    queryString, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ret.Add(reader[0].ToString());
                    }
                }
            }

            return ret;
        }

        private string ReadData(string input, string studentId, string table)
        {
            string queryString =
                "SELECT " + input + " FROM dbo." + table + " WHERE " + "student_id= '" + studentId + "';";
            using (SqlConnection connection = new SqlConnection(
                       connectionString))
            {
                SqlCommand command = new SqlCommand(
                    queryString, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return reader[0].ToString();
                    }
                }
            }

            return "";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            var exepath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(exepath);

            gridView2.ExportToXlsx(directory + "/tempExportExcellForDefine.xlsx");

            System.Diagnostics.Process.Start(directory + "/tempExportExcellForDefine.xlsx");
        }

        private void ReadStudentIdData()
        {
            listStudentId.Clear();

            string queryString =
                "SELECT student_id FROM dbo.student_basic_infor" + " WHERE apply_year = " + cboYear.Text + " AND " + "apply_sesson = '" + cboSesson.Text + "';";
            using (SqlConnection connection = new SqlConnection(
                       connectionString))
            {
                SqlCommand command = new SqlCommand(
                    queryString, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listStudentId.Add(reader[0].ToString());
                    }
                }
            }
        }

        private void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (init)
            {
                List<string> nodeTemps = new List<string>();
                foreach (var node in dicTreeNodes)
                {
                    nodeTemps.Add(node.Key.Name);
                }

                dicTreeNodes.Clear();
                UncheckAllNodes(treeView1.Nodes);
                gridView2.Columns.Clear();
                gridControl1.DataSource = null;
                ReadStudentIdData();

                ResreshSesmester(nodeTemps);
            }
        }

        private void cboSesson_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (init)
            {
                List<string> nodeTemps = new List<string>();
                foreach (var node in dicTreeNodes)
                {
                    nodeTemps.Add(node.Key.Name);
                }

                dicTreeNodes.Clear();
                UncheckAllNodes(treeView1.Nodes);
                gridView2.Columns.Clear();
                gridControl1.DataSource = null;
                ReadStudentIdData();

                ResreshSesmester(nodeTemps);
            }
        }

        void UncheckAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = false;
                CheckChildren(node, false);
            }
        }

        void CheckChildren(TreeNode rootNode, bool isChecked)
        {
            foreach (TreeNode node in rootNode.Nodes)
            {
                CheckChildren(node, isChecked);
                node.Checked = isChecked;
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null)
                return;

            ReadStudentIdData();

            if (e.Node.Checked)
            {
                List<string> ret = new List<string>();
                var sParent = e.Node.Parent.Text;
                if (sParent == "Basic Information")
                {
                    ret = ReadOrderData(e.Node.Name, "student_basic_infor");
                    countData = ret.Count;

                    string nodeText = e.Node.Text;
                    if (nodeText == "Gender")
                    {
                        ret = ConvertGender(ret);
                    }

                    else if (nodeText == "CV Review")
                    {
                        ret = ConvertCVReview(ret);
                    }

                    else if (nodeText == "Interview")
                    {
                        ret = ConvertInterview(ret);
                    }

                    else if (nodeText == "DOB" | nodeText == "Passport Issue" | nodeText == "Passport Expiry" | nodeText == "CV Record Date" |
                        nodeText == "Full CV Recived" | nodeText == "Submission Date")
                    {
                        ret = ConvertDateTime(ret);
                    }
                }
                else if (sParent == "Admission Information")
                {
                    foreach (var studentId in listStudentId)
                    {
                        var infor = ReadData(e.Node.Name, studentId, "student_admission_infor");
                        string nodeText = e.Node.Text;
                        if (nodeText == "Invoice Date" | nodeText == "Visacode Date" | nodeText == "Korea Entry Plan" | nodeText == "Entrance Date")
                            ret.Add(ConvertDateTime(infor));
                        else
                            ret.Add(infor);
                    }
                }
                else if (sParent == "Information In Korea")
                {
                    foreach (var studentId in listStudentId)
                    {
                        ret.Add(ReadData(e.Node.Name, studentId, "student_infor_in_korea"));
                    }
                }
                else if (sParent == "Student Study Languge")
                {
                    foreach (var studentId in listStudentId)
                    {
                        ret.Add(ReadData(e.Node.Name, studentId, "student_study_languge"));
                    }
                }
                else if (sParent == "Student Study Specialized")
                {
                    foreach (var studentId in listStudentId)
                    {
                        ret.Add(ReadData(e.Node.Name, studentId, "student_study_specialized"));
                    }
                }

                dicTreeNodes[e.Node] = ret;

                var col = gridView2.Columns.AddVisible(e.Node.Text);
                e.Node.Tag = col;
                col.Tag = e.Node;
                listColumns.Add(col);
            }
            else
            {
                dicTreeNodes.Remove(e.Node);

                var col = (GridColumn)e.Node.Tag;
                if (col != null)
                    gridView2.Columns.Remove(col);

                foreach (var x in listColumns)
                {
                    if (x == col)
                    {
                        listColumns.Remove(x);
                        break;
                    }
                }
            }
        }
    }
}

