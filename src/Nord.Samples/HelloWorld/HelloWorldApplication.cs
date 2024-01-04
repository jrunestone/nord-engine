using Nord.Engine.Core;
using Nord.Engine.Core.Bus;
using Nord.Engine.Core.Configuration;
using Nord.Engine.Scenes;
using Nord.Samples.HelloWorld.Scenes;
using SFML.Graphics;
using SFML.System;

namespace Nord.Samples.HelloWorld;

public class HelloWorldApplication : DefaultApplication
{
    private readonly IGlobalBus _bus;

    public HelloWorldApplication(
        EngineOptions options,
        RenderWindow window,
        MainRenderTarget mainRenderTarget,
        Clock clock,
        IGlobalBus bus,
        ISceneService sceneService,
        IEnumerable<IGlobalProcess> processes) 
        : base(options, window, mainRenderTarget, clock, bus, sceneService, processes)
    {
        _bus = bus;
    }
    
    public override void Run()
    {
        _bus.Send(new ChangeSceneCommand<MainScene>());
        base.Run();
    }
}