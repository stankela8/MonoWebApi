using Praksa.Common;

namespace Praksa.Service.Common
{
    public interface IFootballClubService
    {
        Task<List<FootballClub>> GetAllClubsAsync(FootballClubFilter filter);
        Task<FootballClub?> GetClubByIdAsync(int id);
        Task<FootballClub> AddClubAsync(FootballClub club);
        Task<FootballClub?> UpdateClubAsync(int id, FootballClub club);
        Task<bool> DeleteClubAsync(int id);
    }
}