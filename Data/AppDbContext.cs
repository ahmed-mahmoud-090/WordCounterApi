using Microsoft.EntityFrameworkCore;
using WordCounterApi.Models;

namespace WordCounterApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TextAnalysis> TextAnalyses { get; set; }
    }
}
