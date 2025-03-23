using DEX.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();


var app = builder.Build();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors("AllowBlazorApp");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapDEXEndpoints();

app.Run();


