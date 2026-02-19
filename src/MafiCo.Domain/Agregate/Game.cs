using MafiCo.Domain.Abstractions;
using MafiCo.Domain.Entities;
using MafiCo.Domain.Events;
using MafiCo.Domain.Exceptions;
using MafiCo.Domain.Interfaces;
using MafiCo.Domain.ValueObjects;

namespace MafiCo.Domain.Agregate;

public class Game {
    public List<IDomainEvent> Events = new List<IDomainEvent>();
    
    private Dictionary<string, Player> _players;
    private DayPhase _currentPhase;
    private Chat _chat;
    
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
                Events.Add(new RoleDeterminateEvent(player.Name, Role.Mafia));
            } else {
                player = new Citizen(name, Role.Citizen);
                Events.Add(new RoleDeterminateEvent(player.Name, Role.Citizen));
            }
            
            _players[name] = player;
        }
    }

    private async Task<string?> StartVoting() {
        throw new NotImplementedException();
    }

    private async Task ProcessDayPhase() {
        Console.WriteLine("Processing Day Phase");
        if (_currentPhase == DayPhase.Day) {
            var playerName = await StartVoting();
            if (playerName != null) {
                var player = _players[playerName];
                player.Kill();
            }
        }
    }
    
    public void SetupGame(List<string> players) {
        if (players.Count <= 2) {
            throw new GameException("Для игры недостаточно игроков");
        }
        this.DetermineRoles(players);
    }
}