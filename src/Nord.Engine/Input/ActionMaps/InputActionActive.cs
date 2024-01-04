namespace Nord.Engine.Input.ActionMaps;

public class InputActionActive<T> : IInputActionEvent<T> where T : IInputAction
{
    public T InputAction { get; }

    public InputActionActive(T inputAction)
    {
        InputAction = inputAction;
    }
}