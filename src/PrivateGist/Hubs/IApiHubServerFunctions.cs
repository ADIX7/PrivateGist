using System.Threading.Tasks;

namespace PrivateGist.Hubs
{
    public interface IApiHubServerFunctions
    {
        Task GetGistsByUserAsync(string userName);
    }
}