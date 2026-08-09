using System.Reflection;
using MafiCo.Console.Presentation.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MafiCo.Console;

public static class HostExtensions {
    public static IServiceCollection AddUiWindows(this IServiceCollection services) {
        var assembly = Assembly.GetExecutingAssembly();
        var windowImplementations = assembly
            .GetTypes()
            .Where(x => typeof(Window).IsAssignableFrom(x) && !x.IsAbstract);

        foreach (var window in windowImplementations) {
            services.AddTransient(window);
        }
        return services;
    }
}