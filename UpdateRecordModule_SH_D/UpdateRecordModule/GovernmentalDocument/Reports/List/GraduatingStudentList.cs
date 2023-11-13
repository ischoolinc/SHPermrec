using System.IO;
using System.Xml;
using Aspose.Cells;
using UpdateRecordModule_SH_D.BL;
using System.Windows.Forms;
namespace UpdateRecordModule_SH_D.GovernmentalDocument.Reports.List
{
    public class GraduatingStudentList : ReportBuilder
    {
        [System.Obsolete]
        protected override void Build(System.Xml.XmlElement source, string location)
        {
            Workbook template = new Workbook();

            //從Resources把Template讀出來
            template.Open(new MemoryStream(Properties.Resources.GraduatingStudentListTemplate), FileFormatType.Xlsx);

            //要產生的excel檔
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(Properties.Resources.GraduatingStudentListTemplate), FileFormatType.Xlsx);

            Worksheet ws = wb.Worksheets[0];

            //頁面間隔幾個row
            int next = 24;

            //索引
            int index = 0;            

            //範本範圍
            Range tempRange = template.Worksheets[0].Cells.CreateRange(0,24,false);

            //總共幾筆異動紀錄
            int count = 0;
            int totalRec = source.SelectNodes("清單/異動紀錄").Count;

