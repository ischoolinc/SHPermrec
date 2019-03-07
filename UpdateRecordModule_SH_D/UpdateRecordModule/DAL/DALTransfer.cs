﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SHSchool.Data;
using System.Xml;
using System.Text.RegularExpressions;
using FISCA.DSAUtil;

namespace UpdateRecordModule_SH_D.DAL
{
    /// <summary>
    /// 資料存取轉換
    /// </summary>
    public class DALTransfer
    {

        // 名冊異動列別集合
        private static Dictionary<string, StudUpdateRecBatchCreator.UpdateRecBatchType> _UpdateRecBatchTypeDict = new Dictionary<string, StudUpdateRecBatchCreator.UpdateRecBatchType>();

        /// <summary>
        /// 取得異動代碼 XElement
        /// </summary>
        /// <returns></returns>
        public static XElement GetUpdateCodeList()
        {

            return XElement.Parse(Properties.Resources.UpdateCode_SHD);
        }

        /// <summary>
        /// 取得異動班別
        /// </summary>
        /// <returns></returns>
        public static List<string> GetClassTypeList()
        {
            List<string> retValue = new List<string>();
            retValue.Add("1-日間部");
            retValue.Add("2-夜間部");
            retValue.Add("3-實用技能學程");
            retValue.Add("4-建教班");
            retValue.Add("7-重點產業班/台德菁英班/雙軌旗艦訓練計畫專班");
            retValue.Add("8-建教僑生專班");
            retValue.Add("01-核定班");
            retValue.Add("02-編制班");
            retValue.Add("03-自給自足班");
            retValue.Add("04-員工進修班");
            retValue.Add("05-重點產業班");
            retValue.Add("06-產業人力套案專班");

            return retValue;
        }

        /// <summary>
        /// 取得單筆學生特殊身分代碼
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        public static string GetSpecialCode(string StudentID)
        {
            // 取得學籍身分對照，如果有比對到回傳 1,2..，沒有回傳空字串
            List<string> strList = (from xx in SHPermrecStatusMapping.SelectyByStudentID(StudentID) orderby xx.Code select xx.Code).ToList();
            if (strList.Count > 0)
            {
                // 移除空白
                strList.Remove("");
                return string.Join(",", strList.ToArray());
            }
            else
                return string.Empty;
        }

        ///// <summary>
        ///// 取得新生入學資格
        ///// </summary>
        ///// <returns></returns>
        //public static List<string> GetNewStudCode()
        //{
        //    List<string> strList = new List<string>();
        //    strList.Add("001-持國民中學畢業證明書者(含國中補校)");
        //    strList.Add("002-持國民中學補習學校資格證明書者");
        //    strList.Add("003-持國民中學補習學校結業證明書者");
        //    strList.Add("004-持國民中學修(結)業證明書者(修畢三學年全部課程)");
        //    strList.Add("005-持國民中學畢業程度學力鑑定考試及格證明書者");
        //    strList.Add("006-回國僑生介考（專案核准）");
        //    strList.Add("007-持大陸學歷者（需附證明文件）");
        //    strList.Add("008-特殊教育學校學生（需附證明文件）");
        //    strList.Add("009-持國外學歷者（需附證明文件）");
        //    strList.Add("099-其他（需附證明文件）");
        //    return strList;
        //}

        /// <summary>
        /// 將異動 SHDAL 資料轉換成名冊用 Rec
        /// </summary>
        /// <param name="recList"></param>
        /// <returns></returns>
        public static List<BL.StudUpdateRecDoc> ConvertSHUpdateRecToStudUpdateRec(List<SHUpdateRecordRecord> recList)
        {
            List<BL.StudUpdateRecDoc> StudRecList = new List<UpdateRecordModule_SH_D.BL.StudUpdateRecDoc>();

            Dictionary<string, string> deptCodeDict = new Dictionary<string, string>();
            foreach (SHDepartmentRecord rec in SHDepartment.SelectAll())
            {
                if (!deptCodeDict.ContainsKey(rec.FullName))
                    deptCodeDict.Add(rec.FullName, rec.Code);

                //if (!deptCodeDict.ContainsKey(rec.Name))
                //    deptCodeDict.Add(rec.Name, rec.Code);
            }

            //int depIdx = 0;
            foreach (SHUpdateRecordRecord rec in recList)
            {
                BL.StudUpdateRecDoc studUrec = new UpdateRecordModule_SH_D.BL.StudUpdateRecDoc();
                studUrec.UpdateDescription = rec.UpdateDescription;
                studUrec.UpdateDate = rec.UpdateDate;
                studUrec.UpdateCode = rec.UpdateCode;
                studUrec.StudentNumber = rec.StudentNumber;
                studUrec.StudentName = rec.StudentName;
                studUrec.SpecialStatus = rec.SpecialStatus;
                studUrec.PreviousStudentNumber = rec.PreviousStudentNumber;
                studUrec.PreviousSchoolLastADNumber = rec.PreviousSchoolLastADNumber;
                studUrec.PreviousSchoolLastADDate = rec.PreviousSchoolLastADDate;
                studUrec.PreviousSchool = rec.PreviousSchool;
                studUrec.PreviousGradeYear = rec.PreviousGradeYear;
                studUrec.PreviousDepartment = rec.PreviousDepartment;
                studUrec.OldDepartmentCode = rec.OldDepartmentCode;
                studUrec.OldClassType = rec.OldClassType;
                studUrec.NewData = rec.NewData;
                studUrec.LastUpdateCode = rec.LastUpdateCode;
                studUrec.LastADNumber = rec.LastADNumber;
                studUrec.LastADDate = rec.LastADDate;
                studUrec.IDNumberComment = rec.IDNumberComment;
                studUrec.IDNumber = rec.IDNumber;
                studUrec.GraduateSchoolYear = rec.GraduateSchoolYear;
                studUrec.GraduateSchoolLocationCode = rec.GraduateSchoolLocationCode;
                studUrec.GraduateSchoolCode = rec.GraduateSchoolCode;
                studUrec.GraduateSchool = rec.GraduateSchool;
                studUrec.GraduateComment = rec.GraduateComment;
                studUrec.GraduateCertificateNumber = rec.GraduateCertificateNumber;
                studUrec.GradeYear = rec.GradeYear;
                studUrec.Gender = rec.Gender;

                studUrec.Code7SchoolCode = rec.Code7SchoolCode;
                studUrec.Code7DeptCode = rec.Code7DeptCode;
                studUrec.Code71BeginDate = rec.Code71BeginDate;
                studUrec.Code71EndDate = rec.Code71EndDate;
                studUrec.Code72BeginDate = rec.Code72BeginDate;
                studUrec.Code72EndDate = rec.Code72EndDate;

                studUrec.ReplicatedSchoolRollNumber = rec.ReplicatedSchoolRollNumber;

                if (rec.Gender == "男")
                    studUrec.GenderCode = "1";
                else if (rec.Gender == "女")
                    studUrec.GenderCode = "2";
                else
                    studUrec.GenderCode = "";

                //// 當科別有:取前面值
                //depIdx = rec.Department.IndexOf(":");
                //if(depIdx >1)
                //    studUrec.Department = rec.Department.Substring(0,depIdx);
                //else
                studUrec.Department = rec.Department;

                if (deptCodeDict.ContainsKey(rec.Department))
                    studUrec.DeptCode = deptCodeDict[rec.Department];
                studUrec.Comment = rec.Comment;
                studUrec.ClassType = rec.ClassType;
                studUrec.Birthday = rec.Birthdate;
                studUrec.ADNumber = rec.ADNumber;
                studUrec.ADDate = rec.ADDate;
                studUrec.URID = rec.ID;
                studUrec.StudentID = rec.StudentID;
                if (rec.Student != null)
                    studUrec.StudStatus = rec.Student.Status.ToString();

                studUrec.PreviousSemester = rec.PreviousSemester;
                studUrec.NewStudentNumber = rec.NewStudentNumber;
                studUrec.Comment2 = rec.Comment2;
                studUrec.GraduateDocument = rec.GraduateDocument;
                studUrec.ExpectGraduateSchoolYear = rec.ExpectGraduateSchoolYear;
                StudRecList.Add(studUrec);
            }
            return StudRecList;
        }



