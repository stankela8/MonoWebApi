using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Praksa.Common;
using Praksa.Service.Common;
using Praksa.WebApi.Models;

namespace Praksa.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FootballClubsController : ControllerBase
    {
        private readonly IFootballClubService _service;
        private readonly IMapper _mapper;
        public FootballClubsController(IFootballClubService service,IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
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
        [HttpPost]
        public async Task<IActionResult> AddClubAsync([FromBody] FootballClubUpsertRequest request)
        {
            if (request == null)
                return BadRequest("Club data is required.");

            var club = _mapper.Map<FootballClub>(request);
            var createdClub = await _service.AddClubAsync(club);
            var response = _mapper.Map<FootballClubResponse>(createdClub);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClubAsync(int id, [FromBody] FootballClubUpsertRequest request)
        {
            if (request == null)
                return BadRequest("Updated club data is required.");

            var club = _mapper.Map<FootballClub>(request);
            var updatedClub = await _service.UpdateClubAsync(id, club);

            if (updatedClub == null)
                return NotFound("Club not found.");

            var response = _mapper.Map<FootballClubResponse>(updatedClub);

            return Ok(response);
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