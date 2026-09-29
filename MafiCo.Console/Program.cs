using System.Runtime.CompilerServices;
using MafiCo.Application;
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
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;

var configPath = GetConfigPath();
var logsDirectory = Path.Combine(Path.GetDirectoryName(configPath)!, "logs");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
    .MinimumLevel.Override("LuckyPennySoftware.MediatR.License", LogEventLevel.Error)
    .Enrich.FromLogContext()
    .Enrich.WithThreadId()
    .WriteTo.File(
        Path.Combine(logsDirectory, "mafico-.log"),
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 14,
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level:u3}] \u001b[36m[TID:{ThreadId}]\u001b[0m [{SourceContext}]: {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

try {
    var app = Host.CreateDefaultBuilder(args)
        .UseSerilog()
        .ConfigureAppConfiguration(cfg => {
            cfg.AddJsonFile(configPath, reloadOnChange: true, optional: false);
        })
        .ConfigureServices((builder, services) => {
            services.AddApplication();
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

    await app.Services.ApplyMigrationsAsync();

    using var scope = app.Services.CreateScope();
    var application = scope.ServiceProvider.GetRequiredService<App>();
    await application.RunAsync();
}
finally {
    Log.CloseAndFlush();
}

static string GetConfigPath([CallerFilePath] string sourceFilePath = "") =>
    Path.Combine(Path.GetDirectoryName(sourceFilePath)!, "appconfig.json");