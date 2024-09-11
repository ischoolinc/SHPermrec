using Aspose.Cells;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using UpdateRecordModule_SH_D.BL;
using System.Windows.Forms;
namespace UpdateRecordModule_SH_D.GovernmentalDocument.Reports.List
{
    public class ExtendingStudentGraduateList2021 : ReportBuilder
    {
        
        protected override void Build(XmlElement source, string location)
        {
            Workbook template = new Workbook();

            //�qResources��TemplateŪ�X��
            template.Open(new MemoryStream(Properties.Resources.ExtendingGraduatingStudentListTemplate), FileFormatType.Xlsx);

            //�n���ͪ�excel��
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(Properties.Resources.ExtendingGraduatingStudentListTemplate), FileFormatType.Xlsx);

            Worksheet ws = wb.Worksheets[0];

            //�������j�X��row
            int next = 24;

            //����
            int index = 0;

            //�d���d��
            Range tempRange = template.Worksheets[0].Cells.CreateRange(0, 24, false);

            //�`�@�X�����ʬ���
            int count = 0;
            int totalRec = source.SelectNodes("�M��/���ʬ���").Count;

            // ���o�W�U���s���̫Ყ�ʥN�X���
            Dictionary<string, string> LastCodeDict = new Dictionary<string, string>();



