using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using DevComponents.DotNetBar.Controls;
using FISCA.Permission;
using FISCA.Presentation.Controls;
using SHSchool.Data;

namespace SmartSchool.GovernmentalDocument.Content
{
    public partial class UpdateRecordForm : FISCA.Presentation.Controls.BaseForm
    {
        //學生編號
        private string _studentid;
        private string _updateid;
        //異動記錄編號
        public event EventHandler DataSaved;
        private bool _saved;

        private Dictionary<UpdateRecordType, XmlElement> _tempInfo;
        private UpdateRecordType _previousType;

        public UpdateRecordForm(string id, string updateid)
        {
            _studentid = id;
            _updateid = updateid;
            _saved = false;
            _tempInfo = new Dictionary<UpdateRecordType, XmlElement>();
            _tempInfo.Add(UpdateRecordType.畢業異動, null);
            _tempInfo.Add(UpdateRecordType.新生異動, null);
            _tempInfo.Add(UpdateRecordType.學籍異動, null);
            _tempInfo.Add(UpdateRecordType.轉入異動, null);
            InitializeComponent();
            Initialize();

            FeatureAce ace = FISCA.Permission.UserAcl.Current[UpdatePalmerwormItem.FeatureCode];

            btnOK.Visible = ace.Editable;

            if (!ace.Editable)
                LockAllControl(this);
        }

        private void LockAllControl(Control parent)
        {
            foreach (Control each in parent.Controls)
            {
                if (each is TextBoxX)
                    (each as TextBoxX).ReadOnly = true;

                if (each is ComboBoxEx)
                    (each as ComboBoxEx).Enabled = false;

                if (each.Controls.Count > 0)
                    LockAllControl(each);
            }
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //先把目前的資料存起來          
            _tempInfo[_previousType] = updateRecordInfo1.GetElement();


            switch (comboBoxEx1.SelectedIndex)
            {
                default:
                case 0:
                    updateRecordInfo1.Style = UpdateRecordType.學籍異動;
                    break;
                case 1:
                    updateRecordInfo1.Style = UpdateRecordType.轉入異動;
                    break;
                case 2:
                    updateRecordInfo1.Style = UpdateRecordType.新生異動;
                    break;
                case 3:
                    updateRecordInfo1.Style = UpdateRecordType.畢業異動;
                    break;
            }

            XmlElement typeRec = _tempInfo[updateRecordInfo1.Style];
            if (typeRec != null)
                BindDataFromElement(typeRec);

            _previousType = updateRecordInfo1.Style;
        }

