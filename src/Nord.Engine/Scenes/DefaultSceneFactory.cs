using Stashbox;

namespace Nord.Engine.Scenes;

public class DefaultSceneFactory(IStashboxContainer container) : ISceneFactory
{
    private readonly IStashboxContainer _container = container;

    public TScene Build<TScene>() where TScene : IScene
    {
        return (TScene)Build(typeof(TScene));
    }

    public IScene Build(Type sceneType)
    {
        if (!typeof(IScene).IsAssignableFrom(sceneType))
        {
            throw new ArgumentException($"Provided type is not of type {nameof(IScene)}");
        }
        
        var sceneContainer = _container.GetChildContainer(sceneType) ??
                             _container.CreateChildContainer(sceneType);

        var targetType = typeof(ISceneCompositionRoot<>)
            .MakeGenericType(sceneType);
        
        var compositionRoot = (ICompositionRoot)_container.Resolve(targetType);
        sceneContainer.ComposeBy(compositionRoot);
        
        return (IScene)sceneContainer.Resolve(sceneType);
    }

    public void Destroy<TScene>(TScene scene) where TScene : IScene
    {
        var sceneContainer = _container.GetChildContainer(typeof(TScene));

        scene.Dispose();
        sceneContainer?.Dispose();
    }
}