            foreach (XmlNode list in source.SelectNodes("�M��"))
            {
                //���ͲM��Ĥ@��
                //for (int row = 0; row < next; row++)
                //{
                //    ws.Cells.CopyRow(template.Worksheets[0].Cells, row, row + index);
                //}
                ws.Cells.CreateRange(index, 24, false).Copy(tempRange);
                ws.Cells.CreateRange(index, 24, false).CopyData(tempRange);
                ws.Cells.CreateRange(index, 24, false).CopyStyle(tempRange);
                //Page
                int currentPage = 1;
                int totalPage = (list.ChildNodes.Count / 18) + 1;

                //�g�J�W�U���O
                if (source.SelectSingleNode("@���O").InnerText == "���ץͲ��~�W�U"||source.SelectSingleNode("@���O").InnerText == "���ץͲ��~�W�U_2021��")
                    ws.Cells[index, 0].PutValue(ws.Cells[index, 0].StringValue.Replace("�����~", "�����~"));
                else
                    ws.Cells[index, 0].PutValue(ws.Cells[index, 0].StringValue.Replace("�����~", "�����~"));

                //�g�J�N��
                ws.Cells[index, 6].PutValue("�N�X�G" + source.SelectSingleNode("@�ǮեN��").InnerText + "-" + list.SelectSingleNode("@��O�N��").InnerText);

                //�g�J�զW�B�Ǧ~�סB�Ǵ��B��O
                ws.Cells[index + 2, 0].PutValue("�զW�G" + source.SelectSingleNode("@�ǮզW��").InnerText);
                ws.Cells[index + 2, 4].PutValue(source.SelectSingleNode("@�Ǧ~��").InnerText + "�Ǧ~�� ��" + source.SelectSingleNode("@�Ǵ�").InnerText + "�Ǵ�");
                ws.Cells[index + 2, 6].PutValue(list.SelectSingleNode("@��O").InnerText);

                //�g�J���
                int recCount = 0;
                int dataIndex = index + 5;
                for (; currentPage <= totalPage; currentPage++)
                {
                    //�ƻs����
                    if (currentPage + 1 <= totalPage)
                    {
                        //for (int row = 0; row < next; row++)
                        //{
                        //    ws.Cells.CopyRow(ws.Cells, row + index, row + index + next);
                        //}
                        ws.Cells.CreateRange(index + next, 24, false).Copy(tempRange);
                        ws.Cells.CreateRange(index + next, 24, false).CopyData(tempRange);
                        ws.Cells.CreateRange(index + next, 24, false).CopyStyle(tempRange);
                    }

                    int updateCount = list.SelectNodes("���ʬ���").Count;

                    //��J���
                    for (int i = 0; i < 18 && recCount < updateCount; i++, recCount++)
                    {
                        //MsgBox.Show(i.ToString()+" "+recCount.ToString());
                        XmlNode rec = list.SelectNodes("���ʬ���")[recCount];
                        ws.Cells[dataIndex, 0].PutValue(rec.SelectSingleNode("@�Ǹ�").InnerText + "\n" + rec.SelectSingleNode("@�m�W").InnerText);
                        ws.Cells[dataIndex, 1].PutValue(rec.SelectSingleNode("@�ʧO�N��").InnerText.ToString());
                        ws.Cells[dataIndex, 2].PutValue(rec.SelectSingleNode("@�ʧO").InnerText);
                        string ssn = rec.SelectSingleNode("@�����Ҹ�").InnerText;
                        if (ssn == "")
                            ssn = rec.SelectSingleNode("@�����Ҹ�").InnerText;

                        if (!LastCodeDict.ContainsKey(ssn))
                            LastCodeDict.Add(ssn, rec.SelectSingleNode("@�̫Ყ�ʥN��").InnerText.ToString());

                        ws.Cells[dataIndex, 3].PutValue(Util.ConvertDateStr2(rec.SelectSingleNode("@�ͤ�").InnerText) + "\n" + ssn);
                        ws.Cells[dataIndex, 4].PutValue(rec.SelectSingleNode("@�̫Ყ�ʥN��").InnerText.ToString());
                        //if (rec.SelectSingleNode("@���{�s�r��").InnerText == "")
                        //{
                            ws.Cells[dataIndex, 5].PutValue(Util.ConvertDateStr2(rec.SelectSingleNode("@�Ƭd���").InnerText) + "\n" + rec.SelectSingleNode("@�Ƭd�帹").InnerText);
                        //}
                        //else
                        //{
                        //    ws.Cells[dataIndex, 5].PutValue(Util.ConvertDateStr2(rec.SelectSingleNode("@���{�s`���").InnerText) + "\n" + rec.SelectSingleNode("@���{�s�ǲ�").InnerText + rec.SelectSingleNode("@���{�s�r��").InnerText);
                        //}
                        ws.Cells[dataIndex, 6].PutValue(rec.SelectSingleNode("@���~�ҮѦr��").InnerText);

                        //ws.Cells[dataIndex, 7].PutValue(rec.SelectSingleNode("@�Ƶ�").InnerText);
                        if (rec.SelectSingleNode("@�S�����N�X") != null)
                            ws.Cells[dataIndex, 7].PutValue(rec.SelectSingleNode("@�S�����N�X").InnerText);

                        dataIndex++;
                        count++;
                    }

                    //�p��X�p
                    if (currentPage == totalPage)
                    {
                        ws.Cells[index + 22, 0].PutValue("�X�p");
                        ws.Cells[index + 22, 1].PutValue(updateCount.ToString());
                    }

                    //����
                    ws.Cells[index + 23, 6].PutValue("�� " + currentPage + " ���A�@ " + totalPage + " ��");
                    ws.HPageBreaks.Add(index + 24, 8);

                    //���ޫ��V�U�@��
                    index += next;
                    dataIndex = index + 5;

                    //�^���i��
                    ReportProgress((int)(((double)count * 100.0) / ((double)totalRec)));
                }
            }

            //�d��
            //�d��
            Worksheet TemplateWb_Cover = wb.Worksheets["���ץͲ��~�W�U�ʭ��d��"];

            //�갵����
            Worksheet cover = wb.Worksheets[wb.Worksheets.Add()];

            //�W��
            cover.Name = "���ץͲ��~�W�U�ʭ�";

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

            //2018/2/2 �o�~���� �A�U���O�s���ʭ����ͤ覡

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

                //��O�N�X
                cover.Cells[cover_row_counter, 6].PutValue(deptCode);

