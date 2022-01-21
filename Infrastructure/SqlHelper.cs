using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Infrastructure.Cache;
using Infrastructure.ExpressionExtend;
using Infrastructure.Extend;
using Infrastructure.Model;
namespace Infrastructure {
    public class SqlHelper {

        private readonly string connRead=  SqlConnectionPool.GetConnectionString (SqlConnectionPool.SqlConnectionType.Read);
          private readonly string connWrite=  SqlConnectionPool.GetConnectionString (SqlConnectionPool.SqlConnectionType.Write);

        public IList<T> FindCondition<T> (Expression<Func<T, bool>> func) where T : BaseModel, new () {
            Type type = typeof (T);
            string columnsString = string.Join (",", type.GetProperties ().Select (p => $"[{p.GetPropAttuibuteMapping()}]"));
            string where = func.ToWhere<T> (out List<SqlParameter> parameters);
            string sql = $"SELECT {columnsString} FROM [{type.GetPropAttuibuteMapping()}] WHERE {where}";
          
            Console.WriteLine ($"当前查询的字符串为{connRead}");
            using (SqlConnection conn = new SqlConnection (connRead)) {
                SqlCommand command = new SqlCommand (sql, conn);
                command.Parameters.AddRange (parameters.ToArray ());
                conn.Open ();
                var reader = command.ExecuteReader ();
                IList<T> result = new List<T> ();
                while (reader.Read ()) {
                    T t = new T ();
                    foreach (var prop in type.GetProperties ()) {
                        string propName = prop.GetPropAttuibuteMapping ();
                        prop.SetValue (t, reader[propName] is DBNull ? null : reader[propName]);
                    }
                    result.Add (t);
                }
                return result;
            }
        }

        public T Find<T> (int id) where T : BaseModel, new () {

            Type type = typeof (T);

            using (SqlConnection connection = new SqlConnection (connRead)) {

                // string column = string.Join (",", type.GetProperties ().Select (x => x.GetPropAttuibuteMapping ()));
                // string sql = $"select {column} from  {type.GetPropAttuibuteMapping()}  where  id=@Id";
                string sql = SqlBuilderCache<T>.GetSql (BuildSqlType.findone);
                var para = new SqlParameter ("@Id", id);
                connection.Open ();
                var command = connection.CreateCommand ();
                command.CommandText = sql;
                command.Parameters.Add (para);
                var reader = command.ExecuteReader ();

                if (reader.Read ()) {
                    T t = new T ();
                    foreach (var prop in type.GetProperties ()) {
                        //查询时as一下，prop.GetPropAttuibuteMapping ()可以不获取特性值，直接获取属性名，ef就是这么做的
                        prop.SetValue (t, reader[prop.GetPropAttuibuteMapping ()] is DBNull? null : reader[prop.Name]);

                    }
                    return t;
                }
                return default;
            }

        }

        public bool Insert<T> (T t) where T : BaseModel, new () {

            //sqlinject  user.Name =  "教育');delete from  User  where id =2;--";

            Type type = t.GetType ();
            // string columnName = string.Join (",", type.GetFilterProp ().Select (x => $"'{x.GetPropAttuibuteMapping()}'"));
            // // string columnValue = string.Join (",", type.GetFilterProp ().Select (x => $"'{x.GetValue(t)}'"));
            // string columnValue = string.Join (",", type.GetFilterProp ().Select (x => $"@{x.GetPropAttuibuteMapping()}"));
            // string sql = $"insert into   {type.GetPropAttuibuteMapping()}   ({columnName})  values    ({columnValue})";
            string sql = SqlBuilderCache<T>.GetSql (BuildSqlType.insert);
            var paras = type.GetFilterProp ().Select (x => new SqlParameter ($"@{x.GetPropAttuibuteMapping()}", x.GetValue (t) ?? DBNull.Value)).ToArray ();
            using (SqlConnection connection = new SqlConnection (connWrite)) {

                connection.Open ();
                var command = connection.CreateCommand ();
                command.CommandText = sql;
                command.Parameters.AddRange (paras);
                var result = command.ExecuteNonQuery ();

                return result > 0;
            }

        }

        //sqlinject
        // public bool Insert<T> (T t) where T : BaseModel, new () {

        //     //sqlinject  user.Name =  "教育');delete from  User  where id =2;--";

        //     Type type = t.GetType ();
        //     string columnName = string.Join (",", type.GetFilterProp ().Select (x => $"'{x.GetPropAttuibuteMapping()}'"));
        //     string columnValue = string.Join (",", type.GetFilterProp ().Select (x => $"'{x.GetValue(t)}'"));
        //     string sql = $"insert into   {type.GetPropAttuibuteMapping()}   ({columnName})  values    ({columnValue})";

        //     using (SqlConnection connection = new SqlConnection (connStr)) {

        //         connection.Open ();
        //         var command = connection.CreateCommand ();
        //         command.CommandText = sql;
        //         var result = command.ExecuteNonQuery ();

        //         return result > 0;
        //     }

        // }

    }
}