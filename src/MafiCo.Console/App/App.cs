using System.Xml.Resolvers;
using MafiCo.Application;
using MafiCo.Application.Interfaces;
using MafiCo.Application.Services;
using MafiCo.Console.App.Configuration.ConfigParts;
using MafiCo.Console.App.UI;
using MafiCo.Domain.Entities;
using MafiCo.Domain.Exceptions;
using MafiCo.Infrastracture.Brains;
using MafiCo.Infrastracture.Clients;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace MafiCo.Console.App;

public class App {
    private readonly AppConfig _config;
    public App(AppConfig config) {
        _config = config;
    }

    public async Task RunAsync() {
        var openRouterClient = new OpenRouterClient(new HttpClient(), _config.Environment.ApiToken);
        var bots = _config.Bots.Select(bot => (bot.Username,(IPlayerBrain) new BotBrain(openRouterClient, bot.SystemPrompt, bot.ModelName)));
        var user = (_config.User.Username, new HumanBrain());
        
        var players = new List<(string name, IPlayerBrain brain)>();
        players.AddRange(bots);
        players.Add(user);
        
        var gameService = new GameService(players);
        var ui = new UserInterface(gameService, user.Item2);
        await ui.StartRetention();
    }
}