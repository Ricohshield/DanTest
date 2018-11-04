using AutoMapper;
using DanTest.Models.DomainModels;
using DanTest.Models.ViewModels;
using Octokit;

namespace DanTest.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<Repository, Repo>()
                .ForMember(ef => ef.Title, m => m.MapFrom(git => git.FullName))
                .ForMember(ef => ef.Url, m => m.MapFrom(git => git.HtmlUrl))
                .ForMember(ef => ef.GitHubId, m => m.MapFrom(git => git.Id))
                .ForMember(ef => ef.Id, m => m.Ignore())
                .ForMember(ef => ef.StarCount, m => m.MapFrom(git => git.StargazersCount))
                .ForMember(ef => ef.ForkCount, m => m.MapFrom(git => git.ForksCount))
                .ForMember(ef => ef.AuthorName, m => m.MapFrom(git => git.Owner.Login))
                .ForMember(ef => ef.AuthorImg, m => m.MapFrom(git => git.Owner.AvatarUrl))
                .ForMember(ef => ef.LastUpdate, m => m.MapFrom(git => git.UpdatedAt));

            CreateMap<Repo, RepoViewModel>();
            CreateMap<RepoSearchRequest, RepoSearchRequestViewModel>();
            CreateMap<RepoSearchRequestViewModel, RepoSearchRequest>();
        }
    }
}
