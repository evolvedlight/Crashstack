namespace Crashstack.Server;

public record Provider(string Name, string Assembly)
{
    public static readonly Provider Sqlite = new(nameof(Sqlite), typeof(Migrations.Sqlite.Marker).Assembly.GetName().Name!);
    public static readonly Provider Postgres = new(nameof(Postgres), typeof(Migrations.Postgres.Marker).Assembly.GetName().Name!);
}
