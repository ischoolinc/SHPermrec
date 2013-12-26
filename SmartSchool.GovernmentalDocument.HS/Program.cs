using System;
using System.Collections.Generic;
using System.Xml;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Customization.PlugIn.ExtendedContent;
using SmartSchool.Adaatper;
using SmartSchool.ExceptionHandler;
using FISCA.Presentation;
using SmartSchool.StudentRelated.RibbonBars.Export;
using SmartSchool.StudentRelated.RibbonBars.Import;
using SmartSchool.GovernmentalDocument.ImportExport;

namespace SmartSchool.GovernmentalDocument
{
    public class Program
    {
        [MainMethod()]
        [FISCA.MainMethod("高中職日間部學籍異動系統")]
        public static void Main()
        {
            if (System.IO.File.Exists(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "阿寶萬歲萬歲萬萬歲"))) return;

            //毛毛蟲
            List<Customization.PlugIn.ExtendedContent.IContentItem> _items = new List<Customization.PlugIn.ExtendedContent.IContentItem>();

            List<Type> _type_list = new List<Type>(new Type[]{
                //// 異動資料項目(舊)先註
                //typeof(Content.UpdatePalmerwormItem)
            });

            foreach (Type type in _type_list)
            {
                if (!Attribute.IsDefined(type, typeof(SmartSchool.AccessControl.FeatureCodeAttribute)) || CurrentUser.Acl[type].Viewable)
                {
                    try
                    {
                        IContentItem item = type.GetConstructor(Type.EmptyTypes).Invoke(null) as IContentItem;
                        _items.Add(item);
                    }
                    catch (Exception ex) {BugReporter.ReportException(ex, false); }
                }
            }
            foreach (Customization.PlugIn.ExtendedContent.IContentItem var in _items)
            {
                K12.Presentation.NLDPanels.Student.AddDetailBulider(new ContentItemBulider(var));
            }

            //UserControl1 updateRecord = new UserControl1();
            //BaseItem item = c.ProcessRibbon.Items[0];

            //SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<BaseItem>.Instance[@"學生\指定"].Add(item);
            //SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<BaseItem>.Instance.Add(@"學生\學籍作業", updateRecord);

            // 舊功能
            //名冊Ribbon
            //new Process.NameList();

            // 批次畢業異動功能
            new Process.BatchUpdateRecord();

            RibbonBarButton rbItemExport = K12.Presentation.NLDPanels.Student.RibbonBarItems["資料統計"]["匯出"];

            #region 匯出(1000708)

            //rbItemExport["異動相關匯出"]["匯出新生異動"].Enable = CurrentUser.Acl["Button0200"].Executable;
            //rbItemExport["異動相關匯出"]["匯出新生異動"].Click += delegate
            //{
            //    new ExportStudent(new ExportNewStudentsUpdateRecord()).ShowDialog();
            //};

            //rbItemExport["異動相關匯出"]["匯出轉入異動"].Enable = CurrentUser.Acl["Button0200"].Executable;
            //rbItemExport["異動相關匯出"]["匯出轉入異動"].Click += delegate
            //{
            //    new ExportStudent(new ExportTransferSchoolStudentsUpdateRecord()).ShowDialog();
            //};

            //rbItemExport["異動相關匯出"]["匯出學籍異動"].Enable = CurrentUser.Acl["Button0200"].Executable;
            //rbItemExport["異動相關匯出"]["匯出學籍異動"].Click += delegate
            //{
            //    new ExportStudent(new ExprotStudentsUpdateRecord()).ShowDialog();
            //};

            //rbItemExport["異動相關匯出"]["匯出畢業異動"].Enable = CurrentUser.Acl["Button0200"].Executable;
            //rbItemExport["異動相關匯出"]["匯出畢業異動"].Click += delegate
            //{
            //    new ExportStudent(new ExportStudentGraduateUpdateRecord()).ShowDialog();
            //}; 
            #endregion


            RibbonBarButton rbItemImport = K12.Presentation.NLDPanels.Student.RibbonBarItems["資料統計"]["匯入"];

            #region 匯入(1000708)
            //rbItemImport["異動相關匯入"]["匯入新生異動"].Enable = CurrentUser.Acl["Button0280"].Executable;
            //rbItemImport["異動相關匯入"]["匯入新生異動"].Click += delegate
            //{
            //    new ImportStudent(new ImportNewStudentsUpdateRecord()).ShowDialog();
            //};

            //rbItemImport["異動相關匯入"]["匯入轉入異動"].Enable = CurrentUser.Acl["Button0280"].Executable;
            //rbItemImport["異動相關匯入"]["匯入轉入異動"].Click += delegate
            //{
            //    new ImportStudent(new ImportTransferSchoolStudentsUpdateRecord()).ShowDialog();
            //};

            //rbItemImport["異動相關匯入"]["匯入畢業異動"].Enable = CurrentUser.Acl["Button0280"].Executable;
            //rbItemImport["異動相關匯入"]["匯入畢業異動"].Click += delegate
            //{
            //    new ImportStudent(new ImportStudentGraduateUpdateRecord()).ShowDialog();
            //}; 
            #endregion

