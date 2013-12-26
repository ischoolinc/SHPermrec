namespace UpdateRecordViewForm
{
    partial class CompleteForm
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
            this.lblMessage = new DevComponents.DotNetBar.LabelX();
            this.btnYes = new DevComponents.DotNetBar.ButtonX();
            this.btnNo = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Location = new System.Drawing.Point(19, 13);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(167, 20);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "匯出成功，是否現在就開啟？";
            // 
            // btnYes
            // 
            this.btnYes.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnYes.BackColor = System.Drawing.Color.Transparent;
            this.btnYes.Location = new System.Drawing.Point(27, 56);
            this.btnYes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(60, 26);
            this.btnYes.TabIndex = 1;
            this.btnYes.Text = "是";
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnNo.BackColor = System.Drawing.Color.Transparent;
            this.btnNo.Location = new System.Drawing.Point(121, 56);
            this.btnNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(60, 26);
            this.btnNo.TabIndex = 2;
            this.btnNo.Text = "否";
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // CompleteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 93);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.lblMessage);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(214, 127);
            this.Name = "CompleteForm";
            this.Text = "";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblMessage;
        private DevComponents.DotNetBar.ButtonX btnYes;
        private DevComponents.DotNetBar.ButtonX btnNo;
    }
}