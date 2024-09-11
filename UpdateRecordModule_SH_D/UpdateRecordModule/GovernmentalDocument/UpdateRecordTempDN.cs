using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UpdateRecordModule_SH_D.GovernmentalDocument
{
    public partial class UpdateRecordTempDN : FISCA.Presentation.Controls.BaseForm
    {
        BL.StudUpdateRecBatchRec GovDoc;

        /// <summary>
        /// 登入文號
        /// </summary>
        public UpdateRecordTempDN(string id)
        {
            InitializeComponent();
            // 取得名冊
            GovDoc = DAL.DALTransfer.GetStudUpdateRecBatchRec(id);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            
            // 檢查資料
            if (string.IsNullOrEmpty(txtTempNumber.Text))
            {
                FISCA.Presentation.Controls.MsgBox.Show("請輸入臨編字號。");
                return;
            }

            if (dtTempDate.IsEmpty)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請輸入臨編日期。");
                return;           
            }
            if (string.IsNullOrEmpty(txtTempDesc.Text))
            {
                FISCA.Presentation.Controls.MsgBox.Show("請輸入臨編學統。");
                return;
            }

            // 修改名冊內異動
            GovDoc.TempDate = dtTempDate.Value;
            GovDoc.TempDesc = txtTempDesc.Text;
            GovDoc.TempNumber = txtTempNumber.Text;

            // 取得名冊內異動資料ID,修改學生異動資料使用
            List<string> updateRecIDList = new List<string>();
            

            foreach (BL.StudUpdateRecDoc rec in GovDoc.StudUpdateRecDocList)
            {
                rec.temp_date = dtTempDate.Value.ToShortDateString();
                rec.temp_desc = txtTempDesc.Text;
                rec.temp_number = txtTempNumber.Text;
                if (!updateRecIDList.Contains(rec.URID))
                    updateRecIDList.Add(rec.URID);
            }

            // 回存名冊
            string PeopleFrom = "";
            string ClassTypeU = "";
            DAL.DALTransfer.SetStudUpdateRecBatchRec(GovDoc, false,PeopleFrom, ClassTypeU);

            // 修改學生身上的相對異動記錄核准日期文號
            DAL.DALTransfer.SetStudsUpdateRecTempData(dtTempDate.Value.ToShortDateString(), txtTempDesc.Text,txtTempNumber.Text, updateRecIDList);
            Global.OnUpdateDocsChange();
            // 修改學生身上的相對異動記錄核准日期文號
           
            this.Close();
        }

        private void UpdateRecordTempDN_Load(object sender, EventArgs e)
        {
            // 載入初始
            if (GovDoc != null)
            {
                if (GovDoc.TempDate.HasValue)
                    dtTempDate.Value = GovDoc.TempDate.Value;
                txtTempDesc.Text = GovDoc.TempDesc;
                txtTempNumber.Text = GovDoc.TempNumber;
            }
            else
            {
                dtTempDate.Text = "";
                txtTempDesc.Text = "";
                txtTempNumber.Text = "";
            }

        }
    }
}
