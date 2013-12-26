using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using FISCA.DSAUtil;
using FISCA.Presentation;
using SHSchool.Data;
using SmartSchool.Common;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.Data.StudentExtension;

namespace SmartSchool.GovernmentalDocument.Process
{
    public partial class BatchUpdateRecord : UserControl
    {
        FeatureAccessControl batchGraduateCtrl;
        public BatchUpdateRecord()
        {
            //權限判斷 - 學生/批次畢業異動
            //var batchGraduateCtrl = new FeatureAccessControl("Button0095");

            //var btnStudentUR = K12.Presentation.NLDPanels.Student.RibbonBarItems["教務"]["異動作業"];
            //btnStudentUR.Image = Properties.Resources.demographic_reload_64;
            //btnStudentUR.Size = RibbonBarButton.MenuButtonSize.Large;
            //var btnChangeDept = btnStudentUR["轉科"];
            //btnChangeDept.Click += new System.EventHandler(this.btnChangeDept_Click);
            //btnChangeDept.Enable = K12.Presentation.NLDPanels.Student.SelectedSource.Count == 1;
            //var btnChangeInfo = btnStudentUR["學籍更正"];
            //btnChangeInfo.Click += new System.EventHandler(this.btnChangeInfo_Click);
            //btnChangeInfo.Enable = K12.Presentation.NLDPanels.Student.SelectedSource.Count == 1;

            //var btnStudentJR = K12.Presentation.NLDPanels.Student.RibbonBarItems["教務"]["畢業作業"];
            //var btnBatchGraduate = btnStudentJR["批次畢業異動"];
            //btnBatchGraduate.Click += new System.EventHandler(this.btnBatchGraduate_Click);
            //btnBatchGraduate.Enable = batchGraduateCtrl.Executable() && K12.Presentation.NLDPanels.Student.SelectedSource.Count > 0;
            //K12.Presentation.NLDPanels.Student.SelectedSourceChanged += delegate
            //{
            //    btnChangeDept.Enable = K12.Presentation.NLDPanels.Student.SelectedSource.Count == 1;
            //    btnChangeInfo.Enable = K12.Presentation.NLDPanels.Student.SelectedSource.Count == 1;
            //  //  btnBatchGraduate.Enable = batchGraduateCtrl.Executable() && K12.Presentation.NLDPanels.Student.SelectedSource.Count > 0;
            //};
        }

        #region 批次畢業異動
        // 功能修改與搬移，先註解 2012/12/17
        //private void btnBatchGraduate_Click(object sender, EventArgs e)
        //{
        //    MotherForm.SetStatusBarMessage("檢查學生中，請稍候");

        //    const string 畢業代碼 = "501";

        //    AccessHelper accessHelper = new AccessHelper();
        //    List<StudentRecord> students = accessHelper.StudentHelper.GetSelectedStudent();
        //    Dictionary<string, string> studentDiplomaNumber = new Dictionary<string, string>();
        //    Dictionary<string, string> hasUpdateRecordWithoutAD = new Dictionary<string, string>();
        //    List<string> containADStudents = new List<string>();
        //    Dictionary<StudentRecord, string> errorList = new Dictionary<StudentRecord, string>();

        //    accessHelper.StudentHelper.FillField("DiplomaNumber", students);
        //    accessHelper.StudentHelper.FillUpdateRecord(students);

        //    SmartSchool.Customization.Data.SystemInformation.getField("SchoolConfig");
        //    XmlElement config = SmartSchool.Customization.Data.SystemInformation.Fields["SchoolConfig"] as XmlElement;

        //    int total = students.Count;
        //    int current = 0;

        //    #region 檢查學生
        //    // 取得目前畢業或離校學生身分證號
        //    Dictionary<string, string> studIDNumber16 = Utility.GetStudentIDNumber16();
        //    // 取得目前畢業或離校學生身分證號
        //    Dictionary<string, string> studStudNumber16 = Utility.GetStudentNumber16();

        //    List<string> errorList16 = new List<string>();
        //    List<string> studentIDList = new List<string>();
        //    foreach (StudentRecord each_stu in students)
        //    {                
        //        // 1. 檢查畢業或離校身分證號是否重複。
        //        if (studIDNumber16.ContainsKey(each_stu.IDNumber.ToUpper()))
        //        {
        //            string strMsg = "身分證號重複：" + each_stu.IDNumber;
        //            errorList16.Add(strMsg);
        //        }
                
