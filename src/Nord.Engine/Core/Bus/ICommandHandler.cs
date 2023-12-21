namespace Nord.Engine.Core.Bus;

public interface ICommandHandler<in T> where T : ICommand
{
    void Handle(T command);
}