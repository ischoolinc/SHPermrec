﻿using System.ComponentModel;
using System.Xml;
using SmartSchool.Common;

namespace UpdateRecordModule_SH_D.GovernmentalDocument.Reports.List
{
    public abstract class ReportBuilder:IReportBuilder
    {        

        private BackgroundWorker _backgroundWorker;

        private Status _Status;

        public ReportBuilder()
        {
            _Status = Status.Ready;
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += new DoWorkEventHandler(_backgroundWorker_DoWork);
            _backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(_backgroundWorker_ProgressChanged);
            _backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_backgroundWorker_RunWorkerCompleted);
            _backgroundWorker.WorkerReportsProgress = true;
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _Status = Status.Ready;
            if (Completed != null)
                Completed.Invoke(this, e);
        }

        private void _backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (ProgressChanged != null)
                ProgressChanged.Invoke(this, e);
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            XmlElement source = (XmlElement)(((object[])e.Argument)[0]);
            string location = (string)(((object[])e.Argument)[1]);
            //若為畢業名冊，則依畢業證書字號排序
            if (location.Contains("畢業"))
            {
                //依畢業證書字號
                source = UtilXml.SortByGraduateCertificateNumber(source);
            }
            else
            {
                // 依學號排序
                source = UtilXml.SortByStudentNumber(source);
            }
            Build(source, location);
        }


        #region IReportBuilder 成員
        public Status Status
        {
            get 
            {
                return _Status;    
            }
        }
        public abstract string Description { get;}

        public abstract string Version { get;}

        public abstract string Copyright { get;}

        public abstract string ReportName{get;}

        public void BuildReport(XmlElement source,string location)
        {
            if (_Status == Status.Busy)
            {
                //throw new Exception("報表正在產生中...");
                FISCA.Presentation.Controls.MsgBox.Show("報表正在產生中...");
            }
            else
            {
                _Status = Status.Busy;
                _backgroundWorker.RunWorkerAsync(new object[] { source, location });
            }
        }

        public event ProgressChangedEventHandler ProgressChanged;

        public event RunWorkerCompletedEventHandler Completed;
        #endregion


        protected abstract void Build(XmlElement source, string location);

        protected void ReportProgress(int percentProgress)
        {
            _backgroundWorker.ReportProgress(percentProgress);
        }
    }
}
