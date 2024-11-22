
namespace Leave_School_Notification
{
    partial class 放棄學籍通單
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
            this.chkBulkMail_S = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkBulkMail_R = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnPrint = new DevComponents.DotNetBar.ButtonX();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.dateDeadline = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateDeadline)).BeginInit();
            this.SuspendLayout();
            // 
            // chkBulkMail_S
            // 
            this.chkBulkMail_S.AutoSize = true;
            this.chkBulkMail_S.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkBulkMail_S.BackgroundStyle.Class = "";
            this.chkBulkMail_S.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkBulkMail_S.Location = new System.Drawing.Point(229, 120);
            this.chkBulkMail_S.Name = "chkBulkMail_S";
            this.chkBulkMail_S.Size = new System.Drawing.Size(174, 21);
            this.chkBulkMail_S.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkBulkMail_S.TabIndex = 43;
            this.chkBulkMail_S.Text = "列印大宗郵件掛號存根聯";
            this.chkBulkMail_S.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // chkBulkMail_R
            // 
            this.chkBulkMail_R.AutoSize = true;
            this.chkBulkMail_R.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkBulkMail_R.BackgroundStyle.Class = "";
            this.chkBulkMail_R.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkBulkMail_R.Location = new System.Drawing.Point(12, 120);
            this.chkBulkMail_R.Name = "chkBulkMail_R";
            this.chkBulkMail_R.Size = new System.Drawing.Size(174, 21);
            this.chkBulkMail_R.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkBulkMail_R.TabIndex = 42;
            this.chkBulkMail_R.Text = "列印大宗郵件掛號收執聯";
            this.chkBulkMail_R.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Location = new System.Drawing.Point(327, 156);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 41;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCalcel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrint.BackColor = System.Drawing.Color.Transparent;
            this.btnPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrint.Location = new System.Drawing.Point(241, 156);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrint.TabIndex = 40;
            this.btnPrint.Text = "列印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.Location = new System.Drawing.Point(35, 156);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(60, 17);
            this.linkLabel1.TabIndex = 39;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "範本設定";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.labelX5);
            this.groupPanel1.Controls.Add(this.dateDeadline);
            this.groupPanel1.Location = new System.Drawing.Point(12, 25);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(391, 76);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.Class = "";
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.Class = "";
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.Class = "";
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 38;
            this.groupPanel1.Text = "列印變數設定";
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(3, 12);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(68, 23);
            this.labelX5.TabIndex = 31;
            this.labelX5.Text = "辦理期限";
            // 
            // dateDeadline
            // 
            this.dateDeadline.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.dateDeadline.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dateDeadline.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateDeadline.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dateDeadline.ButtonDropDown.Visible = true;
            this.dateDeadline.IsPopupCalendarOpen = false;
            this.dateDeadline.Location = new System.Drawing.Point(77, 12);
            this.dateDeadline.MinDate = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
            // 
            // 
            // 
            this.dateDeadline.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateDeadline.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dateDeadline.MonthCalendar.BackgroundStyle.Class = "";
            this.dateDeadline.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateDeadline.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dateDeadline.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dateDeadline.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dateDeadline.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dateDeadline.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dateDeadline.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dateDeadline.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dateDeadline.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dateDeadline.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateDeadline.MonthCalendar.DisplayMonth = new System.DateTime(2023, 3, 1, 0, 0, 0, 0);
            this.dateDeadline.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dateDeadline.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateDeadline.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dateDeadline.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dateDeadline.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dateDeadline.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dateDeadline.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateDeadline.MonthCalendar.TodayButtonVisible = true;
            this.dateDeadline.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dateDeadline.Name = "dateDeadline";
            this.dateDeadline.Size = new System.Drawing.Size(200, 25);
            this.dateDeadline.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dateDeadline.TabIndex = 30;
            // 
            // 放棄學籍通單
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 197);
            this.Controls.Add(this.chkBulkMail_S);
            this.Controls.Add(this.chkBulkMail_R);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.groupPanel1);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(446, 236);
            this.Name = "放棄學籍通單";
            this.Text = "放棄學籍通單";
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateDeadline)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.CheckBoxX chkBulkMail_S;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkBulkMail_R;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnPrint;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dateDeadline;
    }
}