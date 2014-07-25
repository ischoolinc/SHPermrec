using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FISCA.Data;
using K12.Data;
using System.Data;

namespace SHClassUpgrade
{
    class UpgradeClassDAL
    {
        /// <summary>
        /// 修改班級名稱與年級
        /// </summary>
        /// <param name="ClassItems"></param>
        public static void UpdateClassNameGradeYear(List<ClassItem> ClassItems)
        {

            List<ClassRecord> classRecList = new List<ClassRecord>();
            // 依班級年級排序
            ClassItems.Sort(new Comparison<ClassItem>(ClassItemmStrCpmparer));

            foreach (ClassItem ci in ClassItems)
            {
                ClassRecord urRec = ci.Class_Record;

                if (ci.ClassName != ci.newClassName)
                    urRec.Name = ci.newClassName;

                int GrYear;
                if (int.TryParse(ci.newGradeYear, out GrYear))
                {
                    urRec.GradeYear = GrYear;
                }
                else
                    urRec.GradeYear = null;

                classRecList.Add(urRec);
            }
            try
            {
                // 更新班級
                Class.Update(classRecList);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("班級名稱更新失敗!");
            }
            
            //Class.Instance.SyncAllBackground();
        }


        /// <summary>
        /// 檢查系統內是否有班級相同名稱
        /// </summary>
        /// <param name="ClassItems"></param>
        /// <returns></returns>
        public static List<ClassItem> checkUpdateClassName(List<ClassItem> ClassItems)
        {
            List<ClassItem> checkClassItems = new List<ClassItem>();

            List<string> classNameList = new List<string>();

            List<ClassRecord> classRecList = Class.SelectAll();
            foreach (ClassRecord cr in classRecList)
                classNameList.Add(cr.Name);

                foreach (ClassItem ci in ClassItems)
                    if(classNameList.Contains(ci.newClassName))
                        checkClassItems.Add(ci);

            return checkClassItems;

        }


        // 字串比較
        public static int ClassItemmStrCpmparer(ClassItem x, ClassItem y)
        {
            return y.newGradeYear.CompareTo(x.newGradeYear);
        }        
        
