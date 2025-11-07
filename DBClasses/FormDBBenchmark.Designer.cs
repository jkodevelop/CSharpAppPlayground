namespace CSharpAppPlayground.DBClasses
{
    partial class FormDBBenchmark
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
            btnBenchmarkInserts = new Button();
            btnResetTables = new Button();
            numAmount = new NumericUpDown();
            label1 = new Label();
            whichRepoDBSelect = new ComboBox();
            btnBenchmarkInsertsFast = new Button();
            ((System.ComponentModel.ISupportInitialize)numAmount).BeginInit();
            SuspendLayout();
            // 
            // btnBenchmarkInserts
            // 
            btnBenchmarkInserts.Location = new Point(12, 101);
            btnBenchmarkInserts.Name = "btnBenchmarkInserts";
            btnBenchmarkInserts.Size = new Size(146, 40);
            btnBenchmarkInserts.TabIndex = 0;
            btnBenchmarkInserts.Text = "Benchmark Inserts";
            btnBenchmarkInserts.UseVisualStyleBackColor = true;
            btnBenchmarkInserts.Click += btnBenchmarkInserts_Click;
            // 
            // btnResetTables
            // 
            btnResetTables.Location = new Point(12, 415);
            btnResetTables.Name = "btnResetTables";
            btnResetTables.Size = new Size(146, 23);
            btnResetTables.TabIndex = 1;
            btnResetTables.Text = "Reset Tables";
            btnResetTables.UseVisualStyleBackColor = true;
            btnResetTables.Click += btnResetTables_Click;
            // 
            // numAmount
            // 
            numAmount.Location = new Point(12, 34);
            numAmount.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            numAmount.Name = "numAmount";
            numAmount.Size = new Size(146, 23);
            numAmount.TabIndex = 3;
            numAmount.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 16);
            label1.Name = "label1";
            label1.Size = new Size(97, 15);
            label1.TabIndex = 4;
            label1.Text = "Amount to Insert";
            // 
            // whichRepoDBSelect
            // 
            whichRepoDBSelect.FormattingEnabled = true;
            whichRepoDBSelect.Location = new Point(12, 71);
            whichRepoDBSelect.Name = "whichRepoDBSelect";
            whichRepoDBSelect.Size = new Size(146, 23);
            whichRepoDBSelect.TabIndex = 6;
            // 
            // btnBenchmarkInsertsFast
            // 
            btnBenchmarkInsertsFast.Location = new Point(12, 147);
            btnBenchmarkInsertsFast.Name = "btnBenchmarkInsertsFast";
            btnBenchmarkInsertsFast.Size = new Size(146, 23);
            btnBenchmarkInsertsFast.TabIndex = 7;
            btnBenchmarkInsertsFast.Text = "Fastest Inserts Compare";
            btnBenchmarkInsertsFast.UseVisualStyleBackColor = true;
            btnBenchmarkInsertsFast.Click += btnBenchmarkInsertsFast_Click;
            // 
            // FormDBBenchmark
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnBenchmarkInsertsFast);
            Controls.Add(whichRepoDBSelect);
            Controls.Add(label1);
            Controls.Add(numAmount);
            Controls.Add(btnResetTables);
            Controls.Add(btnBenchmarkInserts);
            Name = "FormDBBenchmark";
            Text = "FormDBBenchmark";
            Controls.SetChildIndex(btnBenchmarkInserts, 0);
            Controls.SetChildIndex(btnResetTables, 0);
            Controls.SetChildIndex(numAmount, 0);
            Controls.SetChildIndex(label1, 0);
            Controls.SetChildIndex(whichRepoDBSelect, 0);
            Controls.SetChildIndex(btnBenchmarkInsertsFast, 0);
            ((System.ComponentModel.ISupportInitialize)numAmount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBenchmarkInserts;
        private Button btnResetTables;
        private TextBox textboxAmount;
        private NumericUpDown numAmount;
        private Label label1;
        private ComboBox whichRepoDBSelect;
        private Button btnBenchmarkInsertsFast;
    }
}