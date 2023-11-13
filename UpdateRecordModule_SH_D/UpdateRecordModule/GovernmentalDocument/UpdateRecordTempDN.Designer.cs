namespace UpdateRecordModule_SH_D.GovernmentalDocument
{
    partial class UpdateRecordTempDN
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該公開 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTempNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.dtTempDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTempDesc = new DevComponents.DotNetBar.Controls.TextBoxX();
            ((System.ComponentModel.ISupportInitialize)(this.dtTempDate)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "臨編日期";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(11, 73);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "臨編字號";
            // 
            // txtTempNumber
            // 
            // 
            // 
            // 
            this.txtTempNumber.Border.Class = "TextBoxBorder";
            this.txtTempNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTempNumber.Location = new System.Drawing.Point(79, 70);
            this.txtTempNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtTempNumber.MaxLength = 0;
            this.txtTempNumber.Name = "txtTempNumber";
            this.txtTempNumber.Size = new System.Drawing.Size(129, 25);
            this.txtTempNumber.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.Location = new System.Drawing.Point(89, 107);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(56, 25);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "確定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(153, 107);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(56, 25);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dtTempDate
            // 
            this.dtTempDate.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.dtTempDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtTempDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtTempDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtTempDate.ButtonDropDown.Visible = true;
            this.dtTempDate.DefaultInputValues = false;
            this.dtTempDate.IsPopupCalendarOpen = false;
            this.dtTempDate.Location = new System.Drawing.Point(79, 7);
            // 
            // 
            // 
            this.dtTempDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtTempDate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtTempDate.MonthCalendar.BackgroundStyle.Class = "";
            this.dtTempDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtTempDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtTempDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtTempDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtTempDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtTempDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtTempDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtTempDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtTempDate.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtTempDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtTempDate.MonthCalendar.DayNames = new string[] {
        "日",
        "一",
        "二",
        "三",
        "四",
        "五",
        "六"};
            this.dtTempDate.MonthCalendar.DisplayMonth = new System.DateTime(2010, 11, 1, 0, 0, 0, 0);
            this.dtTempDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtTempDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtTempDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtTempDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtTempDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtTempDate.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtTempDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtTempDate.MonthCalendar.TodayButtonVisible = true;
            this.dtTempDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtTempDate.Name = "dtTempDate";
            this.dtTempDate.Size = new System.Drawing.Size(128, 25);
            this.dtTempDate.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(11, 45);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "臨編學統";
            // 
            // txtTempDesc
            // 
            // 
            // 
            // 
            this.txtTempDesc.Border.Class = "TextBoxBorder";
            this.txtTempDesc.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTempDesc.Location = new System.Drawing.Point(79, 42);
            this.txtTempDesc.Margin = new System.Windows.Forms.Padding(4);
            this.txtTempDesc.MaxLength = 0;
            this.txtTempDesc.Name = "txtTempDesc";
            this.txtTempDesc.Size = new System.Drawing.Size(129, 25);
            this.txtTempDesc.TabIndex = 3;
            // 
            // UpdateRecordTempDN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 145);
            this.Controls.Add(this.dtTempDate);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtTempDesc);
            this.Controls.Add(this.txtTempNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UpdateRecordTempDN";
            this.Text = "登錄臨編文號";
            this.Load += new System.EventHandler(this.UpdateRecordTempDN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtTempDate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTempNumber;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtTempDate;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTempDesc;
    }
}