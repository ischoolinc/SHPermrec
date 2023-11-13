using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UpdateRecordModule_SH_D.GovernmentalDocument.Reports.List;

namespace UpdateRecordModule_SH_D.BL
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
                    case "延修生學籍異動名冊":
                        rptBuild = new ExtendingStudentUpdateRecordList();
                        break;

                    case "學籍異動名冊":
                        rptBuild = new StudentUpdateRecordList();
                        break;

                    case "畢業名冊":
                        rptBuild = new GraduatingStudentList();
                        break;

                    case "延修生畢業名冊":
                        rptBuild = new ExtendingStudentGraduateList();
                        break;

                    case "延修生名冊":
                        rptBuild = new ExtendingStudentList();
                        break;

                    case "轉入學生名冊":
                        rptBuild = new TransferringStudentUpdateRecordList();
                        break;

                    case "新生保留錄取資格名冊":
                        rptBuild = new RetaintoStudentList();
                        break;

                    case "借讀學生名冊":
                        rptBuild= new TemporaryStudentList();
                        break;
 
                case "新生名冊_2021版":
                    rptBuild = new EnrollmentList2021();
                    break;
                case "延修生學籍異動名冊_2021版":
                    rptBuild = new ExtendingStudentUpdateRecordList2021();
                    break;

                case "學籍異動名冊_2021版":
                    rptBuild = new StudentUpdateRecordList2021();
                    break;

                case "畢業名冊_2021版":
                    rptBuild = new GraduatingStudentList2021();
                    break;                
                case "延修生畢業名冊_2021版":
                    rptBuild = new ExtendingStudentGraduateList2021();
                    break;

                case "延修生名冊_2021版":
                    rptBuild = new ExtendingStudentList2021();
                    break;

                case "轉入學生名冊_2021版":
                    rptBuild = new TransferringStudentUpdateRecordList2021();
                    break;

                case "新生保留錄取資格名冊_2021版":
                    rptBuild = new RetaintoStudentList2021();
                    break;

                case "借讀學生名冊_2021版":
                    rptBuild = new TemporaryStudentList2021();
                    break;
            }
                return rptBuild;
          
        }
    }
}
