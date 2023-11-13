using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml ;
using System.Xml.Linq ;

namespace UpdateRecordModule_SH_D.GovernmentalDocument.Reports.List
{
    public class UtilXml
    {
        /// <summary>
        /// 將名冊內資料依學生學號排序
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static XmlElement SortByStudentNumber(XmlElement source)
        {
            XmlElement retVal = null;
            XElement elmRoot = XElement.Parse(source.OuterXml);

            List<XElement> dataElms = (from elm in elmRoot.Elements("清單") select elm).ToList();
            elmRoot.RemoveNodes();

            foreach (XElement d1Elm in dataElms)
            {
                if (d1Elm.Attribute("科別") != null)
                {
                    int idx = d1Elm.Attribute("科別").Value.IndexOf(":");
                    if (idx > 1)
                        d1Elm.Attribute("科別").Value = d1Elm.Attribute("科別").Value.Substring(0, idx);
                }
                List<XElement> upRecList = null;

                //2018/2/5 穎驊 新增異動封面的處理，如不特別補充邏輯，寫進去的資料格式會被洗掉。
                List<XElement> upRecCover =new List<XElement>();

                try
                {
                    // 使用學號排序
                    
                    upRecList = (from elm in d1Elm.Elements("異動紀錄") orderby elm.Attribute("學號").Value select elm).ToList();

                    //2018/2/5 穎驊 新增異動封面的處理
                    upRecCover = (from elm in d1Elm.Elements("異動名冊封面") select elm).ToList();
                }
                catch
                {
                    // 就名冊有些沒有學號，不排序
                    upRecList = (from elm in d1Elm.Elements("異動紀錄") select elm).ToList();
                }

                d1Elm.RemoveNodes();

                foreach (XElement elmRec in upRecList)
                {
                    if (elmRec.Attribute("科別") != null)
                    {
                        int idx = elmRec.Attribute("科別").Value.IndexOf(":");
                        if (idx > 1)
                            elmRec.Attribute("科別").Value = elmRec.Attribute("科別").Value.Substring(0, idx);
                    }


                    // 在排序時處理:名稱
                    d1Elm.Add(elmRec);

                }

                //2018/2/5 穎驊 新增異動封面的處理 因封面只有一張，故為第一個
                if (upRecCover.Count == 1)
                {
                    d1Elm.Add(upRecCover[0]);
                }
                

                elmRoot.Add(d1Elm);
                                
            }
            
           retVal = new XmlDocument ().ReadNode(elmRoot.CreateReader())as XmlElement ;

           return retVal;
        
        }
    }
}
