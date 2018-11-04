using System;

namespace DanTest.Models.ViewModels
{
    public class RepoViewModel
    {
        public long Id { get; set; }
        public long GitHubId { get; set; }
        public long SearchRequestId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int StarCount { get; set; }
        public int ForkCount { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImg { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
