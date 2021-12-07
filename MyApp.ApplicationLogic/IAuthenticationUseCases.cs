using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public interface IAuthenticationUseCases
    {
        Task<string> GetUserInfoAsync(string token);
        Task<string> LoginAsync(string username, string password);
        Task LogoutAsync();
    }
}