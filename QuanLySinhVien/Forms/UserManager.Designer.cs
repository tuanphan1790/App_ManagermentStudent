namespace QuanLySinhVien.Forms
{
    partial class UserManager
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
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.sgUserManager = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sgUserManager
            // 
            this.sgUserManager.ContextMenuStrip = this.contextMenuStrip1;
            this.sgUserManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sgUserManager.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.sgUserManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.sgUserManager.Location = new System.Drawing.Point(0, 0);
            this.sgUserManager.Name = "sgUserManager";
            gridColumn1.AllowEdit = false;
            gridColumn1.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn1.Name = "Id";
            gridColumn1.ReadOnly = true;
            gridColumn1.Visible = false;
            gridColumn2.AllowEdit = false;
            gridColumn2.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            gridColumn2.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn2.Name = "User Name";
            gridColumn2.ReadOnly = true;
            gridColumn3.AllowEdit = false;
            gridColumn3.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            gridColumn3.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn3.Name = "Full Name";
            gridColumn3.ReadOnly = true;
            gridColumn4.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            gridColumn4.Name = "Email";
            gridColumn5.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            gridColumn5.Name = "Phone";
            gridColumn6.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            gridColumn6.Name = "Address";
            gridColumn7.AllowEdit = false;
            gridColumn7.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            gridColumn7.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn7.Name = "Account Type";
            gridColumn7.ReadOnly = true;
            this.sgUserManager.PrimaryGrid.Columns.Add(gridColumn1);
            this.sgUserManager.PrimaryGrid.Columns.Add(gridColumn2);
            this.sgUserManager.PrimaryGrid.Columns.Add(gridColumn3);
            this.sgUserManager.PrimaryGrid.Columns.Add(gridColumn4);
            this.sgUserManager.PrimaryGrid.Columns.Add(gridColumn5);
            this.sgUserManager.PrimaryGrid.Columns.Add(gridColumn6);
            this.sgUserManager.PrimaryGrid.Columns.Add(gridColumn7);
            this.sgUserManager.Size = new System.Drawing.Size(989, 426);
            this.sgUserManager.TabIndex = 1;
            this.sgUserManager.Text = "superGridControl1";
            this.sgUserManager.CellMouseUp += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellMouseEventArgs>(this.sgUserManager_CellMouseUp);
            this.sgUserManager.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sgUserManager_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createUserToolStripMenuItem,
            this.editUserToolStripMenuItem,
            this.deleteUserToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(134, 70);
            // 
            // createUserToolStripMenuItem
            // 
            this.createUserToolStripMenuItem.Name = "createUserToolStripMenuItem";
            this.createUserToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.createUserToolStripMenuItem.Text = "Create user";
            this.createUserToolStripMenuItem.Click += new System.EventHandler(this.createUserToolStripMenuItem_Click);
            // 
            // editUserToolStripMenuItem
            // 
            this.editUserToolStripMenuItem.Name = "editUserToolStripMenuItem";
            this.editUserToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.editUserToolStripMenuItem.Text = "Edit user";
            this.editUserToolStripMenuItem.Click += new System.EventHandler(this.editUserToolStripMenuItem_Click);
            // 
            // deleteUserToolStripMenuItem
            // 
            this.deleteUserToolStripMenuItem.Name = "deleteUserToolStripMenuItem";
            this.deleteUserToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.deleteUserToolStripMenuItem.Text = "Delete user";
            this.deleteUserToolStripMenuItem.Click += new System.EventHandler(this.deleteUserToolStripMenuItem_Click);
            // 
            // UserManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sgUserManager);
            this.Name = "UserManager";
            this.Size = new System.Drawing.Size(989, 426);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperGrid.SuperGridControl sgUserManager;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createUserToolStripMenuItem;
    }
}
