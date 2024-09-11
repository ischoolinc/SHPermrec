using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using FISCA.Presentation.Controls;
using SHSchool.Data;
namespace UpdateRecordModule_SH_D.Batch
{
    public partial class BatchUpdateRec : BaseForm
    {
        BackgroundWorker _bgWorkerLoad;
        BackgroundWorker _bgWorkerRun;
        // 學生已有異動
        Dictionary<string, SHUpdateRecordRecord> _StudHasUpdateRecDict = new Dictionary<string, SHUpdateRecordRecord>();
        // 最後一筆異動
        Dictionary<string, SHUpdateRecordRecord> _StudLastupdateRecDict = new Dictionary<string, SHUpdateRecordRecord>();
       
        // 班級
        Dictionary<string, SHClassRecord> _ClassRecDict = new Dictionary<string, SHClassRecord>();

        // 科別
        Dictionary<string, SHDepartmentRecord> _DeptRecDict = new Dictionary<string, SHDepartmentRecord>();
        //多執行續不能使用Form上的控制元件
        List<string> _StudentIDList;
        /// <summary>
        /// 異動日期
        /// </summary>
        DateTime _UpdateDate = DateTime.Now;

        /// <summary>
        /// 班別
        /// </summary>
        string _ClassType;
        /// <summary>
        /// 異動代碼
        /// </summary>
        string _UpdateCodeStr;
        /// <summary>
        /// 異動說明
        /// </summary>
        string _UpdateDesc;
        

        Dictionary<string, SHStudentRecord> _StudentDict = new Dictionary<string, SHStudentRecord>();

        // 新增資料
        List<SHUpdateRecordRecord> _InsertDataList = new List<SHUpdateRecordRecord>();
        // 異動代碼
        XElement _UpdateCode;
        List<XElement> _UpdateCodeElms;
        private List<string> _UpdateCoodeList;
        // 異動代碼索引，給畫面與檢查使用
        Dictionary<string, string> _UCodeDict;
        
