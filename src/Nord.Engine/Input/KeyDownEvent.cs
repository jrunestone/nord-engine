using SFML.Window;

namespace Nord.Engine.Input;

public class KeyDownEvent
{
    public Keyboard.Key Key { get; }

    public KeyDownEvent(Keyboard.Key key)
    {
        Key = key;
    }
}