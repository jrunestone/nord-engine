using Arch.Core;
using Microsoft.Extensions.Logging;
using Nord.Engine.Core;
using Nord.Engine.Core.Assets;
using Nord.Engine.Core.Bus;
using Nord.Engine.Core.Configuration;
using Nord.Engine.Core.Rendering;
using Nord.Engine.Ecs;
using Nord.Engine.Ecs.Components;
using Nord.Engine.Input.ActionMaps;
using Nord.Engine.Scenes;
using Nord.Samples.HelloWorld.Input;
using SFML.Graphics;
using SFML.System;

namespace Nord.Samples.HelloWorld.Scenes;

public class MainScene : SceneBase
{
    private readonly ITextureCache _textureCache;
    private readonly IFontCache _fontCache;
    private readonly IBus _bus;
    private readonly ILogger<MainScene> _logger;

    public MainScene(
        EngineOptions options,
        World world, 
        IEnumerable<ISystem> systems,
        IEnumerable<IProcess> processes,
        IEnumerable<IRenderLayerRenderTarget> renderLayerRenderTargets,
        ITextureCache textureCache,
        IFontCache fontCache,
        IBus bus,
        ILogger<MainScene> logger) : base(options, world, systems, processes, renderLayerRenderTargets)
    {
        _textureCache = textureCache;
        _fontCache = fontCache;
        _bus = bus;
        _logger = logger;
    }
    
    public override void Create()
    {
        base.Create();
        _logger.LogInformation("MainScene::Create()");
        
        World.Create(
            new DrawableComponent(),
            new SpriteComponent(_textureCache.GetTexture("spritesheet.png"), new Vector2f(10, 200), new IntRect(150, 20, 32, 100)),
            new AnimationComponent
            {
                Animations = new()
                {
                    new ()
                    {
                        Name = "Idle",
                        
                        Frames = new()
                        {
                            new()
                            {
                                Coords = new IntRect(153, 32, 32, 73), 
                                Delay = 0.2f
                            },
                            
                            new()
                            {
                                Coords = new IntRect(255, 32, 32, 73), 
                                Delay = 0.2f
                            },
                            
                            new()
                            {
                                Coords = new IntRect(358, 32, 32, 73), 
                                Delay = 0.2f
                            },
                            
                            new()
                            {
                                Coords = new IntRect(460, 32, 32, 73), 
                                Delay = 0.2f
                            },
                            
                            new()
                            {
                                Coords = new IntRect(562, 32, 32, 73), 
                                Delay = 0.2f
                            },
                            
                            new()
                            {
                                Coords = new IntRect(664, 32, 32, 73), 
                                Delay = 0.2f
                            },
                            
                            new()
                            {
                                Coords = new IntRect(766, 32, 32, 73), 
                                Delay = 0.2f
                            }
                        }
                    }
                }
            });

        World.Create(
            new DrawableComponent(),
            new TextComponent("FPS: {0}", _fontCache.DefaultFont, new Vector2f(10, 10)));
        
        _bus.Send<ChangeInputActionMapCommand<DefaultInputActionMap>>();
    }
}