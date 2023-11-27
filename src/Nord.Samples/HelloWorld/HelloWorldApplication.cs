using Nord.Engine.Core;
using Nord.Engine.Core.Configuration;
using Nord.Engine.Scenes;
using Nord.Samples.HelloWorld.Scenes;

namespace Nord.Samples.HelloWorld;

public class HelloWorldApplication(ISceneService _sceneService, EngineOptions _options) : DefaultApplication(_options)
{
    public override void Run()
    {
        _sceneService.Push<MainScene>();
        base.Run();
    }
}