using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BlogCore.Hosts.Web.Client.Services
{
    public static class JSRuntimeExtensions
    {
        public async static Task SignInAsync(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeAsync<bool>("users.startSigninMainWindow");
        }

        public async static Task SignOutAsync(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeAsync<bool>("users.startSignoutMainWindow");
        }

        public async static Task<UserInfoModel> CallbackAsync(this IJSRuntime jsRuntime)
        {
            return await jsRuntime.InvokeAsync<UserInfoModel>("users.endSigninMainWindow");
        }

        public async static Task<UserInfoModel> GetUserInfoAsync(this IJSRuntime jsRuntime)
        {
            return await jsRuntime.InvokeAsync<UserInfoModel>("users.getUserInfo");
        }

        public async static Task LogAsync(this IJSRuntime jsRuntime, object output)
        {
            await jsRuntime.InvokeAsync<bool>("users.log", JsonConvert.SerializeObject(output));
        }

        public async static Task BindBlogDataTableAsync(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeAsync<bool>("blogs.bindBlogDataTable");
        }

        public async static Task UnbindBlogDataTableAsync(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeAsync<bool>("blogs.unbindBlogDataTable");
        }
    }
}
