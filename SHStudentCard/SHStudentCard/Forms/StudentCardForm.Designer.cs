namespace SHStudentCard.Forms
{
    partial class StudentCardForm
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
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.lnkDesc = new System.Windows.Forms.LinkLabel();
            this.lnkUpload = new System.Windows.Forms.LinkLabel();
            this.lnkDownload = new System.Windows.Forms.LinkLabel();
            this.btnPrint = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.lnkReset = new System.Windows.Forms.LinkLabel();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.lnkReset);
            this.groupPanel1.Controls.Add(this.lnkDesc);
            this.groupPanel1.Controls.Add(this.lnkUpload);
            this.groupPanel1.Controls.Add(this.lnkDownload);
            this.groupPanel1.Location = new System.Drawing.Point(12, 12);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(239, 107);
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
            this.groupPanel1.TabIndex = 3;
            this.groupPanel1.Text = "樣板";
            // 
            // lnkDesc
            // 
            this.lnkDesc.AutoSize = true;
            this.lnkDesc.Location = new System.Drawing.Point(21, 50);
            this.lnkDesc.Name = "lnkDesc";
            this.lnkDesc.Size = new System.Drawing.Size(138, 17);
            this.lnkDesc.TabIndex = 2;
            this.lnkDesc.TabStop = true;
            this.lnkDesc.Text = "樣板可用合併欄位說明";
            this.lnkDesc.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDesc_LinkClicked);
            // 
            // lnkUpload
            // 
            this.lnkUpload.AutoSize = true;
            this.lnkUpload.Location = new System.Drawing.Point(139, 17);
            this.lnkUpload.Name = "lnkUpload";
            this.lnkUpload.Size = new System.Drawing.Size(86, 17);
            this.lnkUpload.TabIndex = 1;
            this.lnkUpload.TabStop = true;
            this.lnkUpload.Text = "上傳自訂樣版";
            this.lnkUpload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUpload_LinkClicked);
            // 
            // lnkDownload
            // 
            this.lnkDownload.AutoSize = true;
            this.lnkDownload.Location = new System.Drawing.Point(21, 17);
            this.lnkDownload.Name = "lnkDownload";
            this.lnkDownload.Size = new System.Drawing.Size(86, 17);
            this.lnkDownload.TabIndex = 0;
            this.lnkDownload.TabStop = true;
            this.lnkDownload.Text = "下載目前樣版";
            this.lnkDownload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDownload_LinkClicked);
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrint.BackColor = System.Drawing.Color.Transparent;
            this.btnPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrint.Location = new System.Drawing.Point(95, 130);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 25);
            this.btnPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "列印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(176, 130);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lnkReset
            // 
            this.lnkReset.AutoSize = true;
            this.lnkReset.Location = new System.Drawing.Point(165, 50);
            this.lnkReset.Name = "lnkReset";
            this.lnkReset.Size = new System.Drawing.Size(60, 17);
            this.lnkReset.TabIndex = 3;
            this.lnkReset.TabStop = true;
            this.lnkReset.Text = "復原預設";
            this.lnkReset.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkReset_LinkClicked);
            // 
            // StudentCardForm
            // 
            this.ClientSize = new System.Drawing.Size(268, 160);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnExit);
            this.Name = "StudentCardForm";
            this.Text = "學生證";
            this.Load += new System.EventHandler(this.StudentCardForm_Load);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.LinkLabel lnkUpload;
        private System.Windows.Forms.LinkLabel lnkDownload;
        private DevComponents.DotNetBar.ButtonX btnPrint;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private System.Windows.Forms.LinkLabel lnkDesc;
        private System.Windows.Forms.LinkLabel lnkReset;
    }
}