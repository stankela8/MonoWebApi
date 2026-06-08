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
        public IActionResult GetAllPlayers([FromQuery] FootballPlayerFilter filter)
        {
            var service = new FootballPlayerService();
            var players = service.GetAllPlayers(filter);
            return Ok(players);
        }

        [HttpGet("{id}")]
        public IActionResult GetPlayerById(int id)
        {
            var service = new FootballPlayerService();
            var player = service.GetPlayerById(id);

            if (player == null)
            {
                return NotFound("Player not found.");
            }

            return Ok(player);
        }

        [HttpGet("byClub/{clubId}")]
        public IActionResult GetPlayersByClubId(int clubId)
        {
            var service = new FootballPlayerService();
            var players = service.GetPlayersByClubId(clubId);
            return Ok(players);
        }


        [HttpPost]
        public IActionResult AddPlayer([FromBody] FootballPlayer newPlayer)
        {
            if (newPlayer == null)
            {
                return BadRequest("Player data is required.");
            }

            var service = new FootballPlayerService();
            var createdPlayer = service.AddPlayer(newPlayer);

            if (createdPlayer == null)
            {
                return BadRequest("ClubId does not exist.");
            }

            return Ok(createdPlayer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlayer(int id, [FromBody] FootballPlayer updatedPlayer)
        {
            if (updatedPlayer == null)
            {
                return BadRequest("Updated player data is required.");
            }

            var service = new FootballPlayerService();
            var success = service.UpdatePlayer(id, updatedPlayer);

            if (!success)
            {
                return BadRequest("Player not found or ClubId does not exist.");
            }

            return Ok(updatedPlayer);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlayer(int id)
        {
            var service = new FootballPlayerService();
            var success = service.DeletePlayer(id);

            if (!success)
            {
                return NotFound("Player not found.");
            }

            return Ok("Player deleted successfully.");
        }
    }
}