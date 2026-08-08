using MafiCo.Domain.Entities;
using MafiCo.Domain.Entities.Players;
using MafiCo.Domain.Interfaces;
using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.AggregatesModel.GameAggregate;

public class Game : Entity, IAggregateRoot {
    private readonly List<Player> _players = new();
    
    public Game() {
        
    }
}