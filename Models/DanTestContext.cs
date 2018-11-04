using DanTest.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DanTest.Models
{

    public class DanTestContext : DbContext
    {
        public DanTestContext(DbContextOptions<DanTestContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite("Data Source=danTest.db");
            base.OnConfiguring(builder);
        }

        public DbSet<Repo> Repositories { get; set; }
        public DbSet<RepoSearchRequest> SearchRequests { get; set; }
    }
}
