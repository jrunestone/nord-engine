namespace Nord.Engine.Scenes;

public interface ISceneService
{
    TScene Push<TScene>() where TScene : IScene;
    IScene? Peek();
    IScene? Pop();
}