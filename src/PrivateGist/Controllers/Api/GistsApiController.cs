using System;
using System.IO;
using System.Linq;
using System.Text;
using LibGit2Sharp;
using Microsoft.AspNetCore.Mvc;
using PrivateGist.Extensions;
using PrivateGist.Models;
using PrivateGist.Services;

namespace PrivateGist.Controllers.Api
{
    [Route("gists/")]
    [ApiController]
    public class GistsApiController : ControllerBase
    {
        private readonly IRepositoryService _repositoryService;

        public GistsApiController(IRepositoryService repositoryService = null) => _repositoryService = repositoryService;

        [HttpPatch("{repoId}")]
        public void UploadGist(string repoId, [FromBody] RepositoryPatch data)
        {
            var repo = _repositoryService.GetRepositoryById(repoId);
            var mainBranch = repo.Branches.GetMainBranch();

            var td = new TreeDefinition();

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

                // TODO else: file moved
            }

            var tree = repo.ObjectDatabase.CreateTree(td);

            // Committer and author
            var committer = new Signature("James", "@jugglingnutcase", DateTime.Now);
            var author = committer;

            // Create binary stream from the text
            var commit = repo.ObjectDatabase.CreateCommit(
                author,
                committer,
                string.Empty,
                tree,
                new[] { mainBranch.Commits.First() },
                false);

            // Update the HEAD reference to point to the latest commit
            repo.Refs.UpdateTarget(repo.Refs.Head, commit.Id);
            repo.Refs.UpdateTarget(mainBranch.Reference, commit.Id);
        }
    }
}