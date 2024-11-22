using FISCA.Presentation.Controls;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using K12.Data.Configuration;
using FISCA.Data;
using System.Xml.Linq;
using K12.Data;
using FISCA.UDT;
using Aspose.Words;
using Campus.ePaperCloud;
using SHSchool.Data;
namespace Leave_School_Notification
{
    public partial class 放棄學籍通單 : BaseForm
    {        //範本設定
        private MemoryStream _template1 = null;
        private byte[] _buffer1 = null;
        private MemoryStream _template2 = null;
        private byte[] _buffer2 = null;
        private MemoryStream _template3 = null;
        private byte[] _buffer3 = null;
        
        private BackgroundWorker _BGWClassStudentMeritDetail; //背景模式  
        private Document _doc1;
        private Document _doc2;
        private Document _doc3;
        private string PrintKind = "";
        private string DeadlineDate = "";        
        private List<string> lstPrintStu = new List<string>();
        private string SchoolYearStr = "";
        private string SemesterStr = "";
        string ConfigName = "放棄學籍通知單";
        private string _useDefaultTemplate1 = "預設範本1";
        private string _useDefaultTemplate2 = "預設範本2";
        private string _useDefaultTemplate3 = "預設範本3";
       
        public MemoryStream Template1
        {
            get
            {
                MemoryStream _defaultTemplate;

                if (_useDefaultTemplate1 == "自訂範本1")
                {
                    return _template1;
                }
                else //預設範本
                {
                    return _defaultTemplate = new MemoryStream(Properties.Resources.視為放棄學籍通知單_7日未到校);
                }
            }
        }
        public MemoryStream Template2
        {
            get
            {
                MemoryStream _defaultTemplate;

                if (_useDefaultTemplate2 == "自訂範本2")
                {
                    return _template2;
                }
                else //預設範本
                {
                    return _defaultTemplate = new MemoryStream(Properties.Resources.大宗限時掛號及掛號函件執據);
                }
            }
        }
        public MemoryStream Template3
        {
            get
            {
                MemoryStream _defaultTemplate;

                if (_useDefaultTemplate3 == "自訂範本3")
                {
                    return _template3;
                }
                else //預設範本
                {
                    return _defaultTemplate = new MemoryStream(Properties.Resources.大宗限時掛號及掛號函件存根);
                }
            }
        }


        //基本組態檔設定
        ConfigData cd { get; set; }

        public 放棄學籍通單()
        {
            InitializeComponent();
           
            dateDeadline.Value = DateTime.Today;            

            //載入範本設定
            LoadPreference();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //傳入True是因為不影響程式結構

            ConfigForm_放 configForm = new ConfigForm_放(_useDefaultTemplate1, _buffer1, _useDefaultTemplate2, _buffer2, _useDefaultTemplate3, _buffer3);

            if (configForm.ShowDialog() == DialogResult.OK)
            {
                LoadPreference();
            }
        }
        private void LoadPreference()
        {
            #region 讀取 Preference

            cd = K12.Data.School.Configuration[ConfigName];
            XmlElement config = cd.GetXml("XmlData", null);
            //刪除預設設定檔
            //if (config != null)
            //{
            //    K12.Data.School.Configuration.Remove(cd);
            //    return;
            //}
            if (config != null)
            {
                _useDefaultTemplate1 = config.GetAttribute("Default1");
                XmlElement customize = (XmlElement)config.SelectSingleNode("CustomizeTemplate1");

                if (customize != null)
                {
                    string templateBase64 = customize.InnerText;
                    _buffer1 = Convert.FromBase64String(templateBase64);
                    _template1 = new MemoryStream(_buffer1);
                }
                _useDefaultTemplate2 = config.GetAttribute("Default2");
                customize = (XmlElement)config.SelectSingleNode("CustomizeTemplate2");

                if (customize != null)
                {
                    string templateBase64 = customize.InnerText;
                    _buffer2 = Convert.FromBase64String(templateBase64);
                    _template2 = new MemoryStream(_buffer2);
                }
                _useDefaultTemplate3 = config.GetAttribute("Default3");
                customize = (XmlElement)config.SelectSingleNode("CustomizeTemplate3");

                if (customize != null)
                {
                    string templateBase64 = customize.InnerText;
                    _buffer3 = Convert.FromBase64String(templateBase64);
                    _template3 = new MemoryStream(_buffer3);
                }

            }
            else
            {
                #region 產生空白設定檔
                config = new XmlDocument().CreateElement("休學期滿通知單範本");
                //使用範本類型
                config.SetAttribute("Default1", "預設範本1");
                //自訂範本上傳儲存設定
                XmlElement customize = config.OwnerDocument.CreateElement("CustomizeTemplate1");
                config.AppendChild(customize);
                //使用範本類型
                config.SetAttribute("Default2", "預設範本2");
                //自訂範本上傳儲存設定
                customize = config.OwnerDocument.CreateElement("CustomizeTemplate2");
                config.AppendChild(customize);
                //使用範本類型
                config.SetAttribute("Default3", "預設範本3");
                //自訂範本上傳儲存設定
                customize = config.OwnerDocument.CreateElement("CustomizeTemplate3");
                config.AppendChild(customize);

                cd.SetXml("XmlData", config);
                cd.Save();

                _useDefaultTemplate1 = "預設範本1";
                _useDefaultTemplate2 = "預設範本2";
                _useDefaultTemplate3 = "預設範本3";

                #endregion

            }
            #endregion
        }





