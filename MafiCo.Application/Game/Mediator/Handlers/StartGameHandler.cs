using MafiCo.Application.Game.Commands;
using MafiCo.Application.Game.Controllers;
using MafiCo.Application.Interfaces.Stores;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MediatR;
using GameAggregate = MafiCo.Domain.AggregatesModel.GameAggregate.Game;

namespace MafiCo.Application.Game.Mediator.Handlers;

public class StartGameHandler : IRequestHandler<StartGameCommand, PlayerView> {
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
    
    public async Task<PlayerView> Handle(StartGameCommand request, CancellationToken cancellationToken) {
        var userId = _store.GetUserId();
        if (!userId.HasValue) throw new ApplicationException("UserId does not exist");
        
        var allProfiles = await _profileRepository.GetAllAsync();
        var profileIds = allProfiles.Select(p => p.Id).ToHashSet();

        var game = new GameAggregate(profileIds);
        await _gameContext.InitializeAsync(game, _mediator);

        foreach (var pair in _gameContext.Processors) {
            var processorId = pair.Key;
            var processor = pair.Value;
            processor.Run();
            processor.SetController(new DefaultController(processorId, new PlayerSender(_mediator)));
        }
        
        return _gameContext.GetPlayerView(userId.Value);
    }
}