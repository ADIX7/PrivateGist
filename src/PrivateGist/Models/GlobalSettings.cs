namespace PrivateGist.Models
{
    public interface IGlobalSettings
    {
        string RepoPath { get; }
        string RootWebUrl { get; }
    }

    public class GlobalSettings : IGlobalSettings
    {
        public string RepoPath => "gistRepos/";
        public string RootWebUrl => "http://localhost:5000/";
    }
}