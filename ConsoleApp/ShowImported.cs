using Controller;
using System;
using System.Linq;
using Utilities.ConsoleCommands;
using Utilities.Dictionaries;

namespace ConsoleApp
{
    public static class ShowImported
    {

        public static void ShowAll(Dictionaries.SqlObject objectType)
        {
            var show = new ShowMethods();

            Dictionaries.SqlObject choosenObjToSearch;
            string nameToSearch = ChooseTheObjToSearch(objectType, show, out choosenObjToSearch);

            show.ShowAll(
                objType: objectType,
                nameToSearch: nameToSearch,
                objTypeToSearch: choosenObjToSearch);
        }

        private static string ChooseTheObjToSearch(Dictionaries.SqlObject objectType, ShowMethods show,
            out Dictionaries.SqlObject choosenObjToSearch)
        {
            choosenObjToSearch = Dictionaries.SqlObject.none;

            if (objectType != Dictionaries.SqlObject.table &&
                objectType != Dictionaries.SqlObject.column)
            {
                return string.Empty;
            }
            if (!UserOptionsMethods.YesOrNo("Czy chcesz wyświetlić dane tylko dla wybranego schematu/bazy/tabeli?"))
            {
                return string.Empty;
            }

            Console.WriteLine($"{Environment.NewLine}Wybierz:");
            WriteAllPossibleSqlObjects(objectType);
            string choosenObj = Console.ReadLine();

            while (!Dictionaries.SqlObjDictForData(objectType).Any(x => x.Value == choosenObj))
            {
                ConsoleWriteError.WriteTheError($"Nie ma takiej opcji. Wpisz poprawną:");
                WriteAllPossibleSqlObjects(objectType);
                choosenObj = Console.ReadLine();
                Console.WriteLine();
            }

            choosenObjToSearch = Dictionaries.SqlObjDictForData(objectType).Where(x => x.Value == choosenObj).FirstOrDefault().Key;
            string nameToSearch = string.Empty;

            if (choosenObjToSearch != Dictionaries.SqlObject.none)
            {
                Console.WriteLine($"{Environment.NewLine}Wybierz, dla którego chcesz wyświetlić:");
                var listToChoose = show.ShowAll(objType: choosenObjToSearch);
                nameToSearch = Console.ReadLine();
                Console.WriteLine();

                while (!listToChoose.Any(x => x.ToLower() == nameToSearch))
                {
                    ConsoleWriteError.WriteTheError($"Nie ma takiego. Podaj jeden z poniższych:");
                    listToChoose = show.ShowAll(objType: choosenObjToSearch);
                    nameToSearch = Console.ReadLine();
                    Console.WriteLine();
                }
            }

            return nameToSearch;
        }

        private static void WriteAllPossibleSqlObjects(Dictionaries.SqlObject objectType)
        {
            foreach (var item in Dictionaries.SqlObjDictForData(objectType))
            {
                Console.WriteLine(item.Value);
            }
        }

    }
}
