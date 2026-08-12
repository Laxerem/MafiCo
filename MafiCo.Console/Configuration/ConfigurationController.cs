using System.Text.Json;
using MafiCo.Console.Configuration.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace MafiCo.Console.Configuration;

public class ConfigurationController {
    private readonly string _path;
    private GlobalConfigOption _configuration;
    
    public ConfigurationController(string path, IOptions<GlobalConfigOption> configuration) {
        _path = path;
        _configuration = configuration.Value;
        
        if (!File.Exists(path)) throw new FileNotFoundException($"File {path} does not exist");
    }

    public void SetUserOptions(UserOptions options) {
        _configuration.User = options;
        File.WriteAllText(_path, JsonSerializer.Serialize(_configuration));
    }

    public UserOptions GetUserOptions() {
        return _configuration.User;
    }
}