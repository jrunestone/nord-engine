using Arch.Core;
using Arch.System;
using Nord.Engine.Core;

namespace Nord.Engine.Ecs;

public abstract class SystemBase(World world) : BaseSystem<World, Time>(world), ISystem
{
    public override void Update(in Time t)
    {
        // so that we can use t in our World.Query lambda
        Update(Data);
    }

    public abstract void Update(Time time);
}