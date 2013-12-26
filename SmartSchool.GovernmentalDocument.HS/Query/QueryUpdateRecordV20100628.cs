using System;
using System.Collections.Generic;
using System.Text;
using iAccess;
using SHSchool.Data;


namespace SmartSchool.GovernmentalDocument.Query
{

    public class QueryUpdateRecordV20100628
    {
        /// <summary>
        /// 學生異動名冊特殊欄位
        /// </summary>
        public class StudUpdateRecordEntity
        { 
        
            /// <summary>
            /// 班別
            /// </summary>
            [Field(Caption = "班別",Remark="取得學生異動紀錄上的班別。")]
            public string ClassType { get; set; }

            /// <summary>
            /// 科別代碼
            /// </summary>
            [Field(Caption = "科別代碼",Remark="取得學生身上的科別代碼，若沒有的話則取得學生異動紀錄上的科別名稱。")]
            public string DeptCode {get;set;}

            /// <summary>
            /// 上傳類別
            /// </summary>
            [Field(Caption = "上傳類別")]
            public string UploadType{get;set;}

            /// <summary>
            /// 年級
            /// </summary>
            [Field(Caption = "年級")]
            public string GradeYear{get;set;}

            /// <summary>
            /// 學號
            /// </summary>
            [Field(Caption = "學號")]
            public string StudentNumber{get;set;}

            /// <summary>
            /// 姓名
            /// </summary>
            [Field(Caption = "姓名")]
            public string StudentName{get;set;}

            /// <summary>
            /// 身分證字號
            /// </summary>
            [Field(Caption = "身分證字號")]
            public string IDNumber{get;set;}
                        
            /// <summary>
            /// 註1
            /// </summary>
            [Field(Caption = "註1")]
            public string IDNumber1{get;set;}
            
            /// <summary>
            /// 性別代碼
            /// </summary>
            [Field(Caption = "性別代碼")]
            public string GenderCode{get;set;}

            /// <summary>
            /// 出生日期
            /// </summary>
            [Field(Caption = "出生日期")]
            public string Birthday{get;set;}

            /// <summary>
            /// 特殊身份代碼
            /// </summary>
            [Field(Caption = "特殊身份代碼")]
            public string StudTypeCode{get;set;}

            /// <summary>
            /// 國中畢業學年度
            /// </summary>
            [Field(Caption = "國中畢業學年度")]
            public string JHGradeSchoolYear{get;set;}

            /// <summary>
            /// 新生入學學年度
            /// </summary>
            [Field(Caption = "新生入學學年度")]
            public string EnrollSchoolYear{get;set;}

            /// <summary>
            /// 入學資格代碼
            /// </summary>
            [Field(Caption = "入學資格代碼")]
            public string EnrollCode{get;set;}

            /// <summary>
            /// 入學資格學校名稱
            /// </summary>
            [Field(Caption = "入學資格學校名稱")]
            public string EnrollSchoolName{get;set;}

            /// <summary>
            /// 入學資格證明文件
            /// </summary>
            [Field(Caption = "入學資格證明文件")]
            public string EnrollDoc{get;set;}

            /// <summary>
            /// 最新狀態代碼
            /// </summary>
            [Field(Caption = "最新狀態代碼")]
            public string StatusCode{get;set;}

            /// <summary>
            /// 最新狀態文字
            /// </summary>
            [Field(Caption = "最新狀態文字")]
            public string StatusText{get;set;}

            /// <summary>
            /// 最新狀態文號
            /// </summary>
            [Field(Caption = "最新狀態文號")]
            public string StatusNo{get;set;}

            /// <summary>
            /// 最新狀態核准日期
            /// </summary>
            [Field(Caption = "最新狀態核准日期")]
            public string StatusDate{get;set;}

            /// <summary>
            /// 最新更正代碼
            /// </summary>
            [Field(Caption = "最新更正代碼")]
            public string CorrectCode{get;set;}

            /// <summary>
            /// 最新更正文字
            /// </summary>
            [Field(Caption = "最新更正文字")]
            public string CorrectText{get;set;}

            /// <summary>
            /// 最新更正文號
            /// </summary>
            [Field(Caption = "最新更正文號")]
            public string CorrectNo{get;set;}

            /// <summary>
            /// 最新更正核准日期
            /// </summary>
            [Field(Caption = "最新更正核准日期")]
            public string CorrectDate{get;set;}

            /// <summary>
            /// 備註
            /// </summary>
            [Field(Caption = "備註")]
            public string Memo { get; set; }


            /// <summary>
            /// 異動日期(非呈報名冊內欄位)，主要用於方面資料處理
            /// </summary>
            [Field(Caption = "異動日期(非呈報名冊內欄位)")]
            public string UpdateDate{get;set;}


            /// <summary>
            /// 學生狀態(非呈報名冊內欄位)，主要用於方面資料處理
            /// </summary>
            [Field(Caption = "學生狀態(非呈報名冊內欄位)")]
            public string StudStatus { get; set; }


