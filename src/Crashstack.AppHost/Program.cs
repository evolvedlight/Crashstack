var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Crashstack_MigrationService>("migration");
builder.AddProject<Projects.Crashstack_Server>("crashstack-server");
builder.Build().Run();
