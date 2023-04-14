using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Oplog.Api;
using Oplog.Core.Infrastructure;
using Oplog.Persistence;
using Oplog.Persistence.Repositories;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
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

builder.Services.AddDbContext<OplogDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Oplog")));
builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
builder.Services.AddTransient<ICommentsRepository, CommentsRepository>();
builder.Services.AddTransient<IAreasRepository, AreasRepository>();
builder.Services.AddTransient<IConfiguredTypesRepository, ConfiguredTypesRepository>();

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
app.MapControllers();
app.Run();


