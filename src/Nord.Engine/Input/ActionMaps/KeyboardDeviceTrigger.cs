using SFML.Window;

namespace Nord.Engine.Input.ActionMaps;

public class KeyboardDeviceTrigger : IInputDeviceTrigger
{
    public bool IsActive { get; private set; }
    public bool IsActivated { get; private set; }
    public Keyboard.Key Key { get; }

    private bool _isPressed;
    
    public KeyboardDeviceTrigger(Keyboard.Key key)
    {
        Key = key;
    }
    
    public void Update()
    {
        var isPressed = Keyboard.IsKeyPressed(Key);

        IsActivated = _isPressed && !isPressed;
        IsActive = isPressed;

        _isPressed = isPressed;
    }
}