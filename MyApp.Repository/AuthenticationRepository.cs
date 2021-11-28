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
            tokenRepository.Token = token;
            return token;
        }

        public async Task<string> GetUserInfoAsync(string token)
        {
            return await this.webApiExecuter.InvokePostReturnStringAsync("getuserinfo", new { Token = token });
        }

    }
}
