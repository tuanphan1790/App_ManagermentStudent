namespace QuanLySinhVien.Forms
{
    partial class AddMoreDescription
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
            this.labelX120 = new DevComponents.DotNetBar.LabelX();
            this.txtAmount1 = new System.Windows.Forms.TextBox();
            this.txtDes1 = new System.Windows.Forms.TextBox();
            this.labelX121 = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // labelX120
            // 
            this.labelX120.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX120.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX120.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX120.ForeColor = System.Drawing.Color.Black;
            this.labelX120.Location = new System.Drawing.Point(21, 12);
            this.labelX120.Name = "labelX120";
            this.labelX120.Size = new System.Drawing.Size(86, 23);
            this.labelX120.TabIndex = 187;
            this.labelX120.Text = "Description";
            // 
            // txtAmount1
            // 
            this.txtAmount1.Location = new System.Drawing.Point(129, 44);
            this.txtAmount1.Name = "txtAmount1";
            this.txtAmount1.Size = new System.Drawing.Size(272, 20);
            this.txtAmount1.TabIndex = 189;
            // 
            // txtDes1
            // 
            this.txtDes1.Location = new System.Drawing.Point(129, 15);
            this.txtDes1.Name = "txtDes1";
            this.txtDes1.Size = new System.Drawing.Size(272, 20);
            this.txtDes1.TabIndex = 186;
            // 
            // labelX121
            // 
            this.labelX121.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX121.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX121.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX121.ForeColor = System.Drawing.Color.Black;
            this.labelX121.Location = new System.Drawing.Point(21, 41);
            this.labelX121.Name = "labelX121";
            this.labelX121.Size = new System.Drawing.Size(86, 23);
            this.labelX121.TabIndex = 188;
            this.labelX121.Text = "Amount";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.buttonX1.Location = new System.Drawing.Point(182, 77);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(84, 27);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 195;
            this.buttonX1.Text = "Add";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // AddMoreDescription
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 116);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.labelX120);
            this.Controls.Add(this.txtAmount1);
            this.Controls.Add(this.txtDes1);
            this.Controls.Add(this.labelX121);
            this.Name = "AddMoreDescription";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add More Description";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX120;
        private System.Windows.Forms.TextBox txtAmount1;
        private System.Windows.Forms.TextBox txtDes1;
        private DevComponents.DotNetBar.LabelX labelX121;
        private DevComponents.DotNetBar.ButtonX buttonX1;
    }
}