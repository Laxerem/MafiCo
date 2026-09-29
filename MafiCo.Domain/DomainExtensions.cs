using MafiCo.Domain.AggregatesModel.GameAggregate.Entities;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Domain.DTOs;

namespace MafiCo.Domain;

public static class DomainExtensions {
    public static ProfileInfo ToInfo(this Profile profile) {
        return new ProfileInfo(profile.Id, profile.Name, profile.VictoriesCount, profile.DefeatsCount);
    }
    
    public static IEnumerable<PlayerInfo> ToInfo(this IEnumerable<Player> players) {
        return players.Select(x => x.ToInfo());
    }

    public static PlayerInfo ToInfo(this Player player) {
        return new PlayerInfo(player.Id, player.Role!.Value, player.IsAlive);
    }
}