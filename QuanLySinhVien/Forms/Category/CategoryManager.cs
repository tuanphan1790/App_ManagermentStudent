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
using DevComponents.DotNetBar.SuperGrid;
using Model;

namespace QuanLySinhVien.Forms.Category
{
    public partial class CategoryManager : UserControl
    {
        CategoryManagerDB db;

        public CategoryManager(CategoryManagerDB db)
        {
            this.db = db;

            InitializeComponent();

            LoadData();
        }

        private void LoadData()
        {
            var listLevel = db.GetAllLevel();
            foreach (var x in listLevel)
            {
                GridPanel panel = sgLevelCategory.PrimaryGrid;
                sgLevelCategory.BeginUpdate();
                object[] ob1 = new object[]
                        {
                            x.id, x.levelName
                        };

                panel.Rows.Add(new GridRow(ob1));
                sgLevelCategory.EndUpdate();
            }

            var listUniversity = db.GetAllUniversity();
            foreach (var x in listUniversity)
            {
                GridPanel panel = sgUniversity.PrimaryGrid;
                sgUniversity.BeginUpdate();
                object[] ob1 = new object[]
                        {
                            x.id, x.universityName, x.number
                        };

                panel.Rows.Add(new GridRow(ob1));
                sgUniversity.EndUpdate();
            }

            var listPartner = db.GetAllPartner();
            foreach (var x in listPartner)
            {
                GridPanel panel = sgKobecPartner.PrimaryGrid;
                sgKobecPartner.BeginUpdate();
                object[] ob1 = new object[]
                        {
                            x.id, x.namePartner, x.idPartner, x.number
                        };

                panel.Rows.Add(new GridRow(ob1));
                sgKobecPartner.EndUpdate();
            }

        }

        int idLevel = 0;
        private void newLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LevelForm form = new LevelForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                string levelName = form.GetLevelName();
                var id = db.AddNewLevel(levelName);

                GridPanel panel = sgLevelCategory.PrimaryGrid;
                sgLevelCategory.BeginUpdate();

                object[] ob1 = new object[]
                        {
                    id, levelName
                        };

