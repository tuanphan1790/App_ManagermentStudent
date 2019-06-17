namespace QuanLySinhVien.Forms
{
    partial class LogHis
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
            this.sgLogHis = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.btnClear = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // sgLogHis
            // 
            this.sgLogHis.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sgLogHis.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.sgLogHis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.sgLogHis.Location = new System.Drawing.Point(0, 48);
            this.sgLogHis.Name = "sgLogHis";
            gridColumn1.AllowEdit = false;
            gridColumn1.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.DisplayedCells;
            gridColumn1.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn1.Name = "Date";
            gridColumn1.ReadOnly = true;
            gridColumn2.AllowEdit = false;
            gridColumn2.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.DisplayedCells;
            gridColumn2.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn2.Name = "User";
            gridColumn2.ReadOnly = true;
            gridColumn3.AllowEdit = false;
            gridColumn3.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            gridColumn3.CellStyles.Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn3.Name = "Operation";
            gridColumn3.ReadOnly = true;
            this.sgLogHis.PrimaryGrid.Columns.Add(gridColumn1);
            this.sgLogHis.PrimaryGrid.Columns.Add(gridColumn2);
            this.sgLogHis.PrimaryGrid.Columns.Add(gridColumn3);
            this.sgLogHis.Size = new System.Drawing.Size(760, 476);
            this.sgLogHis.TabIndex = 0;
            this.sgLogHis.Text = "sgLogHis";
            // 
            // btnClear
            // 
            this.btnClear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnClear.Location = new System.Drawing.Point(619, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(138, 27);
            this.btnClear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClear.TabIndex = 167;
            this.btnClear.Text = "Clear all history";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // LogHis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.sgLogHis);
            this.Name = "LogHis";
            this.Size = new System.Drawing.Size(760, 524);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperGrid.SuperGridControl sgLogHis;
        private DevComponents.DotNetBar.ButtonX btnClear;
    }
}
