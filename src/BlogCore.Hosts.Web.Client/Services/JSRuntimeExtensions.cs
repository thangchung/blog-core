using Microsoft.JSInterop;
using Newtonsoft.Json;
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

        public async static Task<UserInfoModel> CallbackAsync(this IJSRuntime jsRuntime)
        {
            return await jsRuntime.InvokeAsync<UserInfoModel>("endSigninMainWindow");
        }

        public async static Task<UserInfoModel> GetUserInfoAsync(this IJSRuntime jsRuntime)
        {
            return await jsRuntime.InvokeAsync<UserInfoModel>("getUserInfo");
        }

        public async static Task LogAsync(this IJSRuntime jsRuntime, object output)
        {
            await jsRuntime.InvokeAsync<bool>("log", JsonConvert.SerializeObject(output));
        }

        public async static Task BindBlogDataTableAsync(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeAsync<bool>("bindBlogDataTable");
        }
    }
}
