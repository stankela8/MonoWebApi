namespace Praksa.Common
{
    public interface IFootballPlayerRepository
    {
        Task<List<FootballPlayer>> GetAllAsync(FootballPlayerFilter filter);
        Task<FootballPlayer?> GetByIdAsync(int id);
        Task<List<FootballPlayer>> GetByClubIdAsync(int clubId);
        Task<int> GetNextIdAsync();
        Task<FootballPlayer> InsertAsync(FootballPlayer player);
        Task<FootballPlayer?> UpdateAsync(int id, FootballPlayer player);
        Task<bool> DeleteAsync(int id);
    }
}