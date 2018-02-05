using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UpdateRecordModule_SH_D.BL;

namespace UpdateRecordModule_SH_D.GovernmentalDocument
{
    public partial class ModifyingCoverForm : FISCA.Presentation.Controls.BaseForm
    {

        StudUpdateRecBatchRec _BRec;

        public ModifyingCoverForm(StudUpdateRecBatchRec BRec)
        {
            InitializeComponent();

            _BRec = BRec;

            if (_BRec == null)
                return;


            switch (_BRec.UpdateType)
            {
                //case "新生名冊":
                //    rptBuild = new EnrollmentList();
                //    break;
                //case "延修生學籍異動名冊":
                //    rptBuild = new ExtendingStudentUpdateRecordList();
                //    break;

                case "學籍異動名冊":
                    dataGridViewX1.Columns.Add("1","1");
                    dataGridViewX1.Columns.Add("2","2");
                    dataGridViewX1.Columns.Add("3","3");
                    dataGridViewX1.Columns.Add("4","4");
                    break;

                //case "畢業名冊":
                //    rptBuild = new GraduatingStudentList();
                //    break;

                //case "延修生畢業名冊":
                //    rptBuild = new ExtendingStudentGraduateList();
                //    break;

                //case "延修生名冊":
                //    rptBuild = new ExtendingStudentList();
                //    break;

                //case "轉入學生名冊":
                //    rptBuild = new TransferringStudentUpdateRecordList();
                //    break;

                //case "新生保留錄取資格名冊":
                //    rptBuild = new RetaintoStudentList();
                //    break;

                //case "借讀學生名冊":
                //    rptBuild = new TemporaryStudentList();
                //    break;
            }

        }









    }
}
