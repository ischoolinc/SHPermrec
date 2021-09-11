using System;
using System.IO;
using System.Xml;
using Aspose.Cells;
using UpdateRecordModule_SH_D.BL;

namespace UpdateRecordModule_SH_D.GovernmentalDocument.Reports.List
{
    public class ExtendingStudentList : ReportBuilder
    {
        protected override void Build(System.Xml.XmlElement source, string location)
        {
            #region �إ� Excel

            //�q Resources �N���y���ʦW�UtemplateŪ�X��
            Workbook template = new Workbook();
            template.Open(new MemoryStream(Properties.Resources.ExtendingStudentListTemplate), FileFormatType.Excel2003);

            //���� excel
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(Properties.Resources.ExtendingStudentListTemplate), FileFormatType.Excel2003);
                
            #endregion

            #region �ƻs�˦�-�w�]�˦��B��e

            //�]�w�w�]�˦�
            wb.DefaultStyle = template.DefaultStyle;

            //�ƻs�˪����e18�� Column(��e)
            for (int m = 0; m < 18; m++)
            {
                /*
                 * �ƻs template���Ĥ@�� Sheet���� m�� Column
                 * �� wb���Ĥ@�� Sheet���� m�� Column
                 */
                wb.Worksheets[0].Cells.CopyColumn(template.Worksheets[0].Cells, m, m);
            }

            #endregion

            #region ��l�ܼ�
            
                /****************************** 
                * rowi ��J�Ǯո�ƥ�
                * rowj ��J�ǥ͸�ƥ�
                * num �p��M�����
                * numcount �p��C���M�歶��
                * j �p��Ҳ��ͲM�歶��
                * x �P�_�ӼƬO�_��20�Q�ƥ�
                ******************************/
                int rowi = 0, rowj = 1, num = source.SelectNodes("�M��").Count, numcount = 1, j = 0;
                bool x = false;

                int recCount = 0;
                int totalRec = source.SelectNodes("�M��/���ʬ���").Count;
            
            #endregion

