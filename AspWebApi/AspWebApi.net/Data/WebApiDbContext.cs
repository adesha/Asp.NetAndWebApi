using AspWebApi.net.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AspWebApi.net.Data
{
    public class WebApiDbContext:DbContext 
    {
        public WebApiDbContext(DbContextOptions<WebApiDbContext> options) : base(options)
        {

        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulties { get; set; }
    }
}
