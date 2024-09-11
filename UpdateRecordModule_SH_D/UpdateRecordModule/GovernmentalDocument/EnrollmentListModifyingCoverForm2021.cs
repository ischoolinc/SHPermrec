using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UpdateRecordModule_SH_D.BL;
using System.Xml;
using System.Xml.Linq;
using SHSchool.Data;
using Campus.Windows;

namespace UpdateRecordModule_SH_D.GovernmentalDocument
{
    public partial class EnrollmentListModifyingCoverForm2021 : FISCA.Presentation.Controls.BaseForm
    {

        StudUpdateRecBatchRec _BRec;

        List<EnrollmentListRecCoverRec> coverDataList = new List<EnrollmentListRecCoverRec>();
        List<EnrollmentListRecCoverRec> coverDataListA = new List<EnrollmentListRecCoverRec>();

        //2018/2/5 穎驊新增 提供使用者 可以自行調整 異動名冊封面的資料
        public EnrollmentListModifyingCoverForm2021(StudUpdateRecBatchRec BRec)
        {
            InitializeComponent();

            _BRec = BRec;

            if (_BRec == null)
                return;

            System.Xml.XmlElement source;

            source = (XmlElement)BRec.Content.SelectSingleNode("異動名冊");

            //填資料
            #region 填資料
            string school_code = source.SelectSingleNode("@學校代號").InnerText;
            string school_year = source.SelectSingleNode("@學年度").InnerText;
            string school_semester = source.SelectSingleNode("@學期").InnerText;


            foreach (XmlNode list in source.SelectNodes("清單"))
            {
                List<string> row_data = new List<string>();

                string gradeYear = list.SelectSingleNode("@年級").InnerText;
                string deptCode = list.SelectSingleNode("@科別代碼").InnerText;
                string classType = list.SelectSingleNode("@班別") != null ? list.SelectSingleNode("@班別").InnerText : "";

                ///2021-10 Cyn 因異動資料上只有存科別(沒有代碼) 所以DAL.Transfer KEY是年級+科別(+班別)，再用科別去對照出代碼
                ///但原先儲存封面時， KEY卻是年級+代碼(+班別)，若有同樣代碼是101，科別為「普通科普通班」、「普通科體育班」的情況，儲存會造成資料錯誤
                ///故將科別也填入dgv(隱藏)，將key改為 年級+科別+班別
                string deptName = list.SelectSingleNode("@科別").InnerText;

                foreach (XmlElement st in list.SelectNodes("異動名冊封面"))
                {
                    //string reportType = st.SelectSingleNode("@名冊別").InnerText;
                    string reportType = "1";// 新生名冊 固定為1 
                    // string classType = st.SelectSingleNode("@班別") != null? st.SelectSingleNode("@班別").InnerText :"";
                    string updateType = st.SelectSingleNode("@上傳類別") != null ? st.SelectSingleNode("@上傳類別").InnerText : "";
                    string approvedClassCount = st.SelectSingleNode("@核定班數") != null ? st.SelectSingleNode("@核定班數").InnerText : "";
                    string approvedStudentCount = st.SelectSingleNode("@核定學生數") != null ? st.SelectSingleNode("@核定學生數").InnerText : "";
                    string actualClassCount = st.SelectSingleNode("@實招班數") != null ? st.SelectSingleNode("@實招班數").InnerText : "";
                    string actualStudentCount = st.SelectSingleNode("@實招新生數") != null ? st.SelectSingleNode("@實招新生數").InnerText : "";
                    string ExtraAboCount = st.SelectSingleNode("@外加錄取原住民") != null ? st.SelectSingleNode("@外加錄取原住民").InnerText : "";
                    string ExtraDisabilityCount = st.SelectSingleNode("@外加錄取身心障礙生") != null ? st.SelectSingleNode("@外加錄取身心障礙生").InnerText : "";
                    string ExtraOtherCount = st.SelectSingleNode("@外加錄取其他") != null ? st.SelectSingleNode("@外加錄取其他").InnerText : "";
                    string ActualForeignCount = st.SelectSingleNode("@建教班僑生數") != null ? st.SelectSingleNode("@建教班僑生數").InnerText : "";
                    string remarks1 = st.SelectSingleNode("@註1") != null ? st.SelectSingleNode("@註1").InnerText : "";
                    string remarksContent = st.SelectSingleNode("@備註說明") != null ? st.SelectSingleNode("@備註說明").InnerText : "";

                    //學校代號
                    row_data.Add(school_code);
                    //學年度
                    row_data.Add(school_year);
                    //學期
                    row_data.Add(school_semester);
                    //年級
                    row_data.Add(gradeYear);
                    //名冊別
                    row_data.Add(reportType);
                    //班別
                    row_data.Add(classType);
                    //科別代碼
                    row_data.Add(deptCode);
                    //上傳類別
                    row_data.Add(updateType);
                    //核定班數
                    row_data.Add(approvedClassCount);
                    //核定學生數
                    row_data.Add(approvedStudentCount);
                    //實招班數
                    row_data.Add(actualClassCount);
                    //實招新生數
                    row_data.Add(actualStudentCount);
                    //外加原住民
                    row_data.Add(ExtraAboCount);
                    //外加身心障礙
                    row_data.Add(ExtraDisabilityCount);
                    //外加其他
                    row_data.Add(ExtraOtherCount);
                    //建教班僑生
                    row_data.Add(ActualForeignCount);
                    //註1
                    row_data.Add(remarks1);
                    //備註說明
                    row_data.Add(remarksContent);
                    //科別(隱藏)
                    row_data.Add(deptName);


                    // 轉成array 加入 datagridview
                    string[] row_data_array = row_data.ToArray();

                    dataGridViewX1.Rows.Add(row_data_array);
                }
            }
            #endregion

            List<int> cols = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11,12,13,14,15 };
            DataGridViewImeDecorator dec = new DataGridViewImeDecorator(this.dataGridViewX1, cols);


        }


