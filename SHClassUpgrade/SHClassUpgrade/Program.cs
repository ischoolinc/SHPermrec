using FISCA;
using FISCA.Permission;
using FISCA.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SHClassUpgrade
{
    public class Program
    {
        [MainMethod()]
        public static void Main()
        {
            Catalog catalog01 = RoleAclSource.Instance["教務作業"]["功能按鈕"];
            catalog01.Add(new RibbonFeature("SH_SHClassUpgrade", "班級升級"));

            var rbItemClassUpgrade = FISCA.Presentation.MotherForm.RibbonBarItems["教務作業","批次作業/檢視"]["班級升級"];
            rbItemClassUpgrade.Size = RibbonBarButton.MenuButtonSize.Medium;
            rbItemClassUpgrade.Image = Properties.Resources.btnUpgrade_Image;
            rbItemClassUpgrade.Enable = UserAcl.Current["SH_SHClassUpgrade"].Executable;
            rbItemClassUpgrade.Click += delegate
            {
                ClassUpgradeForm CUF = new ClassUpgradeForm();
                CUF.ShowDialog();
            };

        }
    }
}
