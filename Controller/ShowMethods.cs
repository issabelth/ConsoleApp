namespace Controller
{
    using DataModel.Classes;
    using DevExpress.Xpo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities.ConsoleCommands;
    using Utilities.Dictionaries;
    using Utilities.ExtensionsMethods;

    public partial class ShowMethods
    {
        UnitOfWork _uow = OneUnitOfWork.uow;

        public List<string> ShowAll(Dictionaries.SqlObject objType, string nameToSearch = "",
            Dictionaries.SqlObject objTypeToSearch = Dictionaries.SqlObject.none)
        {
            if (!string.IsNullOrWhiteSpace(nameToSearch))
            {
                nameToSearch = nameToSearch.ClearWhiteSpacesAndToLower();
            }

            switch (objType)
            {
                case Dictionaries.SqlObject.database:
                    {
                        return ShowAllNames(_uow.QueryInTransaction<Database>()?.Select(x => x.Name));
                    }
                case Dictionaries.SqlObject.schema:
                    {
                        return ShowAllNames(_uow.QueryInTransaction<Schema>()?.Select(x => x.Name));
                    }
                case Dictionaries.SqlObject.table:
                    {
                        if (string.IsNullOrWhiteSpace(nameToSearch))
                        {
                            return ShowAllNames(_uow.QueryInTransaction<Table>().Select(x => x.Name));
                        }

                        switch (objTypeToSearch)
                        {
                            case Dictionaries.SqlObject.schema:
                                {
                                    var sch = _uow.QueryInTransaction<Schema>().Where(x => x.Name.ToLower() == nameToSearch).First();

                                    return ShowAllNames(_uow.QueryInTransaction<Table>()
                                        .Where(x => x.Schema == sch)
                                        .Select(x => x.Name));
                                }
                            case Dictionaries.SqlObject.database:
                                {
                                    var db = _uow.QueryInTransaction<Database>().Where(x => x.Name.ToLower() == nameToSearch).First();

                                    return ShowAllNames(_uow.QueryInTransaction<Table>()
                                        .Where(x => x.ParentDatabase == db)
                                        .Select(x => x.Name));
                                }
                            default:
                                {
                                    throw new Exception("Niezdefiniowany obiekt do wyszukania dla tabeli");
                                }
                        }
                    }
                case Dictionaries.SqlObject.column:
                    {
                        if (string.IsNullOrWhiteSpace(nameToSearch))
                        {
                            return ShowAllNames(_uow.QueryInTransaction<Column>()?.Select(x => x.Name));
                        }

                        switch (objTypeToSearch)
                        {
                            case Dictionaries.SqlObject.schema:
                                {
                                    var sch = _uow.QueryInTransaction<Schema>().Where(x => x.Name.ToLower() == nameToSearch).First();

                                    return ShowAllNames(_uow.QueryInTransaction<Column>()
                                        .Where(x => x.Schema == sch)
                                        .Select(x => x.Name));
                                }
                            case Dictionaries.SqlObject.table:
                                {
                                    var tb = _uow.QueryInTransaction<Table>().Where(x => x.Name.ToLower() == nameToSearch).First();

                                    return ShowAllNames(_uow.QueryInTransaction<Column>()
                                        .Where(x => x.ParentTable == tb)
                                        .Select(x => x.Name));
                                }
                            default:
                                {
                                    throw new Exception("Niezdefiniowany obiekt do wyszukania dla tabeli");
                                }
                        }
                    }
            }
            return null;
        }

        private List<string> ShowAllNames(IQueryable<string> names)
        {
            if (names == null ||
                names.Count() <= 0)
            {
                ConsoleWriteError.WriteTheError("Brak danych");
            }

            var namesOrdered = names.ToList().OrderBy(x => x);
            int i = 1;

            foreach (var name in namesOrdered)
            {
                Console.WriteLine($"{i++}. {name}");
            }

            return namesOrdered.ToList();
        }

    }
}
