using System.Reflection;

namespace Nord.Engine.Core.Bus;

public class DefaultBus : IBus, IGlobalBus
{
    private readonly ICommandHandlerFactory _commandHandlerFactory;
    private readonly IDictionary<Type, List<object>> EventHandlers = new Dictionary<Type, List<object>>();

    public DefaultBus(ICommandHandlerFactory commandHandlerFactory)
    {
        _commandHandlerFactory = commandHandlerFactory;
    }

    public void Send<T>() where T : ICommand, new()
    {
        Send(new T());
    }
    
    public void Send<T>(T command) where T : ICommand
    {
        var handler = _commandHandlerFactory.Get<T>();
        handler.Handle(command);
    }

    public void Publish<T>(T @event) where T : notnull
    {
        var type = @event.GetType();

        if (!EventHandlers.TryGetValue(type, out var handlers))
        {
            return;
        }

        foreach (var handler in handlers)
        {
            var action = () => {};
            var memberName = nameof(action.Invoke);
            
            handler.GetType()
                .InvokeMember(
                    memberName, 
                    BindingFlags.InvokeMethod, 
                    null, 
                    handler, 
                    new [] { (object)@event });
        }
    }

    public void Subscribe<T>(Action<T> handler)
    {
        var type = typeof(T);

        if (!EventHandlers.TryGetValue(type, out var handlers))
        {
            handlers = new List<object>();
            EventHandlers.Add(type, handlers);
        }
        
        handlers.Add(handler);
    }

    public void Unsubscribe<T>(Action<T> handler)
    {
        var type = typeof(T);
        EventHandlers.Remove(type);
    }
}