using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlogCore.Hosts.Web.Client.Services
{
    public static class JSRuntimeExtensions
    {
        public async static Task SignInAsync(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeAsync<bool>("startSigninMainWindow");
        }

        public async static Task SignOutAsync(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeAsync<bool>("startSignoutMainWindow");
        }

        public async static Task<UserModel> CallbackAsync(this IJSRuntime jsRuntime)
        {
            return await jsRuntime.InvokeAsync<UserModel>("endSigninMainWindow");
        }

        public async static Task<UserModel> GetUserInfoAsync(this IJSRuntime jsRuntime)
        {
            return await jsRuntime.InvokeAsync<UserModel>("getUserInfo");
        }

        public async static Task LogAsync(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeAsync<bool>("log", message);
        }
    }
}
