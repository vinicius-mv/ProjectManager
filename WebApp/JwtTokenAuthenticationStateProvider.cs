using Microsoft.AspNetCore.Components.Authorization;
using MyApp.Repository;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp
{
    public class JwtTokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ITokenRepository tokenRepository;
        private readonly IAuthenticationRepository authenticationRepository;

        public JwtTokenAuthenticationStateProvider(ITokenRepository tokenRepository, IAuthenticationRepository authenticationRepository)
        {
            this.tokenRepository = tokenRepository;
            this.authenticationRepository = authenticationRepository;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            var tokenString = await tokenRepository.GetTokenAsync();

            JwtSecurityToken tokenJwt = null;

            if (!string.IsNullOrWhiteSpace(tokenString))
            {
                // Extract Claim from the token (not secure), don't put sensitive data in tokens
                tokenJwt = tokenHandler.ReadJwtToken(tokenString.Replace("\"", string.Empty));
            }

            if (tokenJwt != null)
            {
                var claims = new List<Claim>();
                claims.AddRange(tokenJwt.Claims);

                var nameClaim = tokenJwt.Claims.FirstOrDefault(c => c.Type == "unique_name");
                var roleClaim = tokenJwt.Claims.FirstOrDefault(c => c.Type == "role");

                if (nameClaim != null)
                    claims.Add(new Claim(ClaimTypes.Name, nameClaim.Value));

                if (roleClaim != null)
                    claims.Add(new Claim(ClaimTypes.Role, roleClaim.Value));


                var identity = new ClaimsIdentity(claims, "Custom JWT Token Auth");
                var principal = new ClaimsPrincipal(identity);

                return new AuthenticationState(principal);
            }

            return new AuthenticationState(new ClaimsPrincipal());
        }
    }
}
