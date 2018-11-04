using System.Collections.Generic;
using System.Threading.Tasks;
using DanTest.Models.ViewModels;
using Octokit;

namespace DanTest.Providers
{
    public interface IGitHubProvider
    {
        Task<IEnumerable<Repository>> GetRepositories(RepoSearchRequestViewModel repoRequest);
        
    }
}
