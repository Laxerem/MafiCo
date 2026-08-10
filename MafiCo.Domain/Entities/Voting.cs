using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.Entities;

public class Voting : Entity {
    private bool _isActive;
    private readonly Dictionary<Guid, Guid> _votes;

    public Voting() : base(Guid.NewGuid()) {
        _votes = new Dictionary<Guid, Guid>();
        _isActive = true;
    }

    public void AddVote(Guid voterId, Guid targetId) {
        if (!_isActive) throw new DomainException("Voting is not active");
        if (_votes.ContainsKey(voterId)) throw new DomainException($"Voter #{voterId} already voted");

        _votes.Add(voterId, targetId);
    }

    public void RemoveVote(Guid voterId) {
        if (!_isActive) throw new DomainException("Voting is not active");
        if (!_votes.Remove(voterId)) throw new DomainException($"Voter #{voterId} has not voted yet");
    }

    public Guid? FinishAndGetResult() {
        if (!_isActive) throw new DomainException("Voting is not active");
        _isActive = false;

        if (_votes.Count == 0) return null;

        var results = _votes.Values
            .GroupBy(targetId => targetId)
            .Select(group => (TargetId: group.Key, Count: group.Count()))
            .OrderByDescending(result => result.Count)
            .ToList();

        var isTie = results.Count > 1 && results[0].Count == results[1].Count;
        return isTie ? null : results[0].TargetId;
    }
}
