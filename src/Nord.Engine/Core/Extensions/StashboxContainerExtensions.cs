using System.Reflection;
using Arch.Core;
using Nord.Engine.Ecs;
using Nord.Engine.Ecs.Systems;
using Stashbox;

namespace Nord.Engine.Core.Extensions;

public static class StashboxContainerExtensions
{
    public static IStashboxContainer AddDefaultSystems(this IStashboxContainer container)
    {
        container.Register<ISystem, DefaultRenderSystem>();
        container.Register<ISystem, DefaultTextRenderSystem>();
        
        return container;
    }
     
    public static IStashboxContainer AddEntityContext(this IStashboxContainer container)
    {
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