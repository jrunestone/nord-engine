namespace Nord.Engine.Scenes;

public interface ISceneFactory
{
    TScene Build<TScene>() where TScene : IScene;
    IScene Build(Type sceneType);
    void Destroy<TScene>(TScene scene) where TScene : IScene;
}