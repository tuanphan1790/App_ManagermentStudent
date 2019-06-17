namespace QuanLySinhVien.Forms
{
    partial class StudentListCandidate
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
            this.components = new System.ComponentModel.Container();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAddNewStudent = new DevComponents.DotNetBar.ButtonX();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridListStudents = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.studentbasicinforBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barButtonItem_View = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.cboSesson = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboYear = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.colid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfullname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldate_of_birth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colphone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colpassport_no = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colapply_semester = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colkobec_partner = new DevExpress.XtraGrid.Columns.GridColumn();
            this.interview = new DevExpress.XtraGrid.Columns.GridColumn();
            this.University = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.addmissionResult = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblNumberStudent = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridListStudents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentbasicinforBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtKeyword
            // 
            this.txtKeyword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtKeyword.Location = new System.Drawing.Point(80, 17);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(249, 23);
            this.txtKeyword.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(12, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Keyword";
            // 
            // btnAddNewStudent
            // 
            this.btnAddNewStudent.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddNewStudent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewStudent.BackColor = System.Drawing.Color.Lime;
            this.btnAddNewStudent.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddNewStudent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnAddNewStudent.Location = new System.Drawing.Point(808, 12);
            this.btnAddNewStudent.Name = "btnAddNewStudent";
            this.btnAddNewStudent.Size = new System.Drawing.Size(222, 43);
            this.btnAddNewStudent.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddNewStudent.Symbol = "";
            this.btnAddNewStudent.TabIndex = 9;
            this.btnAddNewStudent.Text = "ADD NEW CANDIDATE";
            this.btnAddNewStudent.Click += new System.EventHandler(this.btnAddNewStudent_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSearch.Location = new System.Drawing.Point(339, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(99, 23);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.Symbol = "";
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "SEARCH";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.gridListStudents);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox2.Location = new System.Drawing.Point(3, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1037, 450);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // gridListStudents
            // 
            this.gridListStudents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridListStudents.Location = new System.Drawing.Point(3, 19);
            this.gridListStudents.MainView = this.gridView1;
            this.gridListStudents.Name = "gridListStudents";
            this.gridListStudents.Size = new System.Drawing.Size(1031, 428);
            this.gridListStudents.TabIndex = 0;
            this.gridListStudents.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10F);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.GridControl = this.gridListStudents;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            this.gridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseDown);
            this.gridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseUp);
            // 
            // studentbasicinforBindingSource
            // 
            this.studentbasicinforBindingSource.DataMember = "student_basic_infor";
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_View)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // barButtonItem_View
            // 
            this.barButtonItem_View.Caption = "View Detail";
            this.barButtonItem_View.Id = 0;
            this.barButtonItem_View.Name = "barButtonItem_View";
            this.barButtonItem_View.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_View_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem_View});
            this.barManager1.MaxItemId = 1;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1040, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 535);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1040, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 535);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1040, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 535);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.buttonX1.Location = new System.Drawing.Point(339, 51);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(99, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.Symbol = "";
            this.buttonX1.TabIndex = 20;
            this.buttonX1.Text = "SEARCH";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // cboSesson
            // 
            this.cboSesson.DisplayMember = "id";
            this.cboSesson.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSesson.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cboSesson.FormattingEnabled = true;
            this.cboSesson.ItemHeight = 17;
            this.cboSesson.Location = new System.Drawing.Point(191, 51);
            this.cboSesson.Name = "cboSesson";
            this.cboSesson.Size = new System.Drawing.Size(138, 23);
            this.cboSesson.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboSesson.TabIndex = 188;
            this.cboSesson.ValueMember = "id";
            // 
            // cboYear
            // 
            this.cboYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cboYear.FormattingEnabled = true;
            this.cboYear.ItemHeight = 17;
            this.cboYear.Location = new System.Drawing.Point(80, 51);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(105, 23);
            this.cboYear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboYear.TabIndex = 187;
            // 
            // colid
            // 
            this.colid.FieldName = "id";
            this.colid.Name = "colid";
            this.colid.OptionsColumn.AllowEdit = false;
            this.colid.OptionsColumn.ReadOnly = true;
            // 
            // colfullname
            // 
            this.colfullname.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.colfullname.AppearanceHeader.ForeColor = System.Drawing.Color.Blue;
            this.colfullname.AppearanceHeader.Options.UseFont = true;
            this.colfullname.AppearanceHeader.Options.UseForeColor = true;
            this.colfullname.Caption = "Full Name";
            this.colfullname.FieldName = "fullName";
            this.colfullname.Name = "colfullname";
            this.colfullname.OptionsColumn.AllowEdit = false;
            this.colfullname.OptionsColumn.ReadOnly = true;
            this.colfullname.Visible = true;
            this.colfullname.VisibleIndex = 0;
            // 
            // coldate_of_birth
            // 
            this.coldate_of_birth.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.coldate_of_birth.AppearanceHeader.ForeColor = System.Drawing.Color.Blue;
            this.coldate_of_birth.AppearanceHeader.Options.UseFont = true;
            this.coldate_of_birth.AppearanceHeader.Options.UseForeColor = true;
            this.coldate_of_birth.Caption = "DOB";
            this.coldate_of_birth.FieldName = "dateOfBirth";
            this.coldate_of_birth.Name = "coldate_of_birth";
            this.coldate_of_birth.OptionsColumn.AllowEdit = false;
            this.coldate_of_birth.OptionsColumn.ReadOnly = true;
            this.coldate_of_birth.Visible = true;
            this.coldate_of_birth.VisibleIndex = 1;
            // 
            // colphone
            // 
            this.colphone.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.colphone.AppearanceHeader.ForeColor = System.Drawing.Color.Blue;
            this.colphone.AppearanceHeader.Options.UseFont = true;
            this.colphone.AppearanceHeader.Options.UseForeColor = true;
            this.colphone.Caption = "Phone";
            this.colphone.FieldName = "phone";
            this.colphone.Name = "colphone";
            this.colphone.OptionsColumn.AllowEdit = false;
            this.colphone.OptionsColumn.ReadOnly = true;
            this.colphone.Visible = true;
            this.colphone.VisibleIndex = 3;
            // 
            // colpassport_no
            // 
            this.colpassport_no.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.colpassport_no.AppearanceHeader.ForeColor = System.Drawing.Color.Blue;
            this.colpassport_no.AppearanceHeader.Options.UseFont = true;
            this.colpassport_no.AppearanceHeader.Options.UseForeColor = true;
            this.colpassport_no.Caption = "Passport";
            this.colpassport_no.FieldName = "passportNumber";
            this.colpassport_no.Name = "colpassport_no";
            this.colpassport_no.OptionsColumn.AllowEdit = false;
            this.colpassport_no.OptionsColumn.ReadOnly = true;
            this.colpassport_no.Visible = true;
            this.colpassport_no.VisibleIndex = 2;
            // 
            // colapply_semester
            // 
            this.colapply_semester.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.colapply_semester.AppearanceHeader.ForeColor = System.Drawing.Color.Blue;
            this.colapply_semester.AppearanceHeader.Options.UseFont = true;
            this.colapply_semester.AppearanceHeader.Options.UseForeColor = true;
            this.colapply_semester.Caption = "Sesmester";
            this.colapply_semester.FieldName = "applySesmester";
            this.colapply_semester.Name = "colapply_semester";
            this.colapply_semester.OptionsColumn.AllowEdit = false;
            this.colapply_semester.OptionsColumn.ReadOnly = true;
            this.colapply_semester.Visible = true;
            this.colapply_semester.VisibleIndex = 4;
            // 
            // colkobec_partner
            // 
            this.colkobec_partner.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.colkobec_partner.AppearanceHeader.ForeColor = System.Drawing.Color.Blue;
            this.colkobec_partner.AppearanceHeader.Options.UseFont = true;
            this.colkobec_partner.AppearanceHeader.Options.UseForeColor = true;
            this.colkobec_partner.Caption = "KOBEC Partner";
            this.colkobec_partner.FieldName = "kobecPartner";
            this.colkobec_partner.Name = "colkobec_partner";
            this.colkobec_partner.OptionsColumn.AllowEdit = false;
            this.colkobec_partner.OptionsColumn.ReadOnly = true;
            this.colkobec_partner.Visible = true;
            this.colkobec_partner.VisibleIndex = 5;
            // 
            // interview
            // 
            this.interview.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.interview.AppearanceHeader.ForeColor = System.Drawing.Color.Blue;
            this.interview.AppearanceHeader.Options.UseFont = true;
            this.interview.AppearanceHeader.Options.UseForeColor = true;
            this.interview.Caption = "KOBEC Result";
            this.interview.FieldName = "kobecResult";
            this.interview.Name = "interview";
            this.interview.OptionsColumn.AllowEdit = false;
            this.interview.OptionsColumn.ReadOnly = true;
            this.interview.Visible = true;
            this.interview.VisibleIndex = 10;
            // 
            // University
            // 
            this.University.Caption = "University";
            this.University.FieldName = "University";
            this.University.Name = "University";
            this.University.OptionsColumn.AllowEdit = false;
            this.University.OptionsColumn.ReadOnly = true;
            this.University.Visible = true;
            this.University.VisibleIndex = 6;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Invoice Date";
            this.gridColumn3.FieldName = "InvoiceDate";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 7;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Visa Code Date";
            this.gridColumn2.FieldName = "VisaCodeDate";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 9;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Visa";
            this.gridColumn1.FieldName = "Visa";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 8;
            // 
            // addmissionResult
            // 
            this.addmissionResult.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.addmissionResult.AppearanceHeader.ForeColor = System.Drawing.Color.Blue;
            this.addmissionResult.AppearanceHeader.Options.UseFont = true;
            this.addmissionResult.AppearanceHeader.Options.UseForeColor = true;
            this.addmissionResult.Caption = "Addmission Result";
            this.addmissionResult.FieldName = "addmissionResult";
            this.addmissionResult.Name = "addmissionResult";
            this.addmissionResult.OptionsColumn.AllowEdit = false;
            this.addmissionResult.OptionsColumn.ReadOnly = true;
            this.addmissionResult.Visible = true;
            this.addmissionResult.VisibleIndex = 11;
            // 
            // lblNumberStudent
            // 
            this.lblNumberStudent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumberStudent.AutoSize = true;
            this.lblNumberStudent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblNumberStudent.Location = new System.Drawing.Point(991, 64);
            this.lblNumberStudent.Name = "lblNumberStudent";
            this.lblNumberStudent.Size = new System.Drawing.Size(16, 17);
            this.lblNumberStudent.TabIndex = 201;
            this.lblNumberStudent.Text = "0";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(874, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 17);
            this.label1.TabIndex = 200;
            this.label1.Text = "Total Candidates";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(25, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 202;
            this.label2.Text = "Year";
            // 
            // StudentListCandidate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblNumberStudent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboSesson);
            this.Controls.Add(this.cboYear);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.btnAddNewStudent);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtKeyword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "StudentListCandidate";
            this.Size = new System.Drawing.Size(1040, 535);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridListStudents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentbasicinforBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private DevComponents.DotNetBar.ButtonX btnAddNewStudent;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn11;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn12;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn13;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn14;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn15;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn16;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn17;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn18;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn19;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn20;
        private DevExpress.XtraGrid.GridControl gridListStudents;
        private System.Windows.Forms.BindingSource studentbasicinforBindingSource;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_View;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSesson;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboYear;
        private DevExpress.XtraGrid.Columns.GridColumn colid;
        private DevExpress.XtraGrid.Columns.GridColumn colfullname;
        private DevExpress.XtraGrid.Columns.GridColumn coldate_of_birth;
        private DevExpress.XtraGrid.Columns.GridColumn colphone;
        private DevExpress.XtraGrid.Columns.GridColumn colpassport_no;
        private DevExpress.XtraGrid.Columns.GridColumn colapply_semester;
        private DevExpress.XtraGrid.Columns.GridColumn colkobec_partner;
        private DevExpress.XtraGrid.Columns.GridColumn interview;
        private DevExpress.XtraGrid.Columns.GridColumn University;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn addmissionResult;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Label lblNumberStudent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
