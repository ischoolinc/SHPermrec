using Aspose.Cells;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using UpdateRecordModule_SH_D.BL;
using System.Windows.Forms;
namespace UpdateRecordModule_SH_D.GovernmentalDocument.Reports.List
{
    public class ExtendingStudentGraduateList2021 : ReportBuilder
    {
        
        protected override void Build(XmlElement source, string location)
        {
            Workbook template = new Workbook();

            //從Resources把Template讀出來
            template.Open(new MemoryStream(Properties.Resources.ExtendingGraduatingStudentListTemplate), FileFormatType.Xlsx);

            //要產生的excel檔
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(Properties.Resources.ExtendingGraduatingStudentListTemplate), FileFormatType.Xlsx);

            Worksheet ws = wb.Worksheets[0];

            //頁面間隔幾個row
            int next = 24;

            //索引
            int index = 0;

            //範本範圍
            Range tempRange = template.Worksheets[0].Cells.CreateRange(0, 24, false);

            //總共幾筆異動紀錄
            int count = 0;
            int totalRec = source.SelectNodes("清單/異動紀錄").Count;

            // 取得名冊內存的最後異動代碼對照
            Dictionary<string, string> LastCodeDict = new Dictionary<string, string>();



            foreach (XmlNode list in source.SelectNodes("清單"))
            {
                //產生清單第一頁
                //for (int row = 0; row < next; row++)
                //{
                //    ws.Cells.CopyRow(template.Worksheets[0].Cells, row, row + index);
                //}
                ws.Cells.CreateRange(index, 24, false).Copy(tempRange);
                ws.Cells.CreateRange(index, 24, false).CopyData(tempRange);
                ws.Cells.CreateRange(index, 24, false).CopyStyle(tempRange);
                //Page
                int currentPage = 1;
                int totalPage = (list.ChildNodes.Count / 18) + 1;

                //寫入名冊類別
                if (source.SelectSingleNode("@類別").InnerText == "延修生畢業名冊"||source.SelectSingleNode("@類別").InnerText == "延修生畢業名冊_2021版")
                    ws.Cells[index, 0].PutValue(ws.Cells[index, 0].StringValue.Replace("□畢業", "■畢業"));
                else
                    ws.Cells[index, 0].PutValue(ws.Cells[index, 0].StringValue.Replace("□結業", "■結業"));

                //寫入代號
                ws.Cells[index, 6].PutValue("代碼：" + source.SelectSingleNode("@學校代號").InnerText + "-" + list.SelectSingleNode("@科別代號").InnerText);

                //寫入校名、學年度、學期、科別
                ws.Cells[index + 2, 0].PutValue("校名：" + source.SelectSingleNode("@學校名稱").InnerText);
                ws.Cells[index + 2, 4].PutValue(source.SelectSingleNode("@學年度").InnerText + "學年度 第" + source.SelectSingleNode("@學期").InnerText + "學期");
                ws.Cells[index + 2, 6].PutValue(list.SelectSingleNode("@科別").InnerText);

                //寫入資料
                int recCount = 0;
                int dataIndex = index + 5;
                for (; currentPage <= totalPage; currentPage++)
                {
                    //複製頁面
                    if (currentPage + 1 <= totalPage)
                    {
                        //for (int row = 0; row < next; row++)
                        //{
                        //    ws.Cells.CopyRow(ws.Cells, row + index, row + index + next);
                        //}
                        ws.Cells.CreateRange(index + next, 24, false).Copy(tempRange);
                        ws.Cells.CreateRange(index + next, 24, false).CopyData(tempRange);
                        ws.Cells.CreateRange(index + next, 24, false).CopyStyle(tempRange);
                    }

                    int updateCount = list.SelectNodes("異動紀錄").Count;

                    //填入資料
                    for (int i = 0; i < 18 && recCount < updateCount; i++, recCount++)
                    {
                        //MsgBox.Show(i.ToString()+" "+recCount.ToString());
                        XmlNode rec = list.SelectNodes("異動紀錄")[recCount];
                        ws.Cells[dataIndex, 0].PutValue(rec.SelectSingleNode("@學號").InnerText + "\n" + rec.SelectSingleNode("@姓名").InnerText);
                        ws.Cells[dataIndex, 1].PutValue(rec.SelectSingleNode("@性別代號").InnerText.ToString());
                        ws.Cells[dataIndex, 2].PutValue(rec.SelectSingleNode("@性別").InnerText);
                        string ssn = rec.SelectSingleNode("@身分證號").InnerText;
                        if (ssn == "")
                            ssn = rec.SelectSingleNode("@身份證號").InnerText;

                        if (!LastCodeDict.ContainsKey(ssn))
                            LastCodeDict.Add(ssn, rec.SelectSingleNode("@最後異動代號").InnerText.ToString());

                        ws.Cells[dataIndex, 3].PutValue(Util.ConvertDateStr2(rec.SelectSingleNode("@生日").InnerText) + "\n" + ssn);
                        ws.Cells[dataIndex, 4].PutValue(rec.SelectSingleNode("@最後異動代號").InnerText.ToString());
                        //if (rec.SelectSingleNode("@原臨編字號").InnerText == "")
                        //{
                            ws.Cells[dataIndex, 5].PutValue(Util.ConvertDateStr2(rec.SelectSingleNode("@備查日期").InnerText) + "\n" + rec.SelectSingleNode("@備查文號").InnerText);
                        //}
                        //else
                        //{
                        //    ws.Cells[dataIndex, 5].PutValue(Util.ConvertDateStr2(rec.SelectSingleNode("@原臨編`日期").InnerText) + "\n" + rec.SelectSingleNode("@原臨編學統").InnerText + rec.SelectSingleNode("@原臨編字號").InnerText);
                        //}
                        ws.Cells[dataIndex, 6].PutValue(rec.SelectSingleNode("@畢業證書字號").InnerText);

                        //ws.Cells[dataIndex, 7].PutValue(rec.SelectSingleNode("@備註").InnerText);
                        if (rec.SelectSingleNode("@特殊身份代碼") != null)
                            ws.Cells[dataIndex, 7].PutValue(rec.SelectSingleNode("@特殊身份代碼").InnerText);

                        dataIndex++;
                        count++;
                    }

                    //計算合計
                    if (currentPage == totalPage)
                    {
                        ws.Cells[index + 22, 0].PutValue("合計");
                        ws.Cells[index + 22, 1].PutValue(updateCount.ToString());
                    }

                    //分頁
                    ws.Cells[index + 23, 6].PutValue("第 " + currentPage + " 頁，共 " + totalPage + " 頁");
                    ws.HPageBreaks.Add(index + 24, 8);

                    //索引指向下一頁
                    index += next;
                    dataIndex = index + 5;

                    //回報進度
                    ReportProgress((int)(((double)count * 100.0) / ((double)totalRec)));
                }
            }

