using MafiCo.Application.Game.Contexts;
using MafiCo.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MafiCo.Application;

public static class ApplicationExtension {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        services.AddSingleton<GameContext>();
        services.AddSingleton<IEventSource>(sp => sp.GetRequiredService<GameContext>());
        services.AddSingleton<IEventConsumer>(sp => sp.GetRequiredService<GameContext>());

        return services;
    }
}
