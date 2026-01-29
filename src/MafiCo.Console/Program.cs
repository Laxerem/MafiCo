using MafiCo.Application;
using MafiCo.Console.App;
using MafiCo.Console.App.Configuration;
using MafiCo.Domain.Entities;
using MafiCo.Infrastracture.Clients;
using MafiCo.Infrastracture.Configuration;

class Program {
    public static async Task Main() {
        var configLoader = new ConfigurationLoader("../../../players.json", "../../../.env");
        var config = configLoader.Load();
        
        var app = new App(config);
        await app.RunAsync();
    }
}