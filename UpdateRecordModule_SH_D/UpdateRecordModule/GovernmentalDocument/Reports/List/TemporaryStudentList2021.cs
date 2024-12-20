﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Windows.Forms;

namespace UpdateRecordModule_SH_D.GovernmentalDocument.Reports.List
{
    /// <summary>
    /// 借讀學生名冊
    /// </summary>
    public class TemporaryStudentList2021 : ReportBuilder
    {
        public override string Description
        {
            get { return "教育部 國民及學前教育署 編印中華民國 103年7月"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }

        public override string Copyright
        {
            get { return "ischool"; }
        }

        public override string ReportName
        {
            get { return "借讀學生名冊"; }
        }

        
        protected override void Build(System.Xml.XmlElement source, string location)
        {
         #region 建立 Excel

            //從 Resources 將新生名冊template讀出來
            Workbook template = new Workbook();
            template.Open(new MemoryStream(Properties.Resources.TemporaryStudentListTemplate), FileFormatType.Xlsx);

            //產生 excel
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(Properties.Resources.TemporaryStudentListTemplate), FileFormatType.Xlsx);

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


                // 所在地代碼對照
                Dictionary<string, string> gLocationCodeDict = new Dictionary<string, string>();

         
                //將xml資料填入至excel
                foreach (XmlNode st in list.SelectNodes("異動紀錄"))
                {
                    recCount++;

                    if (i % 20 == 0)
                    {
                        #region 複製樣式-欄高、範圍

                        //複製樣版中前28個 Row(欄高)
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
                        int t = j * 28;
                        wb.Worksheets[0].Cells.CreateRange(t, 28, false).Copy(range);
                        wb.Worksheets[0].Cells.CreateRange(t, 28, false).CopyData(range);
                        wb.Worksheets[0].Cells.CreateRange(t, 28, false).CopyStyle(range);
                        #endregion

                        #region 填入學校資料

                        //將學校資料填入適當的位置內
                        wb.Worksheets[0].Cells[rowi, 13].PutValue(source.SelectSingleNode("@學校代號").InnerText);
                        wb.Worksheets[0].Cells[rowi, 16].PutValue(list.SelectSingleNode("@科別代號").InnerText);
                        wb.Worksheets[0].Cells[rowi + 2, 2].PutValue(source.SelectSingleNode("@學校名稱").InnerText);
                        wb.Worksheets[0].Cells[rowi + 2, 6].PutValue(Convert.ToInt32(source.SelectSingleNode("@學年度").InnerText));
                        wb.Worksheets[0].Cells[rowi + 2, 9].PutValue(Convert.ToInt32(source.SelectSingleNode("@學期").InnerText));
                        wb.Worksheets[0].Cells[rowi + 2, 12].PutValue(list.SelectSingleNode("@科別").InnerText);

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
                            wb.Worksheets[0].Cells[(28 * (j - 1)) + 26, 13].PutValue("第" + numcount + "頁，共" + Math.Ceiling((double)count / 20) + "頁");
                        }
                        else
                        {
                            wb.Worksheets[0].Cells[(28 * (j - 1)) + 26, 13].PutValue("第" + numcount + "頁，共" + (Math.Ceiling((double)count / 20) + 1) + "頁");
                        }
                        numcount++;

                        #endregion
                    }

                    #region 填入學生資料

                    //將學生資料填入適當的位置內
                    wb.Worksheets[0].Cells[rowj, 1].PutValue(st.SelectSingleNode("@學號").InnerText);
                    wb.Worksheets[0].Cells[rowj, 3].PutValue(st.SelectSingleNode("@姓名").InnerText);
                    string ss = st.SelectSingleNode("@出生年月日").InnerText + "\n" + st.SelectSingleNode("@身分證號").InnerText;
                    wb.Worksheets[0].Cells[rowj, 4].PutValue(ss);

                    try
                    {
                        wb.Worksheets[0].Cells[rowj, 7].PutValue(Convert.ToInt32(st.SelectSingleNode("@性別代號").InnerText));
                    }
                    catch (Exception)
                    { }
                    wb.Worksheets[0].Cells[rowj, 8].PutValue(st.SelectSingleNode("@性別").InnerText);                    

                    if (st.SelectSingleNode("@特殊身份代碼") != null)
                        wb.Worksheets[0].Cells[rowj, 9].PutValue(st.SelectSingleNode("@特殊身份代碼").InnerText);

                    // 借讀學校代碼
                    string LSCode = "";
                    if (st.SelectSingleNode("@借讀學校代碼") != null)
                        LSCode = st.SelectSingleNode("@借讀學校代碼").InnerText;
                    wb.Worksheets[0].Cells[rowj, 10].PutValue(LSCode);

                    // 借讀科別代碼
                    string LDCode = "";
                    if (st.SelectSingleNode("@借讀科別代碼") != null)
                        LDCode = st.SelectSingleNode("@借讀科別代碼").InnerText;
                    wb.Worksheets[0].Cells[rowj, 11].PutValue(LDCode);

                    
                    string uCode = "";
                    if (st.SelectSingleNode("@異動代號") == null)
                    {
                        if (st.SelectSingleNode("@異動代碼") != null)
                            uCode = st.SelectSingleNode("@異動代碼").InnerText;
                    }
                    else
                        uCode = st.SelectSingleNode("@異動代號").InnerText;

                    wb.Worksheets[0].Cells[rowj, 12].PutValue(uCode);
                    wb.Worksheets[0].Cells[rowj, 13].PutValue(st.SelectSingleNode("@原因及事項").InnerText);
                    
                    //申請日期
                    string sD1 = "";
                    if (st.SelectSingleNode("@申請開始日期") != null)
                        sD1 += ParseCDate1(st.SelectSingleNode("@申請開始日期").InnerText);

                    if (st.SelectSingleNode("@申請結束日期") != null)
                        sD1 += "\n" + ParseCDate1(st.SelectSingleNode("@申請結束日期").InnerText);

                    wb.Worksheets[0].Cells[rowj, 14].PutValue(sD1);

                    //實際日期
                    string sD2 = "";
                    if (st.SelectSingleNode("@實際開始日期") != null)
                        sD2 += ParseCDate1(st.SelectSingleNode("@實際開始日期").InnerText);

                    if (st.SelectSingleNode("@實際結束日期") != null)
                        sD2 += "\n" + ParseCDate1(st.SelectSingleNode("@實際結束日期").InnerText);

                    wb.Worksheets[0].Cells[rowj, 17].PutValue(sD2);

                    wb.Worksheets[0].Cells[rowj, 18].PutValue(st.SelectSingleNode("@備註").InnerText);

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
                    int t = j * 28;
                    wb.Worksheets[0].Cells.CreateRange(t, 28, false).Copy(range);
                    wb.Worksheets[0].Cells.CreateRange(t, 28, false).CopyData(range);
                    wb.Worksheets[0].Cells.CreateRange(t, 28, false).CopyStyle(range);
                    #endregion

                    #region 填入學校資料

                    //將學校資料填入適當的位置內
                    wb.Worksheets[0].Cells[rowi, 13].PutValue(source.SelectSingleNode("@學校代號").InnerText);
                    wb.Worksheets[0].Cells[rowi, 16].PutValue(list.SelectSingleNode("@科別代號").InnerText);
                    wb.Worksheets[0].Cells[rowi + 2, 2].PutValue(source.SelectSingleNode("@學校名稱").InnerText);
                    wb.Worksheets[0].Cells[rowi + 2, 6].PutValue(Convert.ToInt32(source.SelectSingleNode("@學年度").InnerText));
                    wb.Worksheets[0].Cells[rowi + 2, 9].PutValue(Convert.ToInt32(source.SelectSingleNode("@學期").InnerText));
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
                    wb.Worksheets[0].Cells[(28 * (j - 1)) + 26, 13].PutValue("第" + numcount + "頁，共" + (Math.Ceiling((double)count / 20) + 1) + "頁");
                    numcount++;

                    #endregion
                }

