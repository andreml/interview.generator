using InterviewGenerator.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InterviewGenerator.IntegrationTests.Infra;

public class DbFixture : IDisposable
{
    private readonly ApplicationDbContext _dbContext;

    public readonly string DatabaseName = $"InterviewGeneratorTest-{Guid.NewGuid()}";
    public readonly string ConnectionString;

    private bool _disposed;

    public DbFixture()
    {
        ConnectionString = $"Server=[::1];Database={DatabaseName};User=sa;Password=interview@2023;TrustServerCertificate=True";

        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseSqlServer(ConnectionString);

        _dbContext = new ApplicationDbContext(builder.Options);

        _dbContext.Database.Migrate();
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
                _dbContext.Database.EnsureDeleted();

            _disposed = true;
        }
    }
}

[CollectionDefinition("Database")]
public class DatabaseCollection : ICollectionFixture<DbFixture>
{
}
