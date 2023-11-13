﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartSchool.API.PlugIn;
using SHSchool.Data;
using System.Xml.Linq;

namespace UpdateRecordModule_SH_D.ImportExport
{
    /// <summary>
    /// 匯出轉入異動
    /// </summary>
    public class ExportTransferList : SmartSchool.API.PlugIn.Export.Exporter
    {
        /// <summary>
        /// 可匯出項目
        /// </summary>
        List<string> _ExportList;

        public ExportTransferList()
        {

            _ExportList = new List<string>();
            this.Image = null;
            this.Text = "匯出轉入異動";

            _ExportList.Add("學年度");
            _ExportList.Add("學期");
            _ExportList.Add("異動年級");
            _ExportList.Add("異動代碼");
            _ExportList.Add("原因及事項");
            _ExportList.Add("異動日期");
            _ExportList.Add("備註");
            _ExportList.Add("班別");
            _ExportList.Add("科別");
            _ExportList.Add("特殊身分代碼");
            _ExportList.Add("異動姓名");
            _ExportList.Add("異動學號");
            _ExportList.Add("異動身分證字號");
            _ExportList.Add("異動生日");
            _ExportList.Add("異動性別");
            _ExportList.Add("異動身分證註記");            
            _ExportList.Add("原就讀學校");
            _ExportList.Add("原就讀學號");
            _ExportList.Add("原就讀科別");
            _ExportList.Add("原就讀年級");
            _ExportList.Add("原就讀學期");
            _ExportList.Add("原就讀備查日期");
            _ExportList.Add("原就讀備查文號");
            _ExportList.Add("核准日期");
            _ExportList.Add("核准文號");
            _ExportList.Add("臨編日期");
            _ExportList.Add("臨編學統");
            _ExportList.Add("臨編字號");
            _ExportList.Add("轉入身分別代碼");
            _ExportList.Add("建教僑生專班學生國別");
            _ExportList.Add("狀態");
        }


        public override void InitializeExport(SmartSchool.API.PlugIn.Export.ExportWizard wizard)
        {
            wizard.ExportableFields.AddRange(_ExportList);
            wizard.ExportPackage += delegate(object sender, SmartSchool.API.PlugIn.Export.ExportPackageEventArgs e)
            {
                // 取得異動代碼
                XElement _UpdateCode = DAL.DALTransfer.GetUpdateCodeList();
                List<string> UpdateCodeList = (from elm in _UpdateCode.Elements("異動") where elm.Element("分類").Value == "轉入異動" select elm.Element("代號").Value).ToList();
                // 取得學生轉入異動
                List<SHUpdateRecordRecord> updateRecList = (from data in SHUpdateRecord.SelectByStudentIDs(e.List) where UpdateCodeList.Contains(data.UpdateCode) select data).ToList();
                Dictionary<string, string> StudentStatusDict = DAL.FDQuery.GetAllStudentStatus1Dict();

                foreach (SHUpdateRecordRecord rec in updateRecList)
                {
                    RowData row = new RowData();
                    row.ID = rec.StudentID;
                    foreach (string field in e.ExportFields)
                    {
                        if (wizard.ExportableFields.Contains(field))
                        {
                            switch (field)
                            {
                                case "學年度":
                                    if (rec.SchoolYear.HasValue)
                                        row.Add(field, rec.SchoolYear.Value.ToString()); break;
                                case "學期":
                                    if (rec.Semester.HasValue)
                                        row.Add(field, rec.Semester.Value.ToString()); break;
                                case "異動年級": row.Add(field, rec.GradeYear); break;
                                case "異動代碼": row.Add(field, rec.UpdateCode); break;
                                case "原因及事項": row.Add(field, rec.UpdateDescription); break;
                                case "異動日期": row.Add(field, rec.UpdateDate); break;
                                case "備註": row.Add(field, rec.Comment); break;
                                case "班別": row.Add(field, rec.ClassType); break;
                                case "科別": row.Add(field, rec.Department); break;
                                case "特殊身分代碼": row.Add(field, rec.SpecialStatus); break;                                
                                case "異動姓名": row.Add(field, rec.StudentName); break;
                                case "異動學號": row.Add(field, rec.StudentNumber); break;
                                case "異動身分證字號": row.Add(field, rec.IDNumber); break;
                                case "異動生日": row.Add(field, rec.Birthdate); break;
                                case "異動身分證註記": row.Add(field, rec.IDNumberComment); break;
                                case "轉入身分別代碼": row.Add(field, rec.Comment2); break;
                                case "異動性別": row.Add(field, rec.Gender); break;
                                case "原就讀學校": row.Add(field, rec.PreviousSchool); break;
                                case "原就讀學號": row.Add(field, rec.PreviousStudentNumber); break;
                                case "原就讀科別": row.Add(field, rec.PreviousDepartment); break;
                                case "原就讀年級": row.Add(field, rec.PreviousGradeYear); break;
                                case "原就讀學期": row.Add(field, rec.PreviousSemester); break;
                                case "原就讀備查日期": row.Add(field, rec.PreviousSchoolLastADDate); break;
                                case "原就讀備查文號": row.Add(field, rec.PreviousSchoolLastADNumber); break;
                                
                                case "核准日期": row.Add(field, rec.ADDate); break;
                                case "核准文號": row.Add(field, rec.ADNumber); break;
                                case "臨編日期": row.Add(field, rec.TempDate); break;
                                case "臨編學統": row.Add(field, rec.TempDesc); break;
                                case "臨編字號": row.Add(field, rec.TempNumber); break;
                                case "建教僑生專班學生國別": row.Add(field, rec.OverseasChineseStudentCountryCode); break;

                                case "狀態":
                                    if (StudentStatusDict.ContainsKey(rec.StudentID))
                                        row.Add(field, StudentStatusDict[rec.StudentID]); break;
                            }
                        }
                    }
                    e.Items.Add(row);
                }
            };
        }
    }
}
