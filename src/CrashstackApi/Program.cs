using Crashstack.Data;
using Vite.AspNetCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddRazorPages();

// Vite UI
builder.Services.AddViteServices(config =>
{
    config.Base = "/dist/";
    config.Server.AutoRun = true;
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRequestDecompression();

builder.AddNpgsqlDbContext<CrashstackDbContext>("crashstack");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

//app.MapDefaultEndpoints();

app.UseStaticFiles();
app.UseRouting();
app.UseRequestDecompression();
//app.UseHttpsRedirection();

app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.UseViteDevelopmentServer();
app.Run();
