using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien.Forms
{
    public partial class FormViewPdf : Form
    {
        public FormViewPdf(string pathFile)
        {
            InitializeComponent();

            axAcroPDF1.src = pathFile;
        }
    }
}
