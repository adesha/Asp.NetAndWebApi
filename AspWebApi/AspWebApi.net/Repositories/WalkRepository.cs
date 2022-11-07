using AspWebApi.net.Data;
using AspWebApi.net.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AspWebApi.net.Repositories
{
    public class WalkRepository:IWalkRepository
    {
        private readonly WebApiDbContext webApiDbContext;
        public WalkRepository(WebApiDbContext webApiDbContext)
        {
            this.webApiDbContext = webApiDbContext;
        }

        public async Task<IEnumerable<Walk>> GelAllAsync()
        {
            return await 
                webApiDbContext.Walks.Include(x=>x.Region).Include(x=>x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetWalkAsync(Guid id)
        {
            return await webApiDbContext.Walks.Include(x => x.Region).Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await webApiDbContext.Walks.AddAsync(walk);
            await webApiDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingwalk = await webApiDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if(existingwalk != null)
            {
                existingwalk.Name = walk.Name;
                existingwalk.Length = walk.Length;
                existingwalk.Region = walk.Region;
                existingwalk.RegionId = walk.RegionId;
                existingwalk.WalkDifficultyId = walk.WalkDifficultyId;
                await webApiDbContext.SaveChangesAsync();
                return existingwalk;
            }
            return null;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walk = await webApiDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if(walk != null)
            {
                webApiDbContext.Walks.Remove(walk);
                await webApiDbContext.SaveChangesAsync();
                return walk;
            }
            return null;
        }
    }
}