            foreach (XmlNode list in source.SelectNodes("清單"))
            {               
                //產生清單第一頁
                //for (int row = 0; row < next; row++)
                //{
                //    ws.Cells.CopyRow(template.Worksheets[0].Cells, row, row + index);
                //}
                ws.Cells.CreateRange(index, next, false).Copy(tempRange);
                ws.Cells.CreateRange(index, next, false).CopyData(tempRange);
                ws.Cells.CreateRange(index, next, false).CopyStyle(tempRange);
                //Page
                int currentPage = 1;
                //2018/6/19 穎驊新增因應客服 https://ischool.zendesk.com/agent/tickets/6076，
                //反映列印不出問題，發現此處Count 會多數一個 上次新增異動名冊封面的邏輯項目，造成數有些屬性找不到而爆炸，
                //因此都先固定減一
                int totalPage = ((list.ChildNodes.Count-1 )/ 18) + 1; 

                //寫入名冊類別
                if (source.SelectSingleNode("@類別").InnerText == "畢業名冊")
                    ws.Cells[index, 0].PutValue(ws.Cells[index, 0].StringValue.Replace("□畢業", "■畢業"));
                else
                    ws.Cells[index, 0].PutValue(ws.Cells[index, 0].StringValue.Replace("□結業", "■結業"));

                ////寫入代號
                //ws.Cells[index,6].PutValue("代碼："+source.SelectSingleNode("@學校代號").InnerText+"-"+list.SelectSingleNode("@科別代號").InnerText);

                ////寫入校名、學年度、學期、科別
                //ws.Cells[index+2, 0].PutValue("校名：" + source.SelectSingleNode("@學校名稱").InnerText);
                //ws.Cells[index+2, 4].PutValue(source.SelectSingleNode("@學年度").InnerText + "學年度 第" + source.SelectSingleNode("@學期").InnerText + "學期");
                //ws.Cells[index+2, 6].PutValue(list.SelectSingleNode("@科別").InnerText);

                //寫入資料
                int recCount = 0;
                int dataIndex = index + 5;
                for (; currentPage <= totalPage; currentPage++)
                {
                    //寫入代號
                    ws.Cells[index, 6].PutValue("代碼：" + source.SelectSingleNode("@學校代號").InnerText + "-" + list.SelectSingleNode("@科別代號").InnerText);

                    //寫入校名、學年度、學期、科別
                    ws.Cells[index + 2, 0].PutValue("校名：" + source.SelectSingleNode("@學校名稱").InnerText);
                    ws.Cells[index + 2, 4].PutValue(source.SelectSingleNode("@學年度").InnerText + "學年度 第" + source.SelectSingleNode("@學期").InnerText + "學期");
                    ws.Cells[index + 2, 6].PutValue(list.SelectSingleNode("@科別").InnerText);

                    //複製頁面
                    if (currentPage+1 <= totalPage)
                    {
                        ws.Cells.CreateRange(index + next, next, false).Copy(tempRange);
                        ws.Cells.CreateRange(index + next, next, false).CopyData(tempRange);
                        ws.Cells.CreateRange(index + next, next, false).CopyStyle(tempRange);
                        //寫入名冊類別
                        if (source.SelectSingleNode("@類別").InnerText == "畢業名冊")
                            ws.Cells[index + next, 0].PutValue(ws.Cells[index + next, 0].StringValue.Replace("□畢業", "■畢業"));
                        else
                            ws.Cells[index + next, 0].PutValue(ws.Cells[index + next, 0].StringValue.Replace("□結業", "■結業"));
                    }

                    //填入資料
                    for (int i = 0; i < 18 && recCount < list.ChildNodes.Count -1; i++, recCount++)
                    {
                        //MsgBox.Show(i.ToString()+" "+recCount.ToString());
                        XmlNode rec = list.SelectNodes("異動紀錄")[recCount];
                        ws.Cells[dataIndex, 0].PutValue(rec.SelectSingleNode("@學號").InnerText + "\n" + rec.SelectSingleNode("@姓名").InnerText);
                        ws.Cells[dataIndex, 1].PutValue(rec.SelectSingleNode("@性別代號").InnerText.ToString());
                        ws.Cells[dataIndex, 2].PutValue(rec.SelectSingleNode("@性別").InnerText);

                        string ssn = "";
                        if(rec.SelectSingleNode("@身分證號")!=null)
                            ssn=rec.SelectSingleNode("@身分證號").InnerText;

                        if (ssn == "")
                            if(rec.SelectSingleNode("@身份證號")!=null)
                                ssn = rec.SelectSingleNode("@身份證號").InnerText;
                        ws.Cells[dataIndex, 3].PutValue(Util.ConvertDateStr2(rec.SelectSingleNode("@生日").InnerText) + "\n" + ssn);
                        if(rec.SelectSingleNode("@最後異動代號")!=null )
                            ws.Cells[dataIndex, 4].PutValue(rec.SelectSingleNode("@最後異動代號").InnerText.ToString());
                        //if (rec.SelectSingleNode("@原臨編字號").InnerText == "")
                        //{
                            ws.Cells[dataIndex, 5].PutValue(Util.ConvertDateStr2(rec.SelectSingleNode("@備查日期").InnerText) + "\n" + rec.SelectSingleNode("@備查文號").InnerText);
                        //}
                        //else
                        //{
                        //    ws.Cells[dataIndex, 5].PutValue(Util.ConvertDateStr2(rec.SelectSingleNode("@原臨編`日期").InnerText) + "\n" + rec.SelectSingleNode("@原臨編學統").InnerText + rec.SelectSingleNode("@原臨編字號").InnerText);
                        //}
                        ws.Cells[dataIndex, 6].PutValue((rec.SelectSingleNode("@畢業證書字號").InnerText).Replace(" ",""));

                        
                        //ws.Cells[dataIndex, 7].PutValue(rec.SelectSingleNode("@備註").InnerText);
                        if(rec.SelectSingleNode("@特殊身份代碼")!=null)
                            ws.Cells[dataIndex, 7].PutValue(rec.SelectSingleNode("@特殊身份代碼").InnerText);

                        dataIndex++;
                        count++;
                    }

                    //計算合計
                    if (currentPage == totalPage)
                    {
                        ws.Cells[dataIndex, 0].PutValue("合計");
                        ws.Cells[dataIndex, 1].PutValue((list.ChildNodes.Count-1).ToString());
                        //ws.Cells[index + 22, 0].PutValue("合計");
                        //ws.Cells[index + 22, 1].PutValue(list.ChildNodes.Count.ToString());
                    }

                    //分頁
                    ws.Cells[index + 23, 6].PutValue("第 " + currentPage + " 頁，共 " + totalPage + " 頁");
                    ws.HPageBreaks.Add(index+24, 8);

                    //索引指向下一頁
                    index += next;
                    dataIndex = index + 5;

                    //回報進度
                    ReportProgress((int)(((double)count * 100.0) / ((double)totalRec)));
                }
            }


