using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DanTest.Models;
using DanTest.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DanTest.Providers
{
    class RepoProvider : IRepoProvider
    {
        private readonly DanTestContext _context;
        public RepoProvider()
        {
            _context = new DanTestContext(new DbContextOptions<DanTestContext>());
        }
        public async Task InsertRepositories(IEnumerable<Repo> repositories)
        {
            await _context.Repositories.AddRangeAsync(repositories);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Repo>> GetRepositories()
        {
            var lastReqId =(await _context.SearchRequests.LastOrDefaultAsync())?.Id ?? 0;
            var repos =  await _context.Repositories.Where(w => w.SearchRequestId == lastReqId).ToListAsync();
            return repos;
        }

        public async Task<RepoSearchRequest> GetLastRequest()
        {
            var lastReq = await _context.SearchRequests.LastOrDefaultAsync();
            return lastReq;
        }

        public async Task InsertRequest(RepoSearchRequest request)
        {
            await _context.SearchRequests.AddAsync(request);
            await _context.SaveChangesAsync();
        }
    }
}
