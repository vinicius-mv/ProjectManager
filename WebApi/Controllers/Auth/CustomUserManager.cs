using System.Collections.Generic;

namespace WebApi.Controllers.Auth
{
    public class CustomUserManager : ICustomUserManager
    {
        // in memory storage (simplified)
        private Dictionary<string, string> credentials = new Dictionary<string, string>
        {
            { "frank", "passoword" },
            { "tom", "password1" }
        };

        private readonly ICustomTokenManager customTokenManager;

        public CustomUserManager(ICustomTokenManager customTokenManager)
        {
            this.customTokenManager = customTokenManager;
        }

        public string Authenticate(string username, string password)
        {
            // validate credentials
            if(credentials[username] != password) 
                return string.Empty;

            // generate token
            return customTokenManager.CreateToken(username);
        }
    }
}
