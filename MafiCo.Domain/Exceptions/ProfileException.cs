using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.Exceptions;

public class ProfileException : DomainException {
    public ProfileException(string message) : base(message) {
    }
}
