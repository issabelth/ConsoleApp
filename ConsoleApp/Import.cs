using Controller;
using System;
using System.IO;
using Utilities.ConsoleCommands;
using Utilities.Exceptions;

namespace ConsoleApp
{
    public static class Import
    {

        public static void ImportFileAndPrintData()
        {
            Console.WriteLine("Podaj ścieżkę pliku:");
            var filePath = Console.ReadLine();

            filePath = FileChecking.EnsureFileExists(filePath);

            Console.WriteLine("Podaj rozdzielacz (delimiter) w Twoim pliku:");
            var delimiter = Console.ReadKey().KeyChar;

            bool printData = UserOptionsMethods.YesOrNo(question: "Czy chcesz wyświetlić w konsoli informacje o zaimportowanych danych?");

            if (!FileChecking.EnsureFileIsNotLocked(filePath))
            {
                return;
            }

            try
            {
                string errors = string.Empty;
                string infos = string.Empty;
                var reader = new DataReader();

                reader.ImportData(
                    fileToImport: filePath,
                    delimiter: delimiter,
                    errors: out errors,
                    infos: out infos);

                WriteAndSaveMessages(
                    filePath: filePath,
                    printData: printData,
                    errors: errors,
                    infos: infos);
            }
            catch (WrongDelimiterException ex)
            {
                ConsoleWriteError.WriteTheError(ex.Message);
            }
        }

        private static void WriteAndSaveMessages(string filePath, bool printData, string errors, string infos)
        {
            string pathForFiles = Path.Combine(
                path1: FileController.GetFileDirectory(filePath),
                path2: $"importData_{DateTime.Today:yyyy-MM-dd}");

            if (!Directory.Exists(pathForFiles))
            {
                Directory.CreateDirectory(pathForFiles);
            }

            string textFilePath;

            if (string.IsNullOrWhiteSpace(errors))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{Environment.NewLine}Brak błędów");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                InfoMethods.SaveMessages(
                    message: errors,
                    path: pathForFiles,
                    fileName: FileController.GetFileName(filePath),
                    msgType: InfoMethods.MessageType.errors,
                    textFilePath: out textFilePath);

                ConsoleWriteError.WriteTheError($"{Environment.NewLine}-------------- ERRORS --------------" +
                    $"{Environment.NewLine}plik z błędami został zapisany w {textFilePath}{errors}");
            }

            if (!string.IsNullOrWhiteSpace(infos))
            {
                InfoMethods.SaveMessages(
                    message: infos,
                    path: pathForFiles,
                    fileName: FileController.GetFileName(filePath),
                    msgType: InfoMethods.MessageType.infos,
                    textFilePath: out textFilePath);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{Environment.NewLine}-------------- INFOS --------------");
                Console.WriteLine($"{Environment.NewLine}plik z informacjami został zapisany w {textFilePath}");

                if (printData)
                {
                    Console.WriteLine($"{infos}{Environment.NewLine}");
                }

                Console.ForegroundColor = ConsoleColor.White;
            }
        }

    }
}
