using MafiCo.Infrastructure.Interfaces;

namespace MafiCo.Infrastructure.MediatR.Profile.Commands;

public record ChangeProfileNameCommand(string Name) : IAppCommand;
