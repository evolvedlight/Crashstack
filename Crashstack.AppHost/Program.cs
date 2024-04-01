var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CrashstackApi>("crashstackapi");

builder.AddProject<Projects.Crashstack_MigrationService>("migration");
builder.Build().Run();
