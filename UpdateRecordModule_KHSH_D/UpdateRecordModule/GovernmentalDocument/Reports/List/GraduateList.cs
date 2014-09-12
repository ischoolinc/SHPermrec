using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Aspose.Cells;
using System.Xml.Linq;

namespace UpdateRecordModule_KHSH_D.GovernmentalDocument.Reports.List
{
    /// <summary>
    /// 畢業名冊
    /// </summary>
    public class GraduateList : ReportBuilder
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
            get { return "畢業名冊"; }
        }

        protected override void Build(System.Xml.XmlElement source, string location)
        {
            // 讀取樣板內容
            Workbook wb = new Workbook();
            wb.Open(new MemoryStream(Properties.Resources.GraduateTemplate));
            // 解析 XML            
            XElement elmRoot = XElement.Parse(source.OuterXml);
            Worksheet wst = wb.Worksheets["畢業書面"];
            Worksheet wstTemp = wb.Worksheets["Template1"];
            // 取得暫存異動XML使用 年級,科別,XML
            Dictionary<string, Dictionary<string, List<XElement>>> tmpElmList = new Dictionary<string, Dictionary<string, List<XElement>>>();

            // 取讀樣板 Range
            Range R_Title = wb.Worksheets.GetRangeByName("R_Title");
            Range R_Line = wb.Worksheets.GetRangeByName("R_Line");
            Range R_Title1 = wb.Worksheets.GetRangeByName("R_Title1");
            
            int rowIdx = 0, PageMaxStudent = 20;
            string ScYear = utility.GetXMLAttributeStr(elmRoot, "學年度") + "學年度第" + utility.GetXMLAttributeStr(elmRoot, "學期") + "學期";            
         
            
            if (elmRoot != null)
            {
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

                List<string> sidList = new List<string>();

                foreach (string gS in tmpElmList.Keys)
                {
                    foreach (KeyValuePair<string, List<XElement>> data in tmpElmList[gS])
                    {
                        // 取得入學學年                        
                        foreach (XElement elmE in data.Value)
                        {
                            sidList.Add(utility.GetXMLAttributeStr(elmE, "學生編號"));
                        }
                    }
                }

                // 入學年月索引
                Dictionary<string, DateTime> NDateDict = utility.GetStudEnrollDateByStudentIDs(sidList);
                

                foreach (string gS in tmpElmList.Keys)
                {

                    foreach (KeyValuePair<string, List<XElement>> data in tmpElmList[gS])
                    {
                        if (data.Value.Count == 0)
                            continue;

                        string strTitle = utility.GetXMLAttributeStr(elmRoot, "學校名稱") + "    " + ScYear + "    " +data.Key+" "+ utility.GetXMLAttributeStr(elmRoot, "類別");

                        rowIdx++;
                        wst.Cells.CreateRange(rowIdx, 1, false).Copy(R_Title1);
                        wst.Cells[rowIdx, 0].PutValue(strTitle);

                        rowIdx++;
                        wst.Cells.CreateRange(rowIdx, 2, false).Copy(R_Title);
                        rowIdx += 2;

                        int sp_count = 0, PageCount = 1;

                    
                        foreach (XElement elmE in data.Value)
                        {
                            if (sp_count > PageMaxStudent)
                            {
                                sp_count = 0;
                                PageCount++;
                                rowIdx++;
                                wst.HPageBreaks.Add(rowIdx, 1);
                                rowIdx++;
                                wst.Cells.CreateRange(rowIdx, 1, false).Copy(R_Title1);
                                strTitle = utility.GetXMLAttributeStr(elmRoot, "學校名稱") + "    " + ScYear + "    " + data.Key + " " + utility.GetXMLAttributeStr(elmRoot, "類別");
                                wst.Cells[0, 0].PutValue(strTitle);
                                wst.Cells[rowIdx, 0].PutValue(strTitle);

                                rowIdx++;
                                wst.Cells.CreateRange(rowIdx, 2, false).Copy(R_Title);
                                rowIdx += 2;
                            }
                            wst.Cells.CreateRange(rowIdx, 1, false).Copy(R_Line);

                            string sid = utility.GetXMLAttributeStr(elmE, "學生編號");

                            // 學號
                            wst.Cells[rowIdx, 0].PutValue(utility.GetXMLAttributeStr(elmE, "學號"));
                            // 姓名
                            wst.Cells[rowIdx, 1].PutValue(utility.GetXMLAttributeStr(elmE, "姓名") + "\n" + utility.GetXMLAttributeStr(elmE, "身分證號"));
                            // 性別
                            wst.Cells[rowIdx, 2].PutValue(utility.GetXMLAttributeStr(elmE, "性別"));

                            if (elmE.Attribute("生日") != null)
                            {
                                DateTime dt;
                                if (DateTime.TryParse(elmE.Attribute("生日").Value, out dt))
                                {
                                    wst.Cells[rowIdx, 3].PutValue((dt.Year - 1911));
                                    wst.Cells[rowIdx, 4].PutValue((dt.Month));
                                    wst.Cells[rowIdx, 5].PutValue((dt.Day));
                                }
                            }

                            // 入學年月
                            if (NDateDict.ContainsKey(sid))
                            {
                                wst.Cells[rowIdx, 6].PutValue(NDateDict[sid].Year - 1911);
                                wst.Cells[rowIdx, 7].PutValue(NDateDict[sid].Month);
                            }


                            // 畢業年月,取得這筆異業異動日期，取年-1911
                            DateTime dtG;
                            if (DateTime.TryParse(utility.GetXMLAttributeStr(elmE, "異動日期"),out dtG))
                            {
                                // 畢業年
                                wst.Cells[rowIdx, 8].PutValue(dtG.Year - 1911);

                                // 畢業月
                                wst.Cells[rowIdx, 9].PutValue(dtG.Month);
                            }
                            

                            // 畢業證書字號
                            wst.Cells[rowIdx, 10].PutValue(utility.GetXMLAttributeStr(elmE, "畢業證書字號"));

                            // 編號
                            wst.Cells[rowIdx, 11].PutValue(utility.GetXMLAttributeStr(elmE, "編號"));

                            // 備註
                            wst.Cells[rowIdx, 12].PutValue(utility.GetXMLAttributeStr(elmE, "備註"));
                            sp_count++;
                            rowIdx++;

                        }

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
