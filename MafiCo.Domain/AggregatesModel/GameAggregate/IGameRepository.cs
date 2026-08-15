namespace MafiCo.Domain.AggregatesModel.GameAggregate;

public interface IGameRepository {
    Game Add(Game game);
}