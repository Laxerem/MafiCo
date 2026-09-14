using System.Xml;
using MafiCo.Application.Game.Commands;
using MafiCo.Application.Game.Controllers;
using MafiCo.Application.Game.Mediator.Access;
using MafiCo.Application.Game.Mediator.Commands;
using MafiCo.Application.Game.Mediator.Internal.Commands;
using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Game;
using MafiCo.Application.Interfaces.Stores;
using MafiCo.Domain.AggregatesModel.GameAggregate;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MediatR;
using GameAggregate = MafiCo.Domain.AggregatesModel.GameAggregate.Game;

namespace MafiCo.Application.Game.Mediator.Handlers;

public class StartGameHandler : IRequestHandler<StartGameCommand, IPlayerSession> {
    private readonly IMediator _mediator;
    private readonly IProfileRepository _profileRepository;
    private readonly IGameRepository _gameRepository;
    private readonly GameContext _gameContext;
    private readonly IUserStore _store;
    private readonly IUnitOfWork _unitOfWork;
    
    public StartGameHandler(IMediator mediator, GameContext gameContext, IProfileRepository profileRepository, IGameRepository gameRepository, 
        IUserStore store, IUnitOfWork unitOfWork) {
        _mediator = mediator;
        _gameContext = gameContext;
        _profileRepository = profileRepository;
        _gameRepository = gameRepository;
        _store = store;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IPlayerSession> Handle(StartGameCommand request, CancellationToken cancellationToken) {
        var userId = _store.GetUserId();
        if (!userId.HasValue) throw new ApplicationException("UserId does not exist");
        
        var allProfiles = await _profileRepository.GetAllAsync();
        var profileIds = allProfiles.Select(p => p.Id).ToHashSet();

        var game = new GameAggregate(profileIds);
        game = _gameRepository.Add(game);
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);
        
        var session = new GameSession(game);
        _gameContext.SetSession(session);
        
        await session.StartAsync(request.MafiaCount, _mediator);
        return session.GetPlayerSession(userId.Value);
    }
}