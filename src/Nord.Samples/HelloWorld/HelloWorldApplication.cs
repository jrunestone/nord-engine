using Nord.Engine.Core;
using Nord.Engine.Core.Bus;
using Nord.Engine.Core.Configuration;
using Nord.Engine.Scenes;
using Nord.Samples.HelloWorld.Scenes;
using SFML.System;

namespace Nord.Samples.HelloWorld;

public class HelloWorldApplication : DefaultApplication
{
    private readonly IGlobalBus _globalBus;

    public HelloWorldApplication(
        EngineOptions options,
        Clock clock,
        MainRenderTarget mainRenderTarget,
        IGlobalBus globalBus,
        ISceneService sceneService,
        IEnumerable<IGlobalProcess> processes) 
        : base(options, clock, mainRenderTarget, sceneService, processes)
    {
        _globalBus = globalBus;
    }
    
    public override void Run()
    {
        _globalBus.Send(new ChangeSceneCommand<MainScene>());
        base.Run();
    }
}