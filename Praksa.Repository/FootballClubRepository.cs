using Npgsql;
using Praksa.Common;

namespace Praksa.Repository
{
    public class FootballClubRepository:IFootballClubRepository
    {
        private readonly DatabaseHelper _db;
        public FootballClubRepository(DatabaseHelper db)
        {
            _db = db;
        }
        public async Task<List<FootballClub>> GetAllAsync(FootballClubFilter filter)
        {
            var clubs = new List<FootballClub>();
  
            using var connection = _db.GetConnection();
            connection.Open();

            var query = @"SELECT ""Id"", ""Name"", ""Country"", ""FoundedYear""
                          FROM ""FootballClub""
                          WHERE 1=1";

            using var command = new NpgsqlCommand();
            command.Connection = connection;

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query += @" AND ""Name"" ILIKE @name";
                command.Parameters.AddWithValue("@name", $"%{filter.Name}%");
            }

            if (!string.IsNullOrWhiteSpace(filter.Country))
            {
                query += @" AND ""Country"" ILIKE @country";
                command.Parameters.AddWithValue("@country", $"%{filter.Country}%");
            }

            if (filter.FoundedAfter.HasValue)
            {
                query += @" AND ""FoundedYear"" > @foundedAfter";
                command.Parameters.AddWithValue("@foundedAfter", filter.FoundedAfter.Value);
            }

            query += @" ORDER BY ""Id""";
            command.CommandText = query;

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
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

        public async Task<FootballClub?> GetByIdAsync(int id)
        {

            using var connection = _db.GetConnection();
            connection.Open();

            var query = @"SELECT ""Id"", ""Name"", ""Country"", ""FoundedYear""
                          FROM ""FootballClub""
                          WHERE ""Id"" = @id";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
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

        public async Task<int> GetNextIdAsync()
        {

            using var connection = _db.GetConnection();
            connection.Open();

            var query = @"SELECT COALESCE(MAX(""Id""), 0) + 1 FROM ""FootballClub""";
            using var command = new NpgsqlCommand(query, connection);

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task<FootballClub> InsertAsync(FootballClub club)
        {

            using var connection = _db.GetConnection();
            connection.Open();

            var query = @"INSERT INTO ""FootballClub"" (""Id"", ""Name"", ""Country"", ""FoundedYear"")
                          VALUES (@id, @name, @country, @foundedYear)";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", club.Id);
            command.Parameters.AddWithValue("@name", club.Name);
            command.Parameters.AddWithValue("@country", club.Country);
            command.Parameters.AddWithValue("@foundedYear", club.FoundedYear);

            await command.ExecuteNonQueryAsync();
            return club;
        }

        public async Task<FootballClub?> UpdateAsync(int id, FootballClub club)
        {
            var existing = await GetByIdAsync(id);
            if (existing == null)
                return null;

            using var connection = _db.GetConnection();
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

            await command.ExecuteNonQueryAsync();

            club.Id = id;
            return club;
        }

        public async Task<bool> DeleteAsync(int id)
        {

            using var connection = _db.GetConnection();
            connection.Open();

            var query = @"DELETE FROM ""FootballClub"" WHERE ""Id"" = @id";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            return await command.ExecuteNonQueryAsync() > 0;
        }
    }
}