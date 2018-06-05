namespace Document
{
    partial class frmAddItemProfile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddItemProfile));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.butDApply = new System.Windows.Forms.Button();
            this.butDCanecl = new System.Windows.Forms.Button();
            this.txbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.butDApply);
            this.groupBox1.Controls.Add(this.butDCanecl);
            this.groupBox1.Controls.Add(this.txbName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(292, 79);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // butDApply
            // 
            this.butDApply.Image = ((System.Drawing.Image)(resources.GetObject("butDApply.Image")));
            this.butDApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butDApply.Location = new System.Drawing.Point(118, 45);
            this.butDApply.Name = "butDApply";
            this.butDApply.Size = new System.Drawing.Size(61, 23);
            this.butDApply.TabIndex = 12;
            this.butDApply.Text = "Apply";
            this.butDApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butDApply.UseVisualStyleBackColor = true;
            this.butDApply.Click += new System.EventHandler(this.butDApply_Click);
            // 
            // butDCanecl
            // 
            this.butDCanecl.Image = ((System.Drawing.Image)(resources.GetObject("butDCanecl.Image")));
            this.butDCanecl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butDCanecl.Location = new System.Drawing.Point(185, 45);
            this.butDCanecl.Name = "butDCanecl";
            this.butDCanecl.Size = new System.Drawing.Size(71, 23);
            this.butDCanecl.TabIndex = 11;
            this.butDCanecl.Text = "Cancel";
            this.butDCanecl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butDCanecl.UseVisualStyleBackColor = true;
            this.butDCanecl.Click += new System.EventHandler(this.butDCanecl_Click);
            // 
            // txbName
            // 
            this.txbName.Location = new System.Drawing.Point(69, 19);
            this.txbName.Name = "txbName";
            this.txbName.Size = new System.Drawing.Size(187, 20);
            this.txbName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(24, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // frmAddItemProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 79);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(298, 106);
            this.MinimumSize = new System.Drawing.Size(298, 106);
            this.Name = "frmAddItemProfile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "โปรดระบุชื่อ";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button butDApply;
        private System.Windows.Forms.Button butDCanecl;
        private System.Windows.Forms.TextBox txbName;
        private System.Windows.Forms.Label label1;
    }
}