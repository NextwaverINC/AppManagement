namespace Document
{
    partial class frmEditItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditItem));
            this.cbxItemType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbxItemName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.butDApply = new System.Windows.Forms.Button();
            this.butDCanecl = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbxItemType
            // 
            this.cbxItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxItemType.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbxItemType.FormattingEnabled = true;
            this.cbxItemType.Location = new System.Drawing.Point(15, 83);
            this.cbxItemType.Name = "cbxItemType";
            this.cbxItemType.Size = new System.Drawing.Size(221, 28);
            this.cbxItemType.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label10.Location = new System.Drawing.Point(12, 64);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 16);
            this.label10.TabIndex = 10;
            this.label10.Text = "Item Type :";
            // 
            // tbxItemName
            // 
            this.tbxItemName.Location = new System.Drawing.Point(15, 28);
            this.tbxItemName.Name = "tbxItemName";
            this.tbxItemName.Size = new System.Drawing.Size(221, 26);
            this.tbxItemName.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Item Name :";
            // 
            // butDApply
            // 
            this.butDApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.butDApply.Image = ((System.Drawing.Image)(resources.GetObject("butDApply.Image")));
            this.butDApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butDApply.Location = new System.Drawing.Point(92, 126);
            this.butDApply.Name = "butDApply";
            this.butDApply.Size = new System.Drawing.Size(67, 23);
            this.butDApply.TabIndex = 13;
            this.butDApply.Text = "Apply";
            this.butDApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butDApply.UseVisualStyleBackColor = true;
            this.butDApply.Click += new System.EventHandler(this.butDApply_Click);
            // 
            // butDCanecl
            // 
            this.butDCanecl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.butDCanecl.Image = ((System.Drawing.Image)(resources.GetObject("butDCanecl.Image")));
            this.butDCanecl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butDCanecl.Location = new System.Drawing.Point(165, 126);
            this.butDCanecl.Name = "butDCanecl";
            this.butDCanecl.Size = new System.Drawing.Size(71, 23);
            this.butDCanecl.TabIndex = 12;
            this.butDCanecl.Text = "Cancel";
            this.butDCanecl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butDCanecl.UseVisualStyleBackColor = true;
            this.butDCanecl.Click += new System.EventHandler(this.butDCanecl_Click);
            // 
            // frmEditItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 160);
            this.Controls.Add(this.butDApply);
            this.Controls.Add(this.butDCanecl);
            this.Controls.Add(this.cbxItemType);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbxItemName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(259, 271);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MaximumSize = new System.Drawing.Size(259, 187);
            this.MinimumSize = new System.Drawing.Size(259, 187);
            this.Name = "frmEditItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxItemType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbxItemName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butDApply;
        private System.Windows.Forms.Button butDCanecl;

    }
}