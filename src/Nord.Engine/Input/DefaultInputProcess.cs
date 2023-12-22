using System.Reflection;
using Nord.Engine.Core;
using Nord.Engine.Core.Bus;

namespace Nord.Engine.Input;

public class DefaultInputProcess : IProcess
{
    private readonly IBus _bus;
    private readonly IInputActionMapService _inputActionMapService;

    public DefaultInputProcess(IBus bus, IInputActionMapService inputActionMapService)
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
                _bus.Publish(CreateInputEvent<InputActionActivated<IInputAction>>(inputAction));
            }
            else if (inputDeviceTrigger.IsActive)
            {
                // pressed/down
                _bus.Publish(CreateInputEvent<InputActionActive<IInputAction>>(inputAction));
            }
        }
    }

    private object CreateInputEvent<T>(IInputAction inputAction) where T : IInputActionEvent
    {
        return Activator.CreateInstance(typeof(T), BindingFlags.CreateInstance, null, new[] { inputAction }) ?? 
               throw new ArgumentException("Invalid event type");
    } 
}