using System.Reflection;
using Stashbox;

namespace Nord.Engine.Scenes;

public class DefaultSceneFactory(IStashboxContainer _container) : ISceneFactory
{
    public TScene Build<TScene>() where TScene : IScene
    {
        var sceneContainer = _container.GetChildContainer(typeof(TScene)) ??
                             _container.CreateChildContainer(typeof(TScene));
        
        sceneContainer.ComposeBy(FindSceneCompositionRoot<TScene>());
        return sceneContainer.Resolve<TScene>();
    }

    private Type FindSceneCompositionRoot<TScene>() where TScene : IScene
    {
        return AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .First(x => 
                x.GetInterfaces().Contains(typeof(ISceneCompositionRoot<TScene>)) && 
                x.IsClass);
    }
}