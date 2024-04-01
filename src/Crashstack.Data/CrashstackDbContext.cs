using Crashstack.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace Crashstack.Data;

public class CrashstackDbContext(DbContextOptions<CrashstackDbContext> options) : DbContext(options)
{
    public DbSet<CrashstackEvent> CrashstackEvents { get; set; }
    public DbSet<Trace> Traces { get; set; }
    public DbSet<ExceptionEvent> ExceptionEvents { get; set; }
}
