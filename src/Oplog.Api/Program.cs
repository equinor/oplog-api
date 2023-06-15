using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Oplog.Api;
using Oplog.Api.Middleware;
using Oplog.Core.Infrastructure;
using Oplog.Core.Queries;
using Oplog.Persistence;
using Oplog.Persistence.Repositories;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var keyVaultUrl = configuration["KeyVaultEndpoint"];
var clientId = configuration["AzureAd:ClientId"];
var clientSecret = configuration["azure:ClientSecret"];
builder.Configuration.AddAzureKeyVault(keyVaultUrl, clientId, clientSecret);
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(optionsA =>
    {
    }, optionsB =>
    {
        configuration.Bind("AzureAd", optionsB);
        var defaultBackChannel = new HttpClient();
        defaultBackChannel.DefaultRequestHeaders.Add("Origin", "oplog");
        optionsB.Backchannel = defaultBackChannel;
    })
    .EnableTokenAcquisitionToCallDownstreamApi(e => { })
    .AddInMemoryTokenCaches();

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
    builder =>
    {
        //var domainsAsArray = new string[corsDomainsFromConfig.Count];
        //corsDomainsFromConfig.CopyTo(domainsAsArray);

        //builder.WithOrigins(domainsAsArray);
        builder.AllowAnyOrigin()
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<OplogDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Oplog")));
builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
builder.Services.AddTransient<ILogsRepository, LogsRepository>();
builder.Services.AddTransient<IOperationsAreasRepository, OperationAreasRepository>();
builder.Services.AddTransient<IConfiguredTypesRepository, ConfiguredTypesRepository>();
builder.Services.AddTransient<ICustomFilterRepository, CustomFilterRepository>();
builder.Services.AddTransient<ILogTemplateRepository, LogTemplateRepository>();
builder.Services.AddTransient<ILogsQueries, LogsQueries>();
builder.Services.AddTransient<IOperationAreasQueries, OperationAreasQueries>();
builder.Services.AddTransient<IConfiguredTypesQueries, ConfiguredTypesQueries>();
builder.Services.AddTransient<ICustomFilterQueries, CustomFilterQueries>();
builder.Services.AddTransient<ILogTemplateQueries, LogTemplateQueries>();
// The following line enables Application Insights telemetry collection.
var appinsightConnStr = configuration["ApplicationInsights:ConnectionString"];
var optionsAppInsight = new ApplicationInsightsServiceOptions { ConnectionString = configuration["ApplicationInsights:ConnectionString"] };

builder.Services.AddApplicationInsightsTelemetry(options: optionsAppInsight);
//Add command handlers
CommandHandlersSetup.AddCommandHandlers(builder.Services, typeof(ICommandHandler<>));
CommandHandlersSetup.AddCommandHandlers(builder.Services, typeof(ICommandHandler<,>));

SwaggerSetup.ConfigureServices(configuration, builder.Services);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<OplogDbContext>();
    dbContext.Database.Migrate();
}
app.UseCors(MyAllowSpecificOrigins);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
SwaggerSetup.Configure(configuration, app);
app.UseMiddleware(typeof(ErrorHandlingMiddleware));
app.MapControllers();
app.Run();


