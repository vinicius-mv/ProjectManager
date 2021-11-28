using MyApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public class AuthenticationUseCases : IAuthenticationUseCases
    {
        private readonly IAuthenticationRepository authenticationRepository;
        public AuthenticationUseCases(IAuthenticationRepository authenticationRepository)
        {
            this.authenticationRepository = authenticationRepository;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            return await authenticationRepository.LoginAsync(username, password);
        }

        public async Task<string> GetUserInfoAsync(string token)
        {
            return await authenticationRepository.GetUserInfoAsync(token);
        }
    }
}
