using System.Runtime.CompilerServices;
using MafiCo.Console;
using MafiCo.Console.Configuration;
using MafiCo.Console.Configuration.Options;
using MafiCo.Console.System;
using MafiCo.Infrastructure.Interfaces;
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
    .ConfigureLogging(logging => logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Error))
    .ConfigureServices((builder, services) => {
        services.AddDatabase();
        services.ConfigureServices(builder.Configuration);
        services.AddUi();
        services.AddMediatR(conf =>
            conf.RegisterServicesFromAssembly(typeof(Program).Assembly)
        );
        services.AddScoped<ConfigurationController>(sp =>
            new ConfigurationController(configPath, sp.GetRequiredService<IOptions<GlobalConfigOption>>()));
        services.AddScoped<IAppStore, AppStore>();
        services.AddScoped<App>();
    })
    .Build();

using var scope = app.Services.CreateScope();
var application = scope.ServiceProvider.GetRequiredService<App>();
await application.RunAsync();

static string GetConfigPath([CallerFilePath] string sourceFilePath = "") =>
    Path.Combine(Path.GetDirectoryName(sourceFilePath)!, "appconfig.json");
