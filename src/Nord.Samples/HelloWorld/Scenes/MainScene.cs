using Arch.Core;
using Microsoft.Extensions.Logging;
using Nord.Engine.Core.Assets;
using Nord.Engine.Ecs;
using Nord.Engine.Ecs.Components;
using Nord.Engine.Scenes;
using SFML.Graphics;
using SFML.System;

namespace Nord.Samples.HelloWorld.Scenes;

public class MainScene(
    World world, 
    IEnumerable<ISystem> systems,
    ITextureCache textureCache,
    IFontCache fontCache,
    ILogger<MainScene> logger) : SceneBase(world, systems)
{
    private readonly ITextureCache _textureCache = textureCache;
    private readonly IFontCache _fontCache = fontCache;
    private readonly ILogger<MainScene> _logger = logger;

    public override void Create()
    {
        base.Create();
        _logger.LogInformation("MainScene::Create()");
        
        World.Create(
            new SpriteComponent(_textureCache.GetTexture("spritesheet.png"), new Vector2f(10, 200), new IntRect(150, 20, 32, 100)));

        World.Create(
            new TextComponent("Debug text", _fontCache.GetFont("PixelifySans-VariableFont_wght.ttf"), new Vector2f(10, 10)));
    }
}