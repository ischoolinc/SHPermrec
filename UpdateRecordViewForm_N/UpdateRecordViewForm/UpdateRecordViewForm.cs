using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using FISCA.DSAUtil;
using Framework;
//using JHSchool.Permrec.Feature.Legacy;
//using SmartSchool.Common;
using SHSchool.Data;
using FISCA.Presentation.Controls;



namespace UpdateRecordViewForm
{
    public partial class UpdateRecordViewForm : FISCA.Presentation.Controls.BaseForm
    {
        private BackgroundWorker _loader;
        private List<string> _studentsList;
        private List<string> _setStudent;
        private List<SHUpdateRecordRecord> _StudUpdateRecList;
        private UpdateTypeForm _typeForm;


        //Cache學生集合
        private Dictionary<string, SHStudentRecord> students;

        public UpdateRecordViewForm()
        {
            InitializeComponent();
            _typeForm = new UpdateTypeForm();
            students = new Dictionary<string, SHStudentRecord>();
            _StudUpdateRecList = new List<SHUpdateRecordRecord>();
            
        }

        private void Initialize()
        {
            // 設定日期預設值今天
            dtEnd.Value = dtStart.Value = DateTime.Now;
            labelX4.Text = "";
            _loader = new BackgroundWorker();            
            _loader.DoWork += new DoWorkEventHandler(_loader_DoWork);
            _loader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_loader_RunWorkerCompleted);            
            
        }

        private void _loader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            FillDataGridView();
            labelX4.Text = "";
        }

        private void _loader_DoWork(object sender, DoWorkEventArgs e)
        {
            _StudUpdateRecList.Clear();            

            // 讀取學生資料
            if(students.Count ==0)
            foreach (SHStudentRecord var in SHStudent.SelectAll())
                students.Add(var.ID, var);
             
            // 讀取異動資料
            foreach (SHUpdateRecordRecord UpdateRec in SHUpdateRecord.SelectAll())
            {
                DateTime UpdateDate;

                if (DateTime.TryParse(UpdateRec.UpdateDate, out UpdateDate))
                {       
                    if (UpdateDate.Date >= dtStart.Value.Date && UpdateDate.Date <= dtEnd.Value.Date)
                        if (_typeForm.CodeList.Contains(UpdateRec.UpdateCode))
                            _StudUpdateRecList.Add(UpdateRec);
                }
            }
        }

        private void FillDataGridView()
        {
            try
            {                
                dataGridViewX1.SuspendLayout();
                foreach (SHUpdateRecordRecord UpdateRec in _StudUpdateRecList)
                {
                    SHStudentRecord student = new SHStudentRecord();

                    if (students.ContainsKey(UpdateRec.StudentID))
                        student = students[UpdateRec.StudentID];

                    string SeatNo = string.Empty, ClassName = string.Empty, Gender = string.Empty;

                    if (student.SeatNo.HasValue)
                        SeatNo = student.SeatNo.Value + "";

                    if (student.Class != null)
                        ClassName = student.Class.Name;

                    if (!string.IsNullOrEmpty(student.Gender))
                        Gender = student.Gender;

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridViewX1,
                        UpdateRec.ID,
                        UpdateRec.UpdateDate,
                        ClassName,
                        SeatNo,
                        student.StudentNumber,
                        student.Name,
                        Gender,
                        UpdateRec.UpdateCode,
                        UpdateRec.UpdateDescription,
                        "",
                        "",
                        UpdateRec.ADNumber
                    );
                    row.Tag = UpdateRec.StudentID;
                    dataGridViewX1.Rows.Add(row);             
                }
                dataGridViewX1.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }
        }

        private bool IsDateTime(string date)
        {
            DateTime try_value;
            if (DateTime.TryParse(date, out try_value))
                return true;
            return false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (_typeForm.CodeList.Count == 0)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請先設定異動代碼..");
                return;
            }
            labelX4.Text = "資料讀取中..";

            bool valid = true;
            errorProvider1.Clear();

            if (valid && !_loader.IsBusy)
            {
                dataGridViewX1.Rows.Clear();
                _loader.RunWorkerAsync();
            }
        }

        private void UpdateRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            XmlElement pref = new XmlDocument().CreateElement("異動資料檢視_異動代碼");
            foreach (string code in _typeForm.CodeList)
            {
                XmlElement codeElement = pref.OwnerDocument.CreateElement("Code");
                codeElement.InnerText = code;
                pref.AppendChild(codeElement);
            }

            //SmartSchool.Customization.Data.SystemInformation.Preference["異動資料檢視_異動代碼"] = pref;


            K12.Data.Configuration.ConfigData cd = K12.Data.School.Configuration["異動資料檢視"];
            cd["異動代碼"] = pref.OuterXml;
            cd.Save();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            DataGridViewExport export = new DataGridViewExport(dataGridViewX1);
            export.Save(saveFileDialog1.FileName);

            if (new CompleteForm().ShowDialog() == DialogResult.Yes)
                System.Diagnostics.Process.Start(saveFileDialog1.FileName);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (_studentsList == null)
                _studentsList = new List<string>();


            _setStudent = GetSelectedStudentList();
            K12.Presentation.NLDPanels.Student.AddToTemp(_setStudent);

            //將增加的學生記錄下來
            foreach (string var in _setStudent)
            {
                if (!_studentsList.Contains(var))
                    _studentsList.Add(var);
            }
            FISCA.Presentation.Controls.MsgBox.Show("新增 " + _setStudent.Count + " 名學生於待處理");
            labelX4.Text = "待處理共 " + K12.Presentation.NLDPanels.Student.TempSource.Count + " 名學生";
            labelX4.Visible = true;
            btnClear.Visible = true;
        }

        private List<string> GetSelectedStudentList()
        {
            List<string> _temporallist = new List<string>();
            foreach (DataGridViewRow var in dataGridViewX1.SelectedRows)
            {
                if (!_temporallist.Contains((string)var.Tag))
                {
                    _temporallist.Add("" + var.Tag);
                }
            }
            return _temporallist;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            K12.Presentation.NLDPanels.Student.RemoveFromTemp(_studentsList);
            FISCA.Presentation.Controls.MsgBox.Show("已清除所有加入項目");
            labelX4.Visible = false;
            btnClear.Visible = false;
        }
        
        private void btnSetUpCode_Click(object sender, EventArgs e)
        {            
            _typeForm.ShowDialog();
        }

        private void UpdateRecordViewForm_Load(object sender, EventArgs e)
        {
            Initialize();
        }
    }
}
