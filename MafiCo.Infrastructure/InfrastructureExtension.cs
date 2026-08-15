using MafiCo.Domain.AggregatesModel.GameAggregate;
using MafiCo.Domain.AggregatesModel.LlmBotAggregate;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Infrastructure.Interfaces;
using MafiCo.Infrastructure.Persistence;
using MafiCo.Infrastructure.Persistence.Repositories;
using MafiCo.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MafiCo.Infrastructure;

public static class InfrastructureExtension {
    public static void AddInfrastructure(this IServiceCollection services) {
        services.AddDbContext<ApplicationContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<ILlmEntityRepository, LlmEntityRepository>();
        services.AddScoped<IBotRepository, BotRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        
        services.AddScoped<ProfileService>();
        services.AddScoped<LlmService>();
        services.AddScoped<GameService>();
        services.AddScoped<BotService>();
        services.AddMediatR(conf =>
            conf.RegisterServicesFromAssembly(typeof(InfrastructureExtension).Assembly)
        );
    }
}
