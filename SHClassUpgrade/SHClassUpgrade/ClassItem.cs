using K12.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SHClassUpgrade
{
    // 班級升級用
    class ClassItem
    {
        public string ClassID { get; set; }
        /// <summary>
        /// 原班級名稱
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 原班級年級
        /// </summary>
        public string GradeYear { get; set; }
        /// <summary>
        /// 新班級名稱
        /// </summary>
        public string newClassName { get; set; }
        /// <summary>
        /// 新班級年級
        /// </summary>
        public string newGradeYear { get; set; }

        /// <summary>
        /// 班級命名規則
        /// </summary>
        public string NamingRule { get; set; }

        /// <summary>
        /// 班級 record
        /// </summary>
        public ClassRecord Class_Record { get; set; }

    }
}
