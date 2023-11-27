namespace Nord.Engine.Scenes;

public class DefaultSceneService(ISceneFactory _sceneFactory) : ISceneService
{
    public IScene? CurrentScene { get; private set; }
    private readonly Stack<IScene> _scenes = new(); 
        
    public TScene Push<TScene>() where TScene : IScene
    {
        var scene = _sceneFactory.Build<TScene>();

        scene.Create();
        _scenes.Push(scene);
        CurrentScene = scene;
        
        return scene;
    }

    public IScene? Peek()
    {
        return _scenes.Any() ? 
            _scenes.Peek() : 
            null;
    }

    public IScene? Pop()
    {
        if (!_scenes.Any())
        {
            return null;
        }

        var scene = _scenes.Pop();
        scene.Dispose();
        
        return scene;
    }
}