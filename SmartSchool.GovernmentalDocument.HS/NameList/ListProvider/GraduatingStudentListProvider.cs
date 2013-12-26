﻿using System;
using System.Collections.Generic;
using System.Xml;
using FISCA.DSAUtil;

namespace SmartSchool.GovernmentalDocument.NameList
{
    class GraduatingStudentListProvider : INameListProvider
    {
        #region INameListProvider 成員

        //比對大小
        private static int CompareUpdateRecord(XmlElement x, XmlElement y)
        {
            string gradeyear = x.SelectSingleNode("GradeYear").InnerText;
            string dept = x.SelectSingleNode("Department").InnerText;
            int c = gradeyear.CompareTo(y.SelectSingleNode("GradeYear").InnerText);
            if (c == 0)
            {
                return dept.CompareTo(y.SelectSingleNode("Department").InnerText);
            }
            else
            {
                return c;
            }
        }

        //排序畢業證書字號
        private static int GraduateCertificateNumberComparison(XmlElement a, XmlElement b)
        {
            string a1 = new DSXmlHelper(a).GetText("ContextInfo/ContextInfo/GraduateCertificateNumber");
            string b1= new DSXmlHelper(b).GetText("ContextInfo/ContextInfo/GraduateCertificateNumber");
            return a1.CompareTo(b1);
        }

        //排序科別代碼
        private static int DepartmentCodeComparison(XmlElement a, XmlElement b)
        {
            string sa = new DSXmlHelper(a).GetText("@科別代號");
            string sb = new DSXmlHelper(b).GetText("@科別代號");
            int ia, ib;
            if (int.TryParse(sa, out ia) && int.TryParse(sb, out ib))
                return ia.CompareTo(ib);
            else
                return sa.CompareTo(sb);
        }

        //明國年轉換
        private string CDATE(string p)
        {
            DateTime d = DateTime.Now;
            if (p != "" && DateTime.TryParse(p, out d))
            {
                return "" + (d.Year - 1911) + "/" + d.Month + "/" + d.Day;
            }
            else
                return "";
        }

        // 定義標題
        public string Title
        {
            get { return "畢業名冊"; }
        }

        // 定義異動代號
        private string[] strCodeList = new string[] { "501" };


        public List<System.Xml.XmlElement> GetExpectantList()
        {
            List<XmlElement> list = new List<XmlElement>();
            foreach (XmlElement var in SmartSchool.Feature.QueryStudent.GetUpdateRecordByCode(strCodeList).GetContent().GetElements("UpdateRecord"))
            {
                if (var.SelectSingleNode("GradeYear").InnerText != "延修生")
                {
                    list.Add(var);
                }
            }
            return (list);           
            
        }

