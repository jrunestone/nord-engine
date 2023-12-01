namespace Nord.Engine.Scenes;

public interface ISceneService
{
    IScene? CurrentScene { get; }
    TScene Push<TScene>() where TScene : IScene;
    IScene? Peek();
    void Pop();
}