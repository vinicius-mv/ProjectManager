using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Controllers.Auth.Dtos;

namespace WebApi.Controllers.Auth
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICustomTokenManager customTokenManager;
        private readonly ICustomUserManager customUserManager;

        public AuthController(ICustomUserManager customUserManager, ICustomTokenManager customTokenManager)
        {
            this.customUserManager = customUserManager;
            this.customTokenManager = customTokenManager;
        }

        [HttpPost]
        [Route("/authenticate")]
        public async Task<string> Authenticate(UserCredential userCredential)
        {
            return await Task.FromResult(customUserManager.Authenticate(userCredential.UserName, userCredential.Password));
        }

        [HttpGet]
        [Route("/verifytoken")]
        public async Task<bool> Verify(string token)
        {
            return await Task.FromResult(customTokenManager.VerifyToken(token));
        }

        [HttpPost]
        [Route("/getuserinfo")]
        public async Task<ActionResult<string>> GetUserInfoByToken([FromBody] TokenDto request)
        {
            if (string.IsNullOrEmpty(request.Token))
                return BadRequest("Invalid token");

            return await Task.FromResult(customTokenManager.GetUserInfoByToken(request.Token));
        }
    }
}
