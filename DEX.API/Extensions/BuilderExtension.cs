namespace DEX.API.Extensions
{
    public static class BuilderExtension
    {
        public static void AddOpenApi(this WebApplicationBuilder builder)
        {
            builder.Services.AddOpenApi();
        }

        public static void AddCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazorApp",
                    policy => policy.WithOrigins("https://localhost:7143") // Permitir Blazor
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });
        }
    }
}