        //        // 2. 檢查畢業或離校學號是否重複。
        //        if (studStudNumber16.ContainsKey(each_stu.StudentNumber.ToUpper()))
        //        {
        //            string strMsg = "學號重複："+each_stu.StudentNumber;
        //            errorList16.Add(strMsg);                
        //        }
        //        studentIDList.Add(each_stu.StudentID);
        //    }
        //    Dictionary<string, string> checkSameLoginName = Utility.GetHasSameLoginName16(studentIDList);

        //    if (checkSameLoginName.Count > 0)
        //    { 
        //        foreach(KeyValuePair<string,string> data in checkSameLoginName)
        //        {
        //            string msg = "登入帳號重複：" + data.Key;
        //            errorList16.Add(msg);
        //        }
        //    }

        //    if (errorList16.Count > 0)
        //    {
        //        FISCA.Presentation.Controls.MsgBox.Show("畢業或離校狀態有重複資料，無法產生畢業動\n" + string.Join(",", errorList16.ToArray()));
        //        return;
        //    }

        //    foreach ( StudentRecord each_stu in students )
        //    {
        //        #region 檢查學生身上的畢業證書字號

        //        XmlElement dnElement = each_stu.Fields["DiplomaNumber"] as XmlElement;
        //        string dnString = "";

        //        if ( dnElement != null && dnElement.SelectSingleNode("DiplomaNumber") != null )
        //            dnString = dnElement.SelectSingleNode("DiplomaNumber").InnerText;

        //        if ( !studentDiplomaNumber.ContainsKey(each_stu.StudentID) )
        //            studentDiplomaNumber.Add(each_stu.StudentID, dnString);

        //        if ( string.IsNullOrEmpty(dnString) )
        //        {
        //            if ( !errorList.ContainsKey(each_stu) )
        //                errorList.Add(each_stu, "缺少畢業證書字號");
        //        }

        //        #endregion

        //        #region 檢查學生是否有任何異動資料 (不應該一筆都沒有)

        //        //if (each_stu.UpdateRecordList.Count <= 0)
        //        //{
        //        //    if (!errorList.ContainsKey(each_stu))
        //        //        errorList.Add(each_stu, "沒有任何異動資料");
        //        //    else
        //        //        errorList[each_stu] += "; 沒有任何異動資料";
        //        //}

        //        #endregion

        //        #region 檢查學生是否具有 '包含核准文號的畢業異動'

        //        foreach ( UpdateRecordInfo info in each_stu.UpdateRecordList )
        //        {
        //            if ( info.UpdateCode == 畢業代碼 )
        //            {
        //                if ( !string.IsNullOrEmpty(info.ADNumber) )
        //                {
        //                    if ( !containADStudents.Contains(each_stu.StudentID) )
        //                        containADStudents.Add(each_stu.StudentID);
        //                    //if (!errorList.ContainsKey(each_stu))
        //                    //    errorList.Add(each_stu, "已有包含核准文號的畢業異動");
        //                    //else
        //                    //    errorList[each_stu] += "; 已有包含核准文號的畢業異動";
        //                }
        //                else
        //                {
        //                    if ( !hasUpdateRecordWithoutAD.ContainsKey(each_stu.StudentID) )
        //                        hasUpdateRecordWithoutAD.Add(each_stu.StudentID, ( info.Detail as XmlElement ).GetAttribute("ID"));
        //                }
        //            }
        //        }

        //        #endregion
        //    }

        //    MotherForm.SetStatusBarMessage("檢查學生完成");

        //    if ( errorList.Count > 0 )
        //    {
        //        ErrorViewer viewer = new ErrorViewer(errorList);
        //        viewer.ShowDialog();
        //        return;
        //    }

        //    #endregion

        //    InitialForm form = new InitialForm();
        //    if ( form.ShowDialog() != DialogResult.OK )
        //        return;

        //    MotherForm.SetStatusBarMessage("畢業異動產生中", 0);

        //    string date = form.Date.ToShortDateString();

        //    int package_size = 25;
        //    int count = 0;

        //    #region 產生 Request，呼叫 Service

        //    DSXmlHelper urInsertHelper = new DSXmlHelper("InsertRequest"); //新增畢業異動 Helper
        //    DSXmlHelper urUpdateHelper = new DSXmlHelper("UpdateRequest"); //修改畢業異動 Helper
        //    DSXmlHelper updateHelper = new DSXmlHelper("UpdateStudentList"); //更新學生資訊 Helper

