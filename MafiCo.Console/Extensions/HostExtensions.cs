using System.Reflection;
using MafiCo.Console.Configuration.Options;
using MafiCo.Console.Presentation;
using MafiCo.Console.Presentation.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MafiCo.Console.Extensions;

public static class HostExtensions {
    public static IServiceCollection AddUi(this IServiceCollection services) {
        services.AddScoped<UserInterface>();

        var assembly = Assembly.GetExecutingAssembly();
        var windowImplementations = assembly
            .GetTypes()
            .Where(x => typeof(Window).IsAssignableFrom(x) && !x.IsAbstract);

        foreach (var window in windowImplementations) {
            services.AddTransient(window);
        }

        return services;
    }

    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration) {
        services.Configure<UserOptions>(configuration.GetSection("User"));
        services.Configure<GlobalConfigOption>(configuration);
        return services;
    }
}
