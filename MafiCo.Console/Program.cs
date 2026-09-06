using System.Runtime.CompilerServices;
using MafiCo.Application.Interfaces.Stores;
using MafiCo.Console;
using MafiCo.Console.Configuration;
using MafiCo.Console.Configuration.Options;
using MafiCo.Console.Extensions;
using MafiCo.Console.System.Stores;
using MafiCo.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

var configPath = GetConfigPath();

var app = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(cfg => {
        cfg.AddJsonFile(configPath, reloadOnChange: true, optional: false);
    })
    .ConfigureLogging(logging => {
        logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Error);
        logging.AddFilter("LuckyPennySoftware.MediatR.License", LogLevel.Error);
    })
    .ConfigureServices((builder, services) => {
        services.AddInfrastructure();
        services.ConfigureServices(builder.Configuration);
        services.AddUi();
        services.AddScoped<ConfigurationController>(sp =>
            new ConfigurationController(configPath, sp.GetRequiredService<IOptions<GlobalConfigOption>>()));
        services.AddScoped<IUserStore, UserStore>();
        // services.AddScoped<IGameStore, GameStore>();
        services.AddScoped<App>();
    })
    .Build();

using var scope = app.Services.CreateScope();
var application = scope.ServiceProvider.GetRequiredService<App>();
await application.RunAsync();

static string GetConfigPath([CallerFilePath] string sourceFilePath = "") =>
    Path.Combine(Path.GetDirectoryName(sourceFilePath)!, "appconfig.json");