            #region 畢業異動,電子格式

            //範本
            Worksheet TemplateWb = wb.Worksheets["電子格式範本"];
            //實做頁面
            Worksheet DyWb = wb.Worksheets[wb.Worksheets.Add()];
            //名稱
            DyWb.Name = "畢業生名冊";
            //範圍
            Range range_H = TemplateWb.Cells.CreateRange(0, 1, false);
            Range range_R = TemplateWb.Cells.CreateRange(1, 1, false);
            // 107新格式 結束行要 有End 字樣
            Range range_R_EndRow = TemplateWb.Cells.CreateRange(2, 1, false);
            //拷貝range_H
            DyWb.Cells.CreateRange(0, 1, false).CopyData(range_H);
            DyWb.Cells.CreateRange(0, 1, false).CopyStyle(range_H);
            int DyWb_index = 0;

                        
            foreach(XmlElement Record in source.SelectNodes("清單/異動紀錄"))
            {
                DyWb_index++;
                //每增加一行,複製一次
                DyWb.Cells.CreateRange(DyWb_index,1,false).CopyStyle(range_R);

                //班別
                DyWb.Cells[DyWb_index, 0].PutValue(Record.GetAttribute("班別"));
                //科別代碼
                DyWb.Cells[DyWb_index, 1].PutValue((Record.ParentNode as XmlElement).GetAttribute("科別代號"));
                
                // 上傳類別
                
                //學號
                DyWb.Cells[DyWb_index, 3].PutValue(Record.GetAttribute("學號"));
                //姓名
                DyWb.Cells[DyWb_index, 4].PutValue(Record.GetAttribute("姓名"));
                //身分證字號
                if(Record.GetAttribute("身分證號")=="")
                    DyWb.Cells[DyWb_index, 5].PutValue(Record.GetAttribute("身份證號"));
                else
                    DyWb.Cells[DyWb_index, 5].PutValue(Record.GetAttribute("身分證號"));

                //註1
                DyWb.Cells[DyWb_index,6].PutValue(Record.GetAttribute("註1"));

                //性別代碼
                DyWb.Cells[DyWb_index, 7].PutValue(Record.GetAttribute("性別代號"));
                //出生日期
                if(!string.IsNullOrEmpty(Record.GetAttribute("生日")))
                    DyWb.Cells[DyWb_index, 8].PutValue(GetBirthdateWithoutSlash(Record.GetAttribute("生日")));
                else
                    DyWb.Cells[DyWb_index, 8].PutValue(GetBirthdateWithoutSlash(Record.GetAttribute("生日1")));
                // 特殊身分代碼
                DyWb.Cells[DyWb_index, 9].PutValue(Record.GetAttribute("特殊身分代碼"));
                // 年級
                DyWb.Cells[DyWb_index, 10].PutValue(Record.GetAttribute("年級"));
                // 學籍異動代碼
                DyWb.Cells[DyWb_index, 11].PutValue(Record.GetAttribute("最後異動代號"));
                //if (Record.GetAttribute("原臨編字號") == "")
                //{
                    //學籍異動文字
                    //DyWb.Cells[DyWb_index, 12].PutValue(Util.GetDocNo_Doc(Record.GetAttribute("備查文號")));
                    DyWb.Cells[DyWb_index, 12].PutValue(DAL.DALTransfer.GetNumAndSrt1(Record.GetAttribute("備查文號")));
                    //學籍異動文號
                    //DyWb.Cells[DyWb_index, 13].PutValue(Util.GetDocNo_No(Record.GetAttribute("備查文號")));
                    DyWb.Cells[DyWb_index, 13].PutValue(DAL.DALTransfer.GetNumAndSrt2(Record.GetAttribute("備查文號")));
                    // 學籍異動核准日期
                    DyWb.Cells[DyWb_index, 14].PutValue(Util.ConvertDate1(Record.GetAttribute("備查日期")));
                //}
                //else
                //{
                //    //臨編學籍異動文字
                //    //DyWb.Cells[DyWb_index, 12].PutValue(Util.GetDocNo_Doc(Record.GetAttribute("備查文號")));
                //    DyWb.Cells[DyWb_index, 12].PutValue(Record.GetAttribute("原臨編學統"));
                //    //臨編異動文號
                //    //DyWb.Cells[DyWb_index, 13].PutValue(Util.GetDocNo_No(Record.GetAttribute("備查文號")));
                //    DyWb.Cells[DyWb_index, 13].PutValue(Record.GetAttribute("原臨編字號"));
                //    // 臨編異動核准日期
                //    DyWb.Cells[DyWb_index, 14].PutValue(Util.ConvertDate1(Record.GetAttribute("原臨編日期")));
                //}
                //畢業證書字號
                DyWb.Cells[DyWb_index, 15].PutValue(Record.GetAttribute("畢業證書字號").Replace(" ",""));

                //畢業證書註記學程代碼 (2019/02/15 穎驊 檢查發現 目前我們系統沒有支援這個概念，要再研究)
                DyWb.Cells[DyWb_index, 16].PutValue("");

                //備註說明
                DyWb.Cells[DyWb_index, 17].PutValue(Record.GetAttribute("備註"));
            }

