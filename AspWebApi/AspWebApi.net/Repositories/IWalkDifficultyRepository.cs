using AspWebApi.net.Models.Domain;

namespace AspWebApi.net.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllAsync();

        Task<WalkDifficulty> GetWalkDifficultyByIdAsync(Guid id);
        Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty newWalkDifficulty);

        Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id,WalkDifficulty walkDifficulty);

        Task<WalkDifficulty> DeleteAsync(Guid id);
    }
}
