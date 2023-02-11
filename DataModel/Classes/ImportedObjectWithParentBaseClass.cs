using CsvHelper.Configuration.Attributes;
using DevExpress.Xpo;
using System;
using System.Linq;
using Utilities.Exceptions;
using Utilities.ExtensionsMethods;

namespace DataModel.Classes
{
    public abstract class ImportedObjectWithParentBaseClass : ImportedObjectBaseClass
    {

        public ImportedObjectWithParentBaseClass(Session session, string name, string schemaName, string parentName)
            : base(session, name)
        {
            this.SchemaName = schemaName;
            this.ParentName = parentName;
        }

        public ImportedObjectWithParentBaseClass(Session session, string name, Schema schema, string parentName)
            : base(session, name)
        {
            this.Schema = schema;
            this.ParentName = parentName;
        }

        private Schema _schema;
        public Schema Schema
        {
            get
            {
                return _schema;
            }
            set
            {
                SetPropertyValue(nameof(Schema), ref _schema, value);
            }
        }

        private string _schemaName;
        [Name("Schema")]
        public string SchemaName
        {
            get
            {
                return _schemaName;
            }
            set
            {
                SetPropertyValue(nameof(SchemaName), ref _schemaName, value);
            }
        }

        private string _parentName;
        [Name("ParentName")]
        public string ParentName
        {
            get
            {
                return _parentName;
            }
            set
            {
                SetPropertyValue(nameof(ParentName), ref _parentName, value.ClearWhiteSpaces());
            }
        }

        public void SetSchema(UnitOfWork uow, ref string errors)
        {
            if (this.Schema != null)
            {
                return;
            }

            var schema = uow.QueryInTransaction<Schema>().Where(x => x.Name.ToLower() == this.SchemaName.ToLower())?.FirstOrDefault();

            if (schema == null)
            {
                try
                {
                    schema = AddNewObject.AddNewSchema(
                        name: this.SchemaName,
                        uow: uow,
                        errors: ref errors);
                }
                catch (AddingNewObjectException ex)
                {
                    errors += $"{Environment.NewLine}Nie udało się utworzyć schematu {this.SchemaName} dla {this.Name}";
                    return;
                }
            }

            this.Schema = schema;
        }

        public abstract void CheckParentType(string parentType, UnitOfWork uow, ref string errors);

        public abstract void SetParent(UnitOfWork uow, ref string errors);

    }
}
