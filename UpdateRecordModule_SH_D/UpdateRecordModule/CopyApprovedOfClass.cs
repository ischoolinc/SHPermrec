using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using FISCA.UDT;
using SmartSchool.Customization.PlugIn.ImportExport;
using  UpdateRecordModule_SH_D.UDT;
namespace UpdateRecordModule_SH_D
{
    public partial class CopyApprovedOfClass : BaseForm
    {
        public CopyApprovedOfClass()
        {
            InitializeComponent();
        }

        private void InputSchoolYear_Load(object sender, EventArgs e)
        {
            for (int i = int.Parse(K12.Data.School.DefaultSchoolYear) - 10; i <= int.Parse(K12.Data.School.DefaultSchoolYear) + 10; i++)
            {
                cboSchoolYearOld.Items.Add(i.ToString());
                cboSchoolYearNew.Items.Add(i.ToString());
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int temp = 0;
            if (!int.TryParse(cboSchoolYearOld.Text, out temp))
            {
                MessageBox.Show("請選擇來源年度");
                return;
            }
            if (!int.TryParse(cboSchoolYearNew.Text, out temp))
            {
                MessageBox.Show("請選擇目的年度");
                return;
            }

            AccessHelper udtHelper= new AccessHelper();
            List<udtGovApprovedNumOfClass> lstRecord = new List<udtGovApprovedNumOfClass>();
            lstRecord = udtHelper.Select<udtGovApprovedNumOfClass>("SchoolYear =" + cboSchoolYearNew.Text);
            if (lstRecord.Count>0)
            {
                MessageBox.Show("目的年度有資料，無法複製");
                return;
            }
            lstRecord = udtHelper.Select<udtGovApprovedNumOfClass>("SchoolYear =" + cboSchoolYearOld.Text);
            foreach (udtGovApprovedNumOfClass rec in lstRecord)
            {
                udtGovApprovedNumOfClass newRec= new udtGovApprovedNumOfClass();
                newRec.DeptName = rec.DeptName;
                newRec.DeptGroup = rec.DeptGroup;
                newRec.DeptCode = rec.DeptCode;
                newRec.ClassNum = rec.ClassNum;
                newRec.StudentNum= rec.StudentNum;
                newRec.ClassType = rec.ClassType;
                newRec.ClassTypeU=rec.ClassTypeU;
                newRec.SchoolYear=int.Parse(cboSchoolYearNew.Text);
                newRec.Save();
            }
            MessageBox.Show("複製完成");
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
