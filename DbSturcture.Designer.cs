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
            chSelectAll = new CheckBox();
            tablesList = new ListBox();
            label2 = new Label();
            selectAllDest = new CheckBox();
            LbDestTables = new ListBox();
            BtnCopydata = new Button();
            PBAction = new ProgressBar();
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
            // tablesList
            // 
            tablesList.FormattingEnabled = true;
            tablesList.Location = new Point(62, 106);
            tablesList.Name = "tablesList";
            tablesList.SelectionMode = SelectionMode.MultiSimple;
            tablesList.Size = new Size(293, 344);
            tablesList.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(912, 20);
            label2.Name = "label2";
            label2.Size = new Size(123, 20);
            label2.TabIndex = 0;
            label2.Text = "Destanation data";
            // 
            // selectAllDest
            // 
            selectAllDest.AutoSize = true;
            selectAllDest.Checked = true;
            selectAllDest.CheckState = CheckState.Checked;
            selectAllDest.Enabled = false;
            selectAllDest.Location = new Point(912, 61);
            selectAllDest.Name = "selectAllDest";
            selectAllDest.Size = new Size(97, 24);
            selectAllDest.TabIndex = 4;
            selectAllDest.Text = "Select All ";
            selectAllDest.UseVisualStyleBackColor = true;
            selectAllDest.CheckedChanged += SelectAll_CheckedChanged;
            // 
            // LbDestTables
            // 
            LbDestTables.Enabled = false;
            LbDestTables.FormattingEnabled = true;
            LbDestTables.Location = new Point(912, 106);
            LbDestTables.Name = "LbDestTables";
            LbDestTables.SelectionMode = SelectionMode.MultiSimple;
            LbDestTables.Size = new Size(293, 344);
            LbDestTables.TabIndex = 6;
            // 
            // BtnCopydata
            // 
            BtnCopydata.Location = new Point(518, 618);
            BtnCopydata.Name = "BtnCopydata";
            BtnCopydata.Size = new Size(168, 29);
            BtnCopydata.TabIndex = 7;
            BtnCopydata.Text = "Copy Data";
            BtnCopydata.UseVisualStyleBackColor = true;
            BtnCopydata.Click += BtnCopydata_Click;
            // 
            // PBAction
            // 
            PBAction.Location = new Point(62, 525);
            PBAction.Name = "PBAction";
            PBAction.Size = new Size(1230, 29);
            PBAction.TabIndex = 8;
            PBAction.Click += PBAction_Click;
            // 
            // DbSturcture
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1380, 668);
            Controls.Add(PBAction);
            Controls.Add(BtnCopydata);
            Controls.Add(LbDestTables);
            Controls.Add(selectAllDest);
            Controls.Add(tablesList);
            Controls.Add(label2);
            Controls.Add(chSelectAll);
            Controls.Add(label1);
            Name = "DbSturcture";
            Text = "DbSturcture";
            Load += DbSturcture_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private CheckBox chSelectAll;
        private ListBox tablesList;
        private Label label2;
        private CheckBox selectAllDest;
        private ListBox LbDestTables;
        private Button BtnCopydata;
        private ProgressBar PBAction;
    }
}