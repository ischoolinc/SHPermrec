using System;
using System.IO;
using System.Xml;
using Aspose.Cells;
using UpdateRecordModule_SH_D.BL;

namespace UpdateRecordModule_SH_D.GovernmentalDocument.Reports.List
{
    public class ExtendingStudentList : ReportBuilder
    {
        protected override void Build(System.Xml.XmlElement source, string location)
        {
            #region 建立 Excel

            //從 Resources 將學籍異動名冊template讀出來
            Workbook template = new Workbook();
            template.Open(new MemoryStream(Properties.Resources.ExtendingStudentListTemplate), FileFormatType.Excel2003);

            //產生 excel
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(Properties.Resources.ExtendingStudentListTemplate), FileFormatType.Excel2003);
                
            #endregion

            #region 複製樣式-預設樣式、欄寬

            //設定預設樣式
            wb.DefaultStyle = template.DefaultStyle;

            //複製樣版中前18個 Column(欄寬)
            for (int m = 0; m < 18; m++)
            {
                /*
                 * 複製 template的第一個 Sheet的第 m個 Column
                 * 到 wb的第一個 Sheet的第 m個 Column
                 */
                wb.Worksheets[0].Cells.CopyColumn(template.Worksheets[0].Cells, m, m);
            }

            #endregion

            #region 初始變數
            
                /****************************** 
                * rowi 填入學校資料用
                * rowj 填入學生資料用
                * num 計算清單份數
                * numcount 計算每份清單頁數
                * j 計算所產生清單頁數
                * x 判斷個數是否為20被數用
                ******************************/
                int rowi = 0, rowj = 1, num = source.SelectNodes("清單").Count, numcount = 1, j = 0;
                bool x = false;

                int recCount = 0;
                int totalRec = source.SelectNodes("清單/異動紀錄").Count;
            
            #endregion

