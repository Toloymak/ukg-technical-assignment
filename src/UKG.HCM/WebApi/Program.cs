using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using UKG.HCM.Infrastructure.Contexts;
using UKG.HCM.Infrastructure.Entities;
using UKG.HCM.WebApi;
using UKG.HCM.WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDb();
builder.Services.AddWebApi();
builder.ConfigureSwagger();

builder.Services.AddAuthorization();
builder.Services
    .AddIdentityApiEndpoints<IdentityUser<Guid>>()
    .AddEntityFrameworkStores<AppDbContext>();


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

app.MapIdentityApi<IdentityUser<Guid>>();
app.RegisterAllEndpoints();

await app.MigrateDatabaseAsync();

await app.RunAsync();

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global",
    Justification = "Used by the WebApplicationFactory in tests")]
public partial class Program { }