using Arch.Core;
using Microsoft.Extensions.Logging;
using Nord.Engine.Ecs;
using Nord.Samples.HelloWorld.Components;

namespace Nord.Samples.HelloWorld.Systems;

public class MovementSystem(World world, ILogger<MovementSystem> logger) : SystemBase(world)
{
    private readonly ILogger<MovementSystem> _logger = logger;

    private readonly QueryDescription _desc = new QueryDescription()
        .WithAll<Position>();

    public override void Update(float dt)
    {
        World.Query(in _desc, (ref Position position) =>
        {
            position.X += dt;

            if (position.X >= 1)
            {
                _logger.LogInformation("{Pos}", position.X);
                position.X = 0;
            }
        }); 
    }
}