using Nord.Engine.Core.Bus;

namespace Nord.Engine.Scenes;

public class ChangeSceneCommandHandler : ICommandHandler<IChangeSceneCommand>
{
    private readonly ISceneService _sceneService;

    public ChangeSceneCommandHandler(ISceneService sceneService)
    {
        _sceneService = sceneService;
    }
    
    public void Handle(IChangeSceneCommand command)
    {
        _sceneService.Push(command.SceneType);
    }
}