using SFML.Window;

namespace Nord.Engine.Input;

public class KeyboardDeviceTrigger : IInputDeviceTrigger
{
    public Keyboard.Key Key { get; }

    public KeyboardDeviceTrigger(Keyboard.Key key)
    {
        Key = key;
    }
    
    public bool IsActive()
    {
        return Keyboard.IsKeyPressed(Key);
    }
}