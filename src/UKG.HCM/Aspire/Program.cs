var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.WebApi>("WebApi");
builder.AddProject<Projects.WebUi>("Frontend");

await builder.Build().RunAsync();