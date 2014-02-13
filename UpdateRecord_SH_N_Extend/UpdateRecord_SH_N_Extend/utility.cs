using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace UpdateRecord_SH_N_Extend
{
    class utility
    {
        /// <summary>
        /// 將異動代碼與原因及事由，由XML轉成Dict
        /// </summary>
        /// <param name="UpdateCodeElms"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ConvertUpdateCodeDescDict(List<XElement> UpdateCodeElms)
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();

            foreach (XElement elm in UpdateCodeElms)
            {
                if (!retVal.ContainsKey(elm.Element("代號").Value))
                    retVal.Add(elm.Element("代號").Value, elm.Element("原因及事項").Value);
            }
            return retVal;
        }
    }
}
