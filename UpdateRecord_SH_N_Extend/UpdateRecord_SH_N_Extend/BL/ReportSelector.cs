using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using UpdateRecord_SH_N_Extend.GovernmentalDocument.Reports.List;

namespace UpdateRecord_SH_N_Extend.BL
{
    /// <summary>
    /// 報表選擇
    /// </summary>
    class ReportSelector
    {
     
        /// <summary>
        /// 名冊內容
        /// </summary>
        StudUpdateRecBatchRec _BRec;

        public ReportSelector(StudUpdateRecBatchRec BRec)
        {            
            _BRec = BRec;

            if (_BRec == null)
                return;
        }

        ///// <summary>
        ///// 取得名冊報表
        ///// </summary>
        ///// <returns></returns>
        //public object GetReport()
        //{            
        //    SH_N_All_List shnal = new SH_N_All_List(_BRec);
        //    // 進校
        //    return null;
            
        //}
    }
}
