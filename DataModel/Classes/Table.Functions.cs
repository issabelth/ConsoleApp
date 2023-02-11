using DevExpress.Xpo;
using System;
using System.Linq;
using Utilities.Exceptions;
using Utilities.ExtensionsMethods;
using static Utilities.Dictionaries.Dictionaries;

namespace DataModel.Classes
{
    /// <summary>
    /// Table.Functions
    /// </summary>
    public partial class Table
    {

        public override void CheckParentType(string parentType, UnitOfWork uow, ref string errors)
        {
            if (parentType.ClearWhiteSpacesAndToLower() != SqlObject.database.ToString())
            {
                errors += $"{Environment.NewLine}Dla tabeli {this.Name} jako rodzic jest zadeklarowane coś innego niż baza";
            }
        }

        public override void SetParent(UnitOfWork uow, ref string errors)
        {
            var parent = uow.QueryInTransaction<Database>().Where(x => x.Name.ToLower() == this.ParentName.ToLower())?.FirstOrDefault();

            if (parent == null)
            {
                try
                {
                    parent = AddNewObject.AddNewDatabase(
                        name: this.ParentName,
                        uow: uow,
                        errors: ref errors);
                }
                catch (AddingNewObjectException ex)
                {
                    errors += $"{Environment.NewLine}Nie udało się utworzyć bazy {this.ParentName} dla tabeli {this.Name}";
                    return;
                }
            }

            this.ParentDatabase = parent;
        }

    }
}
