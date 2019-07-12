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
}