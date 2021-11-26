using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;

namespace WebApi.Filters
{
    public class ApiKeyAuthFilterAttribute : Attribute, IAuthorizationFilter
    {
        private const string ApiKeyHeader = "apiKey";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeader, out var clientApiKey))
            {
                // short circuit MVC Pipeline
                context.Result = new UnauthorizedResult();
                return;
            }

            // DI in Filter Actions
            var config = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;

            var apiKey = config.GetValue<string>("ApiKey");

            if (apiKey != clientApiKey)
                context.Result = new UnauthorizedResult();

        }
    }
}
