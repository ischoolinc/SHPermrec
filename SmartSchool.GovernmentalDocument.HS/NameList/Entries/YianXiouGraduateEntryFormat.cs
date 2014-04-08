using System.Xml;

namespace SmartSchool.GovernmentalDocument.NameList
{
    class YianXiouGraduateEntryFormat:AbstractEntryFormat
    {
        #region IEntityFormat ����

        public override void Initialize(XmlElement element)
        {
            base.Initialize(element);

            // �B�z�̫���y�֭�帹�B��(��)�~�ҮѦr��
            ColumnInfo column = new ColumnInfo(element.GetAttribute("�̫���y�֭�帹"), 120);
            _attributes.Add("�̫���y�֭�帹", column);

            ColumnInfo column2 = new ColumnInfo(element.GetAttribute("��(��)�~�ҮѦr��"), 100);
            _attributes.Add("��(��)�~�ҮѦr��", column2);

            _attributes.Remove("�~��");
        }

        public override string GradeYear
        {
            get
            {
                return "";
            }
        }

        public override string Group
        {
            get
            {
                return Department;
            }
        }
        #endregion
    }   
}