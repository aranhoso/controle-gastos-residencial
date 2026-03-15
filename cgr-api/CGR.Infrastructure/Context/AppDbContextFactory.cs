using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CGR.Infrastructure.Context;

/// <summary>
/// Fábrica utilizada em tempo de design pelo EF Core Tools para criar o AppDbContext.
/// </summary>
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        var connectionString =
            Environment.GetEnvironmentVariable("CGR_CONNECTION_STRING")
            ?? "Host=localhost;Port=5432;Database=cgr_db;Username=postgres;Password=senha";

        optionsBuilder.UseNpgsql(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}
