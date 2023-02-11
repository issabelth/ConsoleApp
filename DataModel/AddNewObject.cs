using DataModel.Classes;
using DevExpress.Xpo;
using System;
using System.Linq;
using Utilities.Exceptions;

namespace DataModel
{
    public static class AddNewObject
    {

        public static Database AddNewDatabase(string name, UnitOfWork uow, ref string errors)
        {
            try
            {
                var foundDatabase = uow.QueryInTransaction<Database>().Where(x => x.Name == name)?.FirstOrDefault();

                if (foundDatabase != null)
                {
                    return foundDatabase;
                }

                return new Database(session: uow, name: name);
            }
            catch (Exception ex)
            {
                throw new AddingNewObjectException($"Błąd przy dodawaniu nowej bazy {name}");
            }
        }

        public static Schema AddNewSchema(string name, UnitOfWork uow, ref string errors)
        {
            try
            {
                var foundSchema = uow.QueryInTransaction<Schema>().Where(x => x.Name == name)?.FirstOrDefault();

                if (foundSchema != null)
                {
                    return foundSchema;
                }

                return new Schema(session: uow, name: name);
            }
            catch (Exception ex)
            {
                throw new AddingNewObjectException($"Błąd przy dodawaniu nowego schematu {name}");
            }
        }

        public static Table AddNewTable(string name, string schemaName, string parentName, string parentType, UnitOfWork uow, ref string errors)
        {
            try
            {
                var foundTable = uow.QueryInTransaction<Table>().Where(x => x.Name == name)?.FirstOrDefault();

                if (foundTable != null)
                {
                    return foundTable;
                }

                return new Table(
                    session: uow,
                    name: name,
                    schema: schemaName,
                    parentName: parentName);
            }
            catch (Exception ex)
            {
                throw new AddingNewObjectException($"Błąd przy dodawaniu nowej tabeli {name}");
            }
        }

        public static Column AddNewColumn(string name, string schemaName, string parentName, string parentType, string dataType, string isNullable,
            UnitOfWork uow, ref string errors)
        {
            try
            {
                var foundColumn = uow.QueryInTransaction<Column>().Where(x => x.Name == name && x.ParentName == parentName)?.FirstOrDefault();

                if (foundColumn != null)
                {
                    return foundColumn;
                }

                return new Column(
                    session: uow,
                    name: name,
                    schema: schemaName,
                    parentName: parentName,
                    parentType: parentType,
                    dataType: dataType,
                    isNullable: isNullable);
            }
            catch (Exception ex)
            {
                throw new AddingNewObjectException($"Błąd przy dodawaniu nowej kolumny {name}");
            }
        }

    }
}
