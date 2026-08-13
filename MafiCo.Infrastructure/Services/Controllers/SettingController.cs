using MafiCo.Domain.AggregatesModel.GameAggregate;
using MafiCo.Infrastructure.Interfaces;

namespace MafiCo.Infrastructure.Services.Controllers;

public class SettingController : IController {
    private List<Guid> _playerIds;
    private Game _game;
    private int _mafiaCount = 1;
    
    public SettingController(List<Guid> playersIds, Game game) {
        _playerIds = playersIds;
        _game = game;
    }

    public void SetupMafiaCount(int mafiaCount) {
        _mafiaCount = mafiaCount;
    }

    public void StartGame() {
        _game.Setup(_playerIds, _mafiaCount);
    }
}