        public BatchUpdateRec()
        {
            InitializeComponent();
            // 取得異動代碼
            _UpdateCode = DAL.DALTransfer.GetUpdateCodeList();
            _UpdateCodeElms = (from elm in _UpdateCode.Elements("異動") where elm.Element("代號").Value.Substring(0,1)=="3"  select elm).ToList();
            
            _UpdateCoodeList = (from x in _UpdateCodeElms select x.Element("代號").Value + " " + x.Element("原因及事項").Value).ToList();
            // 建異動索引
            _UCodeDict = Utility.UITool.ConvertUpdateCodeDescDict(_UpdateCodeElms);
            _UpdateCoodeList.Add("235 延修生(一)");
            _UpdateCoodeList.Add("236 延修生(二)");
            _UpdateCoodeList.Add("243 延修生(三)");
            _UpdateCoodeList.Add("244 延修生(四)");
            _UpdateCoodeList.Sort();
            _UCodeDict.Add("235", "延修生(一)");
            _UCodeDict.Add("236", "延修生(二)");
            _UCodeDict.Add("243", "延修生(三)");
            _UCodeDict.Add("244", "延修生(四)");
            _StudentIDList = K12.Presentation.NLDPanels.Student.SelectedSource;
            _bgWorkerLoad = new BackgroundWorker();
            _bgWorkerLoad.DoWork += new DoWorkEventHandler(_bgWorkerLoad_DoWork);
            _bgWorkerLoad.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorkerLoad_RunWorkerCompleted);
            _bgWorkerRun = new BackgroundWorker();
            _bgWorkerRun.DoWork += new DoWorkEventHandler(_bgWorkerRun_DoWork);
            _bgWorkerRun.WorkerReportsProgress = true;
            _bgWorkerRun.ProgressChanged += new ProgressChangedEventHandler(_bgWorkerRun_ProgressChanged);
            _bgWorkerRun.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorkerRun_RunWorkerCompleted);
        }
       
        private void cboUpdateCode_DropDown(object sender, EventArgs e)
        {
            cboUpdateCode.Items.Clear();
            cboUpdateCode.Items.AddRange(_UpdateCoodeList.ToArray());
        }
        void _bgWorkerRun_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FISCA.Presentation.MotherForm.SetStatusBarMessage("批次學籍異動產生中", e.ProgressPercentage);
        }

        void _bgWorkerRun_DoWork(object sender, DoWorkEventArgs e)
        {
            // 處理資料
            try
            {
                _bgWorkerRun.ReportProgress(1);
                // 刪除舊資料
                //List<SHUpdateRecordRecord> _oldDataList = _StudHasUpdateRecDict.Values.ToList();
                //SHUpdateRecord.Delete(_oldDataList);
                int SchoolYear = int.Parse(K12.Data.School.DefaultSchoolYear);
                int Semester = int.Parse(K12.Data.School.DefaultSemester);


                // 學籍身分對照表
                Dictionary<string, List<string>> StudPermCodeMappingDict = utility.GetPermCodeMappingDict();

                // 取得學生類別對照
                Dictionary<string, List<string>> StudentTagDict = utility.GetStudentFullNameDictByStudentIDs(_StudentIDList);

                _bgWorkerRun.ReportProgress(10);
                foreach (string sid in _StudentIDList)
                {
                    if (!_StudentDict.ContainsKey(sid))
                        continue;

                    SHUpdateRecordRecord rec = new SHUpdateRecordRecord();
                    // 學年度
                    rec.SchoolYear = SchoolYear;
                    // 學期
                    rec.Semester = Semester;

                    rec.StudentID = sid;

                    string ClassName = "";
                    string DeptName = "";

                    // 年級
                    
                    if (_ClassRecDict.ContainsKey(_StudentDict[sid].RefClassID))
                    {
                        if (_ClassRecDict[_StudentDict[sid].RefClassID].GradeYear.HasValue)
                        {
                            if (_UpdateCodeStr.Substring(0, 1) == "2")
                                rec.GradeYear = "延修生";
                            else 
                                if (_ClassRecDict[_StudentDict[sid].RefClassID].GradeYear.Value>3 || _ClassRecDict[_StudentDict[sid].RefClassID].GradeYear.Value ==0)
                                   rec.GradeYear = "延修生";
                                else
                                   rec.GradeYear = _ClassRecDict[_StudentDict[sid].RefClassID].GradeYear.Value.ToString();

                            // 班級名稱
                            ClassName = _ClassRecDict[_StudentDict[sid].RefClassID].Name;
                        }
                        else
                            if (_UpdateCodeStr.Substring(0, 1) == "2")
                               rec.GradeYear = "延修生";
                            else
                               rec.GradeYear = "";
                    }
                    string depID = _StudentDict[sid].DepartmentID;
                    // 科別名稱
                    if (_DeptRecDict.ContainsKey(depID))
                        DeptName = _DeptRecDict[depID].FullName;

                    // 科別名稱
                    if (DeptName=="")
                      if (_DeptRecDict.ContainsKey(_ClassRecDict[_StudentDict[sid].RefClassID].RefDepartmentID))
                           DeptName = _DeptRecDict[_ClassRecDict[_StudentDict[sid].RefClassID].RefDepartmentID].FullName;
                    rec.Department= DeptName;
                    // 異動代碼
                    rec.UpdateCode = _UpdateCodeStr;

                    // 原因及事項
                    rec.UpdateDescription = _UpdateDesc;

                    // 異動日期
                    rec.UpdateDate = _UpdateDate.ToShortDateString();
                    
                    
                    // 班別
                    rec.ClassType = _ClassType;

                    // 姓名
                    rec.StudentName = _StudentDict[sid].Name;

                    // 學號
                    rec.StudentNumber = _StudentDict[sid].StudentNumber;

                    // 身分證字號
                    rec.IDNumber = _StudentDict[sid].IDNumber;

                    // 生日
                    if (_StudentDict[sid].Birthday.HasValue)
                        rec.Birthdate = _StudentDict[sid].Birthday.Value.ToShortDateString();

                    // 性別
                    rec.Gender = _StudentDict[sid].Gender;

                    //輔導延修
                    if (_UpdateCodeStr == "364")
                        rec.ExpectGraduateSchoolYear = rec.SchoolYear.ToString();

                    if (_StudLastupdateRecDict.ContainsKey(sid))
                    {
                        // 年級
                        if (rec.GradeYear == "")
                            rec.GradeYear = _StudLastupdateRecDict[sid].GradeYear;
                        //應畢業年度
                        rec.ExpectGraduateSchoolYear = _StudLastupdateRecDict[sid].ExpectGraduateSchoolYear;
                        // 備查日期
                        rec.LastADDate = _StudLastupdateRecDict[sid].ADDate;
                            
                        // 備查文號
                        rec.LastADNumber = _StudLastupdateRecDict[sid].ADNumber;

                        // 最後異動代碼
                        rec.LastUpdateCode = _StudLastupdateRecDict[sid].UpdateCode;
                        // 原臨編日期
                        rec.OriginalTempDate = _StudLastupdateRecDict[sid].TempDate;
                        // 原臨編學統
                        rec.OriginalTempDesc = _StudLastupdateRecDict[sid].TempDesc;
                        // 原臨編字號
                        rec.OriginalTempNumber = _StudLastupdateRecDict[sid].TempNumber;
                    }
                   

                    // 學生特殊身分代碼
                    if (StudentTagDict.ContainsKey(sid))
                    {
                        List<string> codeList = new List<string>();
                        foreach (string fullName in StudentTagDict[sid])
                        {
                            if (StudPermCodeMappingDict.ContainsKey(fullName))
                            {
                                foreach (string code in StudPermCodeMappingDict[fullName])
                                    if (!codeList.Contains(code))
                                        codeList.Add(code);
                            }
                        }

                        if (codeList.Count > 0)
                        {
                            codeList.Sort();
                            rec.SpecialStatus = string.Join(",", codeList.ToArray());
                        }
                    }

                    _InsertDataList.Add(rec);

                }
                _bgWorkerRun.ReportProgress(70);
                // 新增資料
                if (_InsertDataList.Count > 0)
                    SHUpdateRecord.Insert(_InsertDataList);

                

                _bgWorkerRun.ReportProgress(99);
            }
            catch (Exception ex)
            {
                e.Result = ex;
                e.Cancel = true;
            }
        }

        void _bgWorkerRun_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Exception ex = e.Result as Exception;
                FISCA.Presentation.Controls.MsgBox.Show("產生過程發生錯誤:" + ex.Message);
            }
            else
            {
                SmartSchool.StudentRelated.Student.Instance.SyncDataBackground(_StudentIDList);
                FISCA.Presentation.Controls.MsgBox.Show("產生完成" + _InsertDataList.Count + "筆資料");
            }
            FISCA.Presentation.MotherForm.SetStatusBarMessage("批次學籍異動產生完成.", 100);
            this.Close();
        }

        void _bgWorkerLoad_DoWork(object sender, DoWorkEventArgs e)
        {
            // 取得就學生異動資料
            List<SHUpdateRecordRecord> recList = SHUpdateRecord.SelectByStudentIDs(_StudentIDList);
            // 取得最後一筆異動
            _StudLastupdateRecDict.Clear();
            foreach (string sid in _StudentIDList)
            {
                List<SHUpdateRecordRecord> recL = (from data in recList where data.StudentID == sid && data.ADDate.Trim() != "" orderby DateTime.Parse(data.ADDate) descending, int.Parse(data.ID) descending select data).ToList();
                if (recL.Count > 0)
                {
                    SHUpdateRecordRecord r1 = recL[0];
                    if (!_StudLastupdateRecDict.ContainsKey(r1.StudentID))
                        _StudLastupdateRecDict.Add(r1.StudentID, r1);
                }
            }

            // 學生資料
            _StudentDict.Clear();
            foreach (SHStudentRecord rec in SHStudent.SelectByIDs(_StudentIDList))
                _StudentDict.Add(rec.ID, rec);

           
            // 班級
            _ClassRecDict.Clear();
            foreach (SHClassRecord rec in SHClass.SelectAll())
                if (!_ClassRecDict.ContainsKey(rec.ID))
                    _ClassRecDict.Add(rec.ID, rec);

            // 科別
            _DeptRecDict.Clear();
            foreach (SHDepartmentRecord rec in SHDepartment.SelectAll())
                if (!_DeptRecDict.ContainsKey(rec.ID))
                    _DeptRecDict.Add(rec.ID, rec);

            

        }

        void _bgWorkerLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnRun.Enabled = true;
        }
        private void cboUpdateCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = cboUpdateCode.Text.IndexOf(" ");
            txtUpdateDesc.Text = "";
            if (idx > 0)
            {
                string code = cboUpdateCode.Text.Substring(0, idx);
                cboUpdateCode.Items.Add(code);
                cboUpdateCode.Text = code;
            }
            if (_UCodeDict.ContainsKey(cboUpdateCode.Text))
            {
                txtUpdateDesc.Text = _UCodeDict[cboUpdateCode.Text].ToString();
            }
           
        }

        private void BatchUpdateRec_Load(object sender, EventArgs e)
        {
            this.btnRun.Enabled = false;
            dtUpdate.Value = DateTime.Now;
            _bgWorkerLoad.RunWorkerAsync();
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

        private void btnRun_Click(object sender, EventArgs e)
        {
            btnRun.Enabled = false;
            // 檢查異動日期
            if (dtUpdate.IsEmpty)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請輸入異動日期!");
                return;
            }
            if (cboUpdateCode.Text.Length < 3)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請輸入異動代碼!");
                return;
            }
            if (txtUpdateDesc.Text.Length<1)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請輸入異動原因及說明!");
                return;
            }
            _UpdateDate = dtUpdate.Value;
            _ClassType = cbxClassType.Text;
            _UpdateCodeStr = cboUpdateCode.Text;
            _UpdateDesc = txtUpdateDesc.Text;
            // 取得就學生異動資料
            List<SHUpdateRecordRecord> recList = SHUpdateRecord.SelectByStudentIDs(_StudentIDList);
            //檢查該異動是否已存在
            _StudHasUpdateRecDict.Clear();
            foreach (SHUpdateRecordRecord rec in recList)
            {
                // 不是選擇的異動跳過
                if (rec.UpdateCode != _UpdateCodeStr || (rec.ADNumber!="" || rec.TempNumber!=""))
                    continue;

                if (!_StudHasUpdateRecDict.ContainsKey(rec.StudentID))
                    _StudHasUpdateRecDict.Add(rec.StudentID, rec);
            }
            // 檢查是否已有資料
            if (_StudHasUpdateRecDict.Count > 0)
            {
                BatchUpdateRec_WarningForm war = new BatchUpdateRec_WarningForm(_ClassRecDict, _StudentDict, _StudHasUpdateRecDict.Values.ToList());
                if (war.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                {
                    _bgWorkerRun.RunWorkerAsync();
                }
                else
                    this.Close();

            }
            else
                _bgWorkerRun.RunWorkerAsync();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
