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
    }
}