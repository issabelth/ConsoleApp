using Controller;
using System;
using System.IO;
using Utilities.ConsoleCommands;

namespace ConsoleApp
{
    public static class FileChecking
    {

        /// <summary>
        /// Asks if the user wants to try to import the file again until it's not locked.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>false if the user doesn't want to try to import the file anymore; true if the file is not locked</returns>
        public static bool EnsureFileIsNotLocked(string filePath)
        {
            while (!FileController.FileIsNotLocked(filePath))
            {
                ConsoleWriteError.WriteTheError($"Plik jest otwarty. Zamknij go i ponownie zaimportuj.");

                if (!UserOptionsMethods.YesOrNo(question: "Czy spróbować ponownie zaimportować plik?"))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Asks the user to give the proper file path until it's correct
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>return the existing file path</returns>
        public static string EnsureFileExists(string filePath)
        {
            while (!File.Exists(filePath))
            {
                ConsoleWriteError.WriteTheError("Błędna ścieżka!");
                Console.WriteLine("Podaj ścieżkę pliku:");
                filePath = Console.ReadLine();
            }

            return filePath;
        }
    }
}
