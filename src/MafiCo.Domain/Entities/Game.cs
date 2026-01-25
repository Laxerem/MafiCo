using MafiCo.Domain.Abstractions;
using MafiCo.Domain.Exceptions;
using MafiCo.Domain.ValueObjects;

namespace MafiCo.Domain.Entities;

public class Game {
    private List<Player> _players;
    private DayPhase _currentPhase;

    public Game() {
        _players = new List<Player>();
    }

    public void AddPlayer(Player player) {
        _players.Add(player);
    }

    public void Start() {
        if (_players.Count <= 2) {
            throw new GameException("Для игры недостаточно игроков");
        }
        
    }
}