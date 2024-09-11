namespace UpdateRecordModule_SH_D.Batch
{
    partial class BatchUpdateRec
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
            this.cbxClassType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.dtUpdate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.btnRun = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtUpdateDesc = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cboUpdateCode = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.dtUpdate)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxClassType
            // 
            this.cbxClassType.DisplayMember = "Text";
            this.cbxClassType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxClassType.FormattingEnabled = true;
            this.cbxClassType.ItemHeight = 23;
            this.cbxClassType.Location = new System.Drawing.Point(125, 95);
            this.cbxClassType.Name = "cbxClassType";
            this.cbxClassType.Size = new System.Drawing.Size(144, 29);
            this.cbxClassType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxClassType.TabIndex = 36;
            this.cbxClassType.DropDown += new System.EventHandler(this.cbxClassType_DropDown);
            this.cbxClassType.SelectedIndexChanged += new System.EventHandler(this.cbxClassType_SelectedIndexChanged);
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
            this.labelX2.Location = new System.Drawing.Point(12, 96);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(74, 26);
            this.labelX2.TabIndex = 34;
            this.labelX2.Text = "班　　別";
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(226, 203);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 33;
            this.btnExit.Text = "取消";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
            this.dtUpdate.Location = new System.Drawing.Point(125, 138);
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
            this.dtUpdate.Size = new System.Drawing.Size(176, 29);
            this.dtUpdate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtUpdate.TabIndex = 32;
            this.dtUpdate.Value = new System.DateTime(2012, 12, 12, 0, 0, 0, 0);
            // 
            // btnRun
            // 
            this.btnRun.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.AutoSize = true;
            this.btnRun.BackColor = System.Drawing.Color.Transparent;
            this.btnRun.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRun.Location = new System.Drawing.Point(100, 203);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 30);
            this.btnRun.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRun.TabIndex = 31;
            this.btnRun.Text = "產生";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
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
            this.labelX1.Location = new System.Drawing.Point(12, 138);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(74, 26);
            this.labelX1.TabIndex = 30;
            this.labelX1.Text = "異動日期";
            // 
            // txtUpdateDesc
            // 
            // 
            // 
            // 
            this.txtUpdateDesc.Border.Class = "TextBoxBorder";
            this.txtUpdateDesc.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtUpdateDesc.Location = new System.Drawing.Point(125, 52);
            this.txtUpdateDesc.Name = "txtUpdateDesc";
            this.txtUpdateDesc.Size = new System.Drawing.Size(251, 29);
            this.txtUpdateDesc.TabIndex = 40;
            this.txtUpdateDesc.Tag = "UpdateDescription";
            // 
            // cboUpdateCode
            // 
            this.cboUpdateCode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboUpdateCode.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboUpdateCode.FormattingEnabled = true;
            this.cboUpdateCode.ItemHeight = 18;
            this.cboUpdateCode.Location = new System.Drawing.Point(125, 14);
            this.cboUpdateCode.Name = "cboUpdateCode";
            this.cboUpdateCode.Size = new System.Drawing.Size(251, 24);
            this.cboUpdateCode.TabIndex = 37;
            this.cboUpdateCode.Tag = "UpdateCode";
            this.cboUpdateCode.DropDown += new System.EventHandler(this.cboUpdateCode_DropDown);
            this.cboUpdateCode.SelectedIndexChanged += new System.EventHandler(this.cboUpdateCode_SelectedIndexChanged);
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
            this.labelX3.Location = new System.Drawing.Point(12, 12);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(107, 26);
            this.labelX3.TabIndex = 41;
            this.labelX3.Text = "異動原因代碼";
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(12, 54);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(90, 26);
            this.labelX4.TabIndex = 42;
            this.labelX4.Text = "原因及說明";
            // 
            // BatchUpdateRec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 234);
            this.Controls.Add(this.cboUpdateCode);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.txtUpdateDesc);
            this.Controls.Add(this.cbxClassType);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dtUpdate);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BatchUpdateRec";
            this.Text = "批次學籍異動";
            this.Load += new System.EventHandler(this.BatchUpdateRec_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtUpdate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxClassType;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtUpdate;
        private DevComponents.DotNetBar.ButtonX btnRun;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtUpdateDesc;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboUpdateCode;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
    }
}