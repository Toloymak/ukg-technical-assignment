var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.WebApi>("WebApi");

await builder.Build().RunAsync();