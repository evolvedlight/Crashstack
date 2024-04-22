using Crashstack.Data;
using Crashstack.Server.Hubs;
using Microsoft.EntityFrameworkCore;
using static Crashstack.Server.Provider;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddRequestDecompression();

var config = builder.Configuration;

var provider = builder.Configuration.GetValue("provider", Sqlite.Name);

builder.Services.AddDbContext<CrashstackDbContext>(options =>
{
    if (provider == Sqlite.Name)
    {
        options.UseSqlite(
            config.GetConnectionString(Sqlite.Name)!,
            x => x.MigrationsAssembly(Sqlite.Assembly)
        );
    }

    if (provider == Postgres.Name)
    {
        options.UseNpgsql(
            config.GetConnectionString(Postgres.Name)!,
            x => x.MigrationsAssembly(Postgres.Assembly)
        );
    }
});

var app = builder.Build();

if (provider == Sqlite.Name)
{
    var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<CrashstackDbContext>();
    db.Database.Migrate();
}

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseRequestDecompression();
app.UseAuthorization();

app.MapHub<CrashstackHub>("/crashstack");

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
