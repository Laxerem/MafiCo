using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Application.Profile.Commands;

public record ChangeProfileNameCommand(string Name) : IUserCommand;
