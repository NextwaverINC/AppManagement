namespace Document
{
    partial class frmProfile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProfile));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txbName = new System.Windows.Forms.TextBox();
            this.butDApply = new System.Windows.Forms.Button();
            this.butDCanecl = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbiAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbiDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.trvProfile = new System.Windows.Forms.TreeView();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(284, 367);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.butDApply);
            this.groupBox1.Controls.Add(this.butDCanecl);
            this.groupBox1.Controls.Add(this.txbName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 87);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Edit Data";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(20, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // txbName
            // 
            this.txbName.Location = new System.Drawing.Point(65, 31);
            this.txbName.Name = "txbName";
            this.txbName.Size = new System.Drawing.Size(187, 20);
            this.txbName.TabIndex = 1;
            // 
            // butDApply
            // 
            this.butDApply.Image = ((System.Drawing.Image)(resources.GetObject("butDApply.Image")));
            this.butDApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butDApply.Location = new System.Drawing.Point(114, 57);
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
            this.butDCanecl.Location = new System.Drawing.Point(181, 57);
            this.butDCanecl.Name = "butDCanecl";
            this.butDCanecl.Size = new System.Drawing.Size(71, 23);
            this.butDCanecl.TabIndex = 11;
            this.butDCanecl.Text = "Cancel";
            this.butDCanecl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butDCanecl.UseVisualStyleBackColor = true;
            this.butDCanecl.Click += new System.EventHandler(this.butDCanecl_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.trvProfile);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 104);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 260);
            this.panel1.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbiAdd,
            this.tsbiDelete,
            this.tsbClose,
            this.tsbSave,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(278, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbiAdd
            // 
            this.tsbiAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbiAdd.Image")));
            this.tsbiAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbiAdd.Name = "tsbiAdd";
            this.tsbiAdd.Size = new System.Drawing.Size(73, 22);
            this.tsbiAdd.Text = "เพิ่มข้อมูล";
            this.tsbiAdd.Click += new System.EventHandler(this.tsbiAdd_Click);
            // 
            // tsbiDelete
            // 
            this.tsbiDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbiDelete.Image")));
            this.tsbiDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbiDelete.Name = "tsbiDelete";
            this.tsbiDelete.Size = new System.Drawing.Size(69, 22);
            this.tsbiDelete.Text = "ลบข้อมูล";
            this.tsbiDelete.Click += new System.EventHandler(this.tsbiDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // trvProfile
            // 
            this.trvProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvProfile.Location = new System.Drawing.Point(0, 25);
            this.trvProfile.Name = "trvProfile";
            this.trvProfile.Size = new System.Drawing.Size(278, 235);
            this.trvProfile.TabIndex = 3;
            this.trvProfile.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvProfile_AfterSelect);
            this.trvProfile.Click += new System.EventHandler(this.trvProfile_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(51, 22);
            this.tsbSave.Text = "Save";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsbClose
            // 
            this.tsbClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(53, 22);
            this.tsbClose.Text = "Close";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // frmProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 367);
            this.Controls.Add(this.tableLayoutPanel1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(292, 400);
            this.MinimumSize = new System.Drawing.Size(292, 400);
            this.Name = "frmProfile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "หน้าต่างจัดการข้อมูล Profile";
            this.Load += new System.EventHandler(this.frmProfile_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butDApply;
        private System.Windows.Forms.Button butDCanecl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbiAdd;
        private System.Windows.Forms.ToolStripButton tsbiDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TreeView trvProfile;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbClose;

    }
}