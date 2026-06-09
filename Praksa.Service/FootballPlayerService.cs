using Praksa.Common;
using Praksa.Service.Common;
using Praksa.Repository.Common;

namespace Praksa.Service
{
    public class FootballPlayerService : IFootballPlayerService
    {
        private readonly IFootballClubRepository _clubRepo;
        private readonly IFootballPlayerRepository _playerRepo;
        public FootballPlayerService(IFootballClubRepository clubRepo, IFootballPlayerRepository playerRepo)
        {
            _clubRepo = clubRepo;
            _playerRepo = playerRepo;
        }

        public async Task<List<FootballPlayer>> GetAllPlayersAsync(FootballPlayerFilter filter)
        {
            return await _playerRepo.GetAllAsync(filter);
        }

        public async Task<FootballPlayer?> GetPlayerByIdAsync(int id)
        {
            return await _playerRepo.GetByIdAsync(id);
        }

        public async Task<List<FootballPlayer>> GetPlayersByClubIdAsync(int clubId)
        {
            return await _playerRepo.GetByClubIdAsync(clubId);
        }

        public async Task<FootballPlayer?> AddPlayerAsync(FootballPlayer player)
        {
            var existingClub = await _clubRepo.GetByIdAsync(player.ClubId);

            if (existingClub == null)
                return null;

            player.Id = await _playerRepo.GetNextIdAsync();
            return await _playerRepo.InsertAsync(player);
        }

        public async Task<FootballPlayer?> UpdatePlayerAsync(int id, FootballPlayer player)
        {
            var existingPlayer = await _playerRepo.GetByIdAsync(id);

            if (existingPlayer == null)
                return null;

            var existingClub = await _clubRepo.GetByIdAsync(player.ClubId);

            if (existingClub == null)
                return null;

            return await _playerRepo.UpdateAsync(id, player);
        }

        public async Task<bool> DeletePlayerAsync(int id)
        {
            return await _playerRepo.DeleteAsync(id);
        }
    }
}