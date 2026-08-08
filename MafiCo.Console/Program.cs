using MafiCo.Console.Presentation;
using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Windows.Lobby;
using MafiCo.Domain.AggregatesModel.LlmBotAggregate;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Infrastructure;
using MafiCo.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var app = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => {
        services.AddDbContext<ApplicationContext>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<ILlmBotRepository, LlmBotRepository>();
        services.AddMediatR(conf => 
            conf.RegisterServicesFromAssembly(typeof(Program).Assembly)
        );
        services.AddTransient<Window, MenuWindow>();
        services.AddTransient<MenuWindow>();
        services.AddTransient<SettingsWindow>();
        services.AddTransient<CreateProfileWindow>();
        services.AddScoped<UserInterface>();
    })
    .Build();

using var scope = app.Services.CreateScope();
var userInterface = scope.ServiceProvider.GetRequiredService<UserInterface>();
await userInterface.StartRetention();