        /// <summary>
        /// 將 XML 轉換成 StudUpdateRecDoc List
        /// </summary>
        public static List<BL.StudUpdateRecDoc> ConvertXmlToStudUpdateRecDocList(XElement XmlElms)
        {
            List<BL.StudUpdateRecDoc> retVal = new List<UpdateRecordModule_SH_D.BL.StudUpdateRecDoc>();

            List<XElement> dataElms = (from elm in XmlElms.Elements("清單") select elm).ToList();

            int depIdx = 0;
            foreach (XElement d1Elm in dataElms)
            {
                // 取得年級與科別
                string strGradeYear = "", strDeptName = "", strDeptCode = "";
                if (d1Elm.Attribute("年級") != null)
                    strGradeYear = d1Elm.Attribute("年級").Value;

                //// 科別處理:問題，當有:取:前文字
                if (d1Elm.Attribute("科別") != null)
                {
                    //depIdx = d1Elm.Attribute("科別").Value.IndexOf(":");
                    //if(depIdx>1)
                    //    strDeptName = d1Elm.Attribute("科別").Value.Substring(0,depIdx);
                    //else
                    strDeptName = d1Elm.Attribute("科別").Value;
                }
                if (d1Elm.Attribute("科別代號") != null)
                    strDeptCode = d1Elm.Attribute("科別代號").Value;

                // 取得異動紀錄集合
                List<XElement> upRecList = (from elm in d1Elm.Elements("異動紀錄") select elm).ToList();

                // 解析異動紀錄
                foreach (XElement elm in upRecList)
                {
                    BL.StudUpdateRecDoc studUpdateRec = new UpdateRecordModule_SH_D.BL.StudUpdateRecDoc();
                    studUpdateRec.Department = strDeptName;
                    studUpdateRec.GradeYear = strGradeYear;

                    foreach (XAttribute val in elm.Attributes())
                    {
                        switch (val.Name.ToString())
                        {
                            case "異動代號":
                                studUpdateRec.UpdateCode = val.Value;
                                break;

                            case "異動代碼":
                                studUpdateRec.UpdateCode = val.Value;
                                break;

                            case "原因及事項":
                                studUpdateRec.UpdateDescription = val.Value;
                                break;

                            case "異動日期":
                                studUpdateRec.UpdateDate = val.Value;
                                break;

                            case "備註":
                                studUpdateRec.Comment = val.Value;
                                break;
                            case "班別":
                                studUpdateRec.ClassType = val.Value;
                                break;
                            //case "科別":
                            //    studUpdateRec.Department = val.Value;
                            //break;

                            case "科別代碼":
                                studUpdateRec.DeptCode = strDeptCode;
                                break;
                            case "特殊身分代碼":
                                studUpdateRec.SpecialStatus = val.Value;
                                break;
                            case "姓名":
                                studUpdateRec.StudentName = val.Value;
                                break;
                            case "學號":
                                studUpdateRec.StudentNumber = val.Value;
                                break;

                            // 當空值才寫入，主要處理分與份，字的問題
                            case "身份證字號":
                            case "身分證字號":
                            case "身分證號":
                            case "身份證號":
                                if (string.IsNullOrEmpty(studUpdateRec.IDNumber))
                                    studUpdateRec.IDNumber = val.Value;
                                break;

                            case "生日":
                                studUpdateRec.Birthday = val.Value;
                                break;

                            case "註1":
                            case "身分證註記":
                            case "身份證註記":
                                if (string.IsNullOrEmpty(studUpdateRec.IDNumberComment))
                                    studUpdateRec.IDNumberComment = val.Value;
                                break;

                            case "更正後身分證註記":
                                if (string.IsNullOrEmpty(studUpdateRec.Comment2))
                                    studUpdateRec.Comment2 = val.Value;
                                break;

                            case "轉入身分別代碼":
                                if (string.IsNullOrEmpty(studUpdateRec.Comment2))
                                    studUpdateRec.Comment2 = val.Value;
                                break;

                            case "性別":
                                studUpdateRec.Gender = val.Value;
                                break;

                            case "性別代碼":
                                studUpdateRec.GenderCode = val.Value;
                                break;

                            case "畢業國中":
                                studUpdateRec.GraduateSchool = val.Value;
                                break;
                            case "畢業國中所在地代碼":
                                studUpdateRec.GraduateSchoolLocationCode = val.Value;
                                break;
                            case "入學資格註記":
                                studUpdateRec.GraduateComment = val.Value;
                                break;
                            case "畢業國中代碼":
                                studUpdateRec.GraduateSchoolCode = val.Value;
                                break;
                            case "國中畢業學年度":
                                studUpdateRec.GraduateSchoolYear = val.Value;
                                break;
                            case "核准日期":
                                studUpdateRec.ADDate = val.Value;
                                break;
                            case "核准文號":
                                studUpdateRec.ADNumber = val.Value;
                                break;
                            case "舊科別代碼":
                                studUpdateRec.OldDepartmentCode = val.Value;
                                break;

                            case "舊班別":
                                studUpdateRec.OldClassType = val.Value;
                                break;

                            case "備查日期":
                                studUpdateRec.LastADDate = val.Value;
                                break;

                            case "備查文號":
                                studUpdateRec.LastADNumber = val.Value;
                                break;

                            case "原就讀學校":
                            case "轉入前學生資料_學校":
                                studUpdateRec.PreviousSchool = val.Value;
                                break;
                            case "原就讀學號":
                            case "轉入前學生資料_學號":
                                studUpdateRec.PreviousStudentNumber = val.Value;
                                break;
                            case "原就讀科別":
                            case "轉入前學生資料_科別":
                                studUpdateRec.PreviousDepartment = val.Value;
                                break;

                            case "原就讀年級":
                            case "轉入前學生資料_年級":
                                studUpdateRec.PreviousGradeYear = val.Value;
                                break;

                            case "原就讀備查日期":
                            case "轉入前學生資料_備查日期":
                                studUpdateRec.PreviousSchoolLastADDate = val.Value;
                                break;

                            case "原就讀備查文號":
                            case "轉入前學生資料_備查文號":
                                studUpdateRec.PreviousSchoolLastADNumber = val.Value;
                                break;
                            case "最後異動代碼":
                                studUpdateRec.LastUpdateCode = val.Value;
                                break;
                            case "畢業證書字號":
                                studUpdateRec.GraduateCertificateNumber = val.Value;
                                break;
                            //case "年級":
                            //    studUpdateRec.GradeYear = val.Value;
                            //    break;
                            case "新資料":
                                studUpdateRec.NewData = val.Value;
                                break;

                            case "異動編號":
                                studUpdateRec.URID = val.Value;
                                break;

                            case "學生編號":
                                studUpdateRec.StudentID = val.Value;
                                break;

                            case "原就讀學期":
                                studUpdateRec.PreviousSemester = val.Value;
                                break;

                            case "新學號":
                                studUpdateRec.NewStudentNumber = val.Value;
                                break;

                            case "入學資格證明文件":
                                studUpdateRec.GraduateDocument = val.Value;
                                break;

                            case "應畢業學年度":
                                studUpdateRec.ExpectGraduateSchoolYear = val.Value;
                                break;

                            case "借讀學校代碼": studUpdateRec.Code7SchoolCode = val.Value; break;
                            case "借讀科別代碼": studUpdateRec.Code7DeptCode = val.Value; break;
                            case "申請開始日期": studUpdateRec.Code71BeginDate = val.Value; break;
                            case "申請結束日期": studUpdateRec.Code71EndDate = val.Value; break;
                            case "實際開始日期": studUpdateRec.Code72BeginDate = val.Value; break;
                            case "實際結束日期": studUpdateRec.Code72EndDate = val.Value; break;

                            case "雙重學籍編號": studUpdateRec.ReplicatedSchoolRollNumber = val.Value; break;

                        }
                    }

                    // 當學生科別代號沒有，使用名冊本身預設
                    if (string.IsNullOrEmpty(studUpdateRec.DeptCode))
                        studUpdateRec.DeptCode = strDeptCode;

                    // 當學生沒有性別代碼，解析填入男1,女2
                    if (string.IsNullOrEmpty(studUpdateRec.GenderCode))
                    {
                        if (studUpdateRec.Gender == "男")
                            studUpdateRec.GenderCode = "1";

                        if (studUpdateRec.Gender == "女")
                            studUpdateRec.GenderCode = "2";
                    }

                    retVal.Add(studUpdateRec);
                }
            }

            return retVal;
        }