                #endregion

                #region 統計人數

                //填入統計人數
                wb.Worksheets[0].Cells[rowj, 1].PutValue("合  計 ");
                wb.Worksheets[0].Cells[rowj, 3].PutValue(count.ToString() + " 名");

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

            #region 97中辦格式

            Worksheet mingdao = wb.Worksheets["電子格式範本"];
            Worksheet mdws = wb.Worksheets[wb.Worksheets.Add()];
            mdws.Name = "借讀學生名冊";

            Range range_header = mingdao.Cells.CreateRange(0, 1, false);
            Range range_row = mingdao.Cells.CreateRange(1, 1, false);
            // 107新格式 結束行要 有End 字樣
            Range range_R_EndRow = mingdao.Cells.CreateRange(2, 1, false);

            mdws.Cells.CreateRange(0, 1, false).CopyData(range_header);
            mdws.Cells.CreateRange(0, 1, false).CopyStyle(range_header);
            int mdws_index = 0;
            foreach (XmlElement record in source.SelectNodes("清單/異動紀錄"))
            {
                mdws_index++;
                mdws.Cells.CreateRange(mdws_index, 1, false).CopyStyle(range_row);

                mdws.Cells[mdws_index, 0].PutValue(record.GetAttribute("班別"));
                mdws.Cells[mdws_index, 1].PutValue((record.ParentNode as XmlElement).GetAttribute("科別代號"));
                mdws.Cells[mdws_index, 2].PutValue(record.GetAttribute("上傳類別"));
                mdws.Cells[mdws_index, 3].PutValue(record.GetAttribute("學號"));
                mdws.Cells[mdws_index, 4].PutValue(record.GetAttribute("姓名"));
                mdws.Cells[mdws_index, 5].PutValue(record.GetAttribute("身分證號"));
                mdws.Cells[mdws_index, 6].PutValue(record.GetAttribute("註1"));
                mdws.Cells[mdws_index, 7].PutValue(record.GetAttribute("性別代號"));
                mdws.Cells[mdws_index, 8].PutValue(GetBirthdateWithoutSlash(record.GetAttribute("出生年月日")));
                mdws.Cells[mdws_index, 9].PutValue(record.GetAttribute("特殊身份代碼"));
                mdws.Cells[mdws_index, 10].PutValue(record.GetAttribute("年級"));
                mdws.Cells[mdws_index, 11].PutValue(record.GetAttribute("借讀學校代碼"));
                mdws.Cells[mdws_index, 12].PutValue(record.GetAttribute("借讀科別代碼"));
                mdws.Cells[mdws_index, 13].PutValue(record.GetAttribute("異動代碼"));
                mdws.Cells[mdws_index, 14].PutValue(ParseCDate2(record.GetAttribute("申請開始日期")));
                mdws.Cells[mdws_index, 15].PutValue(ParseCDate2(record.GetAttribute("申請結束日期")));                
                mdws.Cells[mdws_index, 16].PutValue(record.GetAttribute("備註"));
            }

