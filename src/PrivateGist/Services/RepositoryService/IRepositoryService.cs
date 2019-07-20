using System.Collections.Generic;
using LibGit2Sharp;

namespace PrivateGist.Services
{
    public interface IRepositoryService
    {
        List<string> GetUserRepositories(string username);
        Repository GetRepositoryById(string repoId);
    }
}