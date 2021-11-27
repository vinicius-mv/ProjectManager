using System;

namespace WebApi.Controllers.Auth
{
    public class Token
    {
        public Token(string userName)
        {
            this.UserName = UserName;
            this.TokenString = Guid.NewGuid().ToString();
            this.ExpiryDate = DateTime.Now.AddMinutes(1);
        }

        public string TokenString { get; }
        public string UserName { get; }
        public DateTime ExpiryDate { get; }
    }
}
