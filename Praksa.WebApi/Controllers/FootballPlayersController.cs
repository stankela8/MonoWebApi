using Microsoft.AspNetCore.Mvc;
using Praksa.Common;
using Praksa.Service.Common;

namespace Praksa.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FootballPlayersController : ControllerBase
    {
        private readonly IFootballPlayerService _service;
        public FootballPlayersController(IFootballPlayerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlayersAsync([FromQuery] FootballPlayerFilter filter)
        { 
            var players = await _service.GetAllPlayersAsync(filter);
            return Ok(players);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayerByIdAsync(int id)
        {
            var player = await _service.GetPlayerByIdAsync(id);

            if (player == null)
                return NotFound("Player not found.");

            return Ok(player);
        }

        [HttpGet("byClub/{clubId}")]
        public async Task<IActionResult> GetPlayersByClubIdAsync(int clubId)
        {
            var players = await _service.GetPlayersByClubIdAsync(clubId);
            return Ok(players);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayerAsync([FromBody] FootballPlayer player)
        {
            if (player == null)
                return BadRequest("Player data is required.");

            var createdPlayer = await _service.AddPlayerAsync(player);

            if (createdPlayer == null)
                return BadRequest("ClubId does not exist.");

            return Ok(createdPlayer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayerAsync(int id, [FromBody] FootballPlayer player)
        {
            if (player == null)
                return BadRequest("Updated player data is required.");

            var updatedPlayer = await _service.UpdatePlayerAsync(id, player);

            if (updatedPlayer == null)
                return BadRequest("Player not found or ClubId does not exist.");

            return Ok(updatedPlayer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayerAsync(int id)
        {
            var deleted = await _service.DeletePlayerAsync(id);

            if (!deleted)
                return NotFound("Player not found.");

            return Ok("Player deleted successfully.");
        }
    }
}