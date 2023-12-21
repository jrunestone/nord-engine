using Arch.Core;
using Arch.System;
using Nord.Engine.Core;
using Nord.Engine.Core.Extensions;
using Nord.Engine.Ecs;

namespace Nord.Engine.Scenes;

public abstract class SceneBase : IScene
{
    protected World World { get; }
    protected Group<float> Systems { get; }
    protected IEnumerable<IProcess> Processes { get; }

    public SceneBase(World world, IEnumerable<ISystem> systems, IEnumerable<IProcess> processes)
    {
        World = world;
        Processes = processes;
        Systems = new(systems.Cast<ISystem<float>>().ToArray());
    }
    
    public virtual void Create()
    {
        Systems.Initialize();
    }
    
    public virtual void Update(float dt)
    {
        Processes.ForEach(x => x.Update(dt));
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