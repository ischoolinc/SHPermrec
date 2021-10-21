using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace UpdateRecordModule_SH_D.BL
{
    //2018/3/6 穎驊 新增轉入生名冊封面物件

    /// <summary>
    /// 異動名冊類別(使用在存取異動名冊)
    /// </summary>
    public class TransferStudUpdateRecCoverRec
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
        /// 核定班數
        /// </summary>
        public string ApprovedClassCount { get; set; }
        /// <summary>
        /// 核定學生數
        /// </summary>
        public string ApprovedStudentCount { get; set; }
        /// <summary>
        /// 實招班數
        /// </summary>
        public string ActualClassCount { get; set; }
        /// <summary>
        /// 實招新生數
        /// </summary>
        public string ActualStudentCount { get; set; }

        /// <summary>
        /// 原有學生數
        /// </summary>
        public string OriginalStudentCount { get; set; }

        /// <summary>
        /// 轉入學生數
        /// </summary>
        public string TransferStudentCount { get; set; }

        /// <summary>
        /// 現有學生數
        /// </summary>
        public string CurrentStudentCount { get; set; }

        /// <summary>
        /// 註1
        /// </summary>
        public string Remarks1 { get; set; }

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
