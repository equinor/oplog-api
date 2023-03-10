using Microsoft.OpenApi.Models;

namespace Oplog.Api
{
    public static class SwaggerSetup
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,

                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri($"https://login.microsoftonline.com/{configuration["AzureAd:TenantId"]}/oauth2/v2.0/token"),
                            AuthorizationUrl = new Uri($"https://login.microsoftonline.com/{configuration["AzureAd:TenantId"]}/oauth2/v2.0/authorize"),
                            Scopes = new Dictionary<string, string> {
                                { $"api://{configuration["AzureAd:ClientId"]}/User.Impersonation", "Oplog API" },
                                { $"api://{configuration["AzureAd:ClientId"]}/.default", "Oplog API" },
                                { $"api://{configuration["AzureAd:ClientId"]}/oplog.read", "Oplog API" }
                            },
                        }
                    },
                    Description = "Oplog Security Scheme"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "OAuth2",
                                    Type = ReferenceType.SecurityScheme
                                } ,
                                Scheme = "OAuth2",
                                BearerFormat = "JWT",
                                Type = SecuritySchemeType.Http,
                                Name = "Bearer",
                                In = ParameterLocation.Header
                            },
                            new List<string> { 
                                $"{configuration["AzureAd:ClientId"]}/User.Impersonation",
                                $"{configuration["AzureAd:ClientId"]}/.default",
                                $"{configuration["AzureAd:ClientId"]}/oplog.read"
                            }

                        }
                    });
            });
        }

        public static void Configure(IConfiguration configuration, IApplicationBuilder app)
        {           
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Oplog API");
                c.OAuthClientId(configuration["AzureAd:ClientId"]);
                c.OAuthAppName("Oplog");
                c.OAuthScopeSeparator(" ");
                c.OAuthUsePkce();
            });
        }
    }
}
