using System;
using System.IO;
using System.Xml;
using Aspose.Cells;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Windows.Forms;

namespace UpdateRecordModule_SH_D.GovernmentalDocument.Reports.List
{
    public class EnrollmentList2021 : ReportBuilder
    {
        
        protected override void Build(System.Xml.XmlElement source, string location)
        {   
            #region 建立 Excel

            //從 Resources 將新生名冊template讀出來
            Workbook template = new Workbook();
            template.Open(new MemoryStream(Properties.Resources.EnrollmentListTemplate),FileFormatType.Xlsx);

            //產生 excel
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(Properties.Resources.EnrollmentListTemplate), FileFormatType.Xlsx);
            
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

                foreach (XElement elm in BL.Get.JHSchoolList().Elements("學校"))
                {
                    string code=elm.Attribute("所在地代碼").Value;
                   if(!gLocationCodeDict.ContainsKey(code))
                       gLocationCodeDict.Add(code,elm.Attribute("所在地").Value);
                }

                //將xml資料填入至excel
                foreach (XmlNode st in list.SelectNodes("異動紀錄"))
                {
                    recCount++;

                    if (i % 20 == 0)
                    {
                        #region 複製樣式-欄高、範圍

                        //複製樣版中前27個 Row(欄高)
                        //for (int m = 0; m < 27; m++)
                        //{
                        //    /*
                        //     * 複製 template的第一個 Sheet的第m個 Row
                        //     * 到 wb的第一個 Sheet的第(j * 27) + m個 Row
                        //     */
                        //    wb.Worksheets[0].Cells.CopyRow(template.Worksheets[0].Cells, m, (j * 27) + m);
                        //}

                        /*
                         * 複製Style(包含儲存格合併的資訊)
                         * 先用CreateRange()選取要複製的Range("A1", "R27")
                         * 再用CopyStyle複製另一個Range中的格式
                         */
                        Range range = template.Worksheets[0].Cells.CreateRange(0, 27, false);
                        int t = j * 27;
                        wb.Worksheets[0].Cells.CreateRange(t, 27, false).Copy(range);
                        wb.Worksheets[0].Cells.CreateRange(t, 27, false).CopyData(range);
                        wb.Worksheets[0].Cells.CreateRange(t, 27, false).CopyStyle(range);

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
                            //插入分頁(在 j * 27 跟 (j * 27) +1 中間，R跟S中間)
                            wb.Worksheets[0].HPageBreaks.Add(j * 27, 18);
                            rowj += 7;
                        }
                        else
                        {
                            rowj = 5;
                        }

                        rowi += 27;
                        j++;

                        #region 顯示頁數

                        //顯示頁數
                        if (x != true)
                        {
                            wb.Worksheets[0].Cells[(27 * (j - 1)) + 26, 13].PutValue("第" + numcount + "頁，共" + Math.Ceiling((double)count / 20) + "頁");
                        }
                        else
                        {
                            wb.Worksheets[0].Cells[(27 * (j - 1)) + 26, 13].PutValue("第" + numcount + "頁，共" + (Math.Ceiling((double)count / 20) + 1) + "頁");
                        }
                        numcount++;

                        #endregion
                    }

                    #region 填入學生資料
                    
                        //將學生資料填入適當的位置內
                        wb.Worksheets[0].Cells[rowj, 1].PutValue(st.SelectSingleNode("@學號").InnerText);
                        wb.Worksheets[0].Cells[rowj, 3].PutValue(st.SelectSingleNode("@姓名").InnerText);
                        wb.Worksheets[0].Cells[rowj, 5].PutValue(st.SelectSingleNode("@身分證號").InnerText);
                        
                        try
                        {
                            wb.Worksheets[0].Cells[rowj, 6].PutValue(Convert.ToInt32(st.SelectSingleNode("@性別代號").InnerText));
                        }
                        catch (Exception)
                        {}
                        wb.Worksheets[0].Cells[rowj, 7].PutValue(st.SelectSingleNode("@性別").InnerText);
                        wb.Worksheets[0].Cells[rowj, 8].PutValue(st.SelectSingleNode("@出生年月日").InnerText);

                        string stra1 = "", stra2 = "";
                        if (st.SelectSingleNode("@畢業國中所在縣市代號") != null)
                            stra1 = st.SelectSingleNode("@畢業國中所在縣市代號").InnerText;

