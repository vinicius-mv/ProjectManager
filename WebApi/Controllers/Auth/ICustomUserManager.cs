namespace WebApi.Controllers.Auth
{
    public interface ICustomUserManager
    {
        string Authenticate(string username, string password);
    }
}