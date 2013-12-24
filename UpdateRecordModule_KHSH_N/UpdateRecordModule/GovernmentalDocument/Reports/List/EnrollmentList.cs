using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;
using System.IO;
using System.Xml.Linq;

namespace UpdateRecordModule_KHSH_N.GovernmentalDocument.Reports.List
{
    /// <summary>
    /// 新生名冊
    /// </summary>
    public class EnrollmentList : ReportBuilder
    {
        public override string Description
        {
            get { return "高雄市高級中等學校學生學籍管理"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }

        public override string Copyright
        {
            get { return "iSchool"; }
        }

        public override string ReportName
        {
            get { return "新生名冊"; }
        }

        protected override void Build(System.Xml.XmlElement source, string location)
        {
            // 讀取樣板內容
            Workbook wb = new Workbook();
            wb.Open(new MemoryStream(Properties.Resources.EnrollmentTemplate));

            // 解析 XML            
            XElement elmRoot = XElement.Parse(source.OuterXml);

            Worksheet wst = wb.Worksheets["新生書面"];
            Worksheet wstTemp = wb.Worksheets["Template1"];
            // 取得暫存異動XML使用 年級,科別,XML
            Dictionary<string, Dictionary<string, List<XElement>>> tmpElmList = new Dictionary<string, Dictionary<string, List<XElement>>>();

            if (elmRoot != null)
            {
                // 取讀樣板 Range
                Range R_Title = wb.Worksheets.GetRangeByName("R_Title");
                Range R_Line = wb.Worksheets.GetRangeByName("R_Line");
                Range R_Line1 = wb.Worksheets.GetRangeByName("R_Line1");
                Range R_Memo = wb.Worksheets.GetRangeByName("R_Memo");
                Range R_SchoolName = wb.Worksheets.GetRangeByName("R_SchoolName");
                Range R_Title1 = wb.Worksheets.GetRangeByName("R_Title1");

                int rowIdx = 0, PageMaxStudent = 19;
                string ScYear = utility.GetXMLAttributeStr(elmRoot, "學年度") + "學年度第" + utility.GetXMLAttributeStr(elmRoot, "學期") + "學期";
                wst.Cells.CreateRange(0, 1, false).Copy(R_SchoolName);
                wst.Cells.CreateRange(2, 1, false).Copy(R_Title);
                wst.Cells.CreateRange(1, 1, false).Copy(R_Title1);


                foreach (XElement elmG in elmRoot.Elements("清單"))
                {
                    string gStr = utility.GetXMLAttributeStr(elmG, "年級");

                    string dStr = utility.GetXMLAttributeStr(elmG, "科別");
                    foreach (XElement elmE in elmG.Elements("異動紀錄"))
                    {

                        if (!tmpElmList.ContainsKey(gStr))
                            tmpElmList.Add(gStr, new Dictionary<string, List<XElement>>());

                        if (!tmpElmList[gStr].ContainsKey(dStr))
                            tmpElmList[gStr].Add(dStr, new List<XElement>());

                        tmpElmList[gStr][dStr].Add(elmE);
                    }
                }

                foreach (string gS in tmpElmList.Keys)
                {
                    foreach (KeyValuePair<string, List<XElement>> data in tmpElmList[gS])
                    {
                        if (data.Value.Count == 0)
                            continue;

                        wst.Cells.CreateRange(rowIdx, 1, false).Copy(R_SchoolName);
                        wst.Cells[rowIdx, 0].PutValue(utility.GetXMLAttributeStr(elmRoot, "學校名稱"));
                        rowIdx++;
                        wst.Cells.CreateRange(rowIdx, 1, false).Copy(R_Title1);
                        wst.Cells[rowIdx, 0].PutValue(ScYear + gS + "年級");
                        wst.Cells[rowIdx, 3].PutValue(data.Key + " " + utility.GetXMLAttributeStr(elmRoot, "類別"));
                        rowIdx++;
                        wst.Cells.CreateRange(rowIdx, 1, false).Copy(R_Title);
                        rowIdx++;
                        int sp_count = 0, PageCount = 1;
                        wst.Cells[rowIdx - 2, 6].PutValue("頁數：" + PageCount);

                        foreach (XElement elmE in data.Value)
                        {
                            if (sp_count > PageMaxStudent)
                            {
                                sp_count = 0;
                                PageCount++;
                                rowIdx++;
                                wst.HPageBreaks.Add(rowIdx, 1);
                                wst.Cells.CreateRange(rowIdx, 1, false).Copy(R_SchoolName);
                                wst.Cells[rowIdx, 0].PutValue(utility.GetXMLAttributeStr(elmRoot, "學校名稱"));
                                rowIdx++;
                                wst.Cells.CreateRange(rowIdx, 1, false).Copy(R_Title1);
                                wst.Cells[rowIdx, 0].PutValue(ScYear + gS + "年級");
                                wst.Cells[rowIdx, 3].PutValue(data.Key + " " + utility.GetXMLAttributeStr(elmRoot, "類別"));
                                wst.Cells[rowIdx, 6].PutValue("頁數：" + PageCount);
                                rowIdx++;
                                wst.Cells.CreateRange(rowIdx, 1, false).Copy(R_Title);
                                rowIdx++;
                            }

                            wst.Cells.CreateRange(rowIdx, 1, false).Copy(R_Line1);

                            // 學號
                            wst.Cells[rowIdx, 0].PutValue(utility.GetXMLAttributeStr(elmE, "學號"));
                            // 姓名
                            wst.Cells[rowIdx, 1].PutValue(utility.GetXMLAttributeStr(elmE, "姓名"));
                            // 性別
                            wst.Cells[rowIdx, 2].PutValue(utility.GetXMLAttributeStr(elmE, "性別"));
                            // 入學年月,解析異動日期，取年月10101
                            // wst.Cells[rowIdx, 3].PutValue(utility.GetXMLAttributeStr(elmE, "入學年月"));
                            DateTime dt;
                            string dStr = utility.GetXMLAttributeStr(elmE, "異動日期").Trim();
                            if (DateTime.TryParse(dStr, out dt))
                            {
                                string YM = (dt.Year - 1911) + string.Format("{0:00}", dt.Month);
                                wst.Cells[rowIdx, 3].PutValue(YM);
                            }
                            // 生日
                            wst.Cells[rowIdx, 4].PutValue(utility.DateParse1(utility.GetXMLAttributeStr(elmE, "生日")));

                            // 入學資格,讀取畢業國中
                            //wst.Cells[rowIdx, 5].PutValue(utility.GetXMLAttributeStr(elmE, "入學資格"));
                            wst.Cells[rowIdx, 5].PutValue(utility.GetXMLAttributeStr(elmE, "畢業國中"));

                            // 入學方式
                            wst.Cells[rowIdx, 6].PutValue(utility.GetXMLAttributeStr(elmE, "入學方式"));


                            // 備註
                            wst.Cells[rowIdx, 7].PutValue(utility.GetXMLAttributeStr(elmE, "備註"));
                            sp_count++;
                            rowIdx++;
                        }
                        wst.Cells.CreateRange(rowIdx, 1, false).Copy(R_Line);
                        wst.Cells[rowIdx, 0].PutValue("合計：" + data.Value.Count + "人");
                        rowIdx++;
                        wst.Cells.CreateRange(rowIdx, 1, false).Copy(R_Memo);
                        rowIdx++;

                        wst.HPageBreaks.Add(rowIdx, 1);

                    }
                }
            }

            // 移除樣板
            wb.Worksheets.RemoveAt("Template1");
            //存檔
            wb.Save(location, FileFormatType.Excel2003);
        }
    }
}