        static string errStr = "";
        /// <summary>
        /// 設定學生狀態(只能用畢業)
        /// </summary>
        /// <param name="StudentItems"></param>
        /// <param name="Status"></param>        /// 
        public static void setStudentStatus(List<StudentItem> StudentItems)
        {

            List<string> sidList = new List<string>();
            foreach (StudentItem si in StudentItems)
                if(si.Status== StudentRecord.StudentStatus.一般)
                    sidList.Add(si.StudentID);
        
            if (sidList.Count > 0)
            {             
                // 取得畢業及離校狀態
                Dictionary<string, string> StudentNumberDict = new Dictionary<string, string>();
                Dictionary<string, string> IDNumberDict = new Dictionary<string, string>();
                Dictionary<string, string> LoginNameDict = new Dictionary<string, string>();

                QueryHelper qh = new QueryHelper();
                string queryString = "select id,student_number,id_number,sa_login_name from student where status=16;";
                DataTable dt = qh.Select(queryString);
                foreach (DataRow dr in dt.Rows)
                {
                    string sid = dr["id"].ToString();

                    if (dr["student_number"] != null)
                    {
                        string s1 = dr["student_number"].ToString();
                        if (!string.IsNullOrWhiteSpace(s1))
                            StudentNumberDict.Add(s1, sid);
                    }

                    if (dr["id_number"] != null)
                    {
                        string s2 = dr["id_number"].ToString();
                        if (!string.IsNullOrWhiteSpace(s2))
                            IDNumberDict.Add(s2, sid);
                    }
                }

                QueryHelper qh1 = new QueryHelper();
                string qh1Str = "select id,sa_login_name from student where sa_login_name is not null and sa_login_name<>'';";
                DataTable dt1 = qh1.Select(qh1Str);
                foreach (DataRow dr in dt1.Rows)
                {
                    string sid = dr["id"].ToString();

                    if (dr["sa_login_name"] != null)
                    {
                        string s1 = dr["sa_login_name"].ToString();
                        if (!string.IsNullOrWhiteSpace(s1))
                            LoginNameDict.Add(s1, sid);
                    }
                }

                List<string> errMsg = new List<string>();
                List<StudentRecord> StudRecs = Student.SelectByIDs(sidList);
                foreach (StudentRecord studRec in StudRecs)
                {
                    bool pass = true;

                    if (StudentNumberDict.ContainsKey(studRec.StudentNumber))
                    {
                        errMsg.Add("學號：" + studRec.StudentNumber);
                        pass = false;
                    }

                    if (IDNumberDict.ContainsKey(studRec.IDNumber))
                    {
                        errMsg.Add("身分證號：" + studRec.IDNumber);
                        pass = false;
                    }

                    if (LoginNameDict.ContainsKey(studRec.SALoginName))
                    {
                        errMsg.Add("登入帳號：" + studRec.SALoginName);
                        pass = false;
                    }


                    if (pass)
                        studRec.Status = K12.Data.StudentRecord.StudentStatus.畢業或離校;
                }

                bool isUpdate = true;
                
                if (errMsg.Count > 0)
                {
                    if (FISCA.Presentation.Controls.MsgBox.Show(string.Join(",", errMsg.ToArray()) + "，無法變更這些學生狀態，點「是」繼續變更學生狀態，點「否」完全不變更學生狀態", "資料重複", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                    {
                        isUpdate = false;
                    }
                }

                if(isUpdate)
                    Student.Update(StudRecs);           

             
                    FISCA.Presentation.Controls.MsgBox.Show(errStr);
            }

        }
                

        /// <summary>
        /// 取得班級學生
        /// </summary>
        /// <param name="ClassIDs"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static Dictionary<string, List<StudentItem>> getStudentItems(List<ClassItem> ClassItems, K12.Data.StudentRecord.StudentStatus Status)
        {
         

            Dictionary<string, List<StudentRecord>> classStud = new Dictionary<string, List<StudentRecord>>();

            foreach (StudentRecord studRec in Student.SelectAll())
            {
                if (studRec.Status == K12.Data.StudentRecord.StudentStatus.一般)
                {
                    if (classStud.ContainsKey(studRec.RefClassID))
                        classStud[studRec.RefClassID].Add(studRec);
                    else
                    {
                        List<StudentRecord> StudRecList = new List<StudentRecord>();
                        StudRecList.Add(studRec);
                        classStud.Add(studRec.RefClassID, StudRecList);
                    }

                }
            }

            Dictionary<string, List<StudentItem>> StudentItems = new Dictionary<string, List<StudentItem>>();
            foreach (ClassItem ci in ClassItems)
            {
                List<StudentItem> studItems = new List<StudentItem>();

                if (classStud.ContainsKey(ci.ClassID))
                {
                    foreach (StudentRecord studRec in classStud[ci.ClassID])
                    {
                        StudentItem si = new StudentItem();
                        si.StudentID = studRec.ID;
                        si.Status = studRec.Status;
                        si.ClassName = studRec.Class.Name;
                        studItems.Add(si);
                        si = null;
                    }
                    StudentItems.Add(ci.ClassID, studItems);
                }
            }
            return StudentItems;
        }


        /// <summary>
        /// 取得目前年級班級
        /// </summary>
        /// <returns></returns>
        public static List<ClassItem> getClassItems()
        {
         
            List<string> ClassIDList = new List<string>();
            List<ClassRecord> classRecList = new List<ClassRecord>();

            foreach (StudentRecord stud in Student.SelectAll())
                if (stud.Status == K12.Data.StudentRecord.StudentStatus.一般)
                    if (!string.IsNullOrEmpty(stud.RefClassID))
                        if (!ClassIDList.Contains(stud.RefClassID))
                        {
                            ClassIDList.Add(stud.RefClassID);
                            classRecList.Add(stud.Class);
                        }
            List<ClassItem> ClassItems = new List<ClassItem>();
            List<ClassItem> tmpCItems = new List<ClassItem>();
            foreach (ClassRecord cr in classRecList)
            {
                if (cr.GradeYear.HasValue)
                {
                    ClassItem ci = new ClassItem();
                    ci.ClassID = cr.ID;
                    if (cr.GradeYear.HasValue)
                        ci.GradeYear = cr.GradeYear.Value + "";
                    ci.ClassName = cr.Name;
                    ci.NamingRule = cr.NamingRule;
                    ci.Class_Record = cr;
                    tmpCItems.Add(ci);
                    ci = null;
                }
            }

           

            return ClassItems;
        }

        private static int tmpSortClassItem1(ClassItem x, ClassItem y)
        {
            return x.ClassName.CompareTo(y.ClassName);
        }

        public static string ParseClassName(string namingRule, int gradeYear)
        {
            if (gradeYear >= 6)
                gradeYear -= 6;
            gradeYear--;
            if (!ValidateNamingRule(namingRule))
                return namingRule;
            string classlist_firstname = "", classlist_lastname = "";
            if (namingRule.Length == 0) return "{" + (gradeYear + 1) + "}";

            string tmp_convert = namingRule;

            // 找出"{"之前文字 並放入 classlist_firstname , 並除去"{"
            if (tmp_convert.IndexOf('{') > 0)
            {
                classlist_firstname = tmp_convert.Substring(0, tmp_convert.IndexOf('{'));
                tmp_convert = tmp_convert.Substring(tmp_convert.IndexOf('{') + 1, tmp_convert.Length - (tmp_convert.IndexOf('{') + 1));
            }
            else tmp_convert = tmp_convert.TrimStart('{');

            // 找出 } 之後文字 classlist_lastname , 並除去"}"
            if (tmp_convert.IndexOf('}') > 0 && tmp_convert.IndexOf('}') < tmp_convert.Length - 1)
            {
                classlist_lastname = tmp_convert.Substring(tmp_convert.IndexOf('}') + 1, tmp_convert.Length - (tmp_convert.IndexOf('}') + 1));
                tmp_convert = tmp_convert.Substring(0, tmp_convert.IndexOf('}'));
            }
            else tmp_convert = tmp_convert.TrimEnd('}');

            // , 存入 array
            string[] listArray = new string[tmp_convert.Split(',').Length];
            listArray = tmp_convert.Split(',');

            // 檢查是否在清單範圍
            if (gradeYear >= 0 && gradeYear < listArray.Length)
            {
                tmp_convert = classlist_firstname + listArray[gradeYear] + classlist_lastname;
            }
            else
            {
                tmp_convert = classlist_firstname + "{" + (gradeYear + 1) + "}" + classlist_lastname;
            }
            return tmp_convert;
        }

        public static bool ValidateNamingRule(string namingRule)
        {
            return namingRule.IndexOf('{') < namingRule.IndexOf('}');
        }

    }
}