            foreach (XmlNode list in source.SelectNodes("清單"))
            {
                int i = 0;

                #region 找出資料總數及判斷

                //找出資料總數方便評估進度
                int count = list.SelectNodes("異動紀錄").Count;

                //判斷個數是否為20被數
                if (count % 20 == 0)
                {
                    x = true;
                } 

                #endregion
                

                #region 異動紀錄

                //將xml資料填入至excel
                foreach (XmlNode st in list.SelectNodes("異動紀錄"))
                {
                    recCount++;
                    if (i % 20 == 0)
                    {
                        #region 複製樣式-欄高、範圍

                        //複製樣版中前287個 Row(欄高)
                        //for (int m = 0; m < 28; m++)
                        //{
                        //    /*
                        //     * 複製 template的第一個 Sheet的第m個 Row
                        //     * 到 wb的第一個 Sheet的第(j * 28) + m個 Row
                        //     */
                        //    wb.Worksheets[0].Cells.CopyRow(template.Worksheets[0].Cells, m, (j * 28) + m);
                        //}

                        /*
                         * 複製Style(包含儲存格合併的資訊)
                         * 先用CreateRange()選取要複製的Range("A1", "R28")
                         * 再用CopyStyle複製另一個Range中的格式
                         */
                        Range range = template.Worksheets[0].Cells.CreateRange(0, 28, false);
                        int t= j * 28;
                        wb.Worksheets[0].Cells.CreateRange(t,28,false).Copy(range);

                        #endregion

                        #region 填入學校資料

                        //將學校資料填入適當的位置內
                        wb.Worksheets[0].Cells[rowi, 13].PutValue(source.SelectSingleNode("@學校代號").InnerText);
                        wb.Worksheets[0].Cells[rowi, 16].PutValue(list.SelectSingleNode("@科別代號").InnerText);
                        wb.Worksheets[0].Cells[rowi + 2, 2].PutValue(source.SelectSingleNode("@學校名稱").InnerText);
                        wb.Worksheets[0].Cells[rowi + 2, 7].PutValue(Convert.ToInt32(source.SelectSingleNode("@學年度").InnerText)+" 學年度 第 "+Convert.ToInt32(source.SelectSingleNode("@學期").InnerText)+" 學期");
                        wb.Worksheets[0].Cells[rowi + 2, 12].PutValue(list.SelectSingleNode("@科別").InnerText);
                        wb.Worksheets[0].Cells[rowi + 2, 14].PutValue(list.SelectSingleNode("@年級").InnerText);

                        #endregion

                        if (j > 0)
                        {
                            //插入分頁(在 j * 28 跟 (j * 28) +1 中間，R跟S中間)
                            wb.Worksheets[0].HPageBreaks.Add(j * 28, 18);
                            rowj += 8;
                        }
                        else
                        {
                            rowj = 6;
                        }

                        rowi += 28;
                        j++;

                        #region 顯示頁數

                        //顯示頁數
                        if (x != true)
                        {
                            wb.Worksheets[0].Cells[(28 * (j - 1)) + 27, 13].PutValue("第" + numcount + "頁，共" + Math.Ceiling((double)count / 20) + "頁");
                        }
                        else
                        {
                            wb.Worksheets[0].Cells[(28 * (j - 1)) + 27, 13].PutValue("第" + numcount + "頁，共" + (Math.Ceiling((double)count / 20) + 1) + "頁");
                        }
                        numcount++;

                        #endregion
                    }

                    #region 填入學生資料
                    
                        //將學生資料填入適當的位置內
                        wb.Worksheets[0].Cells[rowj, 1].PutValue(st.SelectSingleNode("@學號").InnerText);
                        wb.Worksheets[0].Cells[rowj, 3].PutValue(st.SelectSingleNode("@姓名").InnerText);
                        wb.Worksheets[0].Cells[rowj, 4].PutValue(st.SelectSingleNode("@身分證號").InnerText);
                        wb.Worksheets[0].Cells[rowj, 8].PutValue(Util.ConvertDateStr2(st.SelectSingleNode("@備查日期").InnerText) + "\n" + st.SelectSingleNode("@備查文號").InnerText);
                        wb.Worksheets[0].Cells[rowj, 11].PutValue(st.SelectSingleNode("@異動代號").InnerText);
                        wb.Worksheets[0].Cells[rowj, 12].PutValue(st.SelectSingleNode("@原因及事項").InnerText);
                        if (st.SelectSingleNode("@新學號").InnerText == "")
                        {
                            wb.Worksheets[0].Cells[rowj, 13].PutValue(Util.ConvertDateStr2(st.SelectSingleNode("@異動日期").InnerText));
                        }
                        else
                        {
                            wb.Worksheets[0].Cells[rowj, 13].PutValue(st.SelectSingleNode("@新學號").InnerText + "\n" + Util.ConvertDateStr2(st.SelectSingleNode("@異動日期").InnerText));
                        }
                            //wb.Worksheets[0].Cells[rowj, 16].PutValue(st.SelectSingleNode("@備註").InnerText);
                    if(st.SelectSingleNode("@特殊身份代碼")!=null)    
                        wb.Worksheets[0].Cells[rowj, 16].PutValue(st.SelectSingleNode("@特殊身份代碼").InnerText);

                    #endregion

                    i++;
                    rowj++;

                    //回報進度
                    ReportProgress((int)(((double)recCount * 100.0) / ((double)totalRec)));
                }

                #endregion

                #region 若個數為20倍數，處理單一頁面

                if (x == true)
                {

                    #region 複製樣式-欄高、範圍

                    //複製樣版前28個 Row(欄高)
                    //for (int m = 0; m < 28; m++)
                    //{
                    //    /*
                    //     * 複製 template的第一個 Sheet的第m個 Row
                    //     * 到 wb的第一個 Sheet的第(j * 28) + m個 Row
                    //     */
                    //    wb.Worksheets[0].Cells.CopyRow(template.Worksheets[0].Cells, m, (j * 28) + m);
                    //}

                    /*
                     * 複製Style(包含儲存格合併的資訊)
                     * 先用CreateRange()選取要複製的Range("A1", "R28")
                     * 再用CopyStyle複製另一個Range中的格式
                     */
                    Range range = template.Worksheets[0].Cells.CreateRange(0, 28, false);                    
                    int t= j * 28;
                    wb.Worksheets[0].Cells.CreateRange(t, 28, false).Copy(range);

                    #endregion

                    #region 填入學校資料

                    //將學校資料填入適當的位置內
                    wb.Worksheets[0].Cells[rowi, 13].PutValue(source.SelectSingleNode("@學校代號").InnerText);
                    wb.Worksheets[0].Cells[rowi, 16].PutValue(list.SelectSingleNode("@科別代號").InnerText);
                    wb.Worksheets[0].Cells[rowi + 2, 2].PutValue(source.SelectSingleNode("@學校名稱").InnerText);
                    wb.Worksheets[0].Cells[rowi + 2, 7].PutValue(Convert.ToInt32(source.SelectSingleNode("@學年度").InnerText) + " 學年度 第 " + Convert.ToInt32(source.SelectSingleNode("@學期").InnerText) + " 學期");
                    wb.Worksheets[0].Cells[rowi + 2, 12].PutValue(list.SelectSingleNode("@科別").InnerText);

                    #endregion

                    if (j > 0)
                    {
                        //插入分頁(在i跟i+1中間，O跟P中間)
                        wb.Worksheets[0].HPageBreaks.Add(j * 28, 18);
                        rowj += 8;
                    }

                    rowi += 28;
                    j++;

                    #region 顯示頁數

                    //顯示頁數
                    wb.Worksheets[0].Cells[(28 * (j - 1)) + 27, 13].PutValue("第" + numcount + "頁，共" + (Math.Ceiling((double)count / 20) + 1) + "頁");
                    numcount++;

                    #endregion
                } 

                #endregion

                #region 統計人數

                //填入統計人數
                wb.Worksheets[0].Cells.CreateRange(rowj, 1, 1, 2).UnMerge();
                wb.Worksheets[0].Cells.Merge(rowj, 1, 1, 3);
                wb.Worksheets[0].Cells[rowj, 1].PutValue("合  計 " + count.ToString() + " 名");

                #endregion

                wb.Worksheets[0].HPageBreaks.Add(j * 28, 18);

                #region 設定變數

                //調整新清單所使用變數
                numcount = 1;
                rowj = (28 * j) - 2;
                rowi = (28 * j);
                x = false; 

                #endregion
            }
                       
