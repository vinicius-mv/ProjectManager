using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebApi.Settings;

namespace WebApi.Controllers.Auth
{
    public class JwtTokenManager : ICustomTokenManager
    {
        private readonly IConfiguration configuration;
        private JwtSecurityTokenHandler tokenHandler;
        private byte[] secretKey;

        public JwtTokenManager(IConfiguration configuration)
        {
            this.configuration = configuration;

            //secretKey = Encoding.ASCII.GetBytes(configuration.GetValue<string>("JwtTokenSettings:SecretKey"));
            JwtTokenSettings jwtTokenSettings = configuration.GetSection("JwtTokenSettings").Get<JwtTokenSettings>();
            secretKey = Encoding.ASCII.GetBytes(jwtTokenSettings.SecretKey);

            tokenHandler = new JwtSecurityTokenHandler();

        }

        public string CreateToken(string username)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username)
                    }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GetUserInfoByToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var jwtToken = tokenHandler.ReadJwtToken(token.Replace("\"", string.Empty));
            var claim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name");

            if (claim != null)
                return claim.Value;

            return null;
        }

        public bool VerifyToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return false;

            SecurityToken securityToken;
            try
            {
                tokenHandler.ValidateToken(
                    token.Replace("\"", string.Empty),
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                        ValidateLifetime = true,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ClockSkew = TimeSpan.Zero
                    },
                    out securityToken);
            }
            catch (SecurityTokenException)
            {
                return false;
            }
            return securityToken != null;
        }
    }
}
