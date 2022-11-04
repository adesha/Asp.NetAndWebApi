using AspWebApi.net.Models.Domain;

namespace AspWebApi.net.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
