using Nord.Engine.Core;
using Nord.Engine.Core.Configuration;
using Nord.Engine.Scenes;
using Nord.Samples.HelloWorld.Scenes;
using SFML.System;

namespace Nord.Samples.HelloWorld;

public class HelloWorldApplication(
    ISceneService sceneService, 
    Clock clock,
    EngineOptions options) : DefaultApplication(sceneService, clock, options)
{
    private readonly ISceneService _sceneService = sceneService;
    
    public override void Run()
    {
        _sceneService.Push<MainScene>();
        base.Run();
    }
}