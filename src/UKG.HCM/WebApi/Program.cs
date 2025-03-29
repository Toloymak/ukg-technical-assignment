using UKG.HCM.WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.RegisterAllEndpoints();

await app.RunAsync();