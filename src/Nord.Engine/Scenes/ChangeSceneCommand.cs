using Nord.Engine.Core.Bus;

namespace Nord.Engine.Scenes;

public interface IChangeSceneCommand : ICommand
{
    Type SceneType { get; }
}

public class ChangeSceneCommand<T> : IChangeSceneCommand where T : IScene
{
    public Type SceneType => typeof(T);
}