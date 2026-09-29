using MafiCo.Application.Game.Mediator.Commands;
using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Game;
using MafiCo.Application.Interfaces.Stores;
using MafiCo.Domain.AggregatesModel.BotAggregate;
using MafiCo.Domain.AggregatesModel.GameAggregate;
using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;
using MediatR;
using GameAggregate = MafiCo.Domain.AggregatesModel.GameAggregate.Game;

namespace MafiCo.Application.Game.Mediator.Handlers;

public class StartGameHandler : IRequestHandler<StartGameCommand, IPlayerSession> {
    private readonly IMediator _mediator;
    private readonly IBotRepository _botRepository;
    private readonly IGameRepository _gameRepository;
    private readonly GameContext _gameContext;
    private readonly IUserStore _store;
    private readonly IUnitOfWork _unitOfWork;

    public StartGameHandler(IMediator mediator, GameContext gameContext, IBotRepository botRepository, IGameRepository gameRepository,
        IUserStore store, IUnitOfWork unitOfWork) {
        _mediator = mediator;
        _gameContext = gameContext;
        _botRepository = botRepository;
        _gameRepository = gameRepository;
        _store = store;
        _unitOfWork = unitOfWork;
    }

    public async Task<IPlayerSession> Handle(StartGameCommand request, CancellationToken cancellationToken) {
        var userId = _store.GetUserId();
        if (!userId.HasValue) throw new ApplicationException("UserId does not exist");

        var allBots = await _botRepository.GetAllAvailableAsync();
        var profileIds = allBots.Select(p => p.ProfileId.Id).ToHashSet();
        profileIds.Add(userId.Value.Id);

        var game = new GameAggregate(profileIds);
        game = _gameRepository.Add(game);
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        var session = new GameSession(game);
        _gameContext.SetSession(session);

        await session.StartAsync(request.MafiaCount, _mediator); // New Thread
        return session.GetPlayerSession(new PlayerId(userId.Value.Id));
    }
}
