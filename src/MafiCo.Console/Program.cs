using MafiCo.Console.App;
using MafiCo.Console.App.Configuration.ConfigParts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

DotNetEnv.Env.Load(".env");

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) => {
        config.AddJsonFile("players.json", optional: false, reloadOnChange: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) => {
        services.Configure<AppConfig>(options => {
            context.Configuration.Bind(options);
            
            var apiToken = context.Configuration["API_TOKEN"] 
                           ?? throw new Exception("API_TOKEN not found");
            
            typeof(AppConfig).GetProperty(nameof(AppConfig.Environment))
                ?.SetValue(options, new EnvironmentConfig(apiToken));
        });

        services.AddHostedService<App>();
    })
    .Build();

await host.RunAsync();