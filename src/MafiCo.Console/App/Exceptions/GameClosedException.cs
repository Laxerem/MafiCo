namespace MafiCo.Console.App.Exceptions;

public class GameClosedException : Exception {
    public GameClosedException(string message) : base(message) {
        
    }
}