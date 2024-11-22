using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leave_School_Notification
{
    class Permissions
    {
        public static string 功能名稱 { get { return "權限代碼"; } }
        public static bool 功能名稱權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[功能名稱].Executable;
            }
        }
    }
}
