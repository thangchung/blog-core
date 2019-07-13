using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogCore.Hosts.Web.Client.Services
{
    public class ServerAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _js;
        public ServerAuthenticationStateProvider(IJSRuntime js)
        {
            _js = js;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userInfo = await _js.GetUserInfoAsync();
            await _js.LogAsync(userInfo);

            if (userInfo.AccessToken == null)
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            else
                return new AuthenticationState(
                    new ClaimsPrincipal(
                        new ClaimsIdentity(
                            new[] { new Claim(ClaimTypes.Name, userInfo.Profile.Name) }, "tokenauth")));
        }
    }
}
