using System.IO;
using LibGit2Sharp;
using PrivateGist.Models;

namespace PrivateGist.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly IGlobalSettings _settings;

        public RepositoryService(IGlobalSettings settings) => _settings = settings;

        public Repository GetRepositoryById(string repoId) => new Repository(Path.Combine(_settings.RepoPath, repoId + ".git"));

        public string[] GetUserRepositories(string username) => new[] { "TestRepo1", "TestRepo2" };
    }
}