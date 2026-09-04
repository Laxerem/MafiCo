using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Application.Interfaces.Controllers;

public interface IPlayerController {
    Task HandleCommandAsync(IPlayerCommand command);
}