        /// <summary>
        /// 將 StudUpdateRecDoc List 轉成 XML
        /// </summary>
        /// <returns></returns>
        public static XElement ConvertStudUpdateRecDocToXML(List<BL.StudUpdateRecDoc> updateRecList)
        {
            // 排序年級與科別
            //var data = from ud in updateRecList orderby;
            Dictionary<string, List<BL.StudUpdateRecDoc>> data = new Dictionary<string, List<UpdateRecordModule_SH_D.BL.StudUpdateRecDoc>>();
            Dictionary<string, string> deptCodeDict = new Dictionary<string, string>();

            foreach (SHDepartmentRecord rec in SHDepartment.SelectAll())
            {
                if (!deptCodeDict.ContainsKey(rec.FullName))
                    deptCodeDict.Add(rec.FullName, rec.Code);

                //if (!deptCodeDict.ContainsKey(rec.Name))
                //    deptCodeDict.Add(rec.Name, rec.Code);
            }

            foreach (BL.StudUpdateRecDoc val in updateRecList)
            {
                string key = val.GradeYear + "_" + val.Department + "_";
                if (data.ContainsKey(key))
                    data[key].Add(val);
                else
                {
                    List<BL.StudUpdateRecDoc> xx = new List<UpdateRecordModule_SH_D.BL.StudUpdateRecDoc>();
                    xx.Add(val);
                    data.Add(key, xx);
                }
            }

            //2018/02/09 穎驊新增
            //若本次異動名冊為初次產生，則尋找之前對應的異動名冊，將相關資料帶到封面

            List<SHUpdateRecordBatchRecord> recBatch_list = new List<SHUpdateRecordBatchRecord>();

            //當學年度的
            //List<SHUpdateRecordBatchRecord> recBatch_list = SHUpdateRecordBatch.SelectBySchoolYearAndSemester(int.Parse(Global._GSchoolYear), int.Parse(Global._GSemester));

            // 以現在的學年度往回去翻最多兩學年 
            for (int schoolyear_index = 0; schoolyear_index < 2; schoolyear_index++)
            {
                recBatch_list.AddRange(SHUpdateRecordBatch.SelectBySchoolYearAndSemester(int.Parse(Global._GSchoolYear) - schoolyear_index, int.Parse(Global._GSemester)));

                // 假如是第一學期，則幫補找第二學期
                if (int.Parse(Global._GSemester) == 1)
                {
                    recBatch_list.AddRange(SHUpdateRecordBatch.SelectBySchoolYearAndSemester(int.Parse(Global._GSchoolYear) - schoolyear_index, int.Parse(Global._GSemester) + 1));
                }
                // 假如是第二學期，則幫補找第一學期
                if (int.Parse(Global._GSemester) == 2)
                {
                    recBatch_list.AddRange(SHUpdateRecordBatch.SelectBySchoolYearAndSemester(int.Parse(Global._GSchoolYear) - schoolyear_index, int.Parse(Global._GSemester) - 1));
                }
            }



            XElement retVal = new XElement("異動名冊");
            retVal.SetAttributeValue("學年度", Global._GSchoolYear);
            retVal.SetAttributeValue("學期", Global._GSemester);
            retVal.SetAttributeValue("學校代碼", Global._GSchoolCode);
            retVal.SetAttributeValue("學校代號", Global._GSchoolCode);
            retVal.SetAttributeValue("學校名稱", Global._GSchoolName);
            retVal.SetAttributeValue("類別", Global._GUpdateBatchType);

            foreach (KeyValuePair<string, List<BL.StudUpdateRecDoc>> val in data)
            {
                // 解析年級科別
                int idx = val.Key.IndexOf("_");
                string grYear = val.Key.Substring(0, idx);
                string DeptName = val.Key.Substring(idx + 1, (val.Key.Length - (idx + 2)));
                string DeptCode = "";
                if (deptCodeDict.ContainsKey(DeptName))
                    DeptCode = deptCodeDict[DeptName];

                XElement elmGrDept = new XElement("清單");
                elmGrDept.SetAttributeValue("年級", grYear);
                elmGrDept.SetAttributeValue("科別", DeptName);
                elmGrDept.SetAttributeValue("科別代碼", DeptCode);
                elmGrDept.SetAttributeValue("科別代號", DeptCode);
                foreach (BL.StudUpdateRecDoc rec in val.Value)
                {
                    XElement elm = new XElement("異動紀錄");
                    elm.SetAttributeValue("異動代碼", rec.UpdateCode);
                    elm.SetAttributeValue("異動代號", rec.UpdateCode);
                    elm.SetAttributeValue("原因及事項", rec.UpdateDescription);
                    elm.SetAttributeValue("異動日期", rec.UpdateDate);
                    elm.SetAttributeValue("備註", rec.Comment);
                    elm.SetAttributeValue("班別", rec.ClassType);
                    elm.SetAttributeValue("科別", rec.Department);
                    elm.SetAttributeValue("科別代碼", rec.DeptCode);
                    elm.SetAttributeValue("特殊身分代碼", rec.SpecialStatus);
                    elm.SetAttributeValue("特殊身份代碼", rec.SpecialStatus);
                    elm.SetAttributeValue("姓名", rec.StudentName);
                    elm.SetAttributeValue("學號", rec.StudentNumber);
                    elm.SetAttributeValue("身分證字號", rec.IDNumber);
                    elm.SetAttributeValue("身分證號", rec.IDNumber);
                    elm.SetAttributeValue("生日", rec.Birthday);
                    DateTime dt;
                    if (DateTime.TryParse(rec.Birthday, out dt))
                        elm.SetAttributeValue("出生年月日", (dt.Year - 1911) + "/" + dt.Month + "/" + dt.Day);
                    else
                        elm.SetAttributeValue("出生年月日", "");
                    elm.SetAttributeValue("身分證註記", rec.IDNumberComment);
                    elm.SetAttributeValue("註1", rec.IDNumberComment);

                    elm.SetAttributeValue("性別", rec.Gender);
                    elm.SetAttributeValue("性別代碼", rec.GenderCode);
                    elm.SetAttributeValue("性別代號", rec.GenderCode);
                    elm.SetAttributeValue("畢業國中", rec.GraduateSchool);
                    elm.SetAttributeValue("畢業國中所在地代碼", rec.GraduateSchoolLocationCode);
                    elm.SetAttributeValue("畢業國中所在縣市代號", rec.GraduateSchoolLocationCode);
                    elm.SetAttributeValue("入學資格註記", rec.GraduateComment);
                    elm.SetAttributeValue("入學資格代號", rec.UpdateCode);
                    elm.SetAttributeValue("畢業國中代碼", rec.GraduateSchoolCode);
                    elm.SetAttributeValue("國中畢業學年度", rec.GraduateSchoolYear);
                    elm.SetAttributeValue("核准日期", rec.ADDate);
                    elm.SetAttributeValue("核准文號", rec.ADNumber);
                    elm.SetAttributeValue("舊科別代碼", rec.OldDepartmentCode);
                    elm.SetAttributeValue("舊班別", rec.OldClassType);
                    elm.SetAttributeValue("備查日期", rec.LastADDate);
                    elm.SetAttributeValue("備查文號", rec.LastADNumber);
                    elm.SetAttributeValue("原就讀學校", rec.PreviousSchool);
                    elm.SetAttributeValue("原就讀學號", rec.PreviousStudentNumber);
                    elm.SetAttributeValue("原就讀科別", rec.PreviousDepartment);
                    elm.SetAttributeValue("原就讀年級", rec.PreviousGradeYear);
                    elm.SetAttributeValue("原就讀學期", rec.PreviousSemester);
                    elm.SetAttributeValue("原就讀備查日期", rec.PreviousSchoolLastADDate);
                    elm.SetAttributeValue("原就讀備查文號", rec.PreviousSchoolLastADNumber);
                    elm.SetAttributeValue("最後異動代碼", rec.LastUpdateCode);
                    elm.SetAttributeValue("最後異動代號", rec.LastUpdateCode);
                    elm.SetAttributeValue("畢業證書字號", rec.GraduateCertificateNumber);
                    elm.SetAttributeValue("年級", rec.GradeYear);
                    elm.SetAttributeValue("新資料", rec.NewData);
                    elm.SetAttributeValue("異動編號", rec.URID);
                    elm.SetAttributeValue("學生編號", rec.StudentID);
                    elm.SetAttributeValue("新學號", rec.NewStudentNumber);
                    elm.SetAttributeValue("轉入前學生資料_學校", rec.PreviousSchool);
                    elm.SetAttributeValue("轉入前學生資料_學號", rec.PreviousStudentNumber);
                    elm.SetAttributeValue("轉入前學生資料_科別", rec.PreviousDepartment);
                    elm.SetAttributeValue("轉入前學生資料_年級", rec.PreviousGradeYear);
                    elm.SetAttributeValue("轉入前學生資料_學期", rec.PreviousSemester);
                    elm.SetAttributeValue("轉入前學生資料_備查日期", rec.PreviousSchoolLastADDate);
                    elm.SetAttributeValue("轉入前學生資料_備查文號", rec.PreviousSchoolLastADNumber);
                    elm.SetAttributeValue("入學資格證明文件", rec.GraduateDocument);

                    elm.SetAttributeValue("雙重學籍編號", rec.ReplicatedSchoolRollNumber);

                    // 當他校轉入
                    if (rec.UpdateCode.Substring(0, 1) == "1")
                        elm.SetAttributeValue("轉入身分別代碼", rec.Comment2);
                    else
                        elm.SetAttributeValue("更正後身分證註記", rec.Comment2);

                    elm.SetAttributeValue("應畢業學年度", rec.ExpectGraduateSchoolYear);

                    elm.SetAttributeValue("借讀學校代碼", rec.Code7SchoolCode);
                    elm.SetAttributeValue("借讀科別代碼", rec.Code7DeptCode);
                    elm.SetAttributeValue("申請開始日期", rec.Code71BeginDate);
                    elm.SetAttributeValue("申請結束日期", rec.Code71EndDate);
                    elm.SetAttributeValue("實際開始日期", rec.Code72BeginDate);
                    elm.SetAttributeValue("實際結束日期", rec.Code72EndDate);

                    elmGrDept.Add(elm);
                }


                //2018/02/14 穎驊新增
                //填封面資料
                //傳入 封面種類、目前學年、年級、科別代碼、近期的(三年內)所有異動名冊
                XElement elmGrDeptCover = AutoGenerateCover(Global._GUpdateBatchType, Global._GSchoolYear, grYear, DeptCode, recBatch_list);



                //加入封面
                elmGrDept.Add(elmGrDeptCover);

                retVal.Add(elmGrDept);
            }
            return retVal;
        }


