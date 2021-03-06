﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SHSchool.Data;
using System.Xml.Linq;


namespace UpdateRecordModule_SH_N.UpdateRecordItemControls
{
    public partial class UpdateRecordInfo01_N : UserControl, IUpdateRecordInfo
    {
        // 異動
        private SHUpdateRecordRecord _UpdateRec;
        // Log
        private PermRecLogProcess _prlp;
        private Utility.FormAndLogDataManager _faldn;
        private List<XElement> _UpdateCodeElms;
        private List<string> _UpdateCoodeList;
        private List<SHDepartmentRecord> _DeptList;
        private ErrorProvider _epUpdateCode;
        private Dictionary<string, string> _updateCodeDict4;        
        // 異動代碼索引，給畫面與檢查使用
        Dictionary<string, string> _UCodeDict;

        /// <summary>
        /// 學籍異動
        /// </summary>
        public UpdateRecordInfo01_N(SHUpdateRecordRecord UpdateRec, PermRecLogProcess prlp, List<XElement> UpdateCodeElms)
        {
            InitializeComponent();
            cbxGender.Items.Add("男");
            cbxGender.Items.Add("女");

            _UpdateRec = UpdateRec;
            _prlp = prlp;
            _UpdateCodeElms = UpdateCodeElms;
            _epUpdateCode = new ErrorProvider();

            _UpdateCoodeList = (from x in _UpdateCodeElms select x.Element("代號").Value + " " + x.Element("原因及事項").Value).ToList();
            _DeptList = SHDepartment.SelectAll();
            

            // 更正學籍顯示對照用
            _updateCodeDict4 = new Dictionary<string, string>();
                // 進校
                _updateCodeDict4.Add("301", "新學號");
                _updateCodeDict4.Add("302", "新姓名");
                _updateCodeDict4.Add("303", "新性別");
                _updateCodeDict4.Add("304", "新身分證號");
                _updateCodeDict4.Add("305", "新生日");


                //// 讀取畢業學年度
                //SHLeaveInfoRecord shl = SHLeaveInfo.SelectByStudentID(_UpdateRec.StudentID);
                //if (shl.SchoolYear.HasValue)
                //    txtSHSchoolYear.Text = shl.SchoolYear.Value.ToString();

            //// 當延修生要顯示
            //    if (_UpdateRec.GradeYear == "延修生")
            //    {
            //        txtSHSchoolYear.Enabled = true;
            //    }
            //    else
            //        txtSHSchoolYear.Enabled = false;

            
            _UCodeDict = Utility.UITool.ConvertUpdateCodeDescDict(_UpdateCodeElms);

            _faldn = new UpdateRecordModule_SH_N.Utility.FormAndLogDataManager(_prlp);

            if (!string.IsNullOrEmpty(_UpdateRec.NewStudentNumber))
            {
                int i;
                if (int.TryParse(_UpdateRec.NewStudentNumber.Trim(), out i))
                {
                    _UpdateRec.NewData = i.ToString();
                    _UpdateRec.NewStudentNumber = i.ToString();
                }
                else
                    _UpdateRec.NewStudentNumber = "";
            }

            // 載入資料           
            cbxUpdateCode = _faldn.SetFormData(_UpdateRec.UpdateCode, cbxUpdateCode, "異動代碼");
            txtDesc = _faldn.SetFormData(_UpdateRec.UpdateDescription, txtDesc, "原因及事項");
            dtUpdateDate = _faldn.SetFormData(_UpdateRec.UpdateDate, dtUpdateDate, "異動日期");
            txtMemo = _faldn.SetFormData(_UpdateRec.Comment, txtMemo, "備註");
            cbxDept = _faldn.SetFormData(_UpdateRec.Department, cbxDept, "科別");
            cbxClass = _faldn.SetFormData(_UpdateRec.ClassType, cbxClass, "班別");
            txtSHSchoolYear = _faldn.SetFormData(_UpdateRec.SpecialStatus, txtSHSchoolYear, "特殊身分代碼");
            txtName = _faldn.SetFormData(_UpdateRec.StudentName, txtName, "姓名");
            txtStudentNumber = _faldn.SetFormData(_UpdateRec.StudentNumber, txtStudentNumber, "學號");
            txtIDNumber = _faldn.SetFormData(_UpdateRec.IDNumber, txtIDNumber, "身分證字號");
            txtNewData = _faldn.SetFormData(_UpdateRec.NewData, txtNewData, "新資料");
            txtIDNumber1 = _faldn.SetFormData(_UpdateRec.IDNumberComment, txtIDNumber1, "註1");                                    
            dtLastUpdateDate = _faldn.SetFormData(_UpdateRec.LastADDate, dtLastUpdateDate, "備查日期");
            txtLastDocNo = _faldn.SetFormData(_UpdateRec.LastADNumber, txtLastDocNo, "備查文號");
            dtADDate = _faldn.SetFormData(_UpdateRec.ADDate, dtADDate, "核准日期");
            txtADDocNo = _faldn.SetFormData(_UpdateRec.ADNumber, txtADDocNo, "核准文號");
            txtSpecial = _faldn.SetFormData(_UpdateRec.SpecialStatus, txtSpecial, "特殊身分代碼");
            cbxGender = _faldn.SetFormData(_UpdateRec.Gender, cbxGender, "性別");
            dtBirthday = _faldn.SetFormData(_UpdateRec.Birthdate, dtBirthday, "生日");
            
        }

