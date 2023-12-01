namespace Nord.Engine.Scenes;

public interface IScene : IDisposable
{
    void Create();
    void Update(float dt);
}