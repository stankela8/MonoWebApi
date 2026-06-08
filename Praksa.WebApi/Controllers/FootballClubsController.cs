using Microsoft.AspNetCore.Mvc;
using Praksa.Common;
using Praksa.Service;

namespace Praksa.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FootballClubsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllClubsAsync([FromQuery] FootballClubFilter filter)
        {
            var service = new FootballClubService();
            var clubs = await service.GetAllClubsAsync(filter);
            return Ok(clubs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClubByIdAsync(int id)
        {
            var service = new FootballClubService();
            var club = await service.GetClubByIdAsync(id);

            if (club == null)
                return NotFound("Club not found.");

            return Ok(club);
        }

        [HttpPost]
        public async Task<IActionResult> AddClubAsync([FromBody] FootballClub club)
        {
            if (club == null)
                return BadRequest("Club data is required.");

            var service = new FootballClubService();
            var createdClub = await service.AddClubAsync(club);
            return Ok(createdClub);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClubAsync(int id, [FromBody] FootballClub club)
        {
            if (club == null)
                return BadRequest("Updated club data is required.");

            var service = new FootballClubService();
            var updatedClub = await service.UpdateClubAsync(id, club);

            if (updatedClub == null)
                return NotFound("Club not found.");

            return Ok(updatedClub);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClubAsync(int id)
        {
            var service = new FootballClubService();
            var deleted = await service.DeleteClubAsync(id);

            if (!deleted)
                return NotFound("Club not found.");

            return Ok("Club deleted successfully.");
        }
    }
}