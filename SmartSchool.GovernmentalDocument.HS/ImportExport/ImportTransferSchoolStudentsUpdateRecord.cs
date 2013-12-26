
using SmartSchool.AccessControl;
namespace SmartSchool.GovernmentalDocument.ImportExport
{
    [FeatureCode("Button0280")]
    class ImportTransferSchoolStudentsUpdateRecord:AbstractImportUpdateRecord
    {
        protected override string[] Fields
        {
            get { return new string[] { "班別", "特殊身份代碼", "異動科別", "年級", "異動學號", "異動姓名", "身分證號", "註1", "性別", "生日", "異動代碼", "異動日期", "原因及事項", "轉入前學生資料-科別", "轉入前學生資料-年級", "轉入前學生資料-學校", "轉入前學生資料-(備查日期)", "轉入前學生資料-(備查文號)", "轉入前學生資料-學號", "核准日期", "核准文號", "備註" }; }
        }

        protected override string Type
        {
            get { return "轉入異動"; }
        }

        protected override string[] InsertRequiredFields
        {
            get { return new string[] { "異動代碼", "異動日期", "年級" }; }
        }
    }
}