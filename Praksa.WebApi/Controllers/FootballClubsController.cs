using Microsoft.AspNetCore.Mvc;
using Praksa.Common;
using Praksa.Service;

namespace Praksa.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FootballClubsController : ControllerBase
    {
        private readonly IFootballClubService _service;
        public FootballClubsController(IFootballClubService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClubsAsync([FromQuery] FootballClubFilter filter)
        {
           
            var clubs = await _service.GetAllClubsAsync(filter);
            return Ok(clubs);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClubByIdAsync(int id)
        {
           
            var club = await _service.GetClubByIdAsync(id);

            if (club == null)
                return NotFound("Club not found.");

            return Ok(club);
        }

        [HttpPost]
        public async Task<IActionResult> AddClubAsync([FromBody] FootballClub club)
        {
            if (club == null)
                return BadRequest("Club data is required.");

           
            var createdClub = await _service.AddClubAsync(club);
            return Ok(createdClub);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClubAsync(int id, [FromBody] FootballClub club)
        {
            if (club == null)
                return BadRequest("Updated club data is required.");

            
            var updatedClub = await _service.UpdateClubAsync(id, club);

            if (updatedClub == null)
                return NotFound("Club not found.");

            return Ok(updatedClub);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClubAsync(int id)
        { 
            var deleted = await _service.DeleteClubAsync(id);

            if (!deleted)
                return NotFound("Club not found.");

            return Ok("Club deleted successfully.");
        }
    }
}