using Npgsql;
using Praksa.Common;

namespace Praksa.Repository
{
    public class FootballPlayerRepository
    {
        public List<FootballPlayer> GetAll(FootballPlayerFilter filter)
        {
            var players = new List<FootballPlayer>();
            var db = new DatabaseHelper();

            using var connection = db.GetConnection();
            connection.Open();

            var query = @"SELECT ""Id"", ""Name"", ""ClubId"", ""JerseyNumber"", ""Position"", ""MarketValue""
                          FROM ""FootballPlayer""
                          WHERE 1=1";

            using var command = new NpgsqlCommand();
            command.Connection = connection;

            if (!string.IsNullOrWhiteSpace(filter.Position))
            {
                query += @" AND LOWER(""Position"") = LOWER(@position)";
                command.Parameters.AddWithValue("@position", filter.Position);
            }

            if (filter.MinValue.HasValue)
            {
                query += @" AND ""MarketValue"" >= @minValue";
                command.Parameters.AddWithValue("@minValue", filter.MinValue.Value);
            }

            if (filter.ClubId.HasValue)
            {
                query += @" AND ""ClubId"" = @clubId";
                command.Parameters.AddWithValue("@clubId", filter.ClubId.Value);
            }

            query += @" ORDER BY ""Id""";
            command.CommandText = query;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                players.Add(new FootballPlayer
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    ClubId = Convert.ToInt32(reader["ClubId"]),
                    JerseyNumber = Convert.ToInt32(reader["JerseyNumber"]),
                    Position = reader["Position"].ToString(),
                    MarketValue = Convert.ToDouble(reader["MarketValue"])
                });
            }

            return players;
        }

        public FootballPlayer? GetById(int id)
        {
            var db = new DatabaseHelper();

            using var connection = db.GetConnection();
            connection.Open();

            var query = @"SELECT ""Id"", ""Name"", ""ClubId"", ""JerseyNumber"", ""Position"", ""MarketValue""
                          FROM ""FootballPlayer""
                          WHERE ""Id"" = @id";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new FootballPlayer
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    ClubId = Convert.ToInt32(reader["ClubId"]),
                    JerseyNumber = Convert.ToInt32(reader["JerseyNumber"]),
                    Position = reader["Position"].ToString(),
                    MarketValue = Convert.ToDouble(reader["MarketValue"])
                };
            }

            return null;
        }

        public List<FootballPlayer> GetByClubId(int clubId)
        {
            var players = new List<FootballPlayer>();
            var db = new DatabaseHelper();

            using var connection = db.GetConnection();
            connection.Open();

            var query = @"SELECT ""Id"", ""Name"", ""ClubId"", ""JerseyNumber"", ""Position"", ""MarketValue""
                  FROM ""FootballPlayer""
                  WHERE ""ClubId"" = @clubId
                  ORDER BY ""Id""";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@clubId", clubId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                players.Add(new FootballPlayer
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    ClubId = Convert.ToInt32(reader["ClubId"]),
                    JerseyNumber = Convert.ToInt32(reader["JerseyNumber"]),
                    Position = reader["Position"].ToString(),
                    MarketValue = Convert.ToDouble(reader["MarketValue"])
                });
            }

            return players;
        }

        public void Insert(FootballPlayer player)
        {
            var db = new DatabaseHelper();

            using var connection = db.GetConnection();
            connection.Open();

            var query = @"INSERT INTO ""FootballPlayer"" (""Id"", ""Name"", ""ClubId"", ""JerseyNumber"", ""Position"", ""MarketValue"")
                          VALUES (@id, @name, @clubId, @jerseyNumber, @position, @marketValue)";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", player.Id);
            command.Parameters.AddWithValue("@name", player.Name);
            command.Parameters.AddWithValue("@clubId", player.ClubId);
            command.Parameters.AddWithValue("@jerseyNumber", player.JerseyNumber);
            command.Parameters.AddWithValue("@position", player.Position);
            command.Parameters.AddWithValue("@marketValue", player.MarketValue);

            command.ExecuteNonQuery();
        }

        public bool Update(int id, FootballPlayer player)
        {
            var db = new DatabaseHelper();

            using var connection = db.GetConnection();
            connection.Open();

            var query = @"UPDATE ""FootballPlayer""
                          SET ""Name"" = @name,
                              ""ClubId"" = @clubId,
                              ""JerseyNumber"" = @jerseyNumber,
                              ""Position"" = @position,
                              ""MarketValue"" = @marketValue
                          WHERE ""Id"" = @id";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", player.Name);
            command.Parameters.AddWithValue("@clubId", player.ClubId);
            command.Parameters.AddWithValue("@jerseyNumber", player.JerseyNumber);
            command.Parameters.AddWithValue("@position", player.Position);
            command.Parameters.AddWithValue("@marketValue", player.MarketValue);

            return command.ExecuteNonQuery() > 0;
        }

        public bool Delete(int id)
        {
            var db = new DatabaseHelper();

            using var connection = db.GetConnection();
            connection.Open();

            var query = @"DELETE FROM ""FootballPlayer"" WHERE ""Id"" = @id";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            return command.ExecuteNonQuery() > 0;
        }

        public int GetNextId()
        {
            var db = new DatabaseHelper();

            using var connection = db.GetConnection();
            connection.Open();

            var query = @"SELECT COALESCE(MAX(""Id""), 0) + 1 FROM ""FootballPlayer""";

            using var command = new NpgsqlCommand(query, connection);
            return Convert.ToInt32(command.ExecuteScalar());
        }
    }
}