        /// <summary>
        /// 取得異動名冊
        /// </summary>
        public static BL.StudUpdateRecBatchRec GetStudUpdateRecBatchRec(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return null;

            BL.StudUpdateRecBatchRec surbr = new UpdateRecordModule_SH_D.BL.StudUpdateRecBatchRec();
            SHUpdateRecordBatchRecord recBatch = SHUpdateRecordBatch.SelectByID(ID);
            surbr.ID = recBatch.ID;
            surbr.ADDate = recBatch.ADDate;
            surbr.ADNumber = recBatch.ADNumber;
            surbr.Name = recBatch.Name;
            surbr.SchoolYear = recBatch.SchoolYear;
            surbr.Semester = recBatch.Semester;
            surbr.Content = recBatch.Content;
            string content = recBatch.Content.InnerXml;
            XElement elm = XElement.Parse(content);
            surbr.StudUpdateRecDocList = ConvertXmlToStudUpdateRecDocList(elm);
            foreach (XAttribute xx in elm.Attributes())
            {
                switch (xx.Name.ToString())
                {
                    case "學校代號":
                        surbr.SchoolCode = xx.Value;
                        break;
                    case "學校代碼":
                        surbr.SchoolCode = xx.Value;
                        break;
                    case "學校名稱":
                        surbr.SchoolName = xx.Value;
                        break;
                    case "類別":
                        surbr.UpdateType = xx.Value;
                        break;
                }
            }
            return surbr;
        }

        /// <summary>
        /// 儲存異動名冊
        /// </summary>
        /// <param name="StudUpdateRecBRec"></param>
        public static void SetStudUpdateRecBatchRec(BL.StudUpdateRecBatchRec StudUpdateRecBRec, bool isInsert)
        {
            SHUpdateRecordBatchRecord shurbr = new SHUpdateRecordBatchRecord();


            //若為新增產生名冊
            if (isInsert)
            {
                shurbr.ADDate = StudUpdateRecBRec.ADDate;
                shurbr.ADNumber = StudUpdateRecBRec.ADNumber;
                Global._GSchoolCode = K12.Data.School.Code;
                Global._GSchoolName = K12.Data.School.ChineseName;
                Global._GUpdateBatchType = StudUpdateRecBRec.UpdateType;
                Global._GSchoolYear = StudUpdateRecBRec.SchoolYear.ToString();
                Global._GSemester = StudUpdateRecBRec.Semester.ToString();
                Global._GDocName = StudUpdateRecBRec.Name;

                // 將 XElement 轉型 XmlElement
                shurbr.Content = new XmlDocument().ReadNode(ConvertStudUpdateRecDocToXML(StudUpdateRecBRec.StudUpdateRecDocList).CreateReader()) as XmlElement;

                shurbr.ID = StudUpdateRecBRec.ID;
                shurbr.Name = StudUpdateRecBRec.Name;
                shurbr.SchoolYear = StudUpdateRecBRec.SchoolYear;
                shurbr.Semester = StudUpdateRecBRec.Semester;

                SHUpdateRecordBatch.Insert(shurbr);
            }
            //若為更新名冊(只有在上傳文號時會使用)
            else
            {
                shurbr.ADDate = StudUpdateRecBRec.ADDate;
                shurbr.ADNumber = StudUpdateRecBRec.ADNumber;
                Global._GSchoolCode = K12.Data.School.Code;
                Global._GSchoolName = K12.Data.School.ChineseName;
                Global._GUpdateBatchType = StudUpdateRecBRec.UpdateType;
                Global._GSchoolYear = StudUpdateRecBRec.SchoolYear.ToString();
                Global._GSemester = StudUpdateRecBRec.Semester.ToString();
                Global._GDocName = StudUpdateRecBRec.Name;

                //更新名冊，只會更新文號，不會更新內容，故照舊
                // 將 string 轉型 XmlElement
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(SHUpdateRecordBatch.SelectByID(StudUpdateRecBRec.ID).Content.InnerXml);

                shurbr.Content = doc.DocumentElement;

                shurbr.ID = StudUpdateRecBRec.ID;
                shurbr.Name = StudUpdateRecBRec.Name;
                shurbr.SchoolYear = StudUpdateRecBRec.SchoolYear;
                shurbr.Semester = StudUpdateRecBRec.Semester;

                SHUpdateRecordBatch.Update(shurbr);
            }

        }

