using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;
using System.IO;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using FISCA.Data;
using System.Data;
using System.Xml.Linq;

namespace UpdateRecordModule_KHSH_D
{
    public class utility
    {
        public static void ExportXls(string inputReportName, Workbook inputXls)
        {
            string reportName = inputReportName;

            string path = Path.Combine(Application.StartupPath, "Reports");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, reportName + ".xls");

            Workbook wb = inputXls;

            if (File.Exists(path))
            {
                int i = 1;
                while (true)
                {
                    string newPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + (i++) + Path.GetExtension(path);
                    if (!File.Exists(newPath))
                    {
                        path = newPath;
                        break;
                    }
                }
            }

            try
            {
                wb.Save(path, Aspose.Cells.FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(path);
            }
            catch
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.Title = "另存新檔";
                sd.FileName = reportName + ".xls";
                sd.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        wb.Save(sd.FileName, Aspose.Cells.FileFormatType.Excel2003);

                    }
                    catch
                    {
                        MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 檢查一般學生學號是否重複
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool CheckStudentNumberSame1(string number)
        {
            bool retVal = false;
            QueryHelper qh = new QueryHelper();
            string strSQL = "select id from student where status=1 and student_number='"+number+"'";
            DataTable dt= qh.Select(strSQL);
            if (dt.Rows.Count > 0)
                retVal = true;
            return retVal;
        }

        /// <summary>
        /// 取的所有狀態一般1的學生學號
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetStudentNumberStatus1()
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();
            QueryHelper qh = new QueryHelper();
            string strSQL = "select student_number,id from student where status=1;";            
            DataTable dt = qh.Select(strSQL);
            foreach (DataRow dr in dt.Rows)
            {
                string num = dr[0].ToString();

                if (!retVal.ContainsKey(num))
                    retVal.Add(num, dr[1].ToString());
            }
            return retVal;
        }

        /// <summary>
        /// 取的所有狀態一般1的學生身分證號
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetStudentIDNumberStatus1()
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();
            QueryHelper qh = new QueryHelper();
            string strSQL = "select id_number,id from student where status=1;";
            DataTable dt = qh.Select(strSQL);
            foreach (DataRow dr in dt.Rows)
            {
                string num = dr[0].ToString();

                if (!retVal.ContainsKey(num))
                    retVal.Add(num, dr[1].ToString());
            }
            return retVal;
        }

        /// <summary>
        /// 取讀非刪除學生ID
        /// </summary>
        /// <returns></returns>
        public static List<string> GetStudentIDListNot256()
        {
            List<string> retVal = new List<string>();
            QueryHelper qh = new QueryHelper();
            string strSQL = "select id from student where status<256;";
            DataTable dt = qh.Select(strSQL);
            foreach (DataRow dr in dt.Rows)
                retVal.Add(dr[0].ToString());

            return retVal;
        }

        /// <summary>
        /// 取得 XML 內Attribute 值
        /// </summary>
        /// <param name="elm"></param>
        /// <param name="AttrName"></param>
        /// <returns></returns>
        public static string GetXMLAttributeStr(XElement elm, string AttrName)
        {
            string retVal = "";
            if (elm.Attribute(AttrName) != null)
                retVal = elm.Attribute(AttrName).Value;
            return retVal;
        }

        /// <summary>
        /// 日期轉換西元->民國,2013/1/1,102.01.01,轉換失敗回傳原輸入
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static string DateParse1(string strDate)
        {
            string retVal = "";
            DateTime dt;
            if (DateTime.TryParse(strDate, out dt))
            {
                retVal = (dt.Year - 1911) +"."+ string.Format("{0:00}", dt.Month) + "."+string.Format("{0:00}", dt.Day);
            }
            else
                retVal = strDate;

            return retVal;
        }
    }
}