            //範本
            //範本
            Worksheet TemplateWb_Cover = wb.Worksheets["延修生畢業名冊封面範本"];

            //實做頁面
            Worksheet cover = wb.Worksheets[wb.Worksheets.Add()];

            //名稱
            cover.Name = "延修生畢業名冊封面";

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

                //科別代碼
                cover.Cells[cover_row_counter, 6].PutValue(deptCode);

                foreach (XmlElement st in list.SelectNodes("異動名冊封面"))
                {
                    string reportType = st.SelectSingleNode("@名冊別").InnerText;
                    string scheduledGraduateYear = st.SelectSingleNode("@應畢業學年度") != null ? st.SelectSingleNode("@應畢業學年度").InnerText : "";
                    //string classType = st.SelectSingleNode("@班別").InnerText;
                    string updateType = st.SelectSingleNode("@上傳類別").InnerText;
                    string approvedExtendingStudentCount = st.SelectSingleNode("@輔導延修學生數").InnerText;
                    string waitingExtendingStudentCount = st.SelectSingleNode("@未申請延修學生數").InnerText;
                    string originalStudentCount = st.SelectSingleNode("@原有學生數").InnerText;
                    string currentStudentCount = st.SelectSingleNode("@現有學生數").InnerText;
                    string graduatedStudentCount = st.SelectSingleNode("@畢業學生數").InnerText;
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
                    //現有學生數
                    cover.Cells[cover_row_counter, 11].PutValue(currentStudentCount);
                    //畢業學生數
                    cover.Cells[cover_row_counter, 12].PutValue(graduatedStudentCount);
                    //備註說明
                    cover.Cells[cover_row_counter, 13].PutValue(remarksContent);

                }
                cover_row_counter++;
            }

