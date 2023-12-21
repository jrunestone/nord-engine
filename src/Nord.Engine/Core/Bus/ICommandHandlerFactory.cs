namespace Nord.Engine.Core.Bus;

public interface ICommandHandlerFactory
{
    ICommandHandler<T> Get<T>() where T : ICommand;
}