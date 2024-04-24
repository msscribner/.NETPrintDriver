namespace PrintSpooler
{
    partial class MainForm
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
            this.lbSpoolChanges = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPrinters = new System.Windows.Forms.ComboBox();
            this.btnMonitor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbSpoolChanges
            // 
            this.lbSpoolChanges.FormattingEnabled = true;
            this.lbSpoolChanges.Location = new System.Drawing.Point(0, 59);
            this.lbSpoolChanges.Name = "lbSpoolChanges";
            this.lbSpoolChanges.Size = new System.Drawing.Size(650, 355);
            this.lbSpoolChanges.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select the Printer:";
            // 
            // cmbPrinters
            // 
            this.cmbPrinters.FormattingEnabled = true;
            this.cmbPrinters.Location = new System.Drawing.Point(110, 20);
            this.cmbPrinters.Name = "cmbPrinters";
            this.cmbPrinters.Size = new System.Drawing.Size(255, 21);
            this.cmbPrinters.TabIndex = 2;
            // 
            // btnMonitor
            // 
            this.btnMonitor.Location = new System.Drawing.Point(384, 21);
            this.btnMonitor.Name = "btnMonitor";
            this.btnMonitor.Size = new System.Drawing.Size(156, 19);
            this.btnMonitor.TabIndex = 3;
            this.btnMonitor.Text = "Start Monitoring...";
            this.btnMonitor.UseVisualStyleBackColor = true;
            this.btnMonitor.Click += new System.EventHandler(this.btnMonitor_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 413);
            this.Controls.Add(this.btnMonitor);
            this.Controls.Add(this.cmbPrinters);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbSpoolChanges);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Windows Printer Spool Monitor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbSpoolChanges;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbPrinters;
        private System.Windows.Forms.Button btnMonitor;
    }
}

