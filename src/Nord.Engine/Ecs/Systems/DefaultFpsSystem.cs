using Arch.Core;
using Nord.Engine.Ecs.Components;

namespace Nord.Engine.Ecs.Systems;

public class DefaultFpsSystem : SystemBase
{
    private readonly QueryDescription _query = new QueryDescription()
        .WithAll<FpsComponent>();

    public DefaultFpsSystem(World world) : base(world)
    {
        
    }
    
    public override void Update(float dt)
    {
        World.Query(in _query, (ref FpsComponent fps) =>
        {
            fps.FrameCounter += 1;
            fps.ElapsedTime += dt;
            
            if (fps.ElapsedTime >= 1.0f)
            {
                fps.AverageFps = (int)(fps.FrameCounter / fps.ElapsedTime);
                fps.FrameCounter = 0;
                fps.ElapsedTime = 0;
            }
        });
    }
}