using Utilities.Dictionaries;

namespace ConsoleApp.ConsoleCommands
{
    public partial class CommandsList
    {

        public static int ShowImportedDatabases()
        {
            ShowImported.ShowAll(Dictionaries.SqlObject.database);
            return 0;
        }

        public static int ShowImportedTables()
        {
            ShowImported.ShowAll(Dictionaries.SqlObject.table);
            return 0;
        }

        public static int ShowImportedSchemas()
        {
            ShowImported.ShowAll(Dictionaries.SqlObject.schema);
            return 0;
        }

        public static int ShowImportedColumns()
        {
            ShowImported.ShowAll(Dictionaries.SqlObject.column);
            return 0;
        }

    }
}