            //匯出異動紀錄(1000708註解)
            //SmartSchool.Customization.PlugIn.ImportExport.ExportStudent.AddProcess(new ImportExport.ExportNewStudentsUpdateRecord());
            //SmartSchool.Customization.PlugIn.ImportExport.ExportStudent.AddProcess(new ImportExport.ExportTransferSchoolStudentsUpdateRecord());
            //SmartSchool.Customization.PlugIn.ImportExport.ExportStudent.AddProcess(new ImportExport.ExprotStudentsUpdateRecord());
            //SmartSchool.Customization.PlugIn.ImportExport.ExportStudent.AddProcess(new ImportExport.ExportStudentGraduateUpdateRecord());

            //匯入異動紀錄(1000708註解)
            //SmartSchool.Customization.PlugIn.ImportExport.ImportStudent.AddProcess(new ImportExport.ImportNewStudentsUpdateRecord());
            //SmartSchool.Customization.PlugIn.ImportExport.ImportStudent.AddProcess(new ImportExport.ImportTransferSchoolStudentsUpdateRecord());
            //SmartSchool.Customization.PlugIn.ImportExport.ImportStudent.AddProcess(new ImportExport.ImportStudentGraduateUpdateRecord());
            
            //??被註解
            //SmartSchool.Customization.PlugIn.ImportExport.ImportStudent.AddProcess(new ImportExport.ImportStudentsUpdateRecord());
            //SmartSchool.Customization.PlugIn.ImportExport.ImportStudent.AddProcess(new ImportExport.ImportStudentGraduateUpdateRecord());
            SmartSchool.Customization.Data.StudentHelper.FillingUpdateRecord += new EventHandler<SmartSchool.Customization.Data.FillEventArgs<SmartSchool.Customization.Data.StudentRecord>>(StudentHelper_FillingUpdateRecord);
        }

        private const int _PackageLimit = 500;

        private static List<T>[] SplitPackage<T>(List<T> list)
        {
            if (list.Count > 0)
            {
                int packageCount = (list.Count / _PackageLimit + 1);
                int packageSize = list.Count / packageCount + list.Count % packageCount;
                packageCount = 0;
                List<List<T>> packages = new List<List<T>>();
                List<T> packageCurrent = new List<T>();
                foreach (T var in list)
                {
                    packageCurrent.Add(var);
                    packageCount++;
                    if (packageCount == packageSize)
                    {
                        packageCount = 0;
                        packages.Add(packageCurrent);
                    }
                }
                return packages.ToArray();
            }
            else
                return new List<T>[0];
        }

        private static List<T> GetList<T>(IEnumerable<T> items)
        {
            List<T> list = new List<T>();
            list.AddRange(items);
            return list;
        }

        static void StudentHelper_FillingUpdateRecord(object sender, SmartSchool.Customization.Data.FillEventArgs<SmartSchool.Customization.Data.StudentRecord> e)
        {
            //取得代碼對照表
            XmlElement updateCodeMappingElement = SmartSchool.Feature.Basic.Config.GetUpdateCodeSynopsis().GetContent().BaseElement;
            //分批次處理
            foreach (List<SmartSchool.Customization.Data.StudentRecord> studentList in SplitPackage<SmartSchool.Customization.Data.StudentRecord>(GetList<SmartSchool.Customization.Data.StudentRecord>(e.List)))
            {
                Dictionary<string, List<SmartSchool.Customization.Data.StudentExtension.UpdateRecordInfo>> studentUpdateRecords = new Dictionary<string, List<SmartSchool.Customization.Data.StudentExtension.UpdateRecordInfo>>();
                //取得編號
                #region 取得編號
                string[] idList = new string[studentList.Count];
                for (int i = 0; i < idList.Length; i++)
                {
                    idList[i] = studentList[i].StudentID;
                }
                if (idList.Length == 0)
                    continue;
                #endregion
                //抓成績資料
                #region 抓成績資料
                foreach (XmlElement element in SmartSchool.Feature.QueryStudent.GetUpdateRecordByStudentIDList(idList).GetContent().GetElements("UpdateRecord"))
                {
                    string RefStudentID = element.GetAttribute("RefStudentID");
                    if (!studentUpdateRecords.ContainsKey(RefStudentID))
                        studentUpdateRecords.Add(RefStudentID, new List<SmartSchool.Customization.Data.StudentExtension.UpdateRecordInfo>());
                    studentUpdateRecords[RefStudentID].Add(new UpdateRecord(updateCodeMappingElement, element));
                }
                #endregion
                //填入學生的異動資料清單
                #region 填入學生的異動資料清單
                foreach (SmartSchool.Customization.Data.StudentRecord student in studentList)
                {
                    student.UpdateRecordList.Clear();
                    if (studentUpdateRecords.ContainsKey(student.StudentID))
                    {
                        foreach (SmartSchool.Customization.Data.StudentExtension.UpdateRecordInfo updateRecord in studentUpdateRecords[student.StudentID])
                        {
                            student.UpdateRecordList.Add(updateRecord);
                        }
                    }
                }
                #endregion
            }
        }
    }
}
