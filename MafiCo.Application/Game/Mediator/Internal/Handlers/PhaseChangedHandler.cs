using MafiCo.Application.Game.Controllers;
using MafiCo.Application.Game.Mediator.Access;
using MafiCo.Application.Game.Mediator.Internal.Events;
using MafiCo.Application.Game.Notifications;
using MafiCo.Application.Interfaces;
using MafiCo.Domain.AggregatesModel.GameAggregate;
using MafiCo.Domain.AggregatesModel.GameAggregate.Items;
using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MediatR;
using GameEntity = MafiCo.Domain.AggregatesModel.GameAggregate.Game;

namespace MafiCo.Application.Game.Mediator.Internal.Handlers;

internal class PhaseChangedHandler : INotificationHandler<PhaseChangedEvent> {
    private readonly GameContext _context;
    private readonly IMediator _mediator;
    
    public PhaseChangedHandler(GameContext context, IMediator mediator) {
        _context = context;
        _mediator = mediator;
    }
    
    public async Task Handle(PhaseChangedEvent evt, CancellationToken cancellationToken) {
        var gameSession = _context.Session!;

        foreach (var playerId in evt.ActivePlayers) {
            var processors = gameSession.GetProcessors();
            foreach (var processor in processors) {
                if (processor.Id != playerId.Id) {
                    processor.Stop();
                    continue;
                }
                processor.Run();
                if (gameSession.Game.Phase is GamePhase.Night) {
                    await processor.SendNotify(new ControllerChangedNotification(
                        new VoterController(playerId, new PlayerSender(_mediator)))
                    );
                }
                else {
                    await processor.SendNotify(new ControllerChangedNotification(
                        new DefaultController(playerId, new PlayerSender(_mediator))
                    ));
                }
            }
        }
        await gameSession.HandleAsync(new PhaseChangedNotification(evt.Phase));
    }
}