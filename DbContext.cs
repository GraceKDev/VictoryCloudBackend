using Microsoft.EntityFrameworkCore;
using VictoryCloudApi.Models;
namespace VictoryCloudApi.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Config> Config { get; set; }
        public DbSet<Comic> Comics { get; set; }
        public DbSet<Art> Art { get; set; }
        public DbSet<ComicDetails> ComicDetails { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
    }
}