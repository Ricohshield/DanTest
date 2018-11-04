using System.Collections.Generic;
using System.Threading.Tasks;
using DanTest.Models.DomainModels;

namespace DanTest.Providers
{
    public interface IRepoProvider
    {
        Task InsertRepositories(IEnumerable<Repo> repositories);
        Task<IEnumerable<Repo>> GetRepositories();
        Task<RepoSearchRequest> GetLastRequest();
        Task InsertRequest(RepoSearchRequest request);
    }
}
