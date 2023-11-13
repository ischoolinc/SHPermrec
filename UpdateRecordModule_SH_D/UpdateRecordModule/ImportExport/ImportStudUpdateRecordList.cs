﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Campus.Import;
using Campus.DocumentValidator;
using SHSchool.Data;

namespace UpdateRecordModule_SH_D.ImportExport
{
    public class ImportStudUpdateRecordList : ImportWizard
    {
        ImportOption mOption;
        // 新增
        List<SHUpdateRecordRecord> _InsertRecList;
        // 更新
        List<SHUpdateRecordRecord> _UpdateRecList;

        public ImportStudUpdateRecordList()
        {
            this.IsSplit = false;
            this.IsLog = false;
        }

        public override ImportAction GetSupportActions()
        {
            return ImportAction.InsertOrUpdate;
        }

        public override string GetValidateRule()
        {
            return Properties.Resources.ImportStudUpdateRecordListVal.ToString();
        }

        public override string Import(List<IRowStream> Rows)
        {

            _InsertRecList.Clear();
            _UpdateRecList.Clear();
            // 取得 Rows內學號
            List<string> StudentNumberList = new List<string>();
            foreach (IRowStream ir in Rows)
                if (ir.Contains("學號"))
                    StudentNumberList.Add(ir.GetValue("學號"));

            // 透過學號去取得學生ID
            Dictionary<string, string> StudNumDict = DAL.FDQuery.GetStudenNumberStatusDictByStudentNumber(StudentNumberList);

            List<string> StudentIDList = new List<string>();
            foreach (IRowStream ir in Rows)
            {
                if (ir.Contains("學號") && ir.Contains("狀態"))
                {
                    string key = ir.GetValue("學號") + "_" + ir.GetValue("狀態");
                    if (StudNumDict.ContainsKey(key))
                        StudentIDList.Add(StudNumDict[key]);
                }

            }

            List<string> updateCodeList = Utility.UITool.GetUpdateCodeListByUpdateType("學籍異動");
            // 取得學籍異動資料
            List<SHUpdateRecordRecord> updateRecList = (from data in SHUpdateRecord.SelectByStudentIDs(StudentIDList) where updateCodeList.Contains(data.UpdateCode) select data).ToList();

            int totalCount = 0;
            // 判斷更新或新增
            foreach (IRowStream ir in Rows)
            {
                totalCount++;
                this.ImportProgress = totalCount;
                bool isInsert = true;
                SHUpdateRecordRecord UpdateRec = null;
                string StudentID = "";

                if (ir.Contains("學號") && ir.Contains("狀態"))
                {
                    string key = ir.GetValue("學號") + "_" + ir.GetValue("狀態");
                    if (StudNumDict.ContainsKey(key))
                        StudentID = StudNumDict[key];
                }

                if (string.IsNullOrEmpty(StudentID))
                    continue;

                DateTime dt;
                DateTime.TryParse(ir.GetValue("異動日期"), out dt);

                foreach (SHUpdateRecordRecord rec in updateRecList.Where(x => x.StudentID == StudentID))
                {
                    string updateCode = string.Format("{0:000}", int.Parse(ir.GetValue("異動代碼").Trim()));

                    if (rec.UpdateCode.Trim() == updateCode)
                    {
                        DateTime dt1;
                        DateTime.TryParse(rec.UpdateDate, out dt1);

                        if (dt.ToShortDateString() == dt1.ToShortDateString())
                        {
                            isInsert = false;
                            UpdateRec = rec;
                        }
                    }
            
                }

                if (isInsert || UpdateRec == null)
                    UpdateRec = new SHUpdateRecordRecord();

                UpdateRec.StudentID = StudentID;

                if (isInsert)
                {

                    int sy, ss;
                    // 學年度
                    if (ir.Contains("學年度"))
                    {
                        if (int.TryParse(ir.GetValue("學年度"), out sy))
                            UpdateRec.SchoolYear = sy;

                        if (string.IsNullOrEmpty(ir.GetValue("學年度")))
                            UpdateRec.SchoolYear = null;

                    }
                    // 學期
                    if (ir.Contains("學期"))
                    {
                        if (int.TryParse(ir.GetValue("學期"), out ss))
                            UpdateRec.Semester = ss;

                        if (string.IsNullOrEmpty(ir.GetValue("學期")))
                            UpdateRec.Semester = null;
                    }
                    // 異動年級
                    if (ir.Contains("異動年級"))
                        UpdateRec.GradeYear = ir.GetValue("異動年級");

                    // 異動代碼
                    UpdateRec.UpdateCode = string.Format("{0:000}", int.Parse(ir.GetValue("異動代碼").Trim()));

                    // 原因及事項
                    if (ir.Contains("原因及事項"))
                        UpdateRec.UpdateDescription = ir.GetValue("原因及事項");


                    // 異動日期
                    UpdateRec.UpdateDate = dt.ToShortDateString();

                    // 備註
                    if (ir.Contains("備註"))
                        UpdateRec.Comment = ir.GetValue("備註");

                    // 班別
                    if (ir.Contains("班別"))
                        UpdateRec.ClassType = ir.GetValue("班別");

                    // 科別
                    if (ir.Contains("科別"))
                        UpdateRec.Department = ir.GetValue("科別");

                    // 特殊身分代碼
                    if (ir.Contains("特殊身分代碼"))
                        UpdateRec.SpecialStatus = ir.GetValue("特殊身分代碼");

                    // 入學資格證明文件
                    if (ir.Contains("入學資格證明文件"))
                        UpdateRec.GraduateDocument = ir.GetValue("入學資格證明文件");

                    // 異動姓名
                    if (ir.Contains("異動姓名"))
                        UpdateRec.StudentName = ir.GetValue("異動姓名");

                    // 異動學號
                    if (ir.Contains("異動學號"))
                        UpdateRec.StudentNumber = ir.GetValue("異動學號");

                    // 異動身分證字號
                    if (ir.Contains("異動身分證字號"))
                        UpdateRec.IDNumber = ir.GetValue("異動身分證字號");

                    // 更正後資料
                    if (ir.Contains("更正後資料"))
                        UpdateRec.NewData = ir.GetValue("更正後資料");

                    // 異動生日
                    if (ir.Contains("異動生日"))
                    {
                        DateTime dtd;
                        if (DateTime.TryParse(ir.GetValue("異動生日"), out dtd))
                            UpdateRec.Birthdate = dtd.ToShortDateString();

                        if (string.IsNullOrEmpty(ir.GetValue("異動生日")))
                            UpdateRec.Birthdate = "";
                    }
                    // 異動身分證註記
                    if (ir.Contains("異動身分證註記"))
                        UpdateRec.IDNumberComment = ir.GetValue("異動身分證註記");

                    // 異動性別
                    if (ir.Contains("異動性別"))
                        UpdateRec.Gender = ir.GetValue("異動性別");

                    // 應畢業學年度
                    if (ir.Contains("應畢業學年度"))
                        UpdateRec.ExpectGraduateSchoolYear = ir.GetValue("應畢業學年度");

                    // 更正後身分證註記
                    if (ir.Contains("更正後身分證註記"))
                        UpdateRec.Comment2 = ir.GetValue("更正後身分證註記");

                    // 舊科別代碼
                    if (ir.Contains("舊科別代碼"))
                        UpdateRec.OldDepartmentCode = ir.GetValue("舊科別代碼");

                    // 舊班別
                    if (ir.Contains("舊班別"))
                        UpdateRec.OldClassType = ir.GetValue("舊班別");


                    // 備查日期
                    if (ir.Contains("備查日期"))
                    {
                        DateTime dta;
                        if (DateTime.TryParse(ir.GetValue("備查日期"), out dta))
                            UpdateRec.LastADDate = dta.ToShortDateString();

                        if (string.IsNullOrEmpty(ir.GetValue("備查日期")))
                            UpdateRec.LastADDate = "";
                    }
                    // 備查文號
                    if (ir.Contains("備查文號"))
                        UpdateRec.LastADNumber = ir.GetValue("備查文號");
                    // 原臨編日期
                    if (ir.Contains("原臨編日期"))
                    {
                        DateTime dta;
                        if (DateTime.TryParse(ir.GetValue("原臨編日期"), out dta))
                            UpdateRec.OriginalTempDate = dta.ToShortDateString();

                        if (string.IsNullOrEmpty(ir.GetValue("原臨編日期")))
                            UpdateRec.OriginalTempDate = "";

                    }
                    // 原臨編學統
                    if (ir.Contains("原臨編學統"))
                        UpdateRec.OriginalTempDesc = ir.GetValue("原臨編學統");
                    // 臨編字號
                    if (ir.Contains("原臨編字號"))
                        UpdateRec.OriginalTempNumber = ir.GetValue("原臨編字號");


                    // 核准日期
                    if (ir.Contains("核准日期"))
                    {
                        DateTime dta;
                        if (DateTime.TryParse(ir.GetValue("核准日期"), out dta))
                            UpdateRec.ADDate = dta.ToShortDateString();

                        if (string.IsNullOrEmpty(ir.GetValue("核准日期")))
                            UpdateRec.ADDate = "";
                    }
                    // 核准文號
                    if (ir.Contains("核准文號"))
                        UpdateRec.ADNumber = ir.GetValue("核准文號");
                    // 臨編日期
                    if (ir.Contains("臨編日期"))
                    {
                        DateTime dta;
                        if (DateTime.TryParse(ir.GetValue("臨編日期"), out dta))
                            UpdateRec.TempDate = dta.ToShortDateString();

                        if (string.IsNullOrEmpty(ir.GetValue("臨編日期")))
                            UpdateRec.TempDate = "";

                    }
                    // 臨編學統
                    if (ir.Contains("臨編學統"))
                        UpdateRec.TempDesc = ir.GetValue("臨編學統");
                    // 臨編字號
                    if (ir.Contains("臨編字號"))
                        UpdateRec.TempNumber = ir.GetValue("臨編字號");
                    // 雙重學籍編號
                    if (ir.Contains("雙重學籍編號"))
                    {
                        UpdateRec.ReplicatedSchoolRollNumber = ir.GetValue("雙重學籍編號");  
                    }
                    

                    // 借讀學校代碼
                    if (ir.Contains("借讀學校代碼"))
                        UpdateRec.Code7SchoolCode = ir.GetValue("借讀學校代碼");
                    // 借讀科別代碼
                    if (ir.Contains("借讀科別代碼"))
                        UpdateRec.Code7DeptCode = ir.GetValue("借讀科別代碼");
                    // 申請開始日期
                    if (ir.Contains("申請開始日期"))
                        UpdateRec.Code71BeginDate = ir.GetValue("申請開始日期");
                    // 申請結束日期
                    if (ir.Contains("申請結束日期"))
                        UpdateRec.Code71EndDate = ir.GetValue("申請結束日期");
                    // 實際開始日期
                    if (ir.Contains("實際開始日期"))
                        UpdateRec.Code72BeginDate = ir.GetValue("實際開始日期");
                    // 實際結束日期
                    if (ir.Contains("實際結束日期"))
                        UpdateRec.Code72EndDate = ir.GetValue("實際結束日期");

                }
                else
                {
                    // 更新有勾選
                    int sy, ss;
                    // 學年度
                    if (ir.Contains("學年度") && mOption.SelectedFields.Contains("學年度"))
                    {
                        if (int.TryParse(ir.GetValue("學年度"), out sy))
                            UpdateRec.SchoolYear = sy;

                        if (string.IsNullOrEmpty(ir.GetValue("學年度")))
                            UpdateRec.SchoolYear = null;
                    }

                    // 學期
                    if (ir.Contains("學期") && mOption.SelectedFields.Contains("學期"))
                    {
                        if (int.TryParse(ir.GetValue("學期"), out ss))
                            UpdateRec.Semester = ss;

                        if (string.IsNullOrEmpty(ir.GetValue("學期")))
                            UpdateRec.Semester = null;
                    }

                    // 異動年級
                    if (ir.Contains("異動年級") && mOption.SelectedFields.Contains("異動年級"))
                        UpdateRec.GradeYear = ir.GetValue("異動年級");

                    //// 異動代碼
                    //UpdateRec.UpdateCode = ir.GetValue("異動代碼").Trim();

                    // 原因及事項
                    if (ir.Contains("原因及事項") && mOption.SelectedFields.Contains("原因及事項"))
                        UpdateRec.UpdateDescription = ir.GetValue("原因及事項");


                    //// 異動日期
                    //UpdateRec.UpdateDate = dt.ToShortDateString();

                    // 備註
                    if (ir.Contains("備註") && mOption.SelectedFields.Contains("備註"))
                        UpdateRec.Comment = ir.GetValue("備註");

                    // 班別
                    if (ir.Contains("班別") && mOption.SelectedFields.Contains("班別"))
                        UpdateRec.ClassType = ir.GetValue("班別");

                    // 科別
                    if (ir.Contains("科別") && mOption.SelectedFields.Contains("科別"))
                        UpdateRec.Department = ir.GetValue("科別");

                    // 特殊身分代碼
                    if (ir.Contains("特殊身分代碼") && mOption.SelectedFields.Contains("特殊身分代碼"))
                        UpdateRec.SpecialStatus = ir.GetValue("特殊身分代碼");

                    // 入學資格證明文件
                    if (ir.Contains("入學資格證明文件") && mOption.SelectedFields.Contains("入學資格證明文件"))
                        UpdateRec.GraduateDocument = ir.GetValue("入學資格證明文件");

                    // 異動姓名
                    if (ir.Contains("異動姓名") && mOption.SelectedFields.Contains("異動姓名"))
                        UpdateRec.StudentName = ir.GetValue("異動姓名");

                    // 異動學號
                    if (ir.Contains("異動學號") && mOption.SelectedFields.Contains("異動學號"))
                        UpdateRec.StudentNumber = ir.GetValue("異動學號");

                    // 異動身分證字號
                    if (ir.Contains("異動身分證字號") && mOption.SelectedFields.Contains("異動身分證字號"))
                        UpdateRec.IDNumber = ir.GetValue("異動身分證字號");

                    // 異動生日
                    if (ir.Contains("異動生日") && mOption.SelectedFields.Contains("異動生日"))
                    {
                        DateTime dtd;
                        if (DateTime.TryParse(ir.GetValue("異動生日"), out dtd))
                            UpdateRec.Birthdate = dtd.ToShortDateString();

                        if(string.IsNullOrEmpty(ir.GetValue("異動生日")))
                            UpdateRec.Birthdate ="";
                    }

                    // 更正後資料
                    if (ir.Contains("更正後資料") && mOption.SelectedFields.Contains("更正後資料"))
                        UpdateRec.NewData = ir.GetValue("更正後資料");


                    // 異動身分證註記
                    if (ir.Contains("異動身分證註記") && mOption.SelectedFields.Contains("異動身分證註記"))
                        UpdateRec.IDNumberComment = ir.GetValue("異動身分證註記");

                    // 異動性別
                    if (ir.Contains("異動性別") && mOption.SelectedFields.Contains("異動性別"))
                        UpdateRec.Gender = ir.GetValue("異動性別");

                    // 應畢業學年度
                    if (ir.Contains("應畢業學年度") && mOption.SelectedFields.Contains("應畢業學年度"))
                        UpdateRec.ExpectGraduateSchoolYear = ir.GetValue("應畢業學年度");

                    // 更正後身分證註記
                    if (ir.Contains("更正後身分證註記") && mOption.SelectedFields.Contains("更正後身分證註記"))
                        UpdateRec.Comment2 = ir.GetValue("更正後身分證註記");

                    // 舊科別代碼
                    if (ir.Contains("舊科別代碼") && mOption.SelectedFields.Contains("舊科別代碼"))
                        UpdateRec.OldDepartmentCode = ir.GetValue("舊科別代碼");

                    // 舊班別
                    if (ir.Contains("舊班別") && mOption.SelectedFields.Contains("舊班別"))
                        UpdateRec.OldClassType = ir.GetValue("舊班別");


                    // 備查日期
                    if (ir.Contains("備查日期") && mOption.SelectedFields.Contains("備查日期"))
                    {
                        DateTime dta;
                        if (DateTime.TryParse(ir.GetValue("備查日期"), out dta))
                            UpdateRec.LastADDate = dta.ToShortDateString();

                        if (string.IsNullOrEmpty(ir.GetValue("備查日期")))
                            UpdateRec.LastADDate = "";
                    }
                    // 備查文號
                    if (ir.Contains("備查文號") && mOption.SelectedFields.Contains("備查文號"))
                        UpdateRec.LastADNumber = ir.GetValue("備查文號");

                    // 原臨編日期
                    if (ir.Contains("原臨編日期"))
                    {
                        DateTime dta;
                        if (DateTime.TryParse(ir.GetValue("原臨編日期"), out dta))
                            UpdateRec.OriginalTempDate = dta.ToShortDateString();

                        if (string.IsNullOrEmpty(ir.GetValue("原臨編日期")))
                            UpdateRec.OriginalTempDate = "";

                    }
                    // 原臨編學統
                    if (ir.Contains("原臨編學統"))
                        UpdateRec.OriginalTempDesc = ir.GetValue("原臨編學統");
                    // 臨編字號
                    if (ir.Contains("原臨編字號"))
                        UpdateRec.OriginalTempNumber = ir.GetValue("原臨編字號");
                    // 核准日期
                    if (ir.Contains("核准日期") && mOption.SelectedFields.Contains("核准日期"))
                    {
                        DateTime dta;
                        if (DateTime.TryParse(ir.GetValue("核准日期"), out dta))
                            UpdateRec.ADDate = dta.ToShortDateString();

                        if (string.IsNullOrEmpty(ir.GetValue("核准日期")))
                            UpdateRec.ADDate = "";
                    }
                    // 臨編日期
                    if (ir.Contains("臨編日期"))
                    {
                        DateTime dta;
                        if (DateTime.TryParse(ir.GetValue("臨編日期"), out dta))
                            UpdateRec.TempDate = dta.ToShortDateString();

                        if (string.IsNullOrEmpty(ir.GetValue("臨編日期")))
                            UpdateRec.TempDate = "";

                    }
                    // 臨編學統
                    if (ir.Contains("臨編學統"))
                        UpdateRec.TempDesc = ir.GetValue("臨編學統");
                    // 臨編字號
                    if (ir.Contains("臨編字號"))
                        UpdateRec.TempNumber = ir.GetValue("臨編字號");
                    // 雙重學籍編號
                    if (ir.Contains("雙重學籍編號"))
                    {
                        UpdateRec.ReplicatedSchoolRollNumber = ir.GetValue("雙重學籍編號");
                    }
                    // 核准文號
                    if (ir.Contains("核准文號") && mOption.SelectedFields.Contains("核准文號"))
                        UpdateRec.ADNumber = ir.GetValue("核准文號");

                    // 借讀學校代碼
                    if (ir.Contains("借讀學校代碼") && mOption.SelectedFields.Contains("借讀學校代碼"))
                        UpdateRec.Code7SchoolCode = ir.GetValue("借讀學校代碼");

                    // 借讀科別代碼
                    if (ir.Contains("借讀科別代碼") && mOption.SelectedFields.Contains("借讀科別代碼"))
                        UpdateRec.Code7DeptCode = ir.GetValue("借讀科別代碼");

                    // 申請開始日期
                    if (ir.Contains("申請開始日期") && mOption.SelectedFields.Contains("申請開始日期"))
                        UpdateRec.Code71BeginDate = ir.GetValue("申請開始日期");

                    // 申請結束日期
                    if (ir.Contains("申請結束日期") && mOption.SelectedFields.Contains("申請結束日期"))
                        UpdateRec.Code71EndDate = ir.GetValue("申請結束日期");

                    // 實際開始日期
                    if (ir.Contains("實際開始日期") && mOption.SelectedFields.Contains("實際開始日期"))
                        UpdateRec.Code72BeginDate = ir.GetValue("實際開始日期");

                    // 實際結束日期
                    if (ir.Contains("實際結束日期") && mOption.SelectedFields.Contains("實際結束日期"))
                        UpdateRec.Code72EndDate = ir.GetValue("實際結束日期");
                }


                if (isInsert)
                    _InsertRecList.Add(UpdateRec);
                else
                    _UpdateRecList.Add(UpdateRec);
            }

            // 執行更新或新增
            if (_InsertRecList.Count > 0)
                SHUpdateRecord.Insert(_InsertRecList);

            if (_UpdateRecList.Count > 0)
                SHUpdateRecord.Update(_UpdateRecList);
            
            return "";
        }

        public override void Prepare(ImportOption Option)
        {
            mOption = Option;
            _InsertRecList = new List<SHUpdateRecordRecord>();
            _UpdateRecList = new List<SHUpdateRecordRecord>();
        }
    }
}
