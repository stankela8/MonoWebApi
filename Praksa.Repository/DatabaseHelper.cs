using Npgsql;

namespace Praksa.Repository
{
    public class DatabaseHelper
    {
        private readonly string _connectionString =
            "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=webapi";

        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}