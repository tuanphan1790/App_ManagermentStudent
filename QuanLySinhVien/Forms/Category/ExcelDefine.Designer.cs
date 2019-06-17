namespace QuanLySinhVien.Forms.Category
{
    partial class ExcelDefine
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtFileNameExcel = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtFileExcelExport = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnCancel.Location = new System.Drawing.Point(270, 103);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 175;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnOK.Location = new System.Drawing.Point(177, 103);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 174;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX4.ForeColor = System.Drawing.Color.Black;
            this.labelX4.Location = new System.Drawing.Point(24, 21);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(124, 23);
            this.labelX4.TabIndex = 172;
            this.labelX4.Text = "File Name Excel";
            // 
            // txtFileNameExcel
            // 
            this.txtFileNameExcel.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtFileNameExcel.Border.Class = "TextBoxBorder";
            this.txtFileNameExcel.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFileNameExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtFileNameExcel.ForeColor = System.Drawing.Color.Black;
            this.txtFileNameExcel.Location = new System.Drawing.Point(154, 22);
            this.txtFileNameExcel.Name = "txtFileNameExcel";
            this.txtFileNameExcel.Size = new System.Drawing.Size(326, 23);
            this.txtFileNameExcel.TabIndex = 173;
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
            this.labelX1.Location = new System.Drawing.Point(24, 59);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(124, 23);
            this.labelX1.TabIndex = 176;
            this.labelX1.Text = "File Excel Export";
            // 
            // txtFileExcelExport
            // 
            this.txtFileExcelExport.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtFileExcelExport.Border.Class = "TextBoxBorder";
            this.txtFileExcelExport.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFileExcelExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtFileExcelExport.ForeColor = System.Drawing.Color.Black;
            this.txtFileExcelExport.Location = new System.Drawing.Point(154, 60);
            this.txtFileExcelExport.Name = "txtFileExcelExport";
            this.txtFileExcelExport.Size = new System.Drawing.Size(326, 23);
            this.txtFileExcelExport.TabIndex = 177;
            // 
            // ExcelDefine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 148);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txtFileExcelExport);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.txtFileNameExcel);
            this.Name = "ExcelDefine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ExcelDefine";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFileNameExcel;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFileExcelExport;
    }
}