            // 資料末底 加End
            DyWb.Cells.CreateRange(DyWb_index + 1, 1, false).CopyData(range_R_EndRow);
            DyWb.Cells.CreateRange(DyWb_index + 1, 1, false).CopyStyle(range_R_EndRow);
            DyWb.AutoFitColumns();

            wb.Worksheets.RemoveAt("電子格式範本");
            //範本
            TemplateWb = wb.Worksheets["電子格式範本_含臨編"];
            //實做頁面
            DyWb = wb.Worksheets[wb.Worksheets.Add()];
            //名稱
            DyWb.Name = "畢業生名冊_含臨編";
            //範圍
            range_H = TemplateWb.Cells.CreateRange(0, 1, false);
            range_R = TemplateWb.Cells.CreateRange(1, 1, false);
            // 107新格式 結束行要 有End 字樣
            range_R_EndRow = TemplateWb.Cells.CreateRange(2, 1, false);
            //拷貝range_H
            DyWb.Cells.CreateRange(0, 1, false).CopyData(range_H);
            DyWb.Cells.CreateRange(0, 1, false).CopyStyle(range_H);
            DyWb_index = 0;


            foreach (XmlElement Record in source.SelectNodes("清單/異動紀錄"))
            {
                DyWb_index++;
                //每增加一行,複製一次
                DyWb.Cells.CreateRange(DyWb_index, 1, false).CopyStyle(range_R);

                //班別
                DyWb.Cells[DyWb_index, 0].PutValue(Record.GetAttribute("班別"));
                //科別代碼
                DyWb.Cells[DyWb_index, 1].PutValue((Record.ParentNode as XmlElement).GetAttribute("科別代號"));

                // 上傳類別

                //學號
                DyWb.Cells[DyWb_index, 3].PutValue(Record.GetAttribute("學號"));
                //姓名
                DyWb.Cells[DyWb_index, 4].PutValue(Record.GetAttribute("姓名"));
                //身分證字號
                if (Record.GetAttribute("身分證號") == "")
                    DyWb.Cells[DyWb_index, 5].PutValue(Record.GetAttribute("身份證號"));
                else
                    DyWb.Cells[DyWb_index, 5].PutValue(Record.GetAttribute("身分證號"));

                //註1
                DyWb.Cells[DyWb_index, 6].PutValue(Record.GetAttribute("註1"));

                //性別代碼
                DyWb.Cells[DyWb_index, 7].PutValue(Record.GetAttribute("性別代號"));
                //出生日期
                if (!string.IsNullOrEmpty(Record.GetAttribute("生日")))
                    DyWb.Cells[DyWb_index, 8].PutValue(GetBirthdateWithoutSlash(Record.GetAttribute("生日")));
                else
                    DyWb.Cells[DyWb_index, 8].PutValue(GetBirthdateWithoutSlash(Record.GetAttribute("生日1")));
                // 特殊身分代碼
                DyWb.Cells[DyWb_index, 9].PutValue(Record.GetAttribute("特殊身分代碼"));
                // 年級
                DyWb.Cells[DyWb_index, 10].PutValue(Record.GetAttribute("年級"));
                // 學籍異動代碼
                DyWb.Cells[DyWb_index, 11].PutValue(Record.GetAttribute("最後異動代號"));
                
                //學籍異動文字
                //DyWb.Cells[DyWb_index, 12].PutValue(Util.GetDocNo_Doc(Record.GetAttribute("備查文號")));
                DyWb.Cells[DyWb_index, 12].PutValue(DAL.DALTransfer.GetNumAndSrt1(Record.GetAttribute("備查文號")));
                //學籍異動文號
                //DyWb.Cells[DyWb_index, 13].PutValue(Util.GetDocNo_No(Record.GetAttribute("備查文號")));
                DyWb.Cells[DyWb_index, 13].PutValue(DAL.DALTransfer.GetNumAndSrt2(Record.GetAttribute("備查文號")));
                // 學籍異動核准日期
                DyWb.Cells[DyWb_index, 14].PutValue(Util.ConvertDate1(Record.GetAttribute("備查日期")));
                
                //畢業證書字號
                DyWb.Cells[DyWb_index, 15].PutValue(Record.GetAttribute("畢業證書字號").Replace(" ", ""));

                //畢業證書註記學程代碼 (2019/02/15 穎驊 檢查發現 目前我們系統沒有支援這個概念，要再研究)
                DyWb.Cells[DyWb_index, 16].PutValue("");

                //備註說明
                DyWb.Cells[DyWb_index, 17].PutValue(Record.GetAttribute("備註"));

                // 臨編異動核准日期
                DyWb.Cells[DyWb_index, 18].PutValue(Util.ConvertDate1(Record.GetAttribute("原臨編日期")));
                //臨編學籍異動文字

                DyWb.Cells[DyWb_index, 19].PutValue(Record.GetAttribute("原臨編學統"));
                //臨編異動文號
                
                DyWb.Cells[DyWb_index, 20].PutValue(Record.GetAttribute("原臨編字號"));
                
            }

