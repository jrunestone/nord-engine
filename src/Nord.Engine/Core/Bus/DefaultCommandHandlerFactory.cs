using Stashbox;

namespace Nord.Engine.Core.Bus;

public class DefaultCommandHandlerFactory : ICommandHandlerFactory
{
    private readonly IStashboxContainer _container;

    public DefaultCommandHandlerFactory(IStashboxContainer container)
    {
        _container = container;
    }
    
    public ICommandHandler<T> Get<T>() where T : ICommand
    {
        return _container.Resolve<ICommandHandler<T>>();
    }
}