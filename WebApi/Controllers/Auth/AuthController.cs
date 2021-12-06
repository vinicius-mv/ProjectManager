﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        [Route("authenticate")]
        public async Task<string> AuthenticateAsync(UserCredential userCredential)
        {
            return await Task.FromResult(customUserManager.Authenticate(userCredential.UserName, userCredential.Password));
        }

        [HttpGet]
        [Route("verifytoken")]
        public async Task<bool> VerifyAsync(string token)
        {
            return await Task.FromResult(customTokenManager.VerifyToken(token));
        }

        [HttpPost]
        [Route("getuserinfo")]
        public async Task<string> GetUserInfoByTokenAsync(string token)
        {
            return await Task.FromResult(customTokenManager.GetUserInfoByToken(token));
        }
    }
}