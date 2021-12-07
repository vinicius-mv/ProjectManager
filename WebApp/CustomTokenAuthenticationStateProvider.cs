﻿using Microsoft.AspNetCore.Components.Authorization;
using MyApp.Repository;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp
{
    public class CustomTokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ITokenRepository tokenRepository;
        private readonly IAuthenticationRepository authenticationRepository;

        public CustomTokenAuthenticationStateProvider(ITokenRepository tokenRepository, IAuthenticationRepository authenticationRepository)
        {
            this.tokenRepository = tokenRepository;
            this.authenticationRepository = authenticationRepository;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await tokenRepository.GetTokenAsync();
            string userName = await authenticationRepository.GetUserInfoAsync(token);
            if (!string.IsNullOrWhiteSpace(userName))
            {
                // ClaimsPrincipal -< ClaimsIdentity -< Claims
                var claim = new Claim(ClaimTypes.Name, userName);
                var identity = new ClaimsIdentity(new[] { claim }, "Custom Token Auth");
                var principal = new ClaimsPrincipal(identity);

                return new AuthenticationState(principal);
            }
            else
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }
    }
}
