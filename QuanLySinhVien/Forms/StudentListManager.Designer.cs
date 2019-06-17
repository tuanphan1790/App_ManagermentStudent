namespace QuanLySinhVien.Forms
{
    partial class StudentListManager
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
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridListStudents = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfullname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldate_of_birth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colphone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colpassport_no = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colapply_semester = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colkobec_partner = new DevExpress.XtraGrid.Columns.GridColumn();
            this.University = new DevExpress.XtraGrid.Columns.GridColumn();
            this.studentbasicinforBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barButtonItem_View = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNumberStudent = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridListStudents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentbasicinforBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSearch.Location = new System.Drawing.Point(291, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(99, 23);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.Symbol = "";
            this.btnSearch.TabIndex = 190;
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
            this.groupBox2.Location = new System.Drawing.Point(3, 46);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(991, 441);
            this.groupBox2.TabIndex = 189;
            this.groupBox2.TabStop = false;
            // 
            // gridListStudents
            // 
            this.gridListStudents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridListStudents.Location = new System.Drawing.Point(3, 19);
            this.gridListStudents.MainView = this.gridView1;
            this.gridListStudents.Name = "gridListStudents";
            this.gridListStudents.Size = new System.Drawing.Size(985, 419);
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
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colid,
            this.colfullname,
            this.coldate_of_birth,
            this.colphone,
            this.colpassport_no,
            this.colapply_semester,
            this.colkobec_partner,
            this.University});
            this.gridView1.GridControl = this.gridListStudents;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseDown);
            this.gridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseUp);
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
            this.colkobec_partner.Visible = true;
            this.colkobec_partner.VisibleIndex = 5;
            // 
            // University
            // 
            this.University.Caption = "University";
            this.University.FieldName = "University";
            this.University.Name = "University";
            this.University.Visible = true;
            this.University.VisibleIndex = 6;
            // 
            // studentbasicinforBindingSource
            // 
            this.studentbasicinforBindingSource.DataMember = "student_basic_infor";
            // 
            // txtKeyword
            // 
            this.txtKeyword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtKeyword.Location = new System.Drawing.Point(81, 17);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(204, 23);
            this.txtKeyword.TabIndex = 193;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(13, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 17);
            this.label4.TabIndex = 192;
            this.label4.Text = "Keyword";
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
            this.barDockControlTop.Size = new System.Drawing.Size(997, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 491);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(997, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 491);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(997, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 491);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(850, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 17);
            this.label1.TabIndex = 198;
            this.label1.Text = "Total Students";
            // 
            // lblNumberStudent
            // 
            this.lblNumberStudent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumberStudent.AutoSize = true;
            this.lblNumberStudent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblNumberStudent.Location = new System.Drawing.Point(953, 19);
            this.lblNumberStudent.Name = "lblNumberStudent";
            this.lblNumberStudent.Size = new System.Drawing.Size(16, 17);
            this.lblNumberStudent.TabIndex = 199;
            this.lblNumberStudent.Text = "0";
            // 
            // StudentListManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblNumberStudent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtKeyword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "StudentListManager";
            this.Size = new System.Drawing.Size(997, 491);
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

        private DevComponents.DotNetBar.ButtonX btnSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl gridListStudents;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colid;
        private DevExpress.XtraGrid.Columns.GridColumn colfullname;
        private DevExpress.XtraGrid.Columns.GridColumn coldate_of_birth;
        private DevExpress.XtraGrid.Columns.GridColumn colphone;
        private DevExpress.XtraGrid.Columns.GridColumn colpassport_no;
        private DevExpress.XtraGrid.Columns.GridColumn colapply_semester;
        private DevExpress.XtraGrid.Columns.GridColumn colkobec_partner;
        private DevExpress.XtraGrid.Columns.GridColumn University;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_View;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.BindingSource studentbasicinforBindingSource;
        private System.Windows.Forms.Label lblNumberStudent;
        private System.Windows.Forms.Label label1;
    }
}
