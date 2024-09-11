namespace UpdateRecordModule_SH_D
{
    partial class frmGovApprovedNumOfClass
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cboSchoolYear = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lstGovApproved = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnDelete = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.btnCopy = new DevComponents.DotNetBar.ButtonX();
            this.cboDept = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbxClassTypeU = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtDeptName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtApprovedClass = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtApprovedStu = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.cboCourseKind = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnClear = new DevComponents.DotNetBar.ButtonX();
            this.lblDataMode = new DevComponents.DotNetBar.LabelX();
            this.cboSchoolYearG = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.cbxClassType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 26);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(110, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "請選擇學年度";
            // 
            // cboSchoolYear
            // 
            this.cboSchoolYear.DisplayMember = "Text";
            this.cboSchoolYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSchoolYear.FormattingEnabled = true;
            this.cboSchoolYear.ItemHeight = 23;
            this.cboSchoolYear.Location = new System.Drawing.Point(128, 20);
            this.cboSchoolYear.Name = "cboSchoolYear";
            this.cboSchoolYear.Size = new System.Drawing.Size(121, 29);
            this.cboSchoolYear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboSchoolYear.TabIndex = 1;
            this.cboSchoolYear.SelectedIndexChanged += new System.EventHandler(this.cboSchoolYear_SelectedIndexChanged);
            // 
            // lstGovApproved
            // 
            // 
            // 
            // 
            this.lstGovApproved.Border.Class = "ListViewBorder";
            this.lstGovApproved.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lstGovApproved.CheckBoxes = true;
            this.lstGovApproved.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader9,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lstGovApproved.FullRowSelect = true;
            this.lstGovApproved.HideSelection = false;
            this.lstGovApproved.Location = new System.Drawing.Point(12, 69);
            this.lstGovApproved.Name = "lstGovApproved";
            this.lstGovApproved.Size = new System.Drawing.Size(826, 400);
            this.lstGovApproved.TabIndex = 2;
            this.lstGovApproved.UseCompatibleStateImageBehavior = false;
            this.lstGovApproved.View = System.Windows.Forms.View.Details;
            this.lstGovApproved.SelectedIndexChanged += new System.EventHandler(this.lstGovApproved_SelectedIndexChanged);
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "勾選";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "課程類型";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "科別代碼";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "科別名稱";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "上傳類別";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "核定班級數";
            this.columnHeader5.Width = 120;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "核定學生數";
            this.columnHeader6.Width = 120;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "ID";
            this.columnHeader7.Width = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDelete.Location = new System.Drawing.Point(132, 490);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "刪除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(611, 490);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCopy.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCopy.AutoSize = true;
            this.btnCopy.BackColor = System.Drawing.Color.Transparent;
            this.btnCopy.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCopy.Location = new System.Drawing.Point(323, 490);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(172, 30);
            this.btnCopy.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCopy.TabIndex = 5;
            this.btnCopy.Text = "複製年度核班人數";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // cboDept
            // 
            this.cboDept.DisplayMember = "Text";
            this.cboDept.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDept.FormattingEnabled = true;
            this.cboDept.ItemHeight = 23;
            this.cboDept.Location = new System.Drawing.Point(934, 182);
            this.cboDept.Name = "cboDept";
            this.cboDept.Size = new System.Drawing.Size(153, 29);
            this.cboDept.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboDept.TabIndex = 6;
            this.cboDept.SelectedIndexChanged += new System.EventHandler(this.cboDept_SelectedIndexChanged);
            // 
            // cbxClassTypeU
            // 
            this.cbxClassTypeU.DisplayMember = "Text";
            this.cbxClassTypeU.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxClassTypeU.FormattingEnabled = true;
            this.cbxClassTypeU.ItemHeight = 23;
            this.cbxClassTypeU.Location = new System.Drawing.Point(934, 317);
            this.cbxClassTypeU.Name = "cbxClassTypeU";
            this.cbxClassTypeU.Size = new System.Drawing.Size(153, 29);
            this.cbxClassTypeU.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxClassTypeU.TabIndex = 8;
            this.cbxClassTypeU.DropDown += new System.EventHandler(this.cbxClassTypeU_DropDown);
            this.cbxClassTypeU.SelectedIndexChanged += new System.EventHandler(this.cbxClassTypeU_SelectedIndexChanged);
            this.cbxClassTypeU.SelectedValueChanged += new System.EventHandler(this.cbxClassTypeU_SelectedIndexChanged);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(844, 184);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(78, 23);
            this.labelX2.TabIndex = 9;
            this.labelX2.Text = "科別代碼";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(844, 230);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(110, 23);
            this.labelX3.TabIndex = 10;
            this.labelX3.Text = "科別名稱";
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(844, 322);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(84, 23);
            this.labelX4.TabIndex = 11;
            this.labelX4.Text = "上傳類別";
            // 
            // txtDeptName
            // 
            // 
            // 
            // 
            this.txtDeptName.Border.Class = "TextBoxBorder";
            this.txtDeptName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDeptName.Enabled = false;
            this.txtDeptName.Location = new System.Drawing.Point(934, 227);
            this.txtDeptName.Name = "txtDeptName";
            this.txtDeptName.Size = new System.Drawing.Size(153, 29);
            this.txtDeptName.TabIndex = 12;
            // 
            // txtApprovedClass
            // 
            // 
            // 
            // 
            this.txtApprovedClass.Border.Class = "TextBoxBorder";
            this.txtApprovedClass.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtApprovedClass.Location = new System.Drawing.Point(934, 362);
            this.txtApprovedClass.Name = "txtApprovedClass";
            this.txtApprovedClass.Size = new System.Drawing.Size(153, 29);
            this.txtApprovedClass.TabIndex = 14;
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(844, 368);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(110, 23);
            this.labelX5.TabIndex = 13;
            this.labelX5.Text = "核定班級數";
            // 
            // txtApprovedStu
            // 
            // 
            // 
            // 
            this.txtApprovedStu.Border.Class = "TextBoxBorder";
            this.txtApprovedStu.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtApprovedStu.Location = new System.Drawing.Point(934, 407);
            this.txtApprovedStu.Name = "txtApprovedStu";
            this.txtApprovedStu.Size = new System.Drawing.Size(153, 29);
            this.txtApprovedStu.TabIndex = 16;
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(844, 414);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(110, 23);
            this.labelX6.TabIndex = 15;
            this.labelX6.Text = "核定學生數";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(983, 467);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(103, 38);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelX7
            // 
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.Class = "";
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(844, 138);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(78, 23);
            this.labelX7.TabIndex = 19;
            this.labelX7.Text = "課程類型";
            // 
            // cboCourseKind
            // 
            this.cboCourseKind.DisplayMember = "Text";
            this.cboCourseKind.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCourseKind.FormattingEnabled = true;
            this.cboCourseKind.ItemHeight = 23;
            this.cboCourseKind.Location = new System.Drawing.Point(934, 137);
            this.cboCourseKind.Name = "cboCourseKind";
            this.cboCourseKind.Size = new System.Drawing.Size(153, 29);
            this.cboCourseKind.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboCourseKind.TabIndex = 18;
            this.cboCourseKind.SelectedIndexChanged += new System.EventHandler(this.cboCourseKind_SelectedIndexChanged);
            // 
            // btnClear
            // 
            this.btnClear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClear.Location = new System.Drawing.Point(874, 467);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(103, 38);
            this.btnClear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClear.TabIndex = 20;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblDataMode
            // 
            this.lblDataMode.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblDataMode.BackgroundStyle.Class = "";
            this.lblDataMode.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblDataMode.Location = new System.Drawing.Point(844, 26);
            this.lblDataMode.Name = "lblDataMode";
            this.lblDataMode.Size = new System.Drawing.Size(75, 23);
            this.lblDataMode.TabIndex = 21;
            this.lblDataMode.Text = "labelX8";
            this.lblDataMode.Visible = false;
            // 
            // cboSchoolYearG
            // 
            this.cboSchoolYearG.DisplayMember = "Text";
            this.cboSchoolYearG.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSchoolYearG.FormattingEnabled = true;
            this.cboSchoolYearG.ItemHeight = 23;
            this.cboSchoolYearG.Location = new System.Drawing.Point(934, 92);
            this.cboSchoolYearG.Name = "cboSchoolYearG";
            this.cboSchoolYearG.Size = new System.Drawing.Size(153, 29);
            this.cboSchoolYearG.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboSchoolYearG.TabIndex = 22;
            // 
            // labelX8
            // 
            this.labelX8.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.Class = "";
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(844, 92);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(78, 23);
            this.labelX8.TabIndex = 23;
            this.labelX8.Text = "學年度";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "班別";
            this.columnHeader9.Width = 100;
            // 
            // labelX9
            // 
            this.labelX9.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.Class = "";
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Location = new System.Drawing.Point(846, 276);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(67, 23);
            this.labelX9.TabIndex = 25;
            this.labelX9.Text = "班別";
            // 
            // cbxClassType
            // 
            this.cbxClassType.DisplayMember = "Text";
            this.cbxClassType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxClassType.FormattingEnabled = true;
            this.cbxClassType.ItemHeight = 23;
            this.cbxClassType.Location = new System.Drawing.Point(936, 272);
            this.cbxClassType.Name = "cbxClassType";
            this.cbxClassType.Size = new System.Drawing.Size(153, 29);
            this.cbxClassType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxClassType.TabIndex = 24;
            this.cbxClassType.DropDown += new System.EventHandler(this.cbxClassType_DropDown);
            this.cbxClassType.SelectedIndexChanged += new System.EventHandler(this.cbxClassType_SelectedIndexChanged);
            this.cbxClassType.SelectedValueChanged += new System.EventHandler(this.cbxClassType_SelectedIndexChanged);
            // 
            // frmGovApprovedNumOfClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 552);
            this.Controls.Add(this.labelX9);
            this.Controls.Add(this.cbxClassType);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.cboSchoolYearG);
            this.Controls.Add(this.lblDataMode);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.cboCourseKind);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtApprovedStu);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.txtApprovedClass);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.txtDeptName);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.cbxClassTypeU);
            this.Controls.Add(this.cboDept);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lstGovApproved);
            this.Controls.Add(this.cboSchoolYear);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmGovApprovedNumOfClass";
            this.Text = "核定班級人數維護";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSchoolYear;
        private DevComponents.DotNetBar.Controls.ListViewEx lstGovApproved;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private DevComponents.DotNetBar.ButtonX btnDelete;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.ButtonX btnCopy;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDept;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxClassTypeU;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDeptName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtApprovedClass;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX txtApprovedStu;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboCourseKind;
        private DevComponents.DotNetBar.ButtonX btnClear;
        private DevComponents.DotNetBar.LabelX lblDataMode;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSchoolYearG;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxClassType;
    }
}