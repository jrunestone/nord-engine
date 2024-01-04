using Arch.Core;
using Microsoft.Extensions.Logging;
using Nord.Engine.Core;
using Nord.Engine.Core.Bus;
using Nord.Engine.Ecs;
using Nord.Engine.Ecs.Components;
using Nord.Engine.Input;
using Nord.Engine.Input.ActionMaps;
using Nord.Samples.HelloWorld.Input;
using SFML.Window;

namespace Nord.Samples.HelloWorld.Systems;

public class MovementSystem : SystemBase
{
    private readonly IGlobalBus _globalBus;
    private readonly ILogger<MovementSystem> _logger;
    private float Elapsed;

    private readonly QueryDescription _desc = new QueryDescription()
        .WithAll<PositionComponent>();

    public MovementSystem(
        World world, 
        IBus bus,
        IGlobalBus globalBus,
        ILogger<MovementSystem> logger) 
        : base(world)
    {
        _globalBus = globalBus;
        _logger = logger;
        
        bus.Subscribe<InputActionActivated<RightInputAction>>(HandleRightAction);
        _globalBus.Subscribe<KeyDownEvent>(HandleRawInputDown);
        _globalBus.Subscribe<KeyUpEvent>(HandleRawInputUp);
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

    private void HandleRightAction(InputActionActivated<RightInputAction> @event)
    {
        _logger.LogInformation("Event: {Event}, action: {Action})", @event.ToString(), @event.InputAction.ToString());
    }
    
    private void HandleRawInputDown(KeyDownEvent @event)
    {
        _logger.LogInformation("Key down: {Key}", @event.Key);
    }
    
    private void HandleRawInputUp(KeyUpEvent @event)
    {
        _logger.LogInformation("Key up: {Key}", @event.Key);
        
        if (@event.Key == Keyboard.Key.Escape)
        {
            _globalBus.Send<ExitApplicationCommand>();
        }
    }
}