using Praksa.Common;
using Praksa.Repository;

namespace Praksa.Service
{
    public class FootballClubService
    {
        public List<FootballClub> GetAllClubs(string? country = null, int? foundedAfter = null)
        {
            var repo = new FootballClubRepository();
            return repo.GetAll(country, foundedAfter);
        }

        public FootballClub? GetClubById(int id)
        {
            var repo = new FootballClubRepository();
            return repo.GetById(id);
        }

        public FootballClub AddClub(FootballClub club)
        {
            var repo = new FootballClubRepository();
            club.Id = repo.GetNextId();
            repo.Insert(club);
            return club;
        }

        public bool UpdateClub(int id, FootballClub club)
        {
            var repo = new FootballClubRepository();
            return repo.Update(id, club);
        }

        public bool DeleteClub(int id)
        {
            var repo = new FootballClubRepository();
            return repo.Delete(id);
        }
    }
}