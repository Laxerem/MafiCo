using MafiCo.Application.Game;
using MafiCo.Application.Interfaces.Game;

namespace MafiCo.Application.Bot;

public class BotProcessor {
    private readonly IPlayerSession _session;
    
    public BotProcessor(PlayerSession session) {
        _session = session;
    }

    public async Task RunAsync() {
        await foreach (var evt in _session.EventsReader.ReadAllAsync()) {
            
        }
    }
}