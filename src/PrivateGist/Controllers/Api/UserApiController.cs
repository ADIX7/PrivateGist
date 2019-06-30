using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrivateGist.Models;
using PrivateGist.Models.Git;
using PrivateGist.Services;
using PrivateGist.Extensions;

namespace PrivateGist.Controllers.Api
{
    [Route("users/")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly IGlobalSettings _settings;
        private readonly IRepositoryService _repositoryService;

        public UserApiController(IGlobalSettings settings, IRepositoryService repositoryService)
        {
            _settings = settings;
            _repositoryService = repositoryService;
        }

        [HttpGet("{username}/gists")]
        public List<Repository> GetUserGists(string username)
        {
            var page = HttpContext.Request.Query["page"];
            try
            {
                var pagei = int.Parse(page);
                if (pagei > 1) return new List<Repository>();
            }
            catch { }
            //var token = HttpContext.Request.Headers["Authorization"];

            var repoIds = _repositoryService.GetUserRepositories(username);
            var repos = new List<Repository>();

            foreach (var repoId in repoIds)
            {
                using (var gitRepo = _repositoryService.GetRepositoryById(repoId))
                {
                    var files = new Dictionary<string, Models.Git.File>();
                    var repo = new Repository(_settings, repoId)
                    {
                        Files = files
                    };

                    var branches = gitRepo.Branches;
                    if (branches.Any())
                    {
                        var mainBranch = branches.GetMainBranch();

                        foreach (var entry in mainBranch.Tip.Tree)
                        {
                            files.Add(entry.Name, new Models.Git.File(_settings, repo, entry.Name, entry.Target.Id.Sha)
                            {
                                Type = "text/plain"
                            });
                        }
                    }

                    repos.Add(repo);
                }
            }

            /* var userRepo = new DirectoryInfo(Path.Combine(_settings.RepoPath, username));

            if (userRepo.Exists)
            {
                foreach (var repoFolder in userRepo.GetDirectories())
                {
                    string repoName = repoFolder.Name;
                    var files = new Dictionary<string, Models.Git.File>();
                    var repo = new Repository(_settings, repoFolder.Name)
                    {
                        Id = repoFolder.Name,
                        Files = files
                    };

                    foreach (var file in repoFolder.GetFiles())
                    {
                        files.Add(file.Name, new Models.Git.File(_settings, repo, file.Name)
                        {
                            Type = "text/plain"
                        });
                    }

                    repos.Add(repo);
                }
            } */

            return repos;
        }
    }
}