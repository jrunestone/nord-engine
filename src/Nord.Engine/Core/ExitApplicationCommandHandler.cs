using Nord.Engine.Core.Bus;

namespace Nord.Engine.Core;

public class ExitApplicationCommandHandler : ICommandHandler<ExitApplicationCommand>
{
    private readonly IApplication _application;

    public ExitApplicationCommandHandler(IApplication application)
    {
        _application = application;
    }
    
    public void Handle(ExitApplicationCommand command)
    {
        _application.Exit();
    }
}