using System.Reflection;
using Stashbox;

namespace Nord.Engine.Core.Extensions;

public static class StashboxContainerExtensions
{
    public static void RegisterImplementations<TService>(this IStashboxContainer container, Assembly assembly)
    {
        container.RegisterAssembly(
            assembly,
            type => type
                .GetInterfaces()
                .Contains(typeof(TService)));
    }
}