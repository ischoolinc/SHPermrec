namespace UpdateRecordModule_SH_D.Batch
{
    partial class BatchGraduateRecForm
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
            this.btnRun = new DevComponents.DotNetBar.ButtonX();
            this.dtUpdate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.cbxClassType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.iptSchoolYear = new DevComponents.Editors.IntegerInput();
            ((System.ComponentModel.ISupportInitialize)(this.dtUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iptSchoolYear)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(16, 20);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(60, 21);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "異動日期";
            // 
            // btnRun
            // 
            this.btnRun.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.AutoSize = true;
            this.btnRun.BackColor = System.Drawing.Color.Transparent;
            this.btnRun.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRun.Location = new System.Drawing.Point(58, 142);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 25);
            this.btnRun.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRun.TabIndex = 1;
            this.btnRun.Text = "產生";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // dtUpdate
            // 
            this.dtUpdate.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.dtUpdate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtUpdate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtUpdate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtUpdate.ButtonDropDown.Visible = true;
            this.dtUpdate.IsPopupCalendarOpen = false;
            this.dtUpdate.Location = new System.Drawing.Point(82, 18);
            this.dtUpdate.MaxDate = new System.DateTime(2300, 12, 31, 0, 0, 0, 0);
            this.dtUpdate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // 
            // 
            this.dtUpdate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtUpdate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtUpdate.MonthCalendar.BackgroundStyle.Class = "";
            this.dtUpdate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtUpdate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtUpdate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtUpdate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtUpdate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtUpdate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtUpdate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtUpdate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtUpdate.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtUpdate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtUpdate.MonthCalendar.DayNames = new string[] {
        "日",
        "一",
        "二",
        "三",
        "四",
        "五",
        "六"};
            this.dtUpdate.MonthCalendar.DisplayMonth = new System.DateTime(2012, 12, 1, 0, 0, 0, 0);
            this.dtUpdate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtUpdate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtUpdate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtUpdate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtUpdate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtUpdate.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtUpdate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtUpdate.MonthCalendar.TodayButtonVisible = true;
            this.dtUpdate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtUpdate.Name = "dtUpdate";
            this.dtUpdate.Size = new System.Drawing.Size(134, 25);
            this.dtUpdate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtUpdate.TabIndex = 2;
            this.dtUpdate.Value = new System.DateTime(2012, 12, 12, 0, 0, 0, 0);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(141, 142);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "取消";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(16, 57);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(60, 21);
            this.labelX2.TabIndex = 4;
            this.labelX2.Text = "班　　別";
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelX3.Location = new System.Drawing.Point(16, 94);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(104, 21);
            this.labelX3.TabIndex = 5;
            this.labelX3.Text = "應 畢 業 學 年 度";
            // 
            // cbxClassType
            // 
            this.cbxClassType.DisplayMember = "Text";
            this.cbxClassType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxClassType.FormattingEnabled = true;
            this.cbxClassType.ItemHeight = 19;
            this.cbxClassType.Location = new System.Drawing.Point(82, 55);
            this.cbxClassType.Name = "cbxClassType";
            this.cbxClassType.Size = new System.Drawing.Size(134, 25);
            this.cbxClassType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxClassType.TabIndex = 6;
            this.cbxClassType.DropDown += new System.EventHandler(this.cbxClassType_DropDown);
            this.cbxClassType.SelectedIndexChanged += new System.EventHandler(this.cbxClassType_SelectedIndexChanged);
            // 
            // iptSchoolYear
            // 
            this.iptSchoolYear.AllowEmptyState = false;
            this.iptSchoolYear.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.iptSchoolYear.BackgroundStyle.Class = "DateTimeInputBackground";
            this.iptSchoolYear.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.iptSchoolYear.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.iptSchoolYear.Location = new System.Drawing.Point(126, 92);
            this.iptSchoolYear.MaxValue = 300;
            this.iptSchoolYear.MinValue = 70;
            this.iptSchoolYear.Name = "iptSchoolYear";
            this.iptSchoolYear.ShowUpDown = true;
            this.iptSchoolYear.Size = new System.Drawing.Size(90, 25);
            this.iptSchoolYear.TabIndex = 29;
            this.iptSchoolYear.Value = 110;
            // 
            // BatchGraduateRecForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 185);
            this.Controls.Add(this.iptSchoolYear);
            this.Controls.Add(this.cbxClassType);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dtUpdate);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Name = "BatchGraduateRecForm";
            this.Text = "產生批次畢業異動";
            this.Load += new System.EventHandler(this.BatchGraduateRecForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iptSchoolYear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnRun;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtUpdate;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxClassType;
        private DevComponents.Editors.IntegerInput iptSchoolYear;
    }
}