using DanTest.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DanTest.Models
{

    public class DanTestContext : DbContext
    {
        private IConfiguration _configuration;
        public DanTestContext(DbContextOptions<DanTestContext> options)
            : base(options)
        {
           
        }
        


        public DbSet<Repo> Repositories { get; set; }
        public DbSet<RepoSearchRequest> SearchRequests { get; set; }
    }
}
