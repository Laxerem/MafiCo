using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;

namespace MafiCo.Domain.AggregatesModel.GameAggregate;

public interface IGameRepository {
    Game Add(Game game);
    Task<Game?> GetAsync(GameId id);
}