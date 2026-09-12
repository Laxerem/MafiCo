using MafiCo.Application.Game;
using MafiCo.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MafiCo.Application;

public static class ApplicationExtension {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        services.AddSingleton<GameContext>();
        services.AddSingleton<INotifySource>(sp => sp.GetRequiredService<GameContext>());
        services.AddSingleton<INotifyConsumer>(sp => sp.GetRequiredService<GameContext>());

        services.AddMediatR(conf =>
            conf.RegisterServicesFromAssembly(typeof(ApplicationExtension).Assembly)
        );

        return services;
    }
}
