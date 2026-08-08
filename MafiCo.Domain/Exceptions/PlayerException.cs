using MafiCo.Domain.Exceptions.Common;

namespace MafiCo.Domain.Exceptions;

public class PlayerException : DomainException {
    public PlayerException(string message) : base(message) {
        
    }
}