                        if (st.SelectSingleNode("@入學資格代號") != null)
                            stra2 = st.SelectSingleNode("@入學資格代號").InnerText;

                        wb.Worksheets[0].Cells[rowj, 11].PutValue(stra1 + "\n" + stra2);

                        string data = "", data1 = "";
                        string uCode = "";
                        if (st.SelectSingleNode("@異動代號") == null)
                        {
                            if (st.SelectSingleNode("@異動代碼") != null)
                                uCode = st.SelectSingleNode("@異動代碼").InnerText;
                        }
                        else
                            uCode = st.SelectSingleNode("@異動代號").InnerText;

                        if (uCode == "001")
                                data1 = "畢業";

                        if (uCode == "003")
                                data1 = "結業";
                        if (uCode == "004")
                                data1 = "修滿";
                    
                        if(st.SelectSingleNode("@畢業國中所在縣市代號")!=null)
                        if (!string.IsNullOrEmpty(st.SelectSingleNode("@畢業國中所在縣市代號").InnerText))
                        {
                            string code=st.SelectSingleNode("@畢業國中所在縣市代號").InnerText;                            
                            
                            if (gLocationCodeDict.ContainsKey(code))
                            {
                                data = gLocationCodeDict[code];
                            }                           
                        
                        }
                        
                            wb.Worksheets[0].Cells[rowj, 12].PutValue(data+st.SelectSingleNode("@畢業國中").InnerText+data1);
                        //wb.Worksheets[0].Cells[rowj, 14].PutValue(st.SelectSingleNode("@備註").InnerText);
                    if(st.SelectSingleNode("@特殊身份代碼")!=null)
                            wb.Worksheets[0].Cells[rowj, 14].PutValue(st.SelectSingleNode("@特殊身份代碼").InnerText);
                    
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

                    //複製樣版前27個 Row(欄高)
                    //for (int m = 0; m < 27; m++)
                    //{
                    //    /*
                    //     * 複製 template的第一個 Sheet的第m個 Row
                    //     * 到 wb的第一個 Sheet的第(j * 27) + m個 Row
                    //     */
                    //    wb.Worksheets[0].Cells.CopyRow(template.Worksheets[0].Cells, m, (j * 27) + m);
                    //}

                    /*
                     * 複製Style(包含儲存格合併的資訊)
                     * 先用CreateRange()選取要複製的Range("A1", "R27")
                     * 再用CopyStyle複製另一個Range中的格式
                     */
                    Range range = template.Worksheets[0].Cells.CreateRange(0, 27, false);
                    int t= j * 27;
                    wb.Worksheets[0].Cells.CreateRange(t, 27, false).Copy(range);
                    wb.Worksheets[0].Cells.CreateRange(t, 27, false).CopyData(range);
                    wb.Worksheets[0].Cells.CreateRange(t, 27, false).CopyStyle(range);
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
                        wb.Worksheets[0].HPageBreaks.Add(j * 27, 18);
                        rowj += 7;
                    }

                    rowi += 27;
                    j++;

                    #region 顯示頁數

                    //顯示頁數
                    wb.Worksheets[0].Cells[(27 * (j - 1)) + 26, 13].PutValue("第" + numcount + "頁，共" + (Math.Ceiling((double)count / 20) + 1) + "頁");
                    numcount++;