        #region IUpdateRecordInfo 成員

        /// <summary>
        /// 取得異動資料
        /// </summary>
        /// <returns></returns>
        SHUpdateRecordRecord IUpdateRecordInfo.GetStudUpdateRecord()
        {
            // 檢查畫面
            if (string.IsNullOrEmpty(cbxUpdateCode.Text))
                _epUpdateCode.SetError(cbxUpdateCode, "請輸入異動代碼.");

            if (_epUpdateCode.GetError(cbxUpdateCode).Length > 0)
                return null;


            // 取得畫面資料
            _UpdateRec.UpdateCode = _faldn.GetFormData(cbxUpdateCode, "異動代碼");
            _UpdateRec.UpdateDescription = _faldn.GetFormData(txtDesc, "原因及事項");
            _UpdateRec.UpdateDate = _faldn.GetFormData(dtUpdateDate, "異動日期");
            _UpdateRec.Comment = _faldn.GetFormData(txtMemo, "備註");
            _UpdateRec.Department = _faldn.GetFormData(cbxDept, "科別");
            _UpdateRec.ClassType = _faldn.GetFormData(cbxClass, "班別");
            _UpdateRec.SpecialStatus = _faldn.GetFormData(txtSHSchoolYear, "特殊身分代碼");
            _UpdateRec.StudentName = _faldn.GetFormData(txtName, "姓名");
            _UpdateRec.StudentNumber = _faldn.GetFormData(txtStudentNumber, "學號");
            _UpdateRec.IDNumber = _faldn.GetFormData(txtIDNumber, "身分證字號");
            _UpdateRec.NewData = _faldn.GetFormData(txtNewData, "新資料");
            _UpdateRec.IDNumberComment = _faldn.GetFormData(txtIDNumber1, "註1");            
            _UpdateRec.OldClassType = _faldn.GetFormData(cbxOldClass, "舊班別");
            _UpdateRec.OldDepartmentCode = _faldn.GetFormData(cbxOldDeptCode, "舊科別代碼");
            _UpdateRec.LastADDate = _faldn.GetFormData(dtLastUpdateDate, "備查日期");
            _UpdateRec.LastADNumber = _faldn.GetFormData(txtLastDocNo, "備查文號");
            _UpdateRec.ADDate = _faldn.GetFormData(dtADDate, "核准日期");
            _UpdateRec.ADNumber = _faldn.GetFormData(txtADDocNo, "核准文號");
            _UpdateRec.SpecialStatus = _faldn.GetFormData(txtSpecial, "特殊身分代碼");
            _UpdateRec.Birthdate = _faldn.GetFormData(dtBirthday, "生日");
            _UpdateRec.Gender = _faldn.GetFormData(cbxGender, "性別");
            

            // 取得 Log
            _prlp = _faldn.GetLogData();

            return _UpdateRec;
        }

