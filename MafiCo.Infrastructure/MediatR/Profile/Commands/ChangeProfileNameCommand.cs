using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Commands;
using MafiCo.Infrastructure.Interfaces;

namespace MafiCo.Infrastructure.MediatR.Profile.Commands;

public record ChangeProfileNameCommand(string Name) : IAppCommand;
