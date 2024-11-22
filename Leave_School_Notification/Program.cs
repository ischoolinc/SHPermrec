using FISCA;
using FISCA.Permission;
using FISCA.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leave_School_Notification

{
    public class Program
    {
        //參考Fisca.dll,讓DLL可被掃描
        [MainMethod()]
        public static void Main()
        {
            var key = "DBCCA830-AE1B-4B85-B2B0-45A1A9C3F390";
            RoleAclSource.Instance["教務作業"]["功能按鈕"].Add(new RibbonFeature(key, "復學通知單"));
            MotherForm.RibbonBarItems["教務作業", "資料統計"]["報表"]["復學通知單"].Enable = FISCA.Permission.UserAcl.Current[key].Executable;

            MotherForm.RibbonBarItems["教務作業", "資料統計"]["報表"]["復學通知單"].Click += delegate
            {
                //開啟功能
                休學期滿復學通知單 f = new 休學期滿復學通知單();
                f.ShowDialog();
            };
            var key1 = "FF7BF929-5552-4019-843B-5117B67BE36E";
            RoleAclSource.Instance["學生"]["功能按鈕"].Add(new RibbonFeature(key1, "休學通知單"));
            MotherForm.RibbonBarItems["學生", "資料統計"]["報表"]["學籍相關報表"]["休學通知單"].Enable = FISCA.Permission.UserAcl.Current[key1].Executable;

            MotherForm.RibbonBarItems["學生", "資料統計"]["報表"]["學籍相關報表"]["休學通知單"].Click += delegate
            {
                //開啟功能
                曠課七日休學通知單 f = new 曠課七日休學通知單();
                f.ShowDialog();
            };
            var key2 = "A79EF9F1-6BBF-46E1-9877-1C331AB68357";
            RoleAclSource.Instance["學生"]["功能按鈕"].Add(new RibbonFeature(key2, "放棄學籍通知單"));
            MotherForm.RibbonBarItems["學生", "資料統計"]["報表"]["學籍相關報表"]["放棄學籍通知單"].Enable = FISCA.Permission.UserAcl.Current[key2].Executable;

            MotherForm.RibbonBarItems["學生", "資料統計"]["報表"]["學籍相關報表"]["放棄學籍通知單"].Click += delegate
            {
                //開啟功能
                放棄學籍通單 f = new 放棄學籍通單();
                f.ShowDialog();
            };

        }
    }
}