        private void btnCalcel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {


            if (dateDeadline.Value.Year == 1)
            {
                MessageBox.Show("沒有設定辦理期限，無法列印");
                return;
            }
            

            if (dateDeadline.Value != null)
                DeadlineDate = (dateDeadline.Value.Year - 1911) + "年" + dateDeadline.Value.Month + "月" + dateDeadline.Value.Day + "日";

            if (K12.Presentation.NLDPanels.Student.SelectedSource.Count < 1)
            {
                MessageBox.Show("沒有選擇學生,無法列印");
                return;
            }

            lstPrintStu.Clear();

            SchoolYearStr = K12.Data.School.DefaultSchoolYear;
            SemesterStr = K12.Data.School.DefaultSemester;
            foreach (string sr in K12.Presentation.NLDPanels.Student.SelectedSource)
            {
                lstPrintStu.Add(sr);
            }
            //ResultMessage.DataSource = null;
            btnPrint.Enabled = false;
            _BGWClassStudentMeritDetail = new BackgroundWorker();
            _BGWClassStudentMeritDetail.DoWork += new DoWorkEventHandler(_BGWClassStudentMeritDetail_DoWork);
            _BGWClassStudentMeritDetail.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BGWClassStudentMeritDetail_RunWorkerCompleted);
            _BGWClassStudentMeritDetail.RunWorkerAsync();
            LoadPreference();



        }
        void _BGWClassStudentMeritDetail_DoWork(object sender, DoWorkEventArgs e)
        {
            //取得設定範本
            MemoryStream _template1 = Template1;
            MemoryStream _template2 = Template2;
            MemoryStream _template3 = Template3;

            try
            {
                _doc1 = new Document(_template1);
            }
            catch
            {
                MessageBox.Show("開啟樣版檔案出現問題");
                return;
            }
            try
            {
                _doc2 = new Document(_template2);
            }
            catch
            {
                MessageBox.Show("開啟樣版檔案出現問題");
                return;
            }
            try
            {
                _doc3 = new Document(_template3);
            }
            catch
            {
                MessageBox.Show("開啟樣版檔案出現問題");
                return;
            }


            // 用 doc.MailMerge.GetFieldNames() Word 合併欄位變數名稱

            // Aspose.Words.DocumentBuilder builder = new Aspose.Words.DocumentBuilder(_doc);

            //建立合併欄位總表
            DataTable table = new DataTable();
            #region 所有的合併欄位
            table.Columns.Add("學年度");
            table.Columns.Add("學期");
            table.Columns.Add("列印日期");
            table.Columns.Add("學校名稱");
            table.Columns.Add("學校地址");
            table.Columns.Add("學校電話");
            table.Columns.Add("系統編號");
            table.Columns.Add("班級");
            table.Columns.Add("學號");
            table.Columns.Add("學生姓名");
            table.Columns.Add("辦理期限");           
            table.Columns.Add("監護人姓名");
            table.Columns.Add("郵遞區號");
            table.Columns.Add("住址");

            #endregion


            FISCA.Presentation.MotherForm.SetStatusBarMessage("產生 " + PrintKind + "中....");
            string parentsname = "";
            string zip_code = "";
            string address = "";
            string studentName = "";
            string ClassName = "";
            string StudentNumber = "";
            string sql = "";
            QueryHelper queryHelper = new QueryHelper();
            DataTable dt = new DataTable();
            foreach (string studentid in lstPrintStu)
            {
                parentsname = "";
                zip_code = "";
                address = "";
                studentName = "";
                ClassName = "";
                StudentNumber = "";
                //監護人姓名及住址查詢
                sql = @"SELECT name,student_number ,class_name,custodian_name as parentsname   
	                ,array_to_string(xpath('//ZipCode/text()', addressEle), '') AS zip_code
                    , array_to_string(xpath('//County/text()', addressEle), '')
                    || array_to_string(xpath('//Town/text()', addressEle), '')
                    || array_to_string(xpath('//DetailAddress/text()', addressEle), '') AS address
                    FROM
                        (
                        SELECT
                            name
                            ,student_number 
                            ,class_name
                            ,custodian_name
                            , student.mailing_address
                            , student.other_addresses
                            , student.permanent_address
                            , unnest(xpath('//AddressList/Address', xmlparse(content student.permanent_address))) as addressEle
                        FROM student  inner join class on ref_class_id=class.id  where student.id = '" + studentid + "') AS StuRec ";
                dt = queryHelper.Select(sql);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        parentsname = dr["parentsname"].ToString();
                        zip_code = dr["zip_code"].ToString();
                        address = dr["address"].ToString();
                        ClassName = dr["class_name"].ToString();
                        StudentNumber = dr["student_number"].ToString();
                        studentName = dr["name"].ToString();
                    }
                }
                else
                {
                    ClassName = K12.Data.Class.SelectByID(K12.Data.Student.SelectByID(studentid).RefClassID).Name;
                    StudentNumber = K12.Data.Student.SelectByID(studentid).StudentNumber;
                    studentName = K12.Data.Student.SelectByID(studentid).Name;

                }
                table.Rows.Add(SchoolYearStr, SemesterStr, (DateTime.Today.Year - 1911) + "年" + DateTime.Today.Month + "月" + DateTime.Today.Day + "日", School.ChineseName, School.Address, School.Telephone, studentid, ClassName, StudentNumber, studentName, DeadlineDate, parentsname, zip_code, address);
                
            }
            if (table.Rows.Count > 0)
            {
                // 執行合併欄位
                _doc1.MailMerge.Execute(table);
                _doc2.MailMerge.Execute(table);
                //_doc2.MailMerge.RemoveEmptyParagraphs = true;
                // 移除沒使用到的合併欄位
                _doc2.MailMerge.DeleteFields();
                _doc3.MailMerge.Execute(table);
                //_doc3.MailMerge.RemoveEmptyParagraphs = true;
                // 移除沒使用到的合併欄位
                _doc3.MailMerge.DeleteFields();
            }
        }
        void _BGWClassStudentMeritDetail_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled != true)
            {

                if (e.Error == null)
                {

                    SaveFileDialog sd = new System.Windows.Forms.SaveFileDialog();
                    sd.Title = "另存新檔";
                    sd.FileName = "視為放棄學籍通知單_7日未到校.docx";
                    sd.Filter = "Word檔案 (*.Docx)|*.docx|所有檔案 (*.*)|*.*";
                    if (sd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            _doc1.Save(sd.FileName);

                            System.Diagnostics.Process.Start(sd.FileName);
                        }
                        catch
                        {
                            FISCA.Presentation.Controls.MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            this.Enabled = true;
                            return;
                        }
                    }
                    //是否列印收執聯
                    if (chkBulkMail_R.Checked)
                    {
                        sd = new System.Windows.Forms.SaveFileDialog();
                        sd.Title = "另存新檔";
                        sd.FileName = "大宗郵件執據.docx";
                        sd.Filter = "Word檔案 (*.Docx)|*.docx|所有檔案 (*.*)|*.*";
                        if (sd.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                _doc2.Save(sd.FileName);

                                System.Diagnostics.Process.Start(sd.FileName);
                            }
                            catch
                            {
                                FISCA.Presentation.Controls.MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                this.Enabled = true;
                                return;
                            }
                        }
                    }
                    //是否列印存根聯
                    if (chkBulkMail_S.Checked)
                    {
                        sd = new System.Windows.Forms.SaveFileDialog();
                        sd.Title = "另存新檔";
                        sd.FileName = "大宗郵件存根.docx";
                        sd.Filter = "Word檔案 (*.Docx)|*.docx|所有檔案 (*.*)|*.*";
                        if (sd.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                _doc3.Save(sd.FileName);

                                System.Diagnostics.Process.Start(sd.FileName);
                            }
                            catch
                            {
                                FISCA.Presentation.Controls.MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                this.Enabled = true;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MsgBox.Show("列印發生錯誤:\n" + e.Error.Message);
                }
            }
            else
            {
                MsgBox.Show("報表列印作業已中止");
            }

            FISCA.Presentation.MotherForm.SetStatusBarMessage("產生 " + PrintKind + " 已完成");
            // FISCA.Presentation.MotherForm.SetStatusBarMessage("                                                ");
            btnPrint.Enabled = true;
        }

    }
}
