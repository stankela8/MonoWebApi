using Praksa.Common;
using Praksa.Repository;

namespace Praksa.Service
{
    public class FootballPlayerService
    {
        public List<FootballPlayer> GetAllPlayers(FootballPlayerFilter filter)
        {
            var repo = new FootballPlayerRepository();
            return repo.GetAll(filter);
        }

        public FootballPlayer? GetPlayerById(int id)
        {
            var repo = new FootballPlayerRepository();
            return repo.GetById(id);
        }

        public List<FootballPlayer> GetPlayersByClubId(int clubId)
        {
            var repo = new FootballPlayerRepository();
            return repo.GetByClubId(clubId);
        }
        public FootballPlayer? AddPlayer(FootballPlayer player)
        {
            var clubRepo = new FootballClubRepository();
            var existingClub = clubRepo.GetById(player.ClubId);

            if (existingClub == null)
            {
                return null;
            }

            var repo = new FootballPlayerRepository();
            player.Id = repo.GetNextId();
            repo.Insert(player);
            return player;
        }

        public bool UpdatePlayer(int id, FootballPlayer player)
        {
            var clubRepo = new FootballClubRepository();
            var existingClub = clubRepo.GetById(player.ClubId);

            if (existingClub == null)
            {
                return false;
            }

            var repo = new FootballPlayerRepository();
            return repo.Update(id, player);
        }

        public bool DeletePlayer(int id)
        {
            var repo = new FootballPlayerRepository();
            return repo.Delete(id);
        }
    }
}