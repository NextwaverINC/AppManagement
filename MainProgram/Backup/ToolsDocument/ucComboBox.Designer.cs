namespace ToolsDocument
{
    partial class ucComboBox
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
            this.cbxTemp = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbxTemp
            // 
            this.cbxTemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxTemp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTemp.FormattingEnabled = true;
            this.cbxTemp.Location = new System.Drawing.Point(0, 0);
            this.cbxTemp.Name = "cbxTemp";
            this.cbxTemp.Size = new System.Drawing.Size(177, 21);
            this.cbxTemp.TabIndex = 0;
            this.cbxTemp.TextChanged += new System.EventHandler(this.cbxTemp_TextChanged);
            // 
            // ucComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbxTemp);
            this.Name = "ucComboBox";
            this.Size = new System.Drawing.Size(177, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxTemp;
    }
}
