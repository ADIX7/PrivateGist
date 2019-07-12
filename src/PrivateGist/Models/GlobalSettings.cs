namespace PrivateGist.Models
{
    public class GlobalSettings : IGlobalSettings
    {
        public string RepoPath => "gistRepos/";
        public string RootWebUrl => "http://localhost:5000/";
    }
}