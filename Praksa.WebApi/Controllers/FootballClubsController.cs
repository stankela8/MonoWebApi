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
        public IActionResult GetAllClubs([FromQuery] FootballClubFilter filter)
        {
            var service = new FootballClubService();
            var clubs = service.GetAllClubs(filter);
            return Ok(clubs);
        }

        [HttpGet("{id}")]
        public IActionResult GetClubById(int id)
        {
            var service = new FootballClubService();
            var club = service.GetClubById(id);

            if (club == null)
            {
                return NotFound("Club not found.");
            }

            return Ok(club);
        }

        [HttpPost]
        public IActionResult AddClub([FromBody] FootballClub newClub)
        {
            if (newClub == null)
            {
                return BadRequest("Club data is required.");
            }

            var service = new FootballClubService();
            var createdClub = service.AddClub(newClub);

            return Ok(createdClub);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClub(int id, [FromBody] FootballClub updatedClub)
        {
            if (updatedClub == null)
            {
                return BadRequest("Updated club data is required.");
            }

            var service = new FootballClubService();
            var success = service.UpdateClub(id, updatedClub);

            if (!success)
            {
                return NotFound("Club not found.");
            }

            return Ok(updatedClub);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClub(int id)
        {
            var service = new FootballClubService();
            var success = service.DeleteClub(id);

            if (!success)
            {
                return NotFound("Club not found.");
            }

            return Ok("Club deleted successfully.");
        }
    }
}