using Arch.System;
using Nord.Engine.Core;

namespace Nord.Engine.Ecs;

public interface ISystem : ISystem<Time>
{
    // void BeforeUpdate(Time time);
    void Update(Time time);
    // void AfterUpdate(Time time);
}