        //    foreach ( StudentRecord each_stu in students )
        //    {
        //        count++;

        //        //如果學生身上有畢業核准文號，不變更畢業異動。
        //        if ( !containADStudents.Contains(each_stu.StudentID) )
        //        {
        //            #region 取得最後異動資訊

        //            DateTime lastADDate = DateTime.MinValue;
        //            string lastADNumber = "";
        //            string lastUpdateCode = "";

        //            foreach ( UpdateRecordInfo info in each_stu.UpdateRecordList )
        //            {
        //                if ( !string.IsNullOrEmpty(info.ADDate) && info.UpdateCode != 畢業代碼 )
        //                {
        //                    DateTime a;
        //                    if ( DateTime.TryParse(info.ADDate, out a) && a > lastADDate )
        //                    {
        //                        lastADDate = a;
        //                        lastADNumber = info.ADNumber;
        //                        lastUpdateCode = info.UpdateCode;
        //                    }
        //                }
        //            }

        //            #endregion

        //            #region 取得學籍身分代號

        //            List<SHPermrecStatusMappingInfo> MappingInfos = SHPermrecStatusMapping.SelectyByStudentID(each_stu.StudentID);

        //            string comment = "";

        //            if (MappingInfos.Count>0)
        //            {
        //                foreach (SHPermrecStatusMappingInfo MappingInfo in MappingInfos)
        //                {
        //                    if (!string.IsNullOrEmpty(comment)) comment += ",";
        //                    comment += MappingInfo.Code;
        //                }
        //            }
        //            #endregion

        //            #region 產生異動 Request

        //            DSXmlHelper helper = new DSXmlHelper("UpdateRecord");
        //            helper.AddElement("Field");

        //            helper.AddElement("Field", "RefStudentID", each_stu.StudentID);

        //            helper.AddElement("Field", "ContextInfo");
        //            helper.AddElement("Field/ContextInfo", "ContextInfo");
        //            helper.AddElement("Field/ContextInfo/ContextInfo", "LastUpdateCode", lastUpdateCode);
        //            helper.AddElement("Field/ContextInfo/ContextInfo", "NewStudentNumber");
        //            helper.AddElement("Field/ContextInfo/ContextInfo", "PreviousSchool");
        //            helper.AddElement("Field/ContextInfo/ContextInfo", "PreviousStudentNumber");
        //            helper.AddElement("Field/ContextInfo/ContextInfo", "PreviousDepartment");
        //            helper.AddElement("Field/ContextInfo/ContextInfo", "PreviousSchoolLastADDate");
        //            helper.AddElement("Field/ContextInfo/ContextInfo", "PreviousSchoolLastADNumber");
        //            helper.AddElement("Field/ContextInfo/ContextInfo", "PreviousGradeYear");
        //            helper.AddElement("Field/ContextInfo/ContextInfo", "GraduateSchoolLocationCode");
        //            helper.AddElement("Field/ContextInfo/ContextInfo", "GraduateSchool");
        //            helper.AddElement("Field/ContextInfo/ContextInfo", "GraduateCertificateNumber", studentDiplomaNumber[each_stu.StudentID]);

        //            helper.AddElement("Field", "Department", each_stu.Department);
        //            helper.AddElement("Field", "ADDate", "");
        //            helper.AddElement("Field", "ADNumber", "");
        //            helper.AddElement("Field", "UpdateDate", date);
        //            helper.AddElement("Field", "UpdateCode", 畢業代碼);
        //            helper.AddElement("Field", "UpdateDescription", "畢業");
        //            helper.AddElement("Field", "Name", each_stu.StudentName);
        //            helper.AddElement("Field", "StudentNumber", each_stu.StudentNumber);
        //            helper.AddElement("Field", "Gender", each_stu.Gender);
        //            helper.AddElement("Field", "IDNumber", each_stu.IDNumber);
        //            helper.AddElement("Field", "Birthdate", each_stu.Birthday);
        //            helper.AddElement("Field", "GradeYear", ( each_stu.RefClass != null ) ? each_stu.RefClass.GradeYear : "");
        //            helper.AddElement("Field", "LastADDate", ( lastADDate == DateTime.MinValue ) ? "" : lastADDate.ToShortDateString());
        //            helper.AddElement("Field", "LastADNumber", lastADNumber);
        //            helper.AddElement("Field", "Comment", comment);

