using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Octokit;

namespace DanTest.Models.DomainModels
{
    public class RepoSearchRequest
    {
        public RepoSearchRequest()
        {
            Repositories = new List<Repo>();
        }
        [Key]
        public long Id { get; set; }
        public string Term { get; set; }
        public int? ForkFrom { get; set; }
        public int? ForkTo { get; set; }
        public Language Language { get; set; }
        public int? StarsFrom { get; set; }
        public int? StarsTo { get; set; }
        public int? SizeFrom { get; set; }
        public int? SizeTo { get; set; }
        public bool IsArchived { get; set; }
        public DateTime TimeStamp { get; set; }
        public ICollection<Repo> Repositories{ get; set; }
    }
}
