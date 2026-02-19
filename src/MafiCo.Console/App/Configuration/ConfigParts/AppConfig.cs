using MafiCo.Infrastracture.Configuration;

namespace MafiCo.Console.App.Configuration.ConfigParts;

public record AppConfig {
    public EnvironmentConfig Environment { get; init; }
    public List<BotConfig> Bots { get; init; } = new();
    public UserConfig User { get; init; }
}

public record EnvironmentConfig(string ApiToken);