        //            if ( hasUpdateRecordWithoutAD.ContainsKey(each_stu.StudentID) )
        //            {
        //                helper.AddElement("Condition");
        //                helper.AddElement("Condition", "ID", hasUpdateRecordWithoutAD[each_stu.StudentID]);

        //                urUpdateHelper.AddElement(".", helper.BaseElement);
        //            }
        //            else
        //                urInsertHelper.AddElement(".", helper.BaseElement);

        //            #endregion
        //        }

        //        #region 產生學生更新資訊 Request

        //        DSXmlHelper helper2 = new DSXmlHelper("Student");
        //        helper2.AddElement("Field");
        //        helper2.AddElement("Field", "RefClassID");
        //        helper2.AddElement("Field", "OverrideDeptID");
        //        helper2.AddElement("Field", "Status", "畢業或離校");

        //        string format = "<LeaveInfo SchoolYear='{0}' Reason='{1}' ClassName='{2}' Department='{3}'></LeaveInfo>";

        //        string schoolYear = SmartSchool.Customization.Data.SystemInformation.SchoolYear.ToString();
        //        string className = ( each_stu.RefClass != null ) ? each_stu.RefClass.ClassName : ( ( each_stu.Fields.ContainsKey("LeaveClassName") && ( each_stu.Fields["LeaveClassName"] as string ) != "" ) ? each_stu.Fields["LeaveClassName"] as string : "" );
        //        string dept = each_stu.Department + ( ( each_stu.Fields.ContainsKey("SubDepartment") && ( each_stu.Fields["SubDepartment"] as string ) != "" ) ? ( ":" + each_stu.Fields["SubDepartment"] ) : "" );

        //        string leaveInfo = string.Format(format, schoolYear, "畢業", className, dept);

        //        helper2.AddElement("Field", "LeaveInfo", leaveInfo, true);
        //        helper2.AddElement("Condition");
        //        helper2.AddElement("Condition", "ID", each_stu.StudentID);

        //        updateHelper.AddElement(".", helper2.BaseElement);

        //        #endregion

        //        #region 批次寫入
        //        if ( count % package_size == 0 || count >= students.Count )
        //        {
        //            #region 寫入畢業異動

        //            if ( urInsertHelper.PathExist("UpdateRecord") )
        //                SmartSchool.Feature.EditStudent.InsertUpdateRecord(new DSRequest(urInsertHelper));
        //            if ( urUpdateHelper.PathExist("UpdateRecord") )
        //                SmartSchool.Feature.EditStudent.ModifyUpdateRecord(new DSRequest(urUpdateHelper));

        //            #endregion
        //            #region 寫入離校資訊、脫離班級、變更狀態為"畢業或離校"

        //            SmartSchool.Feature.EditStudent.Update(new DSRequest(updateHelper));

        //            #endregion

        //            urInsertHelper = new DSXmlHelper("InsertRequest");
        //            urUpdateHelper = new DSXmlHelper("UpdateRequest");
        //            updateHelper = new DSXmlHelper("UpdateStudentList");
        //        }
        //        #endregion

        //        MotherForm.SetStatusBarMessage("畢業異動產生中", (int)( ( ++current ) * 100 / total ));
        //    }

        //    #endregion

        //    #region Invoke BriefData Changed

        //    List<string> idlist = new List<string>();
        //    count = 0;
        //    foreach ( StudentRecord each_stu in students )
        //    {
        //        count++;
        //        idlist.Add(each_stu.StudentID);
        //        if ( count % package_size == 0 || count >= students.Count )
        //        {
        //            SmartSchool.Broadcaster.Events.Items["學生/資料變更"].Invoke(idlist.ToArray());
        //            idlist.Clear();
        //        }
        //    }

        //    #endregion

        //    MotherForm.SetStatusBarMessage("畢業異動產生完成");
        //    MsgBox.Show("批次產生畢業異動完成。");
        //}
        #endregion

        //private void btnChangeInfo_Click(object sender, EventArgs e)
        //{
        //    new ProcessWizards.ChangeInfoProcess(new AccessHelper().StudentHelper.GetSelectedStudent()[0].StudentID).ShowDialog();
        //}

        //private void btnChangeDept_Click(object sender, EventArgs e)
        //{
        //    new ProcessWizards.ChangeDeptProcess(new AccessHelper().StudentHelper.GetSelectedStudent()[0].StudentID).ShowDialog();
        //}
    }
}