            //範本
            Worksheet TemplateWb_Cover = wb.Worksheets["延修生名冊封面範本"];

            //實做頁面
            Worksheet cover = wb.Worksheets[wb.Worksheets.Add()];

            //名稱
            cover.Name = "延修生名冊封面";

            string school_code = source.SelectSingleNode("@學校代號").InnerText;
            string school_year = source.SelectSingleNode("@學年度").InnerText;
            string school_semester = source.SelectSingleNode("@學期").InnerText;

            //範圍
            Range range_H_Cover = TemplateWb_Cover.Cells.CreateRange(0, 1, false);

            //range_H_Cover
            cover.Cells.CreateRange(0, 1, false).Copy(range_H_Cover);

            Range range_R_cover = TemplateWb_Cover.Cells.CreateRange(1, 1, false);
            // 107新格式 結束行要 有End 字樣
            Range range_R_cover_EndRow = TemplateWb_Cover.Cells.CreateRange(2, 1, false);


            int cover_row_counter = 1;

            //2018/2/2 穎驊註解 ，下面是新的封面產生方式

            foreach (XmlNode list in source.SelectNodes("清單"))
            {
                //每增加一行,複製一次
                cover.Cells.CreateRange(cover_row_counter, 1, false).Copy(range_R_cover);

                string gradeYear = list.SelectSingleNode("@年級").InnerText;
                string deptCode = list.SelectSingleNode("@科別代碼").InnerText;

                //學校代碼
                cover.Cells[cover_row_counter, 0].PutValue(school_code);
                //學年度
                cover.Cells[cover_row_counter, 1].PutValue(school_year);
                //學期
                cover.Cells[cover_row_counter, 2].PutValue(school_semester);                

                //科別代碼
                cover.Cells[cover_row_counter, 6].PutValue(deptCode);

                foreach (XmlElement st in list.SelectNodes("異動名冊封面"))
                {
                    string reportType = st.SelectSingleNode("@名冊別").InnerText;
                    string scheduledGraduateYear = st.SelectSingleNode("@應畢業學年度").InnerText;
                    string classType = st.SelectSingleNode("@班別").InnerText;
                    string updateType = st.SelectSingleNode("@上傳類別").InnerText;
                    string approvedExtendingStudentCount = st.SelectSingleNode("@輔導延修學生數").InnerText;
                    string waitingExtendingStudentCount = st.SelectSingleNode("@未申請延修學生數").InnerText;                    
                    string originalStudentCount = st.SelectSingleNode("@原有學生數").InnerText;
                    string increaseStudentCount = st.SelectSingleNode("@增加學生數").InnerText;                    
                    string currentStudentCount = st.SelectSingleNode("@現有學生數").InnerText;                    
                    string remarksContent = st.SelectSingleNode("@備註說明").InnerText;

                    //名冊別
                    cover.Cells[cover_row_counter, 3].PutValue(reportType);
                    //應畢業學年度
                    cover.Cells[cover_row_counter, 4].PutValue(scheduledGraduateYear);
                    //班別
                    cover.Cells[cover_row_counter, 5].PutValue(classType);
                    //上傳類別
                    cover.Cells[cover_row_counter, 7].PutValue(updateType);
                    //輔導延修學生數
                    cover.Cells[cover_row_counter, 8].PutValue(approvedExtendingStudentCount);
                    //未申請延修學生數
                    cover.Cells[cover_row_counter, 9].PutValue(waitingExtendingStudentCount);              
                    //原有學生數
                    cover.Cells[cover_row_counter, 10].PutValue(originalStudentCount);
                    //增加學生數
                    cover.Cells[cover_row_counter, 11].PutValue(increaseStudentCount);;
                    //現有學生數
                    cover.Cells[cover_row_counter, 12].PutValue(currentStudentCount);                    
                    //備註說明
                    cover.Cells[cover_row_counter, 13].PutValue(remarksContent);

                }
                cover_row_counter++;
            }

