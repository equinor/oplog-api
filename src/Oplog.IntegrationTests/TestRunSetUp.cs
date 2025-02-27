using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Oplog.Persistence;
using System.Runtime.InteropServices;
using Testcontainers.MsSql;

namespace Oplog.IntegrationTests;

[SetUpFixture]
public class TestRunSetUp
{
    public static IConfigurationRoot Configuration { get; private set; }
    public static string ConnectionString { get; set; }
    private readonly OplogDbContext _oplogDbContext;
    private readonly MsSqlContainer _msSqlContainer;

    public TestRunSetUp()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            _msSqlContainer = new MsSqlBuilder()
                .WithImage(
                    "mcr.microsoft.com/mssql/server:2022-latest"
                )
                .WithPortBinding(1433, true)
                .Build();
        }
        else
        {
            _msSqlContainer = new MsSqlBuilder()
                .WithPortBinding(1433, true)
                .Build();
        }
        _msSqlContainer.StartAsync().Wait();
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddUserSecrets<TestRunSetUp>()
                            .AddEnvironmentVariables();
        Configuration = configurationBuilder.Build();
     
        ConnectionString = _msSqlContainer.GetConnectionString();

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
        _oplogDbContext.Database.EnsureCreated();
        _oplogDbContext.Database.ExecuteSqlRaw(@"CREATE VIEW LogsView
                                    AS
                                    SELECT        l.Id, l.LogTypeId, l.ParentId, l.LastChangeUserId, l.LastChangeDateTime, l.UpdatedBy, l.UpdatedDate, l.CreatedById, l.Author, l.ScheduleItemState, l.CreatedBy, l.CreatedDate, l.Text, l.OperationAreaId, l.EffectiveTime, l.Unit, 
                                    l.Subtype, l.IsCritical, oa.Name AS AreaName, logtypes.Name AS LogTypeName, subtypes.Name AS SubTypeName, units.Name AS UnitName
                                    FROM            dbo.Logs AS l LEFT OUTER JOIN
                                    dbo.OperationAreas AS oa ON l.OperationAreaId = oa.Id LEFT OUTER JOIN
                                    dbo.ConfiguredTypes AS logtypes ON l.LogTypeId = logtypes.Id LEFT OUTER JOIN
                                    dbo.ConfiguredTypes AS subtypes ON l.Subtype = subtypes.Id LEFT OUTER JOIN
                                    dbo.ConfiguredTypes AS units ON l.Unit = units.Id;");
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {       
        _msSqlContainer.StopAsync().Wait();
    }
}
