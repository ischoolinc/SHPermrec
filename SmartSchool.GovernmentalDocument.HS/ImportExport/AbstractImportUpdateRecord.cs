using System;
using System.Collections.Generic;
using System.Reflection;
using FISCA.Presentation;
using SHSchool.Data;
using SmartSchool.Customization.PlugIn.ImportExport;

namespace SmartSchool.GovernmentalDocument.ImportExport
{
    abstract class AbstractImportUpdateRecord : ImportProcess
    {
        abstract protected string[] Fields { get;}

        abstract protected string Type { get;}

        abstract protected string[] InsertRequiredFields { get;}

        private Dictionary<string, SHUpdateRecordRecord> _NewStudentUpdateRecordInfoList = new Dictionary<string, SHUpdateRecordRecord>();

        //取得代碼對照表
        Dictionary<string, string> updateCodeMapping = null;

        private List<string> _DateFields = new List<string>(new string[] { "核准日期", "異動日期", "生日", "備查日期", "轉入前學生資料-(備查日期)" });

        private List<string> _NonNullFields = new List<string>(new string[] { "異動日期", "異動代碼" });

        public AbstractImportUpdateRecord()
        {
            this.Image = null;
            this.Title = "匯入" + Type;
            this.Group = "學籍基本資料";
            this.PackageLimit = 500;
            
            foreach ( string field in Fields )
            {
                this.ImportableFields.Add(field);
            }
            foreach ( string field in new string[] { "異動代碼" } )
            {
                this.RequiredFields.Add(field);
            }
            this.PackageLimit = 250;
            this.BeginValidate += new EventHandler<BeginValidateEventArgs>(ImportNewStudentsUpdateRecord_BeginValidate);
            this.RowDataValidated += new EventHandler<RowDataValidatedEventArgs>(ImportNewStudentsUpdateRecord_RowDataValidated);
            this.DataImport += new EventHandler<DataImportEventArgs>(ImportNewStudentsUpdateRecord_DataImport);
            this.EndImport += new EventHandler(ImportNewStudentsUpdateRecord_EndImport);
        }

        void ImportNewStudentsUpdateRecord_BeginValidate(object sender, BeginValidateEventArgs e)
        {
            #region 整理異動代號及類別對照

            updateCodeMapping = new Dictionary<string, string>();

            foreach (SHUpdateCodeMappingInfo var in SHUpdateCodeMapping.SelectAll())
                if ( !updateCodeMapping.ContainsKey(var.Code))
                    updateCodeMapping.Add(var.Code, var.Type);
            #endregion

            _NewStudentUpdateRecordInfoList = new Dictionary<string, SHUpdateRecordRecord>();

            List<SHStudentRecord> students = SHStudent.SelectByIDs(e.List);

            foreach (SHStudentRecord stu in students )
            {
                if ( !_NewStudentUpdateRecordInfoList.ContainsKey(stu.ID) )
                    _NewStudentUpdateRecordInfoList.Add(stu.ID, null);
                else
                    _NewStudentUpdateRecordInfoList[stu.ID] = null;
                foreach (SHUpdateRecordRecord uinfo in SHUpdateRecord.SelectByStudentID(stu.ID))
                {
                    if ( uinfo.UpdateType == Type )
                    {
                        _NewStudentUpdateRecordInfoList[stu.ID] = uinfo;
                        break;
                    }
                }
            }
        }

