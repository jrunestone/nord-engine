namespace Nord.Engine.Input;

public class InputActionActive<T> : IInputActionEvent where T : IInputAction
{
    public IInputAction InputAction { get; }

    public InputActionActive(IInputAction inputAction)
    {
        InputAction = inputAction;
    }
}