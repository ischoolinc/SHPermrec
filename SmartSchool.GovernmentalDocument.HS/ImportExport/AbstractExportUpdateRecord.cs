using System;
using System.Collections.Generic;
using SHSchool.Data;
using SmartSchool.Customization.PlugIn.ImportExport;

namespace SmartSchool.GovernmentalDocument.ImportExport
{
    abstract class AbstractExportUpdateRecord : ExportProcess
    {
        abstract protected string[] Fields { get;}

        abstract protected string Type { get;}

        public AbstractExportUpdateRecord()
        {
            this.Title = "匯出"+Type;
            this.Group = "學籍基本資料";
            foreach ( string var in Fields )
            {
                this.ExportableFields.Add(var);
            }
            this.ExportPackage += new EventHandler<ExportPackageEventArgs>(ExportUpdateRecord_ExportPackage);
        }

        private void ExportUpdateRecord_ExportPackage(object sender, ExportPackageEventArgs e)
        {
            for(int i=0;i<e.List.Count;i++)
                foreach(SHUpdateRecordRecord var in SHUpdateRecord.SelectByStudentID(e.List[i]))
                {       
                    if ( var.UpdateType != Type ) continue;

                    RowData row = new RowData();
                    row.ID = e.List[i];
                    foreach ( string field in e.ExportFields )
                    {
                        if ( ExportableFields.Contains(field) )
                        {
                            switch ( field )
                            {
                                case "班別": row.Add(field, var.ClassType); break;
                                case "特殊身份代碼": row.Add(field, var.SpecialStatus); break;
                                case "異動科別": row.Add(field, var.Department); break;
                                case "年級": row.Add(field, var.GradeYear); break;
                                case "異動學號": row.Add(field, var.StudentNumber); break;
                                case "異動姓名": row.Add(field, var.StudentName); break;
                                case "身分證號": row.Add(field, var.IDNumber); break;
                                case "註1": row.Add(field, var.IDNumberComment); break;
                                case "性別": row.Add(field, var.Gender); break;
                                case "生日": row.Add(field, var.Birthdate); break;
                                case "異動種類": row.Add(field, var.UpdateType); break;
                                case "異動代碼": row.Add(field, var.UpdateCode); break;
                                case "異動日期": row.Add(field, var.UpdateDate); break;
                                case "原因及事項": row.Add(field, var.UpdateDescription); break;
                                case "新學號": row.Add(field, var.NewStudentNumber); break;
                                case "更正後資料": row.Add(field, var.NewData); break;
                                case "轉入前學生資料-科別": row.Add(field, var.PreviousDepartment); break;
                                case "轉入前學生資料-年級": row.Add(field, var.PreviousGradeYear); break;
                                case "轉入前學生資料-學校": row.Add(field, var.PreviousSchool); break;
                                case "轉入前學生資料-(備查日期)": row.Add(field, var.PreviousSchoolLastADDate); break;
                                case "轉入前學生資料-(備查文號)": row.Add(field, var.PreviousSchoolLastADNumber); break;
                                case "轉入前學生資料-學號": row.Add(field, var.PreviousStudentNumber); break;
                                case "入學資格-畢業國中": row.Add(field, var.GraduateSchool); break;
                                case "入學資格-畢業國中所在地代碼": row.Add(field, var.GraduateSchoolLocationCode); break;
                                case "入學資格-畢業國中年度": row.Add(field, var.GraduateSchoolYear); break;
                                case "入學資格-註2": row.Add(field, var.GraduateComment); break;
                                case "最後異動代碼": row.Add(field, var.LastUpdateCode); break;
                                case "畢(結)業證書字號": row.Add(field, var.GraduateCertificateNumber); break;
                                case "舊班別": row.Add(field, var.OldClassType); break;
                                case "舊科別代碼": row.Add(field, var.OldDepartmentCode); break;
                                case "備查日期": row.Add(field, var.LastADDate); break;
                                case "備查文號": row.Add(field, var.LastADNumber); break;
                                case "核准日期": row.Add(field, var.ADDate); break;
                                case "核准文號": row.Add(field, var.ADNumber); break;
                                case "備註": row.Add(field, var.Comment); break;
                            }
                        }
                    }
                    e.Items.Add(row);
                }
        }
    }
}