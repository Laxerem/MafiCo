using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.Exceptions;

public class BotException : DomainException {
    public BotException(string message) : base(message) {
    }
}
