namespace Controller
{
    using DataModel;
    using DevExpress.Xpo;
    using System;
    using System.IO;
    using Utilities.Exceptions;
    using Utilities.ExtensionsMethods;
    using static Utilities.Dictionaries.Dictionaries;

    public partial class DataReader
    {
        string _errors = string.Empty;
        string _infos = string.Empty;
        UnitOfWork _uow = OneUnitOfWork.uow;

        public void ImportData(string fileToImport, char delimiter, out string errors, out string infos)
        {
            if (string.IsNullOrWhiteSpace(fileToImport))
            {
                throw new Exception("Brak nazwy pliku");
            }
            if (char.IsWhiteSpace(delimiter))
            {
                throw new Exception("Brak delimitera");
            }

            using (var streamReader = new StreamReader(fileToImport))
            {
                string name, schemaName, parentName, parentType, dataType;
                string isNullable;

                try
                {
                    while (!streamReader.EndOfStream)
                    {
                        var line = streamReader.ReadLine();

                        var values = line.Split(delimiter);

                        if (string.IsNullOrWhiteSpace(values[0]))
                        {
                            continue;
                        }

                        var objectType = values[0].ClearWhiteSpacesAndToLower();

                        try
                        {
                            name = values[1];
                            schemaName = values[2];
                            parentName = values[3];
                            parentType = values[4];
                            dataType = values[5];
                            isNullable = values[6];
                        }
                        catch (Exception ex)
                        {
                            throw new WrongDelimiterException("Błąd przy pobieraniu informacji. Być może dałeś zły delimiter?");
                        }

                        AddAdequateObject(
                            name: name,
                            schemaName: schemaName,
                            parentName: parentName,
                            parentType: parentType,
                            dataType: dataType,
                            isNullable: isNullable,
                            objectType: objectType);
                    }

                    _infos += InfoMethods.GetInfosAboutDatabases(_uow);
                }
                catch (AddingNewObjectException ex)
                {
                    _errors +=
                        $"{Environment.NewLine}Błąd podczas importu:" +
                        $"{Environment.NewLine}{ex.Message}";
                }
                catch (WrongDelimiterException ex)
                {
                    throw new WrongDelimiterException(ex.Message);
                }
                catch (Exception ex)
                {
                    _errors +=
                        $"{Environment.NewLine}Nieobsłużony błąd podczas importu:" +
                        $"{Environment.NewLine}{ex.Message}" +
                        $"{Environment.NewLine}Pełna treść błędu:" +
                        $"{Environment.NewLine}{ex}";
                }
                finally
                {
                    errors = _errors;
                    infos = _infos;
                    streamReader.Close();
                }
            }
        }

        private void AddAdequateObject(string name, string schemaName, string parentName, string parentType, string dataType,
            string isNullable, string objectType)
        {
            if (objectType == SqlObject.database.ToString())
            {
                AddNewObject.AddNewDatabase(
                    name: name,
                    uow: _uow,
                    errors: ref _errors);
            }
            else if (objectType == SqlObject.table.ToString())
            {
                var table = AddNewObject.AddNewTable(
                    name: name,
                    schemaName: schemaName,
                    parentName: parentName,
                    parentType: parentType,
                    uow: _uow,
                    errors: ref _errors);

                table.CheckParentType(
                    parentType: parentType,
                    uow: _uow,
                    errors: ref _errors);

                table.SetParent(
                    uow: _uow,
                    errors: ref _errors);

                table.SetSchema(
                    uow: _uow,
                    errors: ref _errors);
            }
            else if (objectType == SqlObject.column.ToString())
            {
                var column = AddNewObject.AddNewColumn(
                    name: name,
                    schemaName: schemaName,
                    parentName: parentName,
                    parentType: parentType,
                    dataType: dataType,
                    isNullable: isNullable,
                    uow: _uow,
                    errors: ref _errors);

                column.CheckParentType(
                    parentType: parentType,
                    uow: _uow,
                    errors: ref _errors);

                column.SetParent(
                    uow: _uow,
                    errors: ref _errors);

                column.SetSchema(
                    uow: _uow,
                    errors: ref _errors);
            }
            else if (objectType == SqlObject.schema.ToString())
            {
                AddNewObject.AddNewSchema(
                    name: name,
                    uow: _uow,
                    errors: ref _errors);
            }
        }

    }
}
