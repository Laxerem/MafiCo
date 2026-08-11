namespace MafiCo.Console.Presentation.Base;

public abstract class ContextWindow<T> : Window where T : WindowData {
    protected T Context;
    
    public ContextWindow(T context) {
        Context = context;
    }
}