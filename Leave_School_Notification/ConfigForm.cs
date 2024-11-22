using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Rendering;
using FISCA.Presentation.Controls;
using K12.Data.Configuration;
using Aspose.Words;
namespace Leave_School_Notification
{
    public partial class ConfigForm : BaseForm
    {

        private string _defaultTemplate1; //預設範本1 + 預設範本2 + 自訂範本
        private byte[] _buffer1 = null;

        private string base64_1 = null;

        private bool _isUpload1 = false;
        private string _defaultTemplate2; //預設範本1 + 預設範本2 + 自訂範本
        private byte[] _buffer2 = null;
        private string base64_2 = null;

        private bool _isUpload2 = false;
        private string _defaultTemplate3; //預設範本1 + 預設範本2 + 自訂範本
        private byte[] _buffer3 = null;
        private string base64_3 = null;

        private bool _isUpload3 = false;
        private string _defaultTemplate4; //預設範本1 + 預設範本2 + 自訂範本
        private byte[] _buffer4 = null;
        private string base64_4 = null;

        private bool _isUpload4 = false;
        private string _defaultTemplate5; //預設範本1 + 預設範本2 + 自訂範本
        private byte[] _buffer5 = null;
        private string base64_5 = null;

        private bool _isUpload5 = false;
        private string _defaultTemplate6; //預設範本1 + 預設範本2 + 自訂範本
        private byte[] _buffer6 = null;
        private string base64_6 = null;

        private bool _isUpload6 = false;

        string ConfigName = "休學期滿通知單";


        public ConfigForm(string defaultTemplate1, byte[] buffer1, string defaultTemplate2, byte[] buffer2, string defaultTemplate3, byte[] buffer3, string defaultTemplate4, byte[] buffer4, string defaultTemplate5, byte[] buffer5, string defaultTemplate6, byte[] buffer6)
        {
            InitializeComponent();

            //如果系統的Renderer是Office2007Renderer
            //同化_ClassTeacherView,_CategoryView的顏色
            if (GlobalManager.Renderer is Office2007Renderer)
            {
                ((Office2007Renderer)GlobalManager.Renderer).ColorTableChanged += new EventHandler(ScoreCalcRuleEditor_ColorTableChanged);
                SetForeColor(this);
            }
            if (buffer1 != null)
                _buffer1 = buffer1;

            _defaultTemplate1 = defaultTemplate1;

            if (defaultTemplate1 == "自訂範本1") //如果是自訂
                rbUDEF_1.Checked = true;
            else
                rbDEF_1.Checked = true; //如果都不是就進入預設1
            if (buffer2 != null)
                _buffer2 = buffer2;

            _defaultTemplate2 = defaultTemplate2;

            if (defaultTemplate2 == "自訂範本2") //如果是自訂
                rbUDEF_2.Checked = true;
            else
                rbDEF_2.Checked = true; //如果都不是就進入預設1
            if (buffer3 != null)
                _buffer3 = buffer3;

            _defaultTemplate3 = defaultTemplate3;

            if (defaultTemplate3 == "自訂範本3") //如果是自訂
                rbUDEF_3.Checked = true;
            else
                rbDEF_3.Checked = true; //如果都不是就進入預設1
            if (buffer4 != null)
                _buffer4 = buffer4;

            _defaultTemplate4 = defaultTemplate4;

            if (defaultTemplate4 == "自訂範本4") //如果是自訂
                rbUDEF_4.Checked = true;
            else
                rbDEF_4.Checked = true; //如果都不是就進入預設1
            if (buffer5 != null)
                _buffer5 = buffer5;

            _defaultTemplate5 = defaultTemplate5;

            if (defaultTemplate5 == "自訂範本5") //如果是自訂
                rbUDEF_5.Checked = true;
            else
                rbDEF_5.Checked = true; //如果都不是就進入預設1
            if (buffer6 != null)
                _buffer6 = buffer5;

            _defaultTemplate6 = defaultTemplate6;

            if (defaultTemplate6 == "自訂範本6") //如果是自訂
                rbUDEF_6.Checked = true;
            else
                rbDEF_6.Checked = true; //如果都不是就進入預設1


        }

