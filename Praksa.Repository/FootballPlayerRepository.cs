using Npgsql;
using Praksa.Common;
using Praksa.Repository.Common;

namespace Praksa.Repository
{
    public class FootballPlayerRepository:IFootballPlayerRepository
    {
        private readonly DatabaseHelper _db;
        public FootballPlayerRepository(DatabaseHelper db)
        {
            _db = db;
        }

        public async Task<List<FootballPlayer>> GetAllAsync(FootballPlayerFilter filter)
        {
            var players = new List<FootballPlayer>();

            using var connection = _db.GetConnection();
            connection.Open();

            var query = @"SELECT ""Id"", ""Name"", ""ClubId"", ""JerseyNumber"", ""Position"", ""MarketValue""
                          FROM ""FootballPlayer""
                          WHERE 1=1";

            using var command = new NpgsqlCommand();
            command.Connection = connection;

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query += @" AND ""Name"" ILIKE @name";
                command.Parameters.AddWithValue("@name", $"%{filter.Name}%");
            }

            if (!string.IsNullOrWhiteSpace(filter.Position))
            {
                query += @" AND ""Position"" ILIKE @position";
                command.Parameters.AddWithValue("@position", $"%{filter.Position}%");
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

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
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

        public async Task<FootballPlayer?> GetByIdAsync(int id)
        {
            using var connection = _db.GetConnection();
            connection.Open();

            var query = @"SELECT ""Id"", ""Name"", ""ClubId"", ""JerseyNumber"", ""Position"", ""MarketValue""
                          FROM ""FootballPlayer""
                          WHERE ""Id"" = @id";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
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

        public async Task<List<FootballPlayer>> GetByClubIdAsync(int clubId)
        {
            var players = new List<FootballPlayer>();

            using var connection = _db.GetConnection();
            connection.Open();

            var query = @"SELECT ""Id"", ""Name"", ""ClubId"", ""JerseyNumber"", ""Position"", ""MarketValue""
                          FROM ""FootballPlayer""
                          WHERE ""ClubId"" = @clubId
                          ORDER BY ""Id""";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@clubId", clubId);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
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

        public async Task<int> GetNextIdAsync()
        {
            using var connection = _db.GetConnection();
            connection.Open();

            var query = @"SELECT COALESCE(MAX(""Id""), 0) + 1 FROM ""FootballPlayer""";
            using var command = new NpgsqlCommand(query, connection);

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task<FootballPlayer> InsertAsync(FootballPlayer player)
        {
            using var connection = _db.GetConnection();
            connection.Open();

            var query = @"INSERT INTO ""FootballPlayer""
                          (""Id"", ""Name"", ""ClubId"", ""JerseyNumber"", ""Position"", ""MarketValue"")
                          VALUES (@id, @name, @clubId, @jerseyNumber, @position, @marketValue)";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", player.Id);
            command.Parameters.AddWithValue("@name", player.Name);
            command.Parameters.AddWithValue("@clubId", player.ClubId);
            command.Parameters.AddWithValue("@jerseyNumber", player.JerseyNumber);
            command.Parameters.AddWithValue("@position", player.Position);
            command.Parameters.AddWithValue("@marketValue", player.MarketValue);

            await command.ExecuteNonQueryAsync();
            return player;
        }

        public async Task<FootballPlayer?> UpdateAsync(int id, FootballPlayer player)
        {
            var existing = await GetByIdAsync(id);
            if (existing == null)
                return null;

            using var connection = _db.GetConnection();
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

            await command.ExecuteNonQueryAsync();

            player.Id = id;
            return player;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _db.GetConnection();
            connection.Open();

            var query = @"DELETE FROM ""FootballPlayer"" WHERE ""Id"" = @id";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            return await command.ExecuteNonQueryAsync() > 0;
        }
    }
}