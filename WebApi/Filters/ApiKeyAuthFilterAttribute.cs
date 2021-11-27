using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;

namespace WebApi.Filters
{
    public class ApiKeyAuthFilterAttribute : Attribute, IAuthorizationFilter
    {
        private const string ApiKeyHeader = "ApiKey";
        private const string ClientIdHeader = "ClientId";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check ClientApiHeader
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeader, out var clientApiKey))
            {
                // short circuit MVC Pipeline
                context.Result = new UnauthorizedResult();
                return;
            }

            // Check ClientIdHeader 
            if (!context.HttpContext.Request.Headers.TryGetValue(ClientIdHeader, out var clientId))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            // DI in Action Filters 
            var config = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;

            // Check ApiKey
            var apiKey = config.GetValue<string>($"ApiKeyByClients:{clientId}");
            if (apiKey != clientApiKey)
                context.Result = new UnauthorizedResult();
        }
    }
}
