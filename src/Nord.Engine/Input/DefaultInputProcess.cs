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
            if (inputDeviceTrigger.IsActive())
            {
                _bus.Publish(inputAction);
            }
        }
    }
}