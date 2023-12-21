namespace Nord.Engine.Core.Bus;

public interface IBus
{
    void Send<T>() where T : ICommand, new();
    void Send<T>(T command) where T : ICommand;
    void Publish<T>(T @event) where T : notnull;
    void Subscribe<T>(Action<T> handler);
}