            [Field(Caption = "學生班級(非呈報名冊內欄位)")]
            public string ClassName { get; set; }

            [Field(Caption = "學生最後一筆轉出異動(非呈報名冊內欄位)")]
            public string HasExportRecCodeString { get; set; }

        }

        [SelectMethod("StudUpdateRecordV20100628", "取得學籍異動名冊20100628版0705")]
        public static List<StudUpdateRecordEntity> Select()
        {
            List<StudUpdateRecordEntity> Result = new List<StudUpdateRecordEntity>();            

            // 收集有轉出異動文字
            Dictionary<string, List<SHUpdateRecordRecord>> GetExportRec = new Dictionary<string, List<SHUpdateRecordRecord>>();

            try
            {

                // 科別代碼對照
                Dictionary<string, string> DeptCodeDict = new Dictionary<string, string>();
                foreach (SHDepartmentRecord dept in SHDepartment.SelectAll())
                    if (!DeptCodeDict.ContainsKey(dept.Name))
                        DeptCodeDict.Add(dept.Name, dept.Code);

                Dictionary<string, string> NewCodeDict = new Dictionary<string, string>();
                NewCodeDict.Add("006", "回國僑生介考(專案核准)");
                NewCodeDict.Add("007", "省外來台(專案核准):大陸學歷認證部分");
                NewCodeDict.Add("008", "其他(需說明證明文件):持國外學歷證明者");


                #region 取得選取學生，並且過濾有畢業異動的學生

                //貴校現行具學籍之學生（含在學生、休學生、延修生等）；凡是已轉出、已畢業(即98學年度以前畢業生)，不需在此次學生名冊內申報。
                // 畢業生先濾，取得所選學生的已有畢業異動學生 ID

                List<string> GradeStudIDList = new List<string>();
                foreach (SHUpdateRecordRecord rec in SHUpdateRecord.SelectByStudentIDs(K12.Presentation.NLDPanels.Student.SelectedSource))
                    if (rec.UpdateType == "畢業異動" || rec.UpdateCode == "501")
                        GradeStudIDList.Add(rec.StudentID);            


                // 取得畫面上所選學生ID 學生資料
                Dictionary<string, SHStudentRecord> SelectStudDict = new Dictionary<string, SHStudentRecord>();
                foreach (SHStudentRecord shStudRec in SHStudent.SelectByIDs(K12.Presentation.NLDPanels.Student.SelectedSource))
                {
                    // 過濾以畢業學生有畢業異動
                    if (!GradeStudIDList.Contains(shStudRec.ID))
                        SelectStudDict.Add(shStudRec.ID, shStudRec);

                    //// 過濾畢業或離校生
                    //if (shStudRec.Status != K12.Data.StudentRecord.StudentStatus.畢業或離校)
                    //    SelectStudDict.Add(shStudRec.ID, shStudRec);
                }

                #endregion

                #region 取得學生異動資料
                Dictionary<string, List<SHUpdateRecordRecord>> StudUpdateRecDict = new Dictionary<string, List<SHUpdateRecordRecord>>();
                foreach (SHUpdateRecordRecord urr in SHUpdateRecord.SelectByStudentIDs(SelectStudDict.Keys))
                {
                    if (StudUpdateRecDict.ContainsKey(urr.StudentID))
                        StudUpdateRecDict[urr.StudentID].Add(urr);
                    else
                    {
                        List<SHUpdateRecordRecord> urrList = new List<SHUpdateRecordRecord>();
                        urrList.Add(urr);
                        StudUpdateRecDict.Add(urr.StudentID, urrList);
                    }
                }
                #endregion

                // 更正異動代碼 401~409
                List<string> UrCorrectCode = new List<string>();
                for (short s = 401; s <= 409; s++)
                    UrCorrectCode.Add(s.ToString());

                // 轉出異動代碼
                List<string> ExportRecCode = new List<string>();
                for (short s = 311; s <= 315; s++)
                    ExportRecCode.Add(s.ToString());

                //// 轉科
                //ExportRecCode.Add("301");

                //針對每位學生
                foreach (KeyValuePair<string, SHStudentRecord> Stud in SelectStudDict)
                {
                    if (StudUpdateRecDict.ContainsKey(Stud.Key))
                    {
                        // 當沒有任何異動紀錄跳過
                        if (StudUpdateRecDict.Values.Count < 1)
                            continue;

                        // 新生異動
                        SHUpdateRecordRecord NewStudUrr = null;

                        // 轉入異動
                        List<SHUpdateRecordRecord> ImportUrrList = new List<SHUpdateRecordRecord>();

                        // 學籍更正
                        List<SHUpdateRecordRecord> CorrectUrList = new List<SHUpdateRecordRecord>();

                        // 學籍非更正
                        List<SHUpdateRecordRecord> NonCorrectUrrList = new List<SHUpdateRecordRecord>();


                        // 針對每位學生的異動記錄分類
                        foreach (SHUpdateRecordRecord StudUrr in StudUpdateRecDict[Stud.Key])
                        {
                            if (StudUrr.UpdateType == "新生異動")
                                NewStudUrr = StudUrr;

                            if (StudUrr.UpdateType == "轉入異動")
                            {
                                //// 已經報過有核准日期略過
                                //if (!string.IsNullOrEmpty(StudUrr.ADDate.Trim()))
                                //    continue;

                                ImportUrrList.Add(StudUrr);
                            }

                            if (StudUrr.UpdateType == "學籍異動")
                            {
                                //// 已經報過有核准日期略過
                                //if (!string.IsNullOrEmpty(StudUrr.ADDate.Trim()))
                                //    continue;

                                // 如果是轉出異動代碼收集文字
                                if (ExportRecCode.Contains(StudUrr.UpdateCode))
                                {
                                   // string str = "異動日期：" + StudUrr.UpdateDate + ",異動代碼：" + StudUrr.UpdateCode + ",異動核准日期：" + StudUrr.ADDate + ",異動核准文號：" + StudUrr.ADNumber+",";
                                    if (GetExportRec.ContainsKey(StudUrr.StudentID))
                                    {
                                        GetExportRec[StudUrr.StudentID].Add(StudUrr);
                                    }
                                    else
                                    {
                                        List<SHUpdateRecordRecord> ur = new List<SHUpdateRecordRecord>();
                                        ur.Add(StudUrr);
                                        GetExportRec.Add(StudUrr.StudentID, ur);
                                    }
                                }
                                else
                                {
                                    // 有轉科異動301跳過
                                    if (StudUrr.UpdateCode == "301")
                                        continue;

                                    // 有更正異動
                                    //401~409
                                    if (UrCorrectCode.Contains(StudUrr.UpdateCode))
                                        CorrectUrList.Add(StudUrr);
                                    else
                                        NonCorrectUrrList.Add(StudUrr);
                                }
                            }
                        }

                        // 有新生異動
                        if (NewStudUrr != null && NonCorrectUrrList.Count == 0)
                        {
                            StudUpdateRecordEntity sure = new StudUpdateRecordEntity();
                            //班別
                            sure.ClassType = NewStudUrr.ClassType;
                            //科別代碼
                            if (DeptCodeDict.ContainsKey(NewStudUrr.Department))
                                sure.DeptCode = DeptCodeDict[NewStudUrr.Department];
                            else
                                sure.DeptCode = NewStudUrr.Department;


                            //上傳類別
                            sure.UploadType = "";
                            if (Stud.Value.Class != null)
                            {
                                //年級
                                if (Stud.Value.Class.GradeYear.HasValue)
                                    sure.GradeYear = Stud.Value.Class.GradeYear.Value.ToString();
                                else
                                    sure.GradeYear = "";
                                sure.ClassName = Stud.Value.Class.Name;
                            }
                            //學號
                            sure.StudentNumber = Stud.Value.StudentNumber;
                            //姓名
                            sure.StudentName = Stud.Value.Name;
                            //身分證字號
                            sure.IDNumber = Stud.Value.IDNumber;
                            //註1
                            sure.IDNumber1 = NewStudUrr.IDNumberComment;
                            //性別代碼
                            if (string.IsNullOrEmpty(Stud.Value.Gender))
                                sure.GenderCode = "";
                            else
                            {
                                if (NewStudUrr.Gender == "男")
                                    sure.GenderCode = "1";
                                if (NewStudUrr.Gender == "女")
                                    sure.GenderCode = "2";
                            }
                            //出生日期
                            if (Stud.Value.Birthday.HasValue)
                                sure.Birthday = GetConvertStrDate(Stud.Value.Birthday.Value.ToShortDateString());
                            else
                                sure.Birthday = "";

                            //特殊身份代碼
                            sure.StudTypeCode = NewStudUrr.SpecialStatus;

                            // 屬於新生異動 ---
                            if (NewStudUrr != null)
                            {
                                //國中畢業學年度
                                sure.JHGradeSchoolYear = NewStudUrr.GraduateSchoolYear;
                                //新生入學學年度
                                if (NewStudUrr.SchoolYear.HasValue)
                                    sure.EnrollSchoolYear = NewStudUrr.SchoolYear.Value.ToString();
                                else
                                    sure.EnrollSchoolYear = "";
                                //入學資格代碼
                                sure.EnrollCode = NewStudUrr.UpdateCode;
                                //入學資格學校名稱
                                sure.EnrollSchoolName = NewStudUrr.GraduateSchool;
                                //入學資格證明文件
                                if (NewCodeDict.ContainsKey(NewStudUrr.UpdateCode))
                                    sure.EnrollDoc = NewCodeDict[NewStudUrr.UpdateCode];
                                else
                                    sure.EnrollDoc = "";
                            }
                            else
                            {
                                //國中畢業學年度
                                sure.JHGradeSchoolYear = "";
                                //新生入學學年度
                                sure.EnrollSchoolYear = "";
                                //入學資格代碼
                                sure.EnrollCode = "";
                                //入學資格學校名稱
                                sure.EnrollSchoolName = "";
                                //入學資格證明文件
                                sure.EnrollDoc = "";

                            }
                            //最新狀態代碼
                            sure.StatusCode = NewStudUrr.UpdateCode;
                            //最新狀態文字
                            sure.StatusText = GetUpdateRecAddDoc(NewStudUrr.ADNumber);
                            //最新狀態文號
                            sure.StatusNo = GetUpdateRecAddNo(NewStudUrr.ADNumber);
                            //最新狀態核准日期
                            sure.StatusDate = GetConvertStrDate(NewStudUrr.ADDate);

                            //最新更正代碼
                            sure.CorrectCode = "";
                            //最新更正文字
                            sure.CorrectText = "";
                            //最新更正文號
                            sure.CorrectNo = "";
                            //最新更正核准日期
                            sure.CorrectDate = "";


                            if (CorrectUrList.Count > 0)
                            {
                                CorrectUrList.Sort(new Comparison<SHUpdateRecordRecord>(UpdateRecSort1));
                                DateTime dtx, dty;
                                DateTime.TryParse(CorrectUrList[0].UpdateDate, out dtx);
                                DateTime.TryParse(NewStudUrr.UpdateDate, out dty);

                                if (dtx >= dty)
                                {
                                    //最新更正代碼
                                    sure.CorrectCode = CorrectUrList[0].UpdateCode;
                                    //最新更正文字
                                    sure.CorrectText = GetUpdateRecAddDoc(CorrectUrList[0].ADNumber);
                                    //最新更正文號
                                    sure.CorrectNo = GetUpdateRecAddNo(CorrectUrList[0].ADNumber);
                                    //最新更正核准日期
                                    sure.CorrectDate = GetConvertStrDate(CorrectUrList[0].ADDate);
                                }
                            }

                            //備註
                            sure.Memo = "";
                            //異動日期
                            sure.UpdateDate = NewStudUrr.UpdateDate;
                            //學生狀態
                            sure.StudStatus = SelectStudDict[NewStudUrr.StudentID].Status.ToString();

                            sure.HasExportRecCodeString = "";
                            if (GetExportRec.ContainsKey(NewStudUrr.StudentID))
                            {                                
                                if (GetExportRec[NewStudUrr.StudentID].Count > 0)
                                {
                                    
                                    GetExportRec[NewStudUrr.StudentID].Sort(new Comparison<SHUpdateRecordRecord>(UpdateRecSort1));
                                    sure.HasExportRecCodeString = "異動日期：" + GetExportRec[NewStudUrr.StudentID][0].UpdateDate + ",異動代碼：" + GetExportRec[NewStudUrr.StudentID][0].UpdateCode + ",異動核准日期：" + GetExportRec[NewStudUrr.StudentID][0].ADDate + ",異動核准文號：" + GetExportRec[NewStudUrr.StudentID][0].ADNumber;
                                }                                
                            }

                            // 有轉入異動
                            if (ImportUrrList.Count >= 0)
                            {
                                ImportUrrList.Sort(new Comparison<SHUpdateRecordRecord>(UpdateRecSort1));
                                DateTime dtx, dty;
                                if (ImportUrrList.Count > 0)
                                {
                                    DateTime.TryParse(ImportUrrList[0].UpdateDate, out dtx);
                                    DateTime.TryParse(NewStudUrr.UpdateDate, out dty);

                                    if (dtx <= dty)
                                        Result.Add(sure);
                                }

                                if (ImportUrrList.Count == 0)
                                    Result.Add(sure);
                            }
                            else
                                Result.Add(sure);
                        }

                        // 學籍異動非更正
                        //foreach (SHUpdateRecordRecord urr in NonCorrectUrrList)
                        //{
                        if (NonCorrectUrrList.Count > 0)
                        {
                            NonCorrectUrrList.Sort(new Comparison<SHUpdateRecordRecord>(UpdateRecSort1));
                            StudUpdateRecordEntity sure = new StudUpdateRecordEntity();
                            SHUpdateRecordRecord urr = NonCorrectUrrList[0];
                            //班別
                            sure.ClassType = urr.ClassType;
                            //科別代碼
                            if (DeptCodeDict.ContainsKey(urr.Department))
                                sure.DeptCode = DeptCodeDict[urr.Department];
                            else
                                sure.DeptCode = urr.Department;


                            //上傳類別
                            sure.UploadType = "";

                            if (Stud.Value.Class != null)
                            {
                                if (Stud.Value.Class.GradeYear.HasValue)
                                    //年級
                                    sure.GradeYear = Stud.Value.Class.GradeYear.Value.ToString();
                                else
                                    sure.GradeYear = "";

                                sure.ClassName = Stud.Value.Class.Name;
                            }
                            //學號
                            sure.StudentNumber = Stud.Value.StudentNumber;
                            //姓名
                            sure.StudentName = Stud.Value.Name;
                            //身分證字號
                            sure.IDNumber = Stud.Value.IDNumber;
                            //註1
                            sure.IDNumber1 = urr.IDNumberComment;
                            //性別代碼
                            if (string.IsNullOrEmpty(Stud.Value.Gender))
                                sure.GenderCode = "";
                            else
                            {
                                if (urr.Gender == "男")
                                    sure.GenderCode = "1";
                                if (urr.Gender == "女")
                                    sure.GenderCode = "2";
                            }
                            //出生日期
                            if (Stud.Value.Birthday.HasValue)
                                sure.Birthday = GetConvertStrDate(Stud.Value.Birthday.Value.ToShortDateString());
                            else
                                sure.Birthday = "";

                            //特殊身份代碼
                            sure.StudTypeCode = urr.SpecialStatus;

                            // 屬於新生異動 ---
                            if (NewStudUrr != null)
                            {
                                //國中畢業學年度
                                sure.JHGradeSchoolYear = NewStudUrr.GraduateSchoolYear;
                                //新生入學學年度
                                if (NewStudUrr.SchoolYear.HasValue)
                                    sure.EnrollSchoolYear = NewStudUrr.SchoolYear.Value.ToString();
                                else
                                    sure.EnrollSchoolYear = "";
                                //入學資格代碼
                                sure.EnrollCode = NewStudUrr.UpdateCode;
                                //入學資格學校名稱
                                sure.EnrollSchoolName = NewStudUrr.GraduateSchool;
                                //入學資格證明文件
                                if (NewCodeDict.ContainsKey(NewStudUrr.UpdateCode))
                                    sure.EnrollDoc = NewCodeDict[NewStudUrr.UpdateCode];
                                else
                                    sure.EnrollDoc = "";
                            }
                            else
                            {
                                //國中畢業學年度
                                sure.JHGradeSchoolYear = "";
                                //新生入學學年度
                                sure.EnrollSchoolYear = "";
                                //入學資格代碼
                                sure.EnrollCode = "";
                                //入學資格學校名稱
                                sure.EnrollSchoolName = "";
                                //入學資格證明文件
                                sure.EnrollDoc = "";

                            }
                            //最新狀態代碼
                            sure.StatusCode = urr.UpdateCode;
                            //最新狀態文字
                            sure.StatusText = GetUpdateRecAddDoc(urr.ADNumber);
                            //最新狀態文號
                            sure.StatusNo = GetUpdateRecAddNo(urr.ADNumber);
                            //最新狀態核准日期
                            sure.StatusDate = GetConvertStrDate(urr.ADDate);

                            //最新更正代碼
                            sure.CorrectCode = "";
                            //最新更正文字
                            sure.CorrectText = "";
                            //最新更正文號
                            sure.CorrectNo = "";
                            //最新更正核准日期
                            sure.CorrectDate = "";

                            if (CorrectUrList.Count > 0)
                            {
                                CorrectUrList.Sort(new Comparison<SHUpdateRecordRecord>(UpdateRecSort1));
                                DateTime dtx, dty;
                                DateTime.TryParse(CorrectUrList[0].UpdateDate, out dtx);
                                DateTime.TryParse(urr.UpdateDate, out dty);

                                if (dtx >= dty)
                                {
                                    //最新更正代碼
                                    sure.CorrectCode = CorrectUrList[0].UpdateCode;
                                    //最新更正文字
                                    sure.CorrectText = GetUpdateRecAddDoc(CorrectUrList[0].ADNumber);
                                    //最新更正文號
                                    sure.CorrectNo = GetUpdateRecAddNo(CorrectUrList[0].ADNumber);
                                    //最新更正核准日期
                                    sure.CorrectDate = GetConvertStrDate(CorrectUrList[0].ADDate);
                                }

                            }

                            //備註
                            sure.Memo = "";
                            //異動日期
                            sure.UpdateDate = urr.UpdateDate;
                            //學生狀態
                            sure.StudStatus = SelectStudDict[urr.StudentID].Status.ToString();


                            sure.HasExportRecCodeString = "";
                            if (GetExportRec.ContainsKey(urr.StudentID))
                            {                                
                                if (GetExportRec[urr.StudentID].Count > 0)
                                {
                                    GetExportRec[urr.StudentID].Sort(new Comparison<SHUpdateRecordRecord>(UpdateRecSort1));
                                    sure.HasExportRecCodeString = "異動日期：" + GetExportRec[urr.StudentID][0].UpdateDate + ",異動代碼：" + GetExportRec[urr.StudentID][0].UpdateCode + ",異動核准日期：" + GetExportRec[urr.StudentID][0].ADDate + ",異動核准文號：" + GetExportRec[urr.StudentID][0].ADNumber;
                                }
                            }

                            // 檢查轉入異動

                            if (ImportUrrList.Count >= 0)
                            {
                                ImportUrrList.Sort(new Comparison<SHUpdateRecordRecord>(UpdateRecSort1));
                                DateTime dtx, dty;                                


                                if (ImportUrrList.Count > 0)
                                {
                                    DateTime.TryParse(ImportUrrList[0].UpdateDate, out dtx);
                                    if (NewStudUrr != null)
                                    {
                                        DateTime.TryParse(NewStudUrr.UpdateDate, out dty);

                                        if (dtx <= dty)
                                            Result.Add(sure);
                                    }
                                    DateTime dtxa;                
                                    DateTime.TryParse(urr.UpdateDate , out dtxa);

                                    if (dtxa >= dtx)
                                        Result.Add(sure);


                                }

                                if (ImportUrrList.Count == 0)
                                    Result.Add(sure);
                            }
                            else
                                Result.Add(sure);

                        }



                        // 處理轉入異動
                        foreach (SHUpdateRecordRecord urr in ImportUrrList)
                        {
                            StudUpdateRecordEntity sure = new StudUpdateRecordEntity();
                            //班別
                            sure.ClassType = urr.ClassType;
                            //科別代碼
                            if (DeptCodeDict.ContainsKey(urr.Department))
                                sure.DeptCode = DeptCodeDict[urr.Department];
                            else
                                sure.DeptCode = urr.Department;


                            //上傳類別
                            sure.UploadType = "";

                            if (Stud.Value.Class != null)
                            {
                                //年級
                                if (Stud.Value.Class.GradeYear.HasValue)
                                    sure.GradeYear = Stud.Value.Class.GradeYear.Value.ToString();
                                else
                                    sure.GradeYear = "";

                                sure.ClassName = Stud.Value.Class.Name;
                            }
                            //學號
                            sure.StudentNumber = Stud.Value.StudentNumber;
                            //姓名
                            sure.StudentName = Stud.Value.Name;
                            //身分證字號
                            sure.IDNumber = Stud.Value.IDNumber;
                            //註1
                            sure.IDNumber1 = urr.IDNumberComment;
                            //性別代碼
                            if (string.IsNullOrEmpty(Stud.Value.Gender))
                                sure.GenderCode = "";
                            else
                            {
                                if (urr.Gender == "男")
                                    sure.GenderCode = "1";
                                if (urr.Gender == "女")
                                    sure.GenderCode = "2";
                            }
                            //出生日期
                            if (Stud.Value.Birthday.HasValue)
                                sure.Birthday = GetConvertStrDate(Stud.Value.Birthday.Value.ToShortDateString());
                            else
                                sure.Birthday = "";

                            //特殊身份代碼
                            sure.StudTypeCode = urr.SpecialStatus;

                            // 屬於新生異動 ---
                            if (NewStudUrr != null)
                            {
                                //國中畢業學年度
                                sure.JHGradeSchoolYear = NewStudUrr.GraduateSchoolYear;
                                //新生入學學年度
                                if (NewStudUrr.SchoolYear.HasValue)
                                    sure.EnrollSchoolYear = NewStudUrr.SchoolYear.Value.ToString();
                                else
                                    sure.EnrollSchoolYear = "";
                                //入學資格代碼
                                sure.EnrollCode = NewStudUrr.UpdateCode;
                                //入學資格學校名稱
                                sure.EnrollSchoolName = NewStudUrr.GraduateSchool;
                                //入學資格證明文件
                                if (NewCodeDict.ContainsKey(NewStudUrr.UpdateCode))
                                    sure.EnrollDoc = NewCodeDict[NewStudUrr.UpdateCode];
                                else
                                    sure.EnrollDoc = "";
                            }
                            else
                            {
                                //國中畢業學年度
                                sure.JHGradeSchoolYear = "";
                                //新生入學學年度
                                sure.EnrollSchoolYear = "";
                                //入學資格代碼
                                sure.EnrollCode = "";
                                //入學資格學校名稱
                                sure.EnrollSchoolName = "";
                                //入學資格證明文件
                                sure.EnrollDoc = "";

                            }

                            //最新狀態代碼
                            sure.StatusCode = urr.UpdateCode;
                            //最新狀態文字
                            sure.StatusText = GetUpdateRecAddDoc(urr.ADNumber);
                            //最新狀態文號
                            sure.StatusNo = GetUpdateRecAddNo(urr.ADNumber);
                            //最新狀態核准日期
                            sure.StatusDate = GetConvertStrDate(urr.ADDate);

                            //最新更正代碼
                            sure.CorrectCode = "";
                            //最新更正文字
                            sure.CorrectText = "";
                            //最新更正文號
                            sure.CorrectNo = "";
                            //最新更正核准日期
                            sure.CorrectDate = "";

                            if (CorrectUrList.Count > 0)
                            {
                                CorrectUrList.Sort(new Comparison<SHUpdateRecordRecord>(UpdateRecSort1));
                                DateTime dtx, dty;
                                DateTime.TryParse(CorrectUrList[0].UpdateDate, out dtx);
                                DateTime.TryParse(urr.UpdateDate, out dty);

                                if (dtx >= dty)
                                {
                                    //最新更正代碼
                                    sure.CorrectCode = CorrectUrList[0].UpdateCode;
                                    //最新更正文字
                                    sure.CorrectText = GetUpdateRecAddDoc(CorrectUrList[0].ADNumber);
                                    //最新更正文號
                                    sure.CorrectNo = GetUpdateRecAddNo(CorrectUrList[0].ADNumber);
                                    //最新更正核准日期
                                    sure.CorrectDate = GetConvertStrDate(CorrectUrList[0].ADDate);
                                }

                            }

                            //備註
                            sure.Memo = "轉入生";
                            //異動日期
                            sure.UpdateDate = urr.UpdateDate;
                            //學生狀態
                            sure.StudStatus = SelectStudDict[urr.StudentID].Status.ToString();

                            sure.HasExportRecCodeString = "";
                            if (GetExportRec.ContainsKey(urr.StudentID))
                            {                                
                                if (GetExportRec[urr.StudentID].Count > 0)
                                {

                                    GetExportRec[urr.StudentID].Sort(new Comparison<SHUpdateRecordRecord>(UpdateRecSort1));
                                    sure.HasExportRecCodeString = "異動日期：" + GetExportRec[urr.StudentID][0].UpdateDate + ",異動代碼：" + GetExportRec[urr.StudentID][0].UpdateCode + ",異動核准日期：" + GetExportRec[urr.StudentID][0].ADDate + ",異動核准文號：" + GetExportRec[urr.StudentID][0].ADNumber;
                                }
                            }

                            // 有新生異動
                            if (NewStudUrr != null)
                            {
                                DateTime dtx, dty;
                                DateTime.TryParse(urr.UpdateDate, out dtx);
                                DateTime.TryParse(NewStudUrr.UpdateDate, out dty);

                                if (dtx >= dty)
                                {
                                    // 檢查是否有學籍異動
                                    if (NonCorrectUrrList.Count >= 0)
                                    {
                                        if (NonCorrectUrrList.Count > 0)
                                        {
                                            NonCorrectUrrList.Sort(new Comparison<SHUpdateRecordRecord>(UpdateRecSort1));
                                            DateTime dtxa, dtya;
                                            DateTime.TryParse(urr.UpdateDate, out dtxa);
                                            DateTime.TryParse(NonCorrectUrrList[NonCorrectUrrList.Count-1].UpdateDate, out dtya);

                                            if (dtxa >= dtya)
                                                Result.Add(sure);
                                        }
                                        if (NonCorrectUrrList.Count == 0)
                                            Result.Add(sure);
                                    }
                                    else
                                        Result.Add(sure);
                                }
                            }
                            else
                            {
                                // 檢查是否有學籍異動
                                if (NonCorrectUrrList.Count >= 0)
                                {
                                    if (NonCorrectUrrList.Count > 0)
                                    {
                                        NonCorrectUrrList.Sort(new Comparison<SHUpdateRecordRecord>(UpdateRecSort1));
                                        DateTime dtx, dty;
                                        DateTime.TryParse(urr.UpdateDate, out dtx);
                                        DateTime.TryParse(NonCorrectUrrList[NonCorrectUrrList.Count-1].UpdateDate, out dty);

                                        if (dtx >= dty)
                                            Result.Add(sure);
                                    }
                                    if(NonCorrectUrrList.Count ==0)
                                        Result.Add(sure);
                                }
                                else
                                    Result.Add(sure);
                            }
                        }


                        //// 學籍異動只有更正異動
                        //if(NonCorrectUrrList.Count==0 && ImportUrrList.Count ==0)
                        //foreach (SHUpdateRecordRecord urr in CorrectUrList)
                        //{
                        //    StudUpdateRecordEntity sure = new StudUpdateRecordEntity();
                        //    //班別
                        //    sure.ClassType = urr.ClassType;
                        //    //科別代碼
                        //    if (DeptCodeDict.ContainsKey(urr.Department))
                        //        sure.DeptCode = DeptCodeDict[urr.Department];
                        //    else
                        //        sure.DeptCode = urr.Department;


                        //    //上傳類別
                        //    sure.UploadType = "";
                        //    //年級
                        //    sure.GradeYear = urr.GradeYear;
                        //    //學號
                        //    sure.StudentNumber = urr.StudentNumber;
                        //    //姓名
                        //    sure.StudentName = urr.StudentName;
                        //    //身分證字號
                        //    sure.IDNumber = urr.IDNumber;
                        //    //註1
                        //    sure.IDNumber1 = urr.IDNumberComment;
                        //    //性別代碼
                        //    if (string.IsNullOrEmpty(urr.Gender))
                        //        sure.GenderCode = "";
                        //    else
                        //    {
                        //        if (urr.Gender == "男")
                        //            sure.GenderCode = "1";
                        //        if (urr.Gender == "女")
                        //            sure.GenderCode = "2";
                        //    }
                        //    //出生日期
                        //    sure.Birthday = GetConvertStrDate(urr.Birthdate);

                        //    //特殊身份代碼
                        //    sure.StudTypeCode = urr.SpecialStatus;

                        //    // 屬於新生異動 ---
                        //    if (NewStudUrr != null)
                        //    {
                        //        //國中畢業學年度
                        //        sure.JHGradeSchoolYear = NewStudUrr.GraduateSchoolYear;
                        //        //新生入學學年度
                        //        if (NewStudUrr.SchoolYear.HasValue)
                        //            sure.EnrollSchoolYear = NewStudUrr.SchoolYear.Value.ToString();
                        //        else
                        //            sure.EnrollSchoolYear = "";
                        //        //入學資格代碼
                        //        sure.EnrollCode = NewStudUrr.GraduateComment;
                        //        //入學資格學校名稱
                        //        sure.EnrollSchoolName = NewStudUrr.GraduateSchool;
                        //        //入學資格證明文件
                        //        if (string.IsNullOrEmpty(NewStudUrr.GraduateComment) || NewStudUrr.GraduateComment == "1")
                        //            sure.EnrollDoc = "";
                        //        else
                        //            sure.EnrollDoc = NewStudUrr.GraduateComment;
                        //    }
                        //    else
                        //    {
                        //        //國中畢業學年度
                        //        sure.JHGradeSchoolYear = "";
                        //        //新生入學學年度
                        //        sure.EnrollSchoolYear = "";
                        //        //入學資格代碼
                        //        sure.EnrollCode = "";
                        //        //入學資格學校名稱
                        //        sure.EnrollSchoolName = "";
                        //        //入學資格證明文件
                        //        sure.EnrollDoc = "";

                        //    }
                        //    //最新狀態代碼
                        //    sure.StatusCode = "";
                        //    //最新狀態文字
                        //    sure.StatusText = "";
                        //    //最新狀態文號
                        //    sure.StatusNo = "";
                        //    //最新狀態核准日期
                        //    sure.StatusDate = "";

                        //    //最新更正代碼
                        //    sure.CorrectCode = "";
                        //    //最新更正文字
                        //    sure.CorrectText = "";
                        //    //最新更正文號
                        //    sure.CorrectNo = "";
                        //    //最新更正核准日期
                        //    sure.CorrectDate = "";

                        //    if (CorrectUrList.Count > 0)
                        //    {
                        //        CorrectUrList.Sort(new Comparison<SHUpdateRecordRecord>(UpdateRecSort1));
                        //            //最新更正代碼
                        //            sure.CorrectCode = CorrectUrList[0].UpdateCode;
                        //            //最新更正文字
                        //            sure.CorrectText = GetUpdateRecAddDoc(CorrectUrList[0].LastADNumber);
                        //            //最新更正文號
                        //            sure.CorrectNo = GetUpdateRecAddNo(CorrectUrList[0].LastADNumber);
                        //            //最新更正核准日期
                        //            sure.CorrectDate = GetConvertStrDate(CorrectUrList[0].LastADDate);

                        //    }

                        //    //備註
                        //    sure.Memo = "";
                        //    //異動日期
                        //    sure.UpdateDate = urr.UpdateDate;
                        //    //學生狀態
                        //    sure.StudStatus = SelectStudDict[urr.StudentID].Status.ToString();

                        //    Result.Add(sure);
                        //}
                    }
                }
            }
            catch
                (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }

            // 依學號遞增排序
            Result.Sort(new Comparison<StudUpdateRecordEntity>(StudNumberSort));

            return Result;
        }


