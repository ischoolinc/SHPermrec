using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using K12.Data.Configuration;
using FISCA.Data;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.Data.StudentExtension;
namespace Leave_School_Notification
{
    public partial class OutList : BaseForm
    {
        public OutList(DataTable _Source)
        {
            InitializeComponent();
            ResultMessage.DataSource = _Source;
            ResultMessage.Columns[0].Width = 0;
            ResultMessage.Columns[1].Width = 100;
            ResultMessage.Columns[2].Width = 70;
            ResultMessage.Columns[3].Width = 100;
            ResultMessage.Columns[4].Width = 100;
            ResultMessage.Columns[5].Width = 70;
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void AddTemp_Click(object sender, EventArgs e)
        {
            List<string> studentlist = new List<string>();

            for (int i = ResultMessage.Rows.Count - 1; i >= 0; i--)
            {
                studentlist.Add(ResultMessage.Rows[i].Cells[0].Value.ToString());
            }
                K12.Presentation.NLDPanels.Student.AddToTemp(studentlist);
            }
        
    }
}
