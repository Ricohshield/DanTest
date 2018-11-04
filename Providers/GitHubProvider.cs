using System.Collections.Generic;
using System.Threading.Tasks;
using DanTest.Models.ViewModels;
using Octokit;

namespace DanTest.Providers
{
    public class GitHubProvider : IGitHubProvider
    {

        public async Task<IEnumerable<Repository>> GetRepositories(RepoSearchRequestViewModel repoRequest)
        {
            

            var github = new GitHubClient(new ProductHeaderValue("DanTest"));
            var request = new SearchRepositoriesRequest(repoRequest.Term)
            {
                Language = repoRequest.Language,
                Order = SortDirection.Descending,
                SortField = RepoSearchSort.Updated,
                Archived = repoRequest.IsArchived,
                PerPage = 10
            };
            if ((int)repoRequest.Language != -1)
            {
                request.Language = repoRequest.Language;
            }
            var forksRange = BuildRange(repoRequest.ForkFrom, repoRequest.ForkTo);
            var sizeRange = BuildRange(repoRequest.SizeFrom, repoRequest.SizeTo);
            var starsRange = BuildRange(repoRequest.StarsFrom, repoRequest.StarsTo);

            request.Forks = forksRange;
            request.Size = sizeRange;
            request.Stars = starsRange;
            
            var repos = await github.Search.SearchRepo(request);
            return repos.Items;
        }

        private Range BuildRange(int? from, int? to)
        {
            if (to != null && from != null)
            {
                return new Range(from.Value, to.Value);
            }

            if (to != null)
            {
                return new Range(to.Value, SearchQualifierOperator.LessThanOrEqualTo);
            }

            if (from != null)
            {
                return new Range(from.Value, SearchQualifierOperator.GreaterThanOrEqualTo);
            }

            return null;
        }

        
    }
}