        public static int UpdateRecSort1(SHUpdateRecordRecord x, SHUpdateRecordRecord y)
        {
            DateTime dtx, dty;

            DateTime.TryParse(x.UpdateDate , out dtx);
            DateTime.TryParse(y.UpdateDate , out dty);
            return dty.CompareTo(dtx);        
        }

        // 依學號排序
        public static int StudNumberSort(StudUpdateRecordEntity x, StudUpdateRecordEntity y)
        {
            return x.StudentNumber.CompareTo(y.StudentNumber);        
        }



        /// <summary>
        /// 取得日期轉換2010/1/1->990101
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetConvertStrDate(string str)
        {
            string retVal = "";

            DateTime dt;

            if (DateTime.TryParse(str, out dt))
                retVal = String.Format("{0:00}", (dt.Year - 1911)) + String.Format("{0:00}", dt.Month) + String.Format("{0:00}", dt.Day);

            return retVal;        
        }

        /// <summary>
        /// 取得字號前面文字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetUpdateRecAddDoc(string str)
        {
            string retVal = "";

            int i = str.IndexOf("字");

            if (i > 1)
                retVal=str.Substring(0, i).Trim ();

            return retVal;
        }

        /// <summary>
        /// 取的第字的數字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetUpdateRecAddNo(string str)
        {
            string retVal = "";
            int s1 = str.IndexOf("第");
            int e1 = str.IndexOf("號");
            if(s1 >0 && e1>s1)
                retVal = str.Substring((s1 + 1), (e1 - s1 - 1)).Trim();
            return retVal;
        }
    }
}
