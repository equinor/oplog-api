using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using Oplog.Core.Infrastructure;
using Oplog.Core.Queries;
using Oplog.Persistence;
using Oplog.Persistence.Repositories;

namespace Oplog.IntegrationTests
{
    public class TestBase
    {
        protected IHost Host { get; set; }
        protected ICommandDispatcher CommandDispatcher { get; private set; }

        [SetUp]
        public void SetUp()
        {
            Host = new HostBuilder()
                .UseEnvironment("Development")
                .ConfigureHostConfiguration(config => config.AddConfiguration(TestRunSetUp.Configuration))
                .ConfigureServices(services =>
                {                   
                    services.
                        AddDbContext<OplogDbContext>(options =>
                        {
                            options.UseSqlServer(TestRunSetUp.ConnectionString);
                            options.EnableSensitiveDataLogging();
                        });
                    services.AddScoped<ICommandDispatcher, CommandDispatcher>();
                    services.AddTransient<ILogsRepository, LogsRepository>();
                    services.AddTransient<IOperationsAreasRepository, OperationAreasRepository>();
                    services.AddTransient<IConfiguredTypesRepository, ConfiguredTypesRepository>();
                    services.AddTransient<ICustomFilterRepository, CustomFilterRepository>();
                    services.AddTransient<ILogTemplateRepository, LogTemplateRepository>();
                    services.AddTransient<ILogsQueries, LogsQueries>();
                    services.AddTransient<IOperationAreasQueries, OperationAreasQueries>();
                    services.AddTransient<IConfiguredTypesQueries, ConfiguredTypesQueries>();
                    services.AddTransient<ICustomFilterQueries, CustomFilterQueries>();
                    services.AddTransient<ILogTemplateQueries, LogTemplateQueries>();
                    AddCommandHandlers(services, typeof(ICommandHandler<>));
                    AddCommandHandlers(services, typeof(ICommandHandler<,>));
                })
                .Build();

            CommandDispatcher = Host.Services.GetService(typeof(ICommandDispatcher)) as ICommandDispatcher;
        }

        private static void AddCommandHandlers(IServiceCollection services, Type interfaceType)
        {
            var types = interfaceType.Assembly.GetTypes().Where(t =>
                t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType));
            foreach (var type in types)
            {
                type.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType)
                    .ToList().ForEach(i => services.AddScoped(i, type));
            }
        }
    }
}
