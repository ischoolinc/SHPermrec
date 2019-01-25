using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace UpdateRecordModule_SH_D.BL
{
    //2018/2/5 穎驊 新增異動名冊封面物件

    /// <summary>
    /// 借讀學生名冊(使用在存取異動名冊)
    /// </summary>
    public class TemporaryStudentListRecCoverRec
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
        /// 因災害申請借讀學生數
        /// </summary>
        public string DisasterStudentCount { get; set; }

        /// <summary>
        /// 因適應不良申請借讀學生數
        /// </summary>
        public string MaladapStudentCount { get; set; }

        /// <summary>
        /// 因參加國家代表隊選手培集訓申請借讀學生數
        /// </summary>
        public string PlayerTrainingStudentCount { get; set; }

        /// <summary>
        /// 備註說明
        /// </summary>
        public string RemarksContent { get; set; }

    }
}