            // 資料末底 加End
            cover.Cells.CreateRange(cover_row_counter, 1, false).CopyData(range_R_cover_EndRow);
            cover.Cells.CreateRange(cover_row_counter, 1, false).CopyStyle(range_R_cover_EndRow);
            //範本
            Worksheet TemplateWb = wb.Worksheets["電子格式範本"];
            //實做頁面
            Worksheet mdws = wb.Worksheets[wb.Worksheets.Add()];
            //名稱
            mdws.Name = "延修生畢業名冊";
            //範圍
            Range range_H = TemplateWb.Cells.CreateRange(0, 1, false);
            Range range_R = TemplateWb.Cells.CreateRange(1, 1, false);
            // 107新格式 結束行要 有End 字樣
            Range range_R_EndRow = TemplateWb.Cells.CreateRange(2, 1, false);
            //拷貝range_H
            mdws.Cells.CreateRange(0, 1, false).CopyData(range_H);
            mdws.Cells.CreateRange(0, 1, false).CopyStyle(range_H);

            int mdws_index = 0;

            DAL.DALTransfer DALTranser = new DAL.DALTransfer();

            // 格式轉換
            List<GovernmentalDocument.Reports.List.rpt_UpdateRecord> _data = DALTranser.ConvertRptUpdateRecord(source);

            // 排序 (依 班別、年級、科別代碼、異動代碼、畢業證書字號)
            _data = (from data in _data orderby data.ClassType, data.DeptCode, data.UpdateCode,data.GraduateCertificateNumber select data).ToList();

            foreach (GovernmentalDocument.Reports.List.rpt_UpdateRecord rec in _data)
            {
                mdws_index++;
                //每增加一行,複製一次
                mdws.Cells.CreateRange(mdws_index, 1, false).CopyStyle(range_R);
                
                //應畢業學年度
                mdws.Cells[mdws_index, 0].PutValue(rec.ExpectGraduateSchoolYear);

                //班別
                mdws.Cells[mdws_index, 1].PutValue(rec.ClassType);
                //科別代碼
                mdws.Cells[mdws_index, 2].PutValue(rec.DeptCode);

                // 2 放上傳類別
                //上傳類別
                mdws.Cells[mdws_index, 3].PutValue(rec.UpdateType);

                //學號
                mdws.Cells[mdws_index, 4].PutValue(rec.StudentNumber);
                //姓名
                mdws.Cells[mdws_index, 5].PutValue(rec.Name);
                //身分證字號
                mdws.Cells[mdws_index, 6].PutValue(rec.IDNumber);

                //註1
                mdws.Cells[mdws_index, 7].PutValue(rec.Comment1);

                //性別代碼
                mdws.Cells[mdws_index, 8].PutValue(rec.GenderCode);
                //出生日期
                mdws.Cells[mdws_index, 9].PutValue(rec.Birthday);

                //特殊身份代碼
                mdws.Cells[mdws_index, 10].PutValue(rec.SpecialStatusCode);

                //異動原因代碼
                if (LastCodeDict.ContainsKey(rec.IDNumber))
                    mdws.Cells[mdws_index, 11].PutValue(LastCodeDict[rec.IDNumber]);
                else
                    mdws.Cells[mdws_index, 11].PutValue(rec.UpdateCode);

                //if (rec.temp_number == "")
                //{
                    //備查文字
                    mdws.Cells[mdws_index, 12].PutValue(rec.LastADDoc);
                    //備查文號
                    mdws.Cells[mdws_index, 13].PutValue(rec.LastADNum);

                    //備查日期
                    mdws.Cells[mdws_index, 14].PutValue(rec.LastADDate);
                //}
                //else
                //{
                //    //臨編學統
                //    mdws.Cells[mdws_index, 12].PutValue(rec.origin_temp_desc);
                //    //臨編學統文號
                //    mdws.Cells[mdws_index, 13].PutValue(rec.origin_temp_number);

                //    //臨編學統日期
                //    mdws.Cells[mdws_index, 14].PutValue(rec.origin_temp_date);
                //};

                //畢業證書字號
                mdws.Cells[mdws_index, 15].PutValue(rec.GraduateCertificateNumber);

                //畢業證書註記學程代碼 (2019/02/15 穎驊 檢查發現 目前我們系統沒有支援這個概念，要再研究)
                mdws.Cells[mdws_index, 16].PutValue("");

                //備註說明
                mdws.Cells[mdws_index, 17].PutValue(rec.Comment);

            }

