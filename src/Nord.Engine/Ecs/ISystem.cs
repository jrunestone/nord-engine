using Arch.System;

namespace Nord.Engine.Ecs;

public interface ISystem : ISystem<float>
{
    void Update(float dt);
}