                panel.Rows.Add(new GridRow(ob1));
                sgLevelCategory.EndUpdate();
            }
        }

        private void deleteLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (idLevel == 0)
            {
                return;
            }
            else
            {
                db.DeleteLevel(idLevel);

                GridPanel panel = sgLevelCategory.PrimaryGrid;
                var IRows = panel.Rows.GetEnumerator();
                GridRow _r = null;

                while (IRows.MoveNext())
                {
                    GridRow r = (GridRow)IRows.Current;
                    if ((int)r[0].Value == idLevel)
                    {
                        _r = r;
                    }
                }
                panel.Rows.Remove(_r);

                idLevel = 0;
            }
        }

        int idUniversity = 0;
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UniversityForm form = new UniversityForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                UniversityInfor university = new UniversityInfor();
                university.universityName = form.GetUniversityName();
                university.number = db.GetMaxUniversity() + 1;
                var id = db.AddNewUniversity(university);

                GridPanel panel = sgUniversity.PrimaryGrid;
                sgUniversity.BeginUpdate();

                object[] ob1 = new object[]
                        {
                    id, university.universityName, university.number
                        };

                panel.Rows.Add(new GridRow(ob1));
                sgUniversity.EndUpdate();
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (idUniversity == 0)
            {
                return;
            }
            else
            {
                db.DeleteUniversity(idUniversity);

                GridPanel panel = sgUniversity.PrimaryGrid;
                var IRows = panel.Rows.GetEnumerator();
                GridRow _r = null;

                while (IRows.MoveNext())
                {
                    GridRow r = (GridRow)IRows.Current;
                    if ((int)r[0].Value == idUniversity)
                    {
                        _r = r;
                    }
                }
                panel.Rows.Remove(_r);

                idUniversity = 0;
            }
        }
        private void superGridControl1_CellMouseUp(object sender, GridCellMouseEventArgs e)
        {
            SetContextWhenCell(contextUniversity);

            int rowIndex = sgUniversity.ActiveRow.RowIndex;
            GridCell gridCell = sgUniversity.GetCell(rowIndex, 0);
            idUniversity = (int)gridCell.Value;
        }

        private void superGridControl1_MouseDown(object sender, MouseEventArgs e)
        {
            SetContextNoCell(contextUniversity);
        }

        int idPartner = 0;
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            PartnerForm form = new PartnerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                PartnerInfor partner = new PartnerInfor();
                partner.idPartner = form.GetIDPartner();
                partner.namePartner = form.GetNamePartner();
                partner.number = db.GetMaxPartner() + 1;
                var id = db.AddNewPartner(partner);

                GridPanel panel = sgKobecPartner.PrimaryGrid;
                sgKobecPartner.BeginUpdate();

                object[] ob1 = new object[]
                        {
                    id, partner.namePartner, partner.idPartner, partner.number
                        };

                panel.Rows.Add(new GridRow(ob1));
                sgKobecPartner.EndUpdate();


            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (idPartner == 0)
            {
                return;
            }
            else
            {
                db.DeletePartner(idPartner);

                GridPanel panel = sgKobecPartner.PrimaryGrid;
                var IRows = panel.Rows.GetEnumerator();
                GridRow _r = null;

                while (IRows.MoveNext())
                {
                    GridRow r = (GridRow)IRows.Current;
                    if ((int)r[0].Value == idPartner)
                    {
                        _r = r;
                    }
                }
                panel.Rows.Remove(_r);

                idPartner = 0;
            }
        }

        private void superGridControl2_CellMouseUp(object sender, GridCellMouseEventArgs e)
        {
            SetContextWhenCell(contextKobecPartner);
            int rowIndex = sgKobecPartner.ActiveRow.RowIndex;
            GridCell gridCell = sgKobecPartner.GetCell(rowIndex, 0);
            idPartner = (int)gridCell.Value;
        }

        private void superGridControl2_MouseDown(object sender, MouseEventArgs e)
        {
            SetContextNoCell(contextKobecPartner);
        }

        private void SetContextWhenCell(ContextMenuStrip context)
        {
            context.Items[0].Enabled = false;
            context.Items[1].Enabled = true;
        }

        public void SetContextNoCell(ContextMenuStrip context)
        {
            context.Items[0].Enabled = true;
            context.Items[1].Enabled = false;
        }

        private void sgLevelCategory_CellMouseUp(object sender, GridCellMouseEventArgs e)
        {
            SetContextWhenCell(contextLevel);

            int rowIndex = sgLevelCategory.ActiveRow.RowIndex;
            GridCell gridCell = sgLevelCategory.GetCell(rowIndex, 0);
            idLevel = (int)gridCell.Value;
        }

        private void sgLevelCategory_MouseDown(object sender, MouseEventArgs e)
        {
            SetContextNoCell(contextLevel);
        }

        private void sgKobecPartner_CellValidating(object sender, GridCellValidatingEventArgs e)
        {
            GridCell gridCell = sgKobecPartner.GetCell(e.GridCell.RowIndex, 0);
            idPartner = (int)gridCell.Value;

            if (idPartner == 0)
                return;

            PartnerInfor oldRecord = db.GetPartnerDetail(idPartner);
            if (oldRecord != null)
            {
                e.GridCell.Value = e.Value;

                GridPanel panel = sgKobecPartner.PrimaryGrid;
                var IRows = panel.Rows.GetEnumerator();
                while (IRows.MoveNext())
                {
                    GridRow r = (GridRow)IRows.Current;
                    if (Convert.ToInt32(r[3].Value) >= Convert.ToInt32(e.Value) && Convert.ToInt32(r[0].Value) != idPartner)
                    {
                        r[3].Value = Convert.ToInt32(r[3].Value) + 1;
                    }

                    PartnerInfor record = new PartnerInfor();
                    record.number = Convert.ToInt32(r[3].Value);
                    db.EditOrderPartner(Convert.ToInt32(r[0].Value), record);
                }
            }

            idPartner = 0;
        }

        private void sgUniversity_CellValidating(object sender, GridCellValidatingEventArgs e)
        {
            GridCell gridCell = sgUniversity.GetCell(e.GridCell.RowIndex, 0);
            idUniversity = (int)gridCell.Value;

            if (idUniversity == 0)
                return;

            UniversityInfor oldRecord = db.GetUniversityDetail(idUniversity);
            if (oldRecord != null)
            {
                e.GridCell.Value = e.Value;

                GridPanel panel = sgUniversity.PrimaryGrid;
                var IRows = panel.Rows.GetEnumerator();
                while (IRows.MoveNext())
                {
                    GridRow r = (GridRow)IRows.Current;
                    if (Convert.ToInt32(r[2].Value) >= Convert.ToInt32(e.Value) && Convert.ToInt32(r[0].Value) != idUniversity)
                    {
                        r[2].Value = Convert.ToInt32(r[2].Value) + 1;
                    }

                    UniversityInfor record = new UniversityInfor();
                    record.number = Convert.ToInt32(r[2].Value);
                    db.EditOrderUniversity(idUniversity, record);
                }
            }

            idUniversity = 0;
        }
    }
}