            // 資料末底 加End
            cover.Cells.CreateRange(cover_row_counter, 1, false).Copy(range_R_cover_EndRow);


            //範本
            Worksheet TemplateWb = wb.Worksheets["電子格式範本"];

            //實做頁面
            Worksheet mdws = wb.Worksheets[wb.Worksheets.Add()];

            mdws.Name = "延修生名冊";

            //範圍
            Range range_H = TemplateWb.Cells.CreateRange(0, 1, false);
            Range range_R = TemplateWb.Cells.CreateRange(1, 1, false);
            // 107新格式 結束行要 有End 字樣
            Range range_R_EndRow = TemplateWb.Cells.CreateRange(2, 1, false);
            //拷貝range_H
            mdws.Cells.CreateRange(0, 1, false).Copy(range_H);

            int mdws_index = 0;
            foreach (XmlElement record in source.SelectNodes("清單/異動紀錄"))
            {
                mdws_index++;

                //每增加一行,複製一次
                mdws.Cells.CreateRange(mdws_index, 1, false).Copy(range_R);

                // 應畢業學年度                
                mdws.Cells[mdws_index, 0].PutValue(record.GetAttribute("應畢業學年度"));
                mdws.Cells[mdws_index, 1].PutValue(record.GetAttribute("班別"));
                mdws.Cells[mdws_index, 2].PutValue((record.ParentNode as XmlElement).GetAttribute("科別代號"));
                mdws.Cells[mdws_index, 3].PutValue("");

                mdws.Cells[mdws_index, 4].PutValue(record.GetAttribute("學號"));
                mdws.Cells[mdws_index, 5].PutValue(record.GetAttribute("姓名"));
                mdws.Cells[mdws_index, 6].PutValue(record.GetAttribute("身分證號"));
                mdws.Cells[mdws_index, 7].PutValue(record.GetAttribute("註1"));
                mdws.Cells[mdws_index, 8].PutValue(record.GetAttribute("性別代號"));
                mdws.Cells[mdws_index, 9].PutValue((BL.Util.ConvertDate1(record.GetAttribute("出生年月日"))));
                mdws.Cells[mdws_index, 10].PutValue(record.GetAttribute("特殊身份代碼")); //原為抓取註備欄位值
                mdws.Cells[mdws_index, 11].PutValue(record.GetAttribute("異動代號"));
                mdws.Cells[mdws_index, 12].PutValue(BL.Util.ConvertDate1(record.GetAttribute("異動日期")));

                mdws.Cells[mdws_index, 13].PutValue(BL.Util.ConvertDate1(record.GetAttribute("備查日期")));

                mdws.Cells[mdws_index, 14].PutValue(BL.Util.GetDocNo_Doc(record.GetAttribute("備查文號")));
                mdws.Cells[mdws_index, 15].PutValue(BL.Util.GetDocNo_No(record.GetAttribute("備查文號")));
                mdws.Cells[mdws_index, 16].PutValue(record.GetAttribute("雙重學籍編號"));
                mdws.Cells[mdws_index, 17].PutValue(record.GetAttribute("備註"));
            }

            // 資料末底 加End
            mdws.Cells.CreateRange(mdws_index + 1, 1, false).Copy(range_R_EndRow);

            mdws.AutoFitColumns();
            mdws.Cells.SetColumnWidth(5, 8.5);
            mdws.Cells.SetColumnWidth(11, 20);
            
            wb.Worksheets.RemoveAt("延修生名冊封面範本");
            wb.Worksheets.RemoveAt("電子格式範本");
            wb.Worksheets.ActiveSheetIndex = 0;            


            //儲存 Excel
            wb.Save(location, FileFormatType.Excel2003);
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
            get { return "延修生名冊"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }
    }
}
