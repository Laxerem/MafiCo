using MafiCo.Console.Presentation.Base;
using MafiCo.Domain.DTOs;

namespace MafiCo.Console.Presentation.Windows.Lobby.Context;

public record MenuData(
    ProfileInfo User
) : WindowData;