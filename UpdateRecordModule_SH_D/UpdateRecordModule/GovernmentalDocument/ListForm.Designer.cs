﻿namespace UpdateRecordModule_SH_D.GovernmentalDocument
{
    partial class ListForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListForm));
            this.cboSchoolYear = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnInsert = new DevComponents.DotNetBar.ButtonX();
            this.btnDelete = new DevComponents.DotNetBar.ButtonX();
            this.btnAD = new DevComponents.DotNetBar.ButtonX();
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.itemPanel2 = new DevComponents.DotNetBar.ItemPanel();
            this.listView = new SmartSchool.Common.ListViewEX();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblADName3 = new DevComponents.DotNetBar.LabelItem();
            this.controlContainerItem1 = new DevComponents.DotNetBar.ControlContainerItem();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.btnEReport1 = new DevComponents.DotNetBar.ButtonX();
            this.lblADName2 = new System.Windows.Forms.Label();
            this.tabItem2 = new DevComponents.DotNetBar.TabItem(this.components);
            this.pnlReport = new DevComponents.DotNetBar.PanelEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.progressBarX1 = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.itemPanel1 = new DevComponents.DotNetBar.ItemPanel();
            this.lblADCounter = new DevComponents.DotNetBar.LabelItem();
            this.lblListContent = new DevComponents.DotNetBar.LabelItem();
            this.lblADInfo = new DevComponents.DotNetBar.LabelItem();
            this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
            this.lblAD = new DevComponents.DotNetBar.LabelItem();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnModifyCover = new DevComponents.DotNetBar.ButtonX();
            this.btnEReport2 = new DevComponents.DotNetBar.ButtonX();
            this.lblADName1 = new System.Windows.Forms.Label();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.cciADDate = new DevComponents.DotNetBar.ControlContainerItem();
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.backPanel = new DevComponents.DotNetBar.PanelEx();
            this.itemPanelName = new DevComponents.DotNetBar.ItemPanel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnADTempDN = new DevComponents.DotNetBar.ButtonX();
            this.lblTempADInfo = new DevComponents.DotNetBar.LabelItem();
            this.lblTempAD = new DevComponents.DotNetBar.LabelItem();
            this.tabControlPanel2.SuspendLayout();
            this.itemPanel2.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.pnlReport.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.backPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboSchoolYear
            // 
            this.cboSchoolYear.DisplayMember = "Text";
            this.cboSchoolYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSchoolYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSchoolYear.FormattingEnabled = true;
            this.cboSchoolYear.ItemHeight = 19;
            this.cboSchoolYear.Location = new System.Drawing.Point(13, 14);
            this.cboSchoolYear.Name = "cboSchoolYear";
            this.cboSchoolYear.Size = new System.Drawing.Size(191, 25);
            this.cboSchoolYear.TabIndex = 0;
            this.cboSchoolYear.SelectedIndexChanged += new System.EventHandler(this.cboSchoolYear_SelectedIndexChanged);
            // 
            // btnInsert
            // 
            this.btnInsert.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnInsert.BackColor = System.Drawing.Color.Transparent;
            this.btnInsert.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnInsert.Location = new System.Drawing.Point(13, 533);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(58, 24);
            this.btnInsert.TabIndex = 2;
            this.btnInsert.Text = "新增";
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDelete.Location = new System.Drawing.Point(82, 533);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(58, 24);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "刪除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAD
            // 
            this.btnAD.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAD.BackColor = System.Drawing.Color.Transparent;
            this.btnAD.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAD.Location = new System.Drawing.Point(13, 563);
            this.btnAD.Name = "btnAD";
            this.btnAD.Size = new System.Drawing.Size(63, 24);
            this.btnAD.TabIndex = 4;
            this.btnAD.Text = "登錄文號";
            this.btnAD.Click += new System.EventHandler(this.btnAD_Click);
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this.itemPanel2);
            this.tabControlPanel2.Controls.Add(this.panelEx2);
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 28);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(614, 513);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(188)))), ((int)(((byte)(227)))));
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 2;
            this.tabControlPanel2.TabItem = this.tabItem2;
            // 
            // itemPanel2
            // 
            // 
            // 
            // 
            this.itemPanel2.BackgroundStyle.BackColor = System.Drawing.Color.White;
            this.itemPanel2.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.itemPanel2.BackgroundStyle.BorderBottomWidth = 1;
            this.itemPanel2.BackgroundStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.itemPanel2.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.itemPanel2.BackgroundStyle.BorderLeftWidth = 1;
            this.itemPanel2.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.itemPanel2.BackgroundStyle.BorderRightWidth = 1;
            this.itemPanel2.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.itemPanel2.BackgroundStyle.BorderTopWidth = 1;
            this.itemPanel2.BackgroundStyle.Class = "";
            this.itemPanel2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemPanel2.BackgroundStyle.PaddingBottom = 1;
            this.itemPanel2.BackgroundStyle.PaddingLeft = 1;
            this.itemPanel2.BackgroundStyle.PaddingRight = 1;
            this.itemPanel2.BackgroundStyle.PaddingTop = 1;
            this.itemPanel2.ContainerControlProcessDialogKey = true;
            this.itemPanel2.Controls.Add(this.listView);
            this.itemPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemPanel2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblADName3,
            this.controlContainerItem1});
            this.itemPanel2.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemPanel2.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.itemPanel2.Location = new System.Drawing.Point(1, 43);
            this.itemPanel2.Name = "itemPanel2";
            this.itemPanel2.Size = new System.Drawing.Size(612, 469);
            this.itemPanel2.TabIndex = 2;
            this.itemPanel2.Text = "itemPanel2";
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.listView.Border.Class = "ListViewBorder";
            this.listView.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.listView.FullRowSelect = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(3, 25);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(549, 352);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "學號";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "姓名";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "身分證號";
            this.columnHeader3.Width = 101;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "性別";
            this.columnHeader4.Width = 74;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "出生日期";
            this.columnHeader5.Width = 71;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "畢業國中";
            this.columnHeader6.Width = 67;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "入學資格";
            this.columnHeader7.Width = 63;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "備註";
            // 
            // lblADName3
            // 
            this.lblADName3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblADName3.Name = "lblADName3";
            // 
            // controlContainerItem1
            // 
            this.controlContainerItem1.AllowItemResize = false;
            this.controlContainerItem1.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem1.Name = "controlContainerItem1";
            this.controlContainerItem1.Text = "　";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx2.Controls.Add(this.btnEReport1);
            this.panelEx2.Controls.Add(this.lblADName2);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(1, 1);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(612, 42);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // btnEReport1
            // 
            this.btnEReport1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnEReport1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEReport1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnEReport1.Location = new System.Drawing.Point(473, 11);
            this.btnEReport1.Name = "btnEReport1";
            this.btnEReport1.Size = new System.Drawing.Size(97, 24);
            this.btnEReport1.TabIndex = 6;
            this.btnEReport1.Text = "預覽報表";
            this.btnEReport1.Click += new System.EventHandler(this.btnEReport2_Click);
            // 
            // lblADName2
            // 
            this.lblADName2.Location = new System.Drawing.Point(18, 11);
            this.lblADName2.Name = "lblADName2";
            this.lblADName2.Size = new System.Drawing.Size(480, 17);
            this.lblADName2.TabIndex = 0;
            // 
            // tabItem2
            // 
            this.tabItem2.AttachedControl = this.tabControlPanel2;
            this.tabItem2.Name = "tabItem2";
            this.tabItem2.Text = "名冊內容";
            // 
            // pnlReport
            // 
            this.pnlReport.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlReport.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.pnlReport.Controls.Add(this.labelX1);
            this.pnlReport.Controls.Add(this.progressBarX1);
            this.pnlReport.Location = new System.Drawing.Point(217, 258);
            this.pnlReport.Name = "pnlReport";
            this.pnlReport.Size = new System.Drawing.Size(469, 88);
            this.pnlReport.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlReport.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlReport.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlReport.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlReport.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlReport.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlReport.Style.GradientAngle = 90;
            this.pnlReport.TabIndex = 6;
            this.pnlReport.Visible = false;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(20, 9);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(88, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "報表產生中...";
            // 
            // progressBarX1
            // 
            // 
            // 
            // 
            this.progressBarX1.BackgroundStyle.Class = "";
            this.progressBarX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.progressBarX1.Location = new System.Drawing.Point(20, 38);
            this.progressBarX1.Name = "progressBarX1";
            this.progressBarX1.Size = new System.Drawing.Size(440, 23);
            this.progressBarX1.TabIndex = 0;
            this.progressBarX1.Text = "progressBarX1";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.itemPanel1);
            this.tabControlPanel1.Controls.Add(this.panelEx1);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 28);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(614, 513);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(188)))), ((int)(((byte)(227)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabItem1;
            // 
            // itemPanel1
            // 
            // 
            // 
            // 
            this.itemPanel1.BackgroundStyle.BackColor = System.Drawing.Color.White;
            this.itemPanel1.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.itemPanel1.BackgroundStyle.BorderBottomWidth = 1;
            this.itemPanel1.BackgroundStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.itemPanel1.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.itemPanel1.BackgroundStyle.BorderLeftWidth = 1;
            this.itemPanel1.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.itemPanel1.BackgroundStyle.BorderRightWidth = 1;
            this.itemPanel1.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.itemPanel1.BackgroundStyle.BorderTopWidth = 1;
            this.itemPanel1.BackgroundStyle.Class = "";
            this.itemPanel1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemPanel1.BackgroundStyle.PaddingBottom = 1;
            this.itemPanel1.BackgroundStyle.PaddingLeft = 1;
            this.itemPanel1.BackgroundStyle.PaddingRight = 1;
            this.itemPanel1.BackgroundStyle.PaddingTop = 1;
            this.itemPanel1.ContainerControlProcessDialogKey = true;
            this.itemPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemPanel1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblADCounter,
            this.lblListContent,
            this.lblADInfo,
            this.itemContainer1,
            this.lblTempADInfo,
            this.lblTempAD});
            this.itemPanel1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemPanel1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.itemPanel1.Location = new System.Drawing.Point(1, 43);
            this.itemPanel1.Name = "itemPanel1";
            this.itemPanel1.Size = new System.Drawing.Size(612, 469);
            this.itemPanel1.TabIndex = 1;
            this.itemPanel1.Text = "itemPanel1";
            // 
            // lblADCounter
            // 
            this.lblADCounter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblADCounter.Name = "lblADCounter";
            // 
            // lblListContent
            // 
            this.lblListContent.Name = "lblListContent";
            this.lblListContent.Text = "@資料處理科 男生7人 女生9人(合計16人)\r\n@汽車修理科 男生7人 女生9人(合計16人)\r\n@工業管理科 男生7人 女生9人(合計16人)";
            // 
            // lblADInfo
            // 
            this.lblADInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblADInfo.Name = "lblADInfo";
            // 
            // itemContainer1
            // 
            // 
            // 
            // 
            this.itemContainer1.BackgroundStyle.Class = "";
            this.itemContainer1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer1.Name = "itemContainer1";
            this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblAD});
            // 
            // lblAD
            // 
            this.lblAD.Name = "lblAD";
            this.lblAD.Text = "<font color=\'red\'>未登錄</font>";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Controls.Add(this.btnModifyCover);
            this.panelEx1.Controls.Add(this.btnEReport2);
            this.panelEx1.Controls.Add(this.lblADName1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(1, 1);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(612, 42);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // btnModifyCover
            // 
            this.btnModifyCover.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnModifyCover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnModifyCover.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnModifyCover.Location = new System.Drawing.Point(369, 11);
            this.btnModifyCover.Name = "btnModifyCover";
            this.btnModifyCover.Size = new System.Drawing.Size(97, 24);
            this.btnModifyCover.TabIndex = 8;
            this.btnModifyCover.Text = "調整封面資料";
            this.btnModifyCover.Click += new System.EventHandler(this.btnModifyCover_Click);
            // 
            // btnEReport2
            // 
            this.btnEReport2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnEReport2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEReport2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnEReport2.Location = new System.Drawing.Point(473, 11);
            this.btnEReport2.Name = "btnEReport2";
            this.btnEReport2.Size = new System.Drawing.Size(97, 24);
            this.btnEReport2.TabIndex = 7;
            this.btnEReport2.Text = "預覽報表";
            this.btnEReport2.Click += new System.EventHandler(this.btnEReport2_Click);
            // 
            // lblADName1
            // 
            this.lblADName1.BackColor = System.Drawing.Color.Transparent;
            this.lblADName1.Location = new System.Drawing.Point(18, 11);
            this.lblADName1.Name = "lblADName1";
            this.lblADName1.Size = new System.Drawing.Size(478, 17);
            this.lblADName1.TabIndex = 0;
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "名冊摘要";
            // 
            // cciADDate
            // 
            this.cciADDate.AllowItemResize = true;
            this.cciADDate.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.cciADDate.Name = "cciADDate";
            this.cciADDate.Text = "DateTimeTextBox";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.BackColor = System.Drawing.Color.Transparent;
            this.tabControl1.CanReorderTabs = true;
            this.tabControl1.Controls.Add(this.tabControlPanel1);
            this.tabControl1.Controls.Add(this.tabControlPanel2);
            this.tabControl1.Location = new System.Drawing.Point(210, 47);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold);
            this.tabControl1.SelectedTabIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(614, 541);
            this.tabControl1.Style = DevComponents.DotNetBar.eTabStripStyle.Office2007Document;
            this.tabControl1.TabIndex = 5;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.MultilineWithNavigationBox;
            this.tabControl1.Tabs.Add(this.tabItem1);
            this.tabControl1.Tabs.Add(this.tabItem2);
            this.tabControl1.Text = "名冊摘要";
            // 
            // backPanel
            // 
            this.backPanel.CanvasColor = System.Drawing.SystemColors.Control;
            this.backPanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.backPanel.Controls.Add(this.btnADTempDN);
            this.backPanel.Controls.Add(this.itemPanelName);
            this.backPanel.Controls.Add(this.btnDelete);
            this.backPanel.Controls.Add(this.tabControl1);
            this.backPanel.Controls.Add(this.btnInsert);
            this.backPanel.Controls.Add(this.btnAD);
            this.backPanel.Location = new System.Drawing.Point(0, 0);
            this.backPanel.Name = "backPanel";
            this.backPanel.Size = new System.Drawing.Size(838, 604);
            this.backPanel.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.backPanel.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.backPanel.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.backPanel.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.backPanel.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.backPanel.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.backPanel.Style.GradientAngle = 270;
            this.backPanel.TabIndex = 7;
            // 
            // itemPanelName
            // 
            this.itemPanelName.AutoScroll = true;
            this.itemPanelName.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.itemPanelName.BackgroundStyle.BackColor = System.Drawing.Color.White;
            this.itemPanelName.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.itemPanelName.BackgroundStyle.BorderBottomWidth = 1;
            this.itemPanelName.BackgroundStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.itemPanelName.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.itemPanelName.BackgroundStyle.BorderLeftWidth = 1;
            this.itemPanelName.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.itemPanelName.BackgroundStyle.BorderRightWidth = 1;
            this.itemPanelName.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.itemPanelName.BackgroundStyle.BorderTopWidth = 1;
            this.itemPanelName.BackgroundStyle.Class = "";
            this.itemPanelName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemPanelName.BackgroundStyle.PaddingBottom = 1;
            this.itemPanelName.BackgroundStyle.PaddingLeft = 1;
            this.itemPanelName.BackgroundStyle.PaddingRight = 1;
            this.itemPanelName.BackgroundStyle.PaddingTop = 1;
            this.itemPanelName.ContainerControlProcessDialogKey = true;
            this.itemPanelName.Images = this.imageList1;
            this.itemPanelName.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemPanelName.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.itemPanelName.Location = new System.Drawing.Point(12, 48);
            this.itemPanelName.Name = "itemPanelName";
            this.itemPanelName.Size = new System.Drawing.Size(192, 479);
            this.itemPanelName.TabIndex = 0;
            this.itemPanelName.Text = "itemPanel3";
            this.itemPanelName.ItemClick += new System.EventHandler(this.itemPanelName_ItemClick);
            this.itemPanelName.Click += new System.EventHandler(this.itemPanelName_ItemClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "f.png");
            this.imageList1.Images.SetKeyName(1, "ff.png");
            // 
            // btnADTempDN
            // 
            this.btnADTempDN.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnADTempDN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnADTempDN.BackColor = System.Drawing.Color.Transparent;
            this.btnADTempDN.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnADTempDN.Location = new System.Drawing.Point(82, 563);
            this.btnADTempDN.Name = "btnADTempDN";
            this.btnADTempDN.Size = new System.Drawing.Size(105, 24);
            this.btnADTempDN.TabIndex = 6;
            this.btnADTempDN.Text = "登錄臨編學統";
            this.btnADTempDN.Click += new System.EventHandler(this.btnADTempDN_Click);
            // 
            // lblTempADInfo
            // 
            this.lblTempADInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblTempADInfo.Name = "lblTempADInfo";
            // 
            // lblTempAD
            // 
            this.lblTempAD.Name = "lblTempAD";
            this.lblTempAD.Text = "<font color=\'red\'>未登錄</font>";
            // 
            // ListForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(850, 610);
            this.Controls.Add(this.pnlReport);
            this.Controls.Add(this.cboSchoolYear);
            this.Controls.Add(this.backPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ListForm";
            this.Text = "異動名冊管理";
            this.tabControlPanel2.ResumeLayout(false);
            this.itemPanel2.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.pnlReport.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.backPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSchoolYear;
        private DevComponents.DotNetBar.ButtonX btnInsert;
        private DevComponents.DotNetBar.ButtonX btnDelete;
        private DevComponents.DotNetBar.ButtonX btnAD;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.Label lblADName2;
        private DevComponents.DotNetBar.TabItem tabItem2;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.Label lblADName1;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private DevComponents.DotNetBar.TabControl tabControl1;
        private DevComponents.DotNetBar.ItemPanel itemPanel1;
        private DevComponents.DotNetBar.LabelItem lblADCounter;
        private DevComponents.DotNetBar.LabelItem lblListContent;
        private DevComponents.DotNetBar.LabelItem lblADInfo;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;
        private DevComponents.DotNetBar.ControlContainerItem cciADDate;
        private DevComponents.DotNetBar.ItemPanel itemPanel2;
        private DevComponents.DotNetBar.LabelItem lblADName3;
        private SmartSchool.Common.ListViewEX listView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private DevComponents.DotNetBar.LabelItem lblAD;
        private DevComponents.DotNetBar.ButtonX btnEReport1;
        private DevComponents.DotNetBar.PanelEx pnlReport;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ProgressBarX progressBarX1;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem1;
        private DevComponents.DotNetBar.ButtonX btnEReport2;
        private DevComponents.DotNetBar.PanelEx backPanel;
        private DevComponents.DotNetBar.ItemPanel itemPanelName;
        private System.Windows.Forms.ImageList imageList1;
        private DevComponents.DotNetBar.ButtonX btnModifyCover;
        private DevComponents.DotNetBar.ButtonX btnADTempDN;
        private DevComponents.DotNetBar.LabelItem lblTempADInfo;
        private DevComponents.DotNetBar.LabelItem lblTempAD;
    }
}