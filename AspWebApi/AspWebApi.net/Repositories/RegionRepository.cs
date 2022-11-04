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

        public async Task<Region> GetAsync(Guid id)
        {
            return await webApiDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id=Guid.NewGuid();
            await webApiDbContext.AddAsync(region);
            await webApiDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await webApiDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return null;
            }
            //delete the region
            webApiDbContext.Regions.Remove(region);
            await webApiDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingregion=await webApiDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(region == null)
            {
                return null;
            }

            existingregion.Code = region.Code;
            existingregion.Name = region.Name;
            existingregion.Area = region.Area;
            existingregion.Lat = region.Lat;
            existingregion.Long = region.Long;
            existingregion.Population=region.Population;

            await webApiDbContext.SaveChangesAsync();
            return existingregion;
        }
    }
}
