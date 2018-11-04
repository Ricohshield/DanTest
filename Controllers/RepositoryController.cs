using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DanTest.Models.DomainModels;
using DanTest.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using DanTest.Providers;
using Octokit;

namespace DanTest.Controllers
{
    [Route("api/[controller]")]
    public class RepositoryController : Controller
    {

        private readonly IGitHubProvider _gitHubProvider;
        private readonly IMapper _mapper;
        private readonly IRepoProvider _repoProvider;

        public RepositoryController(IGitHubProvider gitHubProvider, IMapper mapper,
            IRepoProvider repoProvider)
        {
            _gitHubProvider = gitHubProvider;
            _mapper = mapper;
            _repoProvider = repoProvider;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LoadRepositories([FromBody]RepoSearchRequestViewModel repoRequest)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repoRequest.Id = 0; 
            repoRequest.TimeStamp = DateTime.Now;

            var gitHubReps = await _gitHubProvider.GetRepositories(repoRequest);
            var reps = _mapper.Map<List<Repo>>(gitHubReps);

            var request = _mapper.Map<RepoSearchRequest>(repoRequest);
            if (!reps.Any())
            {
                await _repoProvider.InsertRequest(request);
            }
            foreach (var rep in reps)
            {
                rep.SearchRequest = request;
            }
            await _repoProvider.InsertRepositories(reps);
            
            return Ok(repoRequest);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRepositories()
        {
            
            var repos = await _repoProvider.GetRepositories();
            var reposVm = _mapper.Map<List<RepoViewModel>>(repos);
            return Ok(reposVm);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetLastRequest()
        {

            var req = await _repoProvider.GetLastRequest();
            var reqVm = _mapper.Map<RepoSearchRequestViewModel>(req);
            return Ok(reqVm);
        }

        [HttpGet("[action]")]
        public IActionResult GetLanguages()
        {
            var langs = Enum.GetValues(typeof(Language))
                .Cast<Language>()
                .Select(t => new {Id = (int)t, Title = t.ToString()});
            
            return Ok(langs);
        }
    }
}
