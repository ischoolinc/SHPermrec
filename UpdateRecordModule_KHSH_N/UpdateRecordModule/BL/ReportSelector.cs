using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UpdateRecordModule_KHSH_N.GovernmentalDocument.Reports.List;

namespace UpdateRecordModule_KHSH_N.BL
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

        /// <summary>
        /// 取得名冊報表
        /// </summary>
        /// <returns></returns>
        public object GetReport()
        {
            IReportBuilder rptBuild = null;
            switch (_BRec.UpdateType)
            {
                case "新生名冊":
                    rptBuild = new EnrollmentList();
                    break;
                case "畢業名冊":
                    rptBuild = new GraduateList();
                    break;

                case "轉學生入學名冊":
                    rptBuild = new TransferImport1List();
                    break;

                case "復學生名冊":
                    rptBuild = new TransferImport2List();
                    break;

                case "轉出學生名冊":
                    rptBuild = new TransferExportList();
                    break;

                case "延修學生名冊":
                    rptBuild = new ExtendingStudentList();
                    break;
            }
            return rptBuild;        
        }
    }
}
