using System;
using System.Linq;
using Infrastructure.Extend;

namespace Infrastructure.Cache {
    public enum BuildSqlType {

        findone,
        insert
    }

    //泛型缓存
    public class SqlBuilderCache<T> {
        private static string _insertSql = null;
        private static string _findOneSql = null;
        static SqlBuilderCache () {
            Type type = typeof (T); {
                string column = string.Join (",", type.GetProperties ().Select (x => x.GetPropAttuibuteMapping ()));
                _findOneSql = $"select {column} from  {type.GetPropAttuibuteMapping()}  where  id=@Id";
            }

            {

                string columnName = string.Join (",", type.GetFilterProp ().Select (x => $"'{x.GetPropAttuibuteMapping()}'"));
                // string columnValue = string.Join (",", type.GetFilterProp ().Select (x => $"'{x.GetValue(t)}'"));
                string columnValue = string.Join (",", type.GetFilterProp ().Select (x => $"@{x.GetPropAttuibuteMapping()}"));
                _insertSql = $"insert into   {type.GetPropAttuibuteMapping()}   ({columnName})  values    ({columnValue})";
            }

        }

        public static string GetSql (BuildSqlType type) {
            switch (type) {
                case BuildSqlType.findone:
                    return _findOneSql;
                case BuildSqlType.insert:
                    return _insertSql;
                    // default throw Exception("");
            }
            return _insertSql;
        }

        public static string GetFindOneSql () {
            return _findOneSql;
        }

    }
}