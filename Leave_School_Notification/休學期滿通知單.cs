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
    //Form -> BaseForm
    public partial class 休學期滿通知單 : BaseForm
    {
        //範本設定
        private MemoryStream _template1 = null;
        private byte[] _buffer1 = null;
        private MemoryStream _template2 = null;
        private byte[] _buffer2 = null;
        private MemoryStream _template3 = null;
        private byte[] _buffer3 = null;
        private MemoryStream _template4 = null;
        private byte[] _buffer4 = null;
        private MemoryStream _template5 = null;
        private byte[] _buffer5 = null;
        private MemoryStream _template6 = null;
        private byte[] _buffer6 = null;
        private BackgroundWorker _BGWClassStudentMeritDetail; //背景模式  
        private Document _doc1;
        private Document _doc2;
        private Document _doc3;
        private string PrintKind = "";
        private string DeadlineDate = "";
        private string endDate = "";
        private string startDate = "";
        private List<string> lstPrintStu = new List<string>();
        private string SchoolYearStr = "";
        private string SemesterStr = "";
        string ConfigName = "休學期滿通知單";
        private string _useDefaultTemplate1 = "預設範本1";
        private string _useDefaultTemplate2 = "預設範本2";
        private string _useDefaultTemplate3 = "預設範本3";
        private string _useDefaultTemplate4 = "預設範本4";
        private string _useDefaultTemplate5 = "預設範本5";
        private string _useDefaultTemplate6 = "預設範本6";
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
                    return _defaultTemplate = new MemoryStream(Properties.Resources.視為休學通知單_7日未到校);
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
                    return _defaultTemplate = new MemoryStream(Properties.Resources.視為放棄學籍通知單_7日未到校);
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
                    return _defaultTemplate = new MemoryStream(Properties.Resources.臺中高工學生復學通知單_第一次休學);
                }
            }
        }
        public MemoryStream Template4
        {
            get
            {
                MemoryStream _defaultTemplate;

                if (_useDefaultTemplate4 == "自訂範本4")
                {
                    return _template4;
                }
                else //預設範本
                {
                    return _defaultTemplate = new MemoryStream(Properties.Resources.臺中高工學生復學通知單_第二次休學);
                }
            }
        }
        public MemoryStream Template5
        {
            get
            {
                MemoryStream _defaultTemplate;

                if (_useDefaultTemplate5 == "自訂範本5")
                {
                    return _template5;
                }
                else //預設範本
                {
                    return _defaultTemplate = new MemoryStream(Properties.Resources.大宗限時掛號及掛號函件執據);
                }
            }
        }
        public MemoryStream Template6
        {
            get
            {
                MemoryStream _defaultTemplate;

                if (_useDefaultTemplate6 == "自訂範本6")
                {
                    return _template6;
                }
                else //預設範本
                {
                    return _defaultTemplate = new MemoryStream(Properties.Resources.大宗限時掛號及掛號函件存根);
                }
            }
        }
        

        //基本組態檔設定
        ConfigData cd { get; set; }

        public 休學期滿通知單()
        {
            InitializeComponent();
            cboReportKind.Items.Add("休學通知單(七日未到校)");
            cboReportKind.Items.Add("放棄學籍通知單(七日未到校)");
            cboReportKind.Items.Add("復學通知單(第一次休學)");
            cboReportKind.Items.Add("復學通知單(第二次休學)");
            intSchoolYear.Value = int.Parse(K12.Data.School.DefaultSchoolYear);
            intSemester.Value = int.Parse(K12.Data.School.DefaultSemester);
            dateDeadline.Value = DateTime.Today;
            dateEnd.Value = DateTime.Today;
            dateStart.Value = DateTime.Today;
            cboReportKind.Text = "休學通知單(七日未到校)";
            lblEndDate.Visible = true;
            lblStartDate.Visible = true;
            dateEnd.Visible = true;
            dateStart.Visible = true;

            //載入範本設定
            LoadPreference();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "休學期滿通知單_合併欄位總表.docx";
            sfd.Filter = "Word檔案 (*.docx)|*.docx|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new System.IO.FileStream(sfd.FileName, FileMode.Create);
                    fs.Write(Properties.Resources.休學期滿通知單_合併欄位總表, 0, Properties.Resources.休學期滿通知單_合併欄位總表.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    FISCA.Presentation.Controls.MsgBox.Show("指定路徑無法存取。", "另存檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //傳入True是因為不影響程式結構

            ConfigForm configForm = new ConfigForm(_useDefaultTemplate1, _buffer1, _useDefaultTemplate2, _buffer2, _useDefaultTemplate3, _buffer3, _useDefaultTemplate4, _buffer4, _useDefaultTemplate5, _buffer5, _useDefaultTemplate6, _buffer6);

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
                _useDefaultTemplate4 = config.GetAttribute("Default4");
                customize = (XmlElement)config.SelectSingleNode("CustomizeTemplate4");

                if (customize != null)
                {
                    string templateBase64 = customize.InnerText;
                    _buffer4 = Convert.FromBase64String(templateBase64);
                    _template4 = new MemoryStream(_buffer4);
                }
                _useDefaultTemplate5 = config.GetAttribute("Default5");
                customize = (XmlElement)config.SelectSingleNode("CustomizeTemplate5");

                if (customize != null)
                {
                    string templateBase64 = customize.InnerText;
                    _buffer5 = Convert.FromBase64String(templateBase64);
                    _template5 = new MemoryStream(_buffer5);
                }
                _useDefaultTemplate6 = config.GetAttribute("Default6");
                customize = (XmlElement)config.SelectSingleNode("CustomizeTemplate6");

                if (customize != null)
                {
                    string templateBase64 = customize.InnerText;
                    _buffer6 = Convert.FromBase64String(templateBase64);
                    _template6 = new MemoryStream(_buffer6);
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
                //使用範本類型
                config.SetAttribute("Default4", "預設範本4");
                //自訂範本上傳儲存設定
                customize = config.OwnerDocument.CreateElement("CustomizeTemplate4");
                config.AppendChild(customize);
                //使用範本類型
                config.SetAttribute("Default5", "預設範本5");
                //自訂範本上傳儲存設定
                customize = config.OwnerDocument.CreateElement("CustomizeTemplate5");
                config.AppendChild(customize);
                //使用範本類型
                config.SetAttribute("Default6", "預設範本6");
                //自訂範本上傳儲存設定
                customize = config.OwnerDocument.CreateElement("CustomizeTemplate6");
                config.AppendChild(customize);
                cd.SetXml("XmlData", config);
                cd.Save();

                _useDefaultTemplate1 = "預設範本1";
                _useDefaultTemplate2 = "預設範本2";
                _useDefaultTemplate3 = "預設範本3";
                _useDefaultTemplate4 = "預設範本4";
                _useDefaultTemplate5 = "預設範本5";
                _useDefaultTemplate6 = "預設範本6";
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

            if (lstStudent.Items.Count <= 0)
            {
                MessageBox.Show("清單內沒有學生，無法列印");
                return;
            }
            if (dateDeadline.Value.Year == 1)
            {
                MessageBox.Show("沒有設定辦理期限，無法列印");
                return;
            }
            if (cboReportKind.Text == "休學通知單(七日未到校)" && (dateEnd.Value.Year == 1 || dateStart.Value.Year == 1))
            {
                MessageBox.Show("沒有設定休學期間，無法列印");
                return;
            }
            PrintKind = cboReportKind.Text;
            if (dateDeadline.Value != null)
                DeadlineDate = (dateDeadline.Value.Year - 1911) + "年" + dateDeadline.Value.Month + "月" + dateDeadline.Value.Day + "日";

            if (dateEnd.Value != null)
                endDate = (dateEnd.Value.Year - 1911) + "年" + dateEnd.Value.Month + "月" + dateEnd.Value.Day + "日";
            if (dateStart.Value != null)
                startDate = (dateStart.Value.Year - 1911) + "年" + dateStart.Value.Month + "月" + dateStart.Value.Day + "日";
            lstPrintStu.Clear();            
            if (cboReportKind.Text == "休學通知單(七日未到校)" || cboReportKind.Text == "放棄學籍通知單(七日未到校)")
            {
                SchoolYearStr = intSchoolYear.Value.ToString();
                SemesterStr = intSemester.Value.ToString();
            }
            else
            {
                if (intSemester.Value == 1)
                {
                    SchoolYearStr = (intSchoolYear.Value - 1).ToString();
                    SemesterStr = "2";
                }
                else
                {
                    SchoolYearStr = intSchoolYear.Value.ToString();
                    SemesterStr = "1";
                }
            }
            for (int i = 0; i < lstStudent.Items.Count; i++)
            {
                lstPrintStu.Add(lstStudent.Items[i].SubItems[5].Text);               
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
            MemoryStream _template4 = Template4;
            MemoryStream _template5 = Template5;
            MemoryStream _template6 = Template6;            
            switch (PrintKind)
            {
                case "休學通知單(七日未到校)":                   
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
                        _doc2 = new Document(_template5);
                    }
                    catch
                    {
                        MessageBox.Show("開啟樣版檔案出現問題");
                        return;
                    }                    
                    try
                    {
                        _doc3 = new Document(_template6);
                    }
                    catch
                    {
                        MessageBox.Show("開啟樣版檔案出現問題");
                        return;
                    }
                    break;                    
                case "放棄學籍通知單(七日未到校)":                    
                    try
                    {
                        _doc1 = new Document(_template2);
                    }
                    catch
                    {
                        MessageBox.Show("開啟樣版檔案出現問題");
                        return;
                    }                    
                    try
                    {
                        _doc2 = new Document(_template5);
                    }
                    catch
                    {
                        MessageBox.Show("開啟樣版檔案出現問題");
                        return;
                    }                    
                    try
                    {
                        _doc3 = new Document(_template6);
                    }
                    catch
                    {
                        MessageBox.Show("開啟樣版檔案出現問題");
                        return;
                    }                    
                    break;
                case "復學通知單(第一次休學)":
                    try
                    {
                        _doc1 = new Document(_template3);
                    }
                    catch
                    {
                        MessageBox.Show("開啟樣版檔案出現問題");
                        return;
                    }
                    try
                    {
                        _doc2 = new Document(_template5);
                    }
                    catch
                    {
                        MessageBox.Show("開啟樣版檔案出現問題");
                        return;
                    }
                    try
                    {
                        _doc3 = new Document(_template6);
                    }
                    catch
                    {
                        MessageBox.Show("開啟樣版檔案出現問題");
                        return;
                    }
                    break;
                case "復學通知單(第二次休學)":
                    try
                    {
                        _doc1 = new Document(_template4);
                    }
                    catch
                    {
                        MessageBox.Show("開啟樣版檔案出現問題");
                        return;
                    }
                    try
                    {
                        _doc2 = new Document(_template5);
                    }
                    catch
                    {
                        MessageBox.Show("開啟樣版檔案出現問題");
                        return;
                    }
                    try
                    {
                        _doc3 = new Document(_template6);
                    }
                    catch
                    {
                        MessageBox.Show("開啟樣版檔案出現問題");
                        return;
                    }
                    break;                    
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
            table.Columns.Add("系統編號");           
            table.Columns.Add("班級");
            table.Columns.Add("學號");            
            table.Columns.Add("學生姓名");
            table.Columns.Add("辦理期限");
            table.Columns.Add("起始休學日");
            table.Columns.Add("終止休學日");
            table.Columns.Add("監護人姓名");
            table.Columns.Add("郵遞區號");
            table.Columns.Add("住址");
            
            #endregion

            
            FISCA.Presentation.MotherForm.SetStatusBarMessage("產生 "+ PrintKind+"中....");
            string parentsname = "";
            string zip_code = "";
            string address="";
            string studentName = "";
            string ClassName = "";
            string StudentNumber = "";
            string sql = "";
            QueryHelper queryHelper = new QueryHelper();
            DataTable dt = new DataTable();
            foreach (string studentid in lstPrintStu)
            {
                //監護人姓名及住址查詢
                sql= @"SELECT name,student_number ,class_name,custodian_name as parentsname   
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
                foreach (DataRow dr in dt.Rows)
                {
                    parentsname = dr["parentsname"].ToString();
                    zip_code = dr["zip_code"].ToString();
                    address = dr["address"].ToString();
                    ClassName = dr["class_name"].ToString();
                    StudentNumber = dr["student_number"].ToString();
                    studentName = dr["name"].ToString();
                }
                table.Rows.Add(SchoolYearStr, SemesterStr, (DateTime.Today.Year-1911)+"年"+DateTime.Today.Month+"月"+DateTime.Today.Day+"日", School.ChineseName, studentid, ClassName, StudentNumber, studentName, DeadlineDate, startDate, endDate, parentsname, zip_code, address);               
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
                        sd.FileName = cboReportKind.Text+".docx";
                        sd.Filter = "Word檔案 (*.Docx)|*.docx|所有檔案 (*.*)|*.*";
                        //是否列印電子報表
                        if (chkePaper.Checked)
                        {
                            MemoryStream memoryStream = new MemoryStream();
                            ePaperCloud ePaperCloud = new ePaperCloud();
                            string _MessageName = string.Format("您的電子報表已收到最新內容<br>「{0}y辦理期限至 {1}」", cboReportKind.Text,dateDeadline.Value.ToShortDateString());
                            _doc1.Save(memoryStream, Aspose.Words.SaveFormat.Docx);
                            ePaperCloud.upload_ePaper(int.Parse(K12.Data.School.DefaultSchoolYear), int.Parse(K12.Data.School.DefaultSemester),
                                 sd.FileName, cboReportKind.Text, memoryStream, ePaperCloud.ViewerType.Student, ePaperCloud.FormatType.Docx, _MessageName);

                        }
                        else
                        {
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
                        }
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
                else
                {
                    MsgBox.Show("列印發生錯誤:\n" + e.Error.Message);
                }
            }
            else
            {
                MsgBox.Show("報表列印作業已中止");
            }

            FISCA.Presentation.MotherForm.SetStatusBarMessage("產生 " + PrintKind+" 已完成");
            // FISCA.Presentation.MotherForm.SetStatusBarMessage("                                                ");
            btnPrint.Enabled = true;
        }

        private void cboReportKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstStudent.Items.Clear();
            switch (cboReportKind.Text)
            {
                case "休學通知單(七日未到校)":
                    lblHourofWeek.Visible = true;
                    intHourofWeek.Visible = true;
                    lblEndDate.Visible = true;
                    lblStartDate.Visible = true;
                    dateEnd.Visible = true;
                    dateStart.Visible = true;                   
                    lstStudent.Columns[3].Text = "缺課時數";
                    lstStudent.Columns[4].Width = 0;
                    break;
                case "放棄學籍通知單(七日未到校)":
                    lblHourofWeek.Visible = true;
                    intHourofWeek.Visible = true;
                    lblEndDate.Visible = false;
                    lblStartDate.Visible = false;
                    dateEnd.Visible = false;
                    dateStart.Visible = false;
                    lstStudent.Columns[3].Text = "缺課時數";
                    lstStudent.Columns[4].Width = 0;
                    break;
                case "復學通知單(第一次休學)":
                    lblHourofWeek.Visible = false;
                    intHourofWeek.Visible = false;
                    lblEndDate.Visible = false;
                    lblStartDate.Visible = false;
                    dateEnd.Visible = false;
                    dateStart.Visible = false;
                    lstStudent.Columns[3].Text = "休學日期";
                    lstStudent.Columns[4].Width = 150;
                    break;
                case "復學通知單(第二次休學)":
                    lblHourofWeek.Visible = false;
                    intHourofWeek.Visible = false;
                    lblEndDate.Visible = false;
                    lblStartDate.Visible = false;
                    dateEnd.Visible = false;
                    dateStart.Visible = false;
                    lstStudent.Columns[3].Text = "休學日期";
                    lstStudent.Columns[4].Width = 150;
                    break;
            }

        }

        private void btnAutoGet_Click(object sender, EventArgs e)
        {
            switch (cboReportKind.Text)
            {
                case "休學通知單(七日未到校)":
                    GetAbsence();
                    break;
                case "放棄學籍通知單(七日未到校)":
                    GetAbsence();
                    break;
                case "復學通知單(第一次休學)":
                    GetLeaveSchoolList(1);
                    break;
                case "復學通知單(第二次休學)":
                    GetLeaveSchoolList(2);
                    break;
            }
        }
        private void GetAbsence()
        {
            AccessHelper helper = new AccessHelper(); //helper物件用來取得ischool相關資料。
            Dictionary<string, int> DicStudentAbsence = new Dictionary<string, int>();
            foreach (ClassRecord crecord in K12.Data.Class.SelectAll())
            {
                if (crecord.GradeYear >= 1 && crecord.GradeYear <= 3)
                {
                    //循訪每位學生記錄，並建立休學通知單清冊來產生報表(7日曠課)
                    List<AttendanceRecord> lstAttendance = new List<AttendanceRecord>();
                    //取得該班當學期缺曠
                    lstAttendance = K12.Data.Attendance.SelectBySchoolYearAndSemester(crecord.Students, intSchoolYear.Value, intSemester.Value);
                    foreach (AttendanceRecord att in lstAttendance)
                    {
                        foreach (AttendancePeriod w in att.PeriodDetail)
                        {
                            //計算缺曠節次  
                            if (w.Period != "" && (w.AbsenceType.Contains("曠") || w.AbsenceType.Contains("缺")))
                            {
                                if (DicStudentAbsence.ContainsKey(w.RefStudentID))
                                    DicStudentAbsence[w.RefStudentID]++;
                                else
                                    DicStudentAbsence.Add(w.RefStudentID, 1);
                            }
                        }
                    }
                }
            }
            lstStudent.Items.Clear();
            foreach (string UID in DicStudentAbsence.Keys)
            {
                if ((DicStudentAbsence[UID] / intHourofWeek.Value) > 7)
                {
                    ListViewItem lvi = new ListViewItem(Student.SelectByID(UID).Class.Name);                    
                    lvi.SubItems.Add(Student.SelectByID(UID).StudentNumber);
                    lvi.SubItems.Add(Student.SelectByID(UID).Name);
                    lvi.SubItems.Add(DicStudentAbsence[UID].ToString());
                    lvi.SubItems.Add("");
                    lvi.SubItems.Add(UID);
                    lvi.SubItems.Add(intSchoolYear.Value.ToString());
                    lvi.SubItems.Add(intSemester.Value.ToString());
                    lstStudent.Items.Add(lvi);
                }
            }            
        }
        private void GetLeaveSchoolList(int LeaveTime)
        {
            QueryHelper helper =new QueryHelper();
            int upSchoolYear = intSchoolYear.Value;
            int upSemester = intSemester.Value;
            if (upSemester == 1)
            {
                upSchoolYear = upSchoolYear - 1;
                upSemester = 2;
            }
            else
                upSemester = 1;
            List<string> leaveCodes = new List<string>() { "340", "341", "342", "343", "344", "345", "346", "347", "348", "350" };
            string strSql = "select ref_student_id,update_date,update_desc from update_record where  update_code in ('340','341','342','343','344','345','346','347','348','350','370') and school_year= " + upSchoolYear.ToString() + "and semester= " + upSemester.ToString() ;
            DataTable UpRec03List = helper.Select(strSql) ;
            List<SHUpdateRecordRecord> update_rec = new List<SHUpdateRecordRecord>();
                    
            int leavetime = 0;
            List<string> BackSchoolCode=new List<string>() { "221" , "222" , "223" , "224","237","238","239","240","242"};
            List<string> QuitCode = new List<string>() { "361", "367", "368", "369", "371", "374", "375", "376", "377", "378", "379", "380", "381" };
            string LastStatu = "";
            DataTable UpRecList = new DataTable();
            lstStudent.Items.Clear();
            foreach (DataRow dr in UpRec03List.Rows)
            {
                strSql = "select ref_student_id,update_date,update_code,update_desc from update_record where ref_student_id=" + dr["ref_student_id"].ToString() + "   ORDER BY update_date DESC";
                UpRecList = helper.Select(strSql);                
                leavetime = 0;
                foreach (DataRow dt in UpRecList.Rows)
                {
                    if (BackSchoolCode.Contains(dt["update_code"].ToString()) && LastStatu != "")
                        LastStatu = "復學";
                    if (leaveCodes.Contains(dt["update_code"].ToString()) && LastStatu != "")
                        LastStatu = "休學";
                    if (leaveCodes.Contains(dt["update_code"].ToString()))
                        leavetime++;
                    if (QuitCode.Contains(dt["update_code"].ToString()) )
                        LastStatu = "放棄學籍";
                }
                if (cboReportKind.Text == "復學通知單(第一次休學)" && leavetime==1 && LastStatu == "休學")
                {
                    ListViewItem lvi = new ListViewItem(Student.SelectByID(dr["ref_student_id"].ToString()).Class.Name);               
                    lvi.SubItems.Add(Student.SelectByID(dr["ref_student_id"].ToString()).StudentNumber);
                    lvi.SubItems.Add(Student.SelectByID(dr["ref_student_id"].ToString()).Name);
                    lvi.SubItems.Add(dr["update_date"].ToString());
                    lvi.SubItems.Add(dr["update_desc"].ToString());
                    lvi.SubItems.Add(dr["ref_student_id"].ToString());
                    lvi.SubItems.Add(upSchoolYear.ToString());
                    lvi.SubItems.Add(upSemester.ToString());
                    lstStudent.Items.Add(lvi);
                }
                if (cboReportKind.Text == "復學通知單(第二次休學)" && leavetime > 1 && LastStatu == "休學")
                {
                    ListViewItem lvi = new ListViewItem(Student.SelectByID(dr["ref_student_id"].ToString()).Class.Name);
                    lvi.SubItems.Add(Student.SelectByID(dr["ref_student_id"].ToString()).StudentNumber);
                    lvi.SubItems.Add(Student.SelectByID(dr["ref_student_id"].ToString()).Name);
                    lvi.SubItems.Add(dr["update_date"].ToString());
                    lvi.SubItems.Add(dr["update_desc"].ToString());
                    lvi.SubItems.Add(dr["ref_student_id"].ToString());
                    lvi.SubItems.Add(upSchoolYear.ToString());
                    lvi.SubItems.Add(upSemester.ToString());
                    lstStudent.Items.Add(lvi);
                }
            }


        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectStudent StuForm = new SelectStudent();
            StuForm.Owner = this;
            StuForm.ShowDialog();
        }

        private void btnSelAll_Click(object sender, EventArgs e)
        {
            for (int i=0; i<lstStudent.Items.Count;i++)
                {
                lstStudent.Items[i].Checked = true;
            }
        }

        private void btnNotSel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstStudent.Items.Count; i++)
            {
                lstStudent.Items[i].Checked = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ListView newStudentView = new ListView();
            for (int i = 0; i < lstStudent.Items.Count; i++)
            {
                if (lstStudent.Items[i].Checked == false)
                {
                    ListViewItem lvi = new ListViewItem(lstStudent.Items[i].SubItems[0].Text);
                    lvi.SubItems.Add(lstStudent.Items[i].SubItems[1].Text);
                    lvi.SubItems.Add(lstStudent.Items[i].SubItems[2].Text);
                    lvi.SubItems.Add(lstStudent.Items[i].SubItems[3].Text);
                    lvi.SubItems.Add(lstStudent.Items[i].SubItems[4].Text);
                    lvi.SubItems.Add(lstStudent.Items[i].SubItems[5].Text);                   
                    newStudentView.Items.Add(lvi);
                }
            }
            lstStudent.Items.Clear();
            for (int i = 0; i < newStudentView.Items.Count; i++)
            {
                ListViewItem lvi = new ListViewItem(newStudentView.Items[i].SubItems[0].Text);
                lvi.SubItems.Add(newStudentView.Items[i].SubItems[1].Text);
                lvi.SubItems.Add(newStudentView.Items[i].SubItems[2].Text);
                lvi.SubItems.Add(newStudentView.Items[i].SubItems[3].Text);
                lvi.SubItems.Add(newStudentView.Items[i].SubItems[4].Text);
                lvi.SubItems.Add(newStudentView.Items[i].SubItems[5].Text);                
                lstStudent.Items.Add(lvi);
            }
        }

        private void dateStart_ValueChanged(object sender, EventArgs e)
        {
            if (dateStart.Value > dateEnd.Value)
            {
                MessageBox.Show("學期期間開始日期須小於結束日期");
                dateEnd.Value = dateStart.Value;
            }
        }

        private void dateEnd_ValueChanged(object sender, EventArgs e)
        {
            if (dateStart.Value > dateEnd.Value)
            {
                MessageBox.Show("學期期間開始日期須小於結束日期");
                dateEnd.Value = dateStart.Value;
            }
        }
    }
    }
