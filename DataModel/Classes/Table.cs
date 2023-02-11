using DevExpress.Xpo;

namespace DataModel.Classes
{
    public partial class Table : ImportedObjectWithParentBaseClass
    {

        public Table(Session session, string name, string schema, string parentName)
            : base(session, name, schema, parentName)
        {
        }

        private Database _parentDatabase;
        public Database ParentDatabase
        {
            get
            {
                return _parentDatabase;
            }
            set
            {
                SetPropertyValue(nameof(ParentDatabase), ref _parentDatabase, value);
            }
        }

    }
}
