
namespace Leave_School_Notification
{
    partial class OutList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.ResultMessage = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.AddTemp = new DevComponents.DotNetBar.ButtonX();
            this.cmdPrint = new DevComponents.DotNetBar.ButtonX();
            this.cmdCancel = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.ResultMessage)).BeginInit();
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
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(600, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "以下學生的重補修修課科目數超過當前樣板「科目數量」，可能會使繳費單內容不正確。";
            // 
            // ResultMessage
            // 
            this.ResultMessage.AllowUserToAddRows = false;
            this.ResultMessage.AllowUserToDeleteRows = false;
            this.ResultMessage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ResultMessage.DefaultCellStyle = dataGridViewCellStyle4;
            this.ResultMessage.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.ResultMessage.Location = new System.Drawing.Point(12, 41);
            this.ResultMessage.Name = "ResultMessage";
            this.ResultMessage.ReadOnly = true;
            this.ResultMessage.RowTemplate.Height = 24;
            this.ResultMessage.Size = new System.Drawing.Size(525, 338);
            this.ResultMessage.TabIndex = 1;
            // 
            // AddTemp
            // 
            this.AddTemp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.AddTemp.BackColor = System.Drawing.Color.Transparent;
            this.AddTemp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.AddTemp.Location = new System.Drawing.Point(29, 393);
            this.AddTemp.Name = "AddTemp";
            this.AddTemp.Size = new System.Drawing.Size(75, 23);
            this.AddTemp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.AddTemp.TabIndex = 2;
            this.AddTemp.Text = "加入待處理";
            this.AddTemp.Click += new System.EventHandler(this.AddTemp_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdPrint.BackColor = System.Drawing.Color.Transparent;
            this.cmdPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdPrint.Location = new System.Drawing.Point(158, 393);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(197, 23);
            this.cmdPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdPrint.TabIndex = 3;
            this.cmdPrint.Text = "跳過以上學生，繼續列印";
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdCancel.BackColor = System.Drawing.Color.Transparent;
            this.cmdCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(438, 393);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdCancel.TabIndex = 4;
            this.cmdCancel.Text = "取消列印";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // OutList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(554, 424);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.AddTemp);
            this.Controls.Add(this.ResultMessage);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(570, 463);
            this.MinimumSize = new System.Drawing.Size(570, 463);
            this.Name = "OutList";
            this.Text = "重補修科目數量超過樣版數量名單";
            ((System.ComponentModel.ISupportInitialize)(this.ResultMessage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.DataGridViewX ResultMessage;
        private DevComponents.DotNetBar.ButtonX AddTemp;
        private DevComponents.DotNetBar.ButtonX cmdPrint;
        private DevComponents.DotNetBar.ButtonX cmdCancel;
    }
}