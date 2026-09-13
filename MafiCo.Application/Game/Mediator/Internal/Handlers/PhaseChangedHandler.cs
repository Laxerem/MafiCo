using MafiCo.Application.Game.Controllers;
using MafiCo.Application.Game.Mediator.Access;
using MafiCo.Application.Game.Mediator.Internal.Events;
using MafiCo.Application.Game.Notifications;
using MafiCo.Application.Interfaces;
using MafiCo.Domain.AggregatesModel.GameAggregate;
using MafiCo.Domain.AggregatesModel.GameAggregate.Items;
using MediatR;
using GameEntity = MafiCo.Domain.AggregatesModel.GameAggregate.Game;

namespace MafiCo.Application.Game.Mediator.Internal.Handlers;

internal class PhaseChangedHandler : INotificationHandler<PhaseChangedEvent> {
    private readonly GameContext _context;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    
    public PhaseChangedHandler(GameContext context, IMediator mediator, IUnitOfWork unitOfWork) {
        _context = context;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(PhaseChangedEvent evt, CancellationToken cancellationToken) {
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);
        await _mediator.DispatchGameEvents(_context.GetGame());
        switch (evt.Phase) {
            case GamePhase.Day:
                foreach (var processor in _context.Processors.Values) {
                    processor.Run();
                }
                GiveControllerToAll(id => new DefaultController(id, new PlayerSender(_mediator)));
                await _context.SendNotify(new PhaseChangedNotification(evt.Phase));
                await Task.Delay(5000, cancellationToken);
                
                GiveControllerToAll(id => new VoterController(id, new PlayerSender(_mediator)));
                await Task.Delay(5000, cancellationToken);
                break;
            case GamePhase.Night:
                GiveControllerToAll(id => null!);
                await _context.SendNotify(new PhaseChangedNotification(evt.Phase));
                var game = _context.GetGame();
                StunRole(game, Role.Citizen);
                GiveControllerToRole(Role.Mafia, id => new VoterController(id, new PlayerSender(_mediator)));
                await Task.Delay(10000, cancellationToken);
                break;
        }
    }

    private void StunRole(GameEntity game, Role role) {
        foreach (var pair in _context.Processors) {
            var processor = pair.Value;
            var playerRole = game.CheckRole(pair.Key);
            if (playerRole == role) {
                processor.Stop();
            }
        }
    }

    private void GiveControllerToRole(Role role, Func<Guid, IPlayerController> factory) {
        var game = _context.GetGame();
        foreach (var pair in _context.Processors) {
            var processor = pair.Value;
            var playerRole = game.CheckRole(pair.Key);
            if (playerRole == role) {
                processor.SetController(factory(pair.Key));
            }
        }
    }

    private void GiveControllerToAll(Func<Guid, IPlayerController?> factory) {
        foreach (var pair in _context.Processors) {
            var processor = pair.Value;
            processor.SetController(factory(pair.Key));
        }
    }
}