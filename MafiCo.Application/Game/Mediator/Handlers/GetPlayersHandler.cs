using MafiCo.Application.Game.Commands;
using MafiCo.Application.Game.DTOs;
using MafiCo.Application.Game.Mediator.Commands;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MediatR;

namespace MafiCo.Application.Game.Mediator.Handlers;

public class GetPlayersHandler : IRequestHandler<GetPlayersCommand, List<PublicPlayerInfo>> {
    private readonly GameContext _context;
    private readonly IProfileRepository _profileRepository;

    public GetPlayersHandler(GameContext context, IProfileRepository profileRepository) {
        _context = context;
        _profileRepository = profileRepository;
    }
    
    public async Task<List<PublicPlayerInfo>> Handle(GetPlayersCommand request, CancellationToken cancellationToken) {
        var game = _context.GetGame();
        var playerIds = game.GetAllPlayers();
        
        var playerProfiles = new List<PublicPlayerInfo>();
        foreach (var playerId in playerIds) {
            var profile = await _profileRepository.GetAsync(playerId);
            playerProfiles.Add(new PublicPlayerInfo(profile!.Id, profile.Name, game.IsAlive(profile.Id)));
        }
        
        return playerProfiles;
    }
}