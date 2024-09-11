using System.IO;
using System.Xml;
using Aspose.Cells;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UpdateRecordModule_SH_D.GovernmentalDocument.Reports.List
{
    public class TransferringStudentUpdateRecordList2021 : ReportBuilder
    {
       
        protected override void Build(XmlElement source, string location)
        {
            Workbook template = new Workbook();

            //從Resources把Template讀出來
            template.Open(new MemoryStream(Properties.Resources.TransferringStudentUpdateRecordListTemplate), FileFormatType.Xlsx);

            //要產生的excel檔
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(Properties.Resources.TransferringStudentUpdateRecordListTemplate), FileFormatType.Xlsx);

            Worksheet ws = wb.Worksheets[0];

            //頁面間隔幾個row
            int next = 23;

            //頁面有幾個col
            int col = 14;

            //資料row數目
            int dataRow = 16;

            //索引
            int index = 0;

            //範本範圍
            Range tempRange = template.Worksheets[0].Cells.CreateRange(0,23,false);

            //總共幾筆異動紀錄
            int count = 0;
            int totalRec = source.SelectNodes("清單/異動紀錄").Count;

            foreach (XmlNode list in source.SelectNodes("清單"))
            {
                //產生清單第一頁
                ws.Cells.CreateRange(index, next, false).Copy(tempRange);
                ws.Cells.CreateRange(index, next, false).CopyData(tempRange);
                ws.Cells.CreateRange(index, next, false).CopyStyle(tempRange);
                //Page
                int currentPage = 1;
                int totalPage = (list.ChildNodes.Count / dataRow) + 1;


                //寫入代號
                ws.Cells[index, 11].PutValue(source.SelectSingleNode("@學校代號").InnerText + "-" + list.SelectSingleNode("@科別代號").InnerText);

                //寫入校名、學年度、學期、科別、年級
                ws.Cells[index + 2, 1].PutValue(source.SelectSingleNode("@學校名稱").InnerText);
                ws.Cells[index + 2, 5].PutValue(source.SelectSingleNode("@學年度").InnerText + " 學年度 第 " + source.SelectSingleNode("@學期").InnerText + " 學期");
                ws.Cells[index + 2, 8].PutValue(list.SelectSingleNode("@科別").InnerText);
                ws.Cells[index + 2, 12].PutValue(list.SelectSingleNode("@年級").InnerText + "年級");

                //寫入資料
                int recCount = 0;
                int dataIndex = index + 6;
                for (; currentPage <= totalPage; currentPage++)
                {
                    //複製頁面
                    if (currentPage + 1 <= totalPage)
                    {
                        ws.Cells.CreateRange(index + next, next, false).Copy(tempRange);
                        ws.Cells.CreateRange(index + next, next, false).CopyData(tempRange);
                        ws.Cells.CreateRange(index + next, next, false).CopyStyle(tempRange);
                    }

                    //填入資料 (2018/3/6 穎驊註解，list.ChildNodes.Count-1 因為要扣掉一個 異動名冊封面 資料)
                    for (int i = 0; i < dataRow && recCount < list.ChildNodes.Count-1; i++, recCount++)
                    {
                        //MsgBox.Show(i.ToString()+" "+recCount.ToString());
                        XmlNode rec = list.SelectNodes("異動紀錄")[recCount];
                        
                        if(rec.SelectSingleNode("@新學號")!=null)
                        if(string.IsNullOrEmpty(rec.SelectSingleNode("@新學號").InnerText))
                            if(rec.SelectSingleNode("@學號")!=null)
                                ws.Cells[dataIndex, 0].PutValue(rec.SelectSingleNode("@學號").InnerText);
                        else
                            ws.Cells[dataIndex, 0].PutValue(rec.SelectSingleNode("@新學號").InnerText);

                        ws.Cells[dataIndex, 1].PutValue(rec.SelectSingleNode("@姓名").InnerText);
                        ws.Cells[dataIndex, 2].PutValue(rec.SelectSingleNode("@身分證號").InnerText.ToString());
                        ws.Cells[dataIndex, 3].PutValue(rec.SelectSingleNode("@性別代號").InnerText);
                        ws.Cells[dataIndex, 4].PutValue(rec.SelectSingleNode("@性別").InnerText);
                        ws.Cells[dataIndex, 5].PutValue(rec.SelectSingleNode("@出生年月日").InnerText);
                        ws.Cells[dataIndex, 6].PutValue(rec.SelectSingleNode("@轉入前學生資料_學校").InnerText);
                        ws.Cells[dataIndex, 7].PutValue(rec.SelectSingleNode("@轉入前學生資料_學號").InnerText + "\n" + rec.SelectSingleNode("@轉入前學生資料_科別").InnerText);
                        ws.Cells[dataIndex, 8].PutValue(BL.Util.ConvertDateStr2(rec.SelectSingleNode("@轉入前學生資料_備查日期").InnerText) + "\n" + rec.SelectSingleNode("@轉入前學生資料_備查文號").InnerText);
                        ws.Cells[dataIndex, 9].PutValue(rec.SelectSingleNode("@轉入前學生資料_年級").InnerText);
                        ws.Cells[dataIndex, 10].PutValue(rec.SelectSingleNode("@異動代號").InnerText);
                        ws.Cells[dataIndex, 11].PutValue(rec.SelectSingleNode("@原因及事項").InnerText);
                        ws.Cells[dataIndex, 12].PutValue(BL.Util.ConvertDateStr2(rec.SelectSingleNode("@異動日期").InnerText));

                        //ws.Cells[dataIndex, 13].PutValue(rec.SelectSingleNode("@備註").InnerText);
                        if(rec.SelectSingleNode("@特殊身份代碼")!=null)
                            ws.Cells[dataIndex, 13].PutValue(rec.SelectSingleNode("@特殊身份代碼").InnerText);

                        dataIndex++;
                        count++;

                        //轉入前學生資料_學校="糕忠高中" 轉入前學生資料_學號="010101" 轉入前學生資料_科別="資訊科" 轉入前學生資料_備查日期="90/09/09" 轉入前學生資料_備查文號="教中三字第09200909090號" 轉入前學生資料_年級="一上"
                    }

                    //計算合計
                    if (currentPage == totalPage)
                    {
                        ws.Cells.CreateRange(dataIndex, 0, 1, 2).Merge();
                        ws.Cells[dataIndex, 0].PutValue("合計 " + (list.ChildNodes.Count-1).ToString() + " 名");
                    }

                    //分頁
                    ws.Cells[index + next -1, 10].PutValue("第 " + currentPage + " 頁，共 " + totalPage + " 頁");
                    ws.HPageBreaks.Add(index + next, col);

                    //索引指向下一頁
                    index += next;
                    dataIndex = index + 6;

                    //回報進度
                    ReportProgress((int)(((double)count * 100.0) / ((double)totalRec)));
                }
            }


            #region 轉入生,電子格式

            Worksheet TemplateWb = wb.Worksheets["電子格式範本"];

            Worksheet DyWb = wb.Worksheets[wb.Worksheets.Add()];
            DyWb.Name = "轉入生名冊";

            Range range_H = TemplateWb.Cells.CreateRange(0, 1, false);
            Range range_R = TemplateWb.Cells.CreateRange(1, 1, false);

            // 107新格式 結束行要 有End 字樣
            Range range_R_EndRow = TemplateWb.Cells.CreateRange(2, 1, false);
            DyWb.Cells.CreateRange(0, 1, false).CopyData(range_H);
            DyWb.Cells.CreateRange(0, 1, false).CopyStyle(range_H);

            int DyWb_index = 0;
            DAL.DALTransfer DALTranser = new DAL.DALTransfer();

            // 格式轉換
            List<GovernmentalDocument.Reports.List.rpt_UpdateRecord> _data = DALTranser.ConvertRptUpdateRecord(source);

            // 排序 (依 班別、年級、科別代碼、學號)
            _data =(from data in _data orderby data.ClassType,data.GradeYear,data.DeptCode,data.StudentNumber select data).ToList ();

            foreach (GovernmentalDocument.Reports.List.rpt_UpdateRecord rec in _data)
            {
                DyWb_index++;
                //每增加一行,複製一次
                DyWb.Cells.CreateRange(DyWb_index, 1, false).CopyStyle(range_R);

                //班別
                DyWb.Cells[DyWb_index, 0].PutValue(rec.ClassType);
                //科別代碼
                DyWb.Cells[DyWb_index, 1].PutValue(rec.DeptCode);

                //上傳類別
                DyWb.Cells[DyWb_index, 2].PutValue(rec.UpdateType);
                //學號
                if (string.IsNullOrEmpty(rec.NewStudNumber))
                    DyWb.Cells[DyWb_index, 3].PutValue(rec.StudentNumber);
                else
                    DyWb.Cells[DyWb_index, 3].PutValue(rec.NewStudNumber);

                //姓名
                DyWb.Cells[DyWb_index, 4].PutValue(rec.Name);
                //身分證字號
                DyWb.Cells[DyWb_index, 5].PutValue(rec.IDNumber);
                //註1
                DyWb.Cells[DyWb_index, 6].PutValue(rec.Comment1);
                //性別代碼
                DyWb.Cells[DyWb_index, 7].PutValue(rec.GenderCode);
                //出生日期
                DyWb.Cells[DyWb_index, 8].PutValue(rec.Birthday);
                //特殊身份代碼
                DyWb.Cells[DyWb_index, 9].PutValue(rec.SpecialStatusCode);
                //年級
                DyWb.Cells[DyWb_index, 10].PutValue(rec.GradeYear);
                //異動原因代碼
                DyWb.Cells[DyWb_index, 11].PutValue(rec.UpdateCode);
                //轉入日期
                DyWb.Cells[DyWb_index, 12].PutValue(rec.UpdateDate);
                // 轉入身分別
                DyWb.Cells[DyWb_index, 13].PutValue(rec.TransferStatus);

                //原備查日期
                DyWb.Cells[DyWb_index, 14].PutValue(rec.PreviousSchoolLastADDate);
                //原備查文字(*)
                DyWb.Cells[DyWb_index, 15].PutValue(rec.PreviousSchoolLastADDoc);
                //原備查文號(*)
                DyWb.Cells[DyWb_index, 16].PutValue(rec.PreviousSchoolLastADNum);
                //原學校代碼(*)
                DyWb.Cells[DyWb_index, 17].PutValue(rec.PreviousSchoolCode);
                //原科別代碼
                DyWb.Cells[DyWb_index, 18].PutValue(rec.PreviousDeptCode);
                //原學號
                DyWb.Cells[DyWb_index, 19].PutValue(rec.PreviousStudentNumber);
                
                // 為支援舊結構年級與學期是用文字字串一上，所以這樣寫
                //原年級
                DyWb.Cells[DyWb_index, 20].PutValue(Getyear(rec.PreviousGradeYear));
                //原學期
                DyWb.Cells[DyWb_index, 21].PutValue(Getsemester(rec.PreviousSemester));

                //建教僑生專班學生國別
                DyWb.Cells[DyWb_index, 22].PutValue(rec.OverseasChineseStudentCountryCode);

                //備註說明
                DyWb.Cells[DyWb_index, 23].PutValue(rec.Comment);            
            }

            // 資料末底 加End
            DyWb.Cells.CreateRange(DyWb_index + 1, 1, false).CopyData(range_R_EndRow);
            DyWb.Cells.CreateRange(DyWb_index + 1, 1, false).CopyStyle(range_R_EndRow);
            DyWb.AutoFitColumns();

            wb.Worksheets.RemoveAt("電子格式範本");

            #endregion

            //2018/3/6 穎驊 新增轉入生 封面格式支援

            //範本
            Worksheet TemplateWb_Cover = wb.Worksheets["轉入生名冊封面範本"];

            //實做頁面
            Worksheet cover = wb.Worksheets[wb.Worksheets.Add()];

            //名稱
            cover.Name = "轉入生名冊封面";
            
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
                    string approvedClassCount = st.SelectSingleNode("@核定班數").InnerText;
                    string approvedStudentCount = st.SelectSingleNode("@核定學生數").InnerText;
                    string actualClassCount = st.SelectSingleNode("@實招班數").InnerText;
                    string actualStudentCount = st.SelectSingleNode("@實招新生數").InnerText;
                    string originalStudentCount = st.SelectSingleNode("@原有學生數").InnerText;
                    string transferStudentCount = st.SelectSingleNode("@轉入學生數").InnerText;                    
                    string currentStudentCount = st.SelectSingleNode("@現有學生數").InnerText;
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
                    //原有學生數
                    cover.Cells[cover_row_counter, 12].PutValue(originalStudentCount);
                    //轉入學生數
                    cover.Cells[cover_row_counter, 13].PutValue(transferStudentCount);                    
                    //現有學生數
                    cover.Cells[cover_row_counter, 14].PutValue(currentStudentCount);
                    //註1
                    cover.Cells[cover_row_counter, 15].PutValue(remarks1);
                    //備註說明
                    cover.Cells[cover_row_counter, 16].PutValue(remarksContent);
                }
                cover_row_counter++;
            }

            // 資料末底 加End
            cover.Cells.CreateRange(cover_row_counter, 1, false).CopyStyle(range_R_cover_EndRow);
            cover.Cells.CreateRange(cover_row_counter, 1, false).CopyData(range_R_cover_EndRow);

            wb.Worksheets.RemoveAt("轉入生名冊封面範本");

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


        #region 切切切

        //切年級
        #region 切年級

        private string Getyear(string year)
        {

            if (year.Contains("一"))
            {
                return "1";
            }
            else if (year.Contains("二"))
            {
                return "2";
            }
            else if (year.Contains("三"))
            {
                return "3";
            }
            else if (year.Contains("四"))
            {
                return "4";
            }
            else
            {
                return year;
            }

        }

        #endregion


        //切學期
        #region 切學期

        private string Getsemester(string sem)
        {

            if (sem.Contains("上"))
            {
                return "1";
            }
            else if (sem.Contains("下"))
            {
                return "2";
            }
            else
            {
                return sem;
            }
        }

        #endregion


        //切文字
        #region 切文字

        private string GetNumAndSrt1(string fuct)
        {
            if (fuct.Contains("字"))
            {
                return fuct.Remove(fuct.LastIndexOf("字"));
            }
            return fuct;
        }

        #endregion

        //切文號
        #region 切文號

        private string GetNumAndSrt2(string fuct)
        {

            if (fuct.Contains("第") && fuct.Contains("號"))
            {
                return fuct.Substring(fuct.LastIndexOf("第") + 1, fuct.LastIndexOf("號") - fuct.LastIndexOf("第") - 1);
            }
            return fuct;

        }

        #endregion

        //西元轉民國年
        #region 西元轉民國年
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
        #endregion 

        #endregion



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
            get { return "轉入學生名冊"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }
    }
}
