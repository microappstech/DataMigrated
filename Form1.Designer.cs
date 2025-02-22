namespace Migratedata
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SeverSource = new TextBox();
            DbNameSrc = new ComboBox();
            BtnTestSource = new Button();
            serverDest = new TextBox();
            DbNameDest = new ComboBox();
            BtnDest = new Button();
            label1 = new Label();
            label2 = new Label();
            ServerTyppSrc = new ComboBox();
            ServerTypeDest = new ComboBox();
            btnMigration = new Button();
            SuspendLayout();
            // 
            // SeverSource
            // 
            SeverSource.Location = new Point(118, 138);
            SeverSource.Name = "SeverSource";
            SeverSource.Size = new Size(241, 27);
            SeverSource.TabIndex = 0;
            SeverSource.TextChanged += SeverSource_TextChanged;
            // 
            // DbNameSrc
            // 
            DbNameSrc.FormattingEnabled = true;
            DbNameSrc.Location = new Point(118, 198);
            DbNameSrc.Name = "DbNameSrc";
            DbNameSrc.Size = new Size(241, 28);
            DbNameSrc.TabIndex = 1;
            DbNameSrc.SelectedIndexChanged += DbNameSrc_SelectedIndexChanged_1;
            // 
            // BtnTestSource
            // 
            BtnTestSource.Location = new Point(121, 268);
            BtnTestSource.Name = "BtnTestSource";
            BtnTestSource.Size = new Size(238, 29);
            BtnTestSource.TabIndex = 2;
            BtnTestSource.Text = "Connect";
            BtnTestSource.UseVisualStyleBackColor = true;
            BtnTestSource.Click += BtnTestSource_Click;
            // 
            // serverDest
            // 
            serverDest.Location = new Point(914, 137);
            serverDest.Name = "serverDest";
            serverDest.Size = new Size(241, 27);
            serverDest.TabIndex = 0;
            serverDest.TextChanged += serverDest_TextChanged;
            // 
            // DbNameDest
            // 
            DbNameDest.FormattingEnabled = true;
            DbNameDest.Location = new Point(914, 194);
            DbNameDest.Name = "DbNameDest";
            DbNameDest.Size = new Size(241, 28);
            DbNameDest.TabIndex = 1;
            DbNameDest.SelectedIndexChanged += DbNameDest_SelectedIndexChanged;
            // 
            // BtnDest
            // 
            BtnDest.Location = new Point(917, 268);
            BtnDest.Name = "BtnDest";
            BtnDest.Size = new Size(238, 29);
            BtnDest.TabIndex = 2;
            BtnDest.Text = "Connect";
            BtnDest.UseVisualStyleBackColor = true;
            BtnDest.Click += BtnDest_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(200, 35);
            label1.Name = "label1";
            label1.Size = new Size(54, 20);
            label1.TabIndex = 3;
            label1.Text = "Source";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(955, 35);
            label2.Name = "label2";
            label2.Size = new Size(89, 20);
            label2.TabIndex = 3;
            label2.Text = "Destinataire";
            // 
            // ServerTyppSrc
            // 
            ServerTyppSrc.FormattingEnabled = true;
            ServerTyppSrc.Location = new Point(117, 81);
            ServerTyppSrc.Name = "ServerTyppSrc";
            ServerTyppSrc.Size = new Size(242, 28);
            ServerTyppSrc.TabIndex = 4;
            ServerTyppSrc.SelectedIndexChanged += ServerTyppSrc_SelectedIndexChanged;
            ServerTyppSrc.ValueMemberChanged += ServerTyppSrc_ValueMemberChanged;
            // 
            // ServerTypeDest
            // 
            ServerTypeDest.FormattingEnabled = true;
            ServerTypeDest.Location = new Point(914, 81);
            ServerTypeDest.Name = "ServerTypeDest";
            ServerTypeDest.Size = new Size(242, 28);
            ServerTypeDest.TabIndex = 4;
            ServerTypeDest.SelectedIndexChanged += ServerTypeDest_SelectedIndexChanged;
            // 
            // btnMigration
            // 
            btnMigration.Location = new Point(423, 413);
            btnMigration.Name = "btnMigration";
            btnMigration.Size = new Size(391, 29);
            btnMigration.TabIndex = 5;
            btnMigration.Text = "Start migration";
            btnMigration.UseVisualStyleBackColor = true;
            btnMigration.Click += btnMigration_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1327, 618);
            Controls.Add(btnMigration);
            Controls.Add(ServerTypeDest);
            Controls.Add(ServerTyppSrc);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(BtnDest);
            Controls.Add(DbNameDest);
            Controls.Add(BtnTestSource);
            Controls.Add(serverDest);
            Controls.Add(DbNameSrc);
            Controls.Add(SeverSource);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox SeverSource;
        private ComboBox DbNameSrc;
        private Button BtnTestSource;
        private TextBox serverDest;
        private ComboBox DbNameDest;
        private Button BtnDest;
        private Label label1;
        private Label label2;
        private ComboBox ServerTyppSrc;
        private ComboBox ServerTypeDest;
        private Button btnMigration;
    }
}
