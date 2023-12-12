using MediatR;

namespace Nord.Engine.Scenes;

public interface IChangeSceneRequest : IRequest
{
    Type SceneType { get; }
}

public class ChangeSceneRequest<T> : IChangeSceneRequest where T : IScene
{
    public Type SceneType => typeof(T);
}