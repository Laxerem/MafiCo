using System.Reflection;
using MafiCo.Console.Configuration.Options;
using MafiCo.Console.Presentation;
using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Windows.Lobby.UseCases;
using MafiCo.Domain.AggregatesModel.LlmBotAggregate;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Infrastructure;
using MafiCo.Infrastructure.Interfaces;
using MafiCo.Infrastructure.Persistence;
using MafiCo.Infrastructure.Persistence.Repositories;
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

        services.AddScoped<CreateProfile>();
        services.AddScoped<ChangeName>();
        services.AddScoped<CreateLlm>();
        services.AddScoped<StartGame>();
        services.AddScoped<GetBots>();
        services.AddScoped<GetLlmModels>();
        services.AddScoped<CreateBot>();
        services.AddScoped<DeleteBot>();
        services.AddScoped<ChangeBotModel>();

        return services;
    }

    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration) {
        services.Configure<UserOptions>(configuration.GetSection("User"));
        services.Configure<GlobalConfigOption>(configuration);
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services) {
        services.AddDbContext<ApplicationContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<ILlmEntityRepository, LlmEntityRepository>();
        services.AddScoped<IBotRepository, BotRepository>();
        return services;
    }
}