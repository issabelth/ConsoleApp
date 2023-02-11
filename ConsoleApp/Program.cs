namespace ConsoleApp
{
    using ConsoleApp.ConsoleCommands;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities.ConsoleCommands;
    using Utilities.ExtensionsMethods;

    internal class Program
    {
        private static readonly List<CommandAction> _cmdList = CommandsList.AllCommandsList;

        static void Main(string[] args)
        {
            do
            {
                var actionKey = GetChosenCommand(_cmdList);
                var myAct = _cmdList.Where(x => x.Key.ToLower() == actionKey).FirstOrDefault()?.Action;

                while (myAct == null)
                {
                    ConsoleWriteError.WriteTheError("Nie rozpoznano akcji.");
                    actionKey = GetChosenCommand(_cmdList);
                    myAct = _cmdList.Where(x => x.Key.ToLower() == actionKey).FirstOrDefault()?.Action;
                }

                Console.WriteLine();
                myAct.Invoke();
            }
            while (UserOptionsMethods.YesOrNo(question: "Czy chcesz coś jeszcze zrobić?"));

            Console.WriteLine("Naciśnij dowolny przycisk, aby wyjść.");
            Console.ReadKey();
        }

        private static string GetChosenCommand(List<CommandAction> cmdList)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Environment.NewLine}Możliwe opcje:");

            foreach (var command in cmdList)
            {
                Console.WriteLine($"{command.Key} - {command.ActionName}");
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            return Console.ReadLine().ClearWhiteSpacesAndToLower();
        }

    }
}
