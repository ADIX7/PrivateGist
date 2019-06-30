using System.Buffers;
using System.Text;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrivateGist.Models;
using PrivateGist.Models.Git;
using System.Text.Json;
using PrivateGist.Services;
using PrivateGist.Extensions;
using LibGit2Sharp;

namespace PrivateGist.Controllers.Api
{
    [Route("gists/")]
    [ApiController]
    public class GistsApiController : ControllerBase
    {
        private readonly IGlobalSettings _settings;
        private readonly IRepositoryService _repositoryService;
        public GistsApiController(IGlobalSettings settings, IRepositoryService repositoryService = null)
        {
            _settings = settings;
            _repositoryService = repositoryService;
        }

        [HttpPatch("{repoId}")]
        public async Task UploadGist(string repoId, [FromBody] RepositoryPatch data)
        {
            var repo = _repositoryService.GetRepositoryById(repoId);
            var mainBranch = repo.Branches.GetMainBranch();

            TreeDefinition td = new TreeDefinition();

            foreach (var previousElement in mainBranch.Tip.Tree)
            {
                var name = previousElement.Name;

                if (!data.Files.Keys.Contains(name))
                    td.Add(name, (Blob)previousElement.Target, previousElement.Mode);
            }

            foreach (var newFile in data.Files)
            {
                var newFileData = newFile.Value;
                if (newFileData.Filename == null)
                {
                    var newFileContent = Encoding.UTF8.GetBytes(newFileData.Content);

                    using var ms = new MemoryStream(newFileContent);
                    var newBlob = repo.ObjectDatabase.CreateBlob(ms);

                    td.Add(newFile.Key, newBlob, Mode.NonExecutableFile);
                }
                //TODO else: file moved
            }

            var tree = repo.ObjectDatabase.CreateTree(td);

            // Committer and author
            Signature committer = new Signature("James", "@jugglingnutcase", DateTime.Now);
            Signature author = committer;

            // Create binary stream from the text
            Commit commit = repo.ObjectDatabase.CreateCommit(
                author,
                committer,
                "",
                tree,
                new[] { mainBranch.Commits.First() },
                false);

            // Update the HEAD reference to point to the latest commit
            repo.Refs.UpdateTarget(repo.Refs.Head, commit.Id);
            repo.Refs.UpdateTarget(mainBranch.Reference, commit.Id);
        }
    }
}