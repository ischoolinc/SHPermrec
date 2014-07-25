using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SHClassUpgrade
{
    class StudentItem
    {
        public string StudentID { get; set; }
        /// <summary>
        /// 學生班級
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 學生狀態
        /// </summary>
        public K12.Data.StudentRecord.StudentStatus Status { get; set; }
    }
}
