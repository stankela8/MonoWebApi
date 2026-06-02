using Microsoft.AspNetCore.Mvc;

namespace Praksa.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FootballPlayersController : ControllerBase
    {
        private static List<FootballPlayer> players = new List<FootballPlayer>
        {
            new FootballPlayer
            {
                Id = 1,
                Name = "Luka Modric",
                ClubId = 1,
                JerseyNumber = 10,
                Position = "Midfielder",
                MarketValue = 8.5
            },
            new FootballPlayer
            {
                Id = 2,
                Name = "Josko Gvardiol",
                ClubId = 2,
                JerseyNumber = 24,
                Position = "Defender",
                MarketValue = 75.0
            },
            new FootballPlayer
            {
                Id = 3,
                Name = "Erling Haaland",
                ClubId = 2,
                JerseyNumber = 9,
                Position = "Forward",
                MarketValue = 180.0
            }
        };

        [HttpGet]
        public List<FootballPlayer> GetAllPlayers()
        {
            return players;
        }

        [HttpGet("{id}")]
        public FootballPlayer GetPlayerById(int id)
        {
            return players.FirstOrDefault(p => p.Id == id);
        }

        [HttpPost]
        public string AddPlayer(FootballPlayer newPlayer)
        {
            newPlayer.Id = players.Max(p => p.Id) + 1;
            players.Add(newPlayer);
            return "Player added successfully.";
        }

        [HttpPut("{id}")]
        public string UpdatePlayer(int id, FootballPlayer updatedPlayer)
        {
            var player = players.FirstOrDefault(p => p.Id == id);

            if (player == null)
            {
                return "Player not found.";
            }

            player.Name = updatedPlayer.Name;
            player.ClubId = updatedPlayer.ClubId;
            player.JerseyNumber = updatedPlayer.JerseyNumber;
            player.Position = updatedPlayer.Position;
            player.MarketValue = updatedPlayer.MarketValue;

            return "Player updated successfully.";
        }

        [HttpDelete("{id}")]
        public string DeletePlayer(int id)
        {
            var player = players.FirstOrDefault(p => p.Id == id);

            if (player == null)
            {
                return "Player not found.";
            }

            players.Remove(player);
            return "Player deleted successfully.";
        }

        [HttpGet("search")]
        public List<FootballPlayer> SearchPlayers(string? position = null, double? minValue = null)
        {
            var result = players.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(position))
            {
                result = result.Where(p => p.Position.ToLower() == position.ToLower());
            }

            if (minValue.HasValue)
            {
                result = result.Where(p => p.MarketValue >= minValue.Value);
            }

            return result.ToList();
        }

    }
}