        /// <summary>
        /// 取得名冊內學年度
        /// </summary>
        /// <returns></returns>
        public static List<string> GetGovDocSchoolYearList()
        {
            List<string> retVal = new List<string>();
            // 載入現有名冊中的學年度清單
            DSResponse dsrsp = SmartSchool.Feature.QueryStudent.GetSchoolYearList();
            DSXmlHelper helper = dsrsp.GetContent();
            foreach (XmlNode node in helper.GetElements("SchoolYear"))
                retVal.Add(node.InnerText);
            retVal.OrderByDescending(x => x);
            return retVal;

        }

        /// <summary>
        /// 取得異動名冊異動類別 Dict
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, StudUpdateRecBatchCreator.UpdateRecBatchType> GetUpdateRecBatchTypeDict()
        {
            // 名冊列別
            _UpdateRecBatchTypeDict.Clear();
            _UpdateRecBatchTypeDict.Add(StudUpdateRecBatchCreator.UpdateRecBatchType.新生名冊.ToString(), StudUpdateRecBatchCreator.UpdateRecBatchType.新生名冊);
            _UpdateRecBatchTypeDict.Add(StudUpdateRecBatchCreator.UpdateRecBatchType.畢業名冊.ToString(), StudUpdateRecBatchCreator.UpdateRecBatchType.畢業名冊);
            _UpdateRecBatchTypeDict.Add(StudUpdateRecBatchCreator.UpdateRecBatchType.學籍異動名冊.ToString(), StudUpdateRecBatchCreator.UpdateRecBatchType.學籍異動名冊);
            _UpdateRecBatchTypeDict.Add(StudUpdateRecBatchCreator.UpdateRecBatchType.轉入學生名冊.ToString(), StudUpdateRecBatchCreator.UpdateRecBatchType.轉入學生名冊);
            _UpdateRecBatchTypeDict.Add(StudUpdateRecBatchCreator.UpdateRecBatchType.延修生名冊.ToString(), StudUpdateRecBatchCreator.UpdateRecBatchType.延修生名冊);
            _UpdateRecBatchTypeDict.Add(StudUpdateRecBatchCreator.UpdateRecBatchType.延修生畢業名冊.ToString(), StudUpdateRecBatchCreator.UpdateRecBatchType.延修生畢業名冊);
            _UpdateRecBatchTypeDict.Add(StudUpdateRecBatchCreator.UpdateRecBatchType.延修生學籍異動名冊.ToString(), StudUpdateRecBatchCreator.UpdateRecBatchType.延修生學籍異動名冊);
            _UpdateRecBatchTypeDict.Add(StudUpdateRecBatchCreator.UpdateRecBatchType.新生保留錄取資格名冊.ToString(), StudUpdateRecBatchCreator.UpdateRecBatchType.新生保留錄取資格名冊);
            _UpdateRecBatchTypeDict.Add(StudUpdateRecBatchCreator.UpdateRecBatchType.借讀學生名冊.ToString(), StudUpdateRecBatchCreator.UpdateRecBatchType.借讀學生名冊);



            return _UpdateRecBatchTypeDict;
        }

        /// <summary>
        /// 設定學生異動記錄 核准日期與文號
        /// </summary>
        /// <param name="ADDate"></param>
        /// <param name="ADNumber"></param>
        /// <param name="studURIDList"></param>
        public static void SetStudsUpdateRecADdata(string ADDate, string ADNumber, List<string> studURIDList)
        {
            // 取得異動 ID            
            List<SHUpdateRecordRecord> updateRecs = SHUpdateRecord.SelectByIDs(studURIDList);
            foreach (SHUpdateRecordRecord rec in updateRecs)
            {
                rec.ADDate = ADDate;
                rec.ADNumber = ADNumber;
            }

            // 更新異動資料
            SHUpdateRecord.Update(updateRecs);
        }


