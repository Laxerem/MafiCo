using MafiCo.Application.Game.Commands;
using MafiCo.Application.Game.Notifications;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MediatR;

namespace MafiCo.Application.Game.Mediator.Handlers;

public class SendMessageHandler : IRequestHandler<SendMessageCommand> {
    private readonly GameContext _context;
    private readonly IProfileRepository _repository;
    
    public SendMessageHandler(GameContext context, IProfileRepository repository) {
        _context = context;
        _repository = repository;
    }
    
    public async Task Handle(SendMessageCommand request, CancellationToken cancellationToken) {
        var userProfile = await _repository.GetAsync(request.PlayerId) ?? throw new NullReferenceException("Player profile not found");
        await _context.Session!.HandleAsync(new PlayerMessageNotification(userProfile.Name, request.Message));
    }
}