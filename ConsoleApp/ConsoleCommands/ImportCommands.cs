using System;
using System.Reflection;

namespace ConsoleApp.ConsoleCommands
{
    public partial class CommandsList
    {

        public static int ImportFile()
        {
            do
            {
                ConstructorInfo constructor = typeof(Controller.OneUnitOfWork)
                    .GetConstructor(BindingFlags.Static | BindingFlags.NonPublic, null, new Type[0], null);

                // re-initialize uow
                constructor.Invoke(null, null);

                Import.ImportFileAndPrintData();
            }
            while (UserOptionsMethods.YesOrNo(question: "Czy chcesz zaimportować kolejny plik?"));

            return 0;
        }

    }
}