        /// <summary>
        /// 將報表 XML 轉成報表用 List
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public List<GovernmentalDocument.Reports.List.rpt_UpdateRecord> ConvertRptUpdateRecord(XmlElement source)
        {
            List<GovernmentalDocument.Reports.List.rpt_UpdateRecord> retVal = new List<GovernmentalDocument.Reports.List.rpt_UpdateRecord>();

            foreach (XmlElement Record in source.SelectNodes("清單/異動紀錄"))
            {
                GovernmentalDocument.Reports.List.rpt_UpdateRecord val = new GovernmentalDocument.Reports.List.rpt_UpdateRecord();

                //班別
                val.ClassType = Record.GetAttribute("班別");

                //科別代碼
                val.DeptCode = (Record.ParentNode as XmlElement).GetAttribute("科別代號");

                //學號
                val.StudentNumber = Record.GetAttribute("學號");

                // StudentID
                val.StudentID = Record.GetAttribute("學生編號");

                //姓名
                val.Name = Record.GetAttribute("姓名");

                //身分證字號
                val.IDNumber = Record.GetAttribute("身分證號");

                //註1
                val.Comment1 = Record.GetAttribute("註1");

                //性別代碼
                val.GenderCode = Record.GetAttribute("性別代號");
                //出生日期
                val.Birthday = GetBirthdateWithoutSlash(Record.GetAttribute("出生年月日"));

                //特殊身份代碼
                val.SpecialStatusCode = Record.GetAttribute("特殊身份代碼"); //原為抓取備註欄位
                                                                       //年級
                val.GradeYear = (Record.ParentNode as XmlElement).GetAttribute("年級");
                //異動原因代碼
                val.UpdateCode = Record.GetAttribute("異動代號");

                //異動日期
                val.UpdateDate = GetBirthdateWithoutSlash(Record.GetAttribute("異動日期"));

                // 原科別代碼
                if (Record.GetAttribute("原就讀科別").Trim() == "")
                    val.PreviousDeptCode = Record.GetAttribute("轉入前學生資料_科別");
                else
                    val.PreviousDeptCode = Record.GetAttribute("原就讀科別");

                // 原學校代碼
                if (Record.GetAttribute("原就讀學校").Trim() == "")
                    val.PreviousSchoolCode = Record.GetAttribute("轉入前學生資料_學校");
                else
                    val.PreviousSchoolCode = Record.GetAttribute("原就讀學校");

                // 原學號
                if (Record.GetAttribute("原就讀學號").Trim() == "")
                    val.PreviousStudentNumber = Record.GetAttribute("轉入前學生資料_學號");
                else
                    val.PreviousStudentNumber = Record.GetAttribute("原就讀學號");

                // 原年級
                if (Record.GetAttribute("原就讀年級").Trim() == "")
                    val.PreviousGradeYear = Record.GetAttribute("轉入前學生資料_年級");
                else
                    val.PreviousGradeYear = Record.GetAttribute("原就讀年級");

                // 原學期
                if (Record.GetAttribute("原就讀學期").Trim() == "")
                    val.PreviousSemester = Record.GetAttribute("轉入前學生資料_學期");
                else
                    val.PreviousSemester = Record.GetAttribute("原就讀學期");

                //原備查日期
                if (Record.GetAttribute("原就讀備查日期").Trim() == "")
                    val.PreviousSchoolLastADDate = GetBirthdateWithoutSlash(Record.GetAttribute("轉入前學生資料_備查日期"));
                else
                    val.PreviousSchoolLastADDate = GetBirthdateWithoutSlash(Record.GetAttribute("原就讀備查日期"));

                //原備查文字
                if (Record.GetAttribute("原就讀備查文號").Trim() == "")
                    val.PreviousSchoolLastADDoc = GetNumAndSrt1(Record.GetAttribute("轉入前學生資料_備查文號"));
                else
                    val.PreviousSchoolLastADDoc = GetNumAndSrt1(Record.GetAttribute("原就讀備查文號"));

                //原備查文號
                if (Record.GetAttribute("原就讀備查文號").Trim() == "")
                    val.PreviousSchoolLastADNum = GetNumAndSrt2(Record.GetAttribute("轉入前學生資料_備查文號"));
                else
                    val.PreviousSchoolLastADNum = GetNumAndSrt2(Record.GetAttribute("原就讀備查文號"));

                //更正後資料               
                val.NewData = Record.GetAttribute("新資料");

                // 新學號
                val.NewStudNumber = Record.GetAttribute("新學號");

                //備註說明
                val.Comment = Record.GetAttribute("備註");

                // 異動編號
                if (Record.GetAttribute("編號") != "")
                    val.URID = Record.GetAttribute("編號");
                else
                    val.URID = Record.GetAttribute("異動編號");


                // 備查日期
                val.LastADDate = GetBirthdateWithoutSlash(Record.GetAttribute("備查日期"));

                // 備查文字
                val.LastADDoc = GetNumAndSrt1(Record.GetAttribute("備查文號"));

                // 備查文號
                val.LastADNum = GetNumAndSrt2(Record.GetAttribute("備查文號"));

                // 畢業證書字號
                val.GraduateCertificateNumber = Record.GetAttribute("畢業證書字號");

                // 更正後身分證註記                
                val.Comment2 = Record.GetAttribute("更正後身分證註記");

                // 轉入身分別代碼 
                val.TransferStatus = Record.GetAttribute("轉入身分別代碼");

                // 應畢業學年度
                val.ExpectGraduateSchoolYear = Record.GetAttribute("應畢業學年度");

                // 借讀學校代碼
                val.Code7SchoolCode = Record.GetAttribute("借讀學校代碼");

                // 借讀科別代碼
                val.Code7DeptCode = Record.GetAttribute("借讀科別代碼");

                // 申請開始日期
                val.Code71BeginDate = Record.GetAttribute("申請開始日期");

                // 申請結束日期
                val.Code71EndDate = Record.GetAttribute("申請結束日期");

                // 實際開始日期
                val.Code72BeginDate = Record.GetAttribute("實際開始日期");

                // 實際結束日期
                val.Code72EndDate = Record.GetAttribute("實際結束日期");

                //雙重學籍編號
                val.ReplicatedSchoolRollNumber = Record.GetAttribute("雙重學籍編號");

                retVal.Add(val);
            }

            // 解析科別代碼用
            Dictionary<string, string> deptCodeDict = new Dictionary<string, string>();
            foreach (SHDepartmentRecord rec in SHDepartment.SelectAll())
                if (!deptCodeDict.ContainsKey(rec.FullName))
                    deptCodeDict.Add(rec.FullName, rec.Code);


            // 排序填入
            int uidint;
            retVal = (from data in retVal orderby data.Name, data.UpdateDate, int.TryParse(data.URID, out uidint) select data).ToList();

            int order = 0;
            string tmpStr = "";
            foreach (GovernmentalDocument.Reports.List.rpt_UpdateRecord rec in retVal)
            {

                if (rec.Name != tmpStr)
                {
                    order = 1;
                    rec.Order = order.ToString();
                }
                else
                    rec.Order = order.ToString();

                order++;
                tmpStr = rec.Name;


                // 教育部 99年學校代碼長度定義6碼，直接切
                if (rec.PreviousSchoolCode.Length >= 6)
                    rec.PreviousSchoolCode = rec.PreviousSchoolCode.Substring(0, 6);

                // 解析科別代碼
                if (deptCodeDict.ContainsKey(rec.PreviousDeptCode))
                    rec.PreviousDeptCode = deptCodeDict[rec.PreviousDeptCode];
            }

            return retVal;
        }

        private string GetBirthdateWithoutSlash(string orig)
        {
            if (string.IsNullOrEmpty(orig)) return orig;
            string[] array = orig.Split('/');
            int chang;
            if (array[0].Length == 4)
            {
                chang = int.Parse(array[0]) - 1911;
            }
            else
            {
                chang = int.Parse(array[0]);
            }
            return chang.ToString() + array[1].PadLeft(2, '0') + array[2].PadLeft(2, '0');
        }

        //切文字
        #region 切文字

        private string GetNumAndSrt1(string fuct)
        {
            // 2019/02/14 穎驊改寫， 根據最新的107年度規範，備查文字、文號 不要出現 字、第、號 字樣
            // 另外原有邏輯採用關鍵字(字、第、號)裁切，會出現無法對應部分文號 如 中市教高學統10712345678 的裁切
            // 這邊統一改寫 ，在移除字、第、號文字後， 使用正規表達式 Regex 裁切， 
            // 中文文字部分將會是 備查文字 ， 數字的部分 則為 備查文號

            #region 舊寫法
            //if (fuct.Contains("字"))
            //{
            //    if (fuct.Remove(fuct.LastIndexOf("字")) != "")
            //        return fuct.Remove(fuct.LastIndexOf("字"));
            //}

            //return GetNumAndSrt2(fuct); 
            #endregion

            fuct = fuct.Replace("字", "");
            fuct = fuct.Replace("第", "");
            fuct = fuct.Replace("號", "");

            // 以文字區別 裁切
            string[] numbers = Regex.Split(fuct, @"\d+");

            return numbers.Length > 1 ? numbers[0] : fuct;
        }

        #endregion

        //切文號
        #region 切文號

        private string GetNumAndSrt2(string fuct)
        {
            #region 舊寫法
            //if (fuct.Contains("第") && fuct.Contains("號"))
            //{
            //    return fuct.Substring(fuct.LastIndexOf("第") + 1, fuct.LastIndexOf("號") - fuct.LastIndexOf("第") - 1);
            //}

            //if (fuct.Contains("字") && fuct.Contains("號"))
            //{
            //    return fuct.Substring(fuct.LastIndexOf("字") + 1, fuct.LastIndexOf("號") - fuct.LastIndexOf("字") - 1);
            //} 
            #endregion
            fuct = fuct.Replace("字", "");
            fuct = fuct.Replace("第", "");
            fuct = fuct.Replace("號", "");

            // 以數字區別 裁切
            string[] numbers = Regex.Split(fuct, @"\D+");

            return numbers.Length > 1 ? numbers[1] : fuct;
        }

        #endregion


