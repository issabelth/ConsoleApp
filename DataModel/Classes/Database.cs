using CsvHelper.Configuration;
using DevExpress.Xpo;

namespace DataModel.Classes
{
    public class Database : ImportedObjectBaseClass
    {

        public Database(Session session, string name)
            : base(session, name)
        {
        }

    }

}
