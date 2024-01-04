using System.Reflection;
using Arch.Core;
using Nord.Engine.Core.Bus;
using Nord.Engine.Ecs;
using Nord.Engine.Ecs.Systems;
using Nord.Engine.Input;
using Nord.Engine.Input.ActionMaps;
using Stashbox;

namespace Nord.Engine.Core.Extensions;

public static class StashboxContainerExtensions
{
    public static IStashboxContainer AddCore(this IStashboxContainer container)
    {
        // scene-scoped
        container.RegisterSingleton<IBus, DefaultBus>();
        container.RegisterInstance<ICommandHandlerFactory>(new DefaultCommandHandlerFactory(container));
        container.RegisterOpenGenericImplementations(Assembly.GetExecutingAssembly(), typeof(ICommandHandler<>));
        
        return container;
    }
    
    public static IStashboxContainer AddRendering(this IStashboxContainer container)
    {
        // scene-scoped
        container.RegisterSingleton<ISystem, DefaultAnimationSystem>();
        container.RegisterSingleton<ISystem, DefaultRenderSystem>();
        container.RegisterSingleton<ISystem, DefaultTextRenderSystem>();
        
        return container;
    }
    
    public static IStashboxContainer AddInput(this IStashboxContainer container)
    {
        // scene-scoped
        container.RegisterSingleton<IInputActionMapService, DefaultInputActionMapService>();
        container.RegisterSingleton<IProcess, DefaultInputActionProcess>();
        
        return container;
    }
     
    public static IStashboxContainer AddEntityContext(this IStashboxContainer container)
    {
        // scene-scoped
        container.RegisterInstance<World>(World.Create(), finalizerDelegate: World.Destroy);
        return container;
    }
    
    public static void RegisterOpenGenericImplementations(
        this IStashboxContainer container, 
        Assembly assembly, 
        Type openType)
    {
        container.RegisterAssembly(
            assembly,
            type => type
                .GetInterfaces()
                .Any(x => x.IsGenericType && openType.IsAssignableFrom(x.GetGenericTypeDefinition())));
    }
    
    public static void RegisterImplementations<TService>(this IStashboxContainer container, Assembly assembly)
    {
        container.RegisterAssembly(
            assembly,
            type => type
                .GetInterfaces()
                .Contains(typeof(TService)));
    }
}