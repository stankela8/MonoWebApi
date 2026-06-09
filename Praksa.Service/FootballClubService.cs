using Praksa.Common;


namespace Praksa.Service
{
    public class FootballClubService : IFootballClubService
    {
        private readonly IFootballClubRepository _repo;
        public FootballClubService(IFootballClubRepository repo)
        {
            _repo = repo;
        }
        public async Task<List<FootballClub>> GetAllClubsAsync(FootballClubFilter filter)
        {
            return await _repo.GetAllAsync(filter);
        }

        public async Task<FootballClub?> GetClubByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<FootballClub> AddClubAsync(FootballClub club)
        {
            
            club.Id = await _repo.GetNextIdAsync();
            return await _repo.InsertAsync(club);
        }

        public async Task<FootballClub?> UpdateClubAsync(int id, FootballClub club)
        {
            return await _repo.UpdateAsync(id, club);
        }

        public async Task<bool> DeleteClubAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}