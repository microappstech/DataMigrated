namespace Migratedata
{
    partial class DbSturcture
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
            label1 = new Label();
            tablesList = new CheckedListBox();
            MigrationType = new RadioButton();
            MigrationType2 = new RadioButton();
            Next = new Button();
            chSelectAll = new CheckBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(61, 20);
            label1.Name = "label1";
            label1.Size = new Size(126, 20);
            label1.TabIndex = 0;
            label1.Text = "Tables To Migrate";
            // 
            // tablesList
            // 
            tablesList.FormattingEnabled = true;
            tablesList.Location = new Point(61, 98);
            tablesList.Name = "tablesList";
            tablesList.Size = new Size(360, 400);
            tablesList.TabIndex = 1;
            tablesList.SelectedIndexChanged += checkedListBox1_SelectedIndexChanged;
            // 
            // MigrationType
            // 
            MigrationType.AutoSize = true;
            MigrationType.Location = new Point(626, 156);
            MigrationType.Name = "MigrationType";
            MigrationType.Size = new Size(96, 24);
            MigrationType.TabIndex = 2;
            MigrationType.TabStop = true;
            MigrationType.Text = "Only Data";
            MigrationType.UseVisualStyleBackColor = true;
            // 
            // MigrationType2
            // 
            MigrationType2.AutoSize = true;
            MigrationType2.Location = new Point(626, 229);
            MigrationType2.Name = "MigrationType2";
            MigrationType2.Size = new Size(156, 24);
            MigrationType2.TabIndex = 2;
            MigrationType2.TabStop = true;
            MigrationType2.Text = "Data And Structure";
            MigrationType2.UseVisualStyleBackColor = true;
            // 
            // Next
            // 
            Next.Location = new Point(626, 334);
            Next.Name = "Next";
            Next.Size = new Size(94, 29);
            Next.TabIndex = 3;
            Next.Text = "Next";
            Next.UseVisualStyleBackColor = true;
            Next.Click += Next_Click;
            // 
            // chSelectAll
            // 
            chSelectAll.AutoSize = true;
            chSelectAll.Checked = true;
            chSelectAll.CheckState = CheckState.Checked;
            chSelectAll.Location = new Point(62, 61);
            chSelectAll.Name = "chSelectAll";
            chSelectAll.Size = new Size(97, 24);
            chSelectAll.TabIndex = 4;
            chSelectAll.Text = "Select All ";
            chSelectAll.UseVisualStyleBackColor = true;
            chSelectAll.CheckedChanged += SelectAll_CheckedChanged;
            // 
            // DbSturcture
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1380, 668);
            Controls.Add(chSelectAll);
            Controls.Add(Next);
            Controls.Add(MigrationType2);
            Controls.Add(MigrationType);
            Controls.Add(tablesList);
            Controls.Add(label1);
            Name = "DbSturcture";
            Text = "DbSturcture";
            Load += DbSturcture_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private CheckedListBox tablesList;
        private RadioButton MigrationType;
        private RadioButton MigrationType2;
        private Button Next;
        private CheckBox chSelectAll;
    }
}