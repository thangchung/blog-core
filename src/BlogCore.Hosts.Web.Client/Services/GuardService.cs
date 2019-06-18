using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlogCore.Hosts.Web.Client.Services
{
    public class GuardService : BaseService
    {
        public GuardService(HttpClient httpClient, IJSRuntime js, AppState appState) : base(httpClient, js, appState)
        {
        }

        public async Task Check()
        {
            if (!AppState.IsLogin())
            {
                await JS.SignInAsync();
            }
        }
    }
}
