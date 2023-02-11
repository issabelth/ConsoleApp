using System.Collections.Generic;

namespace Utilities.Dictionaries
{
    public static partial class Dictionaries
    {

        public static readonly Dictionary<SqlObject, string> sqlObjDictForTable = new Dictionary<SqlObject, string>()
        {
            { SqlObject.database, "baza" },
            { SqlObject.schema, "schemat" },
        };

        public static readonly Dictionary<SqlObject, string> sqlObjDictForColumn = new Dictionary<SqlObject, string>()
        {
            { SqlObject.table, "tabela" },
            { SqlObject.schema, "schemat" },
        };

        public static Dictionary<SqlObject, string> SqlObjDictForData(SqlObject sqlObj)
        {
            switch (sqlObj)
            {
                case SqlObject.table:
                    {
                        return sqlObjDictForTable;
                    }
                case SqlObject.column:
                    {
                        return sqlObjDictForColumn;
                    }
            }
            return null;
        }

        public enum SqlObject
        {
            none,
            database,
            table,
            column,
            schema,
        };

    }
}
