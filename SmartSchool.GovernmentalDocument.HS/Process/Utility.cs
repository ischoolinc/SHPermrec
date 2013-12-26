using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Data;
using System.Data;
namespace SmartSchool.GovernmentalDocument.Process
{
    class Utility
    {
        /// <summary>
        /// 取得畢業或離校學生學號,學號,StudetID (驗證用)
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetStudentNumber16()
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();
            QueryHelper qh = new QueryHelper();
            string strSQL = "select student_number,id from student where status=16 and student_number is not null;";
            DataTable dt = qh.Select(strSQL);
            foreach (DataRow dr in dt.Rows)
            {
                string key = dr[0].ToString().ToUpper();

                if (!retVal.ContainsKey(key))
                    retVal.Add(key,dr[1].ToString());            
            }
            return retVal;
        }

        /// <summary>
        /// 取得畢業或離校學生身分證號,身分證號,StudetID (驗證用)
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetStudentIDNumber16()
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();
            QueryHelper qh = new QueryHelper();
            string strSQL = "select id_number,id from student where status=16 and id_number is not null;";
            DataTable dt = qh.Select(strSQL);
            foreach (DataRow dr in dt.Rows)
            {
                string key = dr[0].ToString().ToUpper();

                if (!retVal.ContainsKey(key))
                    retVal.Add(key, dr[1].ToString());            
            }
            return retVal;
        }

        /// <summary>
        /// 透過學生系統編號取得在畢業或離校有相同登入帳號的學生
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetHasSameLoginName16(List<string> StudentIDList)
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();
            if (StudentIDList.Count > 0)
            {
                QueryHelper qh = new QueryHelper();
                string strSQL = "select sa_login_name,id from student where status=16 and sa_login_name in(select sa_login_name from student where sa_login_name is not null and id in("+string.Join(",",StudentIDList.ToArray())+"));";
                DataTable dt = qh.Select(strSQL);
                foreach (DataRow dr in dt.Rows)
                {
                    string key = dr[0].ToString();
                    if (!retVal.ContainsKey(key))
                        retVal.Add(key, dr[1].ToString());
                }
            }
            return retVal;
        }
    }
}
