namespace QuanLySinhVien.Forms.ExcellExport
{
    partial class ExportForUniversity
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn11 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn12 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn13 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn14 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn15 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn16 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtTitle = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnFilter = new DevComponents.DotNetBar.ButtonX();
            this.cboUniversity = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.cboSesson = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboYear = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.sgUniversity = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(1020, 649);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(141, 35);
            this.btnExport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExport.TabIndex = 174;
            this.btnExport.Text = "Export to Excel";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.Color.Black;
            this.labelX2.Location = new System.Drawing.Point(27, 20);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(86, 23);
            this.labelX2.TabIndex = 178;
            this.labelX2.Text = "Title name";
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtTitle.Border.Class = "TextBoxBorder";
            this.txtTitle.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.ForeColor = System.Drawing.Color.Black;
            this.txtTitle.Location = new System.Drawing.Point(119, 20);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(801, 22);
            this.txtTitle.TabIndex = 179;
            // 
            // btnFilter
            // 
            this.btnFilter.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFilter.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnFilter.Location = new System.Drawing.Point(836, 57);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(84, 23);
            this.btnFilter.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnFilter.TabIndex = 187;
            this.btnFilter.Text = "Filter";
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // cboUniversity
            // 
            this.cboUniversity.DisplayMember = "universityName";
            this.cboUniversity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboUniversity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboUniversity.FormattingEnabled = true;
            this.cboUniversity.ItemHeight = 16;
            this.cboUniversity.Location = new System.Drawing.Point(578, 57);
            this.cboUniversity.Name = "cboUniversity";
            this.cboUniversity.Size = new System.Drawing.Size(239, 22);
            this.cboUniversity.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboUniversity.TabIndex = 189;
            this.cboUniversity.ValueMember = "id";
            // 
            // labelX12
            // 
            this.labelX12.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX12.ForeColor = System.Drawing.Color.Black;
            this.labelX12.Location = new System.Drawing.Point(496, 57);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(67, 23);
            this.labelX12.TabIndex = 188;
            this.labelX12.Text = "University";
            // 
            // cboSesson
            // 
            this.cboSesson.DisplayMember = "universityName";
            this.cboSesson.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSesson.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSesson.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSesson.FormattingEnabled = true;
            this.cboSesson.ItemHeight = 16;
            this.cboSesson.Location = new System.Drawing.Point(230, 57);
            this.cboSesson.Name = "cboSesson";
            this.cboSesson.Size = new System.Drawing.Size(138, 22);
            this.cboSesson.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboSesson.TabIndex = 194;
            this.cboSesson.ValueMember = "id";
            // 
            // cboYear
            // 
            this.cboYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboYear.FormattingEnabled = true;
            this.cboYear.ItemHeight = 16;
            this.cboYear.Location = new System.Drawing.Point(119, 57);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(105, 22);
            this.cboYear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboYear.TabIndex = 193;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.Color.Black;
            this.labelX1.Location = new System.Drawing.Point(27, 57);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(86, 23);
            this.labelX1.TabIndex = 192;
            this.labelX1.Text = "Sesmester";
            // 
            // sgUniversity
            // 
            this.sgUniversity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sgUniversity.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.sgUniversity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.sgUniversity.Location = new System.Drawing.Point(3, 99);
            this.sgUniversity.Name = "sgUniversity";
            gridColumn1.AllowEdit = false;
            gridColumn1.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.DisplayedCells;
            gridColumn1.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn1.Name = "No";
            gridColumn1.ReadOnly = true;
            gridColumn2.AllowEdit = false;
            gridColumn2.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.DisplayedCells;
            gridColumn2.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn2.Name = "English Name";
            gridColumn2.ReadOnly = true;
            gridColumn3.AllowEdit = false;
            gridColumn3.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.DisplayedCells;
            gridColumn3.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn3.Name = "Gender";
            gridColumn3.ReadOnly = true;
            gridColumn4.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.DisplayedCells;
            gridColumn4.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn4.Name = "Birthdate";
            gridColumn5.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.DisplayedCells;
            gridColumn5.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn5.Name = "Passport No";
            gridColumn6.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.DisplayedCells;
            gridColumn6.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn6.Name = "Passport";
            gridColumn7.AllowEdit = false;
            gridColumn7.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.DisplayedCells;
            gridColumn7.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn7.Name = "Passport Expire Date";
            gridColumn7.ReadOnly = true;
            gridColumn8.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.DisplayedCells;
            gridColumn8.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn8.Name = "Home Address";
            gridColumn9.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.DisplayedCells;
            gridColumn9.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn9.Name = "Cell Phone No";
            gridColumn10.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.DisplayedCells;
            gridColumn10.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn10.Name = "School Name";
            gridColumn11.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.DisplayedCells;
            gridColumn11.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn11.Name = "School Address";
            gridColumn12.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.DisplayedCells;
            gridColumn12.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn12.Name = "GPA";
            gridColumn13.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.DisplayedCells;
            gridColumn13.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn13.Name = "Graduation Date";
            gridColumn14.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.DisplayedCells;
            gridColumn14.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn14.Name = "Bank Balance";
            gridColumn15.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.DisplayedCells;
            gridColumn15.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn15.Name = "Bank Account Owner";
            gridColumn16.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.DisplayedCells;
            gridColumn16.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn16.Name = "Remarks";
            this.sgUniversity.PrimaryGrid.Columns.Add(gridColumn1);
            this.sgUniversity.PrimaryGrid.Columns.Add(gridColumn2);
            this.sgUniversity.PrimaryGrid.Columns.Add(gridColumn3);
            this.sgUniversity.PrimaryGrid.Columns.Add(gridColumn4);
            this.sgUniversity.PrimaryGrid.Columns.Add(gridColumn5);
            this.sgUniversity.PrimaryGrid.Columns.Add(gridColumn6);
            this.sgUniversity.PrimaryGrid.Columns.Add(gridColumn7);
            this.sgUniversity.PrimaryGrid.Columns.Add(gridColumn8);
            this.sgUniversity.PrimaryGrid.Columns.Add(gridColumn9);
            this.sgUniversity.PrimaryGrid.Columns.Add(gridColumn10);
            this.sgUniversity.PrimaryGrid.Columns.Add(gridColumn11);
            this.sgUniversity.PrimaryGrid.Columns.Add(gridColumn12);
            this.sgUniversity.PrimaryGrid.Columns.Add(gridColumn13);
            this.sgUniversity.PrimaryGrid.Columns.Add(gridColumn14);
            this.sgUniversity.PrimaryGrid.Columns.Add(gridColumn15);
            this.sgUniversity.PrimaryGrid.Columns.Add(gridColumn16);
            this.sgUniversity.Size = new System.Drawing.Size(1178, 544);
            this.sgUniversity.TabIndex = 2;
            this.sgUniversity.Text = "superGridControl1";
            // 
            // ExportForUniversity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sgUniversity);
            this.Controls.Add(this.cboSesson);
            this.Controls.Add(this.cboYear);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.cboUniversity);
            this.Controls.Add(this.labelX12);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.btnExport);
            this.Name = "ExportForUniversity";
            this.Size = new System.Drawing.Size(1184, 690);
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTitle;
        private DevComponents.DotNetBar.ButtonX btnFilter;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboUniversity;
        private DevComponents.DotNetBar.LabelX labelX12;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSesson;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboYear;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl sgUniversity;
    }
}
