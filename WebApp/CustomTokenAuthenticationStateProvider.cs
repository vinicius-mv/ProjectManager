using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp
{
    public class CustomTokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userName = "Frank";
            if (string.IsNullOrWhiteSpace(userName))
            {
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))); // empty claims principal
            }

            // ClaimsPrincipal -< ClaimsIdentity -< Claims
            var claim = new Claim(ClaimTypes.Name, userName);
            var identity = new ClaimsIdentity(new[] { claim }, "Custom Token Auth");
            var principal = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(principal));
        }
    }
}
