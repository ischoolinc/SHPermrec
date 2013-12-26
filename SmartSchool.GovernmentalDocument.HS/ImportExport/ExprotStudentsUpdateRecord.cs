
using SmartSchool.AccessControl;
namespace SmartSchool.GovernmentalDocument.ImportExport
{
    [FeatureCode("Button0200")]
    class ExprotStudentsUpdateRecord:AbstractExportUpdateRecord
    {
        protected override string[] Fields
        {
            get { return new string[] { "班別", "特殊身份代碼", "異動科別", "年級", "異動學號", "異動姓名", "身分證號","註1", "異動種類", "異動代碼", "異動日期", "原因及事項", "新學號", "更正後資料","舊班別","舊科別代碼", "備查日期", "備查文號", "核准日期", "核准文號", "備註" }; }
        }

        protected override string Type
        {
            get { return "學籍異動"; }
        }
    }
}