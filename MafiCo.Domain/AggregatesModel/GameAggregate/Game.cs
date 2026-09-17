using MafiCo.Domain.AggregatesModel.GameAggregate.Entities;
using MafiCo.Domain.AggregatesModel.GameAggregate.Events;
using MafiCo.Domain.AggregatesModel.GameAggregate.Items;
using MafiCo.Domain.DTOs;
using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.AggregatesModel.GameAggregate;

public class Game : AggregateRoot {
    public DateTime StartedAt { get; private set; }
    public DateTime? FinishedAt { get; private set; }

    private readonly Dictionary<Guid, Player> _players;
    private Voting? _voting;
    private GameStatus _status;
    public GamePhase Phase { get; private set; }

    private Game() : base(Guid.NewGuid()) {
        _players = new Dictionary<Guid, Player>();
    }

    public Game(HashSet<Guid> playerIds) : base(Guid.NewGuid()) {
        _players = playerIds.ToDictionary(id => id, id => new Player(id));
        _status = GameStatus.Setting;
    }

    public void AssignRoles(int mafiaCount) {
        if (_status != GameStatus.Setting) {
            throw new DomainException("Roles are already assigned");
        }
        if (_players.Count <= 3) {
            throw new DomainException("Players count must be 4 or more players.");
        }
        if (mafiaCount >= _players.Count) {
            throw new DomainException("Mafia players count exceeds or equals player count");
        }

        var random = new Random();
        var shuffled = _players.Keys.OrderBy(_ => random.Next()).ToList();

        for (var i = 0; i < shuffled.Count; i++) {
            _players[shuffled[i]].AssignRole(i < mafiaCount ? Role.Mafia : Role.Citizen);
        }

        _status = GameStatus.Running;
        Phase = GamePhase.Day;
        _voting = new Voting();
        StartedAt = DateTime.UtcNow;
    }

    public void MakeVote(Guid playerId, Guid targetId) {
        EnsureRunning();
        // if (Phase != GamePhase.Day) {
        //     throw new DomainException("Voting is not the current phase");
        // }
        if (!IsAlive(playerId)) throw new DomainException("Voter is not an active player");
        if (!IsAlive(targetId)) throw new DomainException("Target is not an active player");

        _voting!.AddVote(playerId, targetId);
    }

    public void NextPhase() {
        if (_status == GameStatus.Finished) {
            throw new DomainException("Game has ended");
        }
        EnsureRunning();
        ResolveVoting();
        if (TryFinish()) return;
        
        if (Phase == GamePhase.Day) {
            Phase = GamePhase.Night;
        }
        else {
            Phase = GamePhase.Day;
        }
        _voting = new Voting();
    }

    public Role CheckRole(Guid playerId) {
        if (_players.TryGetValue(playerId, out var player)) {
            return player.Role ?? throw new DomainException("Player role is not assigned");
        }

        throw new DomainException("Player doesn't exist");
    }

    public IReadOnlyCollection<Guid> GetAllPlayers() => _players.Keys;

    private void ResolveVoting() {
        var targetId = _voting!.FinishAndGetResult();
        if (targetId is null) return;

        var victim = _players[targetId.Value];
        victim.Kill();

        AddNotification(new PlayerKilledDomainEvent(victim.Id, victim.Role!.Value));
    }

    private bool TryFinish() {
        var alivePlayers = _players.Values.Where(player => player.IsAlive).ToList();
        var mafiaAlive = alivePlayers.Count(player => player.Role == Role.Mafia);
        var citizensAlive = alivePlayers.Count - mafiaAlive;

        if (mafiaAlive != 0 && mafiaAlive < citizensAlive) {
            return false;
        }

        Finish(mafiaAlive == 0 ? Role.Citizen : Role.Mafia);
        return true;
    }

    private void Finish(Role winningSide) {
        var winners = new List<PlayerInfo>();
        var losers = new List<PlayerInfo>();

        foreach (var player in _players.Values) {
            var info = new PlayerInfo(player.Id, player.Role!.Value, player.IsAlive);
            if (player.Role == winningSide && player.IsAlive) {
                winners.Add(info);
            }
            else {
                losers.Add(info);
            }
        }

        AddNotification(new GameFinishedEvent(winners, losers));

        _players.Clear();
        _voting = null;
        _status = GameStatus.Finished;
        FinishedAt = DateTime.UtcNow;
    }

    private void EnsureRunning() {
        if (_status != GameStatus.Running) {
            throw new DomainException("Game is not running");
        }
    }

    public bool IsAlive(Guid id) => _players.TryGetValue(id, out var player) && player.IsAlive;
}
