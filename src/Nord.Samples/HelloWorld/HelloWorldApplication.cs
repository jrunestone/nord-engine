using Nord.Engine.Core;
using Nord.Engine.Core.Bus;
using Nord.Engine.Core.Configuration;
using Nord.Engine.Core.Rendering;
using Nord.Engine.Scenes;
using Nord.Samples.HelloWorld.Scenes;
using SFML.Graphics;

namespace Nord.Samples.HelloWorld;

public class HelloWorldApplication : DefaultApplication
{
    private readonly IGlobalBus _bus;

    public HelloWorldApplication(
        EngineOptions options,
        RenderWindow window,
        IMainRenderTarget mainRenderTarget,
        Time time,
        IGlobalBus bus,
        ISceneService sceneService,
        IEnumerable<IGlobalProcess> processes) 
        : base(options, window, mainRenderTarget, time, bus, sceneService, processes)
    {
        _bus = bus;
    }
    
    public override void Run()
    {
        _bus.Send(new ChangeSceneCommand<MainScene>());
        base.Run();
    }
}