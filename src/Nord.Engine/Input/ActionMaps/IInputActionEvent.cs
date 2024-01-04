namespace Nord.Engine.Input.ActionMaps;

public interface IInputActionEvent
{
    
}

public interface IInputActionEvent<T> : IInputActionEvent 
    where T : IInputAction
{
    T InputAction { get; }
}