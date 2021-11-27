using System.Collections.Generic;
using System.Linq;

namespace WebApi.Controllers.Auth
{
    public class CustomTokenManager : ICustomTokenManager
    {
        private List<Token> tokens = new List<Token>();

        public string CreateToken(string username)
        {
            var token = new Token(username);

            tokens.Add(token);

            return token.TokenString;
        }

        public bool VerifyToken(string token)
        {
            return tokens.Any(x => token != null && token.Contains(x.TokenString) && x.ExpiryDate > System.DateTime.Now);
        }

        public string GetUserInfoByToken(string tokenString)
        {
            var token = tokens.FirstOrDefault(x => tokenString != null && tokenString.Contains(x.TokenString));
            if(token != null) 
                return token.UserName;

            return string.Empty;
        }
    }
}
