using System.IO;
using System.Text.RegularExpressions;
using LibGit2Sharp;
using PrivateGist.Models;

namespace PrivateGist.Services
{
    public interface IRepositoryService
    {
        string[] GetUserRepositories(string username);
        Repository GetRepositoryById(string repoId);
    }

    public class RepositoryService : IRepositoryService
    {
        private readonly IGlobalSettings _settings;

        public RepositoryService(IGlobalSettings settings)
        {
            _settings = settings;
        }

        public Repository GetRepositoryById(string repoId)
        {
            return new Repository(Path.Combine(_settings.RepoPath, repoId + ".git"));
        }

        public string[] GetUserRepositories(string username)
        {
            return new[] { "TestRepo1", "TestRepo2" };
        }
    }
}