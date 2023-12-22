using Arch.Core;
using Microsoft.Extensions.Logging;
using Nord.Engine.Core.Bus;
using Nord.Engine.Ecs;
using Nord.Engine.Ecs.Components;
using Nord.Engine.Input;
using Nord.Samples.HelloWorld.Input;

namespace Nord.Samples.HelloWorld.Systems;

public class MovementSystem : SystemBase
{
    private readonly ILogger<MovementSystem> _logger;
    private float Elapsed;

    private readonly QueryDescription _desc = new QueryDescription()
        .WithAll<PositionComponent>();

    public MovementSystem(
        World world, 
        IBus bus,
        ILogger<MovementSystem> logger) 
        : base(world)
    {
        _logger = logger;
        
        bus.Subscribe<InputActionActivated<RightInputAction>>(HandleInput);
        //bus.Subscribe<InputActionHappening<MoveForwardAction>>HandleInput)
        //bus.Subscribe<InputActionHappened<JumpAction>>HandleInput)
        //bus.SubscribeToInputAction<JumpAction>(isHappening: true, HandleInput)
    }
    
    public override void Update(float dt)
    {
        Elapsed += dt;
        if (Elapsed >= 1)
        {
            _logger.LogInformation("{Time}", Elapsed);
            Elapsed = 0;
        }
        // World.Query(in _desc, (ref PositionComponent position) =>
        // {
        // }); 
    }

    public void HandleInput(InputActionActivated<RightInputAction> @event)
    {
        _logger.LogInformation("Event: {Event}, action: {Action})", @event.ToString(), @event.InputAction.ToString());
    }
}