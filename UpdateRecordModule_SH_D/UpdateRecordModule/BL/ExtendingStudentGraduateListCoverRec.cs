using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace UpdateRecordModule_SH_D.BL
{
    //2018/2/5 穎驊 新增延修生畢業名冊封面物件

    /// <summary>
    /// 延修生名冊類別(使用在存取延修生畢業)
    /// </summary>
    public class ExtendingStudentGraduateListCoverRec
    {
        /// <summary>
        /// 應畢業學年度
        /// </summary>
        public string ScheduledGraduateYear { get; set; }

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
        /// 輔導延修學生數
        /// </summary>
        public string ApprovedExtendingStudentCount { get; set; }

        /// <summary>
        /// 未申請延修學生數
        /// </summary>
        public string WaitingExtendingStudentCount { get; set; }

        /// <summary>
        /// 原有學生數
        /// </summary>
        public string OriginalStudentCount { get; set; }
        
        /// <summary>
        /// 現有學生數
        /// </summary>
        public string CurrentStudentCount { get; set; }

        /// <summary>
        /// 畢業學生數
        /// </summary>
        public string GraduatedStudentCount { get; set; }

        /// <summary>
        /// 備註說明
        /// </summary>
        public string RemarksContent { get; set; }

    }
}
