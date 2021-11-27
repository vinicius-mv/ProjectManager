using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers.Auth
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICustomTokenManager customTokenManager;
        private readonly ICustomUserManager customUserManager;

        public AuthController(ICustomUserManager customUserManager, ICustomTokenManager customTokenManager)
        {
            this.customUserManager = customUserManager;
            this.customTokenManager = customTokenManager;
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            return await Task.FromResult(customUserManager.Authenticate(username, password));
        }

        public async Task<bool> VerifyAsync(string token)
        {
            return await Task.FromResult(customTokenManager.VerifyToken(token));
        }

        public async Task<string> GetUserInfoByTokenAsync(string token)
        {
            return await Task.FromResult(customTokenManager.GetUserInfoByToken(token));
        }
    }
}
