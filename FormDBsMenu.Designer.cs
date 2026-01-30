namespace CSharpAppPlayground
{
    partial class FormDBsMenu : Form
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
            btnMySQL = new Button();
            btnPostgres = new Button();
            btnMongoDB = new Button();
            btnMySQLStatus = new Button();
            btnPostgresStatus = new Button();
            btnMongoDBStatus = new Button();
            btnDBBenchmark = new Button();
            btnMariaDB = new Button();
            btnMariaDBStatus = new Button();
            btnStatus = new Button();
            SuspendLayout();
            // 
            // btnMySQL
            // 
            btnMySQL.Location = new Point(25, 85);
            btnMySQL.Name = "btnMySQL";
            btnMySQL.Size = new Size(110, 38);
            btnMySQL.TabIndex = 0;
            btnMySQL.Text = "MySQL";
            btnMySQL.UseVisualStyleBackColor = true;
            btnMySQL.Click += btnMySQL_Click;
            // 
            // btnPostgres
            // 
            btnPostgres.Location = new Point(25, 143);
            btnPostgres.Name = "btnPostgres";
            btnPostgres.Size = new Size(110, 38);
            btnPostgres.TabIndex = 1;
            btnPostgres.Text = "PostgreSQL";
            btnPostgres.UseVisualStyleBackColor = true;
            btnPostgres.Click += btnPostgres_Click;
            // 
            // btnMongoDB
            // 
            btnMongoDB.Location = new Point(25, 256);
            btnMongoDB.Name = "btnMongoDB";
            btnMongoDB.Size = new Size(110, 38);
            btnMongoDB.TabIndex = 2;
            btnMongoDB.Text = "MongoDB";
            btnMongoDB.UseVisualStyleBackColor = true;
            btnMongoDB.Click += btnMongoDB_Click;
            // 
            // btnMySQLStatus
            // 
            btnMySQLStatus.Location = new Point(141, 85);
            btnMySQLStatus.Name = "btnMySQLStatus";
            btnMySQLStatus.Size = new Size(54, 38);
            btnMySQLStatus.TabIndex = 3;
            btnMySQLStatus.Text = "status";
            btnMySQLStatus.UseVisualStyleBackColor = true;
            btnMySQLStatus.Click += btnMySQLStatus_Click;
            // 
            // btnPostgresStatus
            // 
            btnPostgresStatus.Location = new Point(141, 143);
            btnPostgresStatus.Name = "btnPostgresStatus";
            btnPostgresStatus.Size = new Size(54, 38);
            btnPostgresStatus.TabIndex = 4;
            btnPostgresStatus.Text = "status";
            btnPostgresStatus.UseVisualStyleBackColor = true;
            btnPostgresStatus.Click += btnPostgresStatus_Click;
            // 
            // btnMongoDBStatus
            // 
            btnMongoDBStatus.Location = new Point(141, 256);
            btnMongoDBStatus.Name = "btnMongoDBStatus";
            btnMongoDBStatus.Size = new Size(54, 38);
            btnMongoDBStatus.TabIndex = 5;
            btnMongoDBStatus.Text = "status";
            btnMongoDBStatus.UseVisualStyleBackColor = true;
            btnMongoDBStatus.Click += btnMongoDBStatus_Click;
            // 
            // btnDBBenchmark
            // 
            btnDBBenchmark.Location = new Point(25, 315);
            btnDBBenchmark.Name = "btnDBBenchmark";
            btnDBBenchmark.Size = new Size(170, 35);
            btnDBBenchmark.TabIndex = 6;
            btnDBBenchmark.Text = "Benchmark";
            btnDBBenchmark.UseVisualStyleBackColor = true;
            btnDBBenchmark.Click += btnDBBenchmark_Click;
            // 
            // btnMariaDB
            // 
            btnMariaDB.Location = new Point(25, 200);
            btnMariaDB.Name = "btnMariaDB";
            btnMariaDB.Size = new Size(110, 38);
            btnMariaDB.TabIndex = 7;
            btnMariaDB.Text = "MariaDB";
            btnMariaDB.UseVisualStyleBackColor = true;
            btnMariaDB.Click += btnMariaDB_Click;
            // 
            // btnMariaDBStatus
            // 
            btnMariaDBStatus.Location = new Point(141, 200);
            btnMariaDBStatus.Name = "btnMariaDBStatus";
            btnMariaDBStatus.Size = new Size(54, 38);
            btnMariaDBStatus.TabIndex = 8;
            btnMariaDBStatus.Text = "status";
            btnMariaDBStatus.UseVisualStyleBackColor = true;
            btnMariaDBStatus.Click += btnMariaDBStatus_Click;
            // 
            // btnStatus
            // 
            btnStatus.Location = new Point(25, 26);
            btnStatus.Name = "btnStatus";
            btnStatus.Size = new Size(170, 40);
            btnStatus.TabIndex = 9;
            btnStatus.Text = "DB STATUSES";
            btnStatus.UseVisualStyleBackColor = true;
            btnStatus.Click += btnStatus_Click;
            // 
            // FormDBsMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnStatus);
            Controls.Add(btnMariaDBStatus);
            Controls.Add(btnMariaDB);
            Controls.Add(btnDBBenchmark);
            Controls.Add(btnMongoDBStatus);
            Controls.Add(btnPostgresStatus);
            Controls.Add(btnMySQLStatus);
            Controls.Add(btnMongoDB);
            Controls.Add(btnPostgres);
            Controls.Add(btnMySQL);
            Name = "FormDBsMenu";
            Text = "FormDBsMenu";
            ResumeLayout(false);
        }

        #endregion

        private Button btnMySQL;
        private Button btnPostgres;
        private Button btnMongoDB;
        private Button btnMySQLStatus;
        private Button btnPostgresStatus;
        private Button btnMongoDBStatus;
        private Button btnDBBenchmark;
        private Button btnMariaDB;
        private Button btnMariaDBStatus;
        private Button btnStatus;
    }
}