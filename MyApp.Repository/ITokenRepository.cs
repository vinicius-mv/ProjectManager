using System.Threading.Tasks;

namespace MyApp.Repository
{
    public interface ITokenRepository
    {
        Task<string> GetTokenAsync();
        Task SetTokenAsync(string token);
    }
}