        void ImportNewStudentsUpdateRecord_RowDataValidated(object sender, RowDataValidatedEventArgs e)
        {
            if ( !_NewStudentUpdateRecordInfoList.ContainsKey(e.Data.ID) || _NewStudentUpdateRecordInfoList[e.Data.ID]==null )
            {
                List<string> missingFields = new List<string>();
                foreach ( string field in InsertRequiredFields )
                {
                    if ( !e.SelectFields.Contains(field) )
                    {
                        missingFields.Add(field);
                    }
                }
                if ( missingFields.Count > 0 )
                {
                    string msg = "";
                    foreach ( string field in missingFields )
                    {
                        msg += ( msg == "" ? "新增的異動必需包含欄位：" : "、" ) + field;
                    }
                    e.ErrorMessage = msg;
                    return;
                }
            }
            foreach ( string field in e.SelectFields )
            {
                #region 如果是異動代號則檢查輸入代號是否在清單中
                if ( field == "異動代碼" )
                {
                    if ( updateCodeMapping.ContainsKey(e.Data[field]) == false || updateCodeMapping[e.Data[field]] != Type )
                    {
                        e.ErrorFields.Add(field, "輸入的代號不在指定的"+Type+"代號清單中。");
                        continue;
                    }
                }
                #endregion
                #region 如果是日期欄位檢查輸入值
                if ( _DateFields.Contains(field) )
                {
                    if ( e.Data[field] == "" && _NonNullFields.Contains(e.Data[field]) )
                    {
                        e.ErrorFields.Add(field, "此欄為必填欄位，請輸入西元年/月/日。");
                        continue;
                    }
                    else
                    {
                        if ( e.Data[field] != "" )
                        {
                            //檢查欄位值
                            if ( !CheckIsDate(e.Data[field]) )
                            {
                                if ( _NonNullFields.Contains(field) )
                                {
                                    e.ErrorFields.Add(field, "此欄為必填欄位，\n請依照\"西元年/月/日\"格式輸入。");
                                }
                                else
                                {
                                    e.ErrorFields.Add(field, "輸入格式錯誤，請輸入西元年/月/日。\n此筆錯誤資料將不會被儲存");
                                }
                                continue;
                            }

                            // 強制轉換日期型態
                            DateTime dt;

                            if (DateTime.TryParse(e.Data[field], out dt))
                                e.Data[field] = dt.ToShortDateString();
                        }
                    }
                }
                #endregion
                #region 如果是必填欄位檢查非空值
                if ( _NonNullFields.Contains(field) && e.Data[field] == "" )
                {
                    e.ErrorFields.Add(field, "此欄位必須填寫，不允許空值");
                    continue;
                }
                #endregion
                #region 如果是年級則檢查輸入資料
                if ( field == "年級" )
                {
                    int i = 0;
                    if ( e.Data[field] != "延修生" && ( !int.TryParse(e.Data[field], out i) || i <= 0 ) )
                    {
                        e.ErrorFields.Add(field, "年級欄必需依以下格式填寫：\n\t1.若為一般學生請填入學生年級。\n\t2.若為延修生請填入\"延修生\"");
                        continue;
                    }
                }
                #endregion
            }
        }