        /// <summary>
        /// 取得 Log資料
        /// </summary>
        /// <returns></returns>
        PermRecLogProcess IUpdateRecordInfo.GetLogData()
        {
            return _prlp;
        }



        #endregion

        private void cbxUpdateCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string code = cbxUpdateCode.Text.Substring(0, 3);
            cbxUpdateCode.Items.Add(code);
            cbxUpdateCode.Text = code;
        }

        private void cbxUpdateCode_TextChanged(object sender, EventArgs e)
        {
            string Code = "";
            if (cbxUpdateCode.Text.Length >= 3)
                Code = cbxUpdateCode.Text.Substring(0, 3);

            if (Code != "")
            {
                if (_UCodeDict.ContainsKey(Code))
                    txtDesc.Text = _UCodeDict[Code];

                // 當有使用更正學籍異動代碼 401~ 才啟用畫面功能                
                if (_updateCodeDict4.ContainsKey(Code))
                {
                    lblNewData.Text = _updateCodeDict4[Code];
                    
                }

                cbxUpdateCode.Text = Code;
            }
            _epUpdateCode.SetError(cbxUpdateCode, "");
        }

        private void cbxUpdateCode_DropDown(object sender, EventArgs e)
        {
            cbxUpdateCode.Items.Clear();
            cbxUpdateCode.Items.AddRange(_UpdateCoodeList.ToArray());
        }

        private void cbxClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = cbxClass.Text.IndexOf("-");
            if (idx > 0)
            {
                string code = cbxClass.Text.Substring(0, idx);
                cbxClass.Items.Add(code);
                cbxClass.Text = code;
            }
        }

        private void cbxClass_DropDown(object sender, EventArgs e)
        {
            cbxClass.Items.Clear();
            cbxClass.Items.AddRange(DAL.DALTransfer.GetClassTypeList().ToArray());
        }

        private void cbxOldClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = cbxOldClass.Text.IndexOf("-");
            if (idx > 0)
            {
                string code = cbxOldClass.Text.Substring(0, idx);
                cbxOldClass.Items.Add(code);
                cbxOldClass.Text = code;
            }
        }

        private void cbxOldClass_DropDown(object sender, EventArgs e)
        {
            cbxOldClass.Items.Clear();
            cbxOldClass.Items.AddRange(DAL.DALTransfer.GetClassTypeList().ToArray());
        }

        private void cbxOldDeptCode_DropDown(object sender, EventArgs e)
        {
            cbxOldDeptCode.Items.Clear();
            cbxOldDeptCode.Items.AddRange((from dept in _DeptList orderby dept.Code select dept.Code + "-" + dept.FullName).ToArray());
        }

        private void cbxDept_DropDown(object sender, EventArgs e)
        {
            cbxDept.Items.Clear();
            cbxDept.Items.AddRange((from dept in _DeptList orderby dept.Code select dept.FullName).ToArray());
        }

        private void cbxOldDeptCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = cbxOldDeptCode.Text.IndexOf("-");
            if (idx > 0)
            {
                string code = cbxOldDeptCode.Text.Substring(0, idx);
                cbxOldDeptCode.Items.Add(code);
                cbxOldDeptCode.Text = code;
            }
        }

        private void cbxUpdateCode_Leave(object sender, EventArgs e)
        {
            // 檢查異動代碼
            if (!_UCodeDict.ContainsKey(cbxUpdateCode.Text))
                _epUpdateCode.SetError(cbxUpdateCode, "異動代碼不在清單內");
        }

    }
}
