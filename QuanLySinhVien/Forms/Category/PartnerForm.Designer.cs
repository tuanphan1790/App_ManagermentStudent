namespace QuanLySinhVien.Forms.Category
{
    partial class PartnerForm
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
            this.txtIDPartner = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtNamePartner = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnCancel.Location = new System.Drawing.Point(218, 95);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 167;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnOK.Location = new System.Drawing.Point(125, 95);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 166;
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
            this.labelX4.Location = new System.Drawing.Point(28, 18);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(93, 23);
            this.labelX4.TabIndex = 164;
            this.labelX4.Text = "ID Partner";
            // 
            // txtIDPartner
            // 
            this.txtIDPartner.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtIDPartner.Border.Class = "TextBoxBorder";
            this.txtIDPartner.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtIDPartner.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtIDPartner.ForeColor = System.Drawing.Color.Black;
            this.txtIDPartner.Location = new System.Drawing.Point(128, 18);
            this.txtIDPartner.Name = "txtIDPartner";
            this.txtIDPartner.Size = new System.Drawing.Size(242, 23);
            this.txtIDPartner.TabIndex = 165;
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
            this.labelX1.Location = new System.Drawing.Point(29, 55);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(93, 23);
            this.labelX1.TabIndex = 168;
            this.labelX1.Text = "Kobec Partner";
            // 
            // txtNamePartner
            // 
            this.txtNamePartner.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtNamePartner.Border.Class = "TextBoxBorder";
            this.txtNamePartner.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNamePartner.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNamePartner.ForeColor = System.Drawing.Color.Black;
            this.txtNamePartner.Location = new System.Drawing.Point(128, 55);
            this.txtNamePartner.Name = "txtNamePartner";
            this.txtNamePartner.Size = new System.Drawing.Size(242, 23);
            this.txtNamePartner.TabIndex = 169;
            // 
            // PartnerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 135);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txtNamePartner);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.txtIDPartner);
            this.Name = "PartnerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Order";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtIDPartner;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNamePartner;
    }
}