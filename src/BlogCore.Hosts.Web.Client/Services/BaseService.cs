using Microsoft.JSInterop;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlogCore.Hosts.Web.Client.Services
{
    public abstract class BaseService
    {
        protected BaseService(HttpClient httpClient, IJSRuntime js, AppState appState)
        {
            HttpClient = httpClient;
            JS = js;
            AppState = appState;
        }

        protected HttpClient HttpClient { get; }
        protected IJSRuntime JS { get; }
        protected AppState AppState { get; }

        protected virtual async Task SetHeader()
        {
            AppState.SetUserInfo(await JS.GetUserInfoAsync());
            HttpClient.DefaultRequestHeaders.Remove("Authorization");
            HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {AppState.GetAccessToken()}");
        }
    }
}