                    #endregion
                } 

                #endregion

                #region 統計人數

                //填入統計人數
                wb.Worksheets[0].Cells[rowj, 1].PutValue("合  計 ");
                wb.Worksheets[0].Cells[rowj, 3].PutValue(count.ToString() + " 名"); 

                #endregion

                wb.Worksheets[0].HPageBreaks.Add(j * 27, 18);

                #region 設定變數

                //調整新清單所使用變數
                numcount = 1;
                rowj = (27 * j) - 2;
                rowi = (27 * j);
                x = false; 

                #endregion
            }

            #region 97中辦格式

            Worksheet mingdao = template.Worksheets["電子格式"];
            Worksheet mdws = wb.Worksheets[wb.Worksheets.Add()];
            mdws.Name = "新生名冊";

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
                mdws.Cells[mdws_index, 9].PutValue(record.GetAttribute("特殊身份代碼")); //原為抓取註備欄位值
                mdws.Cells[mdws_index, 10].PutValue(record.GetAttribute("入學資格代號"));
                //mdws.Cells[mdws_index, 11].PutValue(record.GetAttribute("畢業國中所在縣市代號"));
                string GradeSchoolCode = record.GetAttribute("畢業國中代碼");
                //if (GradeSchoolCode.Length > 3)                
                //    mdws.Cells[mdws_index, 12].PutValue(GradeSchoolCode.Substring(2, 1));

            //    mdws.Cells[mdws_index, 13].PutValue(record.GetAttribute("畢業國中"));
                mdws.Cells[mdws_index, 11].PutValue(GradeSchoolCode);
                mdws.Cells[mdws_index, 12].PutValue(record.GetAttribute("入學資格證明文件"));
                mdws.Cells[mdws_index, 13].PutValue(record.GetAttribute("建教僑生專班學生國別"));
                mdws.Cells[mdws_index, 14].PutValue(record.GetAttribute("備註"));
            }

            // 資料末底 加End
            mdws.Cells.CreateRange(mdws_index + 1, 1, false).CopyData(range_R_EndRow);
            mdws.Cells.CreateRange(mdws_index + 1, 1, false).CopyStyle(range_R_EndRow);

            mdws.AutoFitColumns();
            mdws.Cells.SetColumnWidth(5, 8.5);
            //mdws.Cells.SetColumnWidth(11, 20);

            wb.Worksheets.RemoveAt("電子格式");

            //範本
            Worksheet TemplateWb_Cover = wb.Worksheets["新生名冊封面範本"];

            //實做頁面
            Worksheet cover = wb.Worksheets[wb.Worksheets.Add()];

            //名稱
            cover.Name = "新生名冊封面";

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
                string classType = list.SelectSingleNode("@班別") != null ? list.SelectSingleNode("@班別").InnerText : "";


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
                   // string classType = st.SelectSingleNode("@班別").InnerText;
                    string updateType = st.SelectSingleNode("@上傳類別").InnerText;
                    string approvedClassCount = st.SelectSingleNode("@核定班數").InnerText;
                    string approvedStudentCount = st.SelectSingleNode("@核定學生數").InnerText;
                    string actualClassCount = st.SelectSingleNode("@實招班數").InnerText;
                    string actualStudentCount = st.SelectSingleNode("@實招新生數").InnerText;
                    string remarks1 = st.SelectSingleNode("@註1").InnerText;
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
                    //註1
                    cover.Cells[cover_row_counter, 12].PutValue(remarks1);
                    //備註說明
                    cover.Cells[cover_row_counter, 13].PutValue(remarksContent);

                }
                cover_row_counter++;
            }

            // 資料末底 加End
            cover.Cells.CreateRange(cover_row_counter, 1, false).CopyData(range_R_cover_EndRow);
            cover.Cells.CreateRange(cover_row_counter, 1, false).CopyStyle(range_R_cover_EndRow);
            wb.Worksheets.RemoveAt("新生名冊封面範本");
            //範本
            Worksheet TemplateWb_Static = wb.Worksheets["新生人數統計表範本"];

            //實做頁面
            Worksheet StaticSheet = wb.Worksheets[wb.Worksheets.Add()];

            //名稱
            StaticSheet.Name = "新生人數統計表";

          

            //範圍
            range_H_Cover = TemplateWb_Static.Cells.CreateRange(0, 1, false);

            //range_H_Cover
            StaticSheet.Cells.CreateRange(0, 1, false).CopyData(range_H_Cover);
            StaticSheet.Cells.CreateRange(0, 1, false).CopyStyle(range_H_Cover);

            Range range_R_StaticSheet = TemplateWb_Static.Cells.CreateRange(1, 1, false);
            // 107新格式 結束行要 有End 字樣
            Range range_R_StaticSheet_EndRow = TemplateWb_Static.Cells.CreateRange(2, 1, false);

            cover_row_counter = 1;

            //統計表(特殊入學人數無法統計待續....

            foreach (XmlNode list in source.SelectNodes("清單"))
            {
                //每增加一行,複製一次
                StaticSheet.Cells.CreateRange(cover_row_counter, 1, false).CopyStyle(range_R_StaticSheet);

               
                string deptCode = list.SelectSingleNode("@科別代碼").InnerText;
                string classType = list.SelectSingleNode("@班別") != null ? list.SelectSingleNode("@班別").InnerText : "";


               

                foreach (XmlElement st in list.SelectNodes("異動名冊封面"))
                {
                   
                    string updateType = st.SelectSingleNode("@上傳類別").InnerText;
                    string actualStudentCount = st.SelectSingleNode("@實招新生數").InnerText;
                    string approvedClassCount = st.SelectSingleNode("@核定班數").InnerText;
                    string approvedStudentCount = st.SelectSingleNode("@核定學生數").InnerText;
                    long temp = 0;
                    string School_Type = "";
                    if (long.TryParse(school_code,out temp))                        
                        School_Type = "日間部";
                    else
                        School_Type = "夜間部";
                    string extraStudent1 = st.SelectSingleNode("@外加錄取原住民") != null ? st.SelectSingleNode("@外加錄取原住民").InnerText:"0";
                    string extraStudent2 = st.SelectSingleNode("@外加錄取身心障礙生") != null ? st.SelectSingleNode("@外加錄取身心障礙生").InnerText:"0";
                    string extraStudent3 = st.SelectSingleNode("@外加錄取其他") != null ? st.SelectSingleNode("@外加錄取其他").InnerText : "0";
                    string extraStudent4 = st.SelectSingleNode("@建教班僑生數") != null ? st.SelectSingleNode("@建教班僑生數").InnerText : "0";
                    
                    int in_temp = 0;
                    int extraStudents = 0;        // 外加學生總數
                    int overEnrollment = 0;      // 超收學生數
                    
                    // 計算外加學生總數
                    if (int.TryParse(extraStudent1, out in_temp))
                        extraStudents += in_temp;
                    if (int.TryParse(extraStudent2, out in_temp))
                        extraStudents += in_temp;
                    if (int.TryParse(extraStudent3, out in_temp))
                        extraStudents += in_temp;
                    
                    // 直接計算超收學生數（使用正確公式）
                    if (int.TryParse(actualStudentCount, out in_temp) && 
                        int.TryParse(approvedClassCount, out int approvedClass) && 
                        int.TryParse(approvedStudentCount, out int approvedStudent))
                    {
                        // 正確公式：實招新生數 - 外加錄取原住民 - 外加錄取身心障礙生 - 外加錄取其他 - 核定班數 × 核定學生數
                        overEnrollment = in_temp - extraStudents - (approvedClass * approvedStudent);
                    }
                    
                    // 確保超收數不為負數
                    if (overEnrollment < 0)
                        overEnrollment = 0;
                    //班別
                    StaticSheet.Cells[cover_row_counter, 0].PutValue(classType);
                    //日夜部
                    StaticSheet.Cells[cover_row_counter, 1].PutValue(School_Type);
                    //科別代碼
                    StaticSheet.Cells[cover_row_counter, 2].PutValue(deptCode);
                    //上傳類別
                    StaticSheet.Cells[cover_row_counter, 3].PutValue(updateType);

                    //外加錄取原住民
                    StaticSheet.Cells[cover_row_counter, 5].PutValue(extraStudent1);
                    //外加錄取身心障礙生
                    StaticSheet.Cells[cover_row_counter, 6].PutValue(extraStudent2);
                    //外加錄取其他
                    StaticSheet.Cells[cover_row_counter, 7].PutValue(extraStudent3);
                    //超收學生數
                    StaticSheet.Cells[cover_row_counter, 8].PutValue(overEnrollment);
                    //建教班僑生數
                    StaticSheet.Cells[cover_row_counter, 10].PutValue(extraStudent4);


                }
                cover_row_counter++;
            }

            // 資料末底 加End
            StaticSheet.Cells.CreateRange(cover_row_counter, 1, false).CopyData(range_R_StaticSheet_EndRow);
            StaticSheet.Cells.CreateRange(cover_row_counter, 1, false).CopyStyle(range_R_StaticSheet_EndRow);
            wb.Worksheets.RemoveAt("新生人數統計表範本");

            #endregion

            wb.Worksheets.ActiveSheetIndex = 0;

            //儲存 Excel
            try
            {
                wb.Save(location,SaveFormat.Xlsx);
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
            get { return "新生名冊"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }
    }
}
