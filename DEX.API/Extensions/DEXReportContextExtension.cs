using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace DEX.API.Extensions
{
    public static class DEXReportContextExtension
    {
        public static void MapDEXEndpoints(this WebApplication app)
        {
            #region Post
            app.MapPost("/api/v1/vdi-dex", async (HttpRequest request) =>
            {
                using var reader = new StreamReader(request.Body, Encoding.UTF8);
                var dexContent = await reader.ReadToEndAsync();

                return Results.Ok("DEX file received successfully.");
            }).RequireAuthorization();
            #endregion
        }
    }
}
