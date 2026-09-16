using MafiCo.Application.Game;
using MafiCo.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using GameEntity = MafiCo.Domain.AggregatesModel.GameAggregate.Game;

namespace MafiCo.Application;

public static class ApplicationExtension {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        services.AddSingleton<GameContext>();

        services.AddMediatR(conf =>
            conf.RegisterServicesFromAssembly(typeof(ApplicationExtension).Assembly)
        );

        return services;
    }

    public static async ValueTask DispatchGameEvents(this IMediator mediator, GameEntity entity) {
        if (entity.Notifications.Count == 0) return;
        
        var domainEvents = entity.Notifications.ToList();
        entity.Notifications.Clear();

        foreach (var domainEvent in domainEvents) {
            await mediator.Publish(domainEvent);
        }
    }
}
