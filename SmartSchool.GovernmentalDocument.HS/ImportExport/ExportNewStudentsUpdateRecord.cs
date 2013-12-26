
using SmartSchool.AccessControl;
namespace SmartSchool.GovernmentalDocument.ImportExport
{
    [FeatureCode("Button0200")]
    class ExportNewStudentsUpdateRecord : AbstractExportUpdateRecord
    {

        protected override string[] Fields
        {
            get { return new string[] {"班別","特殊身份代碼", "異動科別", "異動學號", "異動姓名", "身分證號","註1", "性別","生日", "異動代碼", "異動日期", "原因及事項", "入學資格-畢業國中", "入學資格-畢業國中所在地代碼","入學資格-畢業國中年度","入學資格-註2", "核准日期", "核准文號", "備註" }; }
        }

        protected override string Type
        {
            get { return "新生異動"; }
        }
    }
}