using MafiCo.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MafiCo.Infrastructure;

public static class InfrastructureExtension {
    public static void AddInfrastructure(this IServiceCollection services) {
        services.AddScoped<ProfileService>();
        services.AddScoped<LlmService>();
        services.AddScoped<GameService>();
        services.AddScoped<BotService>();
        services.AddMediatR(conf =>
            conf.RegisterServicesFromAssembly(typeof(InfrastructureExtension).Assembly)
        );
    }
}
