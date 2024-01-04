using Nord.Engine.Core;
using Nord.Engine.Core.Bus;
using Nord.Engine.Core.Extensions;
using SFML.Graphics;
using SFML.Window;

namespace Nord.Engine.Input;

public class DefaultRawInputProcess : IGlobalProcess
{
    private readonly IDictionary<Keyboard.Key, bool> _keyboardMap = new Dictionary<Keyboard.Key, bool>();
    private readonly IBus _bus;

    public DefaultRawInputProcess(IBus bus, RenderWindow window)
    {
        _bus = bus;

        window.KeyPressed += (_, e) => HandleKeyboardInput(e, true);
        window.KeyReleased += (_, e) => HandleKeyboardInput(e, false);
    }

    public void Update(float dt)
    {
        _keyboardMap
            .Where(x => x.Value)
            .ForEach(x => _bus.Publish(new KeyDownEvent(x.Key)));
    }
    
    private void HandleKeyboardInput(KeyEventArgs @event, bool isPressed)
    {
        _keyboardMap.TryAdd(@event.Code, false);
        var wasPressed = _keyboardMap[@event.Code];
        _keyboardMap[@event.Code] = isPressed;

        if (wasPressed && !isPressed)
        {
            _bus.Publish(new KeyUpEvent(@event.Code));
        }
    }
}