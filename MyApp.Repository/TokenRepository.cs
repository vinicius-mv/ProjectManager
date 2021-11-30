using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IJSRuntime jSRuntime;

        public TokenRepository(IJSRuntime jSRuntime)
        {
            this.jSRuntime = jSRuntime;
        }

        public async Task SetToken(string token)
        {
            await jSRuntime.InvokeVoidAsync("sessionStorage.setItem", "token", token);
        }

        public async Task<string> GetToken()
        {
            return await jSRuntime.InvokeAsync<string>("sessionStorage.getItem", "token");
        }
    }
}