        //儲存調整結果
        private void btnSave_Click(object sender, EventArgs e)
        {
            //整理UI介面的資料 打包成xml 儲存
            foreach (DataGridViewRow dr in dataGridViewX1.Rows)
            {
                EnrollmentListRecCoverRec coverData = new EnrollmentListRecCoverRec();

                coverData.grYear = "" + dr.Cells["年級"].Value;
                coverData.DeptCode = "" + dr.Cells["科別代碼"].Value;
                coverData.ReportType = "" + dr.Cells["名冊別"].Value;
                coverData.ClassType = "" + dr.Cells["班別"].Value;
                coverData.UpdateType = "" + dr.Cells["上傳類別"].Value;
                coverData.ApprovedClassCount = "" + dr.Cells["核定班數"].Value;
                coverData.ApprovedStudentCount = "" + dr.Cells["核定學生數"].Value;
                coverData.ActualClassCount = "" + dr.Cells["實招班數"].Value;
                coverData.ActualStudentCount = "" + dr.Cells["實招新生數"].Value;
                coverData.ExtraAboCount = "" + dr.Cells["外加原住民"].Value;
                coverData.ExtraDisabilityCount = "" + dr.Cells["外加身心障礙"].Value;
                coverData.ExtraOtherCount = "" + dr.Cells["外加其他"].Value;
                coverData.ActualForeignCount = "" + dr.Cells["建教班僑生數"].Value;
                coverData.Remarks1 = "" + dr.Cells["註1"].Value;
                coverData.RemarksContent = "" + dr.Cells["備註說明"].Value;
                coverData.Department = "" + dr.Cells["科別"].Value;

                coverDataList.Add(coverData);
            }

            // 下面這段是自 DALTransfer.SetStudUpdateRecBatchRec 抄過來
            SHUpdateRecordBatchRecord shurbr = new SHUpdateRecordBatchRecord();
            shurbr.ADDate = _BRec.ADDate;
            shurbr.ADNumber = _BRec.ADNumber;
            Global._GSchoolCode = K12.Data.School.Code;
            Global._GSchoolName = K12.Data.School.ChineseName;
            Global._GUpdateBatchType = _BRec.UpdateType;
            Global._GSchoolYear = _BRec.SchoolYear.ToString();
            Global._GSemester = _BRec.Semester.ToString();
            Global._GDocName = _BRec.Name;
            Global._TempDate = _BRec.TempDate.ToString();
            Global._TempDesc = _BRec.TempDesc;
            Global._TempNumber = _BRec.TempNumber;
            // 將 XElement 轉型 XmlElement
            shurbr.Content = new XmlDocument().ReadNode(ConvertStudUpdateRecDocToXML(_BRec.StudUpdateRecDocList).CreateReader()) as XmlElement;

            shurbr.ID = _BRec.ID;
            shurbr.Name = _BRec.Name;
            shurbr.SchoolYear = _BRec.SchoolYear;
            shurbr.Semester = _BRec.Semester;

            // 在此處永遠只有 update 方法，從來源哪一個異動名冊改的內容就存回去(另外 有 insert 方法可以使用)
            SHUpdateRecordBatch.Update(shurbr);

            FISCA.Presentation.Controls.MsgBox.Show("上傳更動成功!!");

            this.Close();
        }


