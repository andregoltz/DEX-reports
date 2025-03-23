using DEX.Core.DTO.Request;
using DEX.Core.Entities;
using DEX.Core.Helper;
using DEX.Core.Services;
using DEX.Infra.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DEX.API.Extensions
{
    public static class DEXReportContextExtension
    {
        public static void MapDEXEndpoints(this WebApplication app)
        {
            #region Post
            app.MapPost("/api/v1/vdi-dex", async (DEXRequest request, DEXService service) =>
            {
                if (string.IsNullOrWhiteSpace(request.Machine.ToString()) || string.IsNullOrWhiteSpace(request.DexFile))
                {
                    return Results.BadRequest("Machine and DexFile are required.");
                }

                try
                {
                    var (dEXMeter, dEXLaneMeter) = await service.ProcessDEXFileAsync(request.DexFile, request.Machine);

                    return Results.Ok(new
                    {
                        Message = "DEX file saved successfully.",
                        DEXMeterID = dEXMeter.ID,
                        DEXLaneMeterID = dEXLaneMeter.ID,
                        status = "success"
                    });
                }
                catch (Exception ex)
                {
                    var errorResponse = new
                    {
                        message = $"Error saving DEX file: {ex.Message}",
                        status = "error"
                    };

                    return Results.BadRequest(errorResponse);
                }
            }).RequireAuthorization();
            #endregion

            #region Delete
            app.MapDelete("/api/v1/deleteAll", async (DEXRepository repository) =>
            {
                await repository.DeleteAll();
                return Results.Ok(new { Message = "DEX data deleted successfully.", status = "success" });
            }).RequireAuthorization();
            #endregion
        }
    }
}
