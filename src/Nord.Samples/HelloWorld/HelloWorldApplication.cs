using Nord.Engine.Core;
using Nord.Engine.Core.Configuration;
using Nord.Engine.Scenes;
using Nord.Samples.HelloWorld.Scenes;
using SFML.System;

namespace Nord.Samples.HelloWorld;

public class HelloWorldApplication(
    EngineOptions options,
    Clock clock,
    MainRenderTarget mainRenderTarget,
    ISceneService sceneService) : DefaultApplication(options, clock, mainRenderTarget, sceneService)
{
    private readonly ISceneService _sceneService = sceneService;
    
    public override void Run()
    {
        _sceneService.Push<MainScene>();
        base.Run();
    }
}