using Arch.Core;
using Arch.System;
using Nord.Engine.Core;
using Nord.Engine.Core.Configuration;
using Nord.Engine.Core.Extensions;
using Nord.Engine.Core.Rendering;
using Nord.Engine.Ecs;

namespace Nord.Engine.Scenes;

public abstract class SceneBase : IScene
{
    private readonly EngineOptions _options;
    
    protected World World { get; }
    protected Group<Time> Systems { get; }
    protected IEnumerable<IProcess> Processes { get; }
    protected IEnumerable<IRenderLayerRenderTarget> RenderLayerRenderTargets { get; }

    public SceneBase(
        EngineOptions options,
        World world, 
        IEnumerable<ISystem> systems, 
        IEnumerable<IProcess> processes, 
        IEnumerable<IRenderLayerRenderTarget> renderLayerRenderTargets)
    {
        _options = options;
        
        World = world;
        Processes = processes;
        Systems = new(systems.Cast<ISystem<Time>>().ToArray());
        RenderLayerRenderTargets = renderLayerRenderTargets;
    }
    
    public virtual void Create()
    {
        RenderLayerRenderTargets.ForEach(x => x.Create(_options.VideoMode.Width, _options.VideoMode.Height));
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