using Stashbox;

namespace Nord.Engine.Scenes;

public interface ISceneCompositionRoot<TScene> : ICompositionRoot 
    where TScene : IScene
{
    
}