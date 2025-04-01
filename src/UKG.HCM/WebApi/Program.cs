using System.Diagnostics.CodeAnalysis;
using UKG.HCM.WebApi;
using UKG.HCM.WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider((_, options) =>
{
    options.ValidateScopes = true;
    options.ValidateOnBuild = true;
});

builder.ConfigureDb();
builder.AddWebApiDependencies();
builder.ConfigureSwagger();
builder.AddAuthenticationAndAuthorization();
builder.ConfigureCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapOpenApi();
app.UseSwaggerUi(c =>
{
    c.DocumentPath = "/openapi/v1.json";
    c.Path = string.Empty;
});

app.UseCors("UI"); // Mobe policy name to config
app.UseAuthentication();
app.UseAuthorization();

app.RegisterAllEndpoints();

await app.MigrateDatabaseAsync();

// TODO: fast solution, use config to enable instead
await app.SeedTestData();

await app.RunAsync();

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global",
    Justification = "Used by the WebApplicationFactory in tests")]
public partial class Program { }