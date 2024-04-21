using Crashstack.Data;
using Crashstack.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddRequestDecompression();

builder.AddNpgsqlDbContext<CrashstackDbContext>("crashstack");

var app = builder.Build();

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
