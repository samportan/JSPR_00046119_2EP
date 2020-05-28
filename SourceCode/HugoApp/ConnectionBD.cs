using System.Data;
using Npgsql;

namespace HugoApp
{
    public static class ConnectionBD
    {
        private static readonly string host = "127.0.0.1";

        private static readonly string database = "hugo";

        private static readonly string userId = "postgres";

        private static readonly string password = "uca";

        private static readonly string sConnection =
            $"Server={host};Port=5432;User Id={userId};Password={password};Database={database};";
        //    $"sslmode=Require;Trust Server Certificate=true";

        public static DataTable ExecuteQuery(string query)
        {
            var connection = new NpgsqlConnection(sConnection);
            var ds = new DataSet();

            connection.Open();

            var da = new NpgsqlDataAdapter(query, connection);
            da.Fill(ds);

            connection.Close();

            return ds.Tables[0];
        }

        public static void ExecuteNonQuery(string act)
        {
            var connection = new NpgsqlConnection(sConnection);

            connection.Open();

            var command = new NpgsqlCommand(act, connection);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}