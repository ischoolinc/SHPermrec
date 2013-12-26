using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FISCA.Data;

namespace SHStudentCard
{
    public class Utitlty
    {
        /// <summary>
        /// 透過班級ID取得班級內學生狀態一般的學生ID
        /// </summary>
        /// <param name="ClassIDList"></param>
        /// <returns></returns>
        public static List<string> GetStudentIDList1ByClassID(List<string> ClassIDList)
        {
            List<string> retVal = new List<string>();
            if (ClassIDList.Count > 0)
            {
                QueryHelper qh = new QueryHelper();
                string strSQL = "select student.id from student inner join class on student.ref_class_id=class.id where student.status in(1) and class.id in(" + string.Join(",", ClassIDList.ToArray()) + ") order by student_number;";
                DataTable dt = qh.Select(strSQL);
                foreach (DataRow dr in dt.Rows)
                    retVal.Add(dr[0].ToString());
            }
            return retVal;
        }

        /// <summary>
        /// 班座排序
        /// </summary>
        /// <param name="StudIDList"></param>
        /// <returns></returns>
        public static List<string> GetStudentIDListByStudentID(List<string> StudIDList)
        {
            List<string> retVal = new List<string>();
            if (StudIDList.Count > 0)
            {
                QueryHelper qh = new QueryHelper();
                string strSQL = "select student.id from student left join class on student.ref_class_id=class.id where student.id in(" + string.Join(",", StudIDList.ToArray()) + ") order by class.class_name,student.seat_no;";
                DataTable dt = qh.Select(strSQL);
                foreach (DataRow dr in dt.Rows)
                    retVal.Add(dr[0].ToString());
            }
            return retVal;
        }

    }
}
