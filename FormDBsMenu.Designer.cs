namespace CSharpAppPlayground
{
    partial class FormDBsMenu
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
            SuspendLayout();
            // 
            // btnMySQL
            // 
            btnMySQL.Location = new Point(22, 23);
            btnMySQL.Name = "btnMySQL";
            btnMySQL.Size = new Size(110, 38);
            btnMySQL.TabIndex = 0;
            btnMySQL.Text = "MySQL";
            btnMySQL.UseVisualStyleBackColor = true;
            btnMySQL.Click += btnMySQL_Click;
            // 
            // btnPostgres
            // 
            btnPostgres.Location = new Point(22, 81);
            btnPostgres.Name = "btnPostgres";
            btnPostgres.Size = new Size(110, 38);
            btnPostgres.TabIndex = 1;
            btnPostgres.Text = "PostgreSQL";
            btnPostgres.UseVisualStyleBackColor = true;
            btnPostgres.Click += btnPostgres_Click;
            // 
            // btnMongoDB
            // 
            btnMongoDB.Location = new Point(22, 141);
            btnMongoDB.Name = "btnMongoDB";
            btnMongoDB.Size = new Size(110, 38);
            btnMongoDB.TabIndex = 2;
            btnMongoDB.Text = "MongoDB";
            btnMongoDB.UseVisualStyleBackColor = true;
            btnMongoDB.Click += btnMongoDB_Click;
            // 
            // btnMySQLStatus
            // 
            btnMySQLStatus.Location = new Point(138, 23);
            btnMySQLStatus.Name = "btnMySQLStatus";
            btnMySQLStatus.Size = new Size(54, 38);
            btnMySQLStatus.TabIndex = 3;
            btnMySQLStatus.Text = "status";
            btnMySQLStatus.UseVisualStyleBackColor = true;
            btnMySQLStatus.Click += btnMySQLStatus_Click;
            // 
            // btnPostgresStatus
            // 
            btnPostgresStatus.Location = new Point(138, 81);
            btnPostgresStatus.Name = "btnPostgresStatus";
            btnPostgresStatus.Size = new Size(54, 38);
            btnPostgresStatus.TabIndex = 4;
            btnPostgresStatus.Text = "status";
            btnPostgresStatus.UseVisualStyleBackColor = true;
            btnPostgresStatus.Click += btnPostgresStatus_Click;
            // 
            // btnMongoDBStatus
            // 
            btnMongoDBStatus.Location = new Point(138, 141);
            btnMongoDBStatus.Name = "btnMongoDBStatus";
            btnMongoDBStatus.Size = new Size(54, 38);
            btnMongoDBStatus.TabIndex = 5;
            btnMongoDBStatus.Text = "status";
            btnMongoDBStatus.UseVisualStyleBackColor = true;
            btnMongoDBStatus.Click += btnMongoDBStatus_Click;
            // 
            // FormDBsMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}