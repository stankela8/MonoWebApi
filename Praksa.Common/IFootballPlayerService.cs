namespace Praksa.Common
{
    public interface IFootballPlayerService
    {
        Task<List<FootballPlayer>> GetAllPlayersAsync(FootballPlayerFilter filter);
        Task<FootballPlayer?> GetPlayerByIdAsync(int id);
        Task<List<FootballPlayer>> GetPlayersByClubIdAsync(int clubId);
        Task<FootballPlayer?> AddPlayerAsync(FootballPlayer player);
        Task<FootballPlayer?> UpdatePlayerAsync(int id, FootballPlayer player);
        Task<bool> DeletePlayerAsync(int id);
    }
}