                foreach (XmlElement st in list.SelectNodes("���ʦW�U�ʭ�"))
                {
                    string reportType = st.SelectSingleNode("@�W�U�O").InnerText;
                    string scheduledGraduateYear = st.SelectSingleNode("@�����~�Ǧ~��") != null ? st.SelectSingleNode("@�����~�Ǧ~��").InnerText : "";
                    //string classType = st.SelectSingleNode("@�Z�O").InnerText;
                    string updateType = st.SelectSingleNode("@�W�����O").InnerText;
                    string approvedExtendingStudentCount = st.SelectSingleNode("@���ɩ��׾ǥͼ�").InnerText;
                    string waitingExtendingStudentCount = st.SelectSingleNode("@���ӽЩ��׾ǥͼ�").InnerText;
                    string originalStudentCount = st.SelectSingleNode("@�즳�ǥͼ�").InnerText;
                    string currentStudentCount = st.SelectSingleNode("@�{���ǥͼ�").InnerText;
                    string graduatedStudentCount = st.SelectSingleNode("@���~�ǥͼ�").InnerText;
                    string remarksContent = st.SelectSingleNode("@�Ƶ�����").InnerText;

                    //�W�U�O
                    cover.Cells[cover_row_counter, 3].PutValue(reportType);
                    //�����~�Ǧ~��
                    cover.Cells[cover_row_counter, 4].PutValue(scheduledGraduateYear);
                    //�Z�O
                    cover.Cells[cover_row_counter, 5].PutValue(classType);
                    //�W�����O
                    cover.Cells[cover_row_counter, 7].PutValue(updateType);
                    //���ɩ��׾ǥͼ�
                    cover.Cells[cover_row_counter, 8].PutValue(approvedExtendingStudentCount);
                    //���ӽЩ��׾ǥͼ�
                    cover.Cells[cover_row_counter, 9].PutValue(waitingExtendingStudentCount);
                    //�즳�ǥͼ�
                    cover.Cells[cover_row_counter, 10].PutValue(originalStudentCount);
                    //�{���ǥͼ�
                    cover.Cells[cover_row_counter, 11].PutValue(currentStudentCount);
                    //���~�ǥͼ�
                    cover.Cells[cover_row_counter, 12].PutValue(graduatedStudentCount);
                    //�Ƶ�����
                    cover.Cells[cover_row_counter, 13].PutValue(remarksContent);

                }
                cover_row_counter++;
            }

            // ��ƥ��� �[End
            cover.Cells.CreateRange(cover_row_counter, 1, false).CopyData(range_R_cover_EndRow);
            cover.Cells.CreateRange(cover_row_counter, 1, false).CopyStyle(range_R_cover_EndRow);
            //�d��
            Worksheet TemplateWb = wb.Worksheets["�q�l�榡�d��"];
            //�갵����
            Worksheet mdws = wb.Worksheets[wb.Worksheets.Add()];
            //�W��
            mdws.Name = "���ץͲ��~�W�U";
            //�d��
            Range range_H = TemplateWb.Cells.CreateRange(0, 1, false);
            Range range_R = TemplateWb.Cells.CreateRange(1, 1, false);
            // 107�s�榡 ������n ��End �r��
            Range range_R_EndRow = TemplateWb.Cells.CreateRange(2, 1, false);
            //����range_H
            mdws.Cells.CreateRange(0, 1, false).CopyData(range_H);
            mdws.Cells.CreateRange(0, 1, false).CopyStyle(range_H);

            int mdws_index = 0;

            DAL.DALTransfer DALTranser = new DAL.DALTransfer();

            // �榡�ഫ
            List<GovernmentalDocument.Reports.List.rpt_UpdateRecord> _data = DALTranser.ConvertRptUpdateRecord(source);

            // �Ƨ� (�� �Z�O�B�~�šB��O�N�X�B���ʥN�X�B���~�ҮѦr��)
            _data = (from data in _data orderby data.ClassType, data.DeptCode, data.UpdateCode,data.GraduateCertificateNumber select data).ToList();

