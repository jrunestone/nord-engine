using Arch.Core;
using Arch.System;

namespace Nord.Engine.Ecs;

public abstract class SystemBase(World world) : BaseSystem<World, float>(world), ISystem
{
    public override void Update(in float t)
    {
        // so that we can use dt in our World.Query lambda
        Update(Data);
    }

    public abstract void Update(float dt);
}