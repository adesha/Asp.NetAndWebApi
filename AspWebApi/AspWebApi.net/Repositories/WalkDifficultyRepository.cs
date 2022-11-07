using AspWebApi.net.Data;
using AspWebApi.net.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspWebApi.net.Repositories
{
    public class WalkDifficultyRepository:IWalkDifficultyRepository
    {
        private readonly WebApiDbContext webApiDbContext;
        public WalkDifficultyRepository(WebApiDbContext webApiDbContext)
        {
            this.webApiDbContext = webApiDbContext;
        }


        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await webApiDbContext.WalkDifficulties.ToListAsync();
        }

        public async Task<WalkDifficulty> GetWalkDifficultyByIdAsync(Guid id)
        {
            var walkd=await webApiDbContext.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);
            if (walkd == null)
            {
                return null;
            }
            return walkd;
        }
        public async Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty newWalkDifficulty)
        {
            newWalkDifficulty.Id = Guid.NewGuid();
            await webApiDbContext.WalkDifficulties.AddAsync(newWalkDifficulty);
            await webApiDbContext.SaveChangesAsync();
            return newWalkDifficulty;
        }

        public async Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var walkd = await webApiDbContext.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);
            if(walkd != null)
            {
                walkd.Code = walkDifficulty.Code;
                await webApiDbContext.SaveChangesAsync();
                return walkd;
            }
            return null;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var walkd = await webApiDbContext.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);
            if (walkd != null)
            {
                webApiDbContext.WalkDifficulties.Remove(walkd);
                await webApiDbContext.SaveChangesAsync();
                return walkd;
            }
            return null;
        }
    }
}