            foreach (GovernmentalDocument.Reports.List.rpt_UpdateRecord rec in _data)
            {
                mdws_index++;
                //�C�W�[�@��,�ƻs�@��
                mdws.Cells.CreateRange(mdws_index, 1, false).CopyStyle(range_R);
                
                //�����~�Ǧ~��
                mdws.Cells[mdws_index, 0].PutValue(rec.ExpectGraduateSchoolYear);

                //�Z�O
                mdws.Cells[mdws_index, 1].PutValue(rec.ClassType);
                //��O�N�X
                mdws.Cells[mdws_index, 2].PutValue(rec.DeptCode);

                // 2 ��W�����O
                //�W�����O
                mdws.Cells[mdws_index, 3].PutValue(rec.UpdateType);

                //�Ǹ�
                mdws.Cells[mdws_index, 4].PutValue(rec.StudentNumber);
                //�m�W
                mdws.Cells[mdws_index, 5].PutValue(rec.Name);
                //�����Ҧr��
                mdws.Cells[mdws_index, 6].PutValue(rec.IDNumber);

                //��1
                mdws.Cells[mdws_index, 7].PutValue(rec.Comment1);

                //�ʧO�N�X
                mdws.Cells[mdws_index, 8].PutValue(rec.GenderCode);
                //�X�ͤ��
                mdws.Cells[mdws_index, 9].PutValue(rec.Birthday);

                //�S�����N�X
                mdws.Cells[mdws_index, 10].PutValue(rec.SpecialStatusCode);

                //���ʭ�]�N�X
                if (LastCodeDict.ContainsKey(rec.IDNumber))
                    mdws.Cells[mdws_index, 11].PutValue(LastCodeDict[rec.IDNumber]);
                else
                    mdws.Cells[mdws_index, 11].PutValue(rec.UpdateCode);

                //if (rec.temp_number == "")
                //{
                    //�Ƭd��r
                    mdws.Cells[mdws_index, 12].PutValue(rec.LastADDoc);
                    //�Ƭd�帹
                    mdws.Cells[mdws_index, 13].PutValue(rec.LastADNum);

                    //�Ƭd���
                    mdws.Cells[mdws_index, 14].PutValue(rec.LastADDate);
                //}
                //else
                //{
                //    //�{�s�ǲ�
                //    mdws.Cells[mdws_index, 12].PutValue(rec.origin_temp_desc);
                //    //�{�s�ǲΤ帹
                //    mdws.Cells[mdws_index, 13].PutValue(rec.origin_temp_number);

                //    //�{�s�ǲΤ��
                //    mdws.Cells[mdws_index, 14].PutValue(rec.origin_temp_date);
                //};

                //���~�ҮѦr��
                mdws.Cells[mdws_index, 15].PutValue(rec.GraduateCertificateNumber);

                //���~�Үѵ��O�ǵ{�N�X (2019/02/15 �o�~ �ˬd�o�{ �ثe�ڭ̨t�ΨS���䴩�o�ӷ����A�n�A��s)
                mdws.Cells[mdws_index, 16].PutValue("");

                //�Ƶ�����
                mdws.Cells[mdws_index, 17].PutValue(rec.Comment);

            }

            // ��ƥ��� �[End
            mdws.Cells.CreateRange(mdws_index + 1, 1, false).CopyData(range_R_EndRow);
            mdws.Cells.CreateRange(mdws_index + 1, 1, false).CopyStyle(range_R_EndRow);
            //�d��
            TemplateWb = wb.Worksheets["�q�l�榡�d��_�t�{�s"];
            //�갵����
            mdws = wb.Worksheets[wb.Worksheets.Add()];
            //�W��
            mdws.Name = "���ץͲ��~�W�U_�t�{�s";
            //�d��
            range_H = TemplateWb.Cells.CreateRange(0, 1, false);
            range_R = TemplateWb.Cells.CreateRange(1, 1, false);
            // 107�s�榡 ������n ��End �r��
            range_R_EndRow = TemplateWb.Cells.CreateRange(2, 1, false);
            //����range_H
            mdws.Cells.CreateRange(0, 1, false).CopyData(range_H);
            mdws.Cells.CreateRange(0, 1, false).CopyStyle(range_H);
            mdws_index = 0;

           

