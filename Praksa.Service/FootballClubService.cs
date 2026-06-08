using Praksa.Common;
using Praksa.Repository;

namespace Praksa.Service
{
    public class FootballClubService
    {
        public async Task<List<FootballClub>> GetAllClubsAsync(FootballClubFilter filter)
        {
            var repo = new FootballClubRepository();
            return await repo.GetAllAsync(filter);
        }

        public async Task<FootballClub?> GetClubByIdAsync(int id)
        {
            var repo = new FootballClubRepository();
            return await repo.GetByIdAsync(id);
        }

        public async Task<FootballClub> AddClubAsync(FootballClub club)
        {
            var repo = new FootballClubRepository();
            club.Id = await repo.GetNextIdAsync();
            return await repo.InsertAsync(club);
        }

        public async Task<FootballClub?> UpdateClubAsync(int id, FootballClub club)
        {
            var repo = new FootballClubRepository();
            return await repo.UpdateAsync(id, club);
        }

        public async Task<bool> DeleteClubAsync(int id)
        {
            var repo = new FootballClubRepository();
            return await repo.DeleteAsync(id);
        }
    }
}