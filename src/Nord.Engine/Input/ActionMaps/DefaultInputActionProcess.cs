using Nord.Engine.Core;
using Nord.Engine.Core.Bus;

namespace Nord.Engine.Input.ActionMaps;

public class DefaultInputActionProcess : IProcess
{
    private readonly IBus _bus;
    private readonly IInputActionMapService _inputActionMapService;

    public DefaultInputActionProcess(IBus bus, IInputActionMapService inputActionMapService)
    {
        _bus = bus;
        _inputActionMapService = inputActionMapService;
    }
    
    public void Update(float dt)
    {
        if (_inputActionMapService.CurrentActionMap == null)
        {
            return;
        }

        foreach (var (inputAction, inputDeviceTrigger) in _inputActionMapService.CurrentActionMap.InputActions)
        {
            inputDeviceTrigger.Update();
            
            if (inputDeviceTrigger.IsActivated)
            {
                // released
                _bus.Publish(CreateInputEvent(typeof(InputActionActivated<>), inputAction));
            }
            else if (inputDeviceTrigger.IsActive)
            {
                // pressed/down
                _bus.Publish(CreateInputEvent(typeof(InputActionActive<>), inputAction));
            }
        }
    }

    private object CreateInputEvent<T>(Type baseEventType, T inputAction) where T : IInputAction
    {
        var actionType = inputAction.GetType();
        
        if (!typeof(IInputActionEvent).IsAssignableFrom(baseEventType) ||
            !baseEventType.IsGenericType)
        {
            throw new ArgumentException("Invalid event type");
        }
        
        var eventType = baseEventType.MakeGenericType(actionType);
        
        return Activator.CreateInstance(eventType, inputAction) ?? 
               throw new ArgumentException("Invalid event type");
    } 
}