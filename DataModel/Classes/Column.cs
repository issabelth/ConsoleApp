using CsvHelper.Configuration.Attributes;
using DevExpress.Xpo;
using Utilities;
using Utilities.ExtensionsMethods;

namespace DataModel.Classes
{
    public partial class Column : ImportedObjectWithParentBaseClass
    {

        /// <summary>
        /// Constructor for when the 'isNullable' parameter is a string
        /// </summary>
        /// <param name="name"></param>
        /// <param name="schema"></param>
        /// <param name="parentName"></param>
        /// <param name="parentType"></param>
        /// <param name="dataType"></param>
        /// <param name="isNullable"></param>
        public Column(Session session, string name, string schema, string parentName, string parentType, string dataType, string isNullable)
            : base(session, name, schema, parentName)
        {
            this.ParentType = parentType;
            this.DataType = dataType;
            this.IsNullable = Methods.InterpretTextAsTrueOrFalse(isNullable);
        }

        public Column(Session session, string name, string schema, string parentName, string parentType, string dataType, bool isNullable)
            : base(session, name, schema, parentName)
        {
            this.ParentType = parentType;
            this.DataType = dataType;
            this.IsNullable = isNullable;
        }

        private Table _parentTable;
        public Table ParentTable
        {
            get
            {
                return _parentTable;
            }
            set
            {
                SetPropertyValue(nameof(ParentTable), ref _parentTable, value);
            }
        }

        private string _parentType;
        [Name("ParentType")]
        public string ParentType
        {
            get
            {
                return _parentType;
            }
            set
            {
                SetPropertyValue(nameof(ParentType), ref _parentType, value.ClearWhiteSpaces());
            }
        }

        private string _dataType;
        [Name("DataType")]
        public string DataType
        {
            get
            {
                return _dataType;
            }
            set
            {
                SetPropertyValue(nameof(DataType), ref _dataType, value.TryRecognizeDatatype());
            }
        }

        private bool _isNullable = false;
        [Name("IsNullable")]
        public bool IsNullable
        {
            get
            {
                return _isNullable;
            }
            set
            {
                SetPropertyValue(nameof(IsNullable), ref _isNullable, value);
            }
        }

    }
}
