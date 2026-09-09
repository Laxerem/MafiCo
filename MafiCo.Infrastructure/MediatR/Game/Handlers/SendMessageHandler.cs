using MafiCo.Application.Game.Contexts;
using MafiCo.Application.Notifications;
using MafiCo.Application.Notifications.GameNotifications;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Infrastructure.MediatR.Game.Commands;
using MediatR;

namespace MafiCo.Infrastructure.MediatR.Game.Handlers;

public class SendMessageHandler : IRequestHandler<SendMessageCommand> {
    private readonly GameContext _context;
    private readonly IProfileRepository _repository;
    
    public SendMessageHandler(GameContext context, IProfileRepository repository) {
        _context = context;
        _repository = repository;
    }
    
    public async Task Handle(SendMessageCommand request, CancellationToken cancellationToken) {
        var userProfile = await _repository.GetAsync(request.PlayerId) ?? throw new NullReferenceException("Player profile not found");
        await _context.SendEvent(new PlayerMessageNotification(userProfile.Name, request.Message));
    }
}