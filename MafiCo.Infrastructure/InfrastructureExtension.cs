using MafiCo.Domain.AggregatesModel.BotAggregate;
using MafiCo.Domain.AggregatesModel.GameAggregate;
using MafiCo.Domain.AggregatesModel.LlmAggregate;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Infrastructure.Interfaces;
using MafiCo.Infrastructure.Persistence;
using MafiCo.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MafiCo.Infrastructure;

public static class InfrastructureExtension {
    public static void AddInfrastructure(this IServiceCollection services) {
        services.AddDbContext<ApplicationContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<ILlmRepository, LlmRepository>();
        services.AddScoped<IBotRepository, BotRepository>();
        services.AddScoped<IGameRepository, GameRepository>();

        services.AddMediatR(conf =>
            conf.RegisterServicesFromAssembly(typeof(InfrastructureExtension).Assembly)
        );
    }
}