            foreach (GovernmentalDocument.Reports.List.rpt_UpdateRecord rec in _data)
            {
                mdws_index++;
                //�C�W�[�@��,�ƻs�@��
                mdws.Cells.CreateRange(mdws_index, 1, false).CopyStyle(range_R);

                //�����~�Ǧ~��
                mdws.Cells[mdws_index, 0].PutValue(rec.ExpectGraduateSchoolYear);

                //�Z�O
                mdws.Cells[mdws_index, 1].PutValue(rec.ClassType);
                //��O�N�X
                mdws.Cells[mdws_index, 2].PutValue(rec.DeptCode);

                // 2 ��W�����O�A�ШϥΪ̦۶� 
                //�W�����O
                mdws.Cells[mdws_index, 3].PutValue(rec.UpdateType);
                //�Ǹ�
                mdws.Cells[mdws_index, 4].PutValue(rec.StudentNumber);
                //�m�W
                mdws.Cells[mdws_index, 5].PutValue(rec.Name);
                //�����Ҧr��
                mdws.Cells[mdws_index, 6].PutValue(rec.IDNumber);

                //��1
                mdws.Cells[mdws_index, 7].PutValue(rec.Comment1);

                //�ʧO�N�X
                mdws.Cells[mdws_index, 8].PutValue(rec.GenderCode);
                //�X�ͤ��
                mdws.Cells[mdws_index, 9].PutValue(rec.Birthday);

                //�S�����N�X
                mdws.Cells[mdws_index, 10].PutValue(rec.SpecialStatusCode);

                //���ʭ�]�N�X
                if (LastCodeDict.ContainsKey(rec.IDNumber))
                    mdws.Cells[mdws_index, 11].PutValue(LastCodeDict[rec.IDNumber]);
                else
                    mdws.Cells[mdws_index, 11].PutValue(rec.UpdateCode);

                
                //�Ƭd��r
                mdws.Cells[mdws_index, 12].PutValue(rec.LastADDoc);
                //�Ƭd�帹
                mdws.Cells[mdws_index, 13].PutValue(rec.LastADNum);

                //�Ƭd���
                mdws.Cells[mdws_index, 14].PutValue(rec.LastADDate);
                

                //���~�ҮѦr��
                mdws.Cells[mdws_index, 15].PutValue(rec.GraduateCertificateNumber);

                //���~�Үѵ��O�ǵ{�N�X (2019/02/15 �o�~ �ˬd�o�{ �ثe�ڭ̨t�ΨS���䴩�o�ӷ����A�n�A��s)
                mdws.Cells[mdws_index, 16].PutValue("");

                //�Ƶ�����
                mdws.Cells[mdws_index, 17].PutValue(rec.Comment);
                //�{�s�ǲΤ��
                mdws.Cells[mdws_index, 18].PutValue(rec.origin_temp_date);
                //�{�s�ǲ�
                mdws.Cells[mdws_index, 19].PutValue(rec.origin_temp_desc);
                //�{�s�ǲΤ帹
                mdws.Cells[mdws_index, 20].PutValue(rec.origin_temp_number);

                
            }

            // ��ƥ��� �[End
            mdws.Cells.CreateRange(mdws_index + 1, 1, false).CopyData(range_R_EndRow);
            mdws.Cells.CreateRange(mdws_index + 1, 1, false).CopyStyle(range_R_EndRow);
            wb.Worksheets.RemoveAt("�q�l�榡�d��");
            wb.Worksheets.RemoveAt("�q�l�榡�d��_�t�{�s");
            wb.Worksheets.RemoveAt("���ץͲ��~�W�U�ʭ��d��");

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
            get { return "���ץͲ��~�W�U"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }
    }
}