            foreach (XmlNode list in source.SelectNodes("�M��"))
            {
                int i = 0;

                #region ��X����`�ƤΧP�_

                //��X����`�Ƥ�K�����i��
                int count = list.SelectNodes("���ʬ���").Count;

                //�P�_�ӼƬO�_��20�Q��
                if (count % 20 == 0)
                {
                    x = true;
                } 

                #endregion
                

                #region ���ʬ���

                //�Nxml��ƶ�J��excel
                foreach (XmlNode st in list.SelectNodes("���ʬ���"))
                {
                    recCount++;
                    if (i % 20 == 0)
                    {
                        #region �ƻs�˦�-�氪�B�d��

                        //�ƻs�˪����e287�� Row(�氪)
                        //for (int m = 0; m < 28; m++)
                        //{
                        //    /*
                        //     * �ƻs template���Ĥ@�� Sheet����m�� Row
                        //     * �� wb���Ĥ@�� Sheet����(j * 28) + m�� Row
                        //     */
                        //    wb.Worksheets[0].Cells.CopyRow(template.Worksheets[0].Cells, m, (j * 28) + m);
                        //}

                        /*
                         * �ƻsStyle(�]�t�x�s��X�֪���T)
                         * ����CreateRange()����n�ƻs��Range("A1", "R28")
                         * �A��CopyStyle�ƻs�t�@��Range�����榡
                         */
                        Range range = template.Worksheets[0].Cells.CreateRange(0, 28, false);
                        int t= j * 28;
                        wb.Worksheets[0].Cells.CreateRange(t,28,false).Copy(range);

                        #endregion

                        #region ��J�Ǯո��

                        //�N�Ǯո�ƶ�J�A����m��
                        wb.Worksheets[0].Cells[rowi, 13].PutValue(source.SelectSingleNode("@�ǮեN��").InnerText);
                        wb.Worksheets[0].Cells[rowi, 16].PutValue(list.SelectSingleNode("@��O�N��").InnerText);
                        wb.Worksheets[0].Cells[rowi + 2, 2].PutValue(source.SelectSingleNode("@�ǮզW��").InnerText);
                        wb.Worksheets[0].Cells[rowi + 2, 7].PutValue(Convert.ToInt32(source.SelectSingleNode("@�Ǧ~��").InnerText)+" �Ǧ~�� �� "+Convert.ToInt32(source.SelectSingleNode("@�Ǵ�").InnerText)+" �Ǵ�");
                        wb.Worksheets[0].Cells[rowi + 2, 12].PutValue(list.SelectSingleNode("@��O").InnerText);
                        wb.Worksheets[0].Cells[rowi + 2, 14].PutValue(list.SelectSingleNode("@�~��").InnerText);

                        #endregion

                        if (j > 0)
                        {
                            //���J����(�b j * 28 �� (j * 28) +1 �����AR��S����)
                            wb.Worksheets[0].HPageBreaks.Add(j * 28, 18);
                            rowj += 8;
                        }
                        else
                        {
                            rowj = 6;
                        }

                        rowi += 28;
                        j++;

                        #region ��ܭ���

                        //��ܭ���
                        if (x != true)
                        {
                            wb.Worksheets[0].Cells[(28 * (j - 1)) + 27, 13].PutValue("��" + numcount + "���A�@" + Math.Ceiling((double)count / 20) + "��");
                        }
                        else
                        {
                            wb.Worksheets[0].Cells[(28 * (j - 1)) + 27, 13].PutValue("��" + numcount + "���A�@" + (Math.Ceiling((double)count / 20) + 1) + "��");
                        }
                        numcount++;

                        #endregion
                    }

                    #region ��J�ǥ͸��
                    
                        //�N�ǥ͸�ƶ�J�A����m��
                        wb.Worksheets[0].Cells[rowj, 1].PutValue(st.SelectSingleNode("@�Ǹ�").InnerText);
                        wb.Worksheets[0].Cells[rowj, 3].PutValue(st.SelectSingleNode("@�m�W").InnerText);
                        wb.Worksheets[0].Cells[rowj, 4].PutValue(st.SelectSingleNode("@�����Ҹ�").InnerText);
                        wb.Worksheets[0].Cells[rowj, 8].PutValue(Util.ConvertDateStr2(st.SelectSingleNode("@�Ƭd���").InnerText) + "\n" + st.SelectSingleNode("@�Ƭd�帹").InnerText);
                        wb.Worksheets[0].Cells[rowj, 11].PutValue(st.SelectSingleNode("@���ʥN��").InnerText);
                        wb.Worksheets[0].Cells[rowj, 12].PutValue(st.SelectSingleNode("@��]�Ψƶ�").InnerText);
                        if (st.SelectSingleNode("@�s�Ǹ�").InnerText == "")
                        {
                            wb.Worksheets[0].Cells[rowj, 13].PutValue(Util.ConvertDateStr2(st.SelectSingleNode("@���ʤ��").InnerText));
                        }
                        else
                        {
                            wb.Worksheets[0].Cells[rowj, 13].PutValue(st.SelectSingleNode("@�s�Ǹ�").InnerText + "\n" + Util.ConvertDateStr2(st.SelectSingleNode("@���ʤ��").InnerText));
                        }
                            //wb.Worksheets[0].Cells[rowj, 16].PutValue(st.SelectSingleNode("@�Ƶ�").InnerText);
                    if(st.SelectSingleNode("@�S�����N�X")!=null)    
                        wb.Worksheets[0].Cells[rowj, 16].PutValue(st.SelectSingleNode("@�S�����N�X").InnerText);

                    #endregion

                    i++;
                    rowj++;

                    //�^���i��
                    ReportProgress((int)(((double)recCount * 100.0) / ((double)totalRec)));
                }

                #endregion

                #region �Y�ӼƬ�20���ơA�B�z��@����

                if (x == true)
                {

                    #region �ƻs�˦�-�氪�B�d��

                    //�ƻs�˪��e28�� Row(�氪)
                    //for (int m = 0; m < 28; m++)
                    //{
                    //    /*
                    //     * �ƻs template���Ĥ@�� Sheet����m�� Row
                    //     * �� wb���Ĥ@�� Sheet����(j * 28) + m�� Row
                    //     */
                    //    wb.Worksheets[0].Cells.CopyRow(template.Worksheets[0].Cells, m, (j * 28) + m);
                    //}

                    /*
                     * �ƻsStyle(�]�t�x�s��X�֪���T)
                     * ����CreateRange()����n�ƻs��Range("A1", "R28")
                     * �A��CopyStyle�ƻs�t�@��Range�����榡
                     */
                    Range range = template.Worksheets[0].Cells.CreateRange(0, 28, false);                    
                    int t= j * 28;
                    wb.Worksheets[0].Cells.CreateRange(t, 28, false).Copy(range);

                    #endregion

                    #region ��J�Ǯո��

                    //�N�Ǯո�ƶ�J�A����m��
                    wb.Worksheets[0].Cells[rowi, 13].PutValue(source.SelectSingleNode("@�ǮեN��").InnerText);
                    wb.Worksheets[0].Cells[rowi, 16].PutValue(list.SelectSingleNode("@��O�N��").InnerText);
                    wb.Worksheets[0].Cells[rowi + 2, 2].PutValue(source.SelectSingleNode("@�ǮզW��").InnerText);
                    wb.Worksheets[0].Cells[rowi + 2, 7].PutValue(Convert.ToInt32(source.SelectSingleNode("@�Ǧ~��").InnerText) + " �Ǧ~�� �� " + Convert.ToInt32(source.SelectSingleNode("@�Ǵ�").InnerText) + " �Ǵ�");
                    wb.Worksheets[0].Cells[rowi + 2, 12].PutValue(list.SelectSingleNode("@��O").InnerText);

                    #endregion

                    if (j > 0)
                    {
                        //���J����(�bi��i+1�����AO��P����)
                        wb.Worksheets[0].HPageBreaks.Add(j * 28, 18);
                        rowj += 8;
                    }

                    rowi += 28;
                    j++;

                    #region ��ܭ���

                    //��ܭ���
                    wb.Worksheets[0].Cells[(28 * (j - 1)) + 27, 13].PutValue("��" + numcount + "���A�@" + (Math.Ceiling((double)count / 20) + 1) + "��");
                    numcount++;

                    #endregion
                } 

                #endregion

                #region �έp�H��

                //��J�έp�H��
                wb.Worksheets[0].Cells.CreateRange(rowj, 1, 1, 2).UnMerge();
                wb.Worksheets[0].Cells.Merge(rowj, 1, 1, 3);
                wb.Worksheets[0].Cells[rowj, 1].PutValue("�X  �p " + count.ToString() + " �W");

                #endregion

                wb.Worksheets[0].HPageBreaks.Add(j * 28, 18);

                #region �]�w�ܼ�

                //�վ�s�M��Ҩϥ��ܼ�
                numcount = 1;
                rowj = (28 * j) - 2;
                rowi = (28 * j);
                x = false; 

                #endregion
            }
                       
