using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Oplog.Persistence;

namespace Oplog.IntegrationTests
{
    [SetUpFixture]
    public class TestRunSetUp
    {
        public static IConfigurationRoot Configuration { get; private set; }
        public static string ConnectionString { get; set; }
        private readonly OplogDbContext _oplogDbContext;

        public TestRunSetUp()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddUserSecrets<TestRunSetUp>()
                                .AddEnvironmentVariables();
            Configuration = configurationBuilder.Build();

            var databaseName = GetRandomDatabaseName();
            ConnectionString = Configuration.GetValue<string>("ConnectionString").Replace("DbName", databaseName);

            var optionsBuilder = new DbContextOptionsBuilder<OplogDbContext>();
            optionsBuilder.UseSqlServer(ConnectionString,
                                        providerOptions =>
                                        {
                                            providerOptions.EnableRetryOnFailure();
                                            providerOptions.CommandTimeout(240);
                                        });
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information)
                                .EnableSensitiveDataLogging();

            var options = optionsBuilder.Options;
            _oplogDbContext = new OplogDbContext(options);
        }

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            _oplogDbContext.Database.EnsureDeleted();
            _oplogDbContext.Database.EnsureCreated();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            _oplogDbContext.Database.EnsureDeleted();
        }

        private static string GetRandomDatabaseName()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
