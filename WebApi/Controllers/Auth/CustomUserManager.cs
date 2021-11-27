namespace WebApi.Controllers.Auth
{
    public class CustomUserManager : ICustomUserManager
    {
        private readonly ICustomTokenManager customTokenManager;

        public CustomUserManager(ICustomTokenManager customTokenManager)
        {
            this.customTokenManager = customTokenManager;
        }

        public string Authenticate(string username, string password)
        {
            // validate credentials

            // generate token
            return customTokenManager.CreateToken(username);
        }
    }
}
