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
        public DbSet<Writing> Writing {get;set;}
        public DbSet<ComicDetails> ComicDetails { get; set; }
        public DbSet<ComicChapter> ComicChapters { get; set; }
        public DbSet<WritingChapter> WritingChapter {get;set;}
        public DbSet<WritingChapterContent> WritingChapterContent {get;set;}
        public DbSet<WritingChapterContentBlock> WritingChapterContentBlock {get;set;}
        public DbSet<Comment> Comment {get;set;}
    }
}