using SFML.Window;

namespace Nord.Engine.Input;

public class KeyUpEvent
{
    public Keyboard.Key Key { get; }

    public KeyUpEvent(Keyboard.Key key)
    {
        Key = key;
    }
}