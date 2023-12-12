using MediatR;

namespace Nord.Engine.Scenes;

public class ChangeSceneRequestHandler : IRequestHandler<IChangeSceneRequest>
{
    private readonly ISceneService _sceneService;

    public ChangeSceneRequestHandler(ISceneService sceneService)
    {
        _sceneService = sceneService;
    }
    
    public Task Handle(IChangeSceneRequest request, CancellationToken cancellationToken)
    {
        _sceneService.Push(request.SceneType);
        return Task.CompletedTask;
    }
}