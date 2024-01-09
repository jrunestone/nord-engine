using Arch.Core;
using Arch.System;
using Nord.Engine.Core;

namespace Nord.Engine.Ecs;

public abstract class SystemBase(World world) : BaseSystem<World, Time>(world), ISystem
{
    public override void BeforeUpdate(in Time t)
    {
        base.BeforeUpdate(t);
        
        // so that we can use t in our World.Query lambda
        BeforeUpdate(Data);
    }
    
    public override void Update(in Time t)
    {
        base.Update(t);
        
        // so that we can use t in our World.Query lambda
        Update(Data);
    }
    
    public override void AfterUpdate(in Time t)
    {
        base.AfterUpdate(t);
        
        // so that we can use t in our World.Query lambda
        AfterUpdate(Data);
    }
    
    public virtual void BeforeUpdate(Time time)
    {
        
    }

    public virtual void Update(Time time)
    {
        
    }
    
    public virtual void AfterUpdate(Time time)
    {
        
    }
}