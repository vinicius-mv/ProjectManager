namespace WebApi.Controllers.Auth
{
    public class CustomTokenManager : ICustomTokenManager
    {
        public string CreateToken(string username)
        {
            return string.Empty;
        }

        public bool VerifyToken(string token)
        {
            return false;
        }

        public string GetUserInfoByToken(string token)
        {
            return string.Empty;
        }
    }
}
