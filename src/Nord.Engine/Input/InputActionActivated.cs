namespace Nord.Engine.Input;

public class InputActionActivated<T> : IInputActionEvent where T : IInputAction
{
    public IInputAction InputAction { get; }

    public InputActionActivated(IInputAction inputAction)
    {
        InputAction = inputAction;
    }
}