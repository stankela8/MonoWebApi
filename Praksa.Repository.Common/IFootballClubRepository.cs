using Praksa.Common;

namespace Praksa.Repository.Common
{
    public interface IFootballClubRepository
    {
        Task<List<FootballClub>> GetAllAsync(FootballClubFilter filter);
        Task<FootballClub?> GetByIdAsync(int id);
        Task<int> GetNextIdAsync();
        Task<FootballClub> InsertAsync(FootballClub club);
        Task<FootballClub?> UpdateAsync(int id, FootballClub club);
        Task<bool> DeleteAsync(int id);
    }
}