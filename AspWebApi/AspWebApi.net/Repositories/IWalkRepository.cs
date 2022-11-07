using AspWebApi.net.Models.Domain;

namespace AspWebApi.net.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GelAllAsync();
        Task<Walk> GetWalkAsync(Guid id);
        Task<Walk> AddAsync(Walk walk);
        Task<Walk> UpdateAsync(Guid id,Walk walk);
        Task<Walk> DeleteAsync(Guid id);
    }
}
