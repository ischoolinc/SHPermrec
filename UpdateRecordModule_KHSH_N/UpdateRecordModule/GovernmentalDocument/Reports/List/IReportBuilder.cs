﻿using System.ComponentModel;
using System.Xml;

namespace UpdateRecordModule_KHSH_N.GovernmentalDocument.Reports.List
{
    public enum Status { Ready, Busy }
    public interface IReportBuilder
    {
        void BuildReport(XmlElement source,string location);
        event ProgressChangedEventHandler ProgressChanged;
        event RunWorkerCompletedEventHandler Completed;
        Status Status { get; }
        string Description { get;}
        string Version { get;}
        string Copyright { get;}
        string ReportName { get;}
    }
}
