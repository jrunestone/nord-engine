using Nord.Engine.Scenes;
using Stashbox;

namespace Nord.Samples.HelloWorld.Scenes;

public class MainSceneCompositionRoot : ISceneCompositionRoot<MainScene>
{
    public void Compose(IStashboxContainer container)
    {
        container.Register<TestDependency>();
    }
}