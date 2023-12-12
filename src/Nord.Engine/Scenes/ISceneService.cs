namespace Nord.Engine.Scenes;

public interface ISceneService
{
    IScene? CurrentScene { get; }
    TScene Push<TScene>() where TScene : IScene;
    IScene Push(Type sceneType);
    IScene? Peek();
    void Pop();
}