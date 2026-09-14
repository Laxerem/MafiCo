using MafiCo.Application.Game.Commands;
using MafiCo.Application.Game.Mediator.Commands;
using MafiCo.Application.Game.Notifications;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MediatR;

namespace MafiCo.Application.Game.Mediator.Handlers;

public class MakeVoteHandler : IRequestHandler<MakeVoteCommand> {
    private readonly GameContext _context;
    private readonly IProfileRepository _profileRepository;

    public MakeVoteHandler(GameContext context, IProfileRepository profileRepository) {
        _context = context;
        _profileRepository = profileRepository;
    }
    
    public async Task Handle(MakeVoteCommand request, CancellationToken cancellationToken) {
        var gameSession = _context.Session!;
        gameSession.Game.MakeVote(request.PlayerId, request.TargetId);

        var voterProfile = await _profileRepository.GetAsync(request.PlayerId)!;
        var targetProfile = await _profileRepository.GetAsync(request.TargetId)!;
        
        await gameSession.HandleAsync(new PlayerVotedNotification(voterProfile!.Name, targetProfile!.Name));
    }
}