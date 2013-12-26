using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using SmartSchool.Common;
using FISCA.DSAUtil;//呵呵
using System.Xml;
using K12.Data;
using Framework.Feature;
using Framework;
//using SmartSchool.Common;
//using JHSchool.Permrec.Legacy;
//using JHSchool.Permrec.Feature.Legacy;
//using Framework.Feature;
using System.Xml.Linq;
using UpdateRecordModule_SH_D.DAL;

namespace UpdateRecordViewForm
{
    public partial class UpdateTypeForm : FISCA.Presentation.Controls.BaseForm
    {
            //private DSXmlHelper _codeHelper;

            /// <summary>
            /// 異動代碼
            /// </summary>
            XElement _UpdateCodeList;

            private List<string> _codeList = new List<string>();
            public List<string> CodeList
            {
                get
                {
                    return _codeList;
                }
            }

            public UpdateTypeForm()
            {
                InitializeComponent();
                InitializeBackgroundWorker();
            }

            #region BackgroundWorker
            private BackgroundWorker _loader;
            private void InitializeBackgroundWorker()
            {
                _loader = new BackgroundWorker();
                _loader.DoWork += new DoWorkEventHandler(_loader_DoWork);
                _loader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_loader_RunWorkerCompleted);
                _loader.RunWorkerAsync();
            }

            private void _loader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            {
                if (e.Error == null)
                {
                    FillGrid();
                    ApplyPreference();
                }
            }

            private void ApplyPreference()
            {
                K12.Data.Configuration.ConfigData cd = K12.Data.School.Configuration["異動資料檢視"];
                
//                ConfigData cd = User.Configuration["異動資料檢視"];
                if (cd.Contains("異動代碼"))
                {
                    XmlElement pref = Framework.XmlHelper.LoadXml(cd["異動代碼"]);
                    foreach (DataGridViewRow row in dgvUpdateCode.Rows)
                    {
                        if (pref.SelectSingleNode(string.Format("Code[.='{0}']", "" + row.Cells[colCode.Index].Value)) != null)
                        {
                            row.Cells[colCheck.Index].Value = true;
                            _codeList.Add("" + row.Cells[colCode.Index].Value);
                        }
                    }
                }
            }

            private void _loader_DoWork(object sender, DoWorkEventArgs e)
            {
                //先能Run
                _UpdateCodeList = DALTransfer.GetUpdateCodeList();
                //_codeHelper = GetUpdateCodeSynopsis().GetContent();
            }
            #endregion

            private void UpdateTypeForm_Load(object sender, EventArgs e)
            {
                //if (_codeHelper == null)
                //    _loader.RunWorkerAsync();
            }

            private void FillGrid()
            {
                dgvUpdateCode.SuspendLayout();
                dgvUpdateCode.Rows.Clear();

                //foreach (XmlElement each in _codeHelper.GetElements("異動"))
                //{
                //    DSXmlHelper urHelper = new DSXmlHelper(each);
                //    DataGridViewRow row = new DataGridViewRow();
                //    row.CreateCells(dgvUpdateCode,
                //        false,
                //        urHelper.GetText("代號"),
                //        urHelper.GetText("原因及事項"),
                //        urHelper.GetText("分類"));
                //    dgvUpdateCode.Rows.Add(row);
                //}

                foreach (XElement elm in _UpdateCodeList.Elements("異動"))
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dgvUpdateCode,
                        false,
                        elm.Element("代號").Value,
                        elm.Element("原因及事項").Value,
                        elm.Element("分類").Value);
                    dgvUpdateCode.Rows.Add(row);
                }

                dgvUpdateCode.ResumeLayout();
            }

            private void btnExit_Click(object sender, EventArgs e)
            {
                this.Close();
            }

            private void btnOK_Click(object sender, EventArgs e)
            {
                _codeList.Clear();

                foreach (DataGridViewRow row in dgvUpdateCode.Rows)
                {
                    if ((bool)row.Cells[colCheck.Index].Value)
                        _codeList.Add("" + row.Cells[colCode.Index].Value);
                }

                this.DialogResult = DialogResult.OK;
            }

            private void checkBox_CheckedChanged(object sender, EventArgs e)
            {
                CheckBox chk = sender as CheckBox;
                dgvUpdateCode.SuspendLayout();
                foreach (DataGridViewRow row in dgvUpdateCode.Rows)
                {
                    if ("" + row.Cells[colType.Index].Value == chk.Text)
                        row.Cells[colCheck.Index].Value = chk.Checked;
                }
                dgvUpdateCode.ResumeLayout();
            }

            public static DSResponse GetUpdateCodeSynopsis()
            {
                string serviceName = "SmartSchool.Config.GetUpdateCodeSynopsis";
                if (DataCacheManager.Get(serviceName) == null)
                {
                    DSRequest request = new DSRequest();
                    DSXmlHelper helper = new DSXmlHelper("GetCountyListRequest");
                    helper.AddElement("Field");
                    helper.AddElement("Field", "異動代號對照表");
                    request.SetContent(helper);
                    DSResponse dsrsp = FISCA.Authentication.DSAServices.CallService("SmartSchool.Config.GetUpdateCodeSynopsis", request);
                    DataCacheManager.Add(serviceName, dsrsp);
                }
                return DataCacheManager.Get(serviceName);
            }
        }

}
