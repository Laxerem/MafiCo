using System.Text.Json;
using DotNetEnv;
using MafiCo.Console.App.Configuration.ConfigParts;

namespace MafiCo.Console.App.Configuration;

public record EnvironmentConfig(string ApiToken);

public class ConfigurationLoader {
    private readonly string _configPath;
    private readonly string _envPath;

    public ConfigurationLoader(string configPath, string envPath) {
        _configPath = configPath;
        _envPath = envPath;
    }

    public AppConfig Load() {
        Env.Load(_envPath);

        var json = File.ReadAllText(_configPath);
        var configJson = JsonSerializer.Deserialize<ConfigJson>(json);
        var apiToken = Env.GetString("API_TOKEN");
        
        if (configJson == null || apiToken == null) {
            throw new Exception("Couldn't load config");
        }
        
        return new AppConfig(configJson,  new EnvironmentConfig(apiToken));
    }
    
}