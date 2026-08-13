using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Domain.DTOs;
using MafiCo.Domain.Entities;
using MafiCo.Domain.Entities.Players;
using MafiCo.Domain.Events;
using MafiCo.Domain.Events.Game;
using MafiCo.Domain.Events.Players;
using MafiCo.Domain.Interfaces;
using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.AggregatesModel.GameAggregate;

public class Game : Entity, IAggregateRoot {
    private readonly Dictionary<Guid, Player> _activePlayers;
    private readonly Dictionary<Guid, Player> _deathPlayers;
    private Voting? _voting;
    private GameStatus _status;

    public Game() : base(Guid.NewGuid()) {
        _activePlayers = new Dictionary<Guid, Player>();
        _deathPlayers = new Dictionary<Guid, Player>();
        _status = GameStatus.Setting;
    }

    public void Setup(IEnumerable<Guid> playerIds, int mafiaCount) {
        if (_status != GameStatus.Setting) {
            throw new DomainException("Game is already started");
        }

        var ids = playerIds.ToList();
        var uniquePlayers = new HashSet<Guid>(ids);
        if (uniquePlayers.Count != ids.Count) {
            throw new DomainException("Player list contains duplicate players");
        }
        if (uniquePlayers.Count <= 3) {
            throw new DomainException("Players count must be 4 or more players.");
        }
        if (mafiaCount >= uniquePlayers.Count) {
            throw new DomainException("Mafia players count exceeds or equally player count");
        }

        var random = new Random();
        var shuffled = uniquePlayers.OrderBy(_ => random.Next()).ToList();

        for (int i = 0; i < shuffled.Count; i++) {
            var playerId = shuffled[i];
            Player player = i < mafiaCount ? new Mafia(playerId) : new Citizen(playerId);
            _activePlayers.Add(playerId, player);
        }

        _status = GameStatus.Voting;
        AddNotification(new RolesAssignedEvent());
    }

    public void Vote(Guid voterId, Guid targetId) {
        if (_status != GameStatus.Voting) throw new DomainException("Game already vote");
        if (!IsAlive(voterId)) throw new DomainException("Voter doesn't exist");
        if (!IsAlive(targetId)) throw new DomainException("Target doesn't exist");
        
        _activePlayers[voterId].Vote(targetId);
        AddNotification(new PlayerVotedEvent(voterId, targetId));
    }

    public void Finish() {
        if (_status == GameStatus.Finished) throw new DomainException("Game already finished");
        if (_activePlayers.Any(pair => pair.Value is Mafia)) {
            throw new DomainException("The game have one or more mafia players");
        }
        
        var winners = _activePlayers.Values
            .Select(player => new PlayerInfo(player.Id, player.GetRole()))
            .ToList();
        var losers = _deathPlayers.Values
            .Select(player => new PlayerInfo(player.Id, player.GetRole()))
            .ToList();

        AddNotification(new GameFinishedEvent(winners, losers));

        _activePlayers.Clear();
        _deathPlayers.Clear();
        _status = GameStatus.Finished;
    }

    private bool IsAlive(Guid id) {
        if (_activePlayers.ContainsKey(id)) return true;
        if (_deathPlayers.ContainsKey(id)) return false;
        throw new DomainException("Player doesn't exists");
    }
}