using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Presentation;
using K12.Presentation;
using FISCA.Permission;

namespace SHStudentCard
{
    // 高中學生證
    public class Program
    {
        [FISCA.MainMethod()]
        public static void Main()
        {
            RibbonBarItem rptItem = MotherForm.RibbonBarItems["學生", "資料統計"];
            rptItem["報表"]["學籍相關報表"]["學生證"].Enable = UserAcl.Current["SHStudentCard_Student"].Executable;
            rptItem["報表"]["學籍相關報表"]["學生證"].Click += delegate
            {
                if (NLDPanels.Student.SelectedSource.Count > 0)
                {
                    List<string> SIDList=Utitlty.GetStudentIDListByStudentID( K12.Presentation.NLDPanels.Student.SelectedSource);
                    Forms.StudentCardForm scf = new Forms.StudentCardForm(SIDList);
                    scf.ShowDialog();
                }
                else
                {
                    FISCA.Presentation.Controls.MsgBox.Show("請選擇學生");
                    return;
                }
            };


            RibbonBarItem rptItemC = MotherForm.RibbonBarItems["班級", "資料統計"];
            rptItemC["報表"]["學籍相關報表"]["學生證"].Enable = UserAcl.Current["SHStudentCard_Class"].Executable;
            rptItemC["報表"]["學籍相關報表"]["學生證"].Click += delegate
            {
                if (NLDPanels.Class.SelectedSource.Count > 0)
                {
                    List<string> SIDList = Utitlty.GetStudentIDList1ByClassID(K12.Presentation.NLDPanels.Class.SelectedSource);
                    Forms.StudentCardForm scf = new Forms.StudentCardForm(SIDList);
                    scf.ShowDialog();
                }
                else
                {
                    FISCA.Presentation.Controls.MsgBox.Show("請選擇班級");
                    return;
                }
            };


            // 列印學生證(學生)
            Catalog catalog1 = RoleAclSource.Instance["學生"]["功能按鈕"];
            catalog1.Add(new RibbonFeature("SHStudentCard_Student", "學生證"));

            // 列印學生證(班級)
            Catalog catalog2 = RoleAclSource.Instance["班級"]["功能按鈕"];
            catalog2.Add(new RibbonFeature("SHStudentCard_Class", "學生證"));
        }

    }
}