            // 資料末底 加End
            mdws.Cells.CreateRange(mdws_index + 1, 1, false).CopyData(range_R_EndRow);
            mdws.Cells.CreateRange(mdws_index + 1, 1, false).CopyStyle(range_R_EndRow);
            mdws.AutoFitColumns();
            wb.Worksheets.RemoveAt("電子格式範本");

            //範本
            Worksheet TemplateWb_Cover = wb.Worksheets["借讀學生名冊封面範本"];

            //實做頁面
            Worksheet cover = wb.Worksheets[wb.Worksheets.Add()];

            //名稱
            cover.Name = "借讀學生名冊封面";

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
                string classType = list.SelectSingleNode("@班別").InnerText;
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
                    //string classType = st.SelectSingleNode("@班別").InnerText;
                    string updateType = st.SelectSingleNode("@上傳類別").InnerText;
                    string disasterStudentCount = st.SelectSingleNode("@因災害申請借讀學生數").InnerText;
                    string maladapStudentCount = st.SelectSingleNode("@因適應不良申請借讀學生數").InnerText;
                    string playerTrainingStudentCount = st.SelectSingleNode("@因參加國家代表隊選手培集訓申請借讀學生數").InnerText;
                    string remarksContent = st.SelectSingleNode("@備註說明").InnerText;

                    //名冊別
                    cover.Cells[cover_row_counter, 4].PutValue(reportType);
                    //班別
                    cover.Cells[cover_row_counter, 5].PutValue(classType);
                    //上傳類別
                    cover.Cells[cover_row_counter, 7].PutValue(updateType);
                    //因災害申請借讀學生數
                    cover.Cells[cover_row_counter, 8].PutValue(disasterStudentCount);
                    //因適應不良申請借讀學生數                    
                    cover.Cells[cover_row_counter, 9].PutValue(maladapStudentCount);
                    //因參加國家代表隊選手培集訓申請借讀學生數
                    cover.Cells[cover_row_counter, 10].PutValue(playerTrainingStudentCount);
                    //備註說明
                    cover.Cells[cover_row_counter, 11].PutValue(remarksContent);

                }
                cover_row_counter++;
            }

            // 資料末底 加End
            cover.Cells.CreateRange(cover_row_counter, 1, false).CopyData(range_R_cover_EndRow);
            cover.Cells.CreateRange(cover_row_counter, 1, false).CopyStyle(range_R_cover_EndRow);
            #endregion
            wb.Worksheets.RemoveAt("借讀學生名冊封面範本");
            wb.Worksheets.ActiveSheetIndex = 0;

            //儲存 Excel
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
        private string GetBirthdateWithoutSlash(string orig)
        {
            if (string.IsNullOrEmpty(orig)) return orig;
            string[] array = orig.Split('/');
            return array[0] + array[1].PadLeft(2, '0') + array[2].PadLeft(2, '0');
        }

        /// <summary>
        /// 將西元日期轉成民國 xxx/00/00
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string ParseCDate1(string str)
        {
            string retVal = "";
            DateTime dt;

            if (DateTime.TryParse(str, out dt))
                retVal = string.Format("{0:#00}", (dt.Year - 1911)) + "/" + string.Format("{0:00}", dt.Month) + "/" + string.Format("{0:00}", dt.Day);

            return retVal;
        }

        /// <summary>
        /// 將西元日期轉成民國 xxx0000
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string ParseCDate2(string str)
        {
            string retVal = "";
            DateTime dt;

            if (DateTime.TryParse(str, out dt))
                retVal = string.Format("{0:#00}", (dt.Year - 1911)) + string.Format("{0:00}", dt.Month) + string.Format("{0:00}", dt.Day);

            return retVal;
        }        

    }
}
