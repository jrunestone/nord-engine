using Nord.Engine.Core;

namespace Nord.Engine.Scenes;

public interface IScene : IDisposable
{
    void Create();
    void Update(Time time);
}