            //�d��
            Worksheet TemplateWb_Cover = wb.Worksheets["���ץͦW�U�ʭ��d��"];

            //�갵����
            Worksheet cover = wb.Worksheets[wb.Worksheets.Add()];

            //�W��
            cover.Name = "���ץͦW�U�ʭ�";

            string school_code = source.SelectSingleNode("@�ǮեN��").InnerText;
            string school_year = source.SelectSingleNode("@�Ǧ~��").InnerText;
            string school_semester = source.SelectSingleNode("@�Ǵ�").InnerText;

            //�d��
            Range range_H_Cover = TemplateWb_Cover.Cells.CreateRange(0, 1, false);

            //range_H_Cover
            cover.Cells.CreateRange(0, 1, false).Copy(range_H_Cover);

            Range range_R_cover = TemplateWb_Cover.Cells.CreateRange(1, 1, false);
            // 107�s�榡 ������n ��End �r��
            Range range_R_cover_EndRow = TemplateWb_Cover.Cells.CreateRange(2, 1, false);


            int cover_row_counter = 1;

            //2018/2/2 �o�~���� �A�U���O�s���ʭ����ͤ覡

            foreach (XmlNode list in source.SelectNodes("�M��"))
            {
                //�C�W�[�@��,�ƻs�@��
                cover.Cells.CreateRange(cover_row_counter, 1, false).Copy(range_R_cover);

                string gradeYear = list.SelectSingleNode("@�~��").InnerText;
                string deptCode = list.SelectSingleNode("@��O�N�X").InnerText;

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
                    string scheduledGraduateYear = st.SelectSingleNode("@�����~�Ǧ~��").InnerText;
                    string classType = st.SelectSingleNode("@�Z�O").InnerText;
                    string updateType = st.SelectSingleNode("@�W�����O").InnerText;
                    string approvedExtendingStudentCount = st.SelectSingleNode("@���ɩ��׾ǥͼ�").InnerText;
                    string waitingExtendingStudentCount = st.SelectSingleNode("@���ӽЩ��׾ǥͼ�").InnerText;                    
                    string originalStudentCount = st.SelectSingleNode("@�즳�ǥͼ�").InnerText;
                    string increaseStudentCount = st.SelectSingleNode("@�W�[�ǥͼ�").InnerText;                    
                    string currentStudentCount = st.SelectSingleNode("@�{���ǥͼ�").InnerText;                    
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
                    //�W�[�ǥͼ�
                    cover.Cells[cover_row_counter, 11].PutValue(increaseStudentCount);;
                    //�{���ǥͼ�
                    cover.Cells[cover_row_counter, 12].PutValue(currentStudentCount);                    
                    //�Ƶ�����
                    cover.Cells[cover_row_counter, 13].PutValue(remarksContent);

                }
                cover_row_counter++;
            }

