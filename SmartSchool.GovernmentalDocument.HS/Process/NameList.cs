using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SmartSchool.GovernmentalDocument.NameList;
using FISCA.Presentation;

namespace SmartSchool.GovernmentalDocument.Process
{
    public partial class NameList : UserControl
    {
        FeatureAccessControl nameListCtrl;

        public NameList()
        {
            //InitializeComponent();
            //SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<ButtonItem>.Instance["教務作業/學籍作業"].Add(btnItemNameList);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NameList));


            //將原有函報名冊功能先註解
            var btnItemNameList = MotherForm.RibbonBarItems["教務作業", "批次作業/檢視"]["異動作業"];
            btnItemNameList.Image = Properties.Resources.history_save_64;
            btnItemNameList.Size = RibbonBarButton.MenuButtonSize.Large;

            btnItemNameList["函報名冊(舊)"].Click += new System.EventHandler(this.btnItemNameList_Click);
            //權限判斷 - 學籍作業/函報名冊
            nameListCtrl = new FeatureAccessControl("Button0630");
            btnItemNameList["函報名冊(舊)"].Enable = nameListCtrl.Executable();

        }

        private void btnItemNameList_Click(object sender, EventArgs e)
        {
            if ( ReportBuilderManager.Items["新生名冊"].Count == 0 )
                ReportBuilderManager.Items["新生名冊"].Add(new EnrollmentList());
            if ( ReportBuilderManager.Items["延修生學籍異動名冊"].Count == 0 )
                ReportBuilderManager.Items["延修生學籍異動名冊"].Add(new ExtendingStudentUpdateRecordList());
            if ( ReportBuilderManager.Items["學籍異動名冊"].Count == 0 )
                ReportBuilderManager.Items["學籍異動名冊"].Add(new StudentUpdateRecordList());
            if ( ReportBuilderManager.Items["畢業名冊"].Count == 0 )
                ReportBuilderManager.Items["畢業名冊"].Add(new GraduatingStudentList());
            if ( ReportBuilderManager.Items["延修生畢業名冊"].Count == 0 )
                ReportBuilderManager.Items["延修生畢業名冊"].Add(new ExtendingStudentGraduateList());
            if ( ReportBuilderManager.Items["延修生名冊"].Count == 0 )
                ReportBuilderManager.Items["延修生名冊"].Add(new ExtendingStudentList());
            if ( ReportBuilderManager.Items["轉入學生名冊"].Count == 0 )
                ReportBuilderManager.Items["轉入學生名冊"].Add(new TransferringStudentUpdateRecordList());

            new ListForm().ShowDialog();
        }
    }
}
