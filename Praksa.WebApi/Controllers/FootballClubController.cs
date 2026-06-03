using Microsoft.AspNetCore.Mvc;

namespace Praksa.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FootballClubsController : ControllerBase
    {
        private static List<FootballClub> clubs = new List<FootballClub>
        {
            new FootballClub
            {
                Id = 1,
                Name = "Real Madrid",
                Country = "Spain",
                FoundedYear = 1902
            },
            new FootballClub
            {
                Id = 2,
                Name = "Manchester City",
                Country = "England",
                FoundedYear = 1880
            },
            new FootballClub
            {
                Id = 3,
                Name = "Barcelona",
                Country = "Spain",
                FoundedYear = 1899
            }
        };

        [HttpGet]
        public IActionResult GetAllClubs(string? country = null, int? foundedAfter = null)
        {
            var result = clubs.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(country))
            {
                result = result.Where(c => c.Country.ToLower() == country.ToLower());
            }

            if (foundedAfter.HasValue)
            {
                result = result.Where(c => c.FoundedYear > foundedAfter.Value);
            }

            return Ok(result.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetClubById(int id)
        {
            var club = clubs.FirstOrDefault(c => c.Id == id);

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

            newClub.Id = clubs.Max(c => c.Id) + 1;
            clubs.Add(newClub);

            return Ok(newClub);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClub(int id, [FromBody] FootballClub updatedClub)
        {
            if (updatedClub == null)
            {
                return BadRequest("Updated club data is required.");
            }

            var club = clubs.FirstOrDefault(c => c.Id == id);

            if (club == null)
            {
                return NotFound("Club not found.");
            }

            club.Name = updatedClub.Name;
            club.Country = updatedClub.Country;
            club.FoundedYear = updatedClub.FoundedYear;

            return Ok(club);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClub(int id)
        {
            var club = clubs.FirstOrDefault(c => c.Id == id);

            if (club == null)
            {
                return NotFound("Club not found.");
            }

            clubs.Remove(club);

            return Ok("Club deleted successfully.");
        }
    }
}