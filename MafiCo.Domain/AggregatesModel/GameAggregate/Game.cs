using MafiCo.Domain.AggregatesModel.GameAggregate.Events;
using MafiCo.Domain.DTOs;
using MafiCo.Domain.Interfaces;
using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.AggregatesModel.GameAggregate;

public class Game : Entity, IAggregateRoot {
    public DateTime StartedAt { get; private set; }
    public DateTime? FinishedAt { get; private set; }

    private readonly HashSet<Guid> _playerIds;
    private readonly Dictionary<Guid, Player> _activePlayers = new();
    private readonly Dictionary<Guid, Player> _deadPlayers = new();
    private Voting? _voting;
    private GameStatus _status;
    private GamePhase _phase;

    private Game() : base(Guid.NewGuid()) {
        _playerIds = new HashSet<Guid>();
    }

    public Game(HashSet<Guid> playerIds) : base(Guid.NewGuid()) {
        _playerIds = playerIds;
        _status = GameStatus.Setting;
    }

    public void AssignRoles(int mafiaCount) {
        if (_status != GameStatus.Setting) {
            throw new DomainException("Roles are already assigned");
        }
        if (_playerIds.Count <= 3) {
            throw new DomainException("Players count must be 4 or more players.");
        }
        if (mafiaCount >= _playerIds.Count) {
            throw new DomainException("Mafia players count exceeds or equals player count");
        }

        var random = new Random();
        var shuffled = _playerIds.OrderBy(_ => random.Next()).ToList();

        for (var i = 0; i < shuffled.Count; i++) {
            var player = new Player(shuffled[i]);
            player.AssignRole(i < mafiaCount ? Role.Mafia : Role.Citizen);
            _activePlayers.Add(player.Id, player);
        }

        _status = GameStatus.Running;
        _phase = GamePhase.Voting;
        _voting = new Voting();
        StartedAt = DateTime.UtcNow;
    }

    public void MakeVote(Guid playerId, Guid targetId) {
        EnsureRunning();
        if (_phase != GamePhase.Voting) {
            throw new DomainException("Voting is not the current phase");
        }
        if (!IsAlive(playerId)) throw new DomainException("Voter is not an active player");
        if (!IsAlive(targetId)) throw new DomainException("Target is not an active player");

        _voting!.AddVote(playerId, targetId);
    }

    public void NextPhase() {
        EnsureRunning();

        if (_phase == GamePhase.Voting) {
            ResolveVoting();
            if (TryFinish()) return;
            _phase = GamePhase.Discussion;
        }
        else {
            _phase = GamePhase.Voting;
            _voting = new Voting();
        }
    }

    public Role CheckRole(Guid playerId) {
        if (_activePlayers.TryGetValue(playerId, out var player) ||
            _deadPlayers.TryGetValue(playerId, out player)) {
            return player.Role ?? throw new DomainException("Player role is not assigned");
        }

        throw new DomainException("Player doesn't exist");
    }

    public IReadOnlyCollection<Guid> GetAllPlayers() => _playerIds;

    private void ResolveVoting() {
        var targetId = _voting!.FinishAndGetResult();
        if (targetId is null) return;

        var victim = _activePlayers[targetId.Value];
        _activePlayers.Remove(victim.Id);
        _deadPlayers.Add(victim.Id, victim);

        AddNotification(new PlayerKilledEvent(victim.Id, victim.Role!.Value));
    }

    private bool TryFinish() {
        var mafiaAlive = _activePlayers.Values.Count(player => player.Role == Role.Mafia);
        var citizensAlive = _activePlayers.Count - mafiaAlive;

        if (mafiaAlive != 0 && mafiaAlive < citizensAlive) {
            return false;
        }

        Finish(mafiaAlive == 0 ? Role.Citizen : Role.Mafia);
        return true;
    }

    private void Finish(Role winningSide) {
        var winners = new List<PlayerInfo>();
        var losers = new List<PlayerInfo>();

        foreach (var player in _activePlayers.Values.Concat(_deadPlayers.Values)) {
            var info = new PlayerInfo(player.Id, player.Role!.Value);
            if (player.Role == winningSide) {
                winners.Add(info);
            }
            else {
                losers.Add(info);
            }
        }

        AddNotification(new GameFinishedEvent(winners, losers));

        _activePlayers.Clear();
        _deadPlayers.Clear();
        _voting = null;
        _status = GameStatus.Finished;
        FinishedAt = DateTime.UtcNow;
    }

    private void EnsureRunning() {
        if (_status != GameStatus.Running) {
            throw new DomainException("Game is not running");
        }
    }

    private bool IsAlive(Guid id) => _activePlayers.ContainsKey(id);
}
