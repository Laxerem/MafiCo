using MafiCo.Domain.AggregatesModel.GameAggregate;
using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;

namespace MafiCo.Infrastructure.Persistence.Repositories;

public class GameRepository : IGameRepository {
    private readonly ApplicationContext _context;
    
    public GameRepository(ApplicationContext context) {
        _context = context;
    }
    
    public Game Add(Game game) {
        return _context.Games.Add(game).Entity!;
    }

    public async Task<Game?> GetAsync(GameId id) {
        return await _context.Games.FindAsync(id);
    }
}