using MafiCo.Domain.Abstractions;
using MafiCo.Domain.Entities;
using MafiCo.Domain.Events;
using MafiCo.Domain.Exceptions;
using MafiCo.Domain.ValueObjects;

namespace MafiCo.Domain.Agregate;

public class Game {
    private Dictionary<string, Player> _players;
    private DayPhase _currentPhase;
    private readonly Random _random;

    public Game() {
        _players = new Dictionary<string, Player>();
        _currentPhase = DayPhase.Day;
        _random = new Random();
    }

    private void DetermineRoles(List<string> playerNames) {
        _players.Clear();

        var shuffled = playerNames.OrderBy(_ => _random.Next()).ToList();
        int mafiaCount = 1;

        for (int i = 0; i < shuffled.Count; i++) {
            string name = shuffled[i];
            Player player;

            if (i < mafiaCount) {
                player = new Mafia(name, Role.Mafia);
            } else {
                player = new Citizen(name, Role.Citizen);
            }

            _players[name] = player;
        }
    }

    private void ProcessDayPhase() {
        if (_currentPhase == DayPhase.Day) {
            
        }
    }
    
    public IEnumerable<RoleIsDetermineEvent> SetupGame(List<string> players) {
        if (players.Count <= 2) {
            throw new GameException("Для игры недостаточно игроков");
        }
        this.DetermineRoles(players);

        var result = _players.Select(x => new RoleIsDetermineEvent(x.Key, x.Value.Role));
        return result;
    }
}