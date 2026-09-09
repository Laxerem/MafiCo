using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.Exceptions;

public class LlmException : DomainException {
    public LlmException(string message) : base(message) {
    }
}
