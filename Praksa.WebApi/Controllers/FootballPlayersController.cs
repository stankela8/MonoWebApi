using Microsoft.AspNetCore.Mvc;
using Praksa.Common;
using Praksa.Service;

namespace Praksa.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FootballPlayersController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllPlayersAsync([FromQuery] FootballPlayerFilter filter)
        {
            var service = new FootballPlayerService();
            var players = await service.GetAllPlayersAsync(filter);
            return Ok(players);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayerByIdAsync(int id)
        {
            var service = new FootballPlayerService();
            var player = await service.GetPlayerByIdAsync(id);

            if (player == null)
                return NotFound("Player not found.");

            return Ok(player);
        }

        [HttpGet("byClub/{clubId}")]
        public async Task<IActionResult> GetPlayersByClubIdAsync(int clubId)
        {
            var service = new FootballPlayerService();
            var players = await service.GetPlayersByClubIdAsync(clubId);
            return Ok(players);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayerAsync([FromBody] FootballPlayer player)
        {
            if (player == null)
                return BadRequest("Player data is required.");

            var service = new FootballPlayerService();
            var createdPlayer = await service.AddPlayerAsync(player);

            if (createdPlayer == null)
                return BadRequest("ClubId does not exist.");

            return Ok(createdPlayer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayerAsync(int id, [FromBody] FootballPlayer player)
        {
            if (player == null)
                return BadRequest("Updated player data is required.");

            var service = new FootballPlayerService();
            var updatedPlayer = await service.UpdatePlayerAsync(id, player);

            if (updatedPlayer == null)
                return BadRequest("Player not found or ClubId does not exist.");

            return Ok(updatedPlayer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayerAsync(int id)
        {
            var service = new FootballPlayerService();
            var deleted = await service.DeletePlayerAsync(id);

            if (!deleted)
                return NotFound("Player not found.");

            return Ok("Player deleted successfully.");
        }
    }
}