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
        public List<FootballClub> GetAllClubs()
        {
            return clubs;
        }

        [HttpGet("{id}")]
        public FootballClub GetClubById(int id)
        {
            return clubs.FirstOrDefault(c => c.Id == id);
        }

        [HttpPost]
        public string AddClub(FootballClub newClub)
        {
            clubs.Add(newClub);
            return "Club added successfully.";
        }

        [HttpPut("{id}")]
        public string UpdateClub(int id, FootballClub updatedClub)
        {
            var club = clubs.FirstOrDefault(c => c.Id == id);

            if (club == null)
            {
                return "Club not found.";
            }

            club.Name = updatedClub.Name;
            club.Country = updatedClub.Country;
            club.FoundedYear = updatedClub.FoundedYear;

            return "Club updated successfully.";
        }

        [HttpDelete("{id}")]
        public string DeleteClub(int id)
        {
            var club = clubs.FirstOrDefault(c => c.Id == id);

            if (club == null)
            {
                return "Club not found.";
            }

            clubs.Remove(club);
            return "Club deleted successfully.";
        }
    }
}