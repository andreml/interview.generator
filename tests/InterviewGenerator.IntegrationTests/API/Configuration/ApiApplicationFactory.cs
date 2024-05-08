using InterviewGenerator.IntegrationTests.Infra;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Xunit;

namespace InterviewGenerator.IntegrationTests.API.Configuration;

[Collection("Database")]
public class ApiApplicationFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
    private readonly DbFixture _dbFixture;

    public ApiApplicationFactory(DbFixture dbFixture)
    {
        _dbFixture = dbFixture;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");


        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(LogLevel.None);
        });

        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "ConnectionStrings:DataBase", _dbFixture.ConnectionString}
            });
        });
    }
}
