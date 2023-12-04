using Nord.Engine.Core.Extensions;
using Nord.Engine.Ecs;
using Nord.Engine.Scenes;
using Nord.Samples.HelloWorld.Systems;
using Stashbox;

namespace Nord.Samples.HelloWorld.Scenes;

public class MainSceneCompositionRoot : ISceneCompositionRoot<MainScene>
{
    public void Compose(IStashboxContainer container)
    {
        container
            .AddEntityContext()
            .AddDefaultSystems();
        
        // systems
        container.RegisterSingleton<ISystem, MovementSystem>();
        
        // scene
        container.Register<MainScene>();
    }
}