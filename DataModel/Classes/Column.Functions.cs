using DevExpress.Xpo;
using System;
using System.Linq;
using Utilities.Exceptions;
using Utilities.ExtensionsMethods;
using static Utilities.Dictionaries.Dictionaries;

namespace DataModel.Classes
{
    /// <summary>
    /// Column.Functions
    /// </summary>
    public partial class Column
    {

        public override void CheckParentType(string parentType, UnitOfWork uow, ref string errors)
        {
            if (parentType.ClearWhiteSpacesAndToLower() != SqlObject.table.ToString())
            {
                errors += $"{Environment.NewLine}Dla kolumny {this.Name} jako rodzic jest zadeklarowane coś innego niż tabela";
            }
        }

        public override void SetParent(UnitOfWork uow, ref string errors)
        {
            if (this.ParentTable != null)
            {
                return;
            }

            var parent = uow.QueryInTransaction<Table>().Where(x => x.Name.ToLower() == this.ParentName.ToLower())?.FirstOrDefault();

            if (parent == null)
            {
                try
                {
                    parent = AddNewObject.AddNewTable(
                        name: this.ParentName,
                        schemaName: string.Empty,
                        parentName: string.Empty,
                        parentType: SqlObject.database.ToString(),
                        uow: uow,
                        errors: ref errors);
                }
                catch (AddingNewObjectException ex)
                {
                    errors += $"{Environment.NewLine}Nie udało się utworzyć tabeli {this.ParentName} dla kolumny {this.Name}";
                    return;
                }
            }

            this.ParentTable = parent;
        }

    }
}