        void ScoreCalcRuleEditor_ColorTableChanged(object sender, EventArgs e)
        {
            SetForeColor(this);
        }

        private void SetForeColor(Control parent)
        {
            foreach (Control var in parent.Controls)
            {
                if (var is RadioButton)
                    var.ForeColor = ((Office2007Renderer)GlobalManager.Renderer).ColorTable.CheckBoxItem.Default.Text;
                SetForeColor(var);
            }
        }

        private void rbDEF_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDEF_1.Checked)
            {
                //radioButton2.Checked = false;
                _defaultTemplate1 = "預設範本1";
            }
        }



        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUDEF_1.Checked)
            {
                //radioButton1.Checked = false;
                _defaultTemplate1 = "自訂範本";
            }
        }


        private void linkDef1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "休學通知單_範本.docx";
            sfd.Filter = "Word檔案 (*.docx)|*.docx|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    fs.Write(Properties.Resources.視為休學通知單_7日未到校, 0, Properties.Resources.視為休學通知單_7日未到校.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "另存檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }


        private void buttonX1_Click(object sender, EventArgs e)
        {
            #region 儲存 Preference

            ConfigData cd = K12.Data.School.Configuration[ConfigName];
            XmlElement config = cd.GetXml("XmlData", null);

            //XmlElement config = CurrentUser.Instance.Preference["休學期滿通知單"];

            if (config == null)
            {
                config = new XmlDocument().CreateElement("休學期滿通知單範本");
            }
            //範本選擇設定
            config.SetAttribute("Default1", _defaultTemplate1);
            config.SetAttribute("Default2", _defaultTemplate2);
            config.SetAttribute("Default3", _defaultTemplate3);
            config.SetAttribute("Default4", _defaultTemplate4);
            config.SetAttribute("Default5", _defaultTemplate5);
            config.SetAttribute("Default6", _defaultTemplate6);

            XmlElement customize = config.OwnerDocument.CreateElement("CustomizeTemplate1");
            // XmlElement Retakefree = config.OwnerDocument.CreateElement("Retakefree");
            if (config.SelectSingleNode("customize") == null)
                config.AppendChild(customize);
            //上傳範本
            if (_isUpload1) //如果是自訂範本
            {
                customize.InnerText = base64_1;
                config.ReplaceChild(customize, config.SelectSingleNode("CustomizeTemplate1"));
            }
            customize = config.OwnerDocument.CreateElement("CustomizeTemplate2");
            // XmlElement Retakefree = config.OwnerDocument.CreateElement("Retakefree");
            if (config.SelectSingleNode("customize") == null)
                config.AppendChild(customize);
            //上傳範本
            if (_isUpload2) //如果是自訂範本
            {
                customize.InnerText = base64_2;
                config.ReplaceChild(customize, config.SelectSingleNode("CustomizeTemplate2"));
            }
            customize = config.OwnerDocument.CreateElement("CustomizeTemplate3");
            // XmlElement Retakefree = config.OwnerDocument.CreateElement("Retakefree");
            if (config.SelectSingleNode("customize") == null)
                config.AppendChild(customize);
            //上傳範本
            if (_isUpload3) //如果是自訂範本
            {
                customize.InnerText = base64_3;
                config.ReplaceChild(customize, config.SelectSingleNode("CustomizeTemplate3"));
            }
            customize = config.OwnerDocument.CreateElement("CustomizeTemplate4");
            // XmlElement Retakefree = config.OwnerDocument.CreateElement("Retakefree");
            if (config.SelectSingleNode("customize") == null)
                config.AppendChild(customize);
            //上傳範本
            if (_isUpload4) //如果是自訂範本
            {
                customize.InnerText = base64_4;
                config.ReplaceChild(customize, config.SelectSingleNode("CustomizeTemplate4"));
            }
            customize = config.OwnerDocument.CreateElement("CustomizeTemplate5");
            // XmlElement Retakefree = config.OwnerDocument.CreateElement("Retakefree");
            if (config.SelectSingleNode("customize") == null)
                config.AppendChild(customize);
            //上傳範本
            if (_isUpload5) //如果是自訂範本
            {
                customize.InnerText = base64_5;
                config.ReplaceChild(customize, config.SelectSingleNode("CustomizeTemplate5"));
            }
            customize = config.OwnerDocument.CreateElement("CustomizeTemplate6");
            // XmlElement Retakefree = config.OwnerDocument.CreateElement("Retakefree");
            if (config.SelectSingleNode("customize") == null)
                config.AppendChild(customize);
            //上傳範本
            if (_isUpload6) //如果是自訂範本
            {
                customize.InnerText = base64_6;
                config.ReplaceChild(customize, config.SelectSingleNode("CustomizeTemplate6"));
            }
            cd.SetXml("XmlData", config);
            cd.Save();

            #endregion

            this.DialogResult = DialogResult.OK;
        }
        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void linkDef2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "放棄學籍通知單_範本.docx";
            sfd.Filter = "Word檔案 (*.docx)|*.docx|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    fs.Write(Properties.Resources.視為放棄學籍通知單_7日未到校, 0, Properties.Resources.視為放棄學籍通知單_7日未到校.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "另存檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void linkViewGeDin1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_buffer1 == null)
            {
                MsgBox.Show("目前沒有任何範本，請重新上傳。");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "自訂休學通知單範本.docx";
            sfd.Filter = "Word檔案 (*.docx)|*.docx|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Aspose.Words.Document doc = new Aspose.Words.Document(new MemoryStream(_buffer1));
                    doc.Save(sfd.FileName, Aspose.Words.SaveFormat.Docx);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("檔案無法儲存。" + ex.Message);
                    return;
                }

                try
                {
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("檔案無法開啟。" + ex.Message);
                    return;
                }
            }
        }

        private void linkViewGeDin2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_buffer2 == null)
            {
                MsgBox.Show("目前沒有任何範本，請重新上傳。");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "自訂放棄學籍通知單範本.docx";
            sfd.Filter = "Word檔案 (*.docx)|*.docx|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Aspose.Words.Document doc = new Aspose.Words.Document(new MemoryStream(_buffer2));
                    doc.Save(sfd.FileName, Aspose.Words.SaveFormat.Docx);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("檔案無法儲存。" + ex.Message);
                    return;
                }

                try
                {
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("檔案無法開啟。" + ex.Message);
                    return;
                }
            }
        }

        private void linkViewGeDin3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_buffer3 == null)
            {
                MsgBox.Show("目前沒有任何範本，請重新上傳。");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "自訂復學通知單第一次休學範本.docx";
            sfd.Filter = "Word檔案 (*.docx)|*.docx|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Aspose.Words.Document doc = new Aspose.Words.Document(new MemoryStream(_buffer3));
                    doc.Save(sfd.FileName, Aspose.Words.SaveFormat.Docx);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("檔案無法儲存。" + ex.Message);
                    return;
                }

                try
                {
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("檔案無法開啟。" + ex.Message);
                    return;
                }
            }
        }

        private void linkViewGeDin4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_buffer4 == null)
            {
                MsgBox.Show("目前沒有任何範本，請重新上傳。");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "自訂復學通知單第二次休學範本.docx";
            sfd.Filter = "Word檔案 (*.docx)|*.docx|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Aspose.Words.Document doc = new Aspose.Words.Document(new MemoryStream(_buffer4));
                    doc.Save(sfd.FileName, Aspose.Words.SaveFormat.Docx);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("檔案無法儲存。" + ex.Message);
                    return;
                }

                try
                {
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("檔案無法開啟。" + ex.Message);
                    return;
                }
            }
        }

        private void linkViewGeDin5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_buffer5 == null)
            {
                MsgBox.Show("目前沒有任何範本，請重新上傳。");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "自訂大宗限時掛號及掛號函件執據.docx";
            sfd.Filter = "Word檔案 (*.docx)|*.docx|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Aspose.Words.Document doc = new Aspose.Words.Document(new MemoryStream(_buffer5));
                    doc.Save(sfd.FileName, Aspose.Words.SaveFormat.Docx);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("檔案無法儲存。" + ex.Message);
                    return;
                }

                try
                {
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("檔案無法開啟。" + ex.Message);
                    return;
                }
            }
        }

        private void linkViewGeDin6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_buffer6 == null)
            {
                MsgBox.Show("目前沒有任何範本，請重新上傳。");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "自訂大宗限時掛號及掛號函件存根.docx";
            sfd.Filter = "Word檔案 (*.docx)|*.docx|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Aspose.Words.Document doc = new Aspose.Words.Document(new MemoryStream(_buffer6));
                    doc.Save(sfd.FileName, Aspose.Words.SaveFormat.Docx);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("檔案無法儲存。" + ex.Message);
                    return;
                }

                try
                {
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("檔案無法開啟。" + ex.Message);
                    return;
                }
            }
        }

        private void linkDef3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "復學通知單第一次休學_範本.docx";
            sfd.Filter = "Word檔案 (*.docx)|*.docx|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    fs.Write(Properties.Resources.臺中高工學生復學通知單_第一次休學, 0, Properties.Resources.臺中高工學生復學通知單_第一次休學.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "另存檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void linkDef4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "復學通知單第二次休學_範本.docx";
            sfd.Filter = "Word檔案 (*.docx)|*.docx|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    fs.Write(Properties.Resources.臺中高工學生復學通知單_第二次休學, 0, Properties.Resources.臺中高工學生復學通知單_第二次休學.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "另存檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void linkDef5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "大宗限時掛號及掛號函件執據_範本.docx";
            sfd.Filter = "Word檔案 (*.docx)|*.docx|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    fs.Write(Properties.Resources.大宗限時掛號及掛號函件執據, 0, Properties.Resources.大宗限時掛號及掛號函件執據.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "另存檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void linkDef6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "大宗限時掛號及掛號函件存根_範本.docx";
            sfd.Filter = "Word檔案 (*.docx)|*.docx|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    fs.Write(Properties.Resources.大宗限時掛號及掛號函件存根, 0, Properties.Resources.大宗限時掛號及掛號函件存根.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "另存檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void linkUpData1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "選擇自訂的休學通知單範本";
            ofd.Filter = "Word檔案 (*.docx)|*.docx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(ofd.FileName, FileMode.Open);

                    byte[] tempBuffer = new byte[fs.Length];
                    fs.Read(tempBuffer, 0, tempBuffer.Length);
                    base64_1 = Convert.ToBase64String(tempBuffer);
                    _isUpload1 = true;
                    fs.Close();
                    MsgBox.Show("上傳成功。");
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "開啟檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void linkUpData2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "選擇自訂的放棄學籍通知單範本";
            ofd.Filter = "Word檔案 (*.docx)|*.docx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(ofd.FileName, FileMode.Open);

                    byte[] tempBuffer = new byte[fs.Length];
                    fs.Read(tempBuffer, 0, tempBuffer.Length);
                    base64_2 = Convert.ToBase64String(tempBuffer);
                    _isUpload2 = true;
                    fs.Close();
                    MsgBox.Show("上傳成功。");
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "開啟檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void linkUpData3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "選擇自訂的復學通知單第一次休學範本";
            ofd.Filter = "Word檔案 (*.docx)|*.docx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(ofd.FileName, FileMode.Open);

                    byte[] tempBuffer = new byte[fs.Length];
                    fs.Read(tempBuffer, 0, tempBuffer.Length);
                    base64_3 = Convert.ToBase64String(tempBuffer);
                    _isUpload3 = true;
                    fs.Close();
                    MsgBox.Show("上傳成功。");
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "開啟檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void linkUpData4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "選擇自訂的復學通知單第二次休學範本";
            ofd.Filter = "Word檔案 (*.docx)|*.docx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(ofd.FileName, FileMode.Open);

                    byte[] tempBuffer = new byte[fs.Length];
                    fs.Read(tempBuffer, 0, tempBuffer.Length);
                    base64_4 = Convert.ToBase64String(tempBuffer);
                    _isUpload1 = true;
                    fs.Close();
                    MsgBox.Show("上傳成功。");
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "開啟檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void linkUpData5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "選擇自訂的大宗限時掛號及號函件執據範本";
            ofd.Filter = "Word檔案 (*.docx)|*.docx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(ofd.FileName, FileMode.Open);

                    byte[] tempBuffer = new byte[fs.Length];
                    fs.Read(tempBuffer, 0, tempBuffer.Length);
                    base64_5 = Convert.ToBase64String(tempBuffer);
                    _isUpload5 = true;
                    fs.Close();
                    MsgBox.Show("上傳成功。");
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "開啟檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void linkUpData6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "選擇自訂的大宗限時掛號及號函件存根範本";
            ofd.Filter = "Word檔案 (*.docx)|*.docx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(ofd.FileName, FileMode.Open);

                    byte[] tempBuffer = new byte[fs.Length];
                    fs.Read(tempBuffer, 0, tempBuffer.Length);
                    base64_6 = Convert.ToBase64String(tempBuffer);
                    _isUpload6 = true;
                    fs.Close();
                    MsgBox.Show("上傳成功。");
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "開啟檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
        }

        private void rbDEF_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDEF_2.Checked)
            {
                //radioButton2.Checked = false;
                _defaultTemplate2 = "預設範本2";
            }
        }

        private void rbDEF_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDEF_3.Checked)
            {
                //radioButton2.Checked = false;
                _defaultTemplate3 = "預設範本3";
            }
        }

        private void rbDEF_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDEF_4.Checked)
            {
                //radioButton2.Checked = false;
                _defaultTemplate4 = "預設範本4";
            }
        }

        private void rbDEF_5_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDEF_5.Checked)
            {
                //radioButton2.Checked = false;
                _defaultTemplate5 = "預設範本5";
            }
        }

        private void rbDEF_6_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDEF_6.Checked)
            {
                //radioButton2.Checked = false;
                _defaultTemplate6 = "預設範本6";
            }
        }

        private void rbUDEF_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUDEF_1.Checked)
            {
                //radioButton1.Checked = false;
                _defaultTemplate1 = "自訂範本1";
            }
        }

        private void rbUDEF_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUDEF_2.Checked)
            {
                //radioButton1.Checked = false;
                _defaultTemplate2 = "自訂範本2";
            }
        }

        private void rbUDEF_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUDEF_3.Checked)
            {
                //radioButton1.Checked = false;
                _defaultTemplate3 = "自訂範本3";
            }
        }

        private void rbUDEF_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUDEF_4.Checked)
            {
                //radioButton1.Checked = false;
                _defaultTemplate4 = "自訂範本4";
            }
        }

        private void rbUDEF_5_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUDEF_5.Checked)
            {
                //radioButton1.Checked = false;
                _defaultTemplate5 = "自訂範本5";
            }
        }

        private void rbUDEF_6_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUDEF_6.Checked)
            {
                //radioButton1.Checked = false;
                _defaultTemplate6 = "自訂範本6";
            }
        }
    }
}