        // 2018/2/14 穎驊新增
        //依據目前的名冊種類、以前的資料 自動帶出封面資料
        //需傳入 名冊種類、年級、科別代碼、近期的(三年內)所有異動名冊 ，才能夠對應出資料
        private static XElement AutoGenerateCover(string _GUpdateBatchType, string schoolYear, string gradeYear, string deptCode, List<SHUpdateRecordBatchRecord> recBatch_list)
        {

            bool hasOldUpdateRecordBatchRecord = false;

            XElement elmGrDeptCover = new XElement("異動名冊封面");

            switch (_GUpdateBatchType)
            {
                case "新生名冊":
                    //rptBuild = new EnrollmentList();
                    elmGrDeptCover.SetAttributeValue("名冊別", "1");
                    elmGrDeptCover.SetAttributeValue("班別", "1");
                    elmGrDeptCover.SetAttributeValue("上傳類別", "");
                    elmGrDeptCover.SetAttributeValue("核定班數", "");
                    elmGrDeptCover.SetAttributeValue("核定學生數", "");
                    elmGrDeptCover.SetAttributeValue("實招班數", "");
                    elmGrDeptCover.SetAttributeValue("實招新生數", "");
                    elmGrDeptCover.SetAttributeValue("註1", "");
                    elmGrDeptCover.SetAttributeValue("備註說明", "");
                    break;
                case "延修生學籍異動名冊":
                    elmGrDeptCover.SetAttributeValue("名冊別", "6");
                    elmGrDeptCover.SetAttributeValue("應畢業學年度", "");
                    elmGrDeptCover.SetAttributeValue("班別", "");
                    elmGrDeptCover.SetAttributeValue("上傳類別", "");
                    elmGrDeptCover.SetAttributeValue("輔導延修學生數", "");
                    elmGrDeptCover.SetAttributeValue("原有學生數", "");
                    elmGrDeptCover.SetAttributeValue("減少學生數", "");
                    elmGrDeptCover.SetAttributeValue("更正學生數", "");
                    elmGrDeptCover.SetAttributeValue("現有學生數", "");
                    elmGrDeptCover.SetAttributeValue("備註說明", "");
                    break;


                case "學籍異動名冊":

                    DateTime? uploadTime = new DateTime();

                    //紀錄 是否有舊的學籍異動名冊資料可以參考
                    hasOldUpdateRecordBatchRecord = false;

                    // 先預設基本值
                    #region 基本值
                    elmGrDeptCover.SetAttributeValue("名冊別", "3");
                    elmGrDeptCover.SetAttributeValue("班別", "");
                    elmGrDeptCover.SetAttributeValue("上傳類別", "");
                    elmGrDeptCover.SetAttributeValue("核定班數", "");
                    elmGrDeptCover.SetAttributeValue("核定學生數", "");
                    elmGrDeptCover.SetAttributeValue("實招班數", "");
                    elmGrDeptCover.SetAttributeValue("實招新生數", "");
                    elmGrDeptCover.SetAttributeValue("原有學生數", "");
                    elmGrDeptCover.SetAttributeValue("增加學生數", "");
                    elmGrDeptCover.SetAttributeValue("減少學生數", "");
                    elmGrDeptCover.SetAttributeValue("更正學生數", "");
                    elmGrDeptCover.SetAttributeValue("現有學生數", "");
                    elmGrDeptCover.SetAttributeValue("註1", "");
                    elmGrDeptCover.SetAttributeValue("備註說明", ""); 
                    #endregion

                    foreach (SHUpdateRecordBatchRecord batch_record in recBatch_list)
                    {
                        System.Xml.XmlElement source;

                        source = (XmlElement)batch_record.Content.SelectSingleNode("異動名冊");

                        string school_code = source.SelectSingleNode("@學校代號").InnerText;
                        string school_year = source.SelectSingleNode("@學年度").InnerText;
                        string school_semester = source.SelectSingleNode("@學期").InnerText;
                        string update_type = source.SelectSingleNode("@類別").InnerText;

                        // 舊的名冊 與上傳名冊同種類、且有上傳過後 審核通過的文號(代表核准)，將會抓取最新的那筆紀錄寫進來                    
                        if (update_type == _GUpdateBatchType && batch_record.ADNumber != "" && batch_record.ADDate > uploadTime)
                        {
                            uploadTime = batch_record.ADDate;

                            // 假如目前新異動名冊要求的學年度 與舊的異動名冊學年度相同，代表同一年級科別對應的對象是同一屆的學生
                            if (schoolYear == school_year)
                            {
                                foreach (XmlNode list in source.SelectNodes("清單"))
                                {
                                    if (gradeYear == list.SelectSingleNode("@年級").InnerText && deptCode == list.SelectSingleNode("@科別代碼").InnerText)
                                    {
                                        foreach (XmlElement st in list.SelectNodes("異動名冊封面"))
                                        {
                                            elmGrDeptCover.SetAttributeValue("名冊別", st.SelectSingleNode("@名冊別").InnerText);
                                            elmGrDeptCover.SetAttributeValue("班別", st.SelectSingleNode("@班別").InnerText);
                                            elmGrDeptCover.SetAttributeValue("上傳類別", st.SelectSingleNode("@上傳類別").InnerText);
                                            elmGrDeptCover.SetAttributeValue("核定班數", st.SelectSingleNode("@核定班數").InnerText);
                                            elmGrDeptCover.SetAttributeValue("核定學生數", st.SelectSingleNode("@核定學生數").InnerText);
                                            elmGrDeptCover.SetAttributeValue("實招班數", st.SelectSingleNode("@實招班數").InnerText);
                                            elmGrDeptCover.SetAttributeValue("實招新生數", st.SelectSingleNode("@實招新生數").InnerText);
                                            elmGrDeptCover.SetAttributeValue("原有學生數", st.SelectSingleNode("@原有學生數").InnerText);
                                            elmGrDeptCover.SetAttributeValue("增加學生數", "");
                                            elmGrDeptCover.SetAttributeValue("減少學生數", "");
                                            elmGrDeptCover.SetAttributeValue("更正學生數", "");
                                            elmGrDeptCover.SetAttributeValue("現有學生數", "");
                                            elmGrDeptCover.SetAttributeValue("註1", st.SelectSingleNode("@註1").InnerText);
                                            elmGrDeptCover.SetAttributeValue("備註說明", st.SelectSingleNode("@備註說明").InnerText);
                                        }
                                        hasOldUpdateRecordBatchRecord = true;
                                    }
                                }
                            }
                        }
                    }

                    // 假如無舊資料可以用 ，代表這次新增是第一筆， 
                    // 可以先嘗試從 新生名冊抓基本資料
                    if (!hasOldUpdateRecordBatchRecord)
                    {
                        uploadTime = new DateTime();

                        foreach (SHUpdateRecordBatchRecord batch_record in recBatch_list)
                        {
                            System.Xml.XmlElement source;

                            source = (XmlElement)batch_record.Content.SelectSingleNode("異動名冊");

                            string school_code = source.SelectSingleNode("@學校代號").InnerText;
                            string school_year = source.SelectSingleNode("@學年度").InnerText;
                            string school_semester = source.SelectSingleNode("@學期").InnerText;
                            string update_type = source.SelectSingleNode("@類別").InnerText;

                            // 舊的名冊 與上傳名冊同種類、且有上傳過後 審核通過的文號(代表核准)，將會抓取最新的那筆紀錄寫進來                    
                            if (update_type == "新生名冊" && batch_record.ADNumber != "" && batch_record.ADDate > uploadTime)
                            {
                                uploadTime = batch_record.ADDate;

                                // 如果要找對照的新生名冊 一年級找 當學年 - (1-1 ) 的學年 的新生名冊 、二年級找 當學年 - (2-1 ) 的學年 的新生名冊 三年級找 當學年 - (3-1 ) 的學年 的新生名冊
                                // ex : 現在學年度107-1 二年級 建立異動名冊時要找新生名冊 ， 要去找106 學年度時的新生名冊資料
                                if (int.Parse(schoolYear) -(int.Parse(gradeYear) -1)   == int.Parse(school_year))
                                {
                                    foreach (XmlNode list in source.SelectNodes("清單"))
                                    {
                                        if (gradeYear == list.SelectSingleNode("@年級").InnerText && deptCode == list.SelectSingleNode("@科別代碼").InnerText)
                                        {
                                            foreach (XmlElement st in list.SelectNodes("異動名冊封面"))
                                            {
                                                elmGrDeptCover.SetAttributeValue("名冊別", "3");
                                                elmGrDeptCover.SetAttributeValue("班別", st.SelectSingleNode("@班別").InnerText);
                                                elmGrDeptCover.SetAttributeValue("上傳類別", st.SelectSingleNode("@上傳類別").InnerText);
                                                elmGrDeptCover.SetAttributeValue("核定班數", st.SelectSingleNode("@核定班數").InnerText);
                                                elmGrDeptCover.SetAttributeValue("核定學生數", st.SelectSingleNode("@核定學生數").InnerText);
                                                elmGrDeptCover.SetAttributeValue("實招班數", st.SelectSingleNode("@實招班數").InnerText);
                                                elmGrDeptCover.SetAttributeValue("實招新生數", st.SelectSingleNode("@實招新生數").InnerText);
                                                elmGrDeptCover.SetAttributeValue("原有學生數", st.SelectSingleNode("@核定學生數").InnerText); // 如果是以前都沒有其他異動， 原有學生數就會等於新生的核定學生數
                                                elmGrDeptCover.SetAttributeValue("增加學生數", "");
                                                elmGrDeptCover.SetAttributeValue("減少學生數", "");
                                                elmGrDeptCover.SetAttributeValue("更正學生數", "");
                                                elmGrDeptCover.SetAttributeValue("現有學生數", "");
                                                elmGrDeptCover.SetAttributeValue("註1", st.SelectSingleNode("@註1").InnerText);
                                                elmGrDeptCover.SetAttributeValue("備註說明", st.SelectSingleNode("@備註說明").InnerText);
                                            }                                            
                                        }
                                    }
                                }
                            }
                        }
                    }


                    break;

                case "畢業名冊":
                    //rptBuild = new GraduatingStudentList();
                    break;

                case "延修生畢業名冊":
                    //rptBuild = new ExtendingStudentGraduateList();
                    elmGrDeptCover.SetAttributeValue("名冊別", "7");
                    elmGrDeptCover.SetAttributeValue("應畢業學年度", "");
                    elmGrDeptCover.SetAttributeValue("班別", "");
                    elmGrDeptCover.SetAttributeValue("上傳類別", "");
                    elmGrDeptCover.SetAttributeValue("輔導延修學生數", "");
                    elmGrDeptCover.SetAttributeValue("未申請延修學生數", "");
                    elmGrDeptCover.SetAttributeValue("原有學生數", "");
                    elmGrDeptCover.SetAttributeValue("現有學生數", "");
                    elmGrDeptCover.SetAttributeValue("畢業學生數", "");
                    elmGrDeptCover.SetAttributeValue("備註說明", "");
                    break;

                case "延修生名冊":
                    elmGrDeptCover.SetAttributeValue("名冊別", "5");
                    elmGrDeptCover.SetAttributeValue("應畢業學年度", "");
                    elmGrDeptCover.SetAttributeValue("班別", "");
                    elmGrDeptCover.SetAttributeValue("上傳類別", "");
                    elmGrDeptCover.SetAttributeValue("輔導延修學生數", "");
                    elmGrDeptCover.SetAttributeValue("未申請延修學生數", "");
                    elmGrDeptCover.SetAttributeValue("原有學生數", "");
                    elmGrDeptCover.SetAttributeValue("增加學生數", "");
                    elmGrDeptCover.SetAttributeValue("現有學生數", "");
                    elmGrDeptCover.SetAttributeValue("備註說明", "");
                    break;

                case "轉入學生名冊":
                    //紀錄 是否有舊的異動名冊資料可以參考
                    hasOldUpdateRecordBatchRecord = false;

                    // 先預設基本值
                    #region 基本值
                    elmGrDeptCover.SetAttributeValue("名冊別", "2");
                    elmGrDeptCover.SetAttributeValue("班別", "");
                    elmGrDeptCover.SetAttributeValue("上傳類別", "");
                    elmGrDeptCover.SetAttributeValue("核定班數", "");
                    elmGrDeptCover.SetAttributeValue("核定學生數", "");
                    elmGrDeptCover.SetAttributeValue("實招班數", "");
                    elmGrDeptCover.SetAttributeValue("實招新生數", "");
                    elmGrDeptCover.SetAttributeValue("原有學生數", "");
                    elmGrDeptCover.SetAttributeValue("轉入學生數", "");
                    elmGrDeptCover.SetAttributeValue("現有學生數", "");
                    elmGrDeptCover.SetAttributeValue("註1", "");
                    elmGrDeptCover.SetAttributeValue("備註說明", "");
                    #endregion

                    // 假如無舊資料可以用 ，代表這次新增是第一筆， 
                    // 可以先嘗試從 新生名冊抓基本資料
                    if (!hasOldUpdateRecordBatchRecord)
                    {
                        uploadTime = new DateTime();

                        foreach (SHUpdateRecordBatchRecord batch_record in recBatch_list)
                        {
                            System.Xml.XmlElement source;

                            source = (XmlElement)batch_record.Content.SelectSingleNode("異動名冊");

                            string school_code = source.SelectSingleNode("@學校代號").InnerText;
                            string school_year = source.SelectSingleNode("@學年度").InnerText;
                            string school_semester = source.SelectSingleNode("@學期").InnerText;
                            string update_type = source.SelectSingleNode("@類別").InnerText;

                            // 舊的名冊 與上傳名冊同種類、且有上傳過後 審核通過的文號(代表核准)，將會抓取最新的那筆紀錄寫進來                    
                            if (update_type == "新生名冊" && batch_record.ADNumber != "" && batch_record.ADDate > uploadTime)
                            {
                                uploadTime = batch_record.ADDate;

                                // 如果要找對照的新生名冊 一年級找 當學年 - (1-1 ) 的學年 的新生名冊 、二年級找 當學年 - (2-1 ) 的學年 的新生名冊 三年級找 當學年 - (3-1 ) 的學年 的新生名冊
                                // ex : 現在學年度107-1 二年級 建立異動名冊時要找新生名冊 ， 要去找106 學年度時的新生名冊資料
                                if (int.Parse(schoolYear) - (int.Parse(gradeYear) - 1) == int.Parse(school_year))
                                {
                                    foreach (XmlNode list in source.SelectNodes("清單"))
                                    {
                                        if (gradeYear == list.SelectSingleNode("@年級").InnerText && deptCode == list.SelectSingleNode("@科別代碼").InnerText)
                                        {
                                            foreach (XmlElement st in list.SelectNodes("異動名冊封面"))
                                            {
                                                elmGrDeptCover.SetAttributeValue("名冊別", "2");
                                                elmGrDeptCover.SetAttributeValue("班別", st.SelectSingleNode("@班別").InnerText);
                                                elmGrDeptCover.SetAttributeValue("上傳類別", st.SelectSingleNode("@上傳類別").InnerText);
                                                elmGrDeptCover.SetAttributeValue("核定班數", st.SelectSingleNode("@核定班數").InnerText);
                                                elmGrDeptCover.SetAttributeValue("核定學生數", st.SelectSingleNode("@核定學生數").InnerText);
                                                elmGrDeptCover.SetAttributeValue("實招班數", st.SelectSingleNode("@實招班數").InnerText);
                                                elmGrDeptCover.SetAttributeValue("實招新生數", st.SelectSingleNode("@實招新生數").InnerText);
                                                elmGrDeptCover.SetAttributeValue("原有學生數", st.SelectSingleNode("@核定學生數").InnerText); // 如果是以前都沒有其他異動， 原有學生數就會等於新生的核定學生數
                                                elmGrDeptCover.SetAttributeValue("轉入學生數", "");
                                                elmGrDeptCover.SetAttributeValue("現有學生數", "");
                                                elmGrDeptCover.SetAttributeValue("註1", "");
                                                elmGrDeptCover.SetAttributeValue("備註說明", "");
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }

                    break;

                // 新生保留錄取資格名冊 、借讀學生名冊 每一次的名冊封面都是獨立的 與前次名冊 無關
                case "新生保留錄取資格名冊":
                    //rptBuild = new RetaintoStudentList();
                    break;

                case "借讀學生名冊":
                    //rptBuild = new TemporaryStudentList();
                    break;
            }






            return elmGrDeptCover;
        }


    }
}

