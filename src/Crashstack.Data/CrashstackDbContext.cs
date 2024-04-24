using Crashstack.Data.Entities;
using Microsoft.EntityFrameworkCore;
namespace Crashstack.Data;

public class CrashstackDbContext(DbContextOptions<CrashstackDbContext> options) : DbContext(options)
{
    public DbSet<CrashstackEvent> CrashstackEvents { get; set; }
    public DbSet<Trace> Traces { get; set; }
    public DbSet<Issue> Issues { get; set; }
}
