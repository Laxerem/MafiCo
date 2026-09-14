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
        var gameSession = _context.Session!;
        
        await _mediator.DispatchGameEvents(gameSession.Game);
        switch (evt.Phase) {
            case GamePhase.Day:
                foreach (var processor in gameSession.GetProcessors()) {
                    processor.Run();
                }
                await GiveControllerToAll(gameSession, id => new DefaultController(id, new PlayerSender(_mediator)));
                await gameSession.HandleAsync(new PhaseChangedNotification(evt.Phase));
                await Task.Delay(5000, cancellationToken);
                
                await GiveControllerToAll(gameSession, id => new VoterController(id, new PlayerSender(_mediator)));
                await Task.Delay(5000, cancellationToken);
                break;
            case GamePhase.Night:
                await GiveControllerToAll(gameSession, id => null!);
                await gameSession.HandleAsync(new PhaseChangedNotification(evt.Phase));
                StunRole(gameSession, Role.Citizen);
                await GiveControllerToRole(gameSession, Role.Mafia, id => new VoterController(id, new PlayerSender(_mediator)));
                await Task.Delay(2000, cancellationToken);
                break;
        }
    }

    private void StunRole(GameSession session, Role role) {
        foreach (var processor in session.GetProcessors()) {
            var playerRole = session.Game.CheckRole(processor.Id);
            if (playerRole == role) {
                processor.Stop();
            }
        }
    }

    private async Task GiveControllerToRole(GameSession session, Role role, Func<Guid, IPlayerController> factory) {
        foreach (var processor in session.GetProcessors()) {
            var playerRole = session.Game.CheckRole(processor.Id);
            if (playerRole == role) {
                await processor.SendNotify(new ControllerChangedNotification(factory(processor.Id)));
            }
        }
    }

    private async Task GiveControllerToAll(GameSession session, Func<Guid, IPlayerController?> factory) {
        foreach (var processor in session.GetProcessors()) {
            await processor.SendNotify(new  ControllerChangedNotification(factory(processor.Id)));
        }
    }
}