            // 資料末底 加End
            mdws.Cells.CreateRange(mdws_index + 1, 1, false).CopyData(range_R_EndRow);
            mdws.Cells.CreateRange(mdws_index + 1, 1, false).CopyStyle(range_R_EndRow);
            //範本
            TemplateWb = wb.Worksheets["電子格式範本_含臨編"];
            //實做頁面
            mdws = wb.Worksheets[wb.Worksheets.Add()];
            //名稱
            mdws.Name = "延修生畢業名冊_含臨編";
            //範圍
            range_H = TemplateWb.Cells.CreateRange(0, 1, false);
            range_R = TemplateWb.Cells.CreateRange(1, 1, false);
            // 107新格式 結束行要 有End 字樣
            range_R_EndRow = TemplateWb.Cells.CreateRange(2, 1, false);
            //拷貝range_H
            mdws.Cells.CreateRange(0, 1, false).CopyData(range_H);
            mdws.Cells.CreateRange(0, 1, false).CopyStyle(range_H);
            mdws_index = 0;

           

            foreach (GovernmentalDocument.Reports.List.rpt_UpdateRecord rec in _data)
            {
                mdws_index++;
                //每增加一行,複製一次
                mdws.Cells.CreateRange(mdws_index, 1, false).CopyStyle(range_R);

                //應畢業學年度
                mdws.Cells[mdws_index, 0].PutValue(rec.ExpectGraduateSchoolYear);

                //班別
                mdws.Cells[mdws_index, 1].PutValue(rec.ClassType);
                //科別代碼
                mdws.Cells[mdws_index, 2].PutValue(rec.DeptCode);

                // 2 放上傳類別，請使用者自填 
                //上傳類別
                mdws.Cells[mdws_index, 3].PutValue(rec.UpdateType);
                //學號
                mdws.Cells[mdws_index, 4].PutValue(rec.StudentNumber);
                //姓名
                mdws.Cells[mdws_index, 5].PutValue(rec.Name);
                //身分證字號
                mdws.Cells[mdws_index, 6].PutValue(rec.IDNumber);

                //註1
                mdws.Cells[mdws_index, 7].PutValue(rec.Comment1);

                //性別代碼
                mdws.Cells[mdws_index, 8].PutValue(rec.GenderCode);
                //出生日期
                mdws.Cells[mdws_index, 9].PutValue(rec.Birthday);

                //特殊身份代碼
                mdws.Cells[mdws_index, 10].PutValue(rec.SpecialStatusCode);

                //異動原因代碼
                if (LastCodeDict.ContainsKey(rec.IDNumber))
                    mdws.Cells[mdws_index, 11].PutValue(LastCodeDict[rec.IDNumber]);
                else
                    mdws.Cells[mdws_index, 11].PutValue(rec.UpdateCode);

                
                //備查文字
                mdws.Cells[mdws_index, 12].PutValue(rec.LastADDoc);
                //備查文號
                mdws.Cells[mdws_index, 13].PutValue(rec.LastADNum);

                //備查日期
                mdws.Cells[mdws_index, 14].PutValue(rec.LastADDate);
                

                //畢業證書字號
                mdws.Cells[mdws_index, 15].PutValue(rec.GraduateCertificateNumber);

                //畢業證書註記學程代碼 (2019/02/15 穎驊 檢查發現 目前我們系統沒有支援這個概念，要再研究)
                mdws.Cells[mdws_index, 16].PutValue("");

                //備註說明
                mdws.Cells[mdws_index, 17].PutValue(rec.Comment);
                //臨編學統日期
                mdws.Cells[mdws_index, 18].PutValue(rec.origin_temp_date);
                //臨編學統
                mdws.Cells[mdws_index, 19].PutValue(rec.origin_temp_desc);
                //臨編學統文號
                mdws.Cells[mdws_index, 20].PutValue(rec.origin_temp_number);

                
            }

            // 資料末底 加End
            mdws.Cells.CreateRange(mdws_index + 1, 1, false).CopyData(range_R_EndRow);
            mdws.Cells.CreateRange(mdws_index + 1, 1, false).CopyStyle(range_R_EndRow);
            wb.Worksheets.RemoveAt("電子格式範本");
            wb.Worksheets.RemoveAt("電子格式範本_含臨編");
            wb.Worksheets.RemoveAt("延修生畢業名冊封面範本");

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
            get { return "延修生畢業名冊"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }
    }
}