        void ImportNewStudentsUpdateRecord_DataImport(object sender, DataImportEventArgs e)
        {
            Dictionary<string, string> fieldNameMapping = new Dictionary<string, string>();
            #region 建立匯入欄位名稱跟Xml內欄位對照表
            fieldNameMapping.Add("Department", "異動科別");
            fieldNameMapping.Add("GradeYear", "年級");
            fieldNameMapping.Add("StudentNumber", "異動學號");
            fieldNameMapping.Add("StudentName", "異動姓名");
            fieldNameMapping.Add("IDNumber", "身分證號");
            fieldNameMapping.Add("Gender", "性別");
            fieldNameMapping.Add("Birthdate", "生日");
            fieldNameMapping.Add("UpdateCode", "異動代碼");
            fieldNameMapping.Add("UpdateDate", "異動日期");
            fieldNameMapping.Add("UpdateDescription", "原因及事項");
            fieldNameMapping.Add("NewStudentNumber", "新學號");
            fieldNameMapping.Add("PreviousDepartment", "轉入前學生資料-科別");
            fieldNameMapping.Add("PreviousGradeYear", "轉入前學生資料-年級");
            fieldNameMapping.Add("PreviousSchool", "轉入前學生資料-學校");
            fieldNameMapping.Add("PreviousSchoolLastADDate", "轉入前學生資料-(備查日期)");
            fieldNameMapping.Add("PreviousSchoolLastADNumber", "轉入前學生資料-(備查文號)");
            fieldNameMapping.Add("PreviousStudentNumber", "轉入前學生資料-學號");
            fieldNameMapping.Add("GraduateSchool", "入學資格-畢業國中");
            fieldNameMapping.Add("GraduateSchoolLocationCode", "入學資格-畢業國中所在地代碼");
            fieldNameMapping.Add("LastUpdateCode", "最後異動代碼");
            fieldNameMapping.Add("GraduateCertificateNumber", "畢(結)業證書字號");
            fieldNameMapping.Add("LastADDate", "備查日期");
            fieldNameMapping.Add("LastADNumber", "備查文號");
            fieldNameMapping.Add("ADDate", "核准日期");
            fieldNameMapping.Add("ADNumber", "核准文號");
            fieldNameMapping.Add("Comment", "備註");

            fieldNameMapping.Add("ClassType","班別");
            fieldNameMapping.Add("SpecialStatus","特殊身份代碼");
            fieldNameMapping.Add("IDNumberComment", "註1");
            fieldNameMapping.Add("OldClassType", "舊班別");
            fieldNameMapping.Add("OldDepartmentCode", "舊科別代碼");
            fieldNameMapping.Add("GraduateSchoolYear", "入學資格-畢業國中年度");
            fieldNameMapping.Add("GraduateComment", "入學資格-註2");
            #endregion
            List<SHUpdateRecordRecord> InsertUpdateRecords = new List<SHUpdateRecordRecord>();
            List<SHUpdateRecordRecord> UpdateUpdateRecords = new List<SHUpdateRecordRecord>();
            

            bool insert = false, update = false;
            foreach ( RowData row in e.Items )
            {                
                if ( _NewStudentUpdateRecordInfoList.ContainsKey(row.ID) )
                {
                    if ( _NewStudentUpdateRecordInfoList[row.ID] == null )
                    {
                        insert = true;
                        #region 新增
                        SHUpdateRecordRecord InsertUpdateRecord = new SHUpdateRecordRecord();

                        InsertUpdateRecord.StudentID = row.ID;

                        foreach ( string field in fieldNameMapping.Keys )
                        {
                            string fieldname = fieldNameMapping[field];

                            PropertyInfo Property = InsertUpdateRecord.GetType().GetProperty(field, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

                            if (Property != null)
                                Property.SetValue(InsertUpdateRecord, row.ContainsKey(fieldname) ? row[fieldname] : string.Empty, null);
                        }

                        InsertUpdateRecords.Add(InsertUpdateRecord);
                        #endregion
                    }
                    else
                    {
                        update = true;
                        #region 修改
                        SHUpdateRecordRecord UpdateUpdateRecord = new SHUpdateRecordRecord();
                        

                        UpdateUpdateRecord.ID = _NewStudentUpdateRecordInfoList[row.ID].ID;
                        UpdateUpdateRecord.StudentID = row.ID;

                        foreach ( string field in fieldNameMapping.Keys )
                        {
                            string fieldname = fieldNameMapping[field];

                            PropertyInfo Property = UpdateUpdateRecord.GetType().GetProperty(field, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

                            if (Property != null)
                                Property.SetValue(UpdateUpdateRecord, row.ContainsKey(fieldname) ? row[fieldname] : string.Empty, null);
                        }

                        UpdateUpdateRecords.Add(UpdateUpdateRecord);
                        #endregion
                    }
                }
            }
            if (insert)
            {
                try
                {
                    SHUpdateRecord.Insert(InsertUpdateRecords); 
                }
                catch(Exception ve)
                {
                }
            }
            if (update)
            {
                try
                {
                    SHUpdateRecord.Update(UpdateUpdateRecords);
                }
                catch (Exception ve)
                {
                }
            }
        }

        void ImportNewStudentsUpdateRecord_EndImport(object sender, EventArgs e)
        {
            MotherForm.SetStatusBarMessage(Type + "匯入完成。");
        }


        private bool CheckIsDate(string text)
        {
            if ( text.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries).Length != 3 )
                return false;
            DateTime d = DateTime.Now;
            return DateTime.TryParse(text, out  d);
        }
    }
}