        private void Initialize()
        {
            if (!string.IsNullOrEmpty(_updateid))
            {
                updateRecordInfo1.StudentID = _studentid;
                updateRecordInfo1.SetUpdateValue(_updateid);
            }
            else
            {
                updateRecordInfo1.SetDefaultValue(_studentid);
            }
            switch (updateRecordInfo1.Style)
            {
                case UpdateRecordType.學籍異動:
                    comboBoxEx1.SelectedIndex = 0;
                    break;
                case UpdateRecordType.轉入異動:
                    comboBoxEx1.SelectedIndex = 1;

                    break;
                case UpdateRecordType.新生異動:
                    comboBoxEx1.SelectedIndex = 2;

                    break;
                case UpdateRecordType.畢業異動:
                    comboBoxEx1.SelectedIndex = 3;
                    break;
                default:
                    break;
            }
            _previousType = updateRecordInfo1.Style;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // 檢查是否要修改學生狀態
            int UpdateCoodeInt;
            bool UpdateStudStatus = false;

            if (int.TryParse(updateRecordInfo1.UpdateCode, out UpdateCoodeInt))
            {
                string StudIDNumber = "", StudNumber = "";

                SHStudentRecord stud = SHStudent.SelectByID(updateRecordInfo1.StudentID);

                if (stud != null)
                {
                    StudIDNumber = stud.IDNumber;
                    StudNumber = stud.StudentNumber;
                }


                // 收集檢查用資料
                // 一般
                List<string> tmp01 = new List<string>();
                // 畢業或離校
                List<string> tmp02 = new List<string>();
                // 休學
                List<string> tmp03 = new List<string>();

                foreach (SHStudentRecord studRec in SHStudent.SelectAll())
                {
                    if (studRec.Status == K12.Data.StudentRecord.StudentStatus.一般)
                    {
                        tmp01.Add(studRec.IDNumber);
                        tmp01.Add(studRec.StudentNumber);
                    }

                    if (studRec.Status == K12.Data.StudentRecord.StudentStatus.畢業或離校)
                    {
                        tmp02.Add(studRec.IDNumber);
                        tmp02.Add(studRec.StudentNumber);
                    }

                    if (studRec.Status == K12.Data.StudentRecord.StudentStatus.休學)
                    {
                        tmp03.Add(studRec.IDNumber);
                        tmp03.Add(studRec.StudentNumber);
                    }
                }

                // 復學
                if (UpdateCoodeInt >= 221 && UpdateCoodeInt <= 226)
                {
                    if (MessageBox.Show("請問是否更改學生狀態成 一般？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        // 檢查該狀態是否有相同學號或身分證號學生
                        if (tmp01.Contains(StudIDNumber) || tmp01.Contains(StudNumber))
                        {
                            FISCA.Presentation.Controls.MsgBox.Show("在一般狀態已有相同身分證號或學號的學生，無法自動變更狀態。");
                        }
                        else
                        {
                            UpdateStudStatus = true;
                            stud.Status = K12.Data.StudentRecord.StudentStatus.一般;
                        }
                    }
                }

                // 轉出
                if (UpdateCoodeInt >= 311 && UpdateCoodeInt <= 316)
                {
                    if (MessageBox.Show("請問是否更改學生狀態成 畢業或離校？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        // 檢查該狀態是否有相同學號或身分證號學生
                        if (tmp02.Contains(StudIDNumber) || tmp02.Contains(StudNumber))
                        {
                            FISCA.Presentation.Controls.MsgBox.Show("在畢業或離校狀態已有相同身分證號或學號的學生，無法自動變更狀態。");
                        }
                        else
                        {
                            UpdateStudStatus = true;
                            stud.Status = K12.Data.StudentRecord.StudentStatus.畢業或離校;
                        }
                    }
                }

                // 休學
                if (UpdateCoodeInt >= 341 && UpdateCoodeInt <= 349)
                {
                    if (MessageBox.Show("請問是否更改學生狀態成 休學？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        if (MessageBox.Show("請問是否更改學生狀態成 畢業或離校？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            // 檢查該狀態是否有相同學號或身分證號學生
                            if (tmp03.Contains(StudIDNumber) || tmp03.Contains(StudNumber))
                            {
                                FISCA.Presentation.Controls.MsgBox.Show("在休學狀態已有相同身分證號或學號的學生，無法自動變更狀態。");
                            }
                            else
                            {
                                UpdateStudStatus = true;
                                stud.Status = K12.Data.StudentRecord.StudentStatus.休學;
                            }
                        }
                    }
                }
                // 更新學生狀態
                if (UpdateStudStatus)
                {
                    SHStudent.Update(stud);
                    StudentRelated.Student.Instance.SyncAllBackground();
                }
            }
            if (updateRecordInfo1.Save())
            {
                _saved = true;
                if (DataSaved != null)
                    DataSaved(this, null);
                this.Close();
            }
        }

        private void UpdateRecordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CurrentUser.Acl[UpdatePalmerwormItem.FeatureCode].Editable)
                return;

            if (!_saved)
            {
                if (MsgBox.Show("這個動作將放棄目前編輯中的資料，是否確定離開?", "提醒", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// 驗證資料是否有誤
        /// </summary>
        /// <returns></returns>
        private bool IsValid()
        {
            bool valid = updateRecordInfo1.IsValid();
            if (!valid)
                MsgBox.Show("資料錯誤，請檢查輸入資料", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return valid;
        }

        private string GetElementValue(XmlElement element, string xpath)
        {
            if (element == null) return "";
            if (element.SelectSingleNode(xpath) == null)
                return "";
            return element.SelectSingleNode(xpath).InnerText;
        }

        private void BindDataFromElement(XmlElement element)
        {
            updateRecordInfo1.Name = GetElementValue(element, "Name");
            updateRecordInfo1.StudentNumber = GetElementValue(element, "StudentNumber");
            updateRecordInfo1.IDNumber = GetElementValue(element, "IDNumber");
            updateRecordInfo1.Gender = GetElementValue(element, "Gender");
            updateRecordInfo1.Birthdate = GetElementValue(element, "Birthdate");
            updateRecordInfo1.UpdateDate = GetElementValue(element, "UpdateDate");
            updateRecordInfo1.UpdateCode = GetElementValue(element, "UpdateCode");
            updateRecordInfo1.UpdateDescription = GetElementValue(element, "UpdateDescription");
            updateRecordInfo1.Comment = GetElementValue(element, "Comment");
            updateRecordInfo1.GradeYear = GetElementValue(element, "GradeYear");
            updateRecordInfo1.ADDate = GetElementValue(element, "ADDate");
            updateRecordInfo1.ADNumber = GetElementValue(element, "ADNumber");
            updateRecordInfo1.Department = GetElementValue(element, "Department");
            updateRecordInfo1.LastADDate = GetElementValue(element, "LastADDate");
            updateRecordInfo1.LastADNumber = GetElementValue(element, "LastADNumber");
            updateRecordInfo1.LastUpdateCode = GetElementValue(element, "LastUpdateCode");
            updateRecordInfo1.PreviousDepartment = GetElementValue(element, "PreviousDepartment");
            updateRecordInfo1.PreviousGradeYear = GetElementValue(element, "PreviousGradeYear");
            updateRecordInfo1.PreviousSchool = GetElementValue(element, "PreviousSchool");
            updateRecordInfo1.PreviousSchoolLastADDate = GetElementValue(element, "PreviousSchoolLastADDate");
            updateRecordInfo1.PreviousSchoolLastADNumber = GetElementValue(element, "PreviousSchoolLastADNumber");
            updateRecordInfo1.PreviousStudentNumber = GetElementValue(element, "PreviousStudentNumber");
            updateRecordInfo1.GraduateCertificateNumber = GetElementValue(element, "GraduateCertificateNumber");
            updateRecordInfo1.GraduateSchool = GetElementValue(element, "GraduateSchool");
            updateRecordInfo1.GraduateSchoolLocationCode = GetElementValue(element, "GraduateSchoolLocationCode");            
            
            #region 2009年新制新增
            updateRecordInfo1.ClassType = GetElementValue(element,"ClassType");
            updateRecordInfo1.SpecialStatus = GetElementValue(element,"SpecialStatus");
            updateRecordInfo1.IDNumberComment = GetElementValue(element,"IDNumberComment");
            updateRecordInfo1.OldClassType = GetElementValue(element,"OldClassType");
            updateRecordInfo1.OldDepartmentCode = GetElementValue(element,"OldDepartmentCode");
            updateRecordInfo1.GraduateSchoolYear = GetElementValue(element,"GraduateSchoolYear");
            updateRecordInfo1.GraduateComment = GetElementValue(element,"GraduateComment");
            #endregion 
        }

        /// <summary>
        /// 判斷目前資料是屬於何種異動類別的
        /// 但因為目前資料不足，所以還不確定要怎麼判斷
        /// 先寫成一個function , 將永遠傳回 "學籍異動";
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>異動類別</returns>
        private UpdateRecordType GetUpdateRecordType(object arg)
        {
            return UpdateRecordType.學籍異動;
        }
    }
}