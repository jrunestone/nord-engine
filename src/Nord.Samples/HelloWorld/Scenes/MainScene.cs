using Arch.Core;
using Microsoft.Extensions.Logging;
using Nord.Engine.Ecs;
using Nord.Engine.Scenes;
using Nord.Samples.HelloWorld.Components;

namespace Nord.Samples.HelloWorld.Scenes;

public class MainScene(
    World world, 
    IEnumerable<ISystem> systems,
    ILogger<MainScene> logger) : SceneBase(world, systems)
{
    private readonly ILogger<MainScene> _logger = logger;

    public override void Create()
    {
        base.Create();
        _logger.LogInformation("MainScene::Create()");
        World.Create(new Position { X = 100, Y = 100 });
    }
}