            // 資料末底 加End
            DyWb.Cells.CreateRange(DyWb_index + 1, 1, false).CopyData(range_R_EndRow);
            DyWb.Cells.CreateRange(DyWb_index + 1, 1, false).CopyStyle(range_R_EndRow);
            DyWb.AutoFitColumns();

            wb.Worksheets.RemoveAt("電子格式範本_含臨編");

            //範本
            Worksheet TemplateWb_Cover = wb.Worksheets["畢業生名冊封面範本"];

            //實做頁面
            Worksheet cover = wb.Worksheets[wb.Worksheets.Add()];

            //名稱
            cover.Name = "畢業生名冊封面";

            string school_code = source.SelectSingleNode("@學校代號").InnerText;
            string school_year = source.SelectSingleNode("@學年度").InnerText;
            string school_semester = source.SelectSingleNode("@學期").InnerText;

            //範圍
            Range range_H_Cover = TemplateWb_Cover.Cells.CreateRange(0, 1, false);

            //range_H_Cover
            cover.Cells.CreateRange(0, 1, false).CopyData(range_H_Cover);
            cover.Cells.CreateRange(0, 1, false).CopyStyle(range_H_Cover);
            Range range_R_cover = TemplateWb_Cover.Cells.CreateRange(1, 1, false);
            // 107新格式 結束行要 有End 字樣
            Range range_R_cover_EndRow = TemplateWb_Cover.Cells.CreateRange(2, 1, false);

            int cover_row_counter = 1;

            //2018/2/2 穎驊註解 ，下面是新的封面產生方式

