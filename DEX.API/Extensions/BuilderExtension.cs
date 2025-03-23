using DEX.API.AuthenticationHandler;
using DEX.Core.Services;
using DEX.Infra.Repository;
using Microsoft.AspNetCore.Authentication;

namespace DEX.API.Extensions
{
    public static class BuilderExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            builder.Services.AddAuthorization();

            builder.Services.AddOpenApi();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazorApp",
                    policy => policy.WithOrigins("https://localhost:7143") // Permitir Blazor
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddScoped<DEXRepository>();
            builder.Services.AddScoped<DEXService>();
        }
    }
}
