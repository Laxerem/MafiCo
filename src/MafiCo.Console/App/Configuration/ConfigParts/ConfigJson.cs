using MafiCo.Infrastracture.Configuration;

namespace MafiCo.Console.App.Configuration.ConfigParts;

public class ConfigJson {
    public UserConfig User { get; set; }
    public List<BotConfig> Bots { get; set; }
}