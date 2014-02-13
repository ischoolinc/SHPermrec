using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Presentation;

namespace UpdateRecord_SH_N_Extend
{
    public class Program
    {
        public const string UpdateRecordContentCode = "Content0140";

        [FISCA.MainMethod()]
        public static void Main()
        {
            // 異動
            K12.Presentation.NLDPanels.Student.AddDetailBulider(new FISCA.Presentation.DetailBulider<UpdateRecordItem>());


            // 匯出新生異動
            RibbonBarButton rbEnrollmentListExport = MotherForm.RibbonBarItems["學生", "資料統計"]["匯出"];
            rbEnrollmentListExport["異動相關匯出"]["匯出新生異動(舊進校)"].Enable = FISCA.Permission.UserAcl.Current["Button0200"].Executable;
            rbEnrollmentListExport["異動相關匯出"]["匯出新生異動(舊進校)"].Click += delegate
            {
                SmartSchool.API.PlugIn.Export.Exporter exporter = new Export.ExportEnrollmentListOld();
                ImportExport.ExportStudentV2 wizard = new ImportExport.ExportStudentV2(exporter.Text, exporter.Image);
                exporter.InitializeExport(wizard);
                wizard.ShowDialog();
            };

            // 匯出轉入異動
            RibbonBarButton rbTransferListExport = MotherForm.RibbonBarItems["學生", "資料統計"]["匯出"];
            rbTransferListExport["異動相關匯出"]["匯出轉入異動(舊進校)"].Enable = FISCA.Permission.UserAcl.Current["Button0200"].Executable;
            rbTransferListExport["異動相關匯出"]["匯出轉入異動(舊進校)"].Click += delegate
            {
                SmartSchool.API.PlugIn.Export.Exporter exporter = new Export.ExportTransferListOld();
                ImportExport.ExportStudentV2 wizard = new ImportExport.ExportStudentV2(exporter.Text, exporter.Image);
                exporter.InitializeExport(wizard);
                wizard.ShowDialog();
            };

            // 匯出學籍異動
            RibbonBarButton rbStudUpdateRecordExport = MotherForm.RibbonBarItems["學生", "資料統計"]["匯出"];
            rbStudUpdateRecordExport["異動相關匯出"]["匯出學籍異動(舊進校)"].Enable = FISCA.Permission.UserAcl.Current["Button0200"].Executable;
            rbStudUpdateRecordExport["異動相關匯出"]["匯出學籍異動(舊進校)"].Click += delegate
            {
                SmartSchool.API.PlugIn.Export.Exporter exporter = new Export.ExportStudUpdateRecordListOld();
                ImportExport.ExportStudentV2 wizard = new ImportExport.ExportStudentV2(exporter.Text, exporter.Image);
                exporter.InitializeExport(wizard);
                wizard.ShowDialog();
            };

            // 匯出畢業異動
            RibbonBarButton rbGraduateExport = MotherForm.RibbonBarItems["學生", "資料統計"]["匯出"];
            rbGraduateExport["異動相關匯出"]["匯出畢業異動(舊進校)"].Enable = FISCA.Permission.UserAcl.Current["Button0200"].Executable;
            rbGraduateExport["異動相關匯出"]["匯出畢業異動(舊進校)"].Click += delegate
            {
                SmartSchool.API.PlugIn.Export.Exporter exporter = new Export.ExportGraduateListOld();
                ImportExport.ExportStudentV2 wizard = new ImportExport.ExportStudentV2(exporter.Text, exporter.Image);
                exporter.InitializeExport(wizard);
                wizard.ShowDialog();
            };

        }
    }
}
