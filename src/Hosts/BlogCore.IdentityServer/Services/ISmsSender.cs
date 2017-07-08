using System.Threading.Tasks;

namespace BlogCore.IdentityServer.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
