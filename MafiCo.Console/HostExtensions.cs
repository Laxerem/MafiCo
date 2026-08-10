using System.Reflection;
using MafiCo.Console.Presentation.Base;
using MafiCo.Domain.AggregatesModel.LlmBotAggregate;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Domain.SeedWork;
using MafiCo.Infrastructure;
using MafiCo.Infrastructure.Repositories;
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

    public static IServiceCollection AddDatabase(this IServiceCollection services) {
        services.AddDbContext<ApplicationContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<ILlmBotRepository, LlmBotRepository>();
        return services;
    }
}