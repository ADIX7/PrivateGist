namespace PrivateGist.Models
{
    public interface IGlobalSettings
    {
        string RepoPath { get; }
        string RootWebUrl { get; }
    }
}