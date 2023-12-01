using Arch.Core;
using Nord.Engine.Ecs;
using Nord.Engine.Scenes;
using Nord.Samples.HelloWorld.Systems;
using Stashbox;

namespace Nord.Samples.HelloWorld.Scenes;

public class MainSceneCompositionRoot : ISceneCompositionRoot<MainScene>
{
    public void Compose(IStashboxContainer container)
    {
        // entity context
        container.RegisterInstance<World>(World.Create(), finalizerDelegate: World.Destroy);
        
        // systems
        container.Register<ISystem, MovementSystem>();
        
        // scene
        container.Register<MainScene>();
    }
}