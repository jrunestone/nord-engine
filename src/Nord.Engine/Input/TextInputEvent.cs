namespace Nord.Engine.Input;

public class TextInputEvent
{
    public string Character { get; }

    public TextInputEvent(string character)
    {
        Character = character;
    }
}