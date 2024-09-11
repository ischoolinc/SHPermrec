using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateRecordModule_SH_D.UDT
{
    [TableName("campus.UpdateRecord.GovApprovedNumOfClass")]
    public class udtGovApprovedNumOfClass : ActiveRecord
    {
        /// <summary>
        /// 報名群名稱
        /// </summary>
        [Field(Field = "SchoolYear", Indexed = false)]
        public int SchoolYear { get; set; }
        /// <summary>
        /// 課程類型
        /// </summary>
        [Field(Field = "DeptGroup", Indexed = false)]
        public string DeptGroup { get; set; }
        /// <summary>
        /// 科別代碼
        /// </summary>
        [Field(Field = "Dept_Code", Indexed = false)]
        public string DeptCode { get; set; }
        /// <summary>
        /// 科別名稱
        /// </summary>
        [Field(Field = "Dept_Name", Indexed = false)]
        public string DeptName { get; set; }
        /// <summary>
        /// 上傳類別
        /// </summary>
        [Field(Field = "Class_TypeU", Indexed = false)]
        public string ClassTypeU { get; set; }
        /// <summary>
        /// 班別
        /// </summary>
        [Field(Field = "Class_Type", Indexed = false)]
        public string ClassType { get; set; }

        /// <summary>
        /// 核定班級數
        /// </summary>
        [Field(Field = "ClassNum", Indexed = false)]
        public int ClassNum { get; set; }
        /// <summary>
        /// 核定班級人數
        /// </summary>

        [Field(Field = "StudentNum", Indexed = false)]
        public int StudentNum { get; set; }
    }
}
