namespace Nord.Engine.Scenes;

public class DefaultSceneService(ISceneFactory sceneFactory) : ISceneService
{
    public IScene? CurrentScene => Peek();
    
    private readonly Stack<IScene> _scenes = new();
    private readonly ISceneFactory _sceneFactory = sceneFactory;

    public TScene Push<TScene>() where TScene : IScene
    {
        return (TScene)Push(typeof(TScene));
    }

    public IScene Push(Type sceneType)
    {
        if (!typeof(IScene).IsAssignableFrom(sceneType))
        {
            throw new ArgumentException($"Provided type is not of type {nameof(IScene)}");
        }

        var scene = _sceneFactory.Build(sceneType);
        scene.Create();
        _scenes.Push(scene);
        
        return scene;
    }

    public IScene? Peek()
    {
        return _scenes.Any() ? 
            _scenes.Peek() : 
            null;
    }

    public void Pop()
    {
        if (_scenes.Any())
        {
            var scene = _scenes.Pop();
            _sceneFactory.Destroy(scene);
        }
    }
}