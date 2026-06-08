using Praksa.Common;
using Praksa.Repository;

namespace Praksa.Service
{
    public class FootballPlayerService : IFootballPlayerService
    {
        public async Task<List<FootballPlayer>> GetAllPlayersAsync(FootballPlayerFilter filter)
        {
            var repo = new FootballPlayerRepository();
            return await repo.GetAllAsync(filter);
        }

        public async Task<FootballPlayer?> GetPlayerByIdAsync(int id)
        {
            var repo = new FootballPlayerRepository();
            return await repo.GetByIdAsync(id);
        }

        public async Task<List<FootballPlayer>> GetPlayersByClubIdAsync(int clubId)
        {
            var repo = new FootballPlayerRepository();
            return await repo.GetByClubIdAsync(clubId);
        }

        public async Task<FootballPlayer?> AddPlayerAsync(FootballPlayer player)
        {
            var clubRepo = new FootballClubRepository();
            var existingClub = await clubRepo.GetByIdAsync(player.ClubId);

            if (existingClub == null)
                return null;

            var playerRepo = new FootballPlayerRepository();
            player.Id = await playerRepo.GetNextIdAsync();
            return await playerRepo.InsertAsync(player);
        }

        public async Task<FootballPlayer?> UpdatePlayerAsync(int id, FootballPlayer player)
        {
            var playerRepo = new FootballPlayerRepository();
            var existingPlayer = await playerRepo.GetByIdAsync(id);

            if (existingPlayer == null)
                return null;

            var clubRepo = new FootballClubRepository();
            var existingClub = await clubRepo.GetByIdAsync(player.ClubId);

            if (existingClub == null)
                return null;

            return await playerRepo.UpdateAsync(id, player);
        }

        public async Task<bool> DeletePlayerAsync(int id)
        {
            var repo = new FootballPlayerRepository();
            return await repo.DeleteAsync(id);
        }
    }
}