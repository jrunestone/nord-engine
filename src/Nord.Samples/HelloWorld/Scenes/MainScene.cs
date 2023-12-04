using Arch.Core;
using Microsoft.Extensions.Logging;
using Nord.Engine.Core;
using Nord.Engine.Ecs;
using Nord.Engine.Ecs.Components;
using Nord.Engine.Scenes;

namespace Nord.Samples.HelloWorld.Scenes;

public class MainScene(
    World world, 
    IEnumerable<ISystem> systems,
    ITextureCache textureCache,
    ILogger<MainScene> logger) : SceneBase(world, systems)
{
    private readonly ITextureCache _textureCache = textureCache;
    private readonly ILogger<MainScene> _logger = logger;

    public override void Create()
    {
        base.Create();
        _logger.LogInformation("MainScene::Create()");
        
        World.Create(
            new SpriteComponent(_textureCache.GetTexture("/home/jr/src/nord-engine/src/Nord.Samples/assets/spritesheet1.png")));
    }
}