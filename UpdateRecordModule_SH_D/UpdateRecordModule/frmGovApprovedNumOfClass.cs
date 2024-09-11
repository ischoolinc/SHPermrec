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
using K12.Data;
using SHSchool.Data;
using UpdateRecordModule_SH_D.UDT;


namespace UpdateRecordModule_SH_D
{
    public partial class frmGovApprovedNumOfClass : BaseForm
    {
         List<SHDepartmentRecord> lstdept;
         Dictionary<string,string> group =new Dictionary<string, string>();
        public frmGovApprovedNumOfClass()
        {
            InitializeComponent();
            List<SHDeptGroupRecord> lstdeptgroup = SHSchool.Data.SHDeptGroup.SelectAll();
            lstdept = SHSchool.Data.SHDepartment.SelectAll();
            cboCourseKind.Items.Clear();
            //var newItem = new ComboBoxItem();
            foreach (SHDeptGroupRecord branch in lstdeptgroup)
            {
                cboCourseKind.Items.Add(new ComboBoxItem(branch.ID,branch.Name));
                group.Add(branch.ID,branch.Name);
            }
            for (int i=int.Parse(K12.Data.School.DefaultSchoolYear)-10;  i<= int.Parse(K12.Data.School.DefaultSchoolYear) + 10; i++)
            {
                cboSchoolYear.Items.Add(i.ToString());
                cboSchoolYearG.Items.Add(i.ToString());
            }
            cboSchoolYear.Text = K12.Data.School.DefaultSchoolYear;
            Data_clear();
        }
        public class ComboBoxItem
        {
            public string Value { get; set; }
            public string Text { get; set; }
            public ComboBoxItem(string value, string text)
            {
                Value = value;
                Text = text;
            }
            public override string ToString()
            {
                return Text;
            }
        }


        private void cbxClassType_DropDown(object sender, EventArgs e)
        {
            cbxClassType.Items.Clear();
            cbxClassType.Items.AddRange(DAL.DALTransfer.GetClassTypeList().ToArray());
        }

        private void cbxClassType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = cbxClassType.Text.IndexOf("-");
            if (idx > 0)
            {
                string code = cbxClassType.Text.Substring(0, idx);
                cbxClassType.Items.Add(code);
                cbxClassType.Text = code;
            }
        }
        private void cbxClassTypeU_DropDown(object sender, EventArgs e)
        {
            cbxClassTypeU.Items.Clear();
            cbxClassTypeU.Items.AddRange(DAL.DALTransfer.GetClassTypeUpList().ToArray());
        }

