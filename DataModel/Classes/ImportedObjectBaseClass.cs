using CsvHelper.Configuration.Attributes;
using DevExpress.Xpo;
using System;
using Utilities.ExtensionsMethods;

namespace DataModel.Classes
{
    public class ImportedObjectBaseClass : XPCustomObject
    {

        public ImportedObjectBaseClass(Session session, string name)
            : base(session)
        {
            this.Name = name;
        }

        private string _name;
        [Name("Name")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetPropertyValue(nameof(Name), ref _name, value.ClearWhiteSpaces());
            }
        }

        private Guid _guid = Guid.NewGuid();
        [Key(true)]
        public Guid guid
        {
            get
            {
                return _guid;
            }
            set
            {
                SetPropertyValue(nameof(guid), ref _guid, value);
            }
        }

    }

}