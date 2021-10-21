using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace UpdateRecordModule_SH_D.BL
{
    //2018/2/5 穎驊 新增異動名冊封面物件

    /// <summary>
    /// 新生保留錄取資格名冊(使用在存取異動名冊)
    /// </summary>
    public class RetaintoStudentListRecCoverRec
    {
        /// <summary>
        /// 年級
        /// </summary>
        public string grYear { get; set; }

        /// <summary>
        /// 科別代碼
        /// </summary>
        public string DeptCode { get; set; }


        /// <summary>
        /// 名冊別
        /// </summary>
        public string ReportType { get; set; }

        /// <summary>
        /// 班別
        /// </summary>
        public string ClassType { get; set; }
        /// <summary>
        /// 上傳類別
        /// </summary>
        public string UpdateType { get; set; }

        /// <summary>
        /// 因病須長期療養或懷孕申請保留學生數
        /// </summary>
        public string LongTermCareStudentCount { get; set; }

        /// <summary>
        /// 因服兵役申請保留學生數
        /// </summary>
        public string MilitaryStudentCount { get; set; }

        /// <summary>
        /// 因病申請保留錄取資格期間復受徵召服役者申請學生數
        /// </summary>
        public string ReCallStudentCount { get; set; }

        /// <summary>
        /// 備註說明
        /// </summary>
        public string RemarksContent { get; set; }

        /// <summary>
        /// 科別
        /// </summary>
        public string Department { get; set; }
    }
}
