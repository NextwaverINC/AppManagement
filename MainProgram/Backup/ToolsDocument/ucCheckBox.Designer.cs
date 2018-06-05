namespace ToolsDocument
{
    partial class ucCheckBox
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
            this.chbTemp = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chbTemp
            // 
            this.chbTemp.AutoSize = true;
            this.chbTemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chbTemp.Location = new System.Drawing.Point(0, 0);
            this.chbTemp.Name = "chbTemp";
            this.chbTemp.Size = new System.Drawing.Size(87, 25);
            this.chbTemp.TabIndex = 0;
            this.chbTemp.Text = "checkBox1";
            this.chbTemp.UseVisualStyleBackColor = true;
            // 
            // ucCheckBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chbTemp);
            this.Name = "ucCheckBox";
            this.Size = new System.Drawing.Size(87, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbTemp;
    }
}
