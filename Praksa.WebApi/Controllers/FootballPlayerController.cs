using Microsoft.AspNetCore.Mvc;

namespace Praksa.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FootballPlayersController : ControllerBase
    {
        private static List<FootballPlayer> players = new List<FootballPlayer>
        {
            new FootballPlayer { Id = 1, Name = "Luka Modric", ClubId = 1, JerseyNumber = 10, Position = "Midfielder", MarketValue = 10.0 },
            new FootballPlayer { Id = 2, Name = "Erling Haaland", ClubId = 2, JerseyNumber = 9, Position = "Forward", MarketValue = 180.0 },
            new FootballPlayer { Id = 3, Name = "Josko Gvardiol", ClubId = 2, JerseyNumber = 24, Position = "Defender", MarketValue = 75.0 }
        };

        [HttpGet]
        public IActionResult GetAllPlayers(string? position = null, double? minValue = null)
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

            return Ok(result.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetPlayerById(int id)
        {
            var player = players.FirstOrDefault(p => p.Id == id);

            if (player == null)
            {
                return NotFound("Player not found.");
            }

            return Ok(player);
        }

        [HttpPost]
        public IActionResult AddPlayer([FromBody] FootballPlayer newPlayer)
        {
            if (newPlayer == null)
            {
                return BadRequest("Player data is required.");
            }

            newPlayer.Id = players.Any() ? players.Max(p => p.Id) + 1 : 1;
            players.Add(newPlayer);

            return Ok(newPlayer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlayer(int id, [FromBody] FootballPlayer updatedPlayer)
        {
            if (updatedPlayer == null)
            {
                return BadRequest("Updated player data is required.");
            }

            var existingPlayer = players.FirstOrDefault(p => p.Id == id);

            if (existingPlayer == null)
            {
                return NotFound("Player not found.");
            }

            existingPlayer.Name = updatedPlayer.Name;
            existingPlayer.ClubId = updatedPlayer.ClubId;
            existingPlayer.JerseyNumber = updatedPlayer.JerseyNumber;
            existingPlayer.Position = updatedPlayer.Position;
            existingPlayer.MarketValue = updatedPlayer.MarketValue;

            return Ok(existingPlayer);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlayer(int id)
        {
            var player = players.FirstOrDefault(p => p.Id == id);

            if (player == null)
            {
                return NotFound("Player not found.");
            }

            players.Remove(player);

            return Ok("Player deleted successfully.");
        }
    }
}