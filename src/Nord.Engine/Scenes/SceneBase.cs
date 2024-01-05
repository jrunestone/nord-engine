using Arch.Core;
using Arch.System;
using Nord.Engine.Core;
using Nord.Engine.Core.Extensions;
using Nord.Engine.Ecs;

namespace Nord.Engine.Scenes;

public abstract class SceneBase : IScene
{
    protected World World { get; }
    protected Group<Time> Systems { get; }
    protected IEnumerable<IProcess> Processes { get; }

    public SceneBase(World world, IEnumerable<ISystem> systems, IEnumerable<IProcess> processes)
    {
        World = world;
        Processes = processes;
        Systems = new(systems.Cast<ISystem<Time>>().ToArray());
    }
    
    public virtual void Create()
    {
        Systems.Initialize();
    }
    
    public virtual void Update(Time time)
    {
        Processes.ForEach(x => x.Update(time));
        Systems.BeforeUpdate(time);
        Systems.Update(time);
        Systems.AfterUpdate(time);
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