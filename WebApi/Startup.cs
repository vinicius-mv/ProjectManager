using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataStore.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebApi.Controllers.Auth;

namespace PlatformDemo
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IWebHostEnvironment env)
        {
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsDevelopment())
            {
                services.AddDbContext<BugTrackerContext>(options =>
                {
                    options.UseInMemoryDatabase("BugTracker");
                });
            }

            services.AddControllers();

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                //options.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("x-api-version"), new QueryStringApiVersionReader("version", "api-version"));
                options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
            });

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My Web API v1", Version = "version 1" }); // generate swagger file at path: /swagger/v1/swagger.json"
                options.SwaggerDoc("v2", new OpenApiInfo { Title = "My Web API v2", Version = "version 2" }); // generate swagger file at path: /swagger/v2/swagger.json"

                // Enable Authorization
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "tokenheader",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer,",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your valid token in the textbox below.",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type= ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:50000", "https://localhost:50001")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            //services.AddSingleton<ICustomTokenManager, CustomTokenManager>(); // Guid based simple token
            services.AddSingleton<ICustomTokenManager, JwtTokenManager>();
            services.AddSingleton<ICustomUserManager, CustomUserManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, BugTrackerContext context)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Create in-memory database for dev enviroment
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

            }

            // Configure OpenAPI
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "WebAPI v2");
            });

            app.UseRouting();

            // Default CORS Policy
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
