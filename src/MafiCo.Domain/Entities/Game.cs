using MafiCo.Domain.ValueObjects;

namespace MafiCo.Domain.Entities;

public class Game {
    private List<Player> _players;

    public Game() {
        _players = new List<Player>();
    }

    public void AddPlayer(Player player) {
        _players.Add(player);
    }

    public void Start() {
        Console.WriteLine("Game started");
    }
}