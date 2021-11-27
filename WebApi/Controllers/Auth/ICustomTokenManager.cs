namespace WebApi.Controllers.Auth
{
    public interface ICustomTokenManager
    {
        string CreateToken(string username);
        string GetUserInfoByToken(string token);
        bool VerifyToken(string token);
    }
}