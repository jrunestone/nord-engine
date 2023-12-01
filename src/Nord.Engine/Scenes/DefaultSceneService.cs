namespace Nord.Engine.Scenes;

public class DefaultSceneService(ISceneFactory sceneFactory) : ISceneService
{
    public IScene? CurrentScene => Peek();
    
    private readonly Stack<IScene> _scenes = new();
    private readonly ISceneFactory _sceneFactory = sceneFactory;

    public TScene Push<TScene>() where TScene : IScene
    {
        var scene = _sceneFactory.Build<TScene>();

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