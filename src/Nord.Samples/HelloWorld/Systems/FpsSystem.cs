using Arch.Core;
using Nord.Engine.Ecs;
using Nord.Engine.Ecs.Components;

namespace Nord.Samples.HelloWorld.Systems;

public class FpsTextSystem : SystemBase
{
    private readonly QueryDescription _query = new QueryDescription()
        .WithAll<FpsComponent, TextComponent>();

    public FpsTextSystem(World world) : base(world)
    {
        
    }
    
    public override void Update(float dt)
    {
        World.Query(in _query, (ref FpsComponent fps, ref TextComponent text) =>
        {
            text.Text.DisplayedString = string.Format(text.OriginalString, fps.AverageFps);
        });
    }
}