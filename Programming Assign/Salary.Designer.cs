
namespace Programming_Assign
{
    partial class fSalary
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
            this.txtLv = new System.Windows.Forms.TextBox();
            this.txtEmp = new System.Windows.Forms.Label();
            this.txtEmpID = new System.Windows.Forms.TextBox();
            this.txtOT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddLv = new System.Windows.Forms.Button();
            this.btnMonthlySal = new System.Windows.Forms.Button();
            this.dataG2 = new System.Windows.Forms.DataGridView();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnViewReport = new System.Windows.Forms.Button();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.txtEnd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataG2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Applied Leaves";
            // 
            // txtLv
            // 
            this.txtLv.Location = new System.Drawing.Point(192, 47);
            this.txtLv.Name = "txtLv";
            this.txtLv.Size = new System.Drawing.Size(78, 20);
            this.txtLv.TabIndex = 1;
            // 
            // txtEmp
            // 
            this.txtEmp.AutoSize = true;
            this.txtEmp.Location = new System.Drawing.Point(94, 24);
            this.txtEmp.Name = "txtEmp";
            this.txtEmp.Size = new System.Drawing.Size(67, 13);
            this.txtEmp.TabIndex = 2;
            this.txtEmp.Text = "Employee ID";
            // 
            // txtEmpID
            // 
            this.txtEmpID.Location = new System.Drawing.Point(192, 21);
            this.txtEmpID.Name = "txtEmpID";
            this.txtEmpID.Size = new System.Drawing.Size(78, 20);
            this.txtEmpID.TabIndex = 3;
            // 
            // txtOT
            // 
            this.txtOT.Location = new System.Drawing.Point(192, 73);
            this.txtOT.Name = "txtOT";
            this.txtOT.Size = new System.Drawing.Size(78, 20);
            this.txtOT.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Overtime Hours";
            // 
            // btnAddLv
            // 
            this.btnAddLv.Location = new System.Drawing.Point(8, 19);
            this.btnAddLv.Name = "btnAddLv";
            this.btnAddLv.Size = new System.Drawing.Size(75, 23);
            this.btnAddLv.TabIndex = 6;
            this.btnAddLv.Text = "Add";
            this.btnAddLv.UseVisualStyleBackColor = true;
            this.btnAddLv.Click += new System.EventHandler(this.btnAddLv_Click);
            // 
            // btnMonthlySal
            // 
            this.btnMonthlySal.Location = new System.Drawing.Point(6, 35);
            this.btnMonthlySal.Name = "btnMonthlySal";
            this.btnMonthlySal.Size = new System.Drawing.Size(75, 23);
            this.btnMonthlySal.TabIndex = 7;
            this.btnMonthlySal.Text = "Monthly Sal";
            this.btnMonthlySal.UseVisualStyleBackColor = true;
            this.btnMonthlySal.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataG2
            // 
            this.dataG2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataG2.Location = new System.Drawing.Point(408, 12);
            this.dataG2.Name = "dataG2";
            this.dataG2.Size = new System.Drawing.Size(380, 384);
            this.dataG2.TabIndex = 8;
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(6, 73);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 23);
            this.btnReport.TabIndex = 10;
            this.btnReport.Text = "Generate";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnViewReport
            // 
            this.btnViewReport.Location = new System.Drawing.Point(6, 31);
            this.btnViewReport.Name = "btnViewReport";
            this.btnViewReport.Size = new System.Drawing.Size(75, 23);
            this.btnViewReport.TabIndex = 11;
            this.btnViewReport.Text = "View Report";
            this.btnViewReport.UseVisualStyleBackColor = true;
            this.btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
            // 
            // txtStart
            // 
            this.txtStart.Location = new System.Drawing.Point(190, 31);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(101, 20);
            this.txtStart.TabIndex = 12;
            // 
            // txtEnd
            // 
            this.txtEnd.Location = new System.Drawing.Point(190, 57);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(101, 20);
            this.txtEnd.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(92, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "End Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(92, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Start Date";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(297, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "(MM-DD-YYYY)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(297, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "(MM-DD-YYYY)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnMonthlySal);
            this.groupBox1.Controls.Add(this.btnReport);
            this.groupBox1.Location = new System.Drawing.Point(12, 145);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 124);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Salary Calculation and Reports";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnViewReport);
            this.groupBox2.Controls.Add(this.txtStart);
            this.groupBox2.Controls.Add(this.txtEnd);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(18, 275);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(384, 124);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Screen Report";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnAddLv);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtLv);
            this.groupBox3.Controls.Add(this.txtEmp);
            this.groupBox3.Controls.Add(this.txtEmpID);
            this.groupBox3.Controls.Add(this.txtOT);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(10, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(321, 124);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Redeemed Leave and OT";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(713, 415);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // fSalary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataG2);
            this.Name = "fSalary";
            this.Text = "Salary";
            this.Load += new System.EventHandler(this.fSalary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataG2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLv;
        private System.Windows.Forms.Label txtEmp;
        private System.Windows.Forms.TextBox txtEmpID;
        private System.Windows.Forms.TextBox txtOT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddLv;
        private System.Windows.Forms.Button btnMonthlySal;
        private System.Windows.Forms.DataGridView dataG2;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnViewReport;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.TextBox txtEnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
    }
}