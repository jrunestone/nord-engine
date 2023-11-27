using Microsoft.Extensions.Logging;
using Nord.Engine.Scenes;

namespace Nord.Samples.HelloWorld.Scenes;

public class MainScene(TestDependency _testDependency, ILogger<MainScene> _logger) : IScene
{
    public void Create()
    {
        _logger.LogInformation("MainScene::Create(), {Test}", _testDependency.SomeValue);
    }

    public void Dispose()
    {
        _logger.LogInformation("MainScene::Dispose()");
    }
}