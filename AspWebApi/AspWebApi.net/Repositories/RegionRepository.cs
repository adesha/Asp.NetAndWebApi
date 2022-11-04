using AspWebApi.net.Data;
using AspWebApi.net.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AspWebApi.net.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly WebApiDbContext webApiDbContext;
        public RegionRepository(WebApiDbContext webApiDbContext)
        {
            this.webApiDbContext=webApiDbContext;
        }
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await webApiDbContext.Regions.ToListAsync();
        }
    }
}
