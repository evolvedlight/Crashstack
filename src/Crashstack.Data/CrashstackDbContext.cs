using Crashstack.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace Crashstack.Data;

public class PostgresCrashstackDbContext : CrashstackDbContext
{
    public PostgresCrashstackDbContext(DbContextOptions<CrashstackDbContext> options) : base(options)
    {
    }
}

public class SqliteCrashstackDbContext : CrashstackDbContext
{
    public SqliteCrashstackDbContext(DbContextOptions<CrashstackDbContext> options) : base(options)
    {
    }
}


public class CrashstackDbContext(DbContextOptions<CrashstackDbContext> options) : DbContext(options)
{
    public DbSet<CrashstackEvent> CrashstackEvents { get; set; }
    public DbSet<Trace> Traces { get; set; }
    public DbSet<Issue> Issues { get; set; }
}