        public System.Xml.XmlElement CreateNameList(string schoolYear, string semester, List<System.Xml.XmlElement> list)
        {
            XmlDocument xmlDoc = new XmlDocument();
            Dictionary<string, string> deptCode = new Dictionary<string, string>();
            // 建立科別代碼
            foreach (XmlElement x in SmartSchool.Feature.Basic.Config.GetDepartment().GetContent().GetElements("Department"))
            {
                deptCode.Add(x.SelectSingleNode("Name").InnerText, x.SelectSingleNode("Code").InnerText);
            }

            // 依年級科別將資料排序
            //list.Sort(CompareUpdateRecord);

            //依畢業證書字號排序
            list.Sort(GraduateCertificateNumberComparison);

            // 取得目前異動名冊學生ID
            List<string> StudenIDList = new List<string>();
            foreach (XmlElement xml in list)
            {
                DSXmlHelper helper = new DSXmlHelper(xml);
                if(!string.IsNullOrEmpty(helper.GetText("@RefStudentID")))
                    StudenIDList.Add(helper.GetText("@RefStudentID"));                
            }

            // 取得學生年級
            Dictionary<string, string> StudGradeYearDict = Util.GetStudentGradeYearDict(StudenIDList);

            // 取得學生特殊代碼
            Dictionary<string, string> StudSpaceDict = Util.GetStudSpacCodeDict(StudenIDList);


            // 產生 XML 資料
            Dictionary <string ,Dictionary <string ,XmlElement >> gradeYear_deptMap = new Dictionary<string,Dictionary<string,XmlElement>>() ;

            xmlDoc.LoadXml("<異動名冊 類別=\"畢業名冊\" 學年度=\"" + schoolYear + "\" 學期=\"" + semester + "\" 學校代號=\""+CurrentUser.Instance.SchoolCode+"\" 學校名稱=\""+CurrentUser.Instance.SchoolChineseName+"\"/>");

            foreach (XmlElement x in list)
            {
                DSXmlHelper helper = new DSXmlHelper(x);
                string gradeYear = "_";
                gradeYear = helper.GetText("GradeYear");
                string dept = helper.GetText("Department");

                XmlElement deptGradeNode;
                if (!(gradeYear_deptMap.ContainsKey(gradeYear)))
                {
                    gradeYear_deptMap.Add (gradeYear ,new Dictionary <string ,XmlElement >() );
                }

                if (!(gradeYear_deptMap[gradeYear].ContainsKey(dept )))
                {
                    deptGradeNode=xmlDoc.CreateElement ("清單");
                    deptGradeNode.SetAttribute ("科別",dept);
                    deptGradeNode.SetAttribute ("年級",gradeYear );
                    deptGradeNode.SetAttribute ("科別代號",deptCode.ContainsKey(dept) ? deptCode[dept]:"" );
                    gradeYear_deptMap[gradeYear].Add (dept,deptGradeNode );
                    xmlDoc.DocumentElement.AppendChild (deptGradeNode);

                }
                else
                {
                    deptGradeNode = gradeYear_deptMap[gradeYear][dept];
                }

                // 產生異動細項資料
                XmlElement dataElement = xmlDoc.CreateElement("異動紀錄");

                dataElement.SetAttribute("編號",helper.GetText ("@ID"));
                dataElement.SetAttribute("異動代碼", helper.GetText("UpdateCode"));
                dataElement.SetAttribute("異動日期", CDATE (helper.GetText("UpdateDate")));

                // 畢業證書字號
                dataElement.SetAttribute("畢業證書字號", helper.GetText("ContextInfo/ContextInfo/GraduateCertificateNumber"));

                // 最後異動代號
                dataElement.SetAttribute("最後異動代號", helper.GetText("ContextInfo/ContextInfo/LastUpdateCode"));

                dataElement.SetAttribute("備註", helper.GetText("Comment"));
                dataElement.SetAttribute("備查文號", helper.GetText("LastADNumber"));
                dataElement.SetAttribute("備查日期1", CDATE(helper.GetText("LastADDate")));
                dataElement.SetAttribute("備查日期", helper.GetText("LastADDate"));
                dataElement.SetAttribute("生日1", CDATE(helper.GetText("Birthdate")));
                dataElement.SetAttribute("生日",helper.GetText("Birthdate"));
                dataElement.SetAttribute("身分證號", helper.GetText("IDNumber"));
                dataElement.SetAttribute("身份證號", helper.GetText("IDNumber"));
                dataElement.SetAttribute("性別", helper.GetText("Gender"));

                string StudentID=helper.GetText ("@RefStudentID");

                // 年級
                if (StudGradeYearDict.ContainsKey(StudentID))
                    dataElement.SetAttribute("年級", StudGradeYearDict[StudentID]);

                // 特殊身分代碼
                if (StudSpaceDict.ContainsKey(StudentID))
                    dataElement.SetAttribute("特殊身分代碼", StudSpaceDict[StudentID]);


                // 性別代號 男 1  女 2
                if (helper.GetText("Gender")=="男")
                    dataElement.SetAttribute("性別代號","1");
                else if (helper.GetText("Gender") == "女")
                    dataElement.SetAttribute("性別代號", "2");
                else
                    dataElement.SetAttribute("性別代號", "");

                dataElement.SetAttribute("學號", helper.GetText("StudentNumber"));
                dataElement.SetAttribute("姓名", helper.GetText("Name"));

                #region 2009年新制增加

                dataElement.SetAttribute("註1", helper.GetText("ContextInfo/ContextInfo/IDNumberComment"));
                dataElement.SetAttribute("班別", helper.GetText("ContextInfo/ContextInfo/ClassType"));

                #endregion

                deptGradeNode.AppendChild(dataElement);
            
            }

            #region 排序科別代碼

            List<XmlElement> deptList = new List<XmlElement>();
            foreach (XmlElement var in xmlDoc.DocumentElement.SelectNodes("清單"))
                deptList.Add(var);
            deptList.Sort(DepartmentCodeComparison);

            DSXmlHelper docHelper = new DSXmlHelper(xmlDoc.DocumentElement);
            while (docHelper.PathExist("清單")) docHelper.RemoveElement("清單");

            foreach (XmlElement var in deptList)
                docHelper.AddElement(".", var);

            #endregion

            return xmlDoc.DocumentElement;
        }

        #endregion
    }
}