        //取消
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 將 StudUpdateRecDoc List 轉成 XML (隨同封面資料一起存)
        /// </summary>
        /// <returns></returns>
        public XElement ConvertStudUpdateRecDocToXML(List<BL.StudUpdateRecDoc> updateRecList)
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
                //string key = val.GradeYear + "_" + val.Department + "_";
                string key = val.GradeYear + "_" + val.Department + "_" + val.ClassType;
                if (data.ContainsKey(key))
                    data[key].Add(val);
                else
                {
                    List<BL.StudUpdateRecDoc> xx = new List<UpdateRecordModule_SH_D.BL.StudUpdateRecDoc>();
                    xx.Add(val);
                    data.Add(key, xx);
                }
            }

            XElement retVal = new XElement("異動名冊");
            retVal.SetAttributeValue("學年度", Global._GSchoolYear);
            retVal.SetAttributeValue("學期", Global._GSemester);
            retVal.SetAttributeValue("學校代碼", Global._GSchoolCode);
            retVal.SetAttributeValue("學校代號", Global._GSchoolCode);
            retVal.SetAttributeValue("學校名稱", Global._GSchoolName);
            retVal.SetAttributeValue("類別", Global._GUpdateBatchType);
            coverDataListA.Clear();
            foreach (KeyValuePair<string, List<BL.StudUpdateRecDoc>> val in data)
            {
                // 解析年級科別
                //int idx = val.Key.IndexOf("_");
                //string grYear = val.Key.Substring(0, idx);
                //string DeptName = val.Key.Substring(idx + 1, (val.Key.Length - (idx + 2)));

                //
                string[] strArray = val.Key.Split('_');
                string grYear = strArray[0];
                string DeptName = strArray[1];
                string classType = strArray[2];
                //


                string DeptCode = "";
                if (deptCodeDict.ContainsKey(DeptName))
                    DeptCode = deptCodeDict[DeptName];

                XElement elmGrDept = new XElement("清單");
                elmGrDept.SetAttributeValue("年級", grYear);
                elmGrDept.SetAttributeValue("科別", DeptName);
                elmGrDept.SetAttributeValue("科別代碼", DeptCode);
                elmGrDept.SetAttributeValue("科別代號", DeptCode);
                elmGrDept.SetAttributeValue("班別", classType);

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


                //2018/2/5 穎驊增加 處理封面資料
                
                foreach (EnrollmentListRecCoverRec coverRec in coverDataList)
                {
                    //假如整理好的封面資料 其年級、科別 與 現在的資料相同， 則配對
                    //if (coverRec.grYear == grYear && coverRec.DeptCode == DeptCode)

                    //2021-10 如54行所描述，將key改為 年級+科別(+班別)
                    if (coverRec.grYear == grYear && coverRec.Department == DeptName && coverRec.ClassType == classType)
                    {
                        XElement elmGrDeptCover = new XElement("異動名冊封面");

                        elmGrDeptCover.SetAttributeValue("名冊別", coverRec.ReportType);
                        elmGrDeptCover.SetAttributeValue("上傳類別", coverRec.UpdateType);
                        elmGrDeptCover.SetAttributeValue("核定班數", coverRec.ApprovedClassCount);
                        elmGrDeptCover.SetAttributeValue("核定學生數", coverRec.ApprovedStudentCount);
                        elmGrDeptCover.SetAttributeValue("實招班數", coverRec.ActualClassCount);
                        elmGrDeptCover.SetAttributeValue("實招新生數", coverRec.ActualStudentCount);
                        elmGrDeptCover.SetAttributeValue("外加錄取原住民", coverRec.ExtraAboCount);
                        elmGrDeptCover.SetAttributeValue("外加錄取身心障礙生", coverRec.ExtraDisabilityCount);
                        elmGrDeptCover.SetAttributeValue("外加錄取其他", coverRec.ExtraOtherCount);
                        elmGrDeptCover.SetAttributeValue("建教班僑生數", coverRec.ActualForeignCount);
                        elmGrDeptCover.SetAttributeValue("註1", coverRec.Remarks1);
                        elmGrDeptCover.SetAttributeValue("備註說明", coverRec.RemarksContent);
                        //加入封面
                        elmGrDept.Add(elmGrDeptCover);
                        coverDataListA.Add(coverRec);
                    }
                }

                retVal.Add(elmGrDept);
            }
            //找出沒有異動記錄的封面
            foreach (EnrollmentListRecCoverRec coverRec in coverDataList)
            {
                Boolean find = false;
                foreach (EnrollmentListRecCoverRec coverRecA in coverDataListA)
                    if (coverRecA.Department == coverRec.Department && coverRec.ClassType == coverRecA.ClassType && coverRec.grYear == coverRecA.grYear)
                        find = true;
                if (find == false)
                {
                    XElement elmGrDept = new XElement("清單");
                    elmGrDept.SetAttributeValue("年級", coverRec.grYear);
                    elmGrDept.SetAttributeValue("科別", coverRec.Department);
                    elmGrDept.SetAttributeValue("科別代碼", coverRec.DeptCode);
                    elmGrDept.SetAttributeValue("科別代號", coverRec.DeptCode);
                    elmGrDept.SetAttributeValue("班別", coverRec.ClassType);

                    XElement elmGrDeptCover = new XElement("異動名冊封面");

                    elmGrDeptCover.SetAttributeValue("名冊別", coverRec.ReportType);
                    elmGrDeptCover.SetAttributeValue("上傳類別", coverRec.UpdateType);
                    elmGrDeptCover.SetAttributeValue("核定班數", coverRec.ApprovedClassCount);
                    elmGrDeptCover.SetAttributeValue("核定學生數", coverRec.ApprovedStudentCount);
                    elmGrDeptCover.SetAttributeValue("實招班數", coverRec.ActualClassCount);
                    elmGrDeptCover.SetAttributeValue("實招新生數", coverRec.ActualStudentCount);
                    elmGrDeptCover.SetAttributeValue("外加錄取原住民", coverRec.ExtraAboCount);
                    elmGrDeptCover.SetAttributeValue("外加錄取身心障礙生", coverRec.ExtraDisabilityCount);
                    elmGrDeptCover.SetAttributeValue("外加錄取其他", coverRec.ExtraOtherCount);
                    elmGrDeptCover.SetAttributeValue("建教班僑生數", coverRec.ActualForeignCount);
                    elmGrDeptCover.SetAttributeValue("註1", coverRec.Remarks1);
                    elmGrDeptCover.SetAttributeValue("備註說明", coverRec.RemarksContent);
                    //加入封面
                    elmGrDept.Add(elmGrDeptCover);

                    retVal.Add(elmGrDept);
                }
            }
            return retVal;
        }


    }
}
