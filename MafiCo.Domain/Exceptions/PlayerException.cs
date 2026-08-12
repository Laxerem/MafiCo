using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.Exceptions;

public class PlayerException : DomainException {
    public PlayerException(string message) : base(message) {
        
    }
}