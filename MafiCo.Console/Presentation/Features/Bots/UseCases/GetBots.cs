using MafiCo.Infrastructure.DTOs;
using MafiCo.Infrastructure.Interfaces;
using MafiCo.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace MafiCo.Console.Presentation.Features.Bots.UseCases;

public class GetBots {
    private readonly BotService _service;
    
    public GetBots(BotService service) {
        _service = service;
    }

    public async Task<List<BotDto>> ExecuteAsync() {
        return await _service.GetAllBots();
    }
}