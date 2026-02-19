using System.Text;
using MafiCo.Domain.Abstractions;

namespace MafiCo.Domain.Entities;

public class Chat {
    private StringBuilder _builder;
    
    public Chat() {
        _builder = new StringBuilder();
    }

    public void SendMessage(string username, string message) {
        _builder.AppendLine($"- - -{username}- - -");
        _builder.AppendLine("- - -> {message}");
        _builder.AppendLine();
    }

    public string Read() {
        return _builder.ToString();
    }
}