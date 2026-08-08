namespace MafiCo.Domain.Exceptions.Common;

public abstract class DomainException : Exception {
    public DomainException(string message) : base(message) {
        
    }
}