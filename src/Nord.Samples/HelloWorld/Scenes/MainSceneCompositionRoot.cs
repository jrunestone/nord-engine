using Nord.Engine.Core.Extensions;
using Nord.Engine.Ecs;
using Nord.Engine.Input;
using Nord.Engine.Scenes;
using Nord.Samples.HelloWorld.Input;
using Nord.Samples.HelloWorld.Systems;
using Stashbox;

namespace Nord.Samples.HelloWorld.Scenes;

public class MainSceneCompositionRoot : ISceneCompositionRoot<MainScene>
{
    public void Compose(IStashboxContainer container)
    {
        container
            .AddCore()
            .AddInput()
            .AddEntityContext()
            .AddRendering();
        
        // input
        container.RegisterSingleton<IInputActionMap, DefaultInputActionMap>();
        
        // systems
        container.RegisterSingleton<ISystem, MovementSystem>();
        
        // scene
        container.RegisterSingleton<MainScene>();
    }
}