using MafiCo.Application.Game.DTOs;
using MafiCo.Application.Game.Mediator.Commands;
using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MediatR;
using Serilog;
using ILogger = Serilog.ILogger;

namespace MafiCo.Application.Game.Mediator.Handlers;

public class GetPlayersHandler : IRequestHandler<GetPlayersCommand, List<PublicPlayerInfo>> {
    private readonly GameContext _context;
    private readonly IProfileRepository _profileRepository;
    private readonly ILogger _logger = Log.ForContext<GetPlayersHandler>();

    public GetPlayersHandler(GameContext context, IProfileRepository profileRepository) {
        _context = context;
        _profileRepository = profileRepository;
    }

    public async Task<List<PublicPlayerInfo>> Handle(GetPlayersCommand request, CancellationToken cancellationToken) {
        var game = _context.Session!.Game;
        var playerIds = game.GetAllPlayers();

        var playerProfiles = new List<PublicPlayerInfo>();
        var profileIds = playerIds.Select(p => new ProfileId(p.Id));
        _logger.Information("Getting players for game {gameId}", game.Id);
        
        var profiles = await _profileRepository.GetRangeAsync(profileIds);

        foreach (var profile in profiles) {
            playerProfiles.Add(new PublicPlayerInfo(new PlayerId(profile.Id.Id), profile.Name, game.IsAlive(new PlayerId(profile.Id.Id))));
        }

        return playerProfiles;
    }
}
