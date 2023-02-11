namespace Controller
{
    using DataModel.Classes;
    using DevExpress.Xpo;
    using System;
    using System.IO;
    using System.Linq;

    public static class InfoMethods
    {

        public static string GetInfosAboutDatabases(UnitOfWork uow)
        {
            string infos = string.Empty;

            var databases = uow.QueryInTransaction<Database>();
            var tables = uow.QueryInTransaction<Table>();
            var columns = uow.QueryInTransaction<Column>();

            foreach (Database db in databases)
            {
                var tablesThisDb = uow.QueryInTransaction<Table>().Where(x => x.ParentDatabase == db);

                infos += $"{Environment.NewLine}{Environment.NewLine}--------- Database '{db.Name}' ---------" +
                    $" {(tablesThisDb == null || tablesThisDb.Count() <= 0 ? 0 : tablesThisDb.Count())} tables";

                int tableNr = 1;

                foreach (Table tb in tablesThisDb)
                {
                    var columnsThisTb = uow.QueryInTransaction<Column>().Where(x => x.ParentTable == tb);

                    infos += $"{Environment.NewLine}{Environment.NewLine}{tableNr++}. Table '{tb.Schema?.Name}.{tb.Name}'" +
                        $" ({(columnsThisTb == null || columnsThisTb.Count() <= 0 ? 0 : columnsThisTb.Count())} columns)";

                    int columnNr = 1;

                    foreach (Column cl in columnsThisTb)
                    {
                        infos += $"{Environment.NewLine}{columnNr++}. Column '{cl.Name}' with {cl.DataType} data type" +
                            $" {(cl.IsNullable ? "accepts nulls" : "with no nulls")}";
                    }
                }
            }
            return infos;
        }

        public static void SaveMessages(string message, string path, string fileName, MessageType msgType, out string textFilePath)
        {
            textFilePath = string.Empty;

            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(fileName + path))
            {
                throw new Exception("Pusta nazwa lub ścieżka");
            }

            textFilePath = Path.Combine(
                path1: path,
                path2: $"importData_{DateTime.Now:yyyy-MM-dd_hhmmss}_{fileName.Replace(".", "")}_{msgType}.txt");

            using (StreamWriter sww = new StreamWriter(textFilePath))
            {
                sww.Write($"{Environment.NewLine}{Environment.NewLine}-------------------- {msgType.ToString().ToUpper()} --------------------" +
                $"{Environment.NewLine}{message}");
            }

            System.Diagnostics.Process.Start(textFilePath);
        }

        public enum MessageType
        {
            infos = 0,
            errors = 1,
        }

    }
}
