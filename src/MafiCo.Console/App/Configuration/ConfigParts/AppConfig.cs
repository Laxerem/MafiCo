using MafiCo.Infrastracture.Configuration;

namespace MafiCo.Console.App.Configuration.ConfigParts;

public class AppConfig {
    public EnvironmentConfig Environment { get; private set; }
    public List<BotConfig> Bots { get; private set; }
    public UserConfig User { get; private set; }

    public AppConfig(ConfigJson json, EnvironmentConfig env) {
        Bots = json.Bots;
        User = json.User;
        Environment = env;
    }
}