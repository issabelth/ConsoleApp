using DevExpress.Xpo;

namespace DataModel.Classes
{
    public class Schema : ImportedObjectBaseClass
    {

        public Schema(Session session, string name)
            : base(session, name)
        {
        }

    }
}