        private void cbxClassTypeU_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = cbxClassTypeU.Text.IndexOf("-");
            if (idx > 0)
            {
                string code = cbxClassTypeU.Text.Substring(0, idx);
                cbxClassTypeU.Items.Add(code);
                cbxClassTypeU.Text = code;
            }
        }

        private void cboCourseKind_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            cboDept.Items.Clear();
            
            foreach (SHDepartmentRecord dept in lstdept)
            {
                if (dept.RefDeptGroupID== ComboUtil.GetItem(cboCourseKind).Value )
                    cboDept.Items.Add(new ComboBoxItem(dept.FullName, dept.Code));
            }
        }

        private void cboDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDeptName.Text = ComboUtil.GetItem(cboDept).Value;
        }
        private void Data_clear()
        {
            cboDept.Items.Clear();
            cboDept.Text = "";
            cboDept.Enabled = true;
            cboSchoolYearG.Text = K12.Data.School.DefaultSchoolYear;
            cboSchoolYearG.Enabled = true;
            txtDeptName.Text = "";
            cbxClassTypeU.Text = "";
            cbxClassType.Text = "";
            cboCourseKind.Text = "";
            cboCourseKind.Enabled = true;
            txtApprovedClass.Text = "0";
            txtApprovedStu.Text = "0";
            lblDataMode.Text = "Add";
            lblDataMode.Tag = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Data_clear();
        }
        private void Reflash_SchoolYear()
        {
            int temp = 0;
            if(!int.TryParse(cboSchoolYear.Text,out temp))
                {
                return;
            }
            FISCA.UDT.AccessHelper AccessHelper = new FISCA.UDT.AccessHelper();
            
            string condition = "  SchoolYear=" + cboSchoolYear.Text  ;

            List<udtGovApprovedNumOfClass> tempRecord =AccessHelper.Select<udtGovApprovedNumOfClass> (condition) ;
            lstGovApproved.Items.Clear();
            foreach (udtGovApprovedNumOfClass rec in tempRecord)
            {
                ListViewItem lvi = new ListViewItem();
                if (group.ContainsKey(rec.DeptGroup.ToString()))
                    lvi.SubItems.Add(group[rec.DeptGroup.ToString()]);
                else
                    lvi.SubItems.Add("");
                lvi.SubItems.Add(rec.DeptCode.ToString());
                lvi.SubItems.Add(rec.DeptName.ToString());
                lvi.SubItems.Add(rec.ClassType.ToString());
                lvi.SubItems.Add(rec.ClassTypeU.ToString());
                lvi.SubItems.Add(rec.ClassNum.ToString());
                lvi.SubItems.Add(rec.StudentNum.ToString());
                lvi.SubItems.Add(rec.DeptGroup.ToString());
                lvi.Tag = rec;
                lstGovApproved.Items.Add(lvi);
            }
        }
        private void cboSchoolYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Reflash_SchoolYear();
        }

        private void lstGovApproved_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGovApproved.Items.Count; i++)
            {
                if (lstGovApproved.Items[i].Selected)
                {
                    cboCourseKind.Text = lstGovApproved.Items[i].SubItems[1].Text;
                    cboCourseKind.Enabled = false;
                    cboDept.Text = lstGovApproved.Items[i].SubItems[2].Text;
                    cboDept.Enabled = false;
                    txtDeptName.Text = lstGovApproved.Items[i].SubItems[3].Text;
                    cboSchoolYearG.Text=cboSchoolYear.Text;
                    cboSchoolYearG.Enabled = false;
                    cbxClassType.Text = lstGovApproved.Items[i].SubItems[4].Text;
                    cbxClassTypeU.Text = lstGovApproved.Items[i].SubItems[5].Text;
                    txtApprovedClass.Text = lstGovApproved.Items[i].SubItems[6].Text;
                    txtApprovedStu.Text = lstGovApproved.Items[i].SubItems[7].Text;
                    lblDataMode.Text = "Edit";
                    lblDataMode.Tag = lstGovApproved.Items[i].Tag;
                    break;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int temp = 0;
            if (!int.TryParse(cboSchoolYearG.Text, out temp))
            {
                MessageBox.Show("請設定學年度");
                return;
            }
            if (!int.TryParse(txtApprovedClass.Text, out temp))
            {
                MessageBox.Show("核定班級數有誤");
                return;
            }
            if (!int.TryParse(txtApprovedStu.Text, out temp))
            {
                MessageBox.Show("核定學生數有誤");
                return;
            }
            FISCA.UDT.AccessHelper AccessHelper = new FISCA.UDT.AccessHelper();
            List<udtGovApprovedNumOfClass> lstRecotd = new List<udtGovApprovedNumOfClass>();
            try
            {
                string condition = "";
                
                condition = " SchoolYear=" + cboSchoolYearG.Text + " AND DeptGroup='" + ComboUtil.GetItem(cboCourseKind).Value + "' AND Dept_Code='" + cboDept.Text + "' AND Class_TypeU='" + cbxClassTypeU.Text + "' AND Class_Type='" + cbxClassType.Text + "'";

                lstRecotd = AccessHelper.Select<udtGovApprovedNumOfClass>(condition);
            }
            catch { MessageBox.Show("查詢錯誤"); }
            if (lblDataMode.Text=="Edit")
            {
                
                udtGovApprovedNumOfClass editRec = (udtGovApprovedNumOfClass)lblDataMode.Tag;

                try
                {
                    Boolean find = false;
                    if (lstRecotd.Count > 0)
                    {
                        
                        foreach (udtGovApprovedNumOfClass rec in lstRecotd)
                            if (editRec.UID != rec.UID)
                            {
                                find = true;
                                break;
                            }


                    }
                    if (find)
                    {
                        MessageBox.Show("資料重覆無法修改");
                    }
                    else
                    {
                        editRec.ClassTypeU = cbxClassTypeU.Text;
                        editRec.ClassType = cbxClassType.Text;
                        editRec.ClassNum = int.Parse(txtApprovedClass.Text);
                        editRec.StudentNum = int.Parse(txtApprovedStu.Text);
                        editRec.Save();
                        MessageBox.Show("修改完成");
                    }
                }
                catch { MessageBox.Show("無法修改"); }
            }
           
            if (lblDataMode.Text=="Add")
            {
               
                if (lstRecotd.Count > 0 )
                {
                    MessageBox.Show("己有資料，無法新增");
                    return;
                }
                udtGovApprovedNumOfClass udtGovRec = new udtGovApprovedNumOfClass();
                udtGovRec.DeptGroup = ComboUtil.GetItem(cboCourseKind).Value;        
                udtGovRec.SchoolYear = int.Parse(cboSchoolYearG.Text);
                udtGovRec.ClassNum=int.Parse(txtApprovedClass.Text);
                udtGovRec.StudentNum = int.Parse(txtApprovedStu.Text);
                udtGovRec.ClassType = cbxClassType.Text;
                udtGovRec.ClassTypeU = cbxClassTypeU.Text;
                udtGovRec.DeptCode = cboDept.Text;
                udtGovRec.DeptName = ComboUtil.GetItem(cboDept).Value;
                udtGovRec.Save();
                MessageBox.Show("新增完成");
            }
            Data_clear();
            Reflash_SchoolYear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGovApproved.Items.Count; i++)
            {
                if (lstGovApproved.Items[i].Checked)
                {
                    udtGovApprovedNumOfClass editRec = (udtGovApprovedNumOfClass)lstGovApproved.Items[i].Tag;

                    
                    try
                    {
                        editRec.Deleted = true;;
                        editRec.Save();
                    }
                    catch { MessageBox.Show("無法刪除" + lstGovApproved.Items[i].SubItems[3]); }
                }
            }
            MessageBox.Show("刪除完成");
            Data_clear() ;
            Reflash_SchoolYear() ;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            CopyApprovedOfClass frmcopy = new CopyApprovedOfClass();
            frmcopy.ShowDialog();
            Data_clear();
            Reflash_SchoolYear();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public class ComboUtil
        {
            /// <summary>
            /// 設定下拉值
            /// </summary>
            /// <param name="cbo">物件</param>
            /// <param name="value">值</param>
            public static void SetItemValue(ComboBox cbo, string value)
            {
                var selectedObject = cbo.Items.Cast<ComboBoxItem>().SingleOrDefault(i => i.Value.Equals(value));
                if (selectedObject != null)
                    cbo.SelectedIndex = cbo.FindStringExact(selectedObject.Text.ToString());
                else
                    cbo.SelectedIndex = -1;
            }

            /// <summary>
            /// 取得下拉項目
            /// </summary>
            /// <param name="cbo">物件</param>
            /// <returns></returns>
            public static ComboBoxItem GetItem(ComboBox cbo)
            {
                ComboBoxItem item = new ComboBoxItem("", "");
                if (cbo.SelectedIndex > -1)
                {
                    item = cbo.Items[cbo.SelectedIndex] as ComboBoxItem;
                }
                return item;
            }

            /// <summary>
            /// 取得索引下拉項目
            /// </summary>
            /// <param name="cbo">物件</param>
            /// <param name="index">索引</param>
            /// <returns></returns>
            public static ComboBoxItem GetItem(ComboBox cbo, int index)
            {
                ComboBoxItem item = null;
                if (index > -1)
                {
                    item = cbo.Items[index] as ComboBoxItem;
                }
                return item;
            }

            /// <summary>
            /// 移除下拉項目
            /// </summary>
            /// <param name="cbo">物件</param>
            /// <param name="value">值</param>
            public static void RemoveItem(ComboBox cbo, string value)
            {
                ComboBoxItem selectedObject = cbo.Items.Cast<ComboBoxItem>().SingleOrDefault(i => i.Value.Equals(value));
                cbo.Items.Remove(selectedObject);
            }

            /// <summary>
            /// DataTable 綁定下拉項目
            /// </summary>
            /// <param name="cbo">物件</param>
            /// <param name="dt">資料集</param>
            /// <param name="valueColumn">值欄位</param>
            /// <param name="textColumn">名稱欄位</param>
            /// <param name="addEmpty">是否加空白選項</param>
            public static void BindTableToDDL(ComboBox cbo, DataTable dt, string valueColumn, string textColumn, bool addEmpty)
            {
                cbo.Items.Clear();
                if (addEmpty)
                {
                    cbo.Items.Add(new ComboBoxItem("", ""));
                }
                foreach (DataRow dr in dt.Rows)
                {
                    cbo.Items.Add(new ComboBoxItem(dr[valueColumn].ToString(), dr[textColumn].ToString()));
                }
                if (cbo.Items.Count > 0)
                    cbo.SelectedIndex = 0;
            }
        }
    }
}