            // ��ƥ��� �[End
            cover.Cells.CreateRange(cover_row_counter, 1, false).Copy(range_R_cover_EndRow);


            //�d��
            Worksheet TemplateWb = wb.Worksheets["�q�l�榡�d��"];

            //�갵����
            Worksheet mdws = wb.Worksheets[wb.Worksheets.Add()];

            mdws.Name = "���ץͦW�U";

            //�d��
            Range range_H = TemplateWb.Cells.CreateRange(0, 1, false);
            Range range_R = TemplateWb.Cells.CreateRange(1, 1, false);
            // 107�s�榡 ������n ��End �r��
            Range range_R_EndRow = TemplateWb.Cells.CreateRange(2, 1, false);
            //����range_H
            mdws.Cells.CreateRange(0, 1, false).Copy(range_H);

            int mdws_index = 0;
            foreach (XmlElement record in source.SelectNodes("�M��/���ʬ���"))
            {
                mdws_index++;

                //�C�W�[�@��,�ƻs�@��
                mdws.Cells.CreateRange(mdws_index, 1, false).Copy(range_R);

                // �����~�Ǧ~��                
                mdws.Cells[mdws_index, 0].PutValue(record.GetAttribute("�����~�Ǧ~��"));
                mdws.Cells[mdws_index, 1].PutValue(record.GetAttribute("�Z�O"));
                mdws.Cells[mdws_index, 2].PutValue((record.ParentNode as XmlElement).GetAttribute("��O�N��"));
                mdws.Cells[mdws_index, 3].PutValue("");

                mdws.Cells[mdws_index, 4].PutValue(record.GetAttribute("�Ǹ�"));
                mdws.Cells[mdws_index, 5].PutValue(record.GetAttribute("�m�W"));
                mdws.Cells[mdws_index, 6].PutValue(record.GetAttribute("�����Ҹ�"));
                mdws.Cells[mdws_index, 7].PutValue(record.GetAttribute("��1"));
                mdws.Cells[mdws_index, 8].PutValue(record.GetAttribute("�ʧO�N��"));
                mdws.Cells[mdws_index, 9].PutValue((BL.Util.ConvertDate1(record.GetAttribute("�X�ͦ~���"))));
                mdws.Cells[mdws_index, 10].PutValue(record.GetAttribute("�S�����N�X")); //�쬰�����������
                mdws.Cells[mdws_index, 11].PutValue(record.GetAttribute("���ʥN��"));
                mdws.Cells[mdws_index, 12].PutValue(BL.Util.ConvertDate1(record.GetAttribute("���ʤ��")));

                mdws.Cells[mdws_index, 13].PutValue(BL.Util.ConvertDate1(record.GetAttribute("�Ƭd���")));

                mdws.Cells[mdws_index, 14].PutValue(BL.Util.GetDocNo_Doc(record.GetAttribute("�Ƭd�帹")));
                mdws.Cells[mdws_index, 15].PutValue(BL.Util.GetDocNo_No(record.GetAttribute("�Ƭd�帹")));
                mdws.Cells[mdws_index, 16].PutValue(record.GetAttribute("�������y�s��"));
                mdws.Cells[mdws_index, 17].PutValue(record.GetAttribute("�Ƶ�"));
            }

            // ��ƥ��� �[End
            mdws.Cells.CreateRange(mdws_index + 1, 1, false).Copy(range_R_EndRow);

            mdws.AutoFitColumns();
            mdws.Cells.SetColumnWidth(5, 8.5);
            mdws.Cells.SetColumnWidth(11, 20);
            
            wb.Worksheets.RemoveAt("���ץͦW�U�ʭ��d��");
            wb.Worksheets.RemoveAt("�q�l�榡�d��");
            wb.Worksheets.ActiveSheetIndex = 0;            


            //�x�s Excel
            wb.Save(location, FileFormatType.Excel2003);
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
            get { return "���ץͦW�U"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }
    }
}
