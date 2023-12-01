using Arch.Core;
using Arch.System;
using Nord.Engine.Ecs;

namespace Nord.Engine.Scenes;

public abstract class SceneBase(World world, IEnumerable<ISystem> systems) : IScene
{
    protected World World { get; } = world;
    protected Group<float> Systems { get; } = new(systems.Cast<ISystem<float>>().ToArray());
    
    public virtual void Create()
    {
        Systems.Initialize();
    }
    
    public virtual void Update(float dt)
    {
        Systems.BeforeUpdate(dt);
        Systems.Update(dt);
        Systems.AfterUpdate(dt);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            
        }
    }
}