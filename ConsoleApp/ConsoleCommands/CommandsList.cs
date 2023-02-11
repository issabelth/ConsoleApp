using System;
using System.Collections.Generic;
using Utilities.ConsoleCommands;

namespace ConsoleApp.ConsoleCommands
{
    public partial class CommandsList
    {

        public static readonly List<CommandAction> AllCommandsList = new List<CommandAction>()
        {
            new CommandAction(key: "End", actionName: "zakończ program", action: End),
            new CommandAction(key: "ImportFile", actionName: "import pliku csv", action: ImportFile),
            new CommandAction(key: "ShowAllDb", actionName: "pokaż zaimportowane bazy", action: ShowImportedDatabases),
            new CommandAction(key: "ShowAllTb", actionName: "pokaż zaimportowane tabele", action: ShowImportedTables),
            new CommandAction(key: "ShowAllSch", actionName: "pokaż zaimportowane schematy", action: ShowImportedSchemas),
            new CommandAction(key: "ShowAllCol", actionName: "pokaż zaimportowane kolumny", action: ShowImportedColumns),
        };

        public static int End()
        {
            Environment.Exit(0);
            return 0;
        }

    }
}