            foreach (XmlNode list in source.SelectNodes("清單"))
            {
                //每增加一行,複製一次
                cover.Cells.CreateRange(cover_row_counter, 1, false).CopyStyle(range_R_cover);

                string gradeYear = list.SelectSingleNode("@年級").InnerText;
                string deptCode = list.SelectSingleNode("@科別代碼").InnerText;

                //2021-09 
                //學校代碼
                cover.Cells[cover_row_counter, 0].PutValue(school_code);

                //學年度
                cover.Cells[cover_row_counter, 1].PutValue(school_year);
                //學期
                cover.Cells[cover_row_counter, 2].PutValue(school_semester);
                //年級
                cover.Cells[cover_row_counter, 3].PutValue(gradeYear);
                //科別代碼
                cover.Cells[cover_row_counter, 6].PutValue(deptCode);

                foreach (XmlElement st in list.SelectNodes("異動名冊封面"))
                {
                    string reportType = st.SelectSingleNode("@名冊別").InnerText;
                    string classType = st.SelectSingleNode("@班別").InnerText;
                    string updateType = st.SelectSingleNode("@上傳類別").InnerText;
                    string approvedClassCount = st.SelectSingleNode("@核定班數").InnerText;
                    string approvedStudentCount = st.SelectSingleNode("@核定學生數").InnerText;
                    string actualClassCount = st.SelectSingleNode("@實招班數").InnerText;
                    string actualStudentCount = st.SelectSingleNode("@實招新生數").InnerText;
                    string originalStudentCount = st.SelectSingleNode("@原有學生數").InnerText;                    
                    string graduatedStudentCount = st.SelectSingleNode("@畢業學生數").InnerText;                    
                    string remarksContent = st.SelectSingleNode("@備註說明").InnerText;

                    //名冊別
                    cover.Cells[cover_row_counter, 4].PutValue(reportType);
                    //班別
                    cover.Cells[cover_row_counter, 5].PutValue(classType);
                    //上傳類別
                    cover.Cells[cover_row_counter, 7].PutValue(updateType);
                    //核定班級
                    cover.Cells[cover_row_counter, 8].PutValue(approvedClassCount);
                    //核定學生數
                    cover.Cells[cover_row_counter, 9].PutValue(approvedStudentCount);
                    //實招班數
                    cover.Cells[cover_row_counter, 10].PutValue(actualClassCount);
                    //實招新生數
                    cover.Cells[cover_row_counter, 11].PutValue(actualStudentCount);
                    //原有學生數
                    cover.Cells[cover_row_counter, 12].PutValue(originalStudentCount);
                    //畢業學生數
                    cover.Cells[cover_row_counter, 13].PutValue(graduatedStudentCount);
                    //備註說明
                    cover.Cells[cover_row_counter, 14].PutValue(remarksContent);

                }
                cover_row_counter++;
            }

            // 資料末底 加End
            cover.Cells.CreateRange(cover_row_counter, 1, false).CopyData(range_R_cover_EndRow);
            cover.Cells.CreateRange(cover_row_counter, 1, false).CopyStyle(range_R_cover_EndRow);
            wb.Worksheets.RemoveAt("畢業生名冊封面範本");

            #endregion

            wb.Worksheets.ActiveSheetIndex = 0;
            //儲存
            try
            {
                wb.Save(location, SaveFormat.Xlsx);
                System.Diagnostics.Process.Start(location);
            }
            catch
            {
                MessageBox.Show("檔案儲存失敗");
            }


        }

        //處理符號"/"
        private string GetBirthdateWithoutSlash(string orig)
        {
            if (string.IsNullOrEmpty(orig)) return orig;
            string[] array = orig.Split('/');
            int chang;
            if (array[0].Length == 4)
            {
                chang = int.Parse(array[0]) - 1911;
            }
            else
            {
                chang = int.Parse(array[0]);
            }
            return chang.ToString() + array[1].PadLeft(2, '0') + array[2].PadLeft(2, '0');
        }

        public override string Copyright
        {
            get { return "IntelliSchool"; } 
        }

        public override string Description
        {
            get { return "中部辦公室95年11月編印管理手冊規範格式"; }
        }

        public override string ReportName
        {
            get { return "畢業名冊"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }
    }
}
