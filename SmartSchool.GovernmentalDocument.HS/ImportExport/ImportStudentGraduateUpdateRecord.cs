
using SmartSchool.AccessControl;
namespace SmartSchool.GovernmentalDocument.ImportExport
{
    [FeatureCode("Button0280")]
    class ImportStudentGraduateUpdateRecord:AbstractImportUpdateRecord
    {
        protected override string[] Fields
        {
            get { return new string[] {"班別", "異動科別", "年級", "異動學號", "異動姓名", "身分證號", "註1", "性別", "生日", "異動代碼", "異動日期", "原因及事項", "最後異動代碼", "畢(結)業證書字號", "備查日期", "備查文號", "核准日期", "核准文號", "備註" }; }
        }

        protected override string Type
        {
            get { return "畢業異動"; }
        }

        protected override string[] InsertRequiredFields
        {
            get { return new string[] { "異動代碼", "異動日期", "年級" }; }
        }
    }
}
