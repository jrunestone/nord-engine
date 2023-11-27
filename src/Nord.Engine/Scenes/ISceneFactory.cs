namespace Nord.Engine.Scenes;

public interface ISceneFactory
{
    TScene Build<TScene>() where TScene : IScene;
}