using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LibGit2Sharp;
using Microsoft.AspNetCore.Http;
using PrivateGist.Models;
using PrivateGist.Services;

namespace PrivateGist.Middleware
{
    public class RawFileMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRepositoryService _repositoryService;

        public RawFileMiddleware(RequestDelegate next, IRepositoryService repositoryService)
        {
            _next = next;
            _repositoryService = repositoryService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // http://localhost:5000/Repo1/raw/1.txt
            var rawFileRegex = new Regex("^/(.*)/raw/(.*)$");
            var rawFileMatch = rawFileRegex.Match(context.Request.Path);
            if (rawFileMatch.Success)
            {
                var repoId = rawFileMatch.Groups[1];
                var fileId = rawFileMatch.Groups[2];

                // context.Request.Path= $"/gistRepos/ADIX7/{repoId}/{fileId}";
                var repo = _repositoryService.GetRepositoryById(repoId.Value);
                var t = repo.Lookup<Blob>(fileId.Value).GetContentText();
                var content = Encoding.UTF8.GetBytes(t);

                var provider = new InMemoryFileProvider(new InMemoryFileProvider.MemoryFileInfo("data", content));

                await context.Response.SendFileAsync(provider.GetFileInfo("data")).ConfigureAwait(false);
                return;
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context).ConfigureAwait(false);
        }
    }
}