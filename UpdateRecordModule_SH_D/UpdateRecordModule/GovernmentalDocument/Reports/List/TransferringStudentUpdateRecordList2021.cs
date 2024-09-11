using System.IO;
using System.Xml;
using Aspose.Cells;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UpdateRecordModule_SH_D.GovernmentalDocument.Reports.List
{
    public class TransferringStudentUpdateRecordList2021 : ReportBuilder
    {
       
        protected override void Build(XmlElement source, string location)
        {
            Workbook template = new Workbook();

            //�qResources��TemplateŪ�X��
            template.Open(new MemoryStream(Properties.Resources.TransferringStudentUpdateRecordListTemplate), FileFormatType.Xlsx);

            //�n���ͪ�excel��
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(Properties.Resources.TransferringStudentUpdateRecordListTemplate), FileFormatType.Xlsx);

            Worksheet ws = wb.Worksheets[0];

            //�������j�X��row
            int next = 23;

            //�������X��col
            int col = 14;

            //���row�ƥ�
            int dataRow = 16;

            //����
            int index = 0;

            //�d���d��
            Range tempRange = template.Worksheets[0].Cells.CreateRange(0,23,false);

            //�`�@�X�����ʬ���
            int count = 0;
            int totalRec = source.SelectNodes("�M��/���ʬ���").Count;

            foreach (XmlNode list in source.SelectNodes("�M��"))
            {
                //���ͲM��Ĥ@��
                ws.Cells.CreateRange(index, next, false).Copy(tempRange);
                ws.Cells.CreateRange(index, next, false).CopyData(tempRange);
                ws.Cells.CreateRange(index, next, false).CopyStyle(tempRange);
                //Page
                int currentPage = 1;
                int totalPage = (list.ChildNodes.Count / dataRow) + 1;


                //�g�J�N��
                ws.Cells[index, 11].PutValue(source.SelectSingleNode("@�ǮեN��").InnerText + "-" + list.SelectSingleNode("@��O�N��").InnerText);

                //�g�J�զW�B�Ǧ~�סB�Ǵ��B��O�B�~��
                ws.Cells[index + 2, 1].PutValue(source.SelectSingleNode("@�ǮզW��").InnerText);
                ws.Cells[index + 2, 5].PutValue(source.SelectSingleNode("@�Ǧ~��").InnerText + " �Ǧ~�� �� " + source.SelectSingleNode("@�Ǵ�").InnerText + " �Ǵ�");
                ws.Cells[index + 2, 8].PutValue(list.SelectSingleNode("@��O").InnerText);
                ws.Cells[index + 2, 12].PutValue(list.SelectSingleNode("@�~��").InnerText + "�~��");

                //�g�J���
                int recCount = 0;
                int dataIndex = index + 6;
                for (; currentPage <= totalPage; currentPage++)
                {
                    //�ƻs����
                    if (currentPage + 1 <= totalPage)
                    {
                        ws.Cells.CreateRange(index + next, next, false).Copy(tempRange);
                        ws.Cells.CreateRange(index + next, next, false).CopyData(tempRange);
                        ws.Cells.CreateRange(index + next, next, false).CopyStyle(tempRange);
                    }

                    //��J��� (2018/3/6 �o�~���ѡAlist.ChildNodes.Count-1 �]���n�����@�� ���ʦW�U�ʭ� ���)
                    for (int i = 0; i < dataRow && recCount < list.ChildNodes.Count-1; i++, recCount++)
                    {
                        //MsgBox.Show(i.ToString()+" "+recCount.ToString());
                        XmlNode rec = list.SelectNodes("���ʬ���")[recCount];
                        
                        if(rec.SelectSingleNode("@�s�Ǹ�")!=null)
                        if(string.IsNullOrEmpty(rec.SelectSingleNode("@�s�Ǹ�").InnerText))
                            if(rec.SelectSingleNode("@�Ǹ�")!=null)
                                ws.Cells[dataIndex, 0].PutValue(rec.SelectSingleNode("@�Ǹ�").InnerText);
                        else
                            ws.Cells[dataIndex, 0].PutValue(rec.SelectSingleNode("@�s�Ǹ�").InnerText);

                        ws.Cells[dataIndex, 1].PutValue(rec.SelectSingleNode("@�m�W").InnerText);
                        ws.Cells[dataIndex, 2].PutValue(rec.SelectSingleNode("@�����Ҹ�").InnerText.ToString());
                        ws.Cells[dataIndex, 3].PutValue(rec.SelectSingleNode("@�ʧO�N��").InnerText);
                        ws.Cells[dataIndex, 4].PutValue(rec.SelectSingleNode("@�ʧO").InnerText);
                        ws.Cells[dataIndex, 5].PutValue(rec.SelectSingleNode("@�X�ͦ~���").InnerText);
                        ws.Cells[dataIndex, 6].PutValue(rec.SelectSingleNode("@��J�e�ǥ͸��_�Ǯ�").InnerText);
                        ws.Cells[dataIndex, 7].PutValue(rec.SelectSingleNode("@��J�e�ǥ͸��_�Ǹ�").InnerText + "\n" + rec.SelectSingleNode("@��J�e�ǥ͸��_��O").InnerText);
                        ws.Cells[dataIndex, 8].PutValue(BL.Util.ConvertDateStr2(rec.SelectSingleNode("@��J�e�ǥ͸��_�Ƭd���").InnerText) + "\n" + rec.SelectSingleNode("@��J�e�ǥ͸��_�Ƭd�帹").InnerText);
                        ws.Cells[dataIndex, 9].PutValue(rec.SelectSingleNode("@��J�e�ǥ͸��_�~��").InnerText);
                        ws.Cells[dataIndex, 10].PutValue(rec.SelectSingleNode("@���ʥN��").InnerText);
                        ws.Cells[dataIndex, 11].PutValue(rec.SelectSingleNode("@��]�Ψƶ�").InnerText);
                        ws.Cells[dataIndex, 12].PutValue(BL.Util.ConvertDateStr2(rec.SelectSingleNode("@���ʤ��").InnerText));

                        //ws.Cells[dataIndex, 13].PutValue(rec.SelectSingleNode("@�Ƶ�").InnerText);
                        if(rec.SelectSingleNode("@�S�����N�X")!=null)
                            ws.Cells[dataIndex, 13].PutValue(rec.SelectSingleNode("@�S�����N�X").InnerText);

                        dataIndex++;
                        count++;

                        //��J�e�ǥ͸��_�Ǯ�="�|������" ��J�e�ǥ͸��_�Ǹ�="010101" ��J�e�ǥ͸��_��O="��T��" ��J�e�ǥ͸��_�Ƭd���="90/09/09" ��J�e�ǥ͸��_�Ƭd�帹="�Ф��T�r��09200909090��" ��J�e�ǥ͸��_�~��="�@�W"
                    }

                    //�p��X�p
                    if (currentPage == totalPage)
                    {
                        ws.Cells.CreateRange(dataIndex, 0, 1, 2).Merge();
                        ws.Cells[dataIndex, 0].PutValue("�X�p " + (list.ChildNodes.Count-1).ToString() + " �W");
                    }

                    //����
                    ws.Cells[index + next -1, 10].PutValue("�� " + currentPage + " ���A�@ " + totalPage + " ��");
                    ws.HPageBreaks.Add(index + next, col);

                    //���ޫ��V�U�@��
                    index += next;
                    dataIndex = index + 6;

                    //�^���i��
                    ReportProgress((int)(((double)count * 100.0) / ((double)totalRec)));
                }
            }


            #region ��J��,�q�l�榡

            Worksheet TemplateWb = wb.Worksheets["�q�l�榡�d��"];

            Worksheet DyWb = wb.Worksheets[wb.Worksheets.Add()];
            DyWb.Name = "��J�ͦW�U";

            Range range_H = TemplateWb.Cells.CreateRange(0, 1, false);
            Range range_R = TemplateWb.Cells.CreateRange(1, 1, false);

            // 107�s�榡 ������n ��End �r��
            Range range_R_EndRow = TemplateWb.Cells.CreateRange(2, 1, false);
            DyWb.Cells.CreateRange(0, 1, false).CopyData(range_H);
            DyWb.Cells.CreateRange(0, 1, false).CopyStyle(range_H);

            int DyWb_index = 0;
            DAL.DALTransfer DALTranser = new DAL.DALTransfer();

            // �榡�ഫ
            List<GovernmentalDocument.Reports.List.rpt_UpdateRecord> _data = DALTranser.ConvertRptUpdateRecord(source);

            // �Ƨ� (�� �Z�O�B�~�šB��O�N�X�B�Ǹ�)
            _data =(from data in _data orderby data.ClassType,data.GradeYear,data.DeptCode,data.StudentNumber select data).ToList ();

            foreach (GovernmentalDocument.Reports.List.rpt_UpdateRecord rec in _data)
            {
                DyWb_index++;
                //�C�W�[�@��,�ƻs�@��
                DyWb.Cells.CreateRange(DyWb_index, 1, false).CopyStyle(range_R);

                //�Z�O
                DyWb.Cells[DyWb_index, 0].PutValue(rec.ClassType);
                //��O�N�X
                DyWb.Cells[DyWb_index, 1].PutValue(rec.DeptCode);

                //�W�����O
                DyWb.Cells[DyWb_index, 2].PutValue(rec.UpdateType);
                //�Ǹ�
                if (string.IsNullOrEmpty(rec.NewStudNumber))
                    DyWb.Cells[DyWb_index, 3].PutValue(rec.StudentNumber);
                else
                    DyWb.Cells[DyWb_index, 3].PutValue(rec.NewStudNumber);

                //�m�W
                DyWb.Cells[DyWb_index, 4].PutValue(rec.Name);
                //�����Ҧr��
                DyWb.Cells[DyWb_index, 5].PutValue(rec.IDNumber);
                //��1
                DyWb.Cells[DyWb_index, 6].PutValue(rec.Comment1);
                //�ʧO�N�X
                DyWb.Cells[DyWb_index, 7].PutValue(rec.GenderCode);
                //�X�ͤ��
                DyWb.Cells[DyWb_index, 8].PutValue(rec.Birthday);
                //�S�����N�X
                DyWb.Cells[DyWb_index, 9].PutValue(rec.SpecialStatusCode);
                //�~��
                DyWb.Cells[DyWb_index, 10].PutValue(rec.GradeYear);
                //���ʭ�]�N�X
                DyWb.Cells[DyWb_index, 11].PutValue(rec.UpdateCode);
                //��J���
                DyWb.Cells[DyWb_index, 12].PutValue(rec.UpdateDate);
                // ��J�����O
                DyWb.Cells[DyWb_index, 13].PutValue(rec.TransferStatus);

                //��Ƭd���
                DyWb.Cells[DyWb_index, 14].PutValue(rec.PreviousSchoolLastADDate);
                //��Ƭd��r(*)
                DyWb.Cells[DyWb_index, 15].PutValue(rec.PreviousSchoolLastADDoc);
                //��Ƭd�帹(*)
                DyWb.Cells[DyWb_index, 16].PutValue(rec.PreviousSchoolLastADNum);
                //��ǮեN�X(*)
                DyWb.Cells[DyWb_index, 17].PutValue(rec.PreviousSchoolCode);
                //���O�N�X
                DyWb.Cells[DyWb_index, 18].PutValue(rec.PreviousDeptCode);
                //��Ǹ�
                DyWb.Cells[DyWb_index, 19].PutValue(rec.PreviousStudentNumber);
                
                // ���䴩�µ��c�~�ŻP�Ǵ��O�Τ�r�r��@�W�A�ҥH�o�˼g
                //��~��
                DyWb.Cells[DyWb_index, 20].PutValue(Getyear(rec.PreviousGradeYear));
                //��Ǵ�
                DyWb.Cells[DyWb_index, 21].PutValue(Getsemester(rec.PreviousSemester));

                //�رй��ͱM�Z�ǥͰ�O
                DyWb.Cells[DyWb_index, 22].PutValue(rec.OverseasChineseStudentCountryCode);

                //�Ƶ�����
                DyWb.Cells[DyWb_index, 23].PutValue(rec.Comment);            
            }

            // ��ƥ��� �[End
            DyWb.Cells.CreateRange(DyWb_index + 1, 1, false).CopyData(range_R_EndRow);
            DyWb.Cells.CreateRange(DyWb_index + 1, 1, false).CopyStyle(range_R_EndRow);
            DyWb.AutoFitColumns();

            wb.Worksheets.RemoveAt("�q�l�榡�d��");

            #endregion

            //2018/3/6 �o�~ �s�W��J�� �ʭ��榡�䴩

            //�d��
            Worksheet TemplateWb_Cover = wb.Worksheets["��J�ͦW�U�ʭ��d��"];

            //�갵����
            Worksheet cover = wb.Worksheets[wb.Worksheets.Add()];

            //�W��
            cover.Name = "��J�ͦW�U�ʭ�";
            
            string school_code = source.SelectSingleNode("@�ǮեN��").InnerText;
            string school_year = source.SelectSingleNode("@�Ǧ~��").InnerText;
            string school_semester = source.SelectSingleNode("@�Ǵ�").InnerText;

            //�d��
            Range range_H_Cover = TemplateWb_Cover.Cells.CreateRange(0, 1, false);

            //range_H_Cover
            cover.Cells.CreateRange(0, 1, false).CopyData(range_H_Cover);
            cover.Cells.CreateRange(0, 1, false).CopyStyle(range_H_Cover);

            Range range_R_cover = TemplateWb_Cover.Cells.CreateRange(1, 1, false);
            // 107�s�榡 ������n ��End �r��
            Range range_R_cover_EndRow = TemplateWb_Cover.Cells.CreateRange(2, 1, false);

            int cover_row_counter = 1;
            
            foreach (XmlNode list in source.SelectNodes("�M��"))
            {
                //�C�W�[�@��,�ƻs�@��
                cover.Cells.CreateRange(cover_row_counter, 1, false).CopyStyle(range_R_cover);

                string gradeYear = list.SelectSingleNode("@�~��").InnerText;
                string deptCode = list.SelectSingleNode("@��O�N�X").InnerText;
                string classType = list.SelectSingleNode("@�Z�O").InnerText;

                //�ǮեN�X
                cover.Cells[cover_row_counter, 0].PutValue(school_code);
                //�Ǧ~��
                cover.Cells[cover_row_counter, 1].PutValue(school_year);
                //�Ǵ�
                cover.Cells[cover_row_counter, 2].PutValue(school_semester);
                //�~��
                cover.Cells[cover_row_counter, 3].PutValue(gradeYear);
                //��O�N�X
                cover.Cells[cover_row_counter, 6].PutValue(deptCode);

                foreach (XmlElement st in list.SelectNodes("���ʦW�U�ʭ�"))
                {
                    string reportType = st.SelectSingleNode("@�W�U�O").InnerText;
                    //string classType = st.SelectSingleNode("@�Z�O").InnerText;
                    string updateType = st.SelectSingleNode("@�W�����O").InnerText;
                    string approvedClassCount = st.SelectSingleNode("@�֩w�Z��").InnerText;
                    string approvedStudentCount = st.SelectSingleNode("@�֩w�ǥͼ�").InnerText;
                    string actualClassCount = st.SelectSingleNode("@��ۯZ��").InnerText;
                    string actualStudentCount = st.SelectSingleNode("@��۷s�ͼ�").InnerText;
                    string originalStudentCount = st.SelectSingleNode("@�즳�ǥͼ�").InnerText;
                    string transferStudentCount = st.SelectSingleNode("@��J�ǥͼ�").InnerText;                    
                    string currentStudentCount = st.SelectSingleNode("@�{���ǥͼ�").InnerText;
                    string remarks1 = st.SelectSingleNode("@��1").InnerText;
                    string remarksContent = st.SelectSingleNode("@�Ƶ�����").InnerText;

                    //�W�U�O
                    cover.Cells[cover_row_counter, 4].PutValue(reportType);
                    //�Z�O
                    cover.Cells[cover_row_counter, 5].PutValue(classType);
                    //�W�����O
                    cover.Cells[cover_row_counter, 7].PutValue(updateType);
                    //�֩w�Z��
                    cover.Cells[cover_row_counter, 8].PutValue(approvedClassCount);
                    //�֩w�ǥͼ�
                    cover.Cells[cover_row_counter, 9].PutValue(approvedStudentCount);
                    //��ۯZ��
                    cover.Cells[cover_row_counter, 10].PutValue(actualClassCount);
                    //��۷s�ͼ�
                    cover.Cells[cover_row_counter, 11].PutValue(actualStudentCount);
                    //�즳�ǥͼ�
                    cover.Cells[cover_row_counter, 12].PutValue(originalStudentCount);
                    //��J�ǥͼ�
                    cover.Cells[cover_row_counter, 13].PutValue(transferStudentCount);                    
                    //�{���ǥͼ�
                    cover.Cells[cover_row_counter, 14].PutValue(currentStudentCount);
                    //��1
                    cover.Cells[cover_row_counter, 15].PutValue(remarks1);
                    //�Ƶ�����
                    cover.Cells[cover_row_counter, 16].PutValue(remarksContent);
                }
                cover_row_counter++;
            }

            // ��ƥ��� �[End
            cover.Cells.CreateRange(cover_row_counter, 1, false).CopyStyle(range_R_cover_EndRow);
            cover.Cells.CreateRange(cover_row_counter, 1, false).CopyData(range_R_cover_EndRow);

            wb.Worksheets.RemoveAt("��J�ͦW�U�ʭ��d��");

            wb.Worksheets.ActiveSheetIndex = 0;

            //�x�s
            try
            {
                wb.Save(location, SaveFormat.Xlsx);
                System.Diagnostics.Process.Start(location);
            }
            catch
            {
                MessageBox.Show("�ɮ��x�s����");
            }

        }


        #region ������

        //���~��
        #region ���~��

        private string Getyear(string year)
        {

            if (year.Contains("�@"))
            {
                return "1";
            }
            else if (year.Contains("�G"))
            {
                return "2";
            }
            else if (year.Contains("�T"))
            {
                return "3";
            }
            else if (year.Contains("�|"))
            {
                return "4";
            }
            else
            {
                return year;
            }

        }

        #endregion


        //���Ǵ�
        #region ���Ǵ�

        private string Getsemester(string sem)
        {

            if (sem.Contains("�W"))
            {
                return "1";
            }
            else if (sem.Contains("�U"))
            {
                return "2";
            }
            else
            {
                return sem;
            }
        }

        #endregion


        //����r
        #region ����r

        private string GetNumAndSrt1(string fuct)
        {
            if (fuct.Contains("�r"))
            {
                return fuct.Remove(fuct.LastIndexOf("�r"));
            }
            return fuct;
        }

        #endregion

        //���帹
        #region ���帹

        private string GetNumAndSrt2(string fuct)
        {

            if (fuct.Contains("��") && fuct.Contains("��"))
            {
                return fuct.Substring(fuct.LastIndexOf("��") + 1, fuct.LastIndexOf("��") - fuct.LastIndexOf("��") - 1);
            }
            return fuct;

        }

        #endregion

        //�褸�����~
        #region �褸�����~
        private string GetBirthdateWithoutSlash(string orig)
        {
            if (string.IsNullOrEmpty(orig)) return orig;
            string[] array = orig.Split('/');
            int chang;
            if (array[0].Length == 4)
            {
                chang = int.Parse(array[0]) - 1911;
            }
            else
            {
                chang = int.Parse(array[0]);
            }
            return chang.ToString() + array[1].PadLeft(2, '0') + array[2].PadLeft(2, '0');
        }
        #endregion 

        #endregion



        public override string Copyright
        {
            get { return "IntelliSchool"; }
        }

        public override string Description
        {
            get { return "�����줽��95�~11��s�L�޲z��U�W�d�榡"; }
        }

        public override string ReportName
        {
            get { return "��J�ǥͦW�U"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }
    }
}
