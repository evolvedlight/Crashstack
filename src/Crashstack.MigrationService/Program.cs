// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Crashstack.Data;
using Crashstack.MigrationService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<ApiDbInitializer>();

builder.AddServiceDefaults();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(ApiDbInitializer.ActivitySourceName));

builder.AddNpgsqlDbContext<CrashstackDbContext>("crashstack");

var app = builder.Build();

app.Run();