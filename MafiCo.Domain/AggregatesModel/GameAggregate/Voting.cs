using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;
using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.AggregatesModel.GameAggregate;

public class Voting {
    private bool _isActive;
    private readonly Dictionary<PlayerId, PlayerId> _votes;

    public Voting() {
        _votes = new Dictionary<PlayerId, PlayerId>();
        _isActive = true;
    }

    public void AddVote(PlayerId voterId, PlayerId targetId) {
        if (!_isActive) throw new DomainException("Voting is not active");
        if (voterId == targetId) throw new DomainException("Voter cannot vote for themselves");
        if (_votes.ContainsKey(voterId)) throw new DomainException($"Voter #{voterId} already voted");

        _votes.Add(voterId, targetId);
    }

    public void RemoveVote(PlayerId voterId) {
        if (!_isActive) throw new DomainException("Voting is not active");
        if (!_votes.Remove(voterId)) throw new DomainException($"Voter #{voterId} has not voted yet");
    }

    public PlayerId? FinishAndGetResult() {
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
