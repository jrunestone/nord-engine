using Stashbox;

namespace Nord.Engine.Scenes;

public class DefaultSceneFactory(IStashboxContainer container) : ISceneFactory
{
    private readonly IStashboxContainer _container = container;

    public TScene Build<TScene>() where TScene : IScene
    {
        var sceneContainer = _container.GetChildContainer(typeof(TScene)) ??
                             _container.CreateChildContainer(typeof(TScene));

        var compositionRoot = _container.Resolve<ISceneCompositionRoot<TScene>>();
        sceneContainer.ComposeBy(compositionRoot);
        return sceneContainer.Resolve<TScene>();
    }

    public void Destroy<TScene>(TScene scene) where TScene : IScene
    {
        var sceneContainer = _container.GetChildContainer(typeof(TScene));

        scene.Dispose();
        sceneContainer?.Dispose();
    }
}