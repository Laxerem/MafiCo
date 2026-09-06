using MafiCo.Application.Game;
using MafiCo.Application.Game.Contexts;
using MafiCo.Application.Interfaces.Stores;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Infrastructure.MediatR.Game.Commands;
using MediatR;
using GameAggregate = MafiCo.Domain.AggregatesModel.GameAggregate.Game;

namespace MafiCo.Infrastructure.MediatR.Game.Handlers;

public class StartGameHandler : IRequestHandler<StartGameCommand, PlayerContext> {
    private readonly IMediator _mediator;
    private readonly IProfileRepository _profileRepository;
    private readonly GameContext _gameContext;
    private readonly IUserStore _store;
    
    public StartGameHandler(IMediator mediator, GameContext gameContext, IProfileRepository profileRepository, IUserStore store) {
        _mediator = mediator;
        _gameContext = gameContext;
        _profileRepository = profileRepository;
        _store = store;
    }
    
    public async Task<PlayerContext> Handle(StartGameCommand request, CancellationToken cancellationToken) {
        var allProfiles = await _profileRepository.GetAllAsync();
        var profileIds = allProfiles.Select(p => p.Id).ToHashSet();

        var game = new GameAggregate(profileIds);
        
        var gameOrchestrator = new GameOrchestrator(_gameContext, _mediator);
        await gameOrchestrator.Initialize(profileIds);
        _gameContext.Initialize(game, gameOrchestrator);
        
        await gameOrchestrator.StartAsync(request.MafiaCount);
        
        var userId = _store.GetUserId();
        return gameOrchestrator.GetPlayerContext(userId!.Value);
    }
}