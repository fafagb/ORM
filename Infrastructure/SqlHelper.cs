using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Infrastructure.Extend;
using Infrastructure.Model;
namespace Infrastructure {
    public class SqlHelper {

        private readonly string connStr = "";

        public T Find<T> (int id) where T : BaseModel, new () {

            Type type = typeof (T);

            using (SqlConnection connection = new SqlConnection (connStr)) {

                string column = string.Join (",", type.GetProperties ().Select (x => x.GetPropAttuibuteMapping ()));
                string sql = $"select {column} from  {type.GetPropAttuibuteMapping()}  where  id={id}";
                connection.Open ();
                var command = connection.CreateCommand ();
                command.CommandText = sql;
                var reader = command.ExecuteReader ();

                if (reader.Read ()) {
                    T t = new T ();
                    foreach (var prop in type.GetProperties ()) {
                        prop.SetValue (t, reader[prop.GetPropAttuibuteMapping ()] is DBNull? null : reader[prop.Name]);
                    }
                    return t;
                }
                return default;
            }

        }

        public bool Insert<T> (T t) where T : BaseModel, new () {
            Type type = t.GetType ();
            string columnName = string.Join (",", type.GetFilterProp ().Select (x => $"'{x.GetPropAttuibuteMapping()}'"));
            string columnValue = string.Join (",", type.GetFilterProp ().Select (x => $"'{x.GetValue(t)}'"));
            string sql = $"insert into   {type.GetPropAttuibuteMapping()}   ({columnName})  values    ({columnValue})";

            using (SqlConnection connection = new SqlConnection (connStr)) {

                connection.Open ();
                var command = connection.CreateCommand ();
                command.CommandText = sql;
                var result = command.ExecuteNonQuery ();

                return result > 0;
            }

        }

    }
}