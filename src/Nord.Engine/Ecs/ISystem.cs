using Arch.System;
using Nord.Engine.Core;

namespace Nord.Engine.Ecs;

public interface ISystem : ISystem<Time>
{
    void Update(Time time);
}