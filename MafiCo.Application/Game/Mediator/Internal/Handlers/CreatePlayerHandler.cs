using MafiCo.Application.Game.Mediator.Internal.Commands;
using MafiCo.Application.Interfaces.Game;
using MafiCo.Domain.AggregatesModel.BotAggregate;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MediatR;

namespace MafiCo.Application.Game.Mediator.Internal.Handlers;

internal class CreatePlayerHandler : IRequestHandler<CreatePlayerCommand, PlayerProcessor> {
    private readonly GameContext _context;
    private readonly IBotRepository _botRepository;
    private readonly IProfileRepository _profileRepository;
    
    public CreatePlayerHandler(GameContext context, IBotRepository botRepository, IProfileRepository profileRepository) {
        _context = context;
        _botRepository = botRepository;
        _profileRepository = profileRepository;
    }
    
    public Task<PlayerProcessor> Handle(CreatePlayerCommand request, CancellationToken cancellationToken) {
        if (!_profileRepository.Exists(new ProfileId(request.PlayerId.Id))) {
            throw new ApplicationException($"Profile with id {request.PlayerId} does not exist");
        }
        // bool isBot = await _botRepository.ExistsByProfileIdAsync(profileId);
        // if (isBot) {
        //     return new BotProcessor();
        // }
        // return new UserProcessor();
        
        return Task.FromResult(new PlayerProcessor(request.PlayerId, _context.Session!));
    }
}