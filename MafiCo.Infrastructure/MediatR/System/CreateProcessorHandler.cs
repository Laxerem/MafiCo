using MafiCo.Application.Game;
using MafiCo.Application.Game.Contexts;
using MafiCo.Domain.AggregatesModel.BotAggregate;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MediatR;

namespace MafiCo.Infrastructure.MediatR.System;

public class CreateProcessorHandler : IRequestHandler<CreateProcessorCommand, PlayerProcessor> {
    private readonly GameContext _context;
    private readonly IBotRepository _botRepository;
    private readonly IProfileRepository _profileRepository;
    
    public CreateProcessorHandler(GameContext context, IBotRepository botRepository, IProfileRepository profileRepository) {
        _context = context;
        _botRepository = botRepository;
        _profileRepository = profileRepository;
    }
    
    public Task<PlayerProcessor> Handle(CreateProcessorCommand request, CancellationToken cancellationToken) {
        var profileId = request.profileId;
        if (!_profileRepository.Exists(profileId)) {
            throw new ApplicationException($"Profile with id {profileId} does not exist");
        }
        // bool isBot = await _botRepository.ExistsByProfileIdAsync(profileId);
        // if (isBot) {
        //     return new BotProcessor();
        // }
        // return new UserProcessor();
        return Task.FromResult(new PlayerProcessor(_context));
    }
}