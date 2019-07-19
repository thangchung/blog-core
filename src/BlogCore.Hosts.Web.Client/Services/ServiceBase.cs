using Microsoft.JSInterop;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace BlogCore.Hosts.Web.Client.Services
{
    public abstract class ServiceBase
    {
        protected ServiceBase(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        protected IServiceProvider ServiceProvider { get; }
        protected HttpClient HttpClient => ServiceProvider.GetService<HttpClient>();
        protected IJSRuntime JS => ServiceProvider.GetService<IJSRuntime>();
        protected AppState AppState => ServiceProvider.GetService<AppState>();

        protected async Task<HttpClient> SecureHttpClientAsync()
        {
            var httpClient = ServiceProvider.GetService<HttpClient>();
            AppState.SetUserInfo(await JS.GetUserInfoAsync());
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {AppState.GetAccessToken()}");
            return httpClient;
        }
    }
}