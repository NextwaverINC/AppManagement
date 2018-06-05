namespace MainProgram
{
    partial class frmMenu
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenu));
            this.taskPane1 = new XPExplorerBar.TaskPane();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMinimize = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMaximum = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMode = new System.Windows.Forms.ToolStripMenuItem();
            this.tbtsmShowTree = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsmiLockoff = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.taskPane1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // taskPane1
            // 
            this.taskPane1.AutoScrollMargin = new System.Drawing.Size(12, 12);
            this.taskPane1.CustomSettings.GradientEndColor = System.Drawing.Color.White;
            this.taskPane1.CustomSettings.GradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.taskPane1.CustomSettings.WatermarkAlignment = System.Drawing.ContentAlignment.BottomLeft;
            this.taskPane1.Dock = System.Windows.Forms.DockStyle.Left;
            this.taskPane1.Location = new System.Drawing.Point(0, 0);
            this.taskPane1.Margin = new System.Windows.Forms.Padding(0);
            this.taskPane1.Name = "taskPane1";
            this.taskPane1.Size = new System.Drawing.Size(261, 632);
            this.taskPane1.TabIndex = 4;
            this.taskPane1.Text = "taskPane1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiClose});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(178, 26);
            // 
            // tsmiClose
            // 
            this.tsmiClose.Image = ((System.Drawing.Image)(resources.GetObject("tsmiClose.Image")));
            this.tsmiClose.Name = "tsmiClose";
            this.tsmiClose.Size = new System.Drawing.Size(177, 22);
            this.tsmiClose.Text = "Close Management";
            this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "book_open2.png");
            this.imageList2.Images.SetKeyName(1, "books.png");
            this.imageList2.Images.SetKeyName(2, "book_blue.png");
            this.imageList2.Images.SetKeyName(3, "books_preferences.png");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "id_card.png");
            this.imageList1.Images.SetKeyName(1, "id_card_preferences.png");
            this.imageList1.Images.SetKeyName(2, "Search the internet.png");
            this.imageList1.Images.SetKeyName(3, "Internet.png");
            this.imageList1.Images.SetKeyName(4, "user-busy_256x256.png");
            this.imageList1.Images.SetKeyName(5, "user-off_256x256.png");
            this.imageList1.Images.SetKeyName(6, "user__256x256.png");
            this.imageList1.Images.SetKeyName(7, "user-on_256x256.png");
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.tabControl1.Location = new System.Drawing.Point(261, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(656, 632);
            this.tabControl1.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1,
            this.toolStripStatusLabel1,
            this.tslUserName,
            this.tslPosition,
            this.toolStripStatusLabel2,
            this.lbStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 632);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(917, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.statusStrip1_MouseDoubleClick_1);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCloseAll,
            this.tsmiHorizontal,
            this.tsmiVertical,
            this.tsmiMinimize,
            this.tsmiMaximum,
            this.tsmiCascade,
            this.tsmiMode,
            this.tbtsmShowTree,
            this.tsmiLockoff});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(29, 20);
            this.toolStripSplitButton1.Text = "MDI Control";
            // 
            // tsmiCloseAll
            // 
            this.tsmiCloseAll.Enabled = false;
            this.tsmiCloseAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCloseAll.Image")));
            this.tsmiCloseAll.Name = "tsmiCloseAll";
            this.tsmiCloseAll.Size = new System.Drawing.Size(188, 22);
            this.tsmiCloseAll.Text = "Close All";
            this.tsmiCloseAll.Click += new System.EventHandler(this.tsmiCloseAll_Click);
            // 
            // tsmiHorizontal
            // 
            this.tsmiHorizontal.Enabled = false;
            this.tsmiHorizontal.Image = ((System.Drawing.Image)(resources.GetObject("tsmiHorizontal.Image")));
            this.tsmiHorizontal.Name = "tsmiHorizontal";
            this.tsmiHorizontal.Size = new System.Drawing.Size(188, 22);
            this.tsmiHorizontal.Text = "Arrange Horizontal";
            this.tsmiHorizontal.Click += new System.EventHandler(this.tsmiHorizontal_Click);
            // 
            // tsmiVertical
            // 
            this.tsmiVertical.Enabled = false;
            this.tsmiVertical.Image = ((System.Drawing.Image)(resources.GetObject("tsmiVertical.Image")));
            this.tsmiVertical.Name = "tsmiVertical";
            this.tsmiVertical.Size = new System.Drawing.Size(188, 22);
            this.tsmiVertical.Text = "Arrange Vertical";
            this.tsmiVertical.Click += new System.EventHandler(this.tsmiVertical_Click);
            // 
            // tsmiMinimize
            // 
            this.tsmiMinimize.Enabled = false;
            this.tsmiMinimize.Image = ((System.Drawing.Image)(resources.GetObject("tsmiMinimize.Image")));
            this.tsmiMinimize.Name = "tsmiMinimize";
            this.tsmiMinimize.Size = new System.Drawing.Size(188, 22);
            this.tsmiMinimize.Text = "Minimize all";
            this.tsmiMinimize.Click += new System.EventHandler(this.tsmiMinimize_Click);
            // 
            // tsmiMaximum
            // 
            this.tsmiMaximum.Enabled = false;
            this.tsmiMaximum.Image = ((System.Drawing.Image)(resources.GetObject("tsmiMaximum.Image")));
            this.tsmiMaximum.Name = "tsmiMaximum";
            this.tsmiMaximum.Size = new System.Drawing.Size(188, 22);
            this.tsmiMaximum.Text = "Maximize all";
            this.tsmiMaximum.Click += new System.EventHandler(this.tsmiMaximum_Click);
            // 
            // tsmiCascade
            // 
            this.tsmiCascade.Enabled = false;
            this.tsmiCascade.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCascade.Image")));
            this.tsmiCascade.Name = "tsmiCascade";
            this.tsmiCascade.Size = new System.Drawing.Size(188, 22);
            this.tsmiCascade.Text = "Cascade";
            this.tsmiCascade.Click += new System.EventHandler(this.tsmiCascade_Click);
            // 
            // tsmiMode
            // 
            this.tsmiMode.Image = ((System.Drawing.Image)(resources.GetObject("tsmiMode.Image")));
            this.tsmiMode.Name = "tsmiMode";
            this.tsmiMode.Size = new System.Drawing.Size(188, 22);
            this.tsmiMode.Text = "Tabs Mode";
            this.tsmiMode.Click += new System.EventHandler(this.tsmiMode_Click);
            // 
            // tbtsmShowTree
            // 
            this.tbtsmShowTree.Image = ((System.Drawing.Image)(resources.GetObject("tbtsmShowTree.Image")));
            this.tbtsmShowTree.Name = "tbtsmShowTree";
            this.tbtsmShowTree.Size = new System.Drawing.Size(188, 22);
            this.tbtsmShowTree.Text = "Show and Hide Menu";
            this.tbtsmShowTree.Click += new System.EventHandler(this.tbtsmShowTree_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(582, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // tslUserName
            // 
            this.tslUserName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslUserName.ForeColor = System.Drawing.Color.Navy;
            this.tslUserName.Image = ((System.Drawing.Image)(resources.GetObject("tslUserName.Image")));
            this.tslUserName.Name = "tslUserName";
            this.tslUserName.Size = new System.Drawing.Size(97, 17);
            this.tslUserName.Text = "User Name:";
            // 
            // tslPosition
            // 
            this.tslPosition.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslPosition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tslPosition.Image = ((System.Drawing.Image)(resources.GetObject("tslPosition.Image")));
            this.tslPosition.Name = "tslPosition";
            this.tslPosition.Size = new System.Drawing.Size(84, 17);
            this.tslPosition.Text = "Position :";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // lbStatus
            // 
            this.lbStatus.ForeColor = System.Drawing.Color.Black;
            this.lbStatus.Image = ((System.Drawing.Image)(resources.GetObject("lbStatus.Image")));
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(67, 17);
            this.lbStatus.Tag = "BREADY";
            this.lbStatus.Text = "Ready ...";
            // 
            // tsmiLockoff
            // 
            this.tsmiLockoff.Image = ((System.Drawing.Image)(resources.GetObject("tsmiLockoff.Image")));
            this.tsmiLockoff.Name = "tsmiLockoff";
            this.tsmiLockoff.Size = new System.Drawing.Size(188, 22);
            this.tsmiLockoff.Text = "Switch User";
            this.tsmiLockoff.Click += new System.EventHandler(this.tsmiLockoff_Click);
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 654);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.taskPane1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Name = "frmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.taskPane1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem tbtsmShowTree;
        private System.Windows.Forms.ToolStripMenuItem tsmiHorizontal;
        private System.Windows.Forms.ToolStripMenuItem tsmiVertical;
        private System.Windows.Forms.ToolStripMenuItem tsmiMinimize;
        private System.Windows.Forms.ToolStripMenuItem tsmiCascade;
        private System.Windows.Forms.ToolStripMenuItem tsmiCloseAll;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiClose;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ToolStripStatusLabel tslUserName;
        private System.Windows.Forms.ToolStripStatusLabel tslPosition;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMaximum;
        private XPExplorerBar.TaskPane taskPane1;
        private System.Windows.Forms.ToolStripMenuItem tsmiMode;
        private System.Windows.Forms.ToolStripMenuItem tsmiLockoff;
    }
}