using Npgsql;
using Praksa.Common;

namespace Praksa.Repository
{
    public class FootballClubRepository
    {
        public List<FootballClub> GetAll(FootballClubFilter filter)
        {
            var clubs = new List<FootballClub>();
            var db = new DatabaseHelper();

            using var connection = db.GetConnection();
            connection.Open();

            var query = @"SELECT ""Id"", ""Name"", ""Country"", ""FoundedYear""
                          FROM ""FootballClub""
                          WHERE 1=1";

            using var command = new NpgsqlCommand();
            command.Connection = connection;

            if (!string.IsNullOrWhiteSpace(filter.Country))
            {
                query += @" AND LOWER(""Country"") = LOWER(@country)";
                command.Parameters.AddWithValue("@country", filter.Country);
            }

            if (filter.FoundedAfter.HasValue)
            {
                query += @" AND ""FoundedYear"" > @foundedAfter";
                command.Parameters.AddWithValue("@foundedAfter", filter.FoundedAfter.Value);
            }

            query += @" ORDER BY ""Id""";
            command.CommandText = query;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                clubs.Add(new FootballClub
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Country = reader["Country"].ToString(),
                    FoundedYear = Convert.ToInt32(reader["FoundedYear"])
                });
            }

            return clubs;
        }

        public FootballClub? GetById(int id)
        {
            var db = new DatabaseHelper();

            using var connection = db.GetConnection();
            connection.Open();

            var query = @"SELECT ""Id"", ""Name"", ""Country"", ""FoundedYear""
                          FROM ""FootballClub""
                          WHERE ""Id"" = @id";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new FootballClub
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Country = reader["Country"].ToString(),
                    FoundedYear = Convert.ToInt32(reader["FoundedYear"])
                };
            }

            return null;
        }

        public void Insert(FootballClub club)
        {
            var db = new DatabaseHelper();

            using var connection = db.GetConnection();
            connection.Open();

            var query = @"INSERT INTO ""FootballClub"" (""Id"", ""Name"", ""Country"", ""FoundedYear"")
                          VALUES (@id, @name, @country, @foundedYear)";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", club.Id);
            command.Parameters.AddWithValue("@name", club.Name);
            command.Parameters.AddWithValue("@country", club.Country);
            command.Parameters.AddWithValue("@foundedYear", club.FoundedYear);

            command.ExecuteNonQuery();
        }

        public bool Update(int id, FootballClub club)
        {
            var db = new DatabaseHelper();

            using var connection = db.GetConnection();
            connection.Open();

            var query = @"UPDATE ""FootballClub""
                          SET ""Name"" = @name,
                              ""Country"" = @country,
                              ""FoundedYear"" = @foundedYear
                          WHERE ""Id"" = @id";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", club.Name);
            command.Parameters.AddWithValue("@country", club.Country);
            command.Parameters.AddWithValue("@foundedYear", club.FoundedYear);

            return command.ExecuteNonQuery() > 0;
        }

        public bool Delete(int id)
        {
            var db = new DatabaseHelper();

            using var connection = db.GetConnection();
            connection.Open();

            var query = @"DELETE FROM ""FootballClub"" WHERE ""Id"" = @id";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            return command.ExecuteNonQuery() > 0;
        }

        public int GetNextId()
        {
            var db = new DatabaseHelper();

            using var connection = db.GetConnection();
            connection.Open();

            var query = @"SELECT COALESCE(MAX(""Id""), 0) + 1 FROM ""FootballClub""";

            using var command = new NpgsqlCommand(query, connection);
            return Convert.ToInt32(command.ExecuteScalar());
        }
    }
}