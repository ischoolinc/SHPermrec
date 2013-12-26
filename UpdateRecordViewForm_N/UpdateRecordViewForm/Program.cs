using System;
using System.Collections.Generic;
using System.Text;
using Framework;
using FISCA.Presentation;
using Framework.Security;
using SHSchool.Affair;
//using SmartSchool;

namespace UpdateRecordViewForm
{
    public class Program
    {
        [FISCA.MainMethod()]
        public static void Main()
        {
            try
            {
                RibbonBarButton rbItem = EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["異動作業"];
                rbItem["異動資料檢視"].Enable = FISCA.Permission.UserAcl.Current["SHSchool.EduAdmin.Ribbon0070"].Executable;
                    //User.Acl["SHSchool.EduAdmin.Ribbon0070"].Executable;
                rbItem["異動資料檢視"].Click += delegate
                {
                    
                    UpdateRecordViewForm urvf = new UpdateRecordViewForm();
                    urvf.ShowDialog();
                };



                //// 註冊
                //Catalog ribbon11 = RoleAclSource.Instance["教務作業"]["功能按鈕"];
                //ribbon11.Add(new RibbonFeature("SHSchool.EduAdmin.Ribbon0070", "異動資料檢視"));

            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }
        }
    }
}
