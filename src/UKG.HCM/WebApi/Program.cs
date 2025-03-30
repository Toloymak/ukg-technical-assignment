using System.Diagnostics.CodeAnalysis;
using UKG.HCM.WebApi;
using UKG.HCM.WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDb();
builder.Services.AddWebApi();
builder.ConfigureSwagger();

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

app.RegisterAllEndpoints();

await app.MigrateDatabaseAsync();

await app.RunAsync();

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global",
    Justification = "Used by the WebApplicationFactory in tests")]
public partial class Program { }