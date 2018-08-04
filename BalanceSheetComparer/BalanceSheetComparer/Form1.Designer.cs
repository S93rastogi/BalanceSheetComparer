namespace BalanceSheetComparer
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.firstBalanceSheet = new System.Windows.Forms.TextBox();
            this.secondBalanceSheeet = new System.Windows.Forms.TextBox();
            this.generateReport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "First Balance Sheet";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Second Balance Sheet";
            // 
            // firstBalanceSheet
            // 
            this.firstBalanceSheet.Location = new System.Drawing.Point(242, 38);
            this.firstBalanceSheet.Name = "firstBalanceSheet";
            this.firstBalanceSheet.Size = new System.Drawing.Size(264, 20);
            this.firstBalanceSheet.TabIndex = 2;
            // 
            // secondBalanceSheeet
            // 
            this.secondBalanceSheeet.Location = new System.Drawing.Point(242, 72);
            this.secondBalanceSheeet.Name = "secondBalanceSheeet";
            this.secondBalanceSheeet.Size = new System.Drawing.Size(264, 20);
            this.secondBalanceSheeet.TabIndex = 3;
            // 
            // generateReport
            // 
            this.generateReport.Location = new System.Drawing.Point(228, 121);
            this.generateReport.Name = "generateReport";
            this.generateReport.Size = new System.Drawing.Size(114, 35);
            this.generateReport.TabIndex = 4;
            this.generateReport.Text = "Generate Report";
            this.generateReport.UseVisualStyleBackColor = true;
            this.generateReport.Click += new System.EventHandler(this.generateReport_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.generateReport);
            this.Controls.Add(this.secondBalanceSheeet);
            this.Controls.Add(this.firstBalanceSheet);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox firstBalanceSheet;
        private System.Windows.Forms.TextBox secondBalanceSheeet;
        private System.Windows.Forms.Button generateReport;
    }
}

