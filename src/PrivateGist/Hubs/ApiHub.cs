using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PrivateGist.Services;

namespace PrivateGist.Hubs
{
    public class ApiHub : Hub, IApiHubServerFunctions
    {
        private readonly IRepositoryService _repositoryService;

        public ApiHub(IRepositoryService repositoryService) => _repositoryService = repositoryService;

        public async Task GetGistsByUserAsync(string userName)
        {
            var data = _repositoryService.GetUserRepositories(userName);
            var f = nameof(GetGistsByUserAsync).Substring(3);
            f = f[0..^5];
            await Clients.Client(Context.ConnectionId).SendAsync(f, data);
        }
    }
}