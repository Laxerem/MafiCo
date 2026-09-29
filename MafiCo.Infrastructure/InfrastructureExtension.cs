using MafiCo.Application.Interfaces;
using MafiCo.Domain.AggregatesModel.BotAggregate;
using MafiCo.Domain.AggregatesModel.GameAggregate;
using MafiCo.Domain.AggregatesModel.LlmAggregate;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Infrastructure.Persistence;
using MafiCo.Infrastructure.Persistence.Interceptors;
using MafiCo.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MafiCo.Infrastructure;

public static class InfrastructureExtension {
    public static void AddInfrastructure(this IServiceCollection services) {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<ILlmRepository, LlmRepository>();
        services.AddScoped<IBotRepository, BotRepository>();
        services.AddScoped<IGameRepository, GameRepository>();

        services.AddScoped<DbRequestsInterceptor>();
        
        services.AddDbContext<ApplicationContext>((sp, options) => {
            options.AddInterceptors(
                sp.GetRequiredService<DbRequestsInterceptor>()
            );
        });
    }

    public static async Task ApplyMigrationsAsync(this IServiceProvider services) {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        await context.Database.MigrateAsync();
    }
}
