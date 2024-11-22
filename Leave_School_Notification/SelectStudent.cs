using FISCA.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using K12.Data;
using FISCA.Data;
namespace Leave_School_Notification
{
    public partial class SelectStudent : BaseForm
    {
        public SelectStudent()
        {
            InitializeComponent();
            lstClass.Items.Clear();
            foreach (ClassRecord cr in Class.SelectAll() )
            {
                if (cr.GradeYear >= 1 && cr.GradeYear <= 3)
                {
                    ListViewItem lvi = new ListViewItem(cr.ID);
                    lvi.SubItems.Add(cr.Name);
                    lstClass.Items.Add(lvi);
                }

            }
        }

        private void lstClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lstClass.Items.Count; i++)
            {
                if (lstClass.Items[i].Selected == true)
                {
                    lstStudent.Items.Clear();                    
                    foreach (StudentRecord sr in Student.SelectByClassID(lstClass.Items[i].SubItems[0].Text))
                        {
                        ListViewItem lvi = new ListViewItem(sr.StatusStr);
                        lvi.SubItems.Add(sr.StudentNumber);
                        lvi.SubItems.Add(sr.Name);
                        lvi.SubItems.Add(sr.ID);
                        lvi.SubItems.Add(lstClass.Items[i].SubItems[1].Text);
                        lstStudent.Items.Add(lvi);
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Boolean find = false;
            for (int i = 0; i < lstStudent.Items.Count; i++)
            {
                if (lstStudent.Items[i].Checked == true)
                {
                    find = false;
                    for (int j = 0; j < lstStudentR.Items.Count; j++)
                        if (lstStudentR.Items[j].SubItems[4].Text == lstStudent.Items[i].SubItems[3].Text)
                            find = true;
                    if (find == false)
                    {
                        ListViewItem lvi = new ListViewItem(lstStudent.Items[i].SubItems[0].Text);
                        lvi.SubItems.Add(lstStudent.Items[i].SubItems[4].Text);
                        lvi.SubItems.Add(lstStudent.Items[i].SubItems[1].Text);
                        lvi.SubItems.Add(lstStudent.Items[i].SubItems[2].Text);
                        lvi.SubItems.Add(lstStudent.Items[i].SubItems[3].Text);
                        lstStudentR.Items.Add(lvi);
                    }
                }               
            }
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            ListView newStudentView = new ListView();
            for (int i = 0; i < lstStudentR.Items.Count; i++)
            {
                if (lstStudentR.Items[i].Checked == false)
                {
                    ListViewItem lvi = new ListViewItem(lstStudentR.Items[i].SubItems[0].Text);
                    lvi.SubItems.Add(lstStudentR.Items[i].SubItems[1].Text);
                    lvi.SubItems.Add(lstStudentR.Items[i].SubItems[2].Text);                   
                    lvi.SubItems.Add(lstStudentR.Items[i].SubItems[3].Text);
                    lvi.SubItems.Add(lstStudentR.Items[i].SubItems[4].Text);
                    newStudentView.Items.Add(lvi);
                }
            }
            lstStudentR.Items.Clear();
            for (int i=0;i<newStudentView.Items.Count;i++)
            {
                ListViewItem lvi = new ListViewItem(newStudentView.Items[i].SubItems[0].Text);
                lvi.SubItems.Add(newStudentView.Items[i].SubItems[1].Text);
                lvi.SubItems.Add(newStudentView.Items[i].SubItems[2].Text);
                lvi.SubItems.Add(newStudentView.Items[i].SubItems[3].Text);
                lvi.SubItems.Add(newStudentView.Items[i].SubItems[4].Text);
                lstStudentR.Items.Add(lvi);
            }
        }

        private void txtStudentNumber_TextChanged(object sender, EventArgs e)
        {
            DataTable dtStudent = new DataTable();
            QueryHelper helper = new QueryHelper();
            string sql = "select student.status AS status, student.id AS studentid,class_name,student_number,name from student inner join class on student.ref_class_id=class.id where student_number='" + txtStudentNumber.Text + "' ";
            dtStudent = helper.Select(sql);
            Boolean find = false;
            string StatusStr = "";
            foreach (DataRow dr in dtStudent.Rows)
            {
                for (int i = 0; i < lstStudentR.Items.Count; i++)
                    if (lstStudentR.Items[i].SubItems[4].Text == dr["studentid"].ToString())
                        find = true;
                if (find == false)
                    {
                    switch (dr["status"].ToString())
                    {
                        case "1":
                            StatusStr = "一般";
                            break;
                        case "2":
                            StatusStr = "延修";
                            break;
                        case "4":
                            StatusStr = "休學";
                            break;
                        case "8":
                            StatusStr = "輟學";
                            break;
                        case "16":
                            StatusStr = "畢業或離校";
                            break;
                        case "256":
                            StatusStr = "刪除";
                            break;                           
                    }
                    ListViewItem lvi = new ListViewItem(StatusStr);
                    lvi.SubItems.Add(dr["class_name"].ToString());
                    lvi.SubItems.Add(dr["student_number"].ToString());
                    lvi.SubItems.Add(dr["name"].ToString());
                    lvi.SubItems.Add(dr["studentid"].ToString());
                    lstStudentR.Items.Add(lvi);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            休學期滿復學通知單 ctrdg = (休學期滿復學通知單)this.Owner;
            Boolean find = false;
            for (int i = 0; i < lstStudentR.Items.Count; i++)
            {
                find = false;
                for (int j = 0; j < ctrdg.lstStudent.Items.Count; j++)
                    if (lstStudentR.Items[i].SubItems[4].Text == ctrdg.lstStudent.Items[j].SubItems[6].Text)
                        find = true;
               
                if (find == false)
                {
                    ListViewItem lvi = new ListViewItem(lstStudentR.Items[i].SubItems[0].Text);
                    lvi.SubItems.Add(lstStudentR.Items[i].SubItems[1].Text);
                    lvi.SubItems.Add(lstStudentR.Items[i].SubItems[2].Text);
                    lvi.SubItems.Add(lstStudentR.Items[i].SubItems[3].Text);
                    lvi.SubItems.Add("");
                    lvi.SubItems.Add("");                    
                    lvi.SubItems.Add(lstStudentR.Items[i].SubItems[4].Text);                   
                    ctrdg.lstStudent.Items.Add(lvi);
                }
            }
            ctrdg.lblCount.Text = "共" + ctrdg.lstStudent.Items.Count.ToString() + "筆";
            this.Close();
        }
    }
}
