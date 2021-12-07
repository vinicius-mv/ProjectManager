using MyApp.Repository.ApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IWebApiExecuter webApiExecuter;

        private readonly ITokenRepository tokenRepository;

        public AuthenticationRepository(IWebApiExecuter webApiExecuter, ITokenRepository tokenRepository)
        {
            this.tokenRepository = tokenRepository;
            this.webApiExecuter = webApiExecuter;
        }
        public async Task<string> LoginAsync(string userName, string password)
        {
            var token = await this.webApiExecuter.InvokePostReturnStringAsync("authenticate", new { UserName = userName, Password = password });
            await tokenRepository.SetTokenAsync(token);

            if(string.IsNullOrWhiteSpace(token) || token == "\"\"") 
                return null;

            return token;
        }

        public async Task<string> GetUserInfoAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return string.Empty;

            var username =  await this.webApiExecuter.InvokePostReturnStringAsync("getuserinfo", new { Token = token });
            return username;
        }

    }
}
