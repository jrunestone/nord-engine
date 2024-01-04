namespace Nord.Engine.Input.ActionMaps;

public class InputActionActivated<T> : IInputActionEvent<T> where T : IInputAction
{
    public T InputAction { get; }

    public InputActionActivated(T inputAction)